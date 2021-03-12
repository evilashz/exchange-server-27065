using System;
using System.ComponentModel;
using System.Web.Configuration;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005DF RID: 1503
	[TargetControlType(typeof(Control))]
	[ClientScriptResource("FVASettings", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	public class FieldValidationAssistantExtender : ExtenderControlBase
	{
		// Token: 0x1700262D RID: 9773
		// (get) Token: 0x0600437E RID: 17278 RVA: 0x000CC4CD File Offset: 0x000CA6CD
		// (set) Token: 0x0600437F RID: 17279 RVA: 0x000CC4D4 File Offset: 0x000CA6D4
		public static Func<string, string, string> HelpUrlBuilder { get; set; } = new Func<string, string, string>(HelpUtil.BuildFVAEhcHref);

		// Token: 0x06004380 RID: 17280 RVA: 0x000CC4DC File Offset: 0x000CA6DC
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			if (this.DebugMode)
			{
				descriptor.AddProperty("DebugMode", true);
			}
			descriptor.AddProperty("LocStringsResource", this.LocStringsResource, true);
			descriptor.AddProperty("Canvas", this.Canvas, true);
			descriptor.AddProperty("IndentCssClass", this.IndentCssClass, true);
			descriptor.AddProperty("HelpLinkPrefix", this.HelpLinkPrefix, true);
		}

		// Token: 0x1700262E RID: 9774
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x000CC551 File Offset: 0x000CA751
		[Browsable(false)]
		public bool DebugMode
		{
			get
			{
				return FieldValidationAssistantExtender.compilationSection.Debug;
			}
		}

		// Token: 0x1700262F RID: 9775
		// (get) Token: 0x06004382 RID: 17282 RVA: 0x000CC55D File Offset: 0x000CA75D
		// (set) Token: 0x06004383 RID: 17283 RVA: 0x000CC565 File Offset: 0x000CA765
		[Browsable(false)]
		public string LocStringsResource
		{
			get
			{
				return this.locStringsResource;
			}
			set
			{
				this.locStringsResource = value;
			}
		}

		// Token: 0x17002630 RID: 9776
		// (get) Token: 0x06004384 RID: 17284 RVA: 0x000CC56E File Offset: 0x000CA76E
		// (set) Token: 0x06004385 RID: 17285 RVA: 0x000CC576 File Offset: 0x000CA776
		[Browsable(false)]
		public string Canvas
		{
			get
			{
				return this.canvas;
			}
			set
			{
				this.canvas = value;
			}
		}

		// Token: 0x17002631 RID: 9777
		// (get) Token: 0x06004386 RID: 17286 RVA: 0x000CC57F File Offset: 0x000CA77F
		// (set) Token: 0x06004387 RID: 17287 RVA: 0x000CC587 File Offset: 0x000CA787
		[Browsable(false)]
		public string IndentCssClass
		{
			get
			{
				return this.indentCssClass;
			}
			set
			{
				this.indentCssClass = value;
			}
		}

		// Token: 0x17002632 RID: 9778
		// (get) Token: 0x06004388 RID: 17288 RVA: 0x000CC590 File Offset: 0x000CA790
		// (set) Token: 0x06004389 RID: 17289 RVA: 0x000CC598 File Offset: 0x000CA798
		[Browsable(false)]
		public string HelpLinkPrefix
		{
			get
			{
				return this.helpLinkPrefix;
			}
			private set
			{
				this.helpLinkPrefix = value;
			}
		}

		// Token: 0x17002633 RID: 9779
		// (get) Token: 0x0600438A RID: 17290 RVA: 0x000CC5A1 File Offset: 0x000CA7A1
		// (set) Token: 0x0600438B RID: 17291 RVA: 0x000CC5A9 File Offset: 0x000CA7A9
		[DefaultValue(EACHelpId.Default)]
		[Browsable(false)]
		public string HelpId
		{
			get
			{
				return this.helpId;
			}
			set
			{
				this.helpId = value;
			}
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000CC5D9 File Offset: 0x000CA7D9
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.HelpLinkPrefix = FieldValidationAssistantExtender.HelpUrlBuilder(this.HelpId, "{0}");
		}

		// Token: 0x04002DA8 RID: 11688
		private static CompilationSection compilationSection = (CompilationSection)WebConfigurationManager.GetSection("system.web/compilation");

		// Token: 0x04002DA9 RID: 11689
		private string locStringsResource;

		// Token: 0x04002DAA RID: 11690
		private string canvas;

		// Token: 0x04002DAB RID: 11691
		private string indentCssClass;

		// Token: 0x04002DAC RID: 11692
		private string helpLinkPrefix;

		// Token: 0x04002DAD RID: 11693
		private string helpId = EACHelpId.Default.ToString();
	}
}
