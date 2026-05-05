import { Component, inject } from '@angular/core';
import { RouterOutlet, RouterLinkWithHref, RouterModule, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ChatAIComponent } from './Features/chat-ai/chat-ai.component';
import { CommonModule } from '@angular/common';
import { AuthService } from './Core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLinkWithHref, RouterModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  private modalService = inject(NgbModal);

  role: string | null = null;
  isLoggedIn = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.authService.isLoggedIn$.subscribe(status => {

      this.isLoggedIn = status;

      if (status) {
        this.role = this.authService.getRole(); //  lấy role
      }
    });
  }

  logout(): void {

    this.authService.logout();
    this.role = null;
    this.router.navigate(['/login']);
  }

  toggleChat(): void {

    this.modalService.open(ChatAIComponent, {
      size: 'lg'
    });
  }

  get isAdmin() {
    return this.role === 'Admin';
  }

  get isStaff() {
    return this.role === 'Staff';
  }
}
