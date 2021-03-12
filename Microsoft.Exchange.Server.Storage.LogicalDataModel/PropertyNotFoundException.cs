using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000059 RID: 89
	public class PropertyNotFoundException : StoreException
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x00045999 File Offset: 0x00043B99
		public PropertyNotFoundException(LID lid, string message) : base(lid, ErrorCodeValue.NotFound, message)
		{
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000459A8 File Offset: 0x00043BA8
		public PropertyNotFoundException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.NotFound, message, innerException)
		{
		}
	}
}
