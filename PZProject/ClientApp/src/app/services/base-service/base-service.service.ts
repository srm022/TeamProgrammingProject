import { BaseModel } from './../../models/base.model';
import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root'
// })

export class BaseServiceService {

  private savedModel: BaseModel;

  constructor() {
  }

  pushModel(model: BaseModel): void {
    this.savedModel = model;
  }

  popModel(): object {
    return this.savedModel;
  }

  deleteModel() {
    delete this.savedModel;
  }
}


