using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class LoadEntity
	{
		// Token: 0x0600027D RID: 637 RVA: 0x000082B4 File Offset: 0x000064B4
		public LoadEntity(DirectoryObject directoryObject)
		{
			AnchorUtil.ThrowOnNullArgument(directoryObject, "directoryObject");
			this.DirectoryObject = directoryObject;
			this.DirectoryObjectIdentity = directoryObject.Identity;
			this.ConsumedLoad = new LoadMetricStorage();
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000082E5 File Offset: 0x000064E5
		// (set) Token: 0x0600027F RID: 639 RVA: 0x000082ED File Offset: 0x000064ED
		[DataMember]
		public LoadMetricStorage ConsumedLoad { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000082F6 File Offset: 0x000064F6
		// (set) Token: 0x06000281 RID: 641 RVA: 0x000082FE File Offset: 0x000064FE
		[IgnoreDataMember]
		public DirectoryObject DirectoryObject { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00008307 File Offset: 0x00006507
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000830F File Offset: 0x0000650F
		[DataMember]
		public DirectoryIdentity DirectoryObjectIdentity { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00008318 File Offset: 0x00006518
		public Guid Guid
		{
			get
			{
				return this.DirectoryObjectIdentity.Guid;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00008325 File Offset: 0x00006525
		public string Name
		{
			get
			{
				return this.DirectoryObjectIdentity.Name;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00008332 File Offset: 0x00006532
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000833A File Offset: 0x0000653A
		[DataMember]
		public LoadContainer Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (this.parent != null && this.parent != value)
				{
					this.parent.RemoveChild(this.Guid);
				}
				this.parent = value;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00008365 File Offset: 0x00006565
		public virtual void Accept(ILoadEntityVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000836F File Offset: 0x0000656F
		public virtual void ConvertFromSerializationFormat()
		{
			this.ConsumedLoad.ConvertFromSerializationFormat();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000837C File Offset: 0x0000657C
		public virtual long GetAggregateConsumedLoad(LoadMetric metric)
		{
			return this.ConsumedLoad[metric];
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000838C File Offset: 0x0000658C
		public virtual LoadEntity ToSerializationFormat(bool convertBandToBandData)
		{
			LoadEntity loadEntity = this.CreateCopy();
			loadEntity.ConsumedLoad.ConvertToSerializationMetrics(convertBandToBandData);
			return loadEntity;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000083AD File Offset: 0x000065AD
		public override string ToString()
		{
			return string.Format("LoadEntity{{Name={0},GUID={1},Consumed={2}}}", this.Name, this.Guid, this.ConsumedLoad);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000083D0 File Offset: 0x000065D0
		private LoadEntity CreateCopy()
		{
			return new LoadEntity(this.DirectoryObject)
			{
				ConsumedLoad = new LoadMetricStorage(this.ConsumedLoad)
			};
		}

		// Token: 0x040000B6 RID: 182
		private LoadContainer parent;
	}
}
