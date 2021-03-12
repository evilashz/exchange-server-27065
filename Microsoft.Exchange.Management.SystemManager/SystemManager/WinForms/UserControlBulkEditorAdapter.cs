using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000191 RID: 401
	public class UserControlBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x0003F1E1 File Offset: 0x0003D3E1
		public UserControlBulkEditorAdapter(ExchangeUserControl control) : base(control)
		{
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0003F1EC File Offset: 0x0003D3EC
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			if (base[e.PropertyName] == 2 || base[e.PropertyName] == 3)
			{
				ExchangeUserControl exchangeUserControl = base.HostControl as ExchangeUserControl;
				foreach (Control control in exchangeUserControl.ExposedPropertyRelatedControls[e.PropertyName])
				{
					control.Enabled = false;
				}
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0003F27C File Offset: 0x0003D47C
		protected override void InnerSetState(string propertyName, BulkEditorState state)
		{
			base.InnerSetState(propertyName, state);
			ExchangeUserControl exchangeUserControl = base.HostControl as ExchangeUserControl;
			foreach (BulkEditorAdapter bulkEditorAdapter in UserControlBulkEditorAdapter.GetBulkEditorAdapters(exchangeUserControl.ExposedPropertyRelatedControls[propertyName]))
			{
				bulkEditorAdapter.SetPropertiesState(state);
				bulkEditorAdapter.StateChanged += new BulkEditorAdapter.BulkEditorStateChangedEventHandler(this.BulkEditorAdapter_StateChanged);
			}
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003F300 File Offset: 0x0003D500
		private void BulkEditorAdapter_StateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			ExchangeUserControl exchangeUserControl = base.HostControl as ExchangeUserControl;
			foreach (KeyValuePair<string, HashSet<Control>> keyValuePair in exchangeUserControl.ExposedPropertyRelatedControls)
			{
				if (exchangeUserControl.ExposedPropertyRelatedControls[keyValuePair.Key].Contains(sender.HostControl))
				{
					base[keyValuePair.Key] = sender[e.PropertyName];
				}
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0003F390 File Offset: 0x0003D590
		protected override BulkEditorState InnerGetState(string propertyName)
		{
			return base.InnerGetState(propertyName);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003F3A8 File Offset: 0x0003D5A8
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			ExchangeUserControl exchangeUserControl = base.HostControl as ExchangeUserControl;
			foreach (string item in exchangeUserControl.ExposedPropertyRelatedControls.Keys)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003F414 File Offset: 0x0003D614
		private static HashSet<BulkEditorAdapter> GetBulkEditorAdapters(HashSet<Control> controlSet)
		{
			HashSet<BulkEditorAdapter> hashSet = new HashSet<BulkEditorAdapter>();
			foreach (Control control in controlSet)
			{
				IBulkEditor bulkEditor = control as IBulkEditor;
				if (bulkEditor != null)
				{
					hashSet.Add(bulkEditor.BulkEditorAdapter);
				}
			}
			return hashSet;
		}
	}
}
