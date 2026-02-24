export interface FloorplanProject {
  id: string;
  name: string;
  reference?: string;
  measurementUnit: string;
  floors: Floor[];
}

export interface Floor {
  id: string;
  floorNumber: number;
  name: string;
  layers: FloorLayer[];
  objects: PlanObject[];
}

export interface FloorLayer {
  id: string;
  type: 'SurveyImage' | 'OcrAnnotations' | 'Walls' | 'Rooms' | 'Measurements' | string;
  visible: boolean;
  locked: boolean;
  zIndex: number;
}

export interface PlanObject {
  id: string;
  objectType: 'Wall' | 'Room' | 'Annotation' | 'Measurement' | string;
  label: string;
  geometryJson: string;
}

export interface CarpetEstimate {
  projectId: string;
  totalSquareFeet: number;
  rows: CarpetEstimateRow[];
}

export interface CarpetEstimateRow {
  floorNumber: number;
  roomLabel: string;
  squareFeet: number;
}
