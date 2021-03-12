using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000909 RID: 2313
	[Serializable]
	internal class TaskValidateConfigurationException : TaskExceptionBase
	{
		// Token: 0x06005210 RID: 21008 RVA: 0x001534CD File Offset: 0x001516CD
		public TaskValidateConfigurationException(string taskName, Exception innerException, IEnumerable<LocalizedString> errors) : base("ValidateConfiguration", taskName, innerException, errors)
		{
		}
	}
}
