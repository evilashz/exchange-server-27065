using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200004B RID: 75
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(ScriptControlBase))]
	public abstract class ScriptComponent : ScriptControlBase
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x000528BD File Offset: 0x00050ABD
		protected ScriptComponent() : base(false)
		{
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000528C6 File Offset: 0x00050AC6
		[Browsable(false)]
		public string ComponentID
		{
			get
			{
				return this.ClientID;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000528CE File Offset: 0x00050ACE
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000528D0 File Offset: 0x00050AD0
		protected override void RenderContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000528D2 File Offset: 0x00050AD2
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000528D4 File Offset: 0x00050AD4
		protected override List<ScriptDescriptor> CreateScriptDescriptors()
		{
			return new List<ScriptDescriptor>(1)
			{
				new ScriptComponentDescriptor(this.ClientControlType)
			};
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000528FA File Offset: 0x00050AFA
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("id", this.ClientID);
		}
	}
}
