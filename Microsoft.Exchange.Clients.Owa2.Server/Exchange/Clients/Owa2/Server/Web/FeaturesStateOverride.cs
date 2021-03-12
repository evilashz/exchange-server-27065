using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000477 RID: 1143
	internal class FeaturesStateOverride : IFeaturesStateOverride
	{
		// Token: 0x060026C8 RID: 9928 RVA: 0x0008C93F File Offset: 0x0008AB3F
		internal FeaturesStateOverride(VariantConfigurationSnapshot snapshot, RecipientTypeDetails recipientTypeDetails)
		{
			this.snapshot = snapshot;
			this.recipientTypeDetails = recipientTypeDetails;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0008C958 File Offset: 0x0008AB58
		public bool IsFeatureEnabled(string featureId)
		{
			if (!string.IsNullOrEmpty(featureId) && featureId == this.snapshot.OwaClientServer.ModernGroups.Name)
			{
				if (RequestDispatcherUtilities.GetStringUrlParameter(HttpContext.Current, "sharepointapp") == "true")
				{
					return false;
				}
				if (this.recipientTypeDetails == RecipientTypeDetails.SharedMailbox)
				{
					return false;
				}
			}
			return string.IsNullOrEmpty(featureId) || (!(featureId == this.snapshot.OwaClientServer.O365ParityHeader.Name) && !(featureId == this.snapshot.OwaClientServer.O365Header.Name) && !(featureId == this.snapshot.OwaClientServer.O365G2Header.Name)) || !(RequestDispatcherUtilities.GetStringUrlParameter(HttpContext.Current, "sharepointapp") == "true");
		}

		// Token: 0x040016A4 RID: 5796
		private readonly VariantConfigurationSnapshot snapshot;

		// Token: 0x040016A5 RID: 5797
		private readonly RecipientTypeDetails recipientTypeDetails;
	}
}
