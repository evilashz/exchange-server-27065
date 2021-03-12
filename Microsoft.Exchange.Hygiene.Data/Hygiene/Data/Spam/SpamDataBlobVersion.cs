using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001FD RID: 509
	internal class SpamDataBlobVersion : ConfigurablePropertyBag
	{
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x00042B20 File Offset: 0x00040D20
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("{0}:{1}:{2}:{3}", new object[]
				{
					this.DataID,
					this.DataTypeID,
					this.MajorVersion,
					this.MinorVersion
				}));
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00042B7C File Offset: 0x00040D7C
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x00042B8E File Offset: 0x00040D8E
		internal SpamDataBlobDataID DataID
		{
			get
			{
				return (SpamDataBlobDataID)this[SpamDataBlobVersion.DataIDProperty];
			}
			set
			{
				this[SpamDataBlobVersion.DataIDProperty] = (byte)value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00042BA1 File Offset: 0x00040DA1
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x00042BB3 File Offset: 0x00040DB3
		internal byte DataTypeID
		{
			get
			{
				return (byte)this[SpamDataBlobVersion.DataTypeIDProperty];
			}
			set
			{
				this[SpamDataBlobVersion.DataTypeIDProperty] = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00042BC6 File Offset: 0x00040DC6
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x00042BD8 File Offset: 0x00040DD8
		internal int MajorVersion
		{
			get
			{
				return (int)this[SpamDataBlobVersion.MajorVersionProperty];
			}
			set
			{
				this[SpamDataBlobVersion.MajorVersionProperty] = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00042BEB File Offset: 0x00040DEB
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x00042BFD File Offset: 0x00040DFD
		internal int MinorVersion
		{
			get
			{
				return (int)this[SpamDataBlobVersion.MinorVersionProperty];
			}
			set
			{
				this[SpamDataBlobVersion.MinorVersionProperty] = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00042C10 File Offset: 0x00040E10
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x00042C22 File Offset: 0x00040E22
		internal long SpamDataTotalSize
		{
			get
			{
				return (long)this[SpamDataBlobVersion.SpamDataTotalSizeProperty];
			}
			set
			{
				this[SpamDataBlobVersion.SpamDataTotalSizeProperty] = value;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00042C35 File Offset: 0x00040E35
		public DateTime ChangedDateTime
		{
			get
			{
				return (DateTime)this[SpamDataBlobVersion.ChangedDateTimeProperty];
			}
		}

		// Token: 0x04000AB6 RID: 2742
		public static readonly HygienePropertyDefinition DataIDProperty = new HygienePropertyDefinition("ti_DataId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB7 RID: 2743
		public static readonly HygienePropertyDefinition DataTypeIDProperty = new HygienePropertyDefinition("ti_DataTypeId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB8 RID: 2744
		public static readonly HygienePropertyDefinition MajorVersionProperty = new HygienePropertyDefinition("i_MajorVersion", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB9 RID: 2745
		public static readonly HygienePropertyDefinition MinorVersionProperty = new HygienePropertyDefinition("i_MinorVersion", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ABA RID: 2746
		public static readonly HygienePropertyDefinition SpamDataTotalSizeProperty = new HygienePropertyDefinition("bi_SpamDataTotalSize", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ABB RID: 2747
		public static readonly HygienePropertyDefinition ChangedDateTimeProperty = new HygienePropertyDefinition("dt_ChangedDateTime", typeof(DateTime?));
	}
}
