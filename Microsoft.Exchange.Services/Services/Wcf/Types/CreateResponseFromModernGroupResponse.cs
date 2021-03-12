using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B24 RID: 2852
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateResponseFromModernGroupResponse : CreateItemResponse
	{
		// Token: 0x060050D8 RID: 20696 RVA: 0x00109F9E File Offset: 0x0010819E
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ItemInfoResponseMessage(code, error, value as ItemType[]);
		}
	}
}
