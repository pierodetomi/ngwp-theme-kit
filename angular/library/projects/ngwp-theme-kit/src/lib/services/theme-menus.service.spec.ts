import { TestBed } from '@angular/core/testing';

import { ThemeMenusService } from './theme-menus.service';

describe('ThemeMenusService', () => {
  let service: ThemeMenusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThemeMenusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
