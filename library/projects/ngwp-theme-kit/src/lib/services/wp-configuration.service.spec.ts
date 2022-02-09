import { TestBed } from '@angular/core/testing';

import { WpConfigurationService } from './wp-configuration.service';

describe('WpConfigurationService', () => {
  let service: WpConfigurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WpConfigurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
