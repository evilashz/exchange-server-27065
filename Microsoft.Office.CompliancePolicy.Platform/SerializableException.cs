using System;
using System.Collections.Generic;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	internal class SerializableException
	{
		// Token: 0x060001BD RID: 445 RVA: 0x0000637C File Offset: 0x0000457C
		public SerializableException(Exception ex)
		{
			ArgumentValidator.ThrowIfNull("ex", ex);
			List<string> list;
			List<string> list2;
			string exceptionChain;
			ExecutionLog.GetExceptionTypeAndDetails(ex, out list, out list2, out exceptionChain, true);
			this.ExceptionChain = exceptionChain;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BE RID: 446 RVA: 0x000063AE File Offset: 0x000045AE
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000063B6 File Offset: 0x000045B6
		public string ExceptionChain { get; private set; }
	}
}
