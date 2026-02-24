import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarpetEstimate, Floor, FloorplanProject } from '../../models/floorplan/floorplan.model';

@Injectable({
  providedIn: 'root'
})
export class FloorplanService {
  private http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5001/api';

  createProject(name: string, reference?: string): Observable<FloorplanProject> {
    return this.http.post<FloorplanProject>(`${this.apiUrl}/projects`, {
      name,
      reference,
      measurementUnit: 'ft'
    });
  }

  getProject(projectId: string): Observable<FloorplanProject> {
    return this.http.get<FloorplanProject>(`${this.apiUrl}/projects/${projectId}`);
  }

  duplicateFloor(floorId: string): Observable<Floor> {
    return this.http.post<Floor>(`${this.apiUrl}/floorplans/${floorId}/duplicate`, {});
  }

  getCarpetEstimate(projectId: string): Observable<CarpetEstimate> {
    return this.http.get<CarpetEstimate>(`${this.apiUrl}/projects/${projectId}/estimates/carpet`);
  }
}
