import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Despesa {
  id?: string;
  tipo: number;
  valor: number;
  data: string;
  status?: number;
}

@Injectable({
  providedIn: 'root'
})
export class DespesaService {

  private apiUrl = 'http://localhost:5124/api/despesas';

  constructor(private http: HttpClient) {}

  listar(): Observable<Despesa[]> {
    return this.http.get<Despesa[]>(this.apiUrl);
  }

  criar(despesa: Despesa): Observable<Despesa> {
    return this.http.post<Despesa>(this.apiUrl, despesa);
  }
}
