using System;
using System.Text;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A1C RID: 2588
	[Serializable]
	public class MigrationBatchId : ObjectId
	{
		// Token: 0x06005F29 RID: 24361 RVA: 0x001919CD File Offset: 0x0018FBCD
		internal MigrationBatchId(string batchName, Guid jobId)
		{
			if (jobId == Guid.Empty && string.IsNullOrEmpty(batchName))
			{
				throw new InvalidMigrationBatchIdException();
			}
			this.name = batchName;
			this.jobId = jobId;
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x001919FE File Offset: 0x0018FBFE
		internal MigrationBatchId(string batchName) : this(batchName, Guid.Empty)
		{
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x00191A0C File Offset: 0x0018FC0C
		internal MigrationBatchId(Guid jobId) : this(jobId.ToString(), jobId)
		{
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x06005F2C RID: 24364 RVA: 0x00191A24 File Offset: 0x0018FC24
		public string Id
		{
			get
			{
				if (!(this.JobId != Guid.Empty))
				{
					return this.Name;
				}
				return this.JobId.ToString();
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x06005F2D RID: 24365 RVA: 0x00191A5E File Offset: 0x0018FC5E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x06005F2E RID: 24366 RVA: 0x00191A66 File Offset: 0x0018FC66
		public Guid JobId
		{
			get
			{
				return this.jobId;
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x06005F2F RID: 24367 RVA: 0x00191A6E File Offset: 0x0018FC6E
		internal static MigrationBatchId Any
		{
			get
			{
				return new MigrationBatchId("90DA65B9-2154-4338-A690-602DF1FD5BAC", MigrationBatchId.AnyBatchGuid);
			}
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x00191A7F File Offset: 0x0018FC7F
		public override byte[] GetBytes()
		{
			return Encoding.UTF8.GetBytes(this.Name);
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x00191A94 File Offset: 0x0018FC94
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return this.JobId.ToString();
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x00191ACC File Offset: 0x0018FCCC
		public override bool Equals(object obj)
		{
			MigrationBatchId migrationBatchId = obj as MigrationBatchId;
			return migrationBatchId != null && (string.Equals(this.Name, migrationBatchId.Name, StringComparison.OrdinalIgnoreCase) || this.JobId.Equals(migrationBatchId.JobId));
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x00191B0F File Offset: 0x0018FD0F
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x040034F9 RID: 13561
		public const string MigrationMailboxName = "Migration.8f3e7716-2011-43e4-96b1-aba62d229136";

		// Token: 0x040034FA RID: 13562
		private const string AnyBatchId = "90DA65B9-2154-4338-A690-602DF1FD5BAC";

		// Token: 0x040034FB RID: 13563
		private static readonly Guid AnyBatchGuid = Guid.Parse("90DA65B9-2154-4338-A690-602DF1FD5BAC");

		// Token: 0x040034FC RID: 13564
		private readonly string name;

		// Token: 0x040034FD RID: 13565
		private readonly Guid jobId;
	}
}
