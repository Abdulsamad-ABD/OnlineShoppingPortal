import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../../core/services/shop.service';
import { Product } from '../../../shared/models/products';
import { ProductItemComponent } from "../product-item/product-item.component";
import { MatAnchor, MatButton, MatIconButton } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from '../filters-dialog/filters-dialog.component';
import { MatMenuTrigger, MatMenu } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../../shared/models/shopParams';
import { Pagination } from '../../../shared/models/pagination';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent,
    MatAnchor,
    MatIcon,
    MatButton,
    MatMenuTrigger,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatPaginator,
    FormsModule,
    MatIconButton
],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css',
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService)
  protected dialogService = inject(MatDialog)
  shopParams = new ShopParams()
  products?: Pagination<Product>
 
  sortOptions = [
    {name: 'Alphabetic', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'}
  ]

  pageOptions = [5,10,15,20]

  ngOnInit(): void {
    this.initializeShop()
  }

  initializeShop(){
     this.shopService.getBrands();
     this.shopService.getTypes();
     this.getProducts();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.log(error) 
    })
  }

  handlePageEvent(event: PageEvent){
    this.shopParams.pageNumber = event.pageIndex + 1
    this.shopParams.pageSize = event.pageSize
    this.getProducts()
  }

  onSortChange(event: MatSelectionListChange){
    const selectedOtion = event.options[0]
    if(selectedOtion){
      this.shopParams.sort = selectedOtion.value
      this.shopParams.pageNumber = 1
      this.getProducts()
      console.log(this.shopParams.sort)
    }
  }

  onSearchChange(){
    this.shopParams.pageNumber = 1
    this.getProducts()
  }

  openFiltersDialog(){
    const dialogRef = this.dialogService.open(FiltersDialogComponent,{
      minWidth:'500px',
      data:{
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    })
    dialogRef.afterClosed().subscribe({
      next: result => {
        if(result){
          console.log(result),
          this.shopParams.brands = result.selectedBrands,
          this.shopParams.pageNumber = 1
          this.shopParams.types = result.selectedTypes
          this.getProducts()
        }
      }
    })
  }

}
