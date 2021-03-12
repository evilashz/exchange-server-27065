using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000111 RID: 273
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class LegacyMigrationJobProcessorResponse : MigrationProcessorResponse
	{
		// Token: 0x06000E5A RID: 3674 RVA: 0x0003BA04 File Offset: 0x00039C04
		public LegacyMigrationJobProcessorResponse()
		{
			this.NumItemsProcessed = null;
			this.NumItemsOutstanding = null;
			this.NumItemsTransitioned = null;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003BA44 File Offset: 0x00039C44
		private LegacyMigrationJobProcessorResponse(MigrationProcessorResult result) : base(result, null)
		{
			this.NumItemsProcessed = null;
			this.NumItemsOutstanding = null;
			this.NumItemsTransitioned = null;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x0003BA86 File Offset: 0x00039C86
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x0003BA8E File Offset: 0x00039C8E
		public int? NumItemsProcessed { get; set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x0003BA97 File Offset: 0x00039C97
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x0003BA9F File Offset: 0x00039C9F
		public int? NumItemsOutstanding { get; set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0003BAA8 File Offset: 0x00039CA8
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0003BAB0 File Offset: 0x00039CB0
		public int? NumItemsTransitioned { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0003BAB9 File Offset: 0x00039CB9
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x0003BAC1 File Offset: 0x00039CC1
		public MigrationJobStatus? NextStatus { get; set; }

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003BACC File Offset: 0x00039CCC
		internal static LegacyMigrationJobProcessorResponse Create(MigrationProcessorResult result, TimeSpan? delayTime = null)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = new LegacyMigrationJobProcessorResponse(result);
			if (delayTime != null)
			{
				legacyMigrationJobProcessorResponse.DelayTime = delayTime;
			}
			return legacyMigrationJobProcessorResponse;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003BAF4 File Offset: 0x00039CF4
		protected override void Merge(MigrationProcessorResponse left, MigrationProcessorResponse right)
		{
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse = left as LegacyMigrationJobProcessorResponse;
			LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse2 = right as LegacyMigrationJobProcessorResponse;
			foreach (LegacyMigrationJobProcessorResponse legacyMigrationJobProcessorResponse3 in new LegacyMigrationJobProcessorResponse[]
			{
				legacyMigrationJobProcessorResponse,
				legacyMigrationJobProcessorResponse2
			})
			{
				if (legacyMigrationJobProcessorResponse3 != null)
				{
					if (legacyMigrationJobProcessorResponse3.NumItemsOutstanding != null)
					{
						this.NumItemsOutstanding = (this.NumItemsOutstanding ?? 0) + legacyMigrationJobProcessorResponse3.NumItemsOutstanding;
					}
					if (legacyMigrationJobProcessorResponse3.NumItemsProcessed != null)
					{
						this.NumItemsProcessed = (this.NumItemsOutstanding ?? 0) + legacyMigrationJobProcessorResponse3.NumItemsOutstanding;
					}
					if (legacyMigrationJobProcessorResponse3.NumItemsTransitioned != null)
					{
						this.NumItemsTransitioned = (this.NumItemsOutstanding ?? 0) + legacyMigrationJobProcessorResponse3.NumItemsTransitioned;
					}
				}
			}
			base.Merge(left, right);
		}
	}
}
