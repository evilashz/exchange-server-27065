using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000065 RID: 101
	public class SearchFolderScopeViolationException : StoreException
	{
		// Token: 0x060007EE RID: 2030 RVA: 0x00045B24 File Offset: 0x00043D24
		public SearchFolderScopeViolationException(LID lid, string message) : base(lid, ErrorCodeValue.SearchFolderScopeViolation, message)
		{
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00045B33 File Offset: 0x00043D33
		public SearchFolderScopeViolationException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.SearchFolderScopeViolation, message, innerException)
		{
		}
	}
}
