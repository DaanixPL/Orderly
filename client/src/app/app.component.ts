import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './page/header/header.component';
import { FooterComponent } from './page/footer/footer.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, FooterComponent]
})
export class AppComponent {
  title = 'client';
}
