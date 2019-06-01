using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using ting.lib;
using ting.model;

namespace ting.pal
{
    public class LoadByRight
    {
        //public FrmLogin LoginDialog
        //{ get => new FrmLogin(); }
        #region 身份注册与识别
        public static void LogIn(User user, string password)
        {
            if (withSecure.CheckPassword(user.Salt, user.SaltedHashedPassword, password))
            {
                var identity = new GenericIdentity(user.Name, "PacktAuth");
                var principal = new GenericPrincipal(identity, user.Roles);
                System.Threading.Thread.CurrentPrincipal = principal;
            }
        }
        #endregion
        #region 通过身份获取授权
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
            catch (Exception)
            {
                return null;
            }
        }
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
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<string> GetLevelOneButtonTexts(List<LevelOne> ones)
        {
            return ones.Select(o => o.Name).ToList();
        }
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
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<string, string> GetLevelTwoButtonTexts(List<LevelTwo> twos)
        {
            return twos.ToDictionary(x => x.Name, x => x.ClassName);
        }
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
            catch (Exception)
            {
                return null;
            }
        }
        public Dictionary<string, string> GetActionButtonTexts(List<Operation> ops)
        {
            return ops.ToDictionary(x => x.Name, x => x.MethodName);
        }
        #endregion

    }
}
