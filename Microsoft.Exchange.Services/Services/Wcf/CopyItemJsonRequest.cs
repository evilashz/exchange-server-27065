﻿using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BAF RID: 2991
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CopyItemJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F06 RID: 12038
		[DataMember(IsRequired = true, Order = 0)]
		public CopyItemRequest Body;
	}
}
