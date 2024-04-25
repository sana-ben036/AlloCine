import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedService } from '../shared.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ajout',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './ajout.component.html',
  styleUrl: './ajout.component.css'
})
export class AjoutComponent {

  /**
   *
   */
  categories: any;

  constructor(public _shared : SharedService) { 
    
   /* this._shared.GetAllCategories()
      .subscribe(
        res=>{
          console.log(res);
          this.categories = res;
          
        },
        err=>{
          console.log(err);
        },
      );*/
  }


  movie = {
    Title:'',
    CategoryId:0,
    Rate:0,
    Poster:'',
    Year:0,
    Storyline:''
  }

  

  

  

  ajout(){
    this._shared.CreateNewMovie(this.movie)
    .subscribe(
      res=>{
        //console.log(res);
        this.movie = {
          Title:'',
          CategoryId:0,
          Rate:0,
          Poster:'',
          Year:0,
          Storyline:''
        }
      },
      err=>{
        console.log(err);
      },
    );


  } 


  
}
