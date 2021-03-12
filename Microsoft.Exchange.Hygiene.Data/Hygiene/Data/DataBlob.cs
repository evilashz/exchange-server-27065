using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000AD RID: 173
	internal class DataBlob : ConfigurablePropertyBag
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00013089 File Offset: 0x00011289
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}:{2}", this.DataTypeId, this.BlobId, this.ChunkId));
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x000130BB File Offset: 0x000112BB
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x000130CD File Offset: 0x000112CD
		public int DataTypeId
		{
			get
			{
				return (int)this[DataBlobSchema.DataTypeIdProperty];
			}
			set
			{
				this[DataBlobSchema.DataTypeIdProperty] = value;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000130E0 File Offset: 0x000112E0
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x000130F2 File Offset: 0x000112F2
		public Guid BlobId
		{
			get
			{
				return (Guid)this[DataBlobSchema.BlobIdProperty];
			}
			set
			{
				this[DataBlobSchema.BlobIdProperty] = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00013105 File Offset: 0x00011305
		public Version Version
		{
			get
			{
				if (this.version == null)
				{
					this.SetBuildVersion();
				}
				return this.version;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00013121 File Offset: 0x00011321
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00013133 File Offset: 0x00011333
		public int ChunkId
		{
			get
			{
				return (int)this[DataBlobSchema.ChunkIdProperty];
			}
			set
			{
				this[DataBlobSchema.ChunkIdProperty] = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00013146 File Offset: 0x00011346
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00013158 File Offset: 0x00011358
		public bool IsLastChunk
		{
			get
			{
				return (bool)this[DataBlobSchema.IsLastChunkProperty];
			}
			set
			{
				this[DataBlobSchema.IsLastChunkProperty] = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001316B File Offset: 0x0001136B
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0001317D File Offset: 0x0001137D
		public long Checksum
		{
			get
			{
				return (long)this[DataBlobSchema.ChecksumProperty];
			}
			set
			{
				this[DataBlobSchema.ChecksumProperty] = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x00013190 File Offset: 0x00011390
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x000131A2 File Offset: 0x000113A2
		public byte[] DataChunk
		{
			get
			{
				return (byte[])this[DataBlobSchema.DataChunkProperty];
			}
			set
			{
				this[DataBlobSchema.DataChunkProperty] = value;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000131B0 File Offset: 0x000113B0
		public override Type GetSchemaType()
		{
			return typeof(DataBlobSchema);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000131BC File Offset: 0x000113BC
		private void SetBuildVersion()
		{
			int major = (int)this[DataBlobSchema.MajorVersionProperty];
			int minor = (int)this[DataBlobSchema.MinorVersionProperty];
			int build = (int)this[DataBlobSchema.BuildNumberProperty];
			int revision = (int)this[DataBlobSchema.RevisionNumberProperty];
			this.version = BuildVersion.GetBuildVersionObject(major, minor, build, revision);
		}

		// Token: 0x04000382 RID: 898
		private Version version;
	}
}
