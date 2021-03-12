using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001FC RID: 508
	internal class SpamDataBlob : ConfigurablePropertyBag
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000428C8 File Offset: 0x00040AC8
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.SpamDataBlobID.ToString());
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000428EE File Offset: 0x00040AEE
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x00042900 File Offset: 0x00040B00
		internal Guid SpamDataBlobID
		{
			get
			{
				return (Guid)this[SpamDataBlob.SpamDataBlobIDProperty];
			}
			set
			{
				this[SpamDataBlob.SpamDataBlobIDProperty] = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00042913 File Offset: 0x00040B13
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x00042925 File Offset: 0x00040B25
		internal SpamDataBlobDataID DataID
		{
			get
			{
				return (SpamDataBlobDataID)this[SpamDataBlob.DataIDProperty];
			}
			set
			{
				this[SpamDataBlob.DataIDProperty] = (byte)value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00042938 File Offset: 0x00040B38
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x0004294A File Offset: 0x00040B4A
		internal byte DataTypeID
		{
			get
			{
				return (byte)this[SpamDataBlob.DataTypeIDProperty];
			}
			set
			{
				this[SpamDataBlob.DataTypeIDProperty] = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0004295D File Offset: 0x00040B5D
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x0004296F File Offset: 0x00040B6F
		internal int MajorVersion
		{
			get
			{
				return (int)this[SpamDataBlob.MajorVersionProperty];
			}
			set
			{
				this[SpamDataBlob.MajorVersionProperty] = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00042982 File Offset: 0x00040B82
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x00042994 File Offset: 0x00040B94
		internal int MinorVersion
		{
			get
			{
				return (int)this[SpamDataBlob.MinorVersionProperty];
			}
			set
			{
				this[SpamDataBlob.MinorVersionProperty] = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x000429A7 File Offset: 0x00040BA7
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x000429B9 File Offset: 0x00040BB9
		internal int ChunkID
		{
			get
			{
				return (int)this[SpamDataBlob.ChunkIDProperty];
			}
			set
			{
				this[SpamDataBlob.ChunkIDProperty] = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x000429CC File Offset: 0x00040BCC
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x000429DE File Offset: 0x00040BDE
		internal bool IsEndData
		{
			get
			{
				return (bool)this[SpamDataBlob.IsEndDataProperty];
			}
			set
			{
				this[SpamDataBlob.IsEndDataProperty] = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000429F1 File Offset: 0x00040BF1
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x00042A03 File Offset: 0x00040C03
		internal byte[] DataChunk
		{
			get
			{
				return (byte[])this[SpamDataBlob.DataChunkProperty];
			}
			set
			{
				this[SpamDataBlob.DataChunkProperty] = value;
			}
		}

		// Token: 0x04000AAE RID: 2734
		public static readonly HygienePropertyDefinition SpamDataBlobIDProperty = new HygienePropertyDefinition("id_SpamDataBlobId", typeof(Guid));

		// Token: 0x04000AAF RID: 2735
		public static readonly HygienePropertyDefinition DataIDProperty = new HygienePropertyDefinition("ti_DataId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB0 RID: 2736
		public static readonly HygienePropertyDefinition DataTypeIDProperty = new HygienePropertyDefinition("ti_DataTypeId", typeof(byte), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB1 RID: 2737
		public static readonly HygienePropertyDefinition MajorVersionProperty = new HygienePropertyDefinition("i_MajorVersion", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB2 RID: 2738
		public static readonly HygienePropertyDefinition MinorVersionProperty = new HygienePropertyDefinition("i_MinorVersion", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB3 RID: 2739
		public static readonly HygienePropertyDefinition ChunkIDProperty = new HygienePropertyDefinition("i_ChunkId", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000AB4 RID: 2740
		public static readonly HygienePropertyDefinition IsEndDataProperty = new HygienePropertyDefinition("f_IsEndData", typeof(bool));

		// Token: 0x04000AB5 RID: 2741
		public static readonly HygienePropertyDefinition DataChunkProperty = new HygienePropertyDefinition("vb_DataChunk", typeof(byte[]));
	}
}
