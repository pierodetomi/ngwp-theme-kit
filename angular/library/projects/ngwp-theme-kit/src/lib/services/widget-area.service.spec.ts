import { TestBed } from '@angular/core/testing';

import { WidgetAreaService } from './widget-area.service';

describe('WidgetAreaService', () => {
  let service: WidgetAreaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WidgetAreaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
