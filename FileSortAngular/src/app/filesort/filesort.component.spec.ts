import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FilesortComponent } from './filesort.component';

describe('FilesortComponent', () => {
  let component: FilesortComponent;
  let fixture: ComponentFixture<FilesortComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FilesortComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FilesortComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
