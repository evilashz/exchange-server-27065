using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200075E RID: 1886
	[Serializable]
	public class OccurrenceTimeSpanTooBigException : RecurrenceException
	{
		// Token: 0x06004868 RID: 18536 RVA: 0x00130F3A File Offset: 0x0012F13A
		public OccurrenceTimeSpanTooBigException(TimeSpan occurrenceTimeSpan, TimeSpan minimumTimeBetweenOccurrence, LocalizedString message) : base(message)
		{
			this.OccurrenceTimeSpan = occurrenceTimeSpan;
			this.MinimumTimeBetweenOccurrences = minimumTimeBetweenOccurrence;
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x00130F54 File Offset: 0x0012F154
		protected OccurrenceTimeSpanTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.OccurrenceTimeSpan = (TimeSpan)info.GetValue("OccurrenceTimeSpan", typeof(TimeSpan));
			this.MinimumTimeBetweenOccurrences = (TimeSpan)info.GetValue("MinimumTimeBetweenOccurrences", typeof(TimeSpan));
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x00130FA9 File Offset: 0x0012F1A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("OccurrenceTimeSpan", this.OccurrenceTimeSpan);
			info.AddValue("MinimumTimeBetweenOccurrences", this.MinimumTimeBetweenOccurrences);
		}

		// Token: 0x04002753 RID: 10067
		private const string OccurrenceTimeSpanLabel = "OccurrenceTimeSpan";

		// Token: 0x04002754 RID: 10068
		private const string MinimumTimeBetweenOccurrencesLabel = "MinimumTimeBetweenOccurrences";

		// Token: 0x04002755 RID: 10069
		public readonly TimeSpan OccurrenceTimeSpan;

		// Token: 0x04002756 RID: 10070
		public readonly TimeSpan MinimumTimeBetweenOccurrences;
	}
}
