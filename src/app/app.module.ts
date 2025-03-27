import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routes';

import { AppComponent } from './app.component';
import { HeaderComponent } from './page/header/header.component';
import { HomeComponent } from './page/home/home.component';
import { RestaurantListComponent } from './page/restaurant-list/restaurant-list.component';
import { ContactComponent } from './page/contact/contact.component';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    // Import standalone components instead of declaring them:
    AppComponent,
    HeaderComponent,
    HomeComponent,
    RestaurantListComponent,
    ContactComponent
  ],
  providers: [],
  bootstrap: [AppComponent] // Main application component
})
export class AppModule { }
