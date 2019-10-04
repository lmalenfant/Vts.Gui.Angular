import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RangeComponent } from './range/range.component';
import { OpticalPropertiesComponent } from './optical-properties/optical-properties.component';
import { SolutionDomainComponent } from './solution-domain/solution-domain.component';
import { InverseSolverAnalysisComponent } from './inverse-solver-analysis/inverse-solver-analysis.component';
import { ForwardSolverAnalysisComponent } from './forward-solver-analysis/forward-solver-analysis.component';
import { SpectralComponent } from './spectral/spectral.component';
import { TissueDefinitionComponent } from './tissue-definition/tissue-definition.component';
import { ModelAnalysisTypeComponent } from './model-analysis-type/model-analysis-type.component';
import { ForwardSolverEngineComponent } from './forward-solver-engine/forward-solver-engine.component';
import { OptimizationParametersComponent } from './optimization-parameters/optimization-parameters.component';
import { OptimizerTypeComponent } from './optimizer-type/optimizer-type.component';
import { PlotComponent } from './plot/plot.component';
import { PlotService } from './services/plot.service';
import { HttpClientModule } from '@angular/common/http';
import { ScattererTypeComponent } from './scatterer-type/scatterer-type.component';
import { SingleWavelengthCalculatorComponent } from './single-wavelength-calculator/single-wavelength-calculator.component';

@NgModule({
  declarations: [
    AppComponent,
    RangeComponent,
    OpticalPropertiesComponent,
    SolutionDomainComponent,
    InverseSolverAnalysisComponent,
    ForwardSolverAnalysisComponent,
    SpectralComponent,
    ScattererTypeComponent,
    SingleWavelengthCalculatorComponent,
    TissueDefinitionComponent,
    ModelAnalysisTypeComponent,
    ForwardSolverEngineComponent,
    OptimizationParametersComponent,
    OptimizerTypeComponent,
    PlotComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [PlotService],
  bootstrap: [AppComponent]
})
export class AppModule { }
