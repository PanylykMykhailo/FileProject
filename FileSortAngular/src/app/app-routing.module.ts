import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FilesortComponent } from './filesort/filesort.component';

const routes: Routes = [
  {path:'filesort',component:FilesortComponent},
  {path:'filesort/file',component:FilesortComponent},
  {path:'filesort/video',component:FilesortComponent},
  {path:'filesort/photo',component:FilesortComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
