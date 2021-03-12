using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000B8 RID: 184
	internal class RemoteRoutingAddressGenerator
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x00030CEC File Offset: 0x0002EEEC
		public RemoteRoutingAddressGenerator(IConfigurationSession session)
		{
			QueryFilter filter = new BitMaskAndFilter(DomainContentConfigSchema.AcceptMessageTypes, 256UL);
			DomainContentConfig[] array = session.Find<DomainContentConfig>(session.GetOrgContainerId(), QueryScope.SubTree, filter, null, 1);
			if (array.Length > 0)
			{
				this.targetDeliveryDomain = array[0].DomainName.Domain;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00030D3A File Offset: 0x0002EF3A
		public ProxyAddress GenerateRemoteRoutingAddress(string alias, Task.ErrorLoggerDelegate errorWriter)
		{
			if (string.IsNullOrEmpty(this.targetDeliveryDomain))
			{
				errorWriter(new ErrorCannotFindTargetDeliveryDomainException(), ExchangeErrorCategory.Client, null);
			}
			return ProxyAddress.Parse(alias + "@" + this.targetDeliveryDomain);
		}

		// Token: 0x04000284 RID: 644
		private readonly string targetDeliveryDomain;
	}
}
