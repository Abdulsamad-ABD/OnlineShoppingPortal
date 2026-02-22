import { Component, inject } from '@angular/core';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { ShopService } from '../../../core/services/shop.service';
import { MatAnchor } from "@angular/material/button";
import { MatIcon } from "@angular/material/icon";
import { FormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-filters-dialog',
  imports: [
    MatDivider,
    MatSelectionList,
    MatListOption,
    MatAnchor,
    FormsModule
],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.css',
})
export class FiltersDialogComponent {
 protected shopService = inject(ShopService)
 protected dialogRef = inject(MatDialogRef<FiltersDialogComponent>)
 data = inject(MAT_DIALOG_DATA)
 selectedBrands = this.data.selectedBrands
 selectedTypes = this.data.selectedTypes
 
 applyFilters(){
  this.dialogRef.close({
    selectedBrands: this.selectedBrands,
    selectedTypes: this.selectedTypes
  })
 }
}
