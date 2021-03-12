using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200050F RID: 1295
	[FriendAccessAllowed]
	[EventSource(Name = "System.Threading.SynchronizationEventSource", Guid = "EC631D38-466B-4290-9306-834971BA0217", LocalizationResources = "mscorlib")]
	internal sealed class CdsSyncEtwBCLProvider : EventSource
	{
		// Token: 0x06003DBC RID: 15804 RVA: 0x000E53E1 File Offset: 0x000E35E1
		private CdsSyncEtwBCLProvider()
		{
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x000E53E9 File Offset: 0x000E35E9
		[Event(1, Level = EventLevel.Warning)]
		public void SpinLock_FastPathFailed(int ownerID)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, ownerID);
			}
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x000E53FE File Offset: 0x000E35FE
		[Event(2, Level = EventLevel.Informational)]
		public void SpinWait_NextSpinWillYield()
		{
			if (base.IsEnabled(EventLevel.Informational, EventKeywords.All))
			{
				base.WriteEvent(2);
			}
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x000E5414 File Offset: 0x000E3614
		[SecuritySafeCritical]
		[Event(3, Level = EventLevel.Verbose, Version = 1)]
		public unsafe void Barrier_PhaseFinished(bool currentSense, long phaseNum)
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				int num = currentSense ? 1 : 0;
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&num));
				ptr[1].Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&phaseNum));
				base.WriteEventCore(3, 2, ptr);
			}
		}

		// Token: 0x040019B2 RID: 6578
		public static CdsSyncEtwBCLProvider Log = new CdsSyncEtwBCLProvider();

		// Token: 0x040019B3 RID: 6579
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x040019B4 RID: 6580
		private const int SPINLOCK_FASTPATHFAILED_ID = 1;

		// Token: 0x040019B5 RID: 6581
		private const int SPINWAIT_NEXTSPINWILLYIELD_ID = 2;

		// Token: 0x040019B6 RID: 6582
		private const int BARRIER_PHASEFINISHED_ID = 3;
	}
}
