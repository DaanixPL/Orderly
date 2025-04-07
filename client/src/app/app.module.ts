import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { AppRoutingModule } from './app.routes';

import { AppComponent } from './app.component';
import { HomeComponent } from './page/home/home.component';
import { HeaderComponent } from './page/header/header.component';
import { RestaurantListComponent } from './page/restaurant-list/restaurant-list.component';
import { AuthComponent } from './page/auth/auth.component';
import { CartComponent } from './page/cart/cart.component';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule,
    AppComponent,
    HomeComponent,
    HeaderComponent,
    RestaurantListComponent,
    AuthComponent,
    CartComponent,
  ],

  providers: [
    provideHttpClient()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
