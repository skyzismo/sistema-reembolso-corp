import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DespesaService } from '../../services/despesa';
import { Despesa, TipoDespesa, StatusDespesa } from '../../models/despesa.model';

@Component({
  selector: 'app-despesas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './despesas.html'
})
export class DespesasComponent implements OnInit {

  despesas: Despesa[] = [];

  novaDespesa: Despesa = {
    tipo: TipoDespesa.Alimentacao,
    valor: 0,
    data: ''
  };

  tipos = TipoDespesa;
  status = StatusDespesa;

  constructor(private service: DespesaService) {}

  ngOnInit(): void {
    this.carregar();
  }

  carregar() {
    this.service.listar().subscribe(d => this.despesas = d);
  }

  salvar() {
    this.service.criar(this.novaDespesa).subscribe({
      next: () => {
        this.carregar();
        this.novaDespesa = { tipo: TipoDespesa.Alimentacao, valor: 0, data: '' };
      },
      error: err => {
        alert(err.error?.mensagem || err.error);
      }
    });
  }

  tipoLabel(tipo: number): string {
    return TipoDespesa[tipo];
  }

  statusLabel(status: number): string {
    return StatusDespesa[status];
  }
}
