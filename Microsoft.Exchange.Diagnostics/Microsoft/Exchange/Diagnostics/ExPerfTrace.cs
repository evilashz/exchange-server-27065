using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200012E RID: 302
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExPerfTrace
	{
		// Token: 0x060008BB RID: 2235 RVA: 0x000222F2 File Offset: 0x000204F2
		public static ExPerfTrace.ActivityFrame NewActivity()
		{
			return new ExPerfTrace.ActivityFrame(Guid.NewGuid());
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x000222FE File Offset: 0x000204FE
		public static ExPerfTrace.ActivityFrame RelatedActivity(Guid relatedActivityId)
		{
			return new ExPerfTrace.ActivityFrame(relatedActivityId);
		}

		// Token: 0x060008BD RID: 2237
		[DllImport("ADVAPI32.DLL", SetLastError = true)]
		private static extern uint EventActivityIdControl([MarshalAs(UnmanagedType.U4)] ExPerfTrace.EVENT_ACTIVITY_CTRL controlCode, ref Guid activityId);

		// Token: 0x040005CC RID: 1484
		private const string AdvApi32 = "ADVAPI32.DLL";

		// Token: 0x0200012F RID: 303
		public struct ActivityFrame : IDisposable
		{
			// Token: 0x060008BE RID: 2238 RVA: 0x00022308 File Offset: 0x00020508
			public ActivityFrame(Guid newActivityId)
			{
				this.originalActivityId = Guid.Empty;
				Guid empty = Guid.Empty;
				if (ExPerfTrace.EventActivityIdControl(ExPerfTrace.EVENT_ACTIVITY_CTRL.GET_ID, ref empty) == 0U)
				{
					this.originalActivityId = empty;
				}
				ExPerfTrace.EventActivityIdControl(ExPerfTrace.EVENT_ACTIVITY_CTRL.SET_ID, ref newActivityId);
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x00022342 File Offset: 0x00020542
			public void Dispose()
			{
				ExPerfTrace.EventActivityIdControl(ExPerfTrace.EVENT_ACTIVITY_CTRL.SET_ID, ref this.originalActivityId);
			}

			// Token: 0x040005CD RID: 1485
			private Guid originalActivityId;
		}

		// Token: 0x02000130 RID: 304
		private enum EVENT_ACTIVITY_CTRL : uint
		{
			// Token: 0x040005CF RID: 1487
			GET_ID = 1U,
			// Token: 0x040005D0 RID: 1488
			SET_ID,
			// Token: 0x040005D1 RID: 1489
			CREATE_ID,
			// Token: 0x040005D2 RID: 1490
			GET_SET_ID,
			// Token: 0x040005D3 RID: 1491
			CREATE_SET_ID
		}
	}
}
