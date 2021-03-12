using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D0F RID: 3343
	[Serializable]
	public abstract class DiscoverySearchBase : EwsStoreObject
	{
		// Token: 0x0600731A RID: 29466 RVA: 0x001FE93C File Offset: 0x001FCB3C
		public DiscoverySearchBase()
		{
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x0600731B RID: 29467 RVA: 0x001FE944 File Offset: 0x001FCB44
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x0600731C RID: 29468 RVA: 0x001FE94B File Offset: 0x001FCB4B
		// (set) Token: 0x0600731D RID: 29469 RVA: 0x001FE953 File Offset: 0x001FCB53
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string Name
		{
			get
			{
				return base.AlternativeId;
			}
			set
			{
				base.AlternativeId = value;
			}
		}
	}
}
