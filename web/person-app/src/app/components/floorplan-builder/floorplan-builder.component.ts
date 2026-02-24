import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Floor, FloorplanProject } from '../../models/floorplan/floorplan.model';
import { FloorplanService } from '../../services/floorplan/floorplan.service';

@Component({
  selector: 'app-floorplan-builder',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './floorplan-builder.component.html',
  styleUrl: './floorplan-builder.component.css'
})
export class FloorplanBuilderComponent {
  private floorplanService = inject(FloorplanService);

  project?: FloorplanProject;
  activeFloor?: Floor;
  projectName = 'New Floorplan Project';
  error = '';

  createStarterProject(): void {
    this.error = '';
    this.floorplanService.createProject(this.projectName).subscribe({
      next: (project) => {
        this.project = project;
        this.activeFloor = project.floors[0];
      },
      error: () => {
        this.error = 'Unable to create floorplan project. Verify API is running.';
      }
    });
  }

  switchFloor(floor: Floor): void {
    this.activeFloor = floor;
  }

  duplicateActiveFloor(): void {
    if (!this.activeFloor || !this.project) {
      return;
    }

    this.floorplanService.duplicateFloor(this.activeFloor.id).subscribe({
      next: (newFloor) => {
        this.project = {
          ...this.project!,
          floors: [...this.project!.floors, newFloor]
        };
        this.activeFloor = newFloor;
      },
      error: () => {
        this.error = 'Unable to duplicate floor right now.';
      }
    });
  }
}
