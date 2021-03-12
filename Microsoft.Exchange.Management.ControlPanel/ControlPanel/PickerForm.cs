using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000320 RID: 800
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("PickerForm", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	public class PickerForm : PopupForm
	{
		// Token: 0x06002EBA RID: 11962 RVA: 0x0008EBEA File Offset: 0x0008CDEA
		public PickerForm()
		{
			base.CommitButtonText = Strings.OkButtonText;
			base.ShowHeader = false;
			base.HideFieldValidationAssistant = true;
			base.ShowHelp = false;
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x0008EC17 File Offset: 0x0008CE17
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string SelectionMode
		{
			get
			{
				return this.PickerContent.SelectionMode.ToString().ToLower();
			}
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x0008EC33 File Offset: 0x0008CE33
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public string PickerContentID
		{
			get
			{
				return this.PickerContent.ClientID;
			}
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x0008EC40 File Offset: 0x0008CE40
		protected PickerContent PickerContent
		{
			get
			{
				if (this.pickerContent == null)
				{
					foreach (object obj in base.ContentPanel.Controls)
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
				if (this.pickerContent == null)
				{
					this.pickerContent = this.FindPickerContentRecursive(base.ContentPanel);
				}
				return this.pickerContent;
			}
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x0008ECD4 File Offset: 0x0008CED4
		private PickerContent FindPickerContentRecursive(Control control)
		{
			if (control != null)
			{
				PickerContent pickerContent = control as PickerContent;
				if (pickerContent != null)
				{
					return pickerContent;
				}
				foreach (object obj in control.Controls)
				{
					Control control2 = (Control)obj;
					pickerContent = this.FindPickerContentRecursive(control2);
					if (pickerContent != null)
					{
						return pickerContent;
					}
				}
			}
			return null;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x0008ED4C File Offset: 0x0008CF4C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("SelectionMode", this.SelectionMode, true);
			descriptor.AddComponentProperty("PickerContent", this.PickerContentID, true);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x0008ED7C File Offset: 0x0008CF7C
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

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0008EDE8 File Offset: 0x0008CFE8
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

		// Token: 0x040022D5 RID: 8917
		private PickerContent pickerContent;
	}
}
