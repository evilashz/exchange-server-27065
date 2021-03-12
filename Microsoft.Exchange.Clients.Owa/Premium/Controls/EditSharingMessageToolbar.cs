using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200036F RID: 879
	public class EditSharingMessageToolbar : EditMessageToolbar
	{
		// Token: 0x06002106 RID: 8454 RVA: 0x000BE09A File Offset: 0x000BC29A
		internal EditSharingMessageToolbar(Importance importance, Markup currentMarkup, bool isPublishing) : base(importance, currentMarkup)
		{
			base.IsComplianceButtonAllowedInForm = false;
			this.isPublishing = isPublishing;
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x000BE0B2 File Offset: 0x000BC2B2
		protected override string HelpId
		{
			get
			{
				if (!this.isPublishing)
				{
					return HelpIdsLight.DefaultLight.ToString();
				}
				return HelpIdsLight.DefaultLight.ToString();
			}
		}

		// Token: 0x04001799 RID: 6041
		private bool isPublishing;
	}
}
