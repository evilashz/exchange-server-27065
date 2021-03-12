using System;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001F5 RID: 501
	internal class PublicFolderPermissionLevelComboBoxAdapter
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x0005F7B4 File Offset: 0x0005D9B4
		public PublicFolderPermissionLevelComboBoxAdapter(ExchangeComboBox comboBox)
		{
			this.comboBox = comboBox;
			this.comboBox.DataSource = new PublicFolderPermissionLevelListSource();
			this.comboBox.DisplayMember = "Text";
			this.comboBox.ValueMember = "Value";
			this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox.Painted += this.DrawAppearance;
			this.comboBox.FocusSetted += this.DrawAppearance;
			this.comboBox.FocusKilled += this.DrawAppearance;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0005F888 File Offset: 0x0005DA88
		public void BindPermissionValue(object dataSource, string dataMember)
		{
			Binding binding = this.comboBox.DataBindings.Add("SelectedValue", dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
			binding.Format += delegate(object sender, ConvertEventArgs e)
			{
				this.Custom = (e.Value != null && !PublicFolderPermissionLevelListSource.ContainsPermission((PublicFolderPermission)e.Value));
			};
			binding.Parse += delegate(object sender, ConvertEventArgs e)
			{
				this.Custom = (e.Value == null);
			};
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060016BC RID: 5820 RVA: 0x0005F8D3 File Offset: 0x0005DAD3
		// (set) Token: 0x060016BD RID: 5821 RVA: 0x0005F8DB File Offset: 0x0005DADB
		private bool Custom
		{
			get
			{
				return this.custom;
			}
			set
			{
				if (this.custom != value)
				{
					this.custom = value;
					this.comboBox.Invalidate();
				}
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0005F8F8 File Offset: 0x0005DAF8
		private void DrawAppearance(object sender, EventArgs e)
		{
			if (this.Custom)
			{
				ComboBoxBulkEditorAdapter.DrawComboBoxText(this.comboBox, Strings.PublicFolderPermissionRoleCustom);
			}
		}

		// Token: 0x04000865 RID: 2149
		private const string SelectedValuePropertyName = "SelectedValue";

		// Token: 0x04000866 RID: 2150
		private ExchangeComboBox comboBox;

		// Token: 0x04000867 RID: 2151
		private bool custom;
	}
}
