import { IMenuItem } from './menu-item.interface';

export interface IMenu {
  term_id: number;
  term_taxonomy_id: number;
  count: number;
  description: string;
  filter: string;
  name: string;
  parent: number;
  slug: string;
  taxonomy: string;
  term_group: number;
  
  items: IMenuItem[];
}