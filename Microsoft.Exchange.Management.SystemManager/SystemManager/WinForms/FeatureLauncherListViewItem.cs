using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001EA RID: 490
	[Serializable]
	public class FeatureLauncherListViewItem : ListViewItem
	{
		// Token: 0x060015E5 RID: 5605 RVA: 0x0005A7B8 File Offset: 0x000589B8
		protected FeatureLauncherListViewItem()
		{
			base.SubItems.Add(new ListViewItem.ListViewSubItem(this, LocalizedDescriptionAttribute.FromEnum(typeof(FeatureStatus), this.Status)));
			base.UseItemStyleForSubItems = false;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x0005A817 File Offset: 0x00058A17
		public FeatureLauncherListViewItem(string featureName, string statusPropertyName, Icon icon) : this()
		{
			base.Text = featureName;
			base.Name = featureName;
			this.statusPropertyName = statusPropertyName;
			this.icon = icon;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x0005A83B File Offset: 0x00058A3B
		public string FeatureName
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0005A843 File Offset: 0x00058A43
		public string StatusPropertyName
		{
			get
			{
				return this.statusPropertyName;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0005A84B File Offset: 0x00058A4B
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0005A853 File Offset: 0x00058A53
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x0005A85B File Offset: 0x00058A5B
		[DefaultValue(FeatureStatus.None)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public FeatureStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				if (value != this.status)
				{
					this.status = value;
					this.StatusText = LocalizedDescriptionAttribute.FromEnum(typeof(FeatureStatus), value);
					this.OnStatusChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0005A893 File Offset: 0x00058A93
		internal void FireStatusChanged()
		{
			this.OnStatusChanged(EventArgs.Empty);
		}

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x060015ED RID: 5613 RVA: 0x0005A8A0 File Offset: 0x00058AA0
		// (remove) Token: 0x060015EE RID: 5614 RVA: 0x0005A8D8 File Offset: 0x00058AD8
		public event EventHandler StatusChanged;

		// Token: 0x060015EF RID: 5615 RVA: 0x0005A90D File Offset: 0x00058B0D
		protected virtual void OnStatusChanged(EventArgs e)
		{
			if (this.StatusChanged != null)
			{
				this.StatusChanged(this, e);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0005A924 File Offset: 0x00058B24
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x0005A92C File Offset: 0x00058B2C
		[DefaultValue(DataSourceUpdateMode.OnPropertyChanged)]
		public DataSourceUpdateMode DataSourceUpdateMode
		{
			get
			{
				return this.dataSourceUpdateMode;
			}
			set
			{
				this.dataSourceUpdateMode = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0005A935 File Offset: 0x00058B35
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x0005A948 File Offset: 0x00058B48
		private string StatusText
		{
			get
			{
				return base.SubItems[1].Text;
			}
			set
			{
				base.SubItems[1].Text = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0005A95C File Offset: 0x00058B5C
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x0005A964 File Offset: 0x00058B64
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				if (value != this.Description)
				{
					this.description = value;
				}
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0005A97B File Offset: 0x00058B7B
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0005A984 File Offset: 0x00058B84
		[DefaultValue(null)]
		public Type PropertyPageControl
		{
			get
			{
				return this.propertyPageControl;
			}
			set
			{
				if (!(null == value) && !typeof(ExchangePropertyPageControl).IsAssignableFrom(value))
				{
					throw new ArgumentOutOfRangeException("value", "The PropertyPageControl must derive from ExchangePropertyPageControl");
				}
				if (value != this.PropertyPageControl)
				{
					this.propertyPageControl = value;
					return;
				}
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0005A9D2 File Offset: 0x00058BD2
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x0005A9DA File Offset: 0x00058BDA
		public bool CanChangeStatus
		{
			get
			{
				return this.canChangeStatus;
			}
			set
			{
				this.canChangeStatus = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0005A9E3 File Offset: 0x00058BE3
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x0005A9EB File Offset: 0x00058BEB
		public bool EnablePropertiesButtonOnFeatureStatus
		{
			get
			{
				return this.enablePropertiesButtonOnFeatureStatus;
			}
			set
			{
				this.enablePropertiesButtonOnFeatureStatus = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0005A9F4 File Offset: 0x00058BF4
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x0005A9FC File Offset: 0x00058BFC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		public bool BulkEditing
		{
			get
			{
				return this.bulkEditing;
			}
			set
			{
				if (this.bulkEditing != value)
				{
					this.bulkEditing = value;
					if (this.BulkEditing)
					{
						this.StatusText = Strings.BulkEditingDefaultValueForFeatureItem;
						return;
					}
					this.StatusText = LocalizedDescriptionAttribute.FromEnum(typeof(FeatureStatus), this.Status);
				}
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0005AA54 File Offset: 0x00058C54
		public string UniqueName
		{
			get
			{
				if (string.IsNullOrEmpty(this.uniqueName))
				{
					this.uniqueName = (string.IsNullOrEmpty(this.StatusPropertyName) ? Guid.NewGuid().ToString() : this.StatusPropertyName);
				}
				return this.uniqueName;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0005AAA2 File Offset: 0x00058CA2
		public string StatusBindingName
		{
			get
			{
				return string.Format("{0}_{1}", this.UniqueName, "Status");
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x0005AAB9 File Offset: 0x00058CB9
		// (set) Token: 0x06001601 RID: 5633 RVA: 0x0005AAC1 File Offset: 0x00058CC1
		public bool IsBanned { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x0005AACA File Offset: 0x00058CCA
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x0005AAD2 File Offset: 0x00058CD2
		public bool IsLocked { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0005AADB File Offset: 0x00058CDB
		// (set) Token: 0x06001605 RID: 5637 RVA: 0x0005AAE3 File Offset: 0x00058CE3
		public string BannedMessage { get; set; }

		// Token: 0x040007F6 RID: 2038
		private FeatureStatus status;

		// Token: 0x040007F7 RID: 2039
		private string description = string.Empty;

		// Token: 0x040007F8 RID: 2040
		private Type propertyPageControl;

		// Token: 0x040007F9 RID: 2041
		private string statusPropertyName;

		// Token: 0x040007FA RID: 2042
		private Icon icon;

		// Token: 0x040007FB RID: 2043
		private bool canChangeStatus = true;

		// Token: 0x040007FC RID: 2044
		private bool enablePropertiesButtonOnFeatureStatus;

		// Token: 0x040007FE RID: 2046
		private DataSourceUpdateMode dataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

		// Token: 0x040007FF RID: 2047
		private bool bulkEditing;

		// Token: 0x04000800 RID: 2048
		private string uniqueName;
	}
}
