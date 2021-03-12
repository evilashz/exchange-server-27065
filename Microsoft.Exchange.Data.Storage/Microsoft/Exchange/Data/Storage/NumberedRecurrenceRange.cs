using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003E8 RID: 1000
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NumberedRecurrenceRange : RecurrenceRange
	{
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x000BBBE6 File Offset: 0x000B9DE6
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x000BBBEE File Offset: 0x000B9DEE
		public int NumberOfOccurrences
		{
			get
			{
				return this.numberOfOccurrences;
			}
			private set
			{
				if (!NumberedRecurrenceRange.IsValidNumberedRecurrenceRange(value))
				{
					throw new ArgumentException(ServerStrings.ExInvalidNumberOfOccurrences, "NumberOfOccurrences");
				}
				this.numberOfOccurrences = value;
			}
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000BBC14 File Offset: 0x000B9E14
		public NumberedRecurrenceRange(ExDateTime startDate, int numberOfOccurrences)
		{
			this.StartDate = startDate;
			this.NumberOfOccurrences = numberOfOccurrences;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000BBC2C File Offset: 0x000B9E2C
		public override bool Equals(RecurrenceRange value)
		{
			if (!(value is NumberedRecurrenceRange))
			{
				return false;
			}
			NumberedRecurrenceRange numberedRecurrenceRange = (NumberedRecurrenceRange)value;
			return numberedRecurrenceRange.NumberOfOccurrences == this.numberOfOccurrences && base.Equals(value);
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000BBC64 File Offset: 0x000B9E64
		public override string ToString()
		{
			return string.Format("Starts {0}, occurs {1} times", this.StartDate.ToString(DateTimeFormatInfo.InvariantInfo), this.NumberOfOccurrences);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000BBC99 File Offset: 0x000B9E99
		internal static bool IsValidNumberedRecurrenceRange(int numberedOccurrence)
		{
			return numberedOccurrence >= 1 && numberedOccurrence <= StorageLimits.Instance.RecurrenceMaximumNumberedOccurrences;
		}

		// Token: 0x04001905 RID: 6405
		private int numberOfOccurrences;
	}
}
