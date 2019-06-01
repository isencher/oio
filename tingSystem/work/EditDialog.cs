using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.bll;
using ting.model;

namespace ting.pal
{
    public abstract class EditDialog<T> : Form
        where T : class, IbaseProperties, new()
    {
        public EditStatus EditStatus;
        public T EditEntity { get; set; }
        public Func<T, T, ActionResult> Save;
        public Action UpdateList;
        public T Original { get; set; }
        /// <summary>
        /// 判断一个实体在数据库中是否存在
        /// 第一个参数是【实体】
        /// 第二个参数是判断时所处状态： true - Add时; false - Alter时
        /// </summary>
        public Func<T, bool, bool> IsExist;
        protected abstract bool CheckValidity();
        protected abstract void InputLimits();
        protected abstract void BindingControls(T entity);
        protected void TextBoxBinding(TextBox txt, T entity, string DataProperty, string ControlProperty = "Text")
        {
            txt.DataBindings.Clear();
            txt.DataBindings.Add
            (
                new Binding(ControlProperty, entity, DataProperty)
            );
        }
        protected void ComboBoxBinding(ComboBox box, T entity,
            dynamic DataSource, string DataProperty,
            string ControlProperty = "SelectedValue",
            string DisplayMember = "Name", string ValueMember = "Id")
        {
            box.DataBindings.Clear();
            box.DataSource = DataSource;
            box.DisplayMember = DisplayMember;
            box.ValueMember = ValueMember;
            Binding a = new Binding(ControlProperty, entity, DataProperty);
            box.DataBindings.Add(a);
        }

        protected void CheckBoxBinding(CheckBox box, T entity, string DataProperty, string ControlProperty = "Checked")
        {
            box.DataBindings.Clear();
            box.DataBindings.Add
            (
                new Binding(ControlProperty, entity, DataProperty)
            );
        }

    }
}
