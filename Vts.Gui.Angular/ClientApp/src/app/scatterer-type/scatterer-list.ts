import { ListType } from '../shared/list-definition.model';

export const ScattererTypeList: ListType[] = [
  { value: 'PowerLaw', display: 'PowerLaw [A*λ^(-b)]' },
  { value: 'Intralipid', display: 'Intralipid [vol. frac.]' },
  { value: 'Mie', display: 'Mie Particle' }
];
