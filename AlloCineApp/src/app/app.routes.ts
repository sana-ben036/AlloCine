import { Routes } from '@angular/router';
import { AjoutComponent } from './ajout/ajout.component';
import { ListComponent } from './list/list.component';
import { NotFoundComponent } from './not-found/not-found.component';

export const routes: Routes = [

{path:'', redirectTo:'/list', pathMatch:'full'},
{path: 'ajout', component: AjoutComponent},
{path: 'list', component: ListComponent},

{path:'**', component: NotFoundComponent}

];
