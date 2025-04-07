import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'restaurant-list',
  templateUrl: './restaurant-list.component.html',
  styleUrls: ['./restaurant-list.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class RestaurantListComponent implements OnInit {
  restaurants: any[] = [];
  filteredRestaurants: any[] = [];
  searchTerm: string = '';

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loadRestaurants();
  }

  loadRestaurants() {
    // Fetch restaurants from your API and sort alphabetically by name
    this.http.get('https://localhost:7238/api/restaurants').subscribe((data: any) => {
      // Sprawdzenie, czy odpowiedź zawiera $values jako tablicę
      if (Array.isArray(data.$values)) {
        this.restaurants = data.$values.sort((a: any, b: any) => a.name.localeCompare(b.name));
        this.filteredRestaurants = this.restaurants;
      } else {
        console.error('Expected an array of restaurants');
      }
    }, error => {
      console.error('Error fetching restaurants:', error);
    });
  }


  search(): void {
    if (this.searchTerm.trim() === '') {
      this.filteredRestaurants = this.restaurants;
    } else {
      this.filteredRestaurants = this.restaurants.filter(restaurant =>
        restaurant.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

  openRestaurant(id: number): void {
    // Navigate to the restaurant details page, for example '/restaurant/1'
    this.router.navigate(['/restaurant', id]);
  }
}
