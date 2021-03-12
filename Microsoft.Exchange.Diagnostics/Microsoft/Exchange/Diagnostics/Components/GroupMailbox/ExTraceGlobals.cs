using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.GroupMailbox
{
	// Token: 0x020003FD RID: 1021
	public static class ExTraceGlobals
	{
		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x0005C1AD File Offset: 0x0005A3AD
		public static Trace CmdletsTracer
		{
			get
			{
				if (ExTraceGlobals.cmdletsTracer == null)
				{
					ExTraceGlobals.cmdletsTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.cmdletsTracer;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x0005C1CB File Offset: 0x0005A3CB
		public static Trace WebServicesTracer
		{
			get
			{
				if (ExTraceGlobals.webServicesTracer == null)
				{
					ExTraceGlobals.webServicesTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.webServicesTracer;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x0005C1E9 File Offset: 0x0005A3E9
		public static Trace GroupMailboxAccessLayerTracer
		{
			get
			{
				if (ExTraceGlobals.groupMailboxAccessLayerTracer == null)
				{
					ExTraceGlobals.groupMailboxAccessLayerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.groupMailboxAccessLayerTracer;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x0005C207 File Offset: 0x0005A407
		public static Trace LocalAssociationStoreTracer
		{
			get
			{
				if (ExTraceGlobals.localAssociationStoreTracer == null)
				{
					ExTraceGlobals.localAssociationStoreTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.localAssociationStoreTracer;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x0005C225 File Offset: 0x0005A425
		public static Trace MailboxLocatorTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxLocatorTracer == null)
				{
					ExTraceGlobals.mailboxLocatorTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.mailboxLocatorTracer;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x0005C243 File Offset: 0x0005A443
		public static Trace GroupAssociationAdaptorTracer
		{
			get
			{
				if (ExTraceGlobals.groupAssociationAdaptorTracer == null)
				{
					ExTraceGlobals.groupAssociationAdaptorTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.groupAssociationAdaptorTracer;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x0005C261 File Offset: 0x0005A461
		public static Trace UserAssociationAdaptorTracer
		{
			get
			{
				if (ExTraceGlobals.userAssociationAdaptorTracer == null)
				{
					ExTraceGlobals.userAssociationAdaptorTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.userAssociationAdaptorTracer;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x0005C27F File Offset: 0x0005A47F
		public static Trace UpdateAssociationCommandTracer
		{
			get
			{
				if (ExTraceGlobals.updateAssociationCommandTracer == null)
				{
					ExTraceGlobals.updateAssociationCommandTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.updateAssociationCommandTracer;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0005C29D File Offset: 0x0005A49D
		public static Trace AssociationReplicationTracer
		{
			get
			{
				if (ExTraceGlobals.associationReplicationTracer == null)
				{
					ExTraceGlobals.associationReplicationTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.associationReplicationTracer;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600187E RID: 6270 RVA: 0x0005C2BB File Offset: 0x0005A4BB
		public static Trace AssociationReplicationAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.associationReplicationAssistantTracer == null)
				{
					ExTraceGlobals.associationReplicationAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.associationReplicationAssistantTracer;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x0005C2DA File Offset: 0x0005A4DA
		public static Trace GroupEmailNotificationHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.groupEmailNotificationHandlerTracer == null)
				{
					ExTraceGlobals.groupEmailNotificationHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.groupEmailNotificationHandlerTracer;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0005C2F9 File Offset: 0x0005A4F9
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x0005C318 File Offset: 0x0005A518
		public static Trace GroupMailboxAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.groupMailboxAssistantTracer == null)
				{
					ExTraceGlobals.groupMailboxAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.groupMailboxAssistantTracer;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005C337 File Offset: 0x0005A537
		public static Trace UnseenDataUserAssociationAdaptorTracer
		{
			get
			{
				if (ExTraceGlobals.unseenDataUserAssociationAdaptorTracer == null)
				{
					ExTraceGlobals.unseenDataUserAssociationAdaptorTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.unseenDataUserAssociationAdaptorTracer;
			}
		}

		// Token: 0x04001D23 RID: 7459
		private static Guid componentGuid = new Guid("902B6BA0-4553-4533-8594-4AD6DA001FB7");

		// Token: 0x04001D24 RID: 7460
		private static Trace cmdletsTracer = null;

		// Token: 0x04001D25 RID: 7461
		private static Trace webServicesTracer = null;

		// Token: 0x04001D26 RID: 7462
		private static Trace groupMailboxAccessLayerTracer = null;

		// Token: 0x04001D27 RID: 7463
		private static Trace localAssociationStoreTracer = null;

		// Token: 0x04001D28 RID: 7464
		private static Trace mailboxLocatorTracer = null;

		// Token: 0x04001D29 RID: 7465
		private static Trace groupAssociationAdaptorTracer = null;

		// Token: 0x04001D2A RID: 7466
		private static Trace userAssociationAdaptorTracer = null;

		// Token: 0x04001D2B RID: 7467
		private static Trace updateAssociationCommandTracer = null;

		// Token: 0x04001D2C RID: 7468
		private static Trace associationReplicationTracer = null;

		// Token: 0x04001D2D RID: 7469
		private static Trace associationReplicationAssistantTracer = null;

		// Token: 0x04001D2E RID: 7470
		private static Trace groupEmailNotificationHandlerTracer = null;

		// Token: 0x04001D2F RID: 7471
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001D30 RID: 7472
		private static Trace groupMailboxAssistantTracer = null;

		// Token: 0x04001D31 RID: 7473
		private static Trace unseenDataUserAssociationAdaptorTracer = null;
	}
}
