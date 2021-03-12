using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000B1 RID: 177
	internal class DataBlobVersion : ConfigurablePropertyBag
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00013AB4 File Offset: 0x00011CB4
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}\\{1}:{2}", this.DataTypeId, this.Version.ToString(), this.BlobId));
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00013AE6 File Offset: 0x00011CE6
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public int DataTypeId
		{
			get
			{
				return (int)this[DataBlobVersionSchema.DataTypeIdProperty];
			}
			set
			{
				this[DataBlobVersionSchema.DataTypeIdProperty] = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00013B0B File Offset: 0x00011D0B
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00013B1D File Offset: 0x00011D1D
		public Guid BlobId
		{
			get
			{
				return (Guid)this[DataBlobVersionSchema.BlobIdProperty];
			}
			set
			{
				this[DataBlobVersionSchema.BlobIdProperty] = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00013B30 File Offset: 0x00011D30
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00013B4C File Offset: 0x00011D4C
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
			set
			{
				this.version = value;
				if (value != null)
				{
					this[DataBlobVersionSchema.MajorVersionProperty] = value.Major;
					this[DataBlobVersionSchema.MinorVersionProperty] = value.Minor;
					this[DataBlobVersionSchema.BuildNumberProperty] = value.Build;
					this[DataBlobVersionSchema.RevisionNumberProperty] = value.Revision;
					return;
				}
				this[DataBlobVersionSchema.MajorVersionProperty] = -1;
				this[DataBlobVersionSchema.MinorVersionProperty] = -1;
				this[DataBlobVersionSchema.BuildNumberProperty] = -1;
				this[DataBlobVersionSchema.RevisionNumberProperty] = -1;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00013C06 File Offset: 0x00011E06
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00013C18 File Offset: 0x00011E18
		public long BlobSizeBytes
		{
			get
			{
				return (long)this[DataBlobVersionSchema.BlobSizeBytesProperty];
			}
			set
			{
				this[DataBlobVersionSchema.BlobSizeBytesProperty] = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00013C2B File Offset: 0x00011E2B
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00013C3D File Offset: 0x00011E3D
		public bool IsCompleteBlob
		{
			get
			{
				return (bool)this[DataBlobVersionSchema.IsCompleteBlobProperty];
			}
			set
			{
				this[DataBlobVersionSchema.IsCompleteBlobProperty] = value;
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00013C50 File Offset: 0x00011E50
		public override Type GetSchemaType()
		{
			return typeof(DataBlobVersionSchema);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00013C5C File Offset: 0x00011E5C
		private void SetBuildVersion()
		{
			int major = (int)this[DataBlobVersionSchema.MajorVersionProperty];
			int minor = (int)this[DataBlobVersionSchema.MinorVersionProperty];
			int build = (int)this[DataBlobVersionSchema.BuildNumberProperty];
			int revision = (int)this[DataBlobVersionSchema.RevisionNumberProperty];
			this.version = BuildVersion.GetBuildVersionObject(major, minor, build, revision);
		}

		// Token: 0x0400039F RID: 927
		private Version version;
	}
}
