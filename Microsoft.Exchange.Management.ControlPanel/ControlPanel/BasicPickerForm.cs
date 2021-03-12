using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000632 RID: 1586
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("BasicPickerForm", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	public class BasicPickerForm : PopupForm
	{
		// Token: 0x060045D4 RID: 17876 RVA: 0x000D3356 File Offset: 0x000D1556
		public BasicPickerForm()
		{
			base.CommitButtonText = Strings.OkButtonText;
			base.ShowHeader = false;
			base.HideFieldValidationAssistant = true;
			base.ShowHelp = false;
		}

		// Token: 0x170026EA RID: 9962
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x000D3383 File Offset: 0x000D1583
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string PickerContentID
		{
			get
			{
				return this.PickerContent.ClientID;
			}
		}

		// Token: 0x170026EB RID: 9963
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x000D3390 File Offset: 0x000D1590
		protected BasicPickerContent PickerContent
		{
			get
			{
				if (this.pickerContent == null)
				{
					foreach (object obj in base.ContentPanel.Controls)
					{
						Control control = (Control)obj;
						BasicPickerContent basicPickerContent = control as BasicPickerContent;
						if (basicPickerContent != null)
						{
							this.pickerContent = basicPickerContent;
							break;
						}
					}
				}
				if (this.pickerContent == null)
				{
					this.pickerContent = this.FindPickerContentRecursive(base.ContentPanel);
				}
				return this.pickerContent;
			}
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000D3424 File Offset: 0x000D1624
		private BasicPickerContent FindPickerContentRecursive(Control control)
		{
			if (control != null)
			{
				BasicPickerContent basicPickerContent = control as BasicPickerContent;
				if (basicPickerContent != null)
				{
					return basicPickerContent;
				}
				foreach (object obj in control.Controls)
				{
					Control control2 = (Control)obj;
					basicPickerContent = this.FindPickerContentRecursive(control2);
					if (basicPickerContent != null)
					{
						return basicPickerContent;
					}
				}
			}
			return null;
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x000D349C File Offset: 0x000D169C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddComponentProperty("PickerContent", this.PickerContentID, true);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000D34B8 File Offset: 0x000D16B8
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.PickerContent.WrapperControlID = base.ContentPanel.ClientID;
			if (!base.InPagePanel.CssClass.Contains("noHdr") && string.IsNullOrEmpty(base.Caption))
			{
				Panel inPagePanel = base.InPagePanel;
				inPagePanel.CssClass += " noCap";
			}
		}

		// Token: 0x04002F48 RID: 12104
		private BasicPickerContent pickerContent;
	}
}
