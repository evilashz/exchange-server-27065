using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MessageIdTracker
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000229C File Offset: 0x0000049C
		internal MessageIdTracker(int capacity)
		{
			this.list = new List<int>(capacity);
			this.mode = MessageIdTracker.IdTrackerMode.List;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022B7 File Offset: 0x000004B7
		internal MessageIdTracker(params int[] messageIds) : this(messageIds)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C0 File Offset: 0x000004C0
		internal MessageIdTracker(ICollection<int> messageIds)
		{
			if (messageIds == null)
			{
				throw new ArgumentNullException("messageIds");
			}
			this.list = new List<int>(messageIds.Count);
			this.list.AddRange(messageIds);
			this.mode = MessageIdTracker.IdTrackerMode.List;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022FC File Offset: 0x000004FC
		internal MessageIdTracker(int minId, int maxId, bool backward)
		{
			if (minId <= 0)
			{
				throw new ArgumentOutOfRangeException("minId");
			}
			if (maxId < minId)
			{
				throw new ArgumentOutOfRangeException("maxId");
			}
			this.minId = minId;
			this.maxId = maxId;
			this.backward = backward;
			this.current = (this.backward ? this.maxId : this.minId);
			this.mode = MessageIdTracker.IdTrackerMode.Range;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002368 File Offset: 0x00000568
		internal int Current
		{
			get
			{
				switch (this.mode)
				{
				case MessageIdTracker.IdTrackerMode.Range:
					if (!this.backward)
					{
						return this.current - 1;
					}
					return this.current + 1;
				case MessageIdTracker.IdTrackerMode.List:
					return this.list[this.current - 1];
				default:
					throw new InvalidOperationException("The message id tracker mode is invalid.");
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023C4 File Offset: 0x000005C4
		public override string ToString()
		{
			switch (this.mode)
			{
			case MessageIdTracker.IdTrackerMode.Range:
				return string.Format(CultureInfo.InvariantCulture, "{0} ({1} to {2})", new object[]
				{
					this.Current,
					this.minId,
					this.maxId
				});
			case MessageIdTracker.IdTrackerMode.List:
				return string.Format(CultureInfo.InvariantCulture, "{0} ({1} of {2})", new object[]
				{
					(this.current == 0) ? 0 : this.Current,
					this.current,
					this.list.Count
				});
			default:
				return "The message id tracker mode is invalid.";
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002484 File Offset: 0x00000684
		internal void Add(int id)
		{
			if (this.mode != MessageIdTracker.IdTrackerMode.List)
			{
				throw new InvalidOperationException("The message id tracker is not in valid mode.");
			}
			this.list.Add(id);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024A8 File Offset: 0x000006A8
		internal bool MoveNext()
		{
			switch (this.mode)
			{
			case MessageIdTracker.IdTrackerMode.Range:
				if (this.backward ? (this.current <= 0) : (this.current > this.maxId))
				{
					return false;
				}
				break;
			case MessageIdTracker.IdTrackerMode.List:
				if (this.current >= this.list.Count)
				{
					return false;
				}
				break;
			}
			this.current = (this.backward ? (this.current - 1) : (this.current + 1));
			return true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000252C File Offset: 0x0000072C
		internal bool Contains(int id)
		{
			switch (this.mode)
			{
			case MessageIdTracker.IdTrackerMode.Range:
				return id >= this.minId && id <= this.maxId;
			case MessageIdTracker.IdTrackerMode.List:
				return this.list.Contains(id);
			default:
				throw new InvalidOperationException("The message id tracker mode is invalid.");
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002580 File Offset: 0x00000780
		internal void Remove(int id)
		{
			if (!this.Contains(id))
			{
				throw new InvalidOperationException("The specified id is not being tracked.");
			}
			if (this.mode == MessageIdTracker.IdTrackerMode.Range)
			{
				throw new InvalidOperationException("The message id tracker is not in valid mode.");
			}
			int num = this.list.IndexOf(id);
			if (this.current > num)
			{
				this.current--;
			}
			this.list.Remove(id);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025E8 File Offset: 0x000007E8
		internal void Reset()
		{
			switch (this.mode)
			{
			case MessageIdTracker.IdTrackerMode.Range:
				this.current = (this.backward ? this.maxId : this.minId);
				return;
			case MessageIdTracker.IdTrackerMode.List:
				this.current = 0;
				return;
			default:
				throw new InvalidOperationException("The message id tracker mode is invalid.");
			}
		}

		// Token: 0x04000002 RID: 2
		private readonly List<int> list;

		// Token: 0x04000003 RID: 3
		private readonly int minId;

		// Token: 0x04000004 RID: 4
		private readonly int maxId;

		// Token: 0x04000005 RID: 5
		private readonly bool backward;

		// Token: 0x04000006 RID: 6
		private readonly MessageIdTracker.IdTrackerMode mode;

		// Token: 0x04000007 RID: 7
		private int current;

		// Token: 0x02000005 RID: 5
		private enum IdTrackerMode
		{
			// Token: 0x04000009 RID: 9
			Range,
			// Token: 0x0400000A RID: 10
			List
		}
	}
}
