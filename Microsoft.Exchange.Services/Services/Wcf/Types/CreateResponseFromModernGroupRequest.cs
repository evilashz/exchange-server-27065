using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B1A RID: 2842
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateResponseFromModernGroupRequest : CreateItemRequest
	{
		// Token: 0x060050A1 RID: 20641 RVA: 0x00109C94 File Offset: 0x00107E94
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateResponseFromModernGroup(callContext, this);
		}
	}
}
