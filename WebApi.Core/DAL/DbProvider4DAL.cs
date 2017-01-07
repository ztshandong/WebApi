using System.Data;
using System.Data.Common;
using WebApi.Core;

namespace WebApi.DAL
{
    internal class DbProvider4DAL
    {
        internal DbCommand CurrentCommand { get; private set; }
        internal DbProviderType CurrentDbProviderType { get; set; }
        internal DbDataAdapter CurrentDbDataAdapter { get; private set; }
        internal ChooseDataBase CurrentChooseDataBase { get; private set; }

        internal DbProvider4DAL(string procedureName, ChooseDataBase chooseDataBase, DbProviderType dbProviderType)
        {
            CurrentDbProviderType = dbProviderType;
            CurrentChooseDataBase = chooseDataBase;

            CurrentCommand = ProviderFactory.Instance.GetDbProviderFactory(dbProviderType).CreateCommand();
            CurrentCommand.CommandText = procedureName;
            CurrentCommand.CommandType = CommandType.StoredProcedure;

            CurrentDbDataAdapter = ProviderFactory.Instance.GetDbProviderFactory(dbProviderType).CreateDataAdapter();
            CurrentDbDataAdapter.SelectCommand = CurrentCommand;

            //_CurrentDbProviderType = dbProviderType;
            //_CurrentCommand = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateCommand();
            ////_CurrentConnection = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateConnection();
            //_CurrentDbDataAdapter = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateDataAdapter();
            //_CurrentConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;

            //_CurrentCommand.CommandType = CommandType.StoredProcedure;

            //_CurrentDbDataAdapter.SelectCommand = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateCommand();
            //_CurrentDbDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            ////_CurrentDbDataAdapter.InsertCommand = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateCommand();
            ////_CurrentDbDataAdapter.UpdateCommand = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateCommand();
            ////_CurrentDbDataAdapter.DeleteCommand = ProviderFactory.GetDbProviderFactory(dbProviderType).CreateCommand();

            ////_CurrentDbDataAdapter.InsertCommand.CommandType = CommandType.Text;
            ////_CurrentDbDataAdapter.UpdateCommand.CommandType = CommandType.Text;
            ////_CurrentDbDataAdapter.DeleteCommand.CommandType = CommandType.Text;
        }

        //internal void Connect()
        //{
        //    _CurrentConnection.ConnectionString = _CurrentConnectionString;
        //    _CurrentCommand.Connection = _CurrentConnection;
        //    _CurrentDbDataAdapter.SelectCommand.Connection = _CurrentConnection;
        //    //_CurrentDbDataAdapter.InsertCommand.Connection = _CurrentConnection;
        //    //_CurrentDbDataAdapter.UpdateCommand.Connection = _CurrentConnection;
        //    //_CurrentDbDataAdapter.DeleteCommand.Connection = _CurrentConnection;
        //    _CurrentConnection.Open();
        //}

        //internal void DisConnect()
        //{
        //    _CurrentConnection.Close();
        //    _CurrentConnection.Dispose();
        //}

    }
}
