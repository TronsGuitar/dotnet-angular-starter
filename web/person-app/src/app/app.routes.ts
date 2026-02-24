import { Routes } from '@angular/router';
import { PersonListComponent } from './components/person-list/person-list.component';
import { PersonFormComponent } from './components/person-form/person-form.component';
import { FloorplanBuilderComponent } from './components/floorplan-builder/floorplan-builder.component';

export const routes: Routes = [
  { path: '', redirectTo: '/persons', pathMatch: 'full' },
  { path: 'persons', component: PersonListComponent },
  { path: 'persons/new', component: PersonFormComponent },
  { path: 'persons/edit/:id', component: PersonFormComponent },
  { path: 'floorplans', component: FloorplanBuilderComponent },
  { path: '**', redirectTo: '/persons' }
];
