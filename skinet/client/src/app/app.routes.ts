import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductdetailsComponent } from './features/shop/productdetails/productdetails.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { NotFoundComponent } from './Shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './Shared/components/server-error/server-error.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path:'shop', component: ShopComponent},
    {path:'shop/:id', component: ProductdetailsComponent},
    {path:'test-error', component: TestErrorComponent},
    {path:'not-found', component: NotFoundComponent},
    {path:'server-error', component: ServerErrorComponent},
    {path:'**', redirectTo: 'not-found', pathMatch: 'full'},

];
