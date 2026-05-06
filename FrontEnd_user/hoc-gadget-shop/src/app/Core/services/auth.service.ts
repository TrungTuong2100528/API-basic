import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private API = `${environment.apiUrl}/api/auth`;

  // trạng thái login realtime
  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());

  isLoggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) { }

  login(data: any) {
    return this.http.post<any>(`${this.API}/login`, data);
  }

  register(data: any) {
    return this.http.post<any>(`${this.API}/register`, data);
  }

  saveToken(token: string) {

    localStorage.setItem('token', token);

    this.loggedIn.next(true);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  logout() {

    localStorage.removeItem('token');

    this.loggedIn.next(false);
  }

  isLoggedIn() {
    return this.loggedIn.value;
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }
  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    const payload = JSON.parse(atob(token.split('.')[1]));

    return payload.role;
  }
}