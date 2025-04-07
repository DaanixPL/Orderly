import { Component, OnInit } from '@angular/core';
import { CartService } from './cart.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  styleUrls: ['./cart.component.css'],
  templateUrl: './cart.component.html',
  standalone: true,
  imports: [CommonModule]
})
export class CartComponent implements OnInit {
  items: any[] = [];

  constructor(private cartService: CartService, private router: Router) { }

  ngOnInit(): void {
    this.items = this.cartService.getItems();
  }

  removeFromCart(item: any) {
    this.cartService.removeItem(item);
    this.items = this.cartService.getItems();
  }

  getTotal(): number {
    return this.items.reduce((sum, item) => sum + item.price, 0);
  }

  checkout() {
    const token = localStorage.getItem('token');
    if (!token) {
      this.router.navigate(['/auth']);
      return;
    }

    alert('Order placed!');
    this.cartService.clearCart();
    this.items = [];
  }
}
