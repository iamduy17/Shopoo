export class SelectItemList   {
	public text!: string;
	public value!: string;
	public intValue!: number;

  constructor(text: string = '', intValue: number = -1, value: string = '') {
    this.text = text;
    this.intValue = intValue;
    this.value = value;
  }
}