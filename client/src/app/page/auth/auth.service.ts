import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7238/api/auth';

  constructor(private http: HttpClient) { }

  register(email: string, password: string, username: string): Observable<any> {
    console.log('🔥 Wysyłam rejestrację:', { email, password, username });
    return this.http.post(`${this.apiUrl}/register`, { email, password, username }).pipe(
      catchError(error => {
        console.error('❌ Błąd rejestracji:', error);
        return throwError(error);
      })
    );
  }

  login(emailOrUsername: string, password: string): Observable<any> {
    console.log('🔥 Wysyłam logowanie:', { emailOrUsername, password });
    return this.http.post(`${this.apiUrl}/login`, { emailOrUsername, password }).pipe(
      map((response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('username', response.username); // Zapisz nazwę użytkownika
        return response;
      }),
      catchError(error => {
        console.error('❌ Błąd logowania:', error);
        return throwError(error);
      })
    );
  }

}
