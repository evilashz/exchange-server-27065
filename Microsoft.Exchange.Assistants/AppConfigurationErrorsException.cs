using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000049 RID: 73
	internal class AppConfigurationErrorsException : AIPermanentException
	{
		// Token: 0x0600029C RID: 668 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
		public AppConfigurationErrorsException(Exception innerException) : base(LocalizedString.Empty, innerException)
		{
		}
	}
}
