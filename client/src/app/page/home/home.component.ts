import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RestaurantListComponent } from '../restaurant-list/restaurant-list.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  standalone: true,
  imports: [CommonModule, RestaurantListComponent]
})
export class HomeComponent {
  // HomeComponent logic
}
