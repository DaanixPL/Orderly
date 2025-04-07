import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styleUrls: ['./restaurant.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class RestaurantComponent implements OnInit {
  restaurant: any;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private cartService: CartService
  ) { }

  addToCart(item: any) {
    console.log('Item added:', item);
    this.cartService.addItem(item);
    alert('Added to cart!');
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.http.get(`https://localhost:7238/api/restaurants/${id}`).subscribe((data: any) => {
        this.restaurant = data;
      });
    }
  }
}
