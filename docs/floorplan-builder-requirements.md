# Floorplan Builder Requirements (Web Template Architecture)

## 1. Purpose
Define product and technical requirements for adding a **floorplan builder** to the current web-based starter architecture (.NET Web API + Angular SPA + SQL Server).

The feature set must support:
- Importing survey images.
- AI-assisted text extraction for dimensions and labels from the survey.
- Interactive placement/editing of walls and room labels (including bathrooms and half-bathrooms).
- Saving plans as editable, layerable artifacts.
- Copying first-floor plans into second-floor plans.
- Switching between floors.
- Estimating carpet square footage.

---

## 2. Scope

### 2.1 In Scope
- Two-dimensional floorplan editor in browser.
- Support for at least two floors per project (first and second floor).
- AI OCR pipeline for extracting text from uploaded survey images.
- Manual correction of AI results.
- Persistent storage of project data, floor layers, and plan objects.
- Carpet area estimation reports by room and by floor.

### 2.2 Out of Scope (Phase 1)
- 3D visualization.
- Structural engineering validation.
- Permit generation.
- Real-time multi-user collaboration.
- Native mobile apps.

---

## 3. User Roles
- **Designer/User**: Creates and edits floorplans, runs carpet estimates.
- **Admin (optional future role)**: Manages templates, room catalogs, and usage analytics.

---

## 4. Core User Stories
1. As a user, I can create a project and upload one or more survey images.
2. As a user, I can run AI extraction to detect dimensions/text from survey images.
3. As a user, I can accept, edit, or remove AI-detected annotations.
4. As a user, I can draw walls and place room labels (bathroom, half-bathroom, dining room, and other room types).
5. As a user, I can manage drawing layers (e.g., image, AI annotations, walls, rooms, measurements).
6. As a user, I can save and reopen a project with all layers editable.
7. As a user, I can duplicate the first floor to create a second-floor starting point.
8. As a user, I can switch between first and second floor and continue editing independently.
9. As a user, I can generate carpet square-foot estimates per room/floor and totals.
10. As a user, I can export an image snapshot while preserving editable source data in the system.

---

## 5. Functional Requirements

### 5.1 Project & File Management
- FR-001: The system shall allow project creation with metadata (name, address/reference, units).
- FR-002: The system shall support survey image upload (JPG, PNG, PDF rasterized server-side).
- FR-003: The system shall version plan saves (at least latest + previous revision for rollback).

### 5.2 AI Text/Dimension Extraction
- FR-010: The system shall provide an endpoint to process uploaded survey images with OCR.
- FR-011: OCR output shall include detected text, confidence, and bounding coordinates.
- FR-012: The UI shall render OCR results as an editable annotation layer.
- FR-013: Users shall be able to map detected dimensions to wall segments.
- FR-014: Users shall be able to manually add/edit/remove extracted dimensions.

### 5.3 Floorplan Editing
- FR-020: The editor shall support wall creation, selection, movement, and deletion.
- FR-021: The editor shall support room placement/labeling for:
  - Bathroom
  - Half-bathroom
  - Dining room
  - Bedroom
  - Living room
  - Kitchen
  - Other custom labels
- FR-022: The editor shall support snapping and grid alignment (configurable).
- FR-023: The editor shall support an ordered layer model:
  1. Survey image layer
  2. OCR annotation layer
  3. Wall geometry layer
  4. Room label/shape layer
  5. Measurement layer
- FR-024: Each layer shall support visibility toggle and lock/unlock.

### 5.4 Multi-Floor Support
- FR-030: A project shall support at least two floors.
- FR-031: Users shall be able to duplicate Floor 1 into Floor 2 with preserved geometry/labels.
- FR-032: Floor switching shall update the active canvas and related metadata without page reload.
- FR-033: Floor data shall be persisted independently after duplication.

### 5.5 Saving, Reloading, and Reuse
- FR-040: The system shall persist plans as editable JSON-based vector data plus asset references.
- FR-041: The system shall allow reloading of saved projects and restoration of all layers.
- FR-042: The system shall support cloning an existing project as a new project.

### 5.6 Carpet Estimation
- FR-050: The system shall compute room area in square feet based on room polygon geometry.
- FR-051: The system shall aggregate carpet estimates by floor and entire project.
- FR-052: Users shall be able to include/exclude room types from carpet totals.
- FR-053: The estimate report shall display assumptions (units, rounding, excluded rooms).

---

## 6. Non-Functional Requirements
- NFR-001: Editor interactions (drag/move/draw) should respond within 100ms in typical plans (<500 objects).
- NFR-002: Save operations should complete within 2s under normal load.
- NFR-003: API endpoints shall require authenticated access (JWT/session).
- NFR-004: Uploaded images and project data shall be access-scoped per user/tenant.
- NFR-005: OCR processing failures shall return actionable errors and preserve original upload.
- NFR-006: System shall provide audit fields (created/updated timestamps, user).

---

## 7. Data Model (Proposed)

### 7.1 Backend Entities (Clean Architecture)
- `Project`
  - `Id`, `Name`, `Reference`, `MeasurementUnit`, `CreatedAt`, `UpdatedAt`
- `FloorPlan`
  - `Id`, `ProjectId`, `FloorNumber` (1/2), `Name`, `CanvasWidth`, `CanvasHeight`
- `Layer`
  - `Id`, `FloorPlanId`, `Type`, `Visible`, `Locked`, `ZIndex`
- `SurveyAsset`
  - `Id`, `ProjectId`, `FileName`, `StoragePath`, `ContentType`, `Width`, `Height`
- `PlanObject`
  - `Id`, `FloorPlanId`, `ObjectType` (`Wall`, `Room`, `Annotation`, `Measurement`), `GeometryJson`, `StyleJson`
- `OcrAnnotation`
  - `Id`, `SurveyAssetId`, `Text`, `Confidence`, `BoundingBoxJson`, `MappedPlanObjectId?`
- `EstimateReport`
  - `Id`, `ProjectId`, `FloorPlanId?`, `TotalSqFt`, `IncludedRoomTypesJson`, `GeneratedAt`

### 7.2 Frontend View Models
- `FloorPlanState`
  - active floor id, selected tool, zoom/pan, undo/redo stacks
- `LayerState`
  - visibility, lock status, object ids
- `EstimateViewModel`
  - per-room rows + floor subtotal + project total

---

## 8. API Requirements (REST, .NET Web API)
- `POST /api/projects`
- `GET /api/projects/{projectId}`
- `POST /api/projects/{projectId}/survey-assets`
- `POST /api/survey-assets/{assetId}/ocr`
- `GET /api/floorplans/{floorPlanId}`
- `PUT /api/floorplans/{floorPlanId}` (full save)
- `POST /api/floorplans/{floorPlanId}/duplicate` (for second floor)
- `POST /api/projects/{projectId}/estimates/carpet`
- `GET /api/projects/{projectId}/estimates/carpet`

Response contracts should be versioned and include validation errors in a consistent format.

---

## 9. Frontend Requirements (Angular)
- Canvas-based editor component (SVG or HTML5 Canvas abstraction).
- Tool palette: select, wall draw, room label, measurement, pan.
- Layer panel with hide/show + lock controls.
- Floor selector toggle (Floor 1 / Floor 2).
- OCR review panel listing detected text/confidence with jump-to-annotation behavior.
- Estimate panel with filter controls for includable room types.

---

## 10. Acceptance Criteria (MVP)
1. User can upload a survey image and trigger OCR.
2. OCR annotations appear on canvas and can be edited/deleted.
3. User can draw walls and place room labels including bathroom, half-bathroom, dining room.
4. User can save and reload project with all layer/object data intact.
5. User can duplicate Floor 1 to Floor 2 and switch between floors.
6. User can generate carpet square-foot estimate with per-floor and total values.

---

## 11. Delivery Plan (Suggested)
- **Phase 1**: Project model + floor model + canvas basics + manual room/wall editing.
- **Phase 2**: OCR integration + annotation editing + dimension mapping.
- **Phase 3**: Carpet estimation engine + reporting + exports.
- **Phase 4**: Hardening (auth, performance, audit, test coverage).

---

## 12. Testing Requirements
- Backend unit tests for estimate calculations and floor duplication logic.
- Backend integration tests for project save/load and OCR endpoint contracts.
- Frontend unit tests for editor state reducers/services.
- Frontend E2E tests for:
  - Upload → OCR review → edit annotations
  - Draw/edit walls and rooms
  - Duplicate floor and switch floors
  - Generate and view carpet estimate
