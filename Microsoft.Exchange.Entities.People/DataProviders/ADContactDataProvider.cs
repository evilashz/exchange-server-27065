using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.People.DataProviders
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ADContactDataProvider
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000021DE File Offset: 0x000003DE
		internal ADContactDataProvider(IRecipientSession recipientSession, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("recipientSession", recipientSession);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.recipientSession = recipientSession;
			this.tracer = tracer;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000220C File Offset: 0x0000040C
		internal Result<ADRawEntry>[] GetBatchADObjects(ProxyAddress[] proxyAddresses)
		{
			ArgumentValidator.ThrowIfNull("proxyAddresses", proxyAddresses);
			if (proxyAddresses.Length == 0)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "ADContactDataProvider.GetBatchADObjects: no proxy addresses to get AD objects");
				return Array<Result<ADRawEntry>>.Empty;
			}
			ADPersonToContactConverterSet organizationalContactProperties = ADPersonToContactConverterSet.OrganizationalContactProperties;
			return this.recipientSession.FindByProxyAddresses(proxyAddresses, organizationalContactProperties.ADProperties);
		}

		// Token: 0x04000004 RID: 4
		private readonly IRecipientSession recipientSession;

		// Token: 0x04000005 RID: 5
		private readonly ITracer tracer;
	}
}
