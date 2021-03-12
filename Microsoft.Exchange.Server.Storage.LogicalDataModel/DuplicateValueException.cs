using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200005C RID: 92
	public class DuplicateValueException : StoreException
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x00045A0D File Offset: 0x00043C0D
		public DuplicateValueException(LID lid, string message) : base(lid, ErrorCodeValue.Collision, message)
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00045A1C File Offset: 0x00043C1C
		public DuplicateValueException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.Collision, message, innerException)
		{
		}
	}
}
