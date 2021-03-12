using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000905 RID: 2309
	[Serializable]
	internal class TaskConfigureException : TaskExceptionBase
	{
		// Token: 0x060051F5 RID: 20981 RVA: 0x001533CB File Offset: 0x001515CB
		public TaskConfigureException(string taskName, Exception innerException, IEnumerable<LocalizedString> errors) : base("Configure", taskName, innerException, errors)
		{
		}
	}
}
