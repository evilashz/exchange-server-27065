using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000908 RID: 2312
	[Serializable]
	internal class TaskNeedsConfigurationException : TaskExceptionBase
	{
		// Token: 0x0600520F RID: 21007 RVA: 0x001534BD File Offset: 0x001516BD
		public TaskNeedsConfigurationException(string taskName, Exception innerException, IEnumerable<LocalizedString> errors) : base("NeedsConfiguration", taskName, innerException, errors)
		{
		}
	}
}
