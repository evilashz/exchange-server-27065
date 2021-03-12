using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000003 RID: 3
	internal static class ExceptionDefaults
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002290 File Offset: 0x00000490
		internal static string DefaultMachineName
		{
			get
			{
				return Environment.MachineName;
			}
		}
	}
}
