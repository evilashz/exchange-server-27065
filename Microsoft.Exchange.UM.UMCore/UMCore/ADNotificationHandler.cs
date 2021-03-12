using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000014 RID: 20
	internal abstract class ADNotificationHandler : DisposableBase
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000120 RID: 288 RVA: 0x00005A84 File Offset: 0x00003C84
		// (remove) Token: 0x06000121 RID: 289 RVA: 0x00005ABC File Offset: 0x00003CBC
		internal event ADNotificationEvent ConfigChanged;

		// Token: 0x06000122 RID: 290 RVA: 0x00005AF1 File Offset: 0x00003CF1
		protected static void DebugTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, formatString, formatObjects);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005B05 File Offset: 0x00003D05
		protected static void ErrorTrace(string formatString, params object[] formatObjects)
		{
			CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, formatString, formatObjects);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005B19 File Offset: 0x00003D19
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005B1B File Offset: 0x00003D1B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ADNotificationHandler>(this);
		}

		// Token: 0x06000126 RID: 294
		protected abstract void LogRegistrationError(TimeSpan retryInterval, LocalizedException exception);

		// Token: 0x06000127 RID: 295 RVA: 0x00005B23 File Offset: 0x00003D23
		protected void FireConfigChangedEvent(ADNotificationEventArgs args)
		{
			if (this.ConfigChanged != null)
			{
				this.ConfigChanged(args);
			}
		}
	}
}
