using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200000A RID: 10
	internal static class TraceContext
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000025D8 File Offset: 0x000007D8
		public static object Get()
		{
			object obj = Thread.GetData(TraceContext.slotId);
			if (obj == null)
			{
				obj = "<no context set>";
			}
			return obj;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025FA File Offset: 0x000007FA
		public static void Set(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			TraceContext.SetInternal("Mailbox:" + mailboxSession.MailboxOwner.ToString());
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002624 File Offset: 0x00000824
		public static void Set(object context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			TraceContext.SetInternal(context);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000263A File Offset: 0x0000083A
		public static void Reset()
		{
			TraceContext.SetInternal(null);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002644 File Offset: 0x00000844
		private static void SetInternal(object context)
		{
			object data = Thread.GetData(TraceContext.slotId);
			TraceContext.Tracer.TraceDebug((long)TraceContext.slotId.GetHashCode(), "New context: {0}. Previous context: {1}.", new object[]
			{
				(context == null) ? "<null>" : context,
				(data == null) ? "<null>" : context
			});
			Thread.SetData(TraceContext.slotId, context);
		}

		// Token: 0x04000029 RID: 41
		private static LocalDataStoreSlot slotId = Thread.AllocateDataSlot();

		// Token: 0x0400002A RID: 42
		private static readonly Trace Tracer = ExTraceGlobals.TraceContextTracer;
	}
}
