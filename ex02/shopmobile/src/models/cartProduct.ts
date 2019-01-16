import { ProductInfo } from "./productInfo";

export class CartProduct{
    public product: ProductInfo;
    public amount: number;
    public totalDiscount: number;
    public totalPrice: number;
}