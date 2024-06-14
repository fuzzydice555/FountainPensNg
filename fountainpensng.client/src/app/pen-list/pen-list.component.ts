import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { PenService } from '../services/pen.service';
import { FountainPen } from '../../dtos/FountainPen';

@Component({
  selector: 'app-pen-list',
  standalone: true,
  imports: [MatTableModule],
  templateUrl: './pen-list.component.html',
  styleUrl: './pen-list.component.css'
})
export class PenListComponent implements OnInit {
  displayedColumns: string[] = ['maker', 'modelName', 'color'];
  dataSource: FountainPen[] = [];

  ngOnInit(): void {
    this.penService.getPens().subscribe({
      next: r => {
        this.dataSource = r
      }
    });
  }
  constructor(private penService: PenService) {

  }
}
