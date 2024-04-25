import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor(private http: HttpClient) { }

  private url = 'https://localhost:44328/'



  CreateNewMovie(movie: any){
    return this.http.post(this.url + 'api/movies', movie);
  }

  GetAllMovies(){

   
    //return this.http.get(this.url + 'api/movies');
    return fetch(this.url + 'api/movies');
   
  }


  GetMovie(id : any){

   
    return this.http.get(this.url + 'api/movies/' +id);

   
  }

  /*GetAllCategories(){

   
    return this.http.get(this.url + 'api/categories');

   
  }*/

  


  /*getImage(imageUrl: string): Observable<Blob> {
    return this.http.get(imageUrl, { responseType: 'blob' });
  }*/

  
  
}
