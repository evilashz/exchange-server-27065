using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F3 RID: 2547
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class MaskAutoCompleteRecipientRequest : BaseRequest
	{
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x060047F9 RID: 18425 RVA: 0x00100D3C File Offset: 0x000FEF3C
		// (set) Token: 0x060047FA RID: 18426 RVA: 0x00100D44 File Offset: 0x000FEF44
		[DataMember]
		public string EmailAddress { get; set; }

		// Token: 0x060047FB RID: 18427 RVA: 0x00100D4D File Offset: 0x000FEF4D
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x00100D50 File Offset: 0x000FEF50
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
