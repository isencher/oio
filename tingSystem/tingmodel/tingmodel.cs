using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.lib;
using ting.log;
using ting.model;

namespace ting.bll
{
    public class tingmodel<dbContext, T>
        where dbContext : DbContext, new()
        where T : class, IbaseProperties, new()
    {

        //protected ImodelGetAll modelGetAll;
        //protected ImodelAdd modelAdd;
        //protected ImodelAlter modelAlter;
        //protected ImodelDelete modelDelete;
        //protected ImodelDeletes modelDeletes;
        //protected ImodelAdds modelAdds;

        public Func<T,int> CustomAdd { get; set; }
        public Func<T,int> CustomAlter { get; set; }
        public Func<List<T>> CustomGetAll { get; set; }

        /// <summary>
        /// current entity
        /// </summary>
        public T model { get; set; }

        /// <summary>
        /// entity's id
        /// </summary>
        public int Id { get { return model.Id; } }
        /// <summary>
        /// entity's name
        /// </summary>
        public string Name { get { return model.Name; } }
        /// <summary>
        /// entity's unique tag
        /// </summary>
        public string Unique { get { return model.Unique; } }
        /// <summary>
        /// entity's display value
        /// </summary>
        public string DisplayValue { get { return model.DisplayValue; } }

        /// <summary>
        /// constructor
        /// </summary>
        public tingmodel()
        {
            //modelGetAll = new generalGetAll();
            //modelAdd = new generalAdd();
            //modelAlter = new generalAlter();
            //modelDelete = new generalDelete();
            //modelDeletes = new generalDeletes();
            //modelAdds = new generalAdds();
        }

        #region Action Collection
        /// <summary>
        /// method to get entities list
        /// </summary>
        public List<T> GetAll()
        {
            //if (modelGetAll == null) { return null; }
            //return modelGetAll.GetAll<dbContext, T>();
            return CustomGetAll ==null ? repo.GetAll<dbContext, T>() : CustomGetAll();
        }
        /// <summary>
        /// method to add a entity
        /// </summary>
        public ActionResult Add(T entity)
        {
                if ((CustomAdd == null ? repo.Add<dbContext, T>(entity) : CustomAdd(entity)) > 0)
                {
                    return ActionResult.Success;
                }
                else { return ActionResult.Fail; }

            //if (modelAdd == null) { return ActionResult.NoMethod; }
            //return modelAdd.Add<dbContext, T>(entity);
        }
        /// <summary>
        /// method to alter a entity
        /// </summary>
        public ActionResult Alter(T entity)
        {

            if ((CustomAlter==null ? repo.Alter<dbContext, T>(entity) : CustomAlter(entity)) > 0)
            {
                return ActionResult.Success;
            }
            else { return ActionResult.Fail; }

            //if (modelAlter == null) { return ActionResult.NoMethod; }
            //return modelAlter.Alter<dbContext, T>(entity);
        }
        /// <summary>
        /// method to delete a entity
        /// </summary>
        public ActionResult Delete(T entity)
        {
            if (repo.Delete<dbContext, T>(entity) > 0)
            {
                return ActionResult.Success;
            }
            else { return ActionResult.Fail; }

            //if (modelDelete == null) { return ActionResult.NoMethod; }
            //return modelDelete.Delete<dbContext, T>(entity);
        }
        //public ActionResult Deletes(List<T> entities)
        //{

        //    //if (modelDeletes == null) { return ActionResult.NoMethod; }
        //    //return modelDeletes.Deletes<dbContext, T>(entities);
        //}
        //public ActionResult Adds(List<T> entities)
        //{
        //    //if (modelAdds == null) { return ActionResult.NoMethod; }
        //    //return modelAdds.Adds<dbContext, T>(entities);
        //}
        /// <summary>
        /// clone current entity from db store
        /// </summary>
        public T Alone(int id)
        {
            return repo.GetById<dbContext, T>(id);
        }

        /// <summary>
        /// clone current entity in memory
        /// </summary>
        public T Alone(T entity)
        {
            return withType.Clone(entity);
        }

        //public void SetmodelAdd(ImodelAdd add)
        //{
        //    modelAdd = add;
        //}
        //public void SetmodelAlter(ImodelAlter alter)
        //{
        //    modelAlter = alter;
        //}

        #endregion

    }
}
