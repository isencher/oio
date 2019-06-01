using System.Collections.Generic;
using System.Data.Entity;

namespace ting.model.net
{
    internal class sysDBInitializer : DropCreateDatabaseAlways<sysContext>
    {

        protected override void Seed(sysContext context)
        {
            var sets = new List<SetofBook>
            {
                new SetofBook { Id = 1, Name = "ting系统", DbName = "tingSystem" },
                new SetofBook { Id = 2, Name = "江西中新置业有限公司", DbName = "tingZXZY" }
            };
            var role1 = new Role { Id = 1, Name = "管理员" };
            var role2 = new Role { Id = 2, Name = "会计" };
            var role3 = new Role { Id = 3, Name = "出纳" };
            var role4 = new Role { Id = 4, Name = "收银" };

            var users = new List<User>
            {
                    new User { Id =1, Name = "admin", Account = "0000", IsStop = false, Partners = new HashSet<Role> { role1 },
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =2, Name = "万小勇", Account = "0001", IsStop = false, Partners = new HashSet<Role> { role1 },
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =3, Name = "李睿敏", Account = "0002", IsStop = false, Partners = null,
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =4, Name = "黄卫红", Account = "0003", IsStop = false, Partners = null,
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =5, Name = "帅诗梦", Account = "0004", IsStop = false, Partners = null,
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =6, Name = "余燕", Account = "0005", IsStop = false, Partners = null,
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =7, Name = "万林华", Account = "0006", IsStop = false, Partners = null,
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =8, Name = "周侠", Account = "0007", IsStop = false, Partners = new HashSet<Role> { role3 },
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
                    new User { Id =9, Name = "张丽云", Account = "0008", IsStop = false, Partners = null,
                        Salt= "vOtKyPz3Yq0FpVKdg/1dgw==",SaltedHashedPassword= "bCXAL9+dMoklyJ8BhfEXCSsJkzXfRFSWTPhMdUFYAZ0=" },
            };
            var mods = new List<Module>
                {
                    new Module { Id = 1, Name = "系统设置", AssemblyName = "syst" },
                    new Module { Id = 2, Name = "现金台账", AssemblyName = "cash" },
                };
            var ones = new List<LevelOne>
            {
                new LevelOne { Id =1, Name = "软件配置", ModuleId = 1 },
                new LevelOne { Id =2, Name = "授权管理", ModuleId = 1 },
                new LevelOne { Id =3, Name = "台账设置", ModuleId = 2 },
                new LevelOne { Id =4, Name = "台账录入", ModuleId = 2 },
                new LevelOne { Id =5, Name = "台账查询", ModuleId = 2 },
            };
            var twos = new List<LevelTwo>
            {
                new LevelTwo { Id = 1, Name = "账套管理", ClassName = "ting.pal.setofbook.work", LevelOneId=1},
                new LevelTwo { Id = 2, Name = "软件模块", ClassName = "ting.pal.module.work",LevelOneId=1},
                new LevelTwo { Id = 3, Name = "一级菜单", ClassName = "ting.pal.levelone.work" ,LevelOneId=1},
                new LevelTwo { Id = 4, Name= "二级菜单",  ClassName = "ting.pal.leveltwo.work",LevelOneId=1},
                new LevelTwo { Id = 5, Name= "操作按钮",  ClassName = "ting.pal.operation.work",LevelOneId=1},
                new LevelTwo { Id = 6, Name= "角色管理",  ClassName = "ting.pal.role.work",LevelOneId=2},
                new LevelTwo { Id = 7, Name= "用户管理",  ClassName = "ting.pal.user.work",LevelOneId=2},
                new LevelTwo { Id = 8, Name= "功能启用",  ClassName = "ting.pal.right.work",LevelOneId=2},
                new LevelTwo { Id = 9, Name= "授权管理",  ClassName = "ting.pal.accredit.work",LevelOneId=2},
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------
                new LevelTwo { Id = 10, Name= "银行列表",  ClassName = "ting.cash.bank.work",LevelOneId=3},
                new LevelTwo { Id = 11, Name= "账户类型",  ClassName = "ting.cash.category.work",LevelOneId=3},
                new LevelTwo { Id = 12, Name= "收支类型",  ClassName = "ting.cash.inoutcategory.work",LevelOneId=3},
                new LevelTwo { Id = 13, Name= "单位列表",  ClassName = "ting.cash.unit.work",LevelOneId=3},
                new LevelTwo { Id = 14, Name= "项目列表",  ClassName = "ting.cash.project.work",LevelOneId=3},
                new LevelTwo { Id = 15, Name= "开立账户",  ClassName = "ting.cash.account.work",LevelOneId=4},
                new LevelTwo { Id = 16, Name= "登日记账",  ClassName = "ting.cash.standingbook.work",LevelOneId=4},
                new LevelTwo { Id = 17, Name= "日记明细",  ClassName = "ting.cash.daybook.work",LevelOneId=5},
                new LevelTwo { Id = 18, Name= "日记汇总",  ClassName = "ting.cash.daybooktotal.work",LevelOneId=5},
            };
            var ops = new List<Operation>
            {
                new Operation{Id = 1, Name="新增",MethodName="Add"},
                new Operation{Id = 2, Name="修改",MethodName="Alter"},
                new Operation{Id = 3, Name="删除",MethodName="Delete"},
                new Operation{Id = 4, Name="删除所有",MethodName="Deletes"},
                new Operation{Id = 5, Name="导入",MethodName="Import"},
                new Operation{Id = 6, Name="导出",MethodName="Export"},
                new Operation{Id = 7, Name="查询",MethodName="Query"},
                new Operation{Id = 8, Name="刷新",MethodName="Update"},
                new Operation{Id = 9, Name="关闭",MethodName="Close"},
            };
            //context.SetofBooks.Add(set);
            //context.Users.Add(user);
            //context.LevelTwos.AddRange(twos);
            //context.Operations.AddRange(op);
            //context.SaveChanges();
            var rights = new List<Right>
            {
                new Right{ Id = 1, RoleId=1,SetofBookId=1,LevelTwoId=1,Enabled = true},
                new Right{ Id = 2, RoleId=1,SetofBookId=1,LevelTwoId=2,Enabled = true},
                new Right{ Id = 3, RoleId=1,SetofBookId=1,LevelTwoId=3,Enabled = true},
                new Right{ Id = 4, RoleId=1,SetofBookId=1,LevelTwoId=4,Enabled = true},
                new Right{ Id = 5, RoleId=1,SetofBookId=1,LevelTwoId=5,Enabled = true},
                new Right{ Id = 6, RoleId=1,SetofBookId=1,LevelTwoId=6,Enabled = true},
                new Right{ Id = 7, RoleId=1,SetofBookId=1,LevelTwoId=7,Enabled = true},
                new Right{ Id = 8, RoleId=1,SetofBookId=1,LevelTwoId=8,Enabled = true},
                new Right{ Id = 9, RoleId=1,SetofBookId=1,LevelTwoId=9,Enabled = true},
                //-------------------------------------------------------------------------------------------------------------------------------
                new Right{ Id = 10, RoleId=3,SetofBookId=2,LevelTwoId=10,Enabled = true},
                new Right{ Id = 11, RoleId=3,SetofBookId=2,LevelTwoId=11,Enabled = true},
                new Right{ Id = 12, RoleId=3,SetofBookId=2,LevelTwoId=12,Enabled = true},
                new Right{ Id = 13, RoleId=3,SetofBookId=2,LevelTwoId=13,Enabled = true},
                new Right{ Id = 14, RoleId=3,SetofBookId=2,LevelTwoId=14,Enabled = true},
                new Right{ Id = 15, RoleId=3,SetofBookId=2,LevelTwoId=15,Enabled = true},
                new Right{ Id = 16, RoleId=3,SetofBookId=2,LevelTwoId=16,Enabled = true},
                new Right{ Id = 17, RoleId=3,SetofBookId=2,LevelTwoId=17,Enabled = true},
                new Right{ Id = 18, RoleId=3,SetofBookId=2,LevelTwoId=18,Enabled = true},
            };
            //context.Rights.AddRange(rights);
            //context.SaveChanges();
            var accs = new List<Accredit>
            {
                new Accredit{RightId=1,OperationId=1,Enabled=true},
                new Accredit{RightId=1,OperationId=2,Enabled=true},
                new Accredit{RightId=1,OperationId=3,Enabled=true},
                new Accredit{RightId=1,OperationId=4,Enabled=true},
                new Accredit{RightId=1,OperationId=5,Enabled=true},
                new Accredit{RightId=1,OperationId=6,Enabled=true},
                new Accredit{RightId=1,OperationId=7,Enabled=true},
                new Accredit{RightId=1,OperationId=8,Enabled=true},
                new Accredit{RightId=1,OperationId=9,Enabled=true},
                
                new Accredit{RightId=2,OperationId=1,Enabled=true},
                new Accredit{RightId=2,OperationId=2,Enabled=true},
                new Accredit{RightId=2,OperationId=3,Enabled=true},
                new Accredit{RightId=2,OperationId=4,Enabled=true},
                new Accredit{RightId=2,OperationId=5,Enabled=true},
                new Accredit{RightId=2,OperationId=6,Enabled=true},
                new Accredit{RightId=2,OperationId=7,Enabled=true},
                new Accredit{RightId=2,OperationId=8,Enabled=true},
                new Accredit{RightId=2,OperationId=9,Enabled=true},

                new Accredit{RightId=3,OperationId=1,Enabled=true},
                new Accredit{RightId=3,OperationId=2,Enabled=true},
                new Accredit{RightId=3,OperationId=3,Enabled=true},
                new Accredit{RightId=3,OperationId=4,Enabled=true},
                new Accredit{RightId=3,OperationId=5,Enabled=true},
                new Accredit{RightId=3,OperationId=6,Enabled=true},
                new Accredit{RightId=3,OperationId=7,Enabled=true},
                new Accredit{RightId=3,OperationId=8,Enabled=true},
                new Accredit{RightId=3,OperationId=9,Enabled=true},

                new Accredit{RightId=4,OperationId=1,Enabled=true},
                new Accredit{RightId=4,OperationId=2,Enabled=true},
                new Accredit{RightId=4,OperationId=3,Enabled=true},
                new Accredit{RightId=4,OperationId=4,Enabled=true},
                new Accredit{RightId=4,OperationId=5,Enabled=true},
                new Accredit{RightId=4,OperationId=6,Enabled=true},
                new Accredit{RightId=4,OperationId=7,Enabled=true},
                new Accredit{RightId=4,OperationId=8,Enabled=true},
                new Accredit{RightId=4,OperationId=9,Enabled=true},

                new Accredit{RightId=5,OperationId=1,Enabled=true},
                new Accredit{RightId=5,OperationId=2,Enabled=true},
                new Accredit{RightId=5,OperationId=3,Enabled=true},
                new Accredit{RightId=5,OperationId=4,Enabled=true},
                new Accredit{RightId=5,OperationId=5,Enabled=true},
                new Accredit{RightId=5,OperationId=6,Enabled=true},
                new Accredit{RightId=5,OperationId=7,Enabled=true},
                new Accredit{RightId=5,OperationId=8,Enabled=true},
                new Accredit{RightId=5,OperationId=9,Enabled=true},

                new Accredit{RightId=6,OperationId=1,Enabled=true},
                new Accredit{RightId=6,OperationId=2,Enabled=true},
                new Accredit{RightId=6,OperationId=3,Enabled=true},
                new Accredit{RightId=6,OperationId=4,Enabled=true},
                new Accredit{RightId=6,OperationId=5,Enabled=true},
                new Accredit{RightId=6,OperationId=6,Enabled=true},
                new Accredit{RightId=6,OperationId=7,Enabled=true},
                new Accredit{RightId=6,OperationId=8,Enabled=true},
                new Accredit{RightId=6,OperationId=9,Enabled=true},

                new Accredit{RightId=7,OperationId=1,Enabled=true},
                new Accredit{RightId=7,OperationId=2,Enabled=true},
                new Accredit{RightId=7,OperationId=3,Enabled=true},
                new Accredit{RightId=7,OperationId=4,Enabled=true},
                new Accredit{RightId=7,OperationId=5,Enabled=true},
                new Accredit{RightId=7,OperationId=6,Enabled=true},
                new Accredit{RightId=7,OperationId=7,Enabled=true},
                new Accredit{RightId=7,OperationId=8,Enabled=true},
                new Accredit{RightId=7,OperationId=9,Enabled=true},

                new Accredit{RightId=8,OperationId=1,Enabled=true},
                new Accredit{RightId=8,OperationId=2,Enabled=true},
                new Accredit{RightId=8,OperationId=3,Enabled=true},
                new Accredit{RightId=8,OperationId=4,Enabled=true},
                new Accredit{RightId=8,OperationId=5,Enabled=true},
                new Accredit{RightId=8,OperationId=6,Enabled=true},
                new Accredit{RightId=8,OperationId=7,Enabled=true},
                new Accredit{RightId=8,OperationId=8,Enabled=true},
                new Accredit{RightId=8,OperationId=9,Enabled=true},

                new Accredit{RightId=9,OperationId=1,Enabled=true},
                new Accredit{RightId=9,OperationId=2,Enabled=true},
                new Accredit{RightId=9,OperationId=3,Enabled=true},
                new Accredit{RightId=9,OperationId=4,Enabled=true},
                new Accredit{RightId=9,OperationId=5,Enabled=true},
                new Accredit{RightId=9,OperationId=6,Enabled=true},
                new Accredit{RightId=9,OperationId=7,Enabled=true},
                new Accredit{RightId=9,OperationId=8,Enabled=true},
                new Accredit{RightId=9,OperationId=9,Enabled=true},
                //--------------------------------------------------------------------------------------------
                new Accredit{RightId=10,OperationId=1,Enabled=true},
                new Accredit{RightId=10,OperationId=2,Enabled=true},
                new Accredit{RightId=10,OperationId=3,Enabled=true},
                new Accredit{RightId=10,OperationId=4,Enabled=true},
                new Accredit{RightId=10,OperationId=5,Enabled=true},
                new Accredit{RightId=10,OperationId=6,Enabled=true},
                new Accredit{RightId=10,OperationId=7,Enabled=true},
                new Accredit{RightId=10,OperationId=8,Enabled=true},
                new Accredit{RightId=10,OperationId=9,Enabled=true},

                new Accredit{RightId=11,OperationId=1,Enabled=true},
                new Accredit{RightId=11,OperationId=2,Enabled=true},
                new Accredit{RightId=11,OperationId=3,Enabled=true},
                new Accredit{RightId=11,OperationId=4,Enabled=true},
                new Accredit{RightId=11,OperationId=5,Enabled=true},
                new Accredit{RightId=11,OperationId=6,Enabled=true},
                new Accredit{RightId=11,OperationId=7,Enabled=true},
                new Accredit{RightId=11,OperationId=8,Enabled=true},
                new Accredit{RightId=11,OperationId=9,Enabled=true},

                new Accredit{RightId=12,OperationId=1,Enabled=true},
                new Accredit{RightId=12,OperationId=2,Enabled=true},
                new Accredit{RightId=12,OperationId=3,Enabled=true},
                new Accredit{RightId=12,OperationId=4,Enabled=true},
                new Accredit{RightId=12,OperationId=5,Enabled=true},
                new Accredit{RightId=12,OperationId=6,Enabled=true},
                new Accredit{RightId=12,OperationId=7,Enabled=true},
                new Accredit{RightId=12,OperationId=8,Enabled=true},
                new Accredit{RightId=12,OperationId=9,Enabled=true},

                new Accredit{RightId=13,OperationId=1,Enabled=true},
                new Accredit{RightId=13,OperationId=2,Enabled=true},
                new Accredit{RightId=13,OperationId=3,Enabled=true},
                new Accredit{RightId=13,OperationId=4,Enabled=true},
                new Accredit{RightId=13,OperationId=5,Enabled=true},
                new Accredit{RightId=13,OperationId=6,Enabled=true},
                new Accredit{RightId=13,OperationId=7,Enabled=true},
                new Accredit{RightId=13,OperationId=8,Enabled=true},
                new Accredit{RightId=13,OperationId=9,Enabled=true},

                new Accredit{RightId=14,OperationId=1,Enabled=true},
                new Accredit{RightId=14,OperationId=2,Enabled=true},
                new Accredit{RightId=14,OperationId=3,Enabled=true},
                new Accredit{RightId=14,OperationId=4,Enabled=true},
                new Accredit{RightId=14,OperationId=5,Enabled=true},
                new Accredit{RightId=14,OperationId=6,Enabled=true},
                new Accredit{RightId=14,OperationId=7,Enabled=true},
                new Accredit{RightId=14,OperationId=8,Enabled=true},
                new Accredit{RightId=14,OperationId=9,Enabled=true},

                new Accredit{RightId=15,OperationId=1,Enabled=true},
                new Accredit{RightId=15,OperationId=2,Enabled=true},
                new Accredit{RightId=15,OperationId=3,Enabled=true},
                new Accredit{RightId=15,OperationId=4,Enabled=true},
                new Accredit{RightId=15,OperationId=5,Enabled=true},
                new Accredit{RightId=15,OperationId=6,Enabled=true},
                new Accredit{RightId=15,OperationId=7,Enabled=true},
                new Accredit{RightId=15,OperationId=8,Enabled=true},
                new Accredit{RightId=15,OperationId=9,Enabled=true},

                new Accredit{RightId=16,OperationId=1,Enabled=true},
                new Accredit{RightId=16,OperationId=2,Enabled=true},
                new Accredit{RightId=16,OperationId=3,Enabled=true},
                new Accredit{RightId=16,OperationId=4,Enabled=true},
                new Accredit{RightId=16,OperationId=5,Enabled=true},
                new Accredit{RightId=16,OperationId=6,Enabled=true},
                new Accredit{RightId=16,OperationId=7,Enabled=true},
                new Accredit{RightId=16,OperationId=8,Enabled=true},
                new Accredit{RightId=16,OperationId=9,Enabled=true},

                new Accredit{RightId=17,OperationId=1,Enabled=true},
                new Accredit{RightId=17,OperationId=2,Enabled=true},
                new Accredit{RightId=17,OperationId=3,Enabled=true},
                new Accredit{RightId=17,OperationId=4,Enabled=true},
                new Accredit{RightId=17,OperationId=5,Enabled=true},
                new Accredit{RightId=17,OperationId=6,Enabled=true},
                new Accredit{RightId=17,OperationId=7,Enabled=true},
                new Accredit{RightId=17,OperationId=8,Enabled=true},
                new Accredit{RightId=17,OperationId=9,Enabled=true},

                new Accredit{RightId=18,OperationId=1,Enabled=true},
                new Accredit{RightId=18,OperationId=2,Enabled=true},
                new Accredit{RightId=18,OperationId=3,Enabled=true},
                new Accredit{RightId=18,OperationId=4,Enabled=true},
                new Accredit{RightId=18,OperationId=5,Enabled=true},
                new Accredit{RightId=18,OperationId=6,Enabled=true},
                new Accredit{RightId=18,OperationId=7,Enabled=true},
                new Accredit{RightId=18,OperationId=8,Enabled=true},
                new Accredit{RightId=18,OperationId=9,Enabled=true},
            };
            context.SetofBooks.AddRange(sets);
            context.Roles.Add(role1);
            context.Roles.Add(role2);
            context.Roles.Add(role3);
            context.Roles.Add(role4);
            context.Users.AddRange(users);
            context.Modules.AddRange(mods);
            context.LevelOnes.AddRange(ones);
            context.LevelTwos.AddRange(twos);
            context.Operations.AddRange(ops);
            context.Rights.AddRange(rights);
            context.Accredits.AddRange(accs);

            base.Seed(context);
        }
    }
}