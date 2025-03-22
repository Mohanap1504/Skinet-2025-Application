import { T } from "@angular/cdk/keycodes";

export type Pagination<T> = {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: T[]
}