using System;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000009 RID: 9
	public class ReportingException : Exception
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000020E1 File Offset: 0x000002E1
		internal ReportingException()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020E9 File Offset: 0x000002E9
		internal ReportingException(string message) : base(message)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020F2 File Offset: 0x000002F2
		internal ReportingException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
