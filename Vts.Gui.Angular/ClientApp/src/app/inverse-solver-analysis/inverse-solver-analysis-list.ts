import { ForwardSolverEngine } from '../forward-solver-engine/forward-solver-engine.model';

export const InverseSolverEngineList: ForwardSolverEngine[] = [
  { value: 'PointSourceSDA', display: 'Standard Diffusion (Analytic: Isotropic Point Source)' },
  { value: 'DistributedPointSourceSDA', display: 'Standard Diffusion (Analytic: Distributed Point Source)' },
  { value: 'MonteCarlo', display: 'Scaled Monte Carlo - Basic (g=0.8, n=1.4)' },
  { value: 'Nurbs', display: 'Scaled Monte Carlo - Nurbs (g=0.8, n=1.4)' }
];
