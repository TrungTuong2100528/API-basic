import { Component } from '@angular/core';
import {  ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../Core/services/auth.service';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: [''],
      password: [''],
      confirmPassword: ['']
    });
  }

  onSubmit() {

    if (this.form.value.password !== this.form.value.confirmPassword) {
      alert('Mật khẩu không khớp');
      return;
    }

    this.auth.register(this.form.value).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token); // 🔥 auto login luôn
        this.router.navigate(['/']);
      },
      error: () => {
        alert('Đăng ký thất bại');
      }
    });
  }
}
