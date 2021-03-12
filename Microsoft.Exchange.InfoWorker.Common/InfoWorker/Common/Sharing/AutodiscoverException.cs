using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x0200026A RID: 618
	[Serializable]
	public sealed class AutodiscoverException : SharingSynchronizationException
	{
		// Token: 0x06001199 RID: 4505 RVA: 0x00050E0A File Offset: 0x0004F00A
		public AutodiscoverException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00050E13 File Offset: 0x0004F013
		internal AutodiscoverException(LocalizedString message, UserSettings userSettings) : base(message)
		{
			this.Data.Add("User Settings", userSettings);
		}

		// Token: 0x04000B9B RID: 2971
		private const string UserSettingsAdditionalData = "User Settings";
	}
}
