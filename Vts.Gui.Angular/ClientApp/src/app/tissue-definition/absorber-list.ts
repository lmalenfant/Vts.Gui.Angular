import { AbsorberConcentration } from './absorber-concentration.model';

export const Skin: AbsorberConcentration[] = [
  { label: 'Hb', value: 28.4, units: 'μM' },
  { label: 'HbO2', value: 22.4, units: 'μM' },
  { label: 'H2O', value: 0.7, units: 'vol. frac.' },
  { label: 'Fat', value: 0, units: 'vol. frac.' },
  { label: 'Melanin', value: 0.0051, units: 'vol. frac.' }
];

export const BrainWhiteMatter: AbsorberConcentration[] = [
  { label: 'Hb', value: 24, units: 'μM' },
  { label: 'HbO2', value: 56, units: 'μM' },
  { label: 'H2O', value: 0.8, units: 'vol. frac.' },
  { label: 'Fat', value: 0.12, units: 'vol. frac.' }
];

export const BrainGrayMatter: AbsorberConcentration[] = [
  { label: 'Hb', value: 24, units: 'μM' },
  { label: 'HbO2', value: 56, units: 'μM' },
  { label: 'H2O', value: 0.8, units: 'vol. frac.' },
  { label: 'Fat', value: 0.12, units: 'vol. frac.' }
];

export const BreastPreMenopause: AbsorberConcentration[] = [
  { label: 'Hb', value: 6.9, units: 'μM' },
  { label: 'HbO2', value: 19.6, units: 'μM' },
  { label: 'H2O', value: 0.345, units: 'vol. frac.' },
  { label: 'Fat', value: 0.41, units: 'vol. frac.' }
];

export const BreastPostMenopause: AbsorberConcentration[] = [
  { label: 'Hb', value: 3.75, units: 'μM' },
  { label: 'HbO2', value: 11.3, units: 'μM' },
  { label: 'H2O', value: 0.205, units: 'vol. frac.' },
  { label: 'Fat', value: 0.585, units: 'vol. frac.' }
];

export const Liver: AbsorberConcentration[] = [
  { label: 'Hb', value: 66, units: 'μM' },
  { label: 'HbO2', value: 124, units: 'μM' },
  { label: 'H2O', value: 0.87, units: 'vol. frac.' },
  { label: 'Fat', value: 0.02, units: 'vol. frac.' }
];

export const IntralipidPhantom: AbsorberConcentration[] = [
  { label: 'Nigrosin', value: 0.01, units: 'μM' }
];

export const Custom: AbsorberConcentration[] = [
  { label: 'Hb', value: 20, units: 'μM' },
  { label: 'HbO2', value: 20, units: 'μM' },
  { label: 'H2O', value: 0, units: 'vol. frac.' },
  { label: 'Fat', value: 0, units: 'vol. frac.' },
  { label: 'Melanin', value: 0, units: 'vol. frac.' }
];
