import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  standalone: true
})
export class HeaderComponent implements OnInit {
  username: string = '';

  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.username = localStorage.getItem('username') || '';
  }

  get isLoggedIn(): boolean {
    return localStorage.getItem('token') !== null; // Sprawdzanie tokena
  }

  navigateTo(path: string) {
    this.router.navigate([path]);
  }
}
