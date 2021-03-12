using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.RpcClientAccess
{
	// Token: 0x020003A5 RID: 933
	public static class ExTraceGlobals
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x000581C4 File Offset: 0x000563C4
		public static Trace RpcRawBufferTracer
		{
			get
			{
				if (ExTraceGlobals.rpcRawBufferTracer == null)
				{
					ExTraceGlobals.rpcRawBufferTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.rpcRawBufferTracer;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x000581E2 File Offset: 0x000563E2
		public static Trace FailedRopTracer
		{
			get
			{
				if (ExTraceGlobals.failedRopTracer == null)
				{
					ExTraceGlobals.failedRopTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.failedRopTracer;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00058200 File Offset: 0x00056400
		public static Trace RopLevelExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.ropLevelExceptionTracer == null)
				{
					ExTraceGlobals.ropLevelExceptionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.ropLevelExceptionTracer;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0005821E File Offset: 0x0005641E
		public static Trace NotImplementedTracer
		{
			get
			{
				if (ExTraceGlobals.notImplementedTracer == null)
				{
					ExTraceGlobals.notImplementedTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.notImplementedTracer;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0005823C File Offset: 0x0005643C
		public static Trace NotificationHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.notificationHandlerTracer == null)
				{
					ExTraceGlobals.notificationHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.notificationHandlerTracer;
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x0005825A File Offset: 0x0005645A
		public static Trace NotificationDeliveryTracer
		{
			get
			{
				if (ExTraceGlobals.notificationDeliveryTracer == null)
				{
					ExTraceGlobals.notificationDeliveryTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.notificationDeliveryTracer;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x0600168D RID: 5773 RVA: 0x00058278 File Offset: 0x00056478
		public static Trace AttachmentTracer
		{
			get
			{
				if (ExTraceGlobals.attachmentTracer == null)
				{
					ExTraceGlobals.attachmentTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.attachmentTracer;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00058296 File Offset: 0x00056496
		public static Trace MessageTracer
		{
			get
			{
				if (ExTraceGlobals.messageTracer == null)
				{
					ExTraceGlobals.messageTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.messageTracer;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x000582B4 File Offset: 0x000564B4
		public static Trace FailedRpcTracer
		{
			get
			{
				if (ExTraceGlobals.failedRpcTracer == null)
				{
					ExTraceGlobals.failedRpcTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.failedRpcTracer;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x000582D2 File Offset: 0x000564D2
		public static Trace ClientThrottledTracer
		{
			get
			{
				if (ExTraceGlobals.clientThrottledTracer == null)
				{
					ExTraceGlobals.clientThrottledTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.clientThrottledTracer;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x000582F1 File Offset: 0x000564F1
		public static Trace ConnectRpcTracer
		{
			get
			{
				if (ExTraceGlobals.connectRpcTracer == null)
				{
					ExTraceGlobals.connectRpcTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.connectRpcTracer;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00058310 File Offset: 0x00056510
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

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0005832F File Offset: 0x0005652F
		public static Trace UnhandledExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.unhandledExceptionTracer == null)
				{
					ExTraceGlobals.unhandledExceptionTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.unhandledExceptionTracer;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0005834E File Offset: 0x0005654E
		public static Trace AsyncRpcTracer
		{
			get
			{
				if (ExTraceGlobals.asyncRpcTracer == null)
				{
					ExTraceGlobals.asyncRpcTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.asyncRpcTracer;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0005836D File Offset: 0x0005656D
		public static Trace AccessControlTracer
		{
			get
			{
				if (ExTraceGlobals.accessControlTracer == null)
				{
					ExTraceGlobals.accessControlTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.accessControlTracer;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x0005838C File Offset: 0x0005658C
		public static Trace AsyncRopHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.asyncRopHandlerTracer == null)
				{
					ExTraceGlobals.asyncRopHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.asyncRopHandlerTracer;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000583AB File Offset: 0x000565AB
		public static Trace ConnectXropTracer
		{
			get
			{
				if (ExTraceGlobals.connectXropTracer == null)
				{
					ExTraceGlobals.connectXropTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.connectXropTracer;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x000583CA File Offset: 0x000565CA
		public static Trace FailedXropTracer
		{
			get
			{
				if (ExTraceGlobals.failedXropTracer == null)
				{
					ExTraceGlobals.failedXropTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.failedXropTracer;
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x000583E9 File Offset: 0x000565E9
		public static Trace AvailabilityTracer
		{
			get
			{
				if (ExTraceGlobals.availabilityTracer == null)
				{
					ExTraceGlobals.availabilityTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.availabilityTracer;
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00058408 File Offset: 0x00056608
		public static Trace LogonTracer
		{
			get
			{
				if (ExTraceGlobals.logonTracer == null)
				{
					ExTraceGlobals.logonTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.logonTracer;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00058427 File Offset: 0x00056627
		public static Trace FolderTracer
		{
			get
			{
				if (ExTraceGlobals.folderTracer == null)
				{
					ExTraceGlobals.folderTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.folderTracer;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00058446 File Offset: 0x00056646
		public static Trace ExchangeAsyncDispatchTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeAsyncDispatchTracer == null)
				{
					ExTraceGlobals.exchangeAsyncDispatchTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.exchangeAsyncDispatchTracer;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00058465 File Offset: 0x00056665
		public static Trace ExchangeDispatchTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeDispatchTracer == null)
				{
					ExTraceGlobals.exchangeDispatchTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.exchangeDispatchTracer;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00058484 File Offset: 0x00056684
		public static Trace DispatchTaskTracer
		{
			get
			{
				if (ExTraceGlobals.dispatchTaskTracer == null)
				{
					ExTraceGlobals.dispatchTaskTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.dispatchTaskTracer;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x000584A3 File Offset: 0x000566A3
		public static Trace RpcHttpConnectionRegistrationAsyncDispatchTracer
		{
			get
			{
				if (ExTraceGlobals.rpcHttpConnectionRegistrationAsyncDispatchTracer == null)
				{
					ExTraceGlobals.rpcHttpConnectionRegistrationAsyncDispatchTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.rpcHttpConnectionRegistrationAsyncDispatchTracer;
			}
		}

		// Token: 0x04001B35 RID: 6965
		private static Guid componentGuid = new Guid("E5EC0B19-2F45-4b2f-8B2B-4B0473209669");

		// Token: 0x04001B36 RID: 6966
		private static Trace rpcRawBufferTracer = null;

		// Token: 0x04001B37 RID: 6967
		private static Trace failedRopTracer = null;

		// Token: 0x04001B38 RID: 6968
		private static Trace ropLevelExceptionTracer = null;

		// Token: 0x04001B39 RID: 6969
		private static Trace notImplementedTracer = null;

		// Token: 0x04001B3A RID: 6970
		private static Trace notificationHandlerTracer = null;

		// Token: 0x04001B3B RID: 6971
		private static Trace notificationDeliveryTracer = null;

		// Token: 0x04001B3C RID: 6972
		private static Trace attachmentTracer = null;

		// Token: 0x04001B3D RID: 6973
		private static Trace messageTracer = null;

		// Token: 0x04001B3E RID: 6974
		private static Trace failedRpcTracer = null;

		// Token: 0x04001B3F RID: 6975
		private static Trace clientThrottledTracer = null;

		// Token: 0x04001B40 RID: 6976
		private static Trace connectRpcTracer = null;

		// Token: 0x04001B41 RID: 6977
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001B42 RID: 6978
		private static Trace unhandledExceptionTracer = null;

		// Token: 0x04001B43 RID: 6979
		private static Trace asyncRpcTracer = null;

		// Token: 0x04001B44 RID: 6980
		private static Trace accessControlTracer = null;

		// Token: 0x04001B45 RID: 6981
		private static Trace asyncRopHandlerTracer = null;

		// Token: 0x04001B46 RID: 6982
		private static Trace connectXropTracer = null;

		// Token: 0x04001B47 RID: 6983
		private static Trace failedXropTracer = null;

		// Token: 0x04001B48 RID: 6984
		private static Trace availabilityTracer = null;

		// Token: 0x04001B49 RID: 6985
		private static Trace logonTracer = null;

		// Token: 0x04001B4A RID: 6986
		private static Trace folderTracer = null;

		// Token: 0x04001B4B RID: 6987
		private static Trace exchangeAsyncDispatchTracer = null;

		// Token: 0x04001B4C RID: 6988
		private static Trace exchangeDispatchTracer = null;

		// Token: 0x04001B4D RID: 6989
		private static Trace dispatchTaskTracer = null;

		// Token: 0x04001B4E RID: 6990
		private static Trace rpcHttpConnectionRegistrationAsyncDispatchTracer = null;
	}
}
