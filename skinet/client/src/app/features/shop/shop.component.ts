import { Component, inject } from '@angular/core';
import { ShopService } from '../../Core/services/shop.service';
import { Product } from '../../Shared/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';
import { ShopParams } from '../../Shared/shopParams';
import { Pagination } from '../../Shared/pagination';


@Component({
  selector: 'app-shop',
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    FormsModule,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent {
  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  products?: Pagination<Product>;
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'},
  ]
  shopParams = new ShopParams();
  pageSizeOptions = [5,10,15,20]

  ngOnInit() {
    this.initialiseShop();
  }
  
  initialiseShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
    console.log("Welcome to world");
  }

  getProducts() {
    console.log(this.shopParams)
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.error(error)
    })
    console.log(this.products)
    console.log("welcome to get product");
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }

  openFiltersDialog() {
    console.log("welcome to fileter");
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });
    console.log("welcome to close log");
      dialogRef.afterClosed().subscribe({
      next: result => {
        if (result) {
          console.log(result.selectedBrands);
          console.log(result.selectedTypes);
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber = 1;
          console.log(this.shopParams.brands);
          console.log(this.shopParams.types);
          this.getProducts();
        }
      }
    })
  }
}
