using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002AF RID: 687
	internal class SuppressingPiiContext : DisposeTrackableBase
	{
		// Token: 0x060018BF RID: 6335 RVA: 0x0004E4B1 File Offset: 0x0004C6B1
		private SuppressingPiiContext(bool needPiiSuppression, PiiMap piiMap)
		{
			this.needPiiSuppression = needPiiSuppression;
			this.piiMap = piiMap;
			this.previousContext = SuppressingPiiContext.currentContext;
			SuppressingPiiContext.currentContext = this;
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0004E4D8 File Offset: 0x0004C6D8
		public static bool NeedPiiSuppression
		{
			get
			{
				SuppressingPiiContext suppressingPiiContext = SuppressingPiiContext.currentContext;
				return suppressingPiiContext != null && suppressingPiiContext.needPiiSuppression;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0004E4F8 File Offset: 0x0004C6F8
		public static PiiMap PiiMap
		{
			get
			{
				SuppressingPiiContext suppressingPiiContext = SuppressingPiiContext.currentContext;
				if (suppressingPiiContext == null)
				{
					return null;
				}
				return suppressingPiiContext.piiMap;
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0004E516 File Offset: 0x0004C716
		internal static IDisposable Create(bool needPiiSuppression, PiiMap piiMap)
		{
			return new SuppressingPiiContext(needPiiSuppression, piiMap);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0004E51F File Offset: 0x0004C71F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				SuppressingPiiContext.currentContext = this.previousContext;
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0004E52F File Offset: 0x0004C72F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SuppressingPiiContext>(this);
		}

		// Token: 0x04000EA3 RID: 3747
		[ThreadStatic]
		private static SuppressingPiiContext currentContext;

		// Token: 0x04000EA4 RID: 3748
		private readonly SuppressingPiiContext previousContext;

		// Token: 0x04000EA5 RID: 3749
		private readonly bool needPiiSuppression;

		// Token: 0x04000EA6 RID: 3750
		private readonly PiiMap piiMap;
	}
}
