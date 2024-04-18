import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared.service';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-list',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})
export class ListComponent implements OnInit{
 


  movies: any;

  poster: any;


  /**
   *
   */
  constructor(public _shared : SharedService, private sanitizer: DomSanitizer) {}

  
  ngOnInit(): void {

    

 

    this._shared.GetAllMovies()
      .subscribe(
        res=>{
          console.log(res);
          this.movies = res;
          
        },
        err=>{
          console.log(err);
        },
      );

      


      
  }

  

  
  
}
