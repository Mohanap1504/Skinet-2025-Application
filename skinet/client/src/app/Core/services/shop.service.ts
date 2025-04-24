import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../Shared/pagination';
import { Product } from '../../Shared/product';
import { ShopParams } from '../../Shared/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

   baseUrl = 'https://localhost:5161/api/'
   private http = inject(HttpClient);
   types: string[] = [];
   brands: string[] = [];

   getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    console.log(shopParams.brands.length);
    console.log(shopParams.types.length);
    if(shopParams.brands.length > 0)
    {
      shopParams.brands.forEach(b => params = params.append('brand', b));
    }
    if(shopParams.types.length > 0)
    {
        shopParams.types.forEach(t => params = params.append('type', t));
        console.log(params);
    }

    if(shopParams.sort){
      params = params.append('sort', shopParams.sort);
    }

    if(shopParams.search)
    {
      params = params.append('search', shopParams.search);
    }

     params = params.append('pageSize', shopParams.pageSize);
     params = params.append('pageIndex', shopParams.pageNumber);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'product', {params})

   }
    
  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'product/' + id);
  }
   getBrands() {
    if (this.brands.length > 0) return;
     return this.http.get<string[]>(this.baseUrl + 'product/brands').subscribe({
       next: response => this.brands = response,
     })
   }

   getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'product/types').subscribe({
      next: response => this.types = response,
    })
  }
}
