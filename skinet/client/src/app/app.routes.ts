import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductdetailsComponent } from './features/shop/productdetails/productdetails.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path:'shop', component: ShopComponent},
    {path:'shop/:id', component: ProductdetailsComponent},
    {path:'**', redirectTo: '', pathMatch: 'full'},

];
