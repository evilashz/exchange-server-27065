using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Live.DomainServices
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class DomainServicesPerformanceData
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000075EE File Offset: 0x000057EE
		public static PerformanceDataProvider DomainServicesConnection
		{
			get
			{
				if (DomainServicesPerformanceData.domainServicesConnection == null)
				{
					DomainServicesPerformanceData.domainServicesConnection = new PerformanceDataProvider("DomainServices Connection");
				}
				return DomainServicesPerformanceData.domainServicesConnection;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000760B File Offset: 0x0000580B
		public static PerformanceDataProvider DomainServicesCall
		{
			get
			{
				if (DomainServicesPerformanceData.domainServicesCall == null)
				{
					DomainServicesPerformanceData.domainServicesCall = new PerformanceDataProvider("DomainServices Call");
				}
				return DomainServicesPerformanceData.domainServicesCall;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00007628 File Offset: 0x00005828
		public static IDisposable StartDomainServicesCallRequest()
		{
			return DomainServicesPerformanceData.DomainServicesCall.StartRequestTimer();
		}

		// Token: 0x040000CD RID: 205
		[ThreadStatic]
		private static PerformanceDataProvider domainServicesConnection;

		// Token: 0x040000CE RID: 206
		[ThreadStatic]
		private static PerformanceDataProvider domainServicesCall;
	}
}
