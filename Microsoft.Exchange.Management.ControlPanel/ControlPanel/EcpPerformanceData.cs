using System;
using Microsoft.Exchange.Diagnostics.LatencyDetection;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000281 RID: 641
	internal static class EcpPerformanceData
	{
		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x00083B63 File Offset: 0x00081D63
		public static PerformanceDataProvider CreateRbacSession
		{
			get
			{
				if (EcpPerformanceData.createRbacSession == null)
				{
					EcpPerformanceData.createRbacSession = new PerformanceDataProvider("CreateRbacSession");
				}
				return EcpPerformanceData.createRbacSession;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x060029E9 RID: 10729 RVA: 0x00083B80 File Offset: 0x00081D80
		public static PerformanceDataProvider ActiveRunspace
		{
			get
			{
				if (EcpPerformanceData.activeRunspace == null)
				{
					EcpPerformanceData.activeRunspace = new PerformanceDataProvider("ActiveRunspace");
				}
				return EcpPerformanceData.activeRunspace;
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x060029EA RID: 10730 RVA: 0x00083B9D File Offset: 0x00081D9D
		public static PerformanceDataProvider CreateRunspace
		{
			get
			{
				if (EcpPerformanceData.createRunspace == null)
				{
					EcpPerformanceData.createRunspace = new PerformanceDataProvider("CreateRunspace");
				}
				return EcpPerformanceData.createRunspace;
			}
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x060029EB RID: 10731 RVA: 0x00083BBA File Offset: 0x00081DBA
		internal static PerformanceDataProvider PowerShellInvoke
		{
			get
			{
				if (EcpPerformanceData.powerShellInvoke == null)
				{
					EcpPerformanceData.powerShellInvoke = new PerformanceDataProvider("PowerShellInvoke");
				}
				return EcpPerformanceData.powerShellInvoke;
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x00083BD7 File Offset: 0x00081DD7
		internal static PerformanceDataProvider WcfSerialization
		{
			get
			{
				if (EcpPerformanceData.wcfSerialization == null)
				{
					EcpPerformanceData.wcfSerialization = new PerformanceDataProvider("WcfSerialization");
				}
				return EcpPerformanceData.wcfSerialization;
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x00083BF4 File Offset: 0x00081DF4
		public static PerformanceDataProvider XamlParsed
		{
			get
			{
				if (EcpPerformanceData.xamlParsed == null)
				{
					EcpPerformanceData.xamlParsed = new PerformanceDataProvider("Xaml Parsed");
				}
				return EcpPerformanceData.xamlParsed;
			}
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x060029EE RID: 10734 RVA: 0x00083C11 File Offset: 0x00081E11
		public static PerformanceDataProvider DDIServiceExecution
		{
			get
			{
				if (EcpPerformanceData.ddiServiceExecution == null)
				{
					EcpPerformanceData.ddiServiceExecution = new PerformanceDataProvider("DDI Service Execution");
				}
				return EcpPerformanceData.ddiServiceExecution;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x060029EF RID: 10735 RVA: 0x00083C2E File Offset: 0x00081E2E
		public static PerformanceDataProvider DDITypeConversion
		{
			get
			{
				if (EcpPerformanceData.ddiTypeConversion == null)
				{
					EcpPerformanceData.ddiTypeConversion = new PerformanceDataProvider("DDI Type Conversion");
				}
				return EcpPerformanceData.ddiTypeConversion;
			}
		}

		// Token: 0x040020F9 RID: 8441
		[ThreadStatic]
		private static PerformanceDataProvider createRbacSession;

		// Token: 0x040020FA RID: 8442
		[ThreadStatic]
		private static PerformanceDataProvider activeRunspace;

		// Token: 0x040020FB RID: 8443
		[ThreadStatic]
		private static PerformanceDataProvider createRunspace;

		// Token: 0x040020FC RID: 8444
		[ThreadStatic]
		private static PerformanceDataProvider powerShellInvoke;

		// Token: 0x040020FD RID: 8445
		[ThreadStatic]
		private static PerformanceDataProvider wcfSerialization;

		// Token: 0x040020FE RID: 8446
		[ThreadStatic]
		private static PerformanceDataProvider xamlParsed;

		// Token: 0x040020FF RID: 8447
		[ThreadStatic]
		private static PerformanceDataProvider ddiServiceExecution;

		// Token: 0x04002100 RID: 8448
		[ThreadStatic]
		private static PerformanceDataProvider ddiTypeConversion;
	}
}
