using System;
using System.Collections;
using System.ComponentModel;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000205 RID: 517
	public class SelectionCommandVisibilityBindingUtil : SelectionCommandUpdatingUtil
	{
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00062EB2 File Offset: 0x000610B2
		public DataListViewResultPane DataListViewResultPane
		{
			get
			{
				return base.ResultPane as DataListViewResultPane;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x00062EBF File Offset: 0x000610BF
		// (set) Token: 0x06001786 RID: 6022 RVA: 0x00062EC7 File Offset: 0x000610C7
		public string PropertyName { get; set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x00062ED0 File Offset: 0x000610D0
		// (set) Token: 0x06001788 RID: 6024 RVA: 0x00062ED8 File Offset: 0x000610D8
		public object TrueValue { get; set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001789 RID: 6025 RVA: 0x00062EE1 File Offset: 0x000610E1
		// (set) Token: 0x0600178A RID: 6026 RVA: 0x00062EE9 File Offset: 0x000610E9
		public bool AllowMixedValues { get; set; }

		// Token: 0x0600178B RID: 6027 RVA: 0x00062EF4 File Offset: 0x000610F4
		protected override void UpdateCommand()
		{
			if (!string.IsNullOrEmpty(this.PropertyName))
			{
				SelectionCommandVisibilityBindingUtil.PropertyStatus propertyStatus = this.GetPropertyStatus(this.AllowMixedValues);
				base.Command.Visible = ((propertyStatus & SelectionCommandVisibilityBindingUtil.PropertyStatus.True) == SelectionCommandVisibilityBindingUtil.PropertyStatus.True);
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00062F2C File Offset: 0x0006112C
		private SelectionCommandVisibilityBindingUtil.PropertyStatus GetPropertyStatus()
		{
			SelectionCommandVisibilityBindingUtil.PropertyStatus result = SelectionCommandVisibilityBindingUtil.PropertyStatus.None;
			IEnumerator enumerator = this.DataListViewResultPane.SelectedObjects.GetEnumerator();
			if (enumerator.MoveNext())
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(enumerator.Current)[this.PropertyName];
				if (propertyDescriptor != null)
				{
					object propertyValue = WinformsHelper.GetPropertyValue(enumerator.Current, propertyDescriptor);
					result = (object.Equals(propertyValue, this.TrueValue) ? SelectionCommandVisibilityBindingUtil.PropertyStatus.True : SelectionCommandVisibilityBindingUtil.PropertyStatus.False);
					while (enumerator.MoveNext())
					{
						object component = enumerator.Current;
						object propertyValue2 = WinformsHelper.GetPropertyValue(component, propertyDescriptor);
						if (!object.Equals(propertyValue, propertyValue2))
						{
							result = SelectionCommandVisibilityBindingUtil.PropertyStatus.None;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00062FB8 File Offset: 0x000611B8
		private SelectionCommandVisibilityBindingUtil.PropertyStatus GetPropertyStatus(bool allowMixedValues)
		{
			if (!allowMixedValues)
			{
				return this.GetPropertyStatus();
			}
			SelectionCommandVisibilityBindingUtil.PropertyStatus propertyStatus = SelectionCommandVisibilityBindingUtil.PropertyStatus.None;
			foreach (object component in this.DataListViewResultPane.SelectedObjects)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[this.PropertyName];
				if (propertyDescriptor != null)
				{
					object propertyValue = WinformsHelper.GetPropertyValue(component, propertyDescriptor);
					propertyStatus |= (object.Equals(propertyValue, this.TrueValue) ? SelectionCommandVisibilityBindingUtil.PropertyStatus.True : SelectionCommandVisibilityBindingUtil.PropertyStatus.False);
					if (propertyStatus == SelectionCommandVisibilityBindingUtil.PropertyStatus.TrueAndFalse)
					{
						break;
					}
				}
			}
			return propertyStatus;
		}

		// Token: 0x02000206 RID: 518
		[Flags]
		private enum PropertyStatus
		{
			// Token: 0x040008D1 RID: 2257
			None = 0,
			// Token: 0x040008D2 RID: 2258
			True = 1,
			// Token: 0x040008D3 RID: 2259
			False = 2,
			// Token: 0x040008D4 RID: 2260
			TrueAndFalse = 3
		}
	}
}
