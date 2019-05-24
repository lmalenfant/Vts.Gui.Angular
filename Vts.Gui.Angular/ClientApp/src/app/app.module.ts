import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { RangeComponent } from './range/range.component';
import { OpticalPropertiesComponent } from './optical-properties/optical-properties.component';
import { SolutionDomainComponent } from './solution-domain/solution-domain.component';
import { InverseSolverComponent } from './inverse-solver/inverse-solver.component';
import { ForwardSolverAnalysisComponent } from './forward-solver-analysis/forward-solver-analysis.component';
import { ModelAnalysisTypeComponent } from './model-analysis-type/model-analysis-type.component';
import { ForwardSolverEngineComponent } from './forward-solver-engine/forward-solver-engine.component';

@NgModule({
  declarations: [
    AppComponent,
    RangeComponent,
    OpticalPropertiesComponent,
    SolutionDomainComponent,
    InverseSolverComponent,
    ForwardSolverAnalysisComponent,
    ModelAnalysisTypeComponent,
    ForwardSolverEngineComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
