import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items: any[] = [];

  constructor() {
    const saved = localStorage.getItem('cart');
    if (saved) {
      this.items = JSON.parse(saved);
    }
  }

  addItem(item: any): void {
    this.items.push(item);
    localStorage.setItem('cart', JSON.stringify(this.items));
  }

  getItems(): any[] {
    return this.items;
  }

  clearCart(): void {
    this.items = [];
    localStorage.removeItem('cart');
  }

  getCount(): number {
    return this.items.length;
  }

  removeItem(item: any): void {
    this.items = this.items.filter(i => i !== item);
    localStorage.setItem('cart', JSON.stringify(this.items));
  }

}
