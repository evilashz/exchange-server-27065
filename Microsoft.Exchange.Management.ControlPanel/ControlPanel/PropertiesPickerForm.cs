using System;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200063A RID: 1594
	public class PropertiesPickerForm : BaseForm
	{
		// Token: 0x06004605 RID: 17925 RVA: 0x000D39C0 File Offset: 0x000D1BC0
		public PropertiesPickerForm()
		{
			base.ShowHeader = false;
			base.ShowHelp = false;
			base.HideFieldValidationAssistant = true;
		}

		// Token: 0x170026FD RID: 9981
		// (get) Token: 0x06004606 RID: 17926 RVA: 0x000D39DD File Offset: 0x000D1BDD
		protected Properties Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = (base.ContentControl as Properties);
				}
				return this.properties;
			}
		}

		// Token: 0x170026FE RID: 9982
		// (get) Token: 0x06004607 RID: 17927 RVA: 0x000D3A00 File Offset: 0x000D1C00
		protected PickerContent PickerContent
		{
			get
			{
				if (this.pickerContent == null)
				{
					foreach (object obj in this.Properties.ContentContainer.Controls)
					{
						Control control = (Control)obj;
						PickerContent pickerContent = control as PickerContent;
						if (pickerContent != null)
						{
							this.pickerContent = pickerContent;
							break;
						}
					}
				}
				return this.pickerContent;
			}
		}

		// Token: 0x06004608 RID: 17928 RVA: 0x000D3A80 File Offset: 0x000D1C80
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			try
			{
				this.PickerContent.SelectionMode = (PickerSelectionType)Enum.Parse(typeof(PickerSelectionType), base.Request["mode"] ?? string.Empty, true);
			}
			catch (ArgumentException innerException)
			{
				throw new BadQueryParameterException("mode", innerException);
			}
		}

		// Token: 0x06004609 RID: 17929 RVA: 0x000D3AEC File Offset: 0x000D1CEC
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.PickerContent.WrapperControlID = base.ContentPanel.ClientID;
			Properties properties = this.Properties;
			properties.CssClass += "PickerPane";
		}

		// Token: 0x04002F54 RID: 12116
		private Properties properties;

		// Token: 0x04002F55 RID: 12117
		private PickerContent pickerContent;
	}
}
