using System;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000165 RID: 357
	public abstract class StrongTypeListControl<T> : CustomToolStripList
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x00037C30 File Offset: 0x00035E30
		public StrongTypeListControl()
		{
			base.DataListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			base.DataListView.AutoGenerateColumns = false;
			base.AddCommand.Text = Strings.AddObject;
			base.EditCommand.Text = Strings.EditCommandTextE;
			base.AddCommand.Execute += this.OnAddStrongType;
			base.EditCommand.Execute += this.OnEditStrongType;
			base.Name = "StrongTypeListControl";
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00037CC0 File Offset: 0x00035EC0
		private void OnEditStrongType(object sender, EventArgs e)
		{
			StrongTypeEditor<T> strongTypeEditor = this.EditStrongTypeEditor();
			((StrongTypeEditorDataHandler<T>)strongTypeEditor.Validator).IsOpenedAsEdit = true;
			strongTypeEditor.StrongType = (T)((object)base.DataListView.SelectedObject);
			if (base.ShowDialog(strongTypeEditor) == DialogResult.OK)
			{
				this.InternalEditValue(base.DataListView.SelectedIndices[0], strongTypeEditor.StrongType);
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00037D28 File Offset: 0x00035F28
		private void OnAddStrongType(object sender, EventArgs e)
		{
			StrongTypeEditor<T> strongTypeEditor = this.NewStrongTypeEditor();
			if (base.ShowDialog(strongTypeEditor) == DialogResult.OK)
			{
				this.InternalAddValue(strongTypeEditor.StrongType);
			}
		}

		// Token: 0x06000E8E RID: 3726
		protected abstract StrongTypeEditor<T> NewStrongTypeEditor();

		// Token: 0x06000E8F RID: 3727
		protected abstract StrongTypeEditor<T> EditStrongTypeEditor();
	}
}
