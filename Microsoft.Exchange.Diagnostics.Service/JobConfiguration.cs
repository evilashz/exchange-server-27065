using System;
using Microsoft.Exchange.Diagnostics.Service.Common;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200000B RID: 11
	public class JobConfiguration
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003E96 File Offset: 0x00002096
		public JobConfiguration(string jobName)
		{
			if (string.IsNullOrEmpty(jobName))
			{
				throw new ArgumentNullException("jobName");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003EB1 File Offset: 0x000020B1
		public string DiagnosticsRootDirectory
		{
			get
			{
				return DiagnosticsService.ServiceLogFolderPath;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003EB8 File Offset: 0x000020B8
		public string DiagnosticsRegistryRootKey
		{
			get
			{
				return DiagnosticsConfiguration.DiagnosticsRegistryKey;
			}
		}
	}
}
