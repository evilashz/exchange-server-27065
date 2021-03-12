using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B26 RID: 2854
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateAndPostModernGroupItemResponse : UpdateItemResponse
	{
		// Token: 0x060050DB RID: 20699 RVA: 0x00109FD3 File Offset: 0x001081D3
		public UpdateAndPostModernGroupItemResponse() : base(ResponseType.UpdateAndPostModernGroupItemResponseMessage)
		{
		}
	}
}
