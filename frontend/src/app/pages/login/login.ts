import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  standalone: true, selector: 'app-login', imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.html'
})
export class LoginComponent {
  email = '';
  senha = '';
  erro = '';
  carregando = false;
  constructor(private auth: AuthService, private router: Router) {}
  entrar(): void {
    this.erro = '';
    this.carregando = true;
    this.auth.login({ email: this.email.trim(), senha: this.senha }).subscribe({
      next: () => { this.carregando = false; this.router.navigate(['/']); },
      error: err => {
        this.carregando = false;
        this.erro = err.status === 401 ? 'E-mail ou senha inválidos.' : 'Não foi possível conectar à API. Verifique se o backend está em execução.';
      }
    });
  }
}
