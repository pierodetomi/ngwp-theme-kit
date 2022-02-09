import { TestBed } from '@angular/core/testing';

import { NgwpThemeKitService } from './ngwp-theme-kit.service';

describe('NgwpThemeKitService', () => {
  let service: NgwpThemeKitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NgwpThemeKitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
