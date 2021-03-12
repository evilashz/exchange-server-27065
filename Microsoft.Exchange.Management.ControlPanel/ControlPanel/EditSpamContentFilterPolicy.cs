using System;
using Microsoft.Exchange.Management.DDIService;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000041 RID: 65
	public class EditSpamContentFilterPolicy : BaseForm
	{
		// Token: 0x06001991 RID: 6545 RVA: 0x00051FF0 File Offset: 0x000501F0
		protected override void OnLoad(EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
			{
				string text = base.Request.QueryString["id"];
				if (Antispam.IsDefaultPolicyIdentity(new Identity(text, text)))
				{
					this.spamContentFilter.Sections.Remove(this.spamContentFilter.Sections["Scope"]);
				}
			}
			base.OnLoad(e);
		}

		// Token: 0x04001AC4 RID: 6852
		protected PropertyPageSheet spamContentFilter;
	}
}
