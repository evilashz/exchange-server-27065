using System;
using System.Collections.Generic;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200019D RID: 413
	public sealed class DataListViewBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x06001068 RID: 4200 RVA: 0x0004076D File Offset: 0x0003E96D
		public DataListViewBulkEditorAdapter(DataListView control) : base(control)
		{
			this.bulkEditSupport = control;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00040780 File Offset: 0x0003E980
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			if (base["DataSource"] == 3)
			{
				(base.HostControl as DataListView).DrawLockedIcon = true;
				return;
			}
			if (base["DataSource"] != null)
			{
				this.bulkEditSupport.BulkEditingIndicatorText = base.BulkEditingIndicatorText;
				if (base[e.PropertyName] == 2)
				{
					base.HostControl.Enabled = false;
					return;
				}
			}
			else
			{
				this.bulkEditSupport.BulkEditingIndicatorText = string.Empty;
				this.bulkEditSupport.FireDataSourceChanged();
			}
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0004080C File Offset: 0x0003EA0C
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			list.Add("DataSource");
			return list;
		}

		// Token: 0x0400065E RID: 1630
		private const string ManagedPropertyName = "DataSource";

		// Token: 0x0400065F RID: 1631
		private IDataListViewBulkEditSupport bulkEditSupport;
	}
}
