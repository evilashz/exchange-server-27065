using System;

namespace Microsoft.Exchange.Diagnostics.Components.Entities
{
	// Token: 0x020003FF RID: 1023
	public static class ExTraceGlobals
	{
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x0005C445 File Offset: 0x0005A645
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x0005C463 File Offset: 0x0005A663
		public static Trace ConvertersTracer
		{
			get
			{
				if (ExTraceGlobals.convertersTracer == null)
				{
					ExTraceGlobals.convertersTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.convertersTracer;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x0005C481 File Offset: 0x0005A681
		public static Trace ReliableActionsTracer
		{
			get
			{
				if (ExTraceGlobals.reliableActionsTracer == null)
				{
					ExTraceGlobals.reliableActionsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.reliableActionsTracer;
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x0005C49F File Offset: 0x0005A69F
		public static Trace SerializationTracer
		{
			get
			{
				if (ExTraceGlobals.serializationTracer == null)
				{
					ExTraceGlobals.serializationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.serializationTracer;
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x0005C4BD File Offset: 0x0005A6BD
		public static Trace AttachmentDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.attachmentDataProviderTracer == null)
				{
					ExTraceGlobals.attachmentDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.attachmentDataProviderTracer;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x0005C4DB File Offset: 0x0005A6DB
		public static Trace CreateAttachmentTracer
		{
			get
			{
				if (ExTraceGlobals.createAttachmentTracer == null)
				{
					ExTraceGlobals.createAttachmentTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.createAttachmentTracer;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x0005C4F9 File Offset: 0x0005A6F9
		public static Trace ReadAttachmentTracer
		{
			get
			{
				if (ExTraceGlobals.readAttachmentTracer == null)
				{
					ExTraceGlobals.readAttachmentTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.readAttachmentTracer;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0005C517 File Offset: 0x0005A717
		public static Trace UpdateAttachmentTracer
		{
			get
			{
				if (ExTraceGlobals.updateAttachmentTracer == null)
				{
					ExTraceGlobals.updateAttachmentTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.updateAttachmentTracer;
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x0005C535 File Offset: 0x0005A735
		public static Trace DeleteAttachmentTracer
		{
			get
			{
				if (ExTraceGlobals.deleteAttachmentTracer == null)
				{
					ExTraceGlobals.deleteAttachmentTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.deleteAttachmentTracer;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0005C553 File Offset: 0x0005A753
		public static Trace FindAttachmentsTracer
		{
			get
			{
				if (ExTraceGlobals.findAttachmentsTracer == null)
				{
					ExTraceGlobals.findAttachmentsTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.findAttachmentsTracer;
			}
		}

		// Token: 0x04001D36 RID: 7478
		private static Guid componentGuid = new Guid("B3FC667F-1AD2-4377-AC4D-3AE344A739D4");

		// Token: 0x04001D37 RID: 7479
		private static Trace commonTracer = null;

		// Token: 0x04001D38 RID: 7480
		private static Trace convertersTracer = null;

		// Token: 0x04001D39 RID: 7481
		private static Trace reliableActionsTracer = null;

		// Token: 0x04001D3A RID: 7482
		private static Trace serializationTracer = null;

		// Token: 0x04001D3B RID: 7483
		private static Trace attachmentDataProviderTracer = null;

		// Token: 0x04001D3C RID: 7484
		private static Trace createAttachmentTracer = null;

		// Token: 0x04001D3D RID: 7485
		private static Trace readAttachmentTracer = null;

		// Token: 0x04001D3E RID: 7486
		private static Trace updateAttachmentTracer = null;

		// Token: 0x04001D3F RID: 7487
		private static Trace deleteAttachmentTracer = null;

		// Token: 0x04001D40 RID: 7488
		private static Trace findAttachmentsTracer = null;
	}
}
