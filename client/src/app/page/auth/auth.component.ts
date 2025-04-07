import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent {
  isLogin: boolean = true;
  isLoading: boolean = false;
  errorMessage: string = '';

  // Login properties
  emailOrUsername: string = '';
  password: string = '';

  // Registration properties
  email: string = '';
  username: string = '';
  confirmPassword: string = '';

  constructor(private authService: AuthService) { }

  toggleMode(): void {
    this.isLogin = !this.isLogin;
    this.errorMessage = '';
  }
  isValidEmail(email: string): boolean {
    const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailPattern.test(email);
  }

  onSubmit(): void {
    this.errorMessage = '';

    if (this.isLogin) {
      // Login flow
      this.emailOrUsername = this.emailOrUsername.trim();
      this.password = this.password.trim();

      if (!this.emailOrUsername || !this.password) {
        this.errorMessage = 'Complete all fields';
        return;
      }

      this.isLoading = true;
      this.authService.login(this.emailOrUsername, this.password).subscribe({
        next: response => {
          console.log('Logowanie:', response);
          this.isLoading = false;
        },
        error: error => {
          console.error('Błąd logowania:', error);
          this.errorMessage = error.error?.message || 'Wrong email/login or password.';
          this.isLoading = false;
        }
      });
    } else {
      // Registration flow
      this.email = this.email.trim();
      this.username = this.username.trim();
      this.password = this.password.trim();
      this.confirmPassword = this.confirmPassword.trim();

      if (!this.email || !this.password || !this.username) {
        this.errorMessage = 'Complete all fields';
        return;
      }

      if (!this.isValidEmail(this.email)) {
        this.errorMessage = 'Invalid email address format';
        return;
      }

      if (this.password !== this.confirmPassword) {
        this.errorMessage = 'Passwords do not match';
        return;
      }

      this.isLoading = true;
      this.authService.register(this.email, this.password, this.username).subscribe({
        next: response => {
          console.log('Rejestracja:', response);
          this.isLoading = false;
        },
        error: error => {
          console.error('Błąd rejestracji:', error);
          this.errorMessage = error.error?.message || 'Registration failed';
          this.isLoading = false;
        }
      });
    }
  }
}
