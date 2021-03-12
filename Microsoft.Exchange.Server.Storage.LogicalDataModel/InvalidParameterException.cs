using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200005D RID: 93
	public class InvalidParameterException : StoreException
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x00045A2C File Offset: 0x00043C2C
		public InvalidParameterException(LID lid, string message) : base(lid, ErrorCodeValue.InvalidParameter, message)
		{
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00045A3B File Offset: 0x00043C3B
		public InvalidParameterException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.InvalidParameter, message, innerException)
		{
		}
	}
}
