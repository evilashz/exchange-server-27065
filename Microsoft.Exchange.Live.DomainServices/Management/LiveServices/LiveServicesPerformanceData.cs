using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Management.LiveServices
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class LiveServicesPerformanceData
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00007634 File Offset: 0x00005834
		public static PerformanceDataProvider SPFConnection
		{
			get
			{
				if (LiveServicesPerformanceData.spfConnection == null)
				{
					LiveServicesPerformanceData.spfConnection = new PerformanceDataProvider("SPF Connection");
				}
				return LiveServicesPerformanceData.spfConnection;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00007651 File Offset: 0x00005851
		public static PerformanceDataProvider SPFCall
		{
			get
			{
				if (LiveServicesPerformanceData.spfCall == null)
				{
					LiveServicesPerformanceData.spfCall = new PerformanceDataProvider("SPF Call");
				}
				return LiveServicesPerformanceData.spfCall;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000766E File Offset: 0x0000586E
		public static PerformanceDataProvider CredentialServicesCall
		{
			get
			{
				if (LiveServicesPerformanceData.credentialServicesCall == null)
				{
					LiveServicesPerformanceData.credentialServicesCall = new PerformanceDataProvider("CredentialServices Call");
				}
				return LiveServicesPerformanceData.credentialServicesCall;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000768B File Offset: 0x0000588B
		public static PerformanceDataProvider ProfileServicesCall
		{
			get
			{
				if (LiveServicesPerformanceData.profileServicesCall == null)
				{
					LiveServicesPerformanceData.profileServicesCall = new PerformanceDataProvider("ProfileServices Call");
				}
				return LiveServicesPerformanceData.profileServicesCall;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000076A8 File Offset: 0x000058A8
		public static PerformanceDataProvider NamespaceServicesCall
		{
			get
			{
				if (LiveServicesPerformanceData.namespaceServicesCall == null)
				{
					LiveServicesPerformanceData.namespaceServicesCall = new PerformanceDataProvider("NamespaceServices Call");
				}
				return LiveServicesPerformanceData.namespaceServicesCall;
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000076C5 File Offset: 0x000058C5
		public static IDisposable StartSPFConnectionRequest()
		{
			return LiveServicesPerformanceData.SPFConnection.StartRequestTimer();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000076D1 File Offset: 0x000058D1
		public static IDisposable StartSPFCallRequest()
		{
			return LiveServicesPerformanceData.SPFCall.StartRequestTimer();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000076DD File Offset: 0x000058DD
		public static IDisposable StartCredentialServicesCallRequest()
		{
			return LiveServicesPerformanceData.CredentialServicesCall.StartRequestTimer();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000076E9 File Offset: 0x000058E9
		public static IDisposable StartNamespaceServicesCallRequest()
		{
			return LiveServicesPerformanceData.NamespaceServicesCall.StartRequestTimer();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000076F5 File Offset: 0x000058F5
		public static IDisposable StartProfileServiceCallRequest()
		{
			return LiveServicesPerformanceData.ProfileServicesCall.StartRequestTimer();
		}

		// Token: 0x040000CF RID: 207
		[ThreadStatic]
		private static PerformanceDataProvider spfConnection;

		// Token: 0x040000D0 RID: 208
		[ThreadStatic]
		private static PerformanceDataProvider spfCall;

		// Token: 0x040000D1 RID: 209
		[ThreadStatic]
		private static PerformanceDataProvider credentialServicesCall;

		// Token: 0x040000D2 RID: 210
		[ThreadStatic]
		private static PerformanceDataProvider profileServicesCall;

		// Token: 0x040000D3 RID: 211
		[ThreadStatic]
		private static PerformanceDataProvider namespaceServicesCall;
	}
}
