import { Component, input, Input } from '@angular/core';
import { Product } from '../../../shared/models/products';
import { MatCard, MatCardActions, MatCardContent } from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { CurrencyPipe } from '@angular/common';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-product-item',
  imports: [
    MatCard,
    MatIcon,
    MatButton,
    MatCardContent,
    MatCardActions,
    CurrencyPipe,
    RouterLink
],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css',
})
export class ProductItemComponent {
  @Input() product?: Product
}
