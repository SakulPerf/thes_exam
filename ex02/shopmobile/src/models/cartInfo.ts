import { ProductInfo } from "./productInfo";

export class CartInfo {
    public products: ProductInfo[];
    public rawPrice: number;
    public discount: number;
    public price: number;
}