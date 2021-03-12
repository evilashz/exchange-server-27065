using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000193 RID: 403
	public sealed class FeatureLauncherBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0003F47C File Offset: 0x0003D67C
		public FeatureLauncherBulkEditorAdapter(FeatureLauncherPropertyControl featureLauncherControl) : base(featureLauncherControl)
		{
			((IFeatureLauncherBulkEditSupport)featureLauncherControl).FeatureItemUpdated += this.OnFeatureItemUpdated;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003F4A4 File Offset: 0x0003D6A4
		private void OnFeatureItemUpdated(object sender, EventArgs e)
		{
			this.UpdateFeatureItem(null);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003F4B0 File Offset: 0x0003D6B0
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			FeatureLauncherPropertyControl featureLauncherPropertyControl = base.HostControl as FeatureLauncherPropertyControl;
			foreach (object obj in featureLauncherPropertyControl.FeatureListView.Items)
			{
				FeatureLauncherListViewItem featureLauncherListViewItem = (FeatureLauncherListViewItem)obj;
				list.Add(featureLauncherListViewItem.StatusBindingName);
			}
			return list;
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003F52C File Offset: 0x0003D72C
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			Control hostControl = base.HostControl;
			FeatureLauncherListViewItem featureLauncherListViewItem = this.FindItemByPropertyName(e.PropertyName);
			this.UpdateFeatureItem(featureLauncherListViewItem);
			if (featureLauncherListViewItem != null && base[e.PropertyName] == null)
			{
				featureLauncherListViewItem.FireStatusChanged();
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0003F574 File Offset: 0x0003D774
		internal void UpdateFeatureItem(FeatureLauncherListViewItem featureItem)
		{
			FeatureLauncherPropertyControl featureLauncherPropertyControl = base.HostControl as FeatureLauncherPropertyControl;
			featureItem = (featureItem ?? (featureLauncherPropertyControl.FeatureListView.FirstSelectedItem as FeatureLauncherListViewItem));
			if (featureItem != null)
			{
				if (base[featureItem.StatusBindingName] == 3)
				{
					featureItem.IsLocked = true;
					if (object.ReferenceEquals(featureItem, featureLauncherPropertyControl.FeatureListView.FirstSelectedItem))
					{
						featureLauncherPropertyControl.PropertiesButton.Enabled = false;
						featureLauncherPropertyControl.EnableButton.Enabled = false;
						featureLauncherPropertyControl.DisableButton.Enabled = false;
					}
					featureLauncherPropertyControl.FeatureListView.DrawLockedIcon = true;
					featureLauncherPropertyControl.FeatureListView.Invalidate();
					return;
				}
				if (featureLauncherPropertyControl.EnablingButtonsVisible)
				{
					featureItem.BulkEditing = (base[featureItem.StatusBindingName] != 0);
					if (featureItem.BulkEditing && object.ReferenceEquals(featureItem, featureLauncherPropertyControl.FeatureListView.FirstSelectedItem))
					{
						featureLauncherPropertyControl.PropertiesButton.Enabled = (featureLauncherPropertyControl.PropertiesButton.Enabled && base[featureItem.StatusBindingName] != 2 && !featureItem.EnablePropertiesButtonOnFeatureStatus);
						bool flag = base[featureItem.StatusBindingName] != 2;
						flag = (flag && (featureLauncherPropertyControl.EnableButton.Enabled || featureLauncherPropertyControl.DisableButton.Enabled));
						featureLauncherPropertyControl.EnableButton.Enabled = flag;
						featureLauncherPropertyControl.DisableButton.Enabled = flag;
					}
				}
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0003F6D4 File Offset: 0x0003D8D4
		private FeatureLauncherListViewItem FindItemByPropertyName(string propertyName)
		{
			FeatureLauncherListViewItem result = null;
			FeatureLauncherPropertyControl featureLauncherPropertyControl = base.HostControl as FeatureLauncherPropertyControl;
			foreach (object obj in featureLauncherPropertyControl.FeatureListView.Items)
			{
				FeatureLauncherListViewItem featureLauncherListViewItem = (FeatureLauncherListViewItem)obj;
				if (featureLauncherListViewItem.StatusBindingName.Equals(propertyName))
				{
					result = featureLauncherListViewItem;
					break;
				}
			}
			return result;
		}
	}
}
