using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DisposeTrackerNullObject : DisposeTracker
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000034C3 File Offset: 0x000016C3
		private DisposeTrackerNullObject()
		{
			DisposeTrackerOptions.RefreshNowIfNecessary();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000034D0 File Offset: 0x000016D0
		public static DisposeTrackerNullObject Instance
		{
			get
			{
				if (DisposeTrackerNullObject.instance == null)
				{
					DisposeTrackerNullObject.instance = new DisposeTrackerNullObject();
				}
				return DisposeTrackerNullObject.instance;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000034E8 File Offset: 0x000016E8
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x0400008E RID: 142
		private static DisposeTrackerNullObject instance;
	}
}
