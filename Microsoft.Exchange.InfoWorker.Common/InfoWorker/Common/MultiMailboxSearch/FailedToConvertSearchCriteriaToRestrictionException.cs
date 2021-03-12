using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001CF RID: 463
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class FailedToConvertSearchCriteriaToRestrictionException : MultiMailboxSearchException
	{
		// Token: 0x06000C4C RID: 3148 RVA: 0x000355ED File Offset: 0x000337ED
		public FailedToConvertSearchCriteriaToRestrictionException(string query, Guid database, string error, Exception innerException) : base(Strings.FailedToConvertSearchCriteriaToRestriction(query, database.ToString(), error), innerException)
		{
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0003560B File Offset: 0x0003380B
		protected FailedToConvertSearchCriteriaToRestrictionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
