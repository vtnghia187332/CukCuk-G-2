const misaEnum = {
    // Enum cho trạng thái của Form
    formMode: {
        Add: 0,
        Edit: 1,
        Duplicate: 2,
    },
}

// Tài nguyên API chung
const misaApi = {
    // Material
    getMaterial: "http://localhost:5236/api/v1/Materials",
    getMaterialById: "http://localhost:5236/api/v1/Materials",
    getNewMaterialCode: "http://localhost:5236/api/v1/Materials/GetNewCode",
    deleteMaterial: "http://localhost:5236/api/v1/Materials",
    postMaterialWithConvertions: "http://localhost:5236/api/v1/Materials/MaterialWithConvertions",

    getMaterialFilter: "http://localhost:5236/api/v1/Materials/filter",

    // Unit
    getUnit: "http://localhost:5236/api/v1/Units",

    // Storage
    getStorage: "http://localhost:5236/api/v1/Storages",

    // Convertion
    getConvertionByMaterialId: "http://localhost:5236/api/v1/Convertions/ConvertionsOfMaterial",
    updateConvertion: "http://localhost:5236/api/v1/Convertions",
    insertConvertion: "http://localhost:5236/api/v1/Convertions",
    deleteConvertion: "http://localhost:5236/api/v1/Convertions",

    insertMulConvertionByMaterialId: "http://localhost:5236/api/v1/Convertions/ConvertionsOfMaterial",
    updateMulConvertionByMaterialId: "http://localhost:5236/api/v1/Convertions/ConvertionsOfMaterial",
}

//Tài nguyên Operator
const misaOperator = {
    notContain: {
        id: 0,
        value: 0,
        txtOperator: "Không chứa",
        iconOperator: "!",
    },
    begin: {
        id: 1,
        value: 1,
        txtOperator: "Bắt đầu bằng",
        iconOperator: "+",
    },
    end: {
        id: 2,
        value: 2,
        txtOperator: "Kết thúc bằng",
        iconOperator: "+",
    },
    equal: {
        id: 3,
        value: 3,
        txtOperator: "Bằng",
        iconOperator: "=",
    },
    contain: {
        id: 4,
        value: 4,
        txtOperator: "Chứa",
        iconOperator: "*",
    },
}


export default {
    misaEnum, misaApi, misaOperator,
};