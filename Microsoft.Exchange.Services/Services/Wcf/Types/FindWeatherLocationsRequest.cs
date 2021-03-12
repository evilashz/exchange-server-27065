using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B57 RID: 2903
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindWeatherLocationsRequest : BaseRequest
	{
		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06005257 RID: 21079 RVA: 0x0010B37D File Offset: 0x0010957D
		// (set) Token: 0x06005258 RID: 21080 RVA: 0x0010B385 File Offset: 0x00109585
		[DataMember]
		public string SearchString { get; set; }

		// Token: 0x06005259 RID: 21081 RVA: 0x0010B38E File Offset: 0x0010958E
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x0010B391 File Offset: 0x00109591
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
