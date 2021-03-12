using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000349 RID: 841
	public static class ExTraceGlobals
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x00051AE0 File Offset: 0x0004FCE0
		public static Trace ELCAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.eLCAssistantTracer == null)
				{
					ExTraceGlobals.eLCAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.eLCAssistantTracer;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00051AFE File Offset: 0x0004FCFE
		public static Trace FolderProvisionerTracer
		{
			get
			{
				if (ExTraceGlobals.folderProvisionerTracer == null)
				{
					ExTraceGlobals.folderProvisionerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.folderProvisionerTracer;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x00051B1C File Offset: 0x0004FD1C
		public static Trace CommonEnforcerOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.commonEnforcerOperationsTracer == null)
				{
					ExTraceGlobals.commonEnforcerOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.commonEnforcerOperationsTracer;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00051B3A File Offset: 0x0004FD3A
		public static Trace ExpirationEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.expirationEnforcerTracer == null)
				{
					ExTraceGlobals.expirationEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.expirationEnforcerTracer;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x00051B58 File Offset: 0x0004FD58
		public static Trace AutoCopyEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.autoCopyEnforcerTracer == null)
				{
					ExTraceGlobals.autoCopyEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.autoCopyEnforcerTracer;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00051B76 File Offset: 0x0004FD76
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x00051B94 File Offset: 0x0004FD94
		public static Trace TagProvisionerTracer
		{
			get
			{
				if (ExTraceGlobals.tagProvisionerTracer == null)
				{
					ExTraceGlobals.tagProvisionerTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.tagProvisionerTracer;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00051BB2 File Offset: 0x0004FDB2
		public static Trace CommonTagEnforcerOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.commonTagEnforcerOperationsTracer == null)
				{
					ExTraceGlobals.commonTagEnforcerOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.commonTagEnforcerOperationsTracer;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00051BD0 File Offset: 0x0004FDD0
		public static Trace ExpirationTagEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.expirationTagEnforcerTracer == null)
				{
					ExTraceGlobals.expirationTagEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.expirationTagEnforcerTracer;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00051BEE File Offset: 0x0004FDEE
		public static Trace AutocopyTagEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.autocopyTagEnforcerTracer == null)
				{
					ExTraceGlobals.autocopyTagEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.autocopyTagEnforcerTracer;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x00051C0D File Offset: 0x0004FE0D
		public static Trace EventBasedAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.eventBasedAssistantTracer == null)
				{
					ExTraceGlobals.eventBasedAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.eventBasedAssistantTracer;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00051C2C File Offset: 0x0004FE2C
		public static Trace DeliveryAgentTracer
		{
			get
			{
				if (ExTraceGlobals.deliveryAgentTracer == null)
				{
					ExTraceGlobals.deliveryAgentTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.deliveryAgentTracer;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00051C4B File Offset: 0x0004FE4B
		public static Trace TagExpirationExecutorTracer
		{
			get
			{
				if (ExTraceGlobals.tagExpirationExecutorTracer == null)
				{
					ExTraceGlobals.tagExpirationExecutorTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.tagExpirationExecutorTracer;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00051C6A File Offset: 0x0004FE6A
		public static Trace CommonCleanupEnforcerOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.commonCleanupEnforcerOperationsTracer == null)
				{
					ExTraceGlobals.commonCleanupEnforcerOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.commonCleanupEnforcerOperationsTracer;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x00051C89 File Offset: 0x0004FE89
		public static Trace DumpsterExpirationEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.dumpsterExpirationEnforcerTracer == null)
				{
					ExTraceGlobals.dumpsterExpirationEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.dumpsterExpirationEnforcerTracer;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		public static Trace AuditExpirationEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.auditExpirationEnforcerTracer == null)
				{
					ExTraceGlobals.auditExpirationEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.auditExpirationEnforcerTracer;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x00051CC7 File Offset: 0x0004FEC7
		public static Trace CalendarLogExpirationEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.calendarLogExpirationEnforcerTracer == null)
				{
					ExTraceGlobals.calendarLogExpirationEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.calendarLogExpirationEnforcerTracer;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00051CE6 File Offset: 0x0004FEE6
		public static Trace DumpsterQuotaEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.dumpsterQuotaEnforcerTracer == null)
				{
					ExTraceGlobals.dumpsterQuotaEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.dumpsterQuotaEnforcerTracer;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x00051D05 File Offset: 0x0004FF05
		public static Trace SupplementExpirationEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.supplementExpirationEnforcerTracer == null)
				{
					ExTraceGlobals.supplementExpirationEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.supplementExpirationEnforcerTracer;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00051D24 File Offset: 0x0004FF24
		public static Trace DiscoveryHoldEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.discoveryHoldEnforcerTracer == null)
				{
					ExTraceGlobals.discoveryHoldEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.discoveryHoldEnforcerTracer;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x00051D43 File Offset: 0x0004FF43
		public static Trace ElcReportingTracer
		{
			get
			{
				if (ExTraceGlobals.elcReportingTracer == null)
				{
					ExTraceGlobals.elcReportingTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.elcReportingTracer;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00051D62 File Offset: 0x0004FF62
		public static Trace HoldCleanupEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.holdCleanupEnforcerTracer == null)
				{
					ExTraceGlobals.holdCleanupEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.holdCleanupEnforcerTracer;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00051D81 File Offset: 0x0004FF81
		public static Trace EHAHiddenFolderCleanupEnforcerTracer
		{
			get
			{
				if (ExTraceGlobals.eHAHiddenFolderCleanupEnforcerTracer == null)
				{
					ExTraceGlobals.eHAHiddenFolderCleanupEnforcerTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.eHAHiddenFolderCleanupEnforcerTracer;
			}
		}

		// Token: 0x0400183B RID: 6203
		private static Guid componentGuid = new Guid("75989588-FD78-490c-B0DC-EC9E6F6E148B");

		// Token: 0x0400183C RID: 6204
		private static Trace eLCAssistantTracer = null;

		// Token: 0x0400183D RID: 6205
		private static Trace folderProvisionerTracer = null;

		// Token: 0x0400183E RID: 6206
		private static Trace commonEnforcerOperationsTracer = null;

		// Token: 0x0400183F RID: 6207
		private static Trace expirationEnforcerTracer = null;

		// Token: 0x04001840 RID: 6208
		private static Trace autoCopyEnforcerTracer = null;

		// Token: 0x04001841 RID: 6209
		private static Trace pFDTracer = null;

		// Token: 0x04001842 RID: 6210
		private static Trace tagProvisionerTracer = null;

		// Token: 0x04001843 RID: 6211
		private static Trace commonTagEnforcerOperationsTracer = null;

		// Token: 0x04001844 RID: 6212
		private static Trace expirationTagEnforcerTracer = null;

		// Token: 0x04001845 RID: 6213
		private static Trace autocopyTagEnforcerTracer = null;

		// Token: 0x04001846 RID: 6214
		private static Trace eventBasedAssistantTracer = null;

		// Token: 0x04001847 RID: 6215
		private static Trace deliveryAgentTracer = null;

		// Token: 0x04001848 RID: 6216
		private static Trace tagExpirationExecutorTracer = null;

		// Token: 0x04001849 RID: 6217
		private static Trace commonCleanupEnforcerOperationsTracer = null;

		// Token: 0x0400184A RID: 6218
		private static Trace dumpsterExpirationEnforcerTracer = null;

		// Token: 0x0400184B RID: 6219
		private static Trace auditExpirationEnforcerTracer = null;

		// Token: 0x0400184C RID: 6220
		private static Trace calendarLogExpirationEnforcerTracer = null;

		// Token: 0x0400184D RID: 6221
		private static Trace dumpsterQuotaEnforcerTracer = null;

		// Token: 0x0400184E RID: 6222
		private static Trace supplementExpirationEnforcerTracer = null;

		// Token: 0x0400184F RID: 6223
		private static Trace discoveryHoldEnforcerTracer = null;

		// Token: 0x04001850 RID: 6224
		private static Trace elcReportingTracer = null;

		// Token: 0x04001851 RID: 6225
		private static Trace holdCleanupEnforcerTracer = null;

		// Token: 0x04001852 RID: 6226
		private static Trace eHAHiddenFolderCleanupEnforcerTracer = null;
	}
}
