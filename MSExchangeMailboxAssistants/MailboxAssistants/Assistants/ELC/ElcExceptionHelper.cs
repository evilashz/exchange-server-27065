using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ElcExceptionHelper
	{
		// Token: 0x06000191 RID: 401 RVA: 0x0000B918 File Offset: 0x00009B18
		internal static AggregateException ExtractExceptionsFromAggregateOperationResult(AggregateOperationResult aggregateOperationResult)
		{
			AggregateException result = null;
			if (aggregateOperationResult != null && aggregateOperationResult.OperationResult != OperationResult.Succeeded && aggregateOperationResult.GroupOperationResults != null)
			{
				List<Exception> list = new List<Exception>();
				foreach (GroupOperationResult groupOperationResult in aggregateOperationResult.GroupOperationResults)
				{
					if (groupOperationResult != null && groupOperationResult.Exception != null)
					{
						list.Add(groupOperationResult.Exception);
					}
				}
				result = new AggregateException(list);
			}
			return result;
		}
	}
}
