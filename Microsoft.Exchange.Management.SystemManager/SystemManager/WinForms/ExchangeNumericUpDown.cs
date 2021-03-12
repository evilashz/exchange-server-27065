using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E0 RID: 480
	public class ExchangeNumericUpDown : NumericUpDown, IBulkEditor, IBulkEditSupport
	{
		// Token: 0x060015BB RID: 5563 RVA: 0x00059490 File Offset: 0x00057690
		public ExchangeNumericUpDown()
		{
			base.Name = "ExchangeNumericUpDown";
			base.DataBindings.CollectionChanged += this.DataBindings_CollectionChanged;
			base.GotFocus += delegate(object param0, EventArgs param1)
			{
				base.Select(0, this.Text.Length);
			};
		}

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x060015BC RID: 5564 RVA: 0x000594E0 File Offset: 0x000576E0
		// (remove) Token: 0x060015BD RID: 5565 RVA: 0x00059518 File Offset: 0x00057718
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x060015BE RID: 5566 RVA: 0x0005954D File Offset: 0x0005774D
		protected void OnUserModified(EventArgs e)
		{
			if (this.UserModified != null)
			{
				this.UserModified(this, new PropertyChangedEventArgs("Value"));
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0005956D File Offset: 0x0005776D
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = new NumericUpDownBulkEditorAdapter(this);
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0005958C File Offset: 0x0005778C
		private void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Binding binding = e.Element as Binding;
			if (e.Action == CollectionChangeAction.Add && (binding.PropertyName == "Text" || binding.PropertyName == "Value"))
			{
				object constraintProvider = (binding.DataSource is BindingSource) ? ((BindingSource)binding.DataSource).DataSource : binding.DataSource;
				PropertyDefinitionConstraint[] propertyDefinitionConstraints = PropertyConstraintProvider.GetPropertyDefinitionConstraints(constraintProvider, binding.BindingMemberInfo.BindingField);
				for (int i = 0; i < propertyDefinitionConstraints.Length; i++)
				{
					if (propertyDefinitionConstraints[i].GetType() == typeof(RangedValueConstraint<int>))
					{
						RangedValueConstraint<int> rangedValueConstraint = (RangedValueConstraint<int>)propertyDefinitionConstraints[i];
						base.Maximum = rangedValueConstraint.MaximumValue;
						base.Minimum = rangedValueConstraint.MinimumValue;
						return;
					}
				}
			}
		}

		// Token: 0x040007D8 RID: 2008
		private BulkEditorAdapter bulkEditorAdapter;
	}
}
