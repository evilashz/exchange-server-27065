using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000290 RID: 656
	internal class PerThreadTimeTracker
	{
		// Token: 0x06001818 RID: 6168 RVA: 0x0008D955 File Offset: 0x0008BB55
		internal PerThreadTimeTracker()
		{
			this.IsValid = true;
			this.Root = this.Start(TimeId.Root);
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0008D97C File Offset: 0x0008BB7C
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0008D984 File Offset: 0x0008BB84
		public bool IsValid { get; private set; }

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0008D98D File Offset: 0x0008BB8D
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0008D995 File Offset: 0x0008BB95
		public string InvalidReason { get; private set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0008D99E File Offset: 0x0008BB9E
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x0008D9A6 File Offset: 0x0008BBA6
		public TimeEntry Root { get; private set; }

		// Token: 0x0600181F RID: 6175 RVA: 0x0008D9B0 File Offset: 0x0008BBB0
		public TimeEntry Start(TimeId timeId)
		{
			if (this.Root != null)
			{
				this.Root.VerifyThread();
			}
			TimeEntry timeEntry = new TimeEntry(timeId, new Action<TimeEntry>(this.End));
			this.callStack.Push(timeEntry);
			return timeEntry;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x0008D9F0 File Offset: 0x0008BBF0
		private void Invalidate(string reason)
		{
			this.IsValid = false;
			if (this.InvalidReason == null)
			{
				this.InvalidReason = reason;
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0008DA08 File Offset: 0x0008BC08
		private void End(TimeEntry timeEntry)
		{
			this.Root.VerifyThread();
			if (this.callStack.Count == 0)
			{
				this.Invalidate("EmptyStack-" + timeEntry.TimeId);
				return;
			}
			TimeEntry timeEntry2 = this.callStack.Pop();
			if (object.ReferenceEquals(timeEntry, timeEntry2))
			{
				if (this.callStack.Count > 0)
				{
					TimeEntry timeEntry3 = this.callStack.Peek();
					if (timeEntry3 != null)
					{
						timeEntry3.AddChild(timeEntry2);
					}
				}
				return;
			}
			if (timeEntry.TimeId == timeEntry2.TimeId)
			{
				this.Invalidate(string.Format("MismatchStartEnd-RefMismatch:'{0}'", timeEntry.TimeId));
				return;
			}
			this.Invalidate(string.Format("MismatchStartEnd-Exp:'{0}'-Act:'{1}'", timeEntry2.TimeId, timeEntry.TimeId));
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0008DAD3 File Offset: 0x0008BCD3
		public override string ToString()
		{
			if (!this.IsValid)
			{
				return this.InvalidReason;
			}
			return this.Root.ToString();
		}

		// Token: 0x04000EB9 RID: 3769
		private Stack<TimeEntry> callStack = new Stack<TimeEntry>();
	}
}
