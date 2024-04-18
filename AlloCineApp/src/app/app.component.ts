import { Component, NgModule } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AjoutComponent } from './ajout/ajout.component';
import { ListComponent } from './list/list.component';
import { HeaderComponent } from './header/header.component';




@Component({
  selector: 'app-root',
  standalone: true,
  imports: 
  [
    RouterOutlet,
    AjoutComponent,
    ListComponent,
    HeaderComponent
    
    
    
    
    
    
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'AlloCineApp';
  
}
