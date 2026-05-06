import { Component } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../Core/services/auth.service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      email: [''],
      password: ['']
    });
  }

  onSubmit() {
    this.auth.login(this.form.value).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token); // 🔥 lưu token
        this.router.navigate(['/']);    // 🔥 về home
      },
      error: () => {
        alert('Sai tài khoản hoặc mật khẩu');
      }
    });
  }
}
