import { Component, NgModule } from '@angular/core';

import { AjoutComponent } from './ajout/ajout.component';
import { ListComponent } from './list/list.component';
import { HeaderComponent } from './header/header.component';
import { AsideComponent } from './aside/aside.component';
import { ContainerComponent } from './container/container.component';





@Component({
  selector: 'app-root',
  standalone: true,
  imports: 
  [
    
    AjoutComponent,
    ListComponent,
    AsideComponent,
    ContainerComponent,
    HeaderComponent
    
    
    
    
    
    
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AlloCineApp';
  
}
