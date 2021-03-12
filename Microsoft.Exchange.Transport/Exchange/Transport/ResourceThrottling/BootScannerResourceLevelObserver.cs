using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x0200000F RID: 15
	internal class BootScannerResourceLevelObserver : ResourceLevelObserver
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002C70 File Offset: 0x00000E70
		public BootScannerResourceLevelObserver(IStartableTransportComponent bootScanner) : base("BootScanner", bootScanner, new List<ResourceIdentifier>
		{
			new ResourceIdentifier("PrivateBytes", ""),
			new ResourceIdentifier("QueueLength", "SubmissionQueue")
		})
		{
		}

		// Token: 0x04000028 RID: 40
		internal const string ResourceObserverName = "BootScanner";
	}
}
