using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.lib;
using ting.model;

namespace ting.dal
{
    public class repo
    {
        /// <summary>
        /// get entity all collection from store
        /// </summary>
        public static List<T> GetAll<dbContext, T>()
            where dbContext : DbContext, new()
            where T : class, IbaseProperties, new()
        {
            #region to get list
            try
            {
                using (var sc = new dbContext())
                {
                    return sc.Set<T>().ToList();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
                throw;
            }
            #endregion
        }

        /// <summary>
        /// get entity all collection include sub collection from store
        /// </summary>
        public static List<T> GetAllwithInclude<dbContext, T>()
            where dbContext : DbContext, new()
            where T : class, IbaseProperties, new()
        {
            #region to get list
            try
            {
                List<T> result = new List<T>();
                var navs = withType.GetNaviProps<T>();
                int n = navs.Length;

                using (dbContext sc = new dbContext())
                {
                    switch (n)
                    {
                        case 1:
                            result = sc.Set<T>().Include(navs[0]).ToList();
                            break;
                        case 2:
                            result = sc.Set<T>()
                                .Include(navs[0])
                                .Include(navs[1])
                                .ToList();
                            break;
                        case 3:
                            result = sc.Set<T>()
                                .Include(navs[0])
                                .Include(navs[1])
                                .Include(navs[2])
                                .ToList();
                            break;
                        case 4:
                            result = sc.Set<T>()
                                .Include(navs[0])
                                .Include(navs[1])
                                .Include(navs[2])
                                .Include(navs[3])
                                .ToList();
                            break;
                        case 5:
                            result = sc.Set<T>()
                                .Include(navs[0])
                                .Include(navs[1])
                                .Include(navs[2])
                                .Include(navs[3])
                                .Include(navs[4])
                                .ToList();
                            break;
                        default:
                            result = sc.Set<T>().ToList();
                            break;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return null;
                throw;
            }
            #endregion
        }
        /// <summary>
        /// add a entity
        /// </summary>
        public static int Add<dbContext, T>(T entity)
                    where dbContext : DbContext, new()
                    where T : class, /*IbaseProperties,*/ new()
        {
            try
            {
                using (var sc = new dbContext())
                {

                    sc.Set<T>().Add(entity);
                    return sc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return 0;
                throw;
            }

        }
        /// <summary>
        /// alter a entity
        /// </summary>
        public static int Alter<dbContext, T>(T entity)
                    where dbContext : DbContext, new()
                    where T : class, IbaseProperties, new()
        {
            try
            {
                using (var sc = new dbContext())
                {
                    var entry = sc.Entry(entity);

                    entry.State = EntityState.Modified;
                    
                    return sc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                return 0;
                throw;
            }

        }
        /// <summary>
        /// delete a entity
        /// </summary>
        public static int Delete<dbContext, T>(T entity)
                    where dbContext : DbContext, new()
                    where T : class, IbaseProperties, new()
        {
            try
            {
                using (var sc = new dbContext())
                {
                    sc.Set<T>().Attach(entity);
                    sc.Set<T>().Remove(entity);
                    return sc.SaveChanges();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                return 0;
                throw;
            }
        }
        /// <summary>
        /// get a entity by id
        /// </summary>
        public static T GetById<dbContext, T>(int id)
                    where dbContext : DbContext, new()
                    where T : class, IbaseProperties, new()
        {
            try
            {
                using (var context = new dbContext())
                {
                    return context.Set<T>().Find(id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// add a many-to-many entity
        /// </summary>
        public static int m2mAdd<dbContext, T, S>(T entity)
            where dbContext : DbContext, new()
            where T : class, IbaseProperties, IManytoMany<S>, new()
            where S : class, IbaseProperties, new()
        {
            try
            {
                using (var sc = new dbContext())
                {
                    foreach (var item in entity.Partners)
                    {
                        sc.Set<S>().Attach(item);
                    }

                    sc.Set<T>().Add(entity);
                    return sc.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
        }
        /// <summary>
        /// update a many-to-many entity
        /// </summary>
        public static int m2mAlter<dbContext, T, S>(T entity)
            where dbContext : DbContext, new()
            where T : class, IbaseProperties, IManytoMany<S>, new()
            where S : class, IbaseProperties, new()
        {
            try
            {
                var result = 0;
                using (var sc = new dbContext())
                {
                    #region save entity's partners
                    /* 1- Get existing data from database */
                    var existingEntity = sc.Set<T>().Include("Partners")
                            .Where(e => e.Id == entity.Id).FirstOrDefault();

                    /* 2- Find deleted partners from entity's partner collection by 
                    entity' existing partners (existing data from database) minus entity's
                    current partner list (came from client in disconnected scenario) */
                    var deletedPartners = existingEntity.Partners.Except(entity.Partners, new EntityComparer<S>()).ToList<S>();

                    /* 3- Find Added partners in entity's partner collection by entity's 
                    current partner list (came from client in disconnected scenario) minus 
                    entity's existing partners (existing data from database)  */
                    var addedPartners = entity.Partners.Except(existingEntity.Partners, new EntityComparer<S>()).ToList<S>();

                    //sc.Entry(existingEntity).State = EntityState.Detached;
                    //sc.Entry(entity).State = EntityState.Modified;

                    /* 4- Remove deleted partners from entity's existing partner collection 
                    (existing data from database)*/
                    deletedPartners.ForEach(c => existingEntity.Partners.Remove(c));


                    //5- Add new partners
                    foreach (S c in addedPartners)
                    {
                        /*6- Attach partners because it came from client 
                        as detached state in disconnected scenario*/
                        if (sc.Entry(c).State == EntityState.Detached)
                            sc.Set<S>().Attach(c);

                        //7- Add partner in existing entity's partner collection
                        existingEntity.Partners.Add(c);
                    }

                    //8- Save changes which will reflect in EntityPartner table only
                    result = sc.SaveChanges();
                    #endregion

                    #region save entity
                    var local = sc.Set<T>()
                         .Local
                         .FirstOrDefault(f => f.Id == entity.Id);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }
                    entity.Partners = null;
                    sc.Entry(entity).State = EntityState.Modified;
                    result += sc.SaveChanges();
                    #endregion

                }
                return result;
            }
            catch (Exception ex)
            {
                return 0;
                throw;
            }
        }

        /// <summary>
        /// register as a user
        /// </summary>
        public static User Register(User user, string password)
        {
            user.Salt = withSecure.GenerateSalt();
            user.SaltedHashedPassword = withSecure.GenerateSaltedHashedPassword(user.Salt, password);

            return user;
        }

        #region 通过身份获取授权
        /// <summary>
        /// get module list for specific user
        /// </summary>
        public List<Module> GetModules(User user, SetofBook set)
        {
            try
            {
                using (var sc = new sysContext())
                {
                    var roleids = sc.Users.Include(u => u.Partners)
                        .FirstOrDefault(u => u.Id == user.Id).Partners.Select(r => r.Id);
                    var mods = sc.Rights.Where(r => roleids.Contains(r.RoleId) && r.SetofBookId == set.Id && r.Enabled == true)
                        .Select(r => r.LevelTwo.LevelOne.Module).Distinct().ToList();
                    return mods;
                    #region waive
                    //var roles = sc.Users.Find(user.Id).SRoles.ToList();
                    //var roleids = roles.Select(r => r.Id);
                    //var book = sc.SetofBooks.Find(set.Id);
                    //var rights = sc.Rights.Where(r => r.SetofBooksId == book.Id && r.Enabled == true).ToList();
                    //var rightids = rights.Select(r => r.Id);
                    //var acc = sc.Accredits.Where(a => roleids.Contains(a.RoleId) && rightids.Contains(a.RightId)).ToList();
                    //rightids = acc.Select(a => a.RightId).Distinct();
                    //rights = sc.Rights.Include("LevelTwo")
                    //    .Where(r => rightids.Contains(r.Id)).ToList();
                    //var twoids = rights.Select(r => r.LevelTwoId);
                    //var oneids = sc.LevelTwos.Where(t => twoids.Contains(t.Id)).Select(t => t.LevelOneId).Distinct().ToList();
                    //var mods = sc.LevelOnes.Where(o => oneids.Contains(o.Id)).Select(o => o.Module).ToList();

                    //return mods;
                    #endregion
                }
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// get levelone list for specific user
        /// </summary>
        public List<LevelOne> GetLevelOnes(User user, SetofBook set, Module module)
        {
            try
            {
                using (var sc = new sysContext())
                {
                    var roleids = sc.Users.Include(u => u.Partners)
                        .FirstOrDefault(u => u.Id == user.Id).Partners.Select(r => r.Id);
                    var ones = sc.Rights.Include(r => r.LevelTwo.LevelOne.Module)
                        .Where(r => roleids.Contains(r.RoleId)
                            && r.SetofBookId == set.Id
                            && r.Enabled == true
                            && r.LevelTwo.LevelOne.Module.Id == module.Id)
                        .Select(r => r.LevelTwo.LevelOne).Distinct().ToList();
                    return ones;
                    #region waive
                    //var roles = (from u in sc.Users where u.Id == user.Id select u.SRoles).ToList();
                    //var roles = sc.Users.Find(user.Id).SRoles.ToList();
                    //var roleids = roles.Select(r => r.Id);
                    //var book = sc.SetofBooks.Find(set.Id);
                    //var rights = sc.Rights.Where(r => r.SetofBooksId == book.Id && r.Enabled == true).ToList();
                    //var rightids = rights.Select(r => r.Id);
                    //var acc = sc.Accredits.Where(a => roleids.Contains(a.RoleId) && rightids.Contains(a.RightId)).ToList();
                    //rightids = acc.Select(a => a.RightId).Distinct();
                    //rights = sc.Rights.Include("LevelTwo")
                    //    .Where(r => rightids.Contains(r.Id)).ToList();
                    //var twoids = rights.Select(r => r.LevelTwoId);
                    //var ones = sc.LevelTwos.Include("LevelOne")
                    //    .Where(t => twoids.Contains(t.Id) && t.LevelOne.ModuleId == module.Id)
                    //    .Select(t => t.LevelOne).Distinct();

                    //return ones.ToList();                    
                    #endregion
                }
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// get levelone name list for specific user
        /// </summary>
        public List<string> GetLevelOneButtonTexts(List<LevelOne> ones)
        {
            return ones.Select(o => o.Name).ToList();
        }
        /// <summary>
        /// get leveltwo list for specific user
        /// </summary>
        public List<LevelTwo> GetLevelTwos(User user, SetofBook set, LevelOne levelone)
        {
            try
            {
                using (var sc = new sysContext())
                {
                    var roleids = sc.Users.Include(u => u.Partners)
                        .FirstOrDefault(u => u.Id == user.Id).Partners.Select(r => r.Id);
                    var rightids = sc.Rights.Include(r => r.LevelTwo.LevelOne)
                        .Where(r => roleids.Contains(r.RoleId)
                            && r.SetofBookId == set.Id
                            && r.Enabled == true
                            && r.LevelTwo.LevelOne.Id == levelone.Id)
                        .Select(r => r.Id).ToList();
                    var twos = sc.LevelTwos.Where(t => rightids.Contains(t.Id)).ToList();
                    return twos;
                    #region waive
                    //var roles = (from u in sc.Users where u.Id == user.Id select u.SRoles).ToList();
                    //var roles = sc.Users.Find(user.Id).SRoles.ToList();
                    //var roleids = roles.Select(r => r.Id);
                    //var book = sc.SetofBooks.Find(set.Id);
                    //var rightids = sc.Accredits
                    //        .Where(a => roleids.Contains(a.RoleId)).Select(a => a.RightId).ToList();
                    //var twoids = sc.Rights.Where(r => rightids.Contains(r.Id) || r.Enabled == true).Select(r => r.LevelTwoId).ToList();
                    //var twos = sc.LevelTwos.Where(t => twoids.Contains(t.Id) || t.LevelOneId == levelone.Id).ToList();

                    //return twos;
                    #endregion
                }
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// get leveltwo dictionary for specific user
        /// </summary>
        public Dictionary<string, string> GetLevelTwoButtonTexts(List<LevelTwo> twos)
        {
            return twos.ToDictionary(x => x.Name, x => x.ClassName);
        }
        /// <summary>
        /// get operation list for specific user
        /// </summary>
        public List<Operation> GetActionButtonText(User user, SetofBook set, LevelTwo leveltwo)
        {
            try
            {
                using (var sc = new sysContext())
                {
                    var roleids = sc.Users.Include(u => u.Partners)
                        .FirstOrDefault(u => u.Id == user.Id).Partners.Select(r => r.Id);
                    var rightids = sc.Rights.Where(r => roleids.Contains(r.RoleId)
                                            && r.SetofBookId == set.Id
                                            && r.Enabled == true
                                            && r.LevelTwoId == leveltwo.Id)
                        .Select(r => r.Id).ToList();
                    var ops = sc.Accredits.Where(a => rightids.Contains(a.RightId)
                                            && a.Enabled == true).Select(a => a.Operation).ToList();
                    return ops;
                    #region waive
                    //var roles = sc.Users.Where(u => u.Id == user.Id).Select(u => u.Partners).ToList();
                    //var roleids = sc.Roles.Select(r => r.Id);
                    //var rights = sc.Rights
                    //    .Where(r => r.LevelTwoId == leveltwo.Id && r.SetofBooksId == set.Id).ToList();
                    //var rightids = rights.Select(r => r.Id);
                    //var opids = sc.Accredits
                    //    .Where(a => roleids.Contains(a.RoleId) && rightids.Contains(a.RightId))
                    //    .Select(a => a.OperationId).ToList();
                    //var ops = sc.Operations.Where(o => opids.Contains(o.Id)).ToList();

                    //return ops;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// get operation dictionary for specific user
        /// </summary>
        public Dictionary<string, string> GetActionButtonTexts(List<Operation> ops)
        {
            return ops.ToDictionary(x => x.Name, x => x.MethodName);
        }
        #endregion

    }
    public class EntityComparer<S> : IEqualityComparer<S>
        where S : class, IbaseProperties, new()
    {
        public bool Equals(S x, S y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Id == y.Id;
        }

        public int GetHashCode(S entity)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(entity, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashRoleName = entity.Name == null ? 0 : entity.Name.GetHashCode();

            //Get hash code for the Id field.
            int hashRoleId = entity.Id.GetHashCode();

            //Calculate the hash code for the role.
            return hashRoleName ^ hashRoleId;
        }
    }

}
