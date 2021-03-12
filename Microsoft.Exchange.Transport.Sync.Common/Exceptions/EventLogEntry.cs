using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Exceptions
{
	// Token: 0x02000069 RID: 105
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class EventLogEntry
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000751A File Offset: 0x0000571A
		public EventLogEntry(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			this.tuple = tuple;
			this.periodicKey = periodicKey;
			this.messageArgs = messageArgs;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007537 File Offset: 0x00005737
		public ExEventLog.EventTuple Tuple
		{
			get
			{
				return this.tuple;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000753F File Offset: 0x0000573F
		public string PeriodicKey
		{
			get
			{
				return this.periodicKey;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00007547 File Offset: 0x00005747
		public object[] MessageArgs
		{
			get
			{
				return this.messageArgs;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000754F File Offset: 0x0000574F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00007558 File Offset: 0x00005758
		public override bool Equals(object obj)
		{
			EventLogEntry eventLogEntry = obj as EventLogEntry;
			if (eventLogEntry == null)
			{
				return false;
			}
			if (this.Tuple.EventId != eventLogEntry.Tuple.EventId || this.Tuple.CategoryId != eventLogEntry.Tuple.CategoryId || this.Tuple.EntryType != eventLogEntry.Tuple.EntryType || this.Tuple.Level != eventLogEntry.Tuple.Level || this.Tuple.Period != eventLogEntry.Tuple.Period)
			{
				return false;
			}
			if (this.PeriodicKey != eventLogEntry.PeriodicKey)
			{
				return false;
			}
			if ((this.MessageArgs == null && eventLogEntry.MessageArgs != null) || (this.MessageArgs != null && eventLogEntry.MessageArgs == null))
			{
				return false;
			}
			if (this.MessageArgs != null && eventLogEntry.MessageArgs != null)
			{
				if (this.MessageArgs.Length != eventLogEntry.MessageArgs.Length)
				{
					return false;
				}
				for (int i = 0; i < this.MessageArgs.Length; i++)
				{
					object objA = this.MessageArgs[i];
					object objB = eventLogEntry.MessageArgs[i];
					if (!object.Equals(objA, objB))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000076A4 File Offset: 0x000058A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("EventId:");
			stringBuilder.Append(this.Tuple.EventId);
			stringBuilder.Append(",");
			stringBuilder.Append("CategoryId:");
			stringBuilder.Append(this.Tuple.CategoryId);
			stringBuilder.Append(",");
			stringBuilder.Append("EntryType:");
			stringBuilder.Append(this.Tuple.EntryType);
			stringBuilder.Append(",");
			stringBuilder.Append("Level:");
			stringBuilder.Append(this.Tuple.Level);
			stringBuilder.Append(",");
			stringBuilder.Append("Period:");
			stringBuilder.Append(this.Tuple.Period);
			stringBuilder.Append(",");
			stringBuilder.Append("PeriodicKey:");
			stringBuilder.Append(this.PeriodicKey);
			stringBuilder.Append(",");
			stringBuilder.Append("MessageArgs:");
			if (this.MessageArgs != null)
			{
				foreach (object value in this.MessageArgs)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(",");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400011B RID: 283
		private ExEventLog.EventTuple tuple;

		// Token: 0x0400011C RID: 284
		private string periodicKey;

		// Token: 0x0400011D RID: 285
		private object[] messageArgs;
	}
}
