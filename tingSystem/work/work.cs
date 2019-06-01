using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.bll;
using ting.log;
using ting.model;
using WeifenLuo.WinFormsUI.Docking;

namespace ting.pal
{
    public abstract class work<dbContext,T> : DockContent
        where dbContext: DbContext,new()
        where T: class, IbaseProperties, new()
    {
        /// <summary>
        /// tingmodel instance
        /// </summary>
        protected tingmodel<dbContext, T> tingmodelinstance;
        /// <summary>
        /// logger object
        /// </summary>
        protected Logger logger;

        /// <summary>
        /// current user
        /// </summary>
        public User CurrentUser { get; set; }
        /// <summary>
        /// allow user to pass a getall method when in-getall is not enough
        /// </summary>
        public Func<List<T>> CustomGetAll
        {
            get { return tingmodelinstance.CustomGetAll; }
            set { tingmodelinstance.CustomGetAll = value; }
        }
        public Func<T,int> CustomAdd
        {
            get => tingmodelinstance.CustomAdd;
            set => tingmodelinstance.CustomAdd = value;
        }
        public Func<T,int> CustomAlter
        {
            get=> tingmodelinstance.CustomAlter;
            set => tingmodelinstance.CustomAlter = value;
        }
        /// <summary>
        /// allow to pass a validate method before to delete a entity
        /// </summary>
        public Func<T, bool> ValidateBeforeDelete;

        /// <summary>
        /// current user account
        /// </summary>
        protected string useraccount { get { return CurrentUser?.Account; } }
        /// <summary>
        /// current editting entity
        /// </summary>
        public abstract T Current { get; }
        /// <summary>
        /// entity list
        /// </summary>
        public List<T> DataSource { get { return GetAll(); } }
        /// <summary>
        /// to update entity list
        /// </summary>
        public abstract void UpdateList();
        /// <summary>
        /// open add dialog
        /// </summary>
        public abstract void OpenAdd(T entity);
        /// <summary>
        /// open alter dialog
        /// </summary>
        public abstract void OpenAlter(T entity);
        /// <summary>
        /// to delete current entity
        /// </summary>
        public abstract ActionResult ToDelete(T entity);
        public abstract ActionResult ToDeletes(List<T> entity);
        /// <summary>
        /// to import entity list from a file
        /// </summary>
        public abstract void ToImport();
        /// <summary>
        /// to export entity list
        /// </summary>
        public abstract void ToExport();
        /// <summary>
        /// to filter entity list
        /// </summary>
        public abstract void ToQuery();

        /// <summary>
        /// add current editting entity in db store
        /// </summary>
        protected ActionResult Add(T entity, T original = null)
        {
            ActionResult result = ActionResult.None;
            if (entity == null) { result = ActionResult.None; }
            else
            {
                if (IsExist(entity, true)) { result = ActionResult.IsExist; }
                else
                {
                    if (tingmodelinstance.Add(entity) == ActionResult.Success)
                    {
                        if (WriteLog(CreateLog("Add", entity.DisplayValue)))
                        {
                            return ActionResult.Success;
                        }
                        else { return ActionResult.SuccessButNotLog; }
                    }
                    else { return ActionResult.Fail; }
                }
            }

            return result;
        }
        //protected ActionResult Adds(List<T> entities)
        //{
        //    ActionResult result = ActionResult.None;
        //    int temp=0;
        //    List<T> store = GetAll();
        //    if (entities == null) { result = ActionResult.None; }
        //    else
        //    {
        //        foreach (var item in entities)
        //        {
        //            if (store == null)
        //            {
        //                if (tingmodel.Add(item) == ActionResult.Success)
        //                {
        //                    if(WriteLog(CreateLog("Add",item.DisplayValue)))
        //                    temp++;
        //                }
        //            }
        //            else
        //            {
        //                if( store.Where(s=>s.Unique==item.Unique) != null )
        //                {
        //                    if (tingmodel.Add(item) == ActionResult.Success)
        //                    {
        //                        if (WriteLog(CreateLog("Add", item.DisplayValue)))
        //                            temp++;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (temp == 0) { result = ActionResult.Fail; }
        //    else if(temp == entities.Count) { result = ActionResult.Success; }
        //    else if(temp < entities.Count) { result = ActionResult.ParticalSuccess; }

        //    return result;
        //}
        /// <summary>
        /// alter current editting entity which has in db store
        /// </summary>
        protected ActionResult Alter(T entity, T original)
        {
            ActionResult result = ActionResult.None;
            if (entity == null) { result = ActionResult.None; }
            else if (entity.DisplayValue == original.DisplayValue) { result = ActionResult.None; }
            else
            {
                if (IsExist(entity, false)) { result = ActionResult.IsExist; }
                else
                {
                    if (tingmodelinstance.Alter(entity) == ActionResult.Success)
                    {
                        if (WriteLog(CreateLog("Alter", entity.DisplayValue, original.DisplayValue)))
                        {
                            return ActionResult.Success;
                        }
                        else { return ActionResult.SuccessButNotLog; }
                    }
                    else { return ActionResult.Fail; }
                }
            }

            return result;
        }
        /// <summary>
        /// delete current editting entity from db store
        /// </summary>
        protected ActionResult Delete(T entity)
        {
            ActionResult result;
            if (entity == null) { result = ActionResult.None; }
            else
            {
                if (tingmodelinstance.Delete(entity) == ActionResult.Success)
                {
                    if (WriteLog(CreateLog("Delete", null, entity.DisplayValue)))
                    {
                        result = ActionResult.Success;
                    }
                    else { result = ActionResult.SuccessButNotLog; }
                }
                else { result = ActionResult.Fail; }
            }
            return result;
        }
        //protected ActionResult Deletes(List<T> entities)
        //{
        //    return tingmodel.Deletes(entities);
        //}
        /// <summary>
        /// get entity list from db store
        /// </summary>
        protected List<T> GetAll()
        {
            return tingmodelinstance.GetAll();
        }
        /// <summary>
        /// to write a log into db store
        /// </summary>
        protected bool WriteLog(Log log)
        {
            return logger.Write(log);
        }

        /// <summary>
        /// create log record
        /// </summary>
        protected Log CreateLog(string action, string content, string original = null)
        {
            var log = new Log()
            {
                UserAcount = useraccount,
                Date = DateTime.Now,
                Action = action,
                Entity = typeof(T).FullName,
                Content = content,
                Original = original
            };
            return log;
        }

        /// <summary>
        /// judge whether a specific entity in db store
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="editstatus"></param>
        /// <returns></returns>
        public bool IsExist(T entity,bool editstatus)
        {
            // editstatus :  true - a entity out store
            // editstatus : false - a entity in store 
            var store = GetAll();
            if (editstatus)
            {
                if (store == null) 
                { return false; }
                else //(store != null)
                {
                    if (store.FirstOrDefault(s=>s.Unique==entity.Unique) != null)
                    { return true; } else { return false; }
                }
            }
            else // editstatus == false
            {
                if(store == null)
                { return false; }
                else
                {
                    store = store.Where(s => s.Id != entity.Id).ToList();

                    if (store == null)
                    { return false; }
                    else //(store != null)
                    {
                        if (store.FirstOrDefault(s => s.Unique == entity.Unique) != null)
                        { return true; }
                        else { return false; }
                    }
                }
            }

        }
        /// <summary>
        /// clone current entity before editting
        /// </summary>
        protected T Alone(T entity)
        {
            return tingmodelinstance.Alone(entity);
        }
    }
}
