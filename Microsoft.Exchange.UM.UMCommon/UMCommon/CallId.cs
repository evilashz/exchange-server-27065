using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200001B RID: 27
	internal class CallId : DisposableBase
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00007C38 File Offset: 0x00005E38
		internal CallId(string currentCallId)
		{
			if (CallId.id == null)
			{
				CallId.id = currentCallId;
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "Call-id has been set to {0}.", new object[]
				{
					CallId.id ?? "<null>"
				});
				return;
			}
			if (!string.Equals(CallId.id, currentCallId, StringComparison.InvariantCulture))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "Call-id already set to a different value, Existing call-id {0}, Required call-id {1}", new object[]
				{
					CallId.id ?? "<null>",
					currentCallId ?? "<null>"
				});
				throw new InvalidOperationException(Strings.CallIdNotNull(CallId.Id, currentCallId));
			}
			this.disposeCallId = false;
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "Call-id already set to the required value, Existing call-id {0}, Required call-id {1}", new object[]
			{
				CallId.id ?? "<null>",
				currentCallId ?? "<null>"
			});
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00007D23 File Offset: 0x00005F23
		internal static string Id
		{
			get
			{
				return CallId.id;
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00007D2A File Offset: 0x00005F2A
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.disposeCallId)
			{
				CallId.id = null;
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, null, "Call-id has been disposed.", new object[0]);
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00007D53 File Offset: 0x00005F53
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CallId>(this);
		}

		// Token: 0x04000093 RID: 147
		[ThreadStatic]
		private static string id;

		// Token: 0x04000094 RID: 148
		private bool disposeCallId = true;
	}
}
