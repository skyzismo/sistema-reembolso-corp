import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DespesaService } from '../../services/despesa';
import { Despesa, TipoDespesa } from '../../models/despesa.model';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-formulario',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './formulario.html'
})
export class FormularioComponent {

  novaDespesa: Despesa = {
    tipo: TipoDespesa.Alimentacao,
    valor: 0,
    data: ''
  };

  constructor(private service: DespesaService) {}

salvar(form: NgForm) {

  if (!this.novaDespesa.valor || this.novaDespesa.valor <= 0) {
    alert('Informe um valor maior que zero');
    return;
  }

  if (!this.novaDespesa.data) {
    alert('Informe a data da despesa');
    return;
  }

  const dataInformada = new Date(this.novaDespesa.data);
  const hoje = new Date();

  if (dataInformada > hoje) {
    alert('Não é permitido cadastrar despesas com data futura');
    return;
  }

  this.service.criar(this.novaDespesa).subscribe({
    next: () => {
      alert('Despesa cadastrada com sucesso');

      // 🔥 ISSO RESOLVE O SEU PROBLEMA
      form.resetForm({
        tipo: 1,
        valor: null,
        data: ''
      });
    },
    error: err => {
      alert(err.error?.mensagem || err.error || 'Erro ao cadastrar despesa');
    }
  });
}

}
