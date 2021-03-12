﻿using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C1B RID: 3099
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetHoldOnMailboxesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F72 RID: 12146
		[DataMember(IsRequired = true, Order = 0)]
		public GetHoldOnMailboxesRequest Body;
	}
}
