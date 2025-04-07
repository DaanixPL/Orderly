import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './page/home/home.component';
import { ContactComponent } from './page/contact/contact.component';
import { AuthComponent } from './page/auth/auth.component';
import { RestaurantComponent } from './page/restaurant/restaurant.component';
import { CartComponent } from './page/cart/cart.component';

export const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'contact', component: ContactComponent },
  { path: 'auth', component: AuthComponent },
  { path: 'restaurant/:id', component: RestaurantComponent },
  { path: 'cart', component: CartComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
