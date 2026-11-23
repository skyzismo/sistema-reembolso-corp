import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { DespesaService } from '../../services/despesa';
import { TipoDespesa } from '../../models/despesa.model';
@Component({ selector: 'app-formulario', standalone: true, imports: [CommonModule, FormsModule, RouterModule], templateUrl: './formulario.html' })
export class FormularioComponent {
  tipo = TipoDespesa.Alimentacao; valor: number | null = null; data = '';
  erro = ''; sucesso = ''; carregando = false;
  hoje = new Date().toISOString().slice(0, 10);
  constructor(private service: DespesaService) {}
  salvar(form: NgForm): void {
    this.erro = ''; this.sucesso = '';
    if (form.invalid || this.valor === null || this.valor <= 0 || !this.data || this.data > this.hoje) {
      this.erro = 'Confira os campos. Informe valor positivo e uma data válida, sem datas futuras.'; return;
    }
    this.carregando = true;
    this.service.criar({ tipo: this.tipo, valor: this.valor, data: `${this.data}T12:00:00` }).subscribe({
      next: () => { this.carregando = false; this.sucesso = 'Despesa cadastrada com sucesso!'; form.resetForm(); this.tipo = TipoDespesa.Alimentacao; },
      error: err => { this.carregando = false; this.erro = typeof err.error === 'string' ? err.error : (err.error?.mensagem ?? 'Não foi possível cadastrar a despesa.'); }
    });
  }

  abrirCalendario(event: Event): void {
  const campo = event.target as HTMLInputElement;

  if (typeof campo.showPicker === 'function') {
    campo.showPicker();
  }
}
}
