import { TestBed } from '@angular/core/testing';

import { ThemeSettingsService } from './theme-settings.service';

describe('ThemeSettingsService', () => {
  let service: ThemeSettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ThemeSettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
