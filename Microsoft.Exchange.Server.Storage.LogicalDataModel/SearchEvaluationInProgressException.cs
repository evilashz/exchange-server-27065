using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000064 RID: 100
	public class SearchEvaluationInProgressException : StoreException
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x00045B05 File Offset: 0x00043D05
		public SearchEvaluationInProgressException(LID lid, string message) : base(lid, ErrorCodeValue.SearchEvaluationInProgress, message)
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00045B14 File Offset: 0x00043D14
		public SearchEvaluationInProgressException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.SearchEvaluationInProgress, message, innerException)
		{
		}
	}
}
