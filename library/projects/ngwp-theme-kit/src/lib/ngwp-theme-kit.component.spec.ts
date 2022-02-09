import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NgwpThemeKitComponent } from './ngwp-theme-kit.component';

describe('NgwpThemeKitComponent', () => {
  let component: NgwpThemeKitComponent;
  let fixture: ComponentFixture<NgwpThemeKitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NgwpThemeKitComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NgwpThemeKitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
