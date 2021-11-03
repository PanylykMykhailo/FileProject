import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FilesortComponent } from './filesort/filesort.component';

const routes: Routes = [
  {path:'filesort',component:FilesortComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
