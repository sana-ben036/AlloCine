import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SharedService } from '../shared.service';

@Component({
  selector: 'app-ajout',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './ajout.component.html',
  styleUrl: './ajout.component.css'
})
export class AjoutComponent {

  /**
   *
   */
  


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
        console.log(res);
      },
      err=>{
        console.log(err);
      },
    );


  } 


  constructor(public _shared : SharedService) { }
}
