import { createRouter, createWebHistory } from 'vue-router'
// Định nghĩa / import router:
import EmployeeList from '../view/employee/EmployeeList.vue'
import CustomerList from '../view/customer/CustomerList.vue'
import MaterialList from '../view/material/MaterialList.vue'
// import ReportList from './view/employee/Report.vue'

// ĐỊnh nghĩa các router
const routers = [

    { path: "/", redirect: '/material' },
    { path: "/employee", name: "Employee", component: EmployeeList },
    { path: "/customer", name: "Customer", component: CustomerList },
    { path: "/material", name: "Material", component: MaterialList },
    // { path: "/report", name: "Report", component: ReportList }
]
//Khởi tạo các router và truyền các router đã được định nghĩa vào
const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes: routers

})
export default router 
