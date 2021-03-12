using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.Mapi
{
	// Token: 0x02000394 RID: 916
	public static class ExTraceGlobals
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000565DA File Offset: 0x000547DA
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x000565F8 File Offset: 0x000547F8
		public static Trace SchemaMapEntryAddedTracer
		{
			get
			{
				if (ExTraceGlobals.schemaMapEntryAddedTracer == null)
				{
					ExTraceGlobals.schemaMapEntryAddedTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.schemaMapEntryAddedTracer;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00056616 File Offset: 0x00054816
		public static Trace SchemaMapEntryUpdatedTracer
		{
			get
			{
				if (ExTraceGlobals.schemaMapEntryUpdatedTracer == null)
				{
					ExTraceGlobals.schemaMapEntryUpdatedTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.schemaMapEntryUpdatedTracer;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x00056634 File Offset: 0x00054834
		public static Trace PropertyMappingTracer
		{
			get
			{
				if (ExTraceGlobals.propertyMappingTracer == null)
				{
					ExTraceGlobals.propertyMappingTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.propertyMappingTracer;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x00056652 File Offset: 0x00054852
		public static Trace GetPropsPropertiesTracer
		{
			get
			{
				if (ExTraceGlobals.getPropsPropertiesTracer == null)
				{
					ExTraceGlobals.getPropsPropertiesTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.getPropsPropertiesTracer;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00056670 File Offset: 0x00054870
		public static Trace SetPropsPropertiesTracer
		{
			get
			{
				if (ExTraceGlobals.setPropsPropertiesTracer == null)
				{
					ExTraceGlobals.setPropsPropertiesTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.setPropsPropertiesTracer;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0005668E File Offset: 0x0005488E
		public static Trace DeletePropsPropertiesTracer
		{
			get
			{
				if (ExTraceGlobals.deletePropsPropertiesTracer == null)
				{
					ExTraceGlobals.deletePropsPropertiesTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.deletePropsPropertiesTracer;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000566AC File Offset: 0x000548AC
		public static Trace CopyOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.copyOperationsTracer == null)
				{
					ExTraceGlobals.copyOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.copyOperationsTracer;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000566CA File Offset: 0x000548CA
		public static Trace StreamOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.streamOperationsTracer == null)
				{
					ExTraceGlobals.streamOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.streamOperationsTracer;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000566E8 File Offset: 0x000548E8
		public static Trace AttachmentOperationsTracer
		{
			get
			{
				if (ExTraceGlobals.attachmentOperationsTracer == null)
				{
					ExTraceGlobals.attachmentOperationsTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.attachmentOperationsTracer;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00056707 File Offset: 0x00054907
		public static Trace NotificationTracer
		{
			get
			{
				if (ExTraceGlobals.notificationTracer == null)
				{
					ExTraceGlobals.notificationTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.notificationTracer;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00056726 File Offset: 0x00054926
		public static Trace CreateLogonTracer
		{
			get
			{
				if (ExTraceGlobals.createLogonTracer == null)
				{
					ExTraceGlobals.createLogonTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.createLogonTracer;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00056745 File Offset: 0x00054945
		public static Trace CreateSessionTracer
		{
			get
			{
				if (ExTraceGlobals.createSessionTracer == null)
				{
					ExTraceGlobals.createSessionTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.createSessionTracer;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00056764 File Offset: 0x00054964
		public static Trace SubmitMessageTracer
		{
			get
			{
				if (ExTraceGlobals.submitMessageTracer == null)
				{
					ExTraceGlobals.submitMessageTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.submitMessageTracer;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00056783 File Offset: 0x00054983
		public static Trace AccessCheckTracer
		{
			get
			{
				if (ExTraceGlobals.accessCheckTracer == null)
				{
					ExTraceGlobals.accessCheckTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.accessCheckTracer;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x000567A2 File Offset: 0x000549A2
		public static Trace TimedEventsTracer
		{
			get
			{
				if (ExTraceGlobals.timedEventsTracer == null)
				{
					ExTraceGlobals.timedEventsTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.timedEventsTracer;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000567C1 File Offset: 0x000549C1
		public static Trace DeferredSendTracer
		{
			get
			{
				if (ExTraceGlobals.deferredSendTracer == null)
				{
					ExTraceGlobals.deferredSendTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.deferredSendTracer;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000567E0 File Offset: 0x000549E0
		public static Trace MailboxSignatureTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxSignatureTracer == null)
				{
					ExTraceGlobals.mailboxSignatureTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.mailboxSignatureTracer;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000567FF File Offset: 0x000549FF
		public static Trace QuotaTracer
		{
			get
			{
				if (ExTraceGlobals.quotaTracer == null)
				{
					ExTraceGlobals.quotaTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.quotaTracer;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0005681E File Offset: 0x00054A1E
		public static Trace FillRowTracer
		{
			get
			{
				if (ExTraceGlobals.fillRowTracer == null)
				{
					ExTraceGlobals.fillRowTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.fillRowTracer;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0005683D File Offset: 0x00054A3D
		public static Trace SecurityContextManagerTracer
		{
			get
			{
				if (ExTraceGlobals.securityContextManagerTracer == null)
				{
					ExTraceGlobals.securityContextManagerTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.securityContextManagerTracer;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0005685C File Offset: 0x00054A5C
		public static Trace InTransitTransitionsTracer
		{
			get
			{
				if (ExTraceGlobals.inTransitTransitionsTracer == null)
				{
					ExTraceGlobals.inTransitTransitionsTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.inTransitTransitionsTracer;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0005687B File Offset: 0x00054A7B
		public static Trace RestrictTracer
		{
			get
			{
				if (ExTraceGlobals.restrictTracer == null)
				{
					ExTraceGlobals.restrictTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.restrictTracer;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0005689A File Offset: 0x00054A9A
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000568B9 File Offset: 0x00054AB9
		public static Trace EnableBadItemInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.enableBadItemInjectionTracer == null)
				{
					ExTraceGlobals.enableBadItemInjectionTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.enableBadItemInjectionTracer;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000568D8 File Offset: 0x00054AD8
		public static Trace CreateMailboxTracer
		{
			get
			{
				if (ExTraceGlobals.createMailboxTracer == null)
				{
					ExTraceGlobals.createMailboxTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.createMailboxTracer;
			}
		}

		// Token: 0x04001A6E RID: 6766
		private static Guid componentGuid = new Guid("7927e3f9-b2bc-461f-96e7-c78d73ed4f04");

		// Token: 0x04001A6F RID: 6767
		private static Trace generalTracer = null;

		// Token: 0x04001A70 RID: 6768
		private static Trace schemaMapEntryAddedTracer = null;

		// Token: 0x04001A71 RID: 6769
		private static Trace schemaMapEntryUpdatedTracer = null;

		// Token: 0x04001A72 RID: 6770
		private static Trace propertyMappingTracer = null;

		// Token: 0x04001A73 RID: 6771
		private static Trace getPropsPropertiesTracer = null;

		// Token: 0x04001A74 RID: 6772
		private static Trace setPropsPropertiesTracer = null;

		// Token: 0x04001A75 RID: 6773
		private static Trace deletePropsPropertiesTracer = null;

		// Token: 0x04001A76 RID: 6774
		private static Trace copyOperationsTracer = null;

		// Token: 0x04001A77 RID: 6775
		private static Trace streamOperationsTracer = null;

		// Token: 0x04001A78 RID: 6776
		private static Trace attachmentOperationsTracer = null;

		// Token: 0x04001A79 RID: 6777
		private static Trace notificationTracer = null;

		// Token: 0x04001A7A RID: 6778
		private static Trace createLogonTracer = null;

		// Token: 0x04001A7B RID: 6779
		private static Trace createSessionTracer = null;

		// Token: 0x04001A7C RID: 6780
		private static Trace submitMessageTracer = null;

		// Token: 0x04001A7D RID: 6781
		private static Trace accessCheckTracer = null;

		// Token: 0x04001A7E RID: 6782
		private static Trace timedEventsTracer = null;

		// Token: 0x04001A7F RID: 6783
		private static Trace deferredSendTracer = null;

		// Token: 0x04001A80 RID: 6784
		private static Trace mailboxSignatureTracer = null;

		// Token: 0x04001A81 RID: 6785
		private static Trace quotaTracer = null;

		// Token: 0x04001A82 RID: 6786
		private static Trace fillRowTracer = null;

		// Token: 0x04001A83 RID: 6787
		private static Trace securityContextManagerTracer = null;

		// Token: 0x04001A84 RID: 6788
		private static Trace inTransitTransitionsTracer = null;

		// Token: 0x04001A85 RID: 6789
		private static Trace restrictTracer = null;

		// Token: 0x04001A86 RID: 6790
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001A87 RID: 6791
		private static Trace enableBadItemInjectionTracer = null;

		// Token: 0x04001A88 RID: 6792
		private static Trace createMailboxTracer = null;
	}
}
