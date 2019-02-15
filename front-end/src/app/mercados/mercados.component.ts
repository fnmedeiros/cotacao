import { Component, OnInit } from '@angular/core';
import { Mercado } from '../mercado';
import { MERCADOS } from '../mock-mercados';

@Component({
  selector: 'app-mercados',
  templateUrl: './mercados.component.html',
  styleUrls: ['./mercados.component.css']
})
export class MercadosComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  mercados = MERCADOS;
}
