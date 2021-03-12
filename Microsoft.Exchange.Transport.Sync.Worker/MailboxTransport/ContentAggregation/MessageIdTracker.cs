using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x020001EA RID: 490
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MessageIdTracker
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x000328C8 File Offset: 0x00030AC8
		internal MessageIdTracker(int capacity)
		{
			this.list = new List<int>(capacity);
			this.mode = MessageIdTracker.IdTrackerMode.List;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000328E3 File Offset: 0x00030AE3
		internal MessageIdTracker(params int[] messageIds) : this(messageIds)
		{
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000328EC File Offset: 0x00030AEC
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

		// Token: 0x0600102B RID: 4139 RVA: 0x00032928 File Offset: 0x00030B28
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

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00032994 File Offset: 0x00030B94
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

		// Token: 0x0600102D RID: 4141 RVA: 0x000329F0 File Offset: 0x00030BF0
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

		// Token: 0x0600102E RID: 4142 RVA: 0x00032AB0 File Offset: 0x00030CB0
		internal void Add(int id)
		{
			if (this.mode != MessageIdTracker.IdTrackerMode.List)
			{
				throw new InvalidOperationException("The message id tracker is not in valid mode.");
			}
			this.list.Add(id);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x00032AD4 File Offset: 0x00030CD4
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

		// Token: 0x06001030 RID: 4144 RVA: 0x00032B58 File Offset: 0x00030D58
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

		// Token: 0x06001031 RID: 4145 RVA: 0x00032BAC File Offset: 0x00030DAC
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

		// Token: 0x06001032 RID: 4146 RVA: 0x00032C14 File Offset: 0x00030E14
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

		// Token: 0x040008C8 RID: 2248
		private List<int> list;

		// Token: 0x040008C9 RID: 2249
		private int minId;

		// Token: 0x040008CA RID: 2250
		private int maxId;

		// Token: 0x040008CB RID: 2251
		private bool backward;

		// Token: 0x040008CC RID: 2252
		private int current;

		// Token: 0x040008CD RID: 2253
		private MessageIdTracker.IdTrackerMode mode;

		// Token: 0x020001EB RID: 491
		private enum IdTrackerMode
		{
			// Token: 0x040008CF RID: 2255
			Range,
			// Token: 0x040008D0 RID: 2256
			List
		}
	}
}
