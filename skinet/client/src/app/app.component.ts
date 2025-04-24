import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./Layout/header/header.component";
import { ShopComponent } from "./features/shop/shop.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [HeaderComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Skinet';
}
