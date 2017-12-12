import { Address } from "./address";
import { Department } from "./department";

export class Teacher {
    id: number;
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    title: string;
    employmentDate: Date;
    dateOfBirth: Date;
    address: Address;
    department: Department;
    phoneNumber: string;
}