using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA6 RID: 3750
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncrementalSyncIntervalRangeException : RecipientTaskException
	{
		// Token: 0x0600A812 RID: 43026 RVA: 0x0028964E File Offset: 0x0028784E
		public IncrementalSyncIntervalRangeException(int minDays, int maxDays) : base(Strings.ErrorIncrementalSyncIntervalRange(minDays, maxDays))
		{
			this.minDays = minDays;
			this.maxDays = maxDays;
		}

		// Token: 0x0600A813 RID: 43027 RVA: 0x0028966B File Offset: 0x0028786B
		public IncrementalSyncIntervalRangeException(int minDays, int maxDays, Exception innerException) : base(Strings.ErrorIncrementalSyncIntervalRange(minDays, maxDays), innerException)
		{
			this.minDays = minDays;
			this.maxDays = maxDays;
		}

		// Token: 0x0600A814 RID: 43028 RVA: 0x0028968C File Offset: 0x0028788C
		protected IncrementalSyncIntervalRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.minDays = (int)info.GetValue("minDays", typeof(int));
			this.maxDays = (int)info.GetValue("maxDays", typeof(int));
		}

		// Token: 0x0600A815 RID: 43029 RVA: 0x002896E1 File Offset: 0x002878E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("minDays", this.minDays);
			info.AddValue("maxDays", this.maxDays);
		}

		// Token: 0x17003697 RID: 13975
		// (get) Token: 0x0600A816 RID: 43030 RVA: 0x0028970D File Offset: 0x0028790D
		public int MinDays
		{
			get
			{
				return this.minDays;
			}
		}

		// Token: 0x17003698 RID: 13976
		// (get) Token: 0x0600A817 RID: 43031 RVA: 0x00289715 File Offset: 0x00287915
		public int MaxDays
		{
			get
			{
				return this.maxDays;
			}
		}

		// Token: 0x04005FFD RID: 24573
		private readonly int minDays;

		// Token: 0x04005FFE RID: 24574
		private readonly int maxDays;
	}
}
