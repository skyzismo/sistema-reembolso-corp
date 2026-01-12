import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { DespesaService } from '../../services/despesa';
import { Despesa, TipoDespesa, StatusDespesa } from '../../models/despesa.model';

@Component({
  selector: 'app-painel-gestor',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './painel-gestor.html'
})
export class PainelGestorComponent {

  despesas$!: Observable<Despesa[]>;

  tipos = TipoDespesa;
  status = StatusDespesa;

  constructor(private service: DespesaService) {
    this.despesas$ = this.service.listar();
  }

  tipoLabel(tipo: number) {
    return TipoDespesa[tipo];
  }

  statusLabel(status: number) {
    return StatusDespesa[status];
  }
}
