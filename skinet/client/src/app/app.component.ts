import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./Layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './Shared/product';
import { Pagination } from './Shared/pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements  OnInit{
  baseUrl = 'https://localhost:5161/api/'
  private http = inject(HttpClient);
  title = 'Skinet';
  products: Product[] = [];

  ngOnInit(): void {
    this.http.get<Pagination<Product>>(this.baseUrl + 'product').subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error),
      complete: () => console.log('complete')
    })
  }
}
