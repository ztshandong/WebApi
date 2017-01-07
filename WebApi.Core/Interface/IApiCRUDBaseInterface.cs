using System.Data;
using WebApi.Models;

namespace WebApi.Interface
{
    internal interface IApiCRUDBaseInterface4BLL
    {
        DataSet DoGet(Params4ApiCRUD params4ApiCRUD);
        DataSet DoSearch(Params4ApiCRUD params4ApiCRUD);
        DataSet DoPost(Params4ApiCRUD params4ApiCRUD);
        DataSet DoPut(Params4ApiCRUD params4ApiCRUD);
        DataSet DoDelete(Params4ApiCRUD params4ApiCRUD);
    }

    internal interface IApiCRUDBaseInterface4DAL
    {
        DataSet Get(Params4ApiCRUD params4ApiCRUD);
        DataSet Search(Params4ApiCRUD params4ApiCRUD);
        DataSet Post(Params4ApiCRUD params4ApiCRUD);
        DataSet Put(Params4ApiCRUD params4ApiCRUD);
        DataSet Delete(Params4ApiCRUD params4ApiCRUD);
    }
}
