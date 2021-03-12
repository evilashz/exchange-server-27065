using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000CB RID: 203
	internal abstract class ConfigurationContextBase
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001AAAB File Offset: 0x00018CAB
		public virtual AttachmentPolicy AttachmentPolicy
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0001AAB2 File Offset: 0x00018CB2
		public virtual WebBeaconFilterLevels FilterWebBeaconsAndHtmlForms
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001AAB9 File Offset: 0x00018CB9
		public virtual bool IsFeatureEnabled(Feature feature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		public virtual ulong GetFeaturesEnabled(Feature feature)
		{
			throw new NotImplementedException();
		}
	}
}
