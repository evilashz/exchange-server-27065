using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000676 RID: 1654
	[ToolboxData("<{0}:UploaderBase runat=server></{0}:UploaderBase>")]
	[ClientScriptResource("UploaderBase", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class UploaderBase : ScriptControlBase
	{
		// Token: 0x06004792 RID: 18322 RVA: 0x000D9CB0 File Offset: 0x000D7EB0
		public UploaderBase() : base(HtmlTextWriterTag.Div)
		{
			this.iframeElement = new HtmlGenericControl(HtmlTextWriterTag.Iframe.ToString());
			this.iframeElement.ID = "iframe";
			this.iframeElement.Attributes["class"] = "UploaderIframe" + (Util.IsSafari() ? " UploaderIframe-Safari" : string.Empty);
			if (Util.IsIE())
			{
				this.iframeElement.Attributes["src"] = ThemeResource.BlankHtmlPath;
			}
			this.Controls.Add(this.iframeElement);
			this.Controls.Add(new LiteralControl("<br />"));
			this.errorLabel = new EncodingLabel();
			this.errorLabel.ID = "errorLbl";
			this.Controls.Add(this.errorLabel);
			this.Bindings = new DataContractBinding();
		}

		// Token: 0x17002778 RID: 10104
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x000D9DA7 File Offset: 0x000D7FA7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002779 RID: 10105
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x000D9DAF File Offset: 0x000D7FAF
		// (set) Token: 0x06004795 RID: 18325 RVA: 0x000D9DB7 File Offset: 0x000D7FB7
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] ParameterNames { get; set; }

		// Token: 0x1700277A RID: 10106
		// (get) Token: 0x06004796 RID: 18326 RVA: 0x000D9DC0 File Offset: 0x000D7FC0
		// (set) Token: 0x06004797 RID: 18327 RVA: 0x000D9DC8 File Offset: 0x000D7FC8
		public DataContractBinding Bindings { get; private set; }

		// Token: 0x1700277B RID: 10107
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x000D9DD1 File Offset: 0x000D7FD1
		// (set) Token: 0x06004799 RID: 18329 RVA: 0x000D9DD9 File Offset: 0x000D7FD9
		public UploadHandlers UploadHandlerClass { get; set; }

		// Token: 0x1700277C RID: 10108
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x000D9DE2 File Offset: 0x000D7FE2
		// (set) Token: 0x0600479B RID: 18331 RVA: 0x000D9DEA File Offset: 0x000D7FEA
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Extensions { get; set; }

		// Token: 0x0600479C RID: 18332 RVA: 0x000D9DF4 File Offset: 0x000D7FF4
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.UploadHandlerClass == UploadHandlers.None)
			{
				throw new ArgumentNullException("UploadHandlerClass", "UploadHandlerClass must be set.");
			}
			descriptor.AddProperty("UploadHandlerClass", this.UploadHandlerClass.ToString());
			if (!this.Extensions.IsNullOrEmpty())
			{
				descriptor.AddProperty("Extensions", this.Extensions);
			}
			if (!this.ParameterNames.IsNullOrEmpty())
			{
				descriptor.AddProperty("ParameterNames", this.ParameterNames);
			}
			descriptor.AddElementProperty("Iframe", this.iframeElement.ClientID);
			descriptor.AddElementProperty("ErrorLbl", this.errorLabel.ClientID);
			foreach (Binding binding in this.Parameters)
			{
				this.Bindings.Bindings.Add(binding.Name, binding);
			}
			descriptor.AddScriptProperty("Parameters", this.Bindings.ToJavaScript(this));
		}

		// Token: 0x0400301B RID: 12315
		private HtmlGenericControl iframeElement;

		// Token: 0x0400301C RID: 12316
		private EncodingLabel errorLabel;

		// Token: 0x0400301D RID: 12317
		private BindingCollection parameters = new BindingCollection();
	}
}
