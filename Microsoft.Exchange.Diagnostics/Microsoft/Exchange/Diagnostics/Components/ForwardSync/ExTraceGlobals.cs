using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ForwardSync
{
	// Token: 0x020003B8 RID: 952
	public static class ExTraceGlobals
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00058DF8 File Offset: 0x00056FF8
		public static Trace ForwardSyncServiceTracer
		{
			get
			{
				if (ExTraceGlobals.forwardSyncServiceTracer == null)
				{
					ExTraceGlobals.forwardSyncServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.forwardSyncServiceTracer;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060016E8 RID: 5864 RVA: 0x00058E16 File Offset: 0x00057016
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00058E34 File Offset: 0x00057034
		public static Trace MainStreamTracer
		{
			get
			{
				if (ExTraceGlobals.mainStreamTracer == null)
				{
					ExTraceGlobals.mainStreamTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.mainStreamTracer;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00058E52 File Offset: 0x00057052
		public static Trace FullSyncStreamTracer
		{
			get
			{
				if (ExTraceGlobals.fullSyncStreamTracer == null)
				{
					ExTraceGlobals.fullSyncStreamTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.fullSyncStreamTracer;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00058E70 File Offset: 0x00057070
		public static Trace MsoSyncServiceTracer
		{
			get
			{
				if (ExTraceGlobals.msoSyncServiceTracer == null)
				{
					ExTraceGlobals.msoSyncServiceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.msoSyncServiceTracer;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00058E8E File Offset: 0x0005708E
		public static Trace PowerShellTracer
		{
			get
			{
				if (ExTraceGlobals.powerShellTracer == null)
				{
					ExTraceGlobals.powerShellTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.powerShellTracer;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00058EAC File Offset: 0x000570AC
		public static Trace JobProcessorTracer
		{
			get
			{
				if (ExTraceGlobals.jobProcessorTracer == null)
				{
					ExTraceGlobals.jobProcessorTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.jobProcessorTracer;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00058ECA File Offset: 0x000570CA
		public static Trace RecipientWorkflowTracer
		{
			get
			{
				if (ExTraceGlobals.recipientWorkflowTracer == null)
				{
					ExTraceGlobals.recipientWorkflowTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.recipientWorkflowTracer;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00058EE8 File Offset: 0x000570E8
		public static Trace OrganizationWorkflowTracer
		{
			get
			{
				if (ExTraceGlobals.organizationWorkflowTracer == null)
				{
					ExTraceGlobals.organizationWorkflowTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.organizationWorkflowTracer;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00058F06 File Offset: 0x00057106
		public static Trace ProvisioningLicenseTracer
		{
			get
			{
				if (ExTraceGlobals.provisioningLicenseTracer == null)
				{
					ExTraceGlobals.provisioningLicenseTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.provisioningLicenseTracer;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00058F25 File Offset: 0x00057125
		public static Trace UnifiedGroupTracer
		{
			get
			{
				if (ExTraceGlobals.unifiedGroupTracer == null)
				{
					ExTraceGlobals.unifiedGroupTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.unifiedGroupTracer;
			}
		}

		// Token: 0x04001B95 RID: 7061
		private static Guid componentGuid = new Guid("8FAC856B-D0D4-4f7d-BBE9-B713EDFCBAAD");

		// Token: 0x04001B96 RID: 7062
		private static Trace forwardSyncServiceTracer = null;

		// Token: 0x04001B97 RID: 7063
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B98 RID: 7064
		private static Trace mainStreamTracer = null;

		// Token: 0x04001B99 RID: 7065
		private static Trace fullSyncStreamTracer = null;

		// Token: 0x04001B9A RID: 7066
		private static Trace msoSyncServiceTracer = null;

		// Token: 0x04001B9B RID: 7067
		private static Trace powerShellTracer = null;

		// Token: 0x04001B9C RID: 7068
		private static Trace jobProcessorTracer = null;

		// Token: 0x04001B9D RID: 7069
		private static Trace recipientWorkflowTracer = null;

		// Token: 0x04001B9E RID: 7070
		private static Trace organizationWorkflowTracer = null;

		// Token: 0x04001B9F RID: 7071
		private static Trace provisioningLicenseTracer = null;

		// Token: 0x04001BA0 RID: 7072
		private static Trace unifiedGroupTracer = null;
	}
}
