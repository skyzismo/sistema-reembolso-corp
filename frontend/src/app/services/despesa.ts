import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CriarDespesaRequest, Despesa } from '../models/despesa.model';
import { CriarUsuarioRequest } from '../models/auth.model';

@Injectable({ providedIn: 'root' })
export class DespesaService {
  private readonly apiUrl = 'http://localhost:5124/api/despesas';
  private readonly usuariosUrl = 'http://localhost:5124/api/usuarios';
  constructor(private http: HttpClient) {}
  listar(): Observable<Despesa[]> { return this.http.get<Despesa[]>(this.apiUrl); }
  criar(despesa: CriarDespesaRequest): Observable<Despesa> {
    return this.http.post<Despesa>(this.apiUrl, despesa);
  }
  aprovar(id: string): Observable<unknown> {
    return this.http.put(`${this.apiUrl}/${id}/aprovar`, {});
  }
  rejeitar(id: string): Observable<unknown> {
    return this.http.put(`${this.apiUrl}/${id}/rejeitar`, {});
  }
  excluir(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  criarUsuario(usuario: CriarUsuarioRequest): Observable<unknown> {
    return this.http.post(this.usuariosUrl, usuario);
  }
}
