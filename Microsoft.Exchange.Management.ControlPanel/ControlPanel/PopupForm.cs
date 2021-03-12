using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200031F RID: 799
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("PopupForm", "Microsoft.Exchange.Management.ControlPanel.Client.Pickers.js")]
	public class PopupForm : BaseForm
	{
		// Token: 0x06002EB5 RID: 11957 RVA: 0x0008EB88 File Offset: 0x0008CD88
		public PopupForm()
		{
			base.CommitButtonText = Strings.OkButtonText;
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x0008EBA0 File Offset: 0x0008CDA0
		internal override bool PassingDataOnClient
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x0008EBA3 File Offset: 0x0008CDA3
		// (set) Token: 0x06002EB8 RID: 11960 RVA: 0x0008EBAB File Offset: 0x0008CDAB
		public string ConverterType { get; set; }

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0008EBB4 File Offset: 0x0008CDB4
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (!string.IsNullOrEmpty(this.ConverterType))
			{
				descriptor.AddScriptProperty("Converter", "new " + this.ConverterType + "()");
			}
		}
	}
}
