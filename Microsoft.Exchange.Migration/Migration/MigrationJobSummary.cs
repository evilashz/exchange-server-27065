using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000073 RID: 115
	internal class MigrationJobSummary
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001E0C1 File Offset: 0x0001C2C1
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0001E0C9 File Offset: 0x0001C2C9
		public string BatchName { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001E0D2 File Offset: 0x0001C2D2
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001E0DA File Offset: 0x0001C2DA
		public Guid BatchGuid { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001E0E3 File Offset: 0x0001C2E3
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001E0EB File Offset: 0x0001C2EB
		public ExTimeZone UserTimeZone { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001E0F4 File Offset: 0x0001C2F4
		public MigrationBatchId BatchId
		{
			get
			{
				return new MigrationBatchId(this.BatchName, this.BatchGuid);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001E108 File Offset: 0x0001C308
		public static MigrationJobSummary LoadFromRow(object[] propertyValues)
		{
			if (propertyValues == null)
			{
				return null;
			}
			MigrationJobSummary migrationJobSummary = new MigrationJobSummary();
			for (int i = 0; i < MigrationJobSummary.PropertyDefinitions.Length; i++)
			{
				if (propertyValues[i] != null && !(propertyValues[i] is PropertyError))
				{
					if (MigrationJobSummary.PropertyDefinitions[i] == StoreObjectSchema.ItemClass && !string.Equals((string)propertyValues[i], MigrationBatchMessageSchema.MigrationJobClass))
					{
						return null;
					}
					if (MigrationJobSummary.PropertyDefinitions[i] == MigrationBatchMessageSchema.MigrationJobName)
					{
						migrationJobSummary.BatchName = (string)propertyValues[i];
					}
					else if (MigrationJobSummary.PropertyDefinitions[i] == MigrationBatchMessageSchema.MigrationJobId)
					{
						migrationJobSummary.BatchGuid = (Guid)propertyValues[i];
					}
					else if (MigrationJobSummary.PropertyDefinitions[i] == MigrationBatchMessageSchema.MigrationJobUserTimeZone)
					{
						Exception ex = null;
						migrationJobSummary.UserTimeZone = MigrationHelper.GetExTimeZoneValue(propertyValues[i], ref ex);
					}
				}
			}
			return migrationJobSummary;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		internal static MigrationJobSummary CreateFromJob(MigrationJob job)
		{
			return new MigrationJobSummary
			{
				BatchGuid = job.JobId,
				BatchName = job.JobName,
				UserTimeZone = job.UserTimeZone
			};
		}

		// Token: 0x040002B4 RID: 692
		internal static readonly PropertyDefinition[] PropertyDefinitions = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MigrationBatchMessageSchema.MigrationJobName,
			MigrationBatchMessageSchema.MigrationJobId,
			MigrationBatchMessageSchema.MigrationJobUserTimeZone
		};
	}
}
