using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000004 RID: 4
	[KnownType(typeof(PropertyValue))]
	[KnownType(typeof(DxStoreBatchCommand.CreateKey))]
	[KnownType(typeof(DxStoreBatchCommand.DeleteKey))]
	[KnownType(typeof(DxStoreBatchCommand.SetProperty))]
	[KnownType(typeof(DxStoreBatchCommand.DeleteProperty))]
	[Serializable]
	public class DxStoreBatchCommand
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002107 File Offset: 0x00000307
		public virtual WellKnownBatchCommandName GetTypeId()
		{
			return WellKnownBatchCommandName.Unknown;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000210A File Offset: 0x0000030A
		public virtual string GetDebugString()
		{
			return string.Empty;
		}

		// Token: 0x02000005 RID: 5
		[Serializable]
		public class CreateKey : DxStoreBatchCommand
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000A RID: 10 RVA: 0x00002119 File Offset: 0x00000319
			// (set) Token: 0x0600000B RID: 11 RVA: 0x00002121 File Offset: 0x00000321
			[DataMember]
			public string Name { get; set; }

			// Token: 0x0600000C RID: 12 RVA: 0x0000212A File Offset: 0x0000032A
			public override WellKnownBatchCommandName GetTypeId()
			{
				return WellKnownBatchCommandName.CreateKey;
			}

			// Token: 0x0600000D RID: 13 RVA: 0x0000212D File Offset: 0x0000032D
			public override string GetDebugString()
			{
				return string.Format("Key={0}", this.Name);
			}
		}

		// Token: 0x02000006 RID: 6
		[Serializable]
		public class DeleteKey : DxStoreBatchCommand
		{
			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000F RID: 15 RVA: 0x00002147 File Offset: 0x00000347
			// (set) Token: 0x06000010 RID: 16 RVA: 0x0000214F File Offset: 0x0000034F
			[DataMember]
			public string Name { get; set; }

			// Token: 0x06000011 RID: 17 RVA: 0x00002158 File Offset: 0x00000358
			public override WellKnownBatchCommandName GetTypeId()
			{
				return WellKnownBatchCommandName.DeleteKey;
			}

			// Token: 0x06000012 RID: 18 RVA: 0x0000215B File Offset: 0x0000035B
			public override string GetDebugString()
			{
				return string.Format("Key={0}", this.Name);
			}
		}

		// Token: 0x02000007 RID: 7
		[Serializable]
		public class SetProperty : DxStoreBatchCommand
		{
			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000014 RID: 20 RVA: 0x00002175 File Offset: 0x00000375
			// (set) Token: 0x06000015 RID: 21 RVA: 0x0000217D File Offset: 0x0000037D
			[DataMember]
			public string Name { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000016 RID: 22 RVA: 0x00002186 File Offset: 0x00000386
			// (set) Token: 0x06000017 RID: 23 RVA: 0x0000218E File Offset: 0x0000038E
			[DataMember]
			public PropertyValue Value { get; set; }

			// Token: 0x06000018 RID: 24 RVA: 0x00002197 File Offset: 0x00000397
			public override WellKnownBatchCommandName GetTypeId()
			{
				return WellKnownBatchCommandName.SetProperty;
			}

			// Token: 0x06000019 RID: 25 RVA: 0x0000219A File Offset: 0x0000039A
			public override string GetDebugString()
			{
				return string.Format("Name={0} {1}", this.Name, this.Value.GetDebugString());
			}
		}

		// Token: 0x02000008 RID: 8
		[Serializable]
		public class DeleteProperty : DxStoreBatchCommand
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001B RID: 27 RVA: 0x000021BF File Offset: 0x000003BF
			// (set) Token: 0x0600001C RID: 28 RVA: 0x000021C7 File Offset: 0x000003C7
			[DataMember]
			public string Name { get; set; }

			// Token: 0x0600001D RID: 29 RVA: 0x000021D0 File Offset: 0x000003D0
			public override WellKnownBatchCommandName GetTypeId()
			{
				return WellKnownBatchCommandName.DeleteProperty;
			}

			// Token: 0x0600001E RID: 30 RVA: 0x000021D3 File Offset: 0x000003D3
			public override string GetDebugString()
			{
				return string.Format("Name={0}", this.Name);
			}
		}
	}
}
