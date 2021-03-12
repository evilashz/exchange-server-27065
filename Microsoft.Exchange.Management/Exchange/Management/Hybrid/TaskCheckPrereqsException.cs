using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000903 RID: 2307
	[Serializable]
	internal class TaskCheckPrereqsException : TaskExceptionBase
	{
		// Token: 0x060051E7 RID: 20967 RVA: 0x00152F58 File Offset: 0x00151158
		public TaskCheckPrereqsException(string taskName, Exception innerException, IEnumerable<LocalizedString> errors) : base("CheckPrereqs", taskName, innerException, errors)
		{
		}
	}
}
