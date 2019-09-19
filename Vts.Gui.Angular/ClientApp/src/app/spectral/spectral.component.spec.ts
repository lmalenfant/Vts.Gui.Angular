/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixture, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule, By } from "@angular/platform-browser";
import { SpectralComponent } from './spectral.component';

let component: SpectralComponent;
let fixture: ComponentFixture<SpectralComponent>;

describe('spectral component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ SpectralComponent ],
            imports: [ BrowserModule ],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(SpectralComponent);
        component = fixture.componentInstance;
    }));

    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});