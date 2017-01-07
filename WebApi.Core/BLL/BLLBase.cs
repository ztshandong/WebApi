using System;
using System.Data;
using System.Reflection;
using System.Text;
using WebApi.Core;
using WebApi.DAL;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.BLL
{
    public abstract partial class BLLBase : IApiCRUDBaseInterface4BLL
    {
        protected DALBase _DALBase;
        protected virtual void AddErrMsg(DataSet ds, string ErrMsg)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ErrMsg");
            dt.Columns.Add("CRUDResult");
            dt.Rows.Add();
            dt.Rows[0]["ErrMsg"] = ErrMsg;
            dt.Rows[0]["CRUDResult"] = CustomHttpResponseMessageStatus.Fail;
            ds.Tables.Add(dt);
        }

        protected virtual string GetErrMsg(object fromUri)
        {
            StringBuilder sb = new StringBuilder();
            Type ORM_Model = fromUri.GetType();
            //object obj = ORM_Model.Assembly.CreateInstance(ORM_Model.FullName);
            PropertyInfo[] properties = ORM_Model.GetProperties();

            foreach (PropertyInfo propInfo in properties)
            {
                ORM_ValidateAttribute[] RequiredFields = (ORM_ValidateAttribute[])propInfo.GetCustomAttributes(typeof(ORM_ValidateAttribute), false);
                if (RequiredFields != null && RequiredFields.Length == 1)
                {
                    ORM_ValidateAttribute r = RequiredFields[0];
                    var fieldvalue = propInfo.GetValue(fromUri);

                    if (r.IsRequired)
                    {
                        if (!Validate4Required(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsRequired);
                        }
                    }

                    if (r.IsMobile)
                    {
                        if (!Validate4IsMobile(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsMobile);
                        }
                    }

                    if (r.IsCarPlateNo)
                    {
                        if (!Validate4IsCarPlateNo(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsCarPlateNo);
                        }
                    }

                    if (r.IsPersonID)
                    {
                        if (!Validate4IsPersonID(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsPersonID);
                        }
                    }

                    if (r.IsInt)
                    {
                        if (!Validate4IsInt(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsInt);
                        }
                    }

                    if (r.IsNum)
                    {
                        if (!Validate4IsNum(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsNum);
                        }
                    }

                    if (r.IsPositive)
                    {
                        if (!Validate4IsPositive(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsPositive);
                        }
                    }

                    if (r.IsValueArea)
                    {
                        if (!Validate4IsValueArea(fieldvalue, RequiredFields[0].MinValue, RequiredFields[0].MaxValue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsValueArea);
                        }
                    }

                    if (r.IsCanNotZero)
                    {
                        if (!Validate4IsCanNotZero(fieldvalue))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsCanNotZero);
                        }
                    }

                    if (r.IsLimitLenth)
                    {
                        if (!Validate4IsLimitLenth(fieldvalue, RequiredFields[0].MinLenth, RequiredFields[0].MaxLenth))
                        {
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(propInfo.Name + RequiredFields[0].ErrInfo4IsLimitLenth);
                        }
                    }
                }
            }
            return sb.ToStringEx();
        }
        #region 验证算法
        protected virtual bool Validate4Required(object str)
        {
            return !str.IsNullOrEmpty();
        }
        protected virtual bool Validate4IsMobile(object str)
        {
            return str.IsMobile();
        }
        protected virtual bool Validate4IsCarPlateNo(object str)
        {
            return str.IsCarPlateNo();
        }
        protected virtual bool Validate4IsPersonID(object str)
        {
            return str.IsPersonID();
        }
        protected virtual bool Validate4IsInt(object str)
        {
            try
            {
                Convert.ToInt32(str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected virtual bool Validate4IsNum(object str)
        {
            try
            {
                Convert.ToDouble(str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected virtual bool Validate4IsCanNotZero(object str)
        {
            return str.ToDecimalEx() != 0;
        }
        protected virtual bool Validate4IsPositive(object str)
        {
            return str.ToDecimalEx() > 0;
        }
        protected virtual bool Validate4IsValueArea(object str, decimal minValue, decimal maxValue)
        {
            try
            {
                decimal d = Convert.ToDecimal(str);
                return d >= minValue && d <= maxValue;
            }
            catch (Exception ex) { return false; }
        }
        protected virtual bool Validate4IsLimitLenth(object str, int minLenth, int maxLenth)
        {
            try
            {
                int i = str.ToStringEx().Length;
                return i >= minLenth && i <= maxLenth;
            }
            catch (Exception ex) { return false; }
        }
        #endregion

        public virtual DataSet DoGet(Params4ApiCRUD params4ApiCRUD)
        {
            return this.Get(params4ApiCRUD);
        }
        protected virtual DataSet Get(Params4ApiCRUD params4ApiCRUD)
        {
            DataSet ds = new DataSet();
            string errmsg = GetErrMsg(params4ApiCRUD.fromUri);
            if (!errmsg.IsNullOrEmpty())
            {
                AddErrMsg(ds, errmsg);
            }
            else
            {
                ds = _DALBase.Get(params4ApiCRUD);
            }
            return ds;
        }
        public virtual DataSet DoPost(Params4ApiCRUD params4ApiCRUD)
        {
            return this.Post(params4ApiCRUD);
        }
        protected virtual DataSet Post(Params4ApiCRUD params4ApiCRUD)
        {
            DataSet ds = new DataSet();
            string errmsg = GetErrMsg(params4ApiCRUD.fromUri);
            if (!errmsg.IsNullOrEmpty())
            {
                AddErrMsg(ds, errmsg);
            }
            else
            {
                ds = _DALBase.Post(params4ApiCRUD);
            }
            return ds;
        }
        public virtual DataSet DoPut(Params4ApiCRUD params4ApiCRUD)
        {
            return this.Put(params4ApiCRUD);
        }
        protected virtual DataSet Put(Params4ApiCRUD params4ApiCRUD)
        {
            DataSet ds = new DataSet();
            string errmsg = GetErrMsg(params4ApiCRUD.fromUri);
            if (!errmsg.IsNullOrEmpty())
            {
                AddErrMsg(ds, errmsg);
            }
            else
            {
                ds = _DALBase.Put(params4ApiCRUD);
            }
            return ds;
        }
        public virtual DataSet DoSearch(Params4ApiCRUD params4ApiCRUD)
        {
            return this.Search(params4ApiCRUD);
        }
        protected virtual DataSet Search(Params4ApiCRUD params4ApiCRUD)
        {
            DataSet ds = new DataSet();
            string errmsg = GetErrMsg(params4ApiCRUD.fromUri);
            if (!errmsg.IsNullOrEmpty())
            {
                AddErrMsg(ds, errmsg);
            }
            else
            {
                ds = _DALBase.Search(params4ApiCRUD);
            }
            return ds;
            
        }
        public virtual DataSet DoDelete(Params4ApiCRUD params4ApiCRUD)
        {
            return this.Delete(params4ApiCRUD);
        }
        protected virtual DataSet Delete(Params4ApiCRUD params4ApiCRUD)
        {
            DataSet ds = new DataSet();
            string errmsg = GetErrMsg(params4ApiCRUD.fromUri);
            if (!errmsg.IsNullOrEmpty())
            {
                AddErrMsg(ds, errmsg);
            }
            else
            {
                ds = this.Delete(params4ApiCRUD);
            }
            return ds;
        }
    }
}


/*
   private FieldInfo[] GetAllInfos(Type T)
        {
            FieldInfo[] infos = null;
            FieldInfo[] infosSelf = T.GetFields();
            FieldInfo[] infosBase = null;

            if (T.BaseType != null)
                infosBase = GetAllInfos(T.BaseType);

            if (infosBase == null || infosBase.Length == 0)
                infos = infosSelf;
            else
            {
                int iLength = infosBase.Length;
                Array.Resize<FieldInfo>(ref infosBase, infosBase.Length + infosSelf.Length);//相加两数组
                infosSelf.CopyTo(infosBase, iLength);
                infos = infosBase;
            }
            return infos;
        }
     
     */
