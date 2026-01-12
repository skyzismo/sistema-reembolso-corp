import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DespesaService, Despesa } from '../../services/despesa';

@Component({
  selector: 'app-despesas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './despesas.html'
})
export class DespesasComponent implements OnInit {

  despesas: Despesa[] = [];

  novaDespesa: Despesa = {
    tipo: 1,
    valor: 0,
    data: ''
  };

  constructor(private service: DespesaService) {}

  ngOnInit(): void {
    this.carregar();
  }

  carregar() {
    this.service.listar().subscribe(res => this.despesas = res);
  }

  salvar() {
    this.service.criar(this.novaDespesa).subscribe({
      next: () => {
        this.carregar();
        this.novaDespesa = { tipo: 1, valor: 0, data: '' };
      },
      error: err => alert(err.error)
    });
  }
}
