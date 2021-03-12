using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.InfoWorker.Availability
{
	// Token: 0x02000006 RID: 6
	[KnownType(typeof(GetUserAvailabilityRequest))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BaseAvailabilityRequest : BaseRequest
	{
		// Token: 0x06000036 RID: 54 RVA: 0x0000308C File Offset: 0x0000128C
		internal override void UpdatePerformanceCounters(ServiceCommandBase serviceCommand, IExchangeWebMethodResponse response)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000308E File Offset: 0x0000128E
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003091 File Offset: 0x00001291
		internal override bool ShouldReturnXDBMountedOn()
		{
			return false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003094 File Offset: 0x00001294
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
