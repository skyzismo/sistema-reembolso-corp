import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse, PerfilUsuario } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly apiUrl = 'http://localhost:5124/api/Auth';
  private readonly storageKey = 'reembolso.auth';
  usuario = signal<LoginResponse | null>(this.lerSessao());

  constructor(private http: HttpClient) {}

  login(dados: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, dados).pipe(
      tap(resposta => {
        localStorage.setItem(this.storageKey, JSON.stringify(resposta));
        this.usuario.set(resposta);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.storageKey);
    this.usuario.set(null);
  }

  get token(): string | null { return this.usuario()?.token ?? null; }
  get perfil(): PerfilUsuario | null { return this.usuario()?.perfil ?? null; }
  get nome(): string { return this.usuario()?.nome ?? ''; }
  get autenticado(): boolean {
    const sessao = this.usuario();
    return !!sessao?.token && new Date(sessao.expiraEm).getTime() > Date.now();
  }
  get gestor(): boolean { return this.autenticado && this.perfil === 'Gestor'; }

  private lerSessao(): LoginResponse | null {
    try {
      const valor = localStorage.getItem(this.storageKey);
      if (!valor) return null;
      const sessao = JSON.parse(valor) as LoginResponse;
      return new Date(sessao.expiraEm).getTime() > Date.now() ? sessao : null;
    } catch { return null; }
  }
}
