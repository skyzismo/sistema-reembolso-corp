import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PainelGestor } from './painel-gestor';

describe('PainelGestor', () => {
  let component: PainelGestor;
  let fixture: ComponentFixture<PainelGestor>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PainelGestor]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PainelGestor);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
