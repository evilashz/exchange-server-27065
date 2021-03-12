using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Constraints;
using Microsoft.Exchange.MailboxLoadBalance.Data.LoadMetrics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LoadContainer : LoadEntity
	{
		// Token: 0x0600028E RID: 654 RVA: 0x000083FC File Offset: 0x000065FC
		public LoadContainer(DirectoryObject directoryObject, ContainerType type = ContainerType.Generic) : base(directoryObject)
		{
			this.ContainerType = type;
			this.DataRetrievedTimestamp = ExDateTime.UtcNow;
			this.children = new List<LoadEntity>();
			this.reusableCapacity = new LoadMetricStorage();
			this.maximumLoad = new LoadMetricStorage();
			this.committedLoad = new LoadMetricStorage();
			this.Constraint = new LoadCapacityConstraint(this);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000845A File Offset: 0x0000665A
		public LoadMetricStorage AvailableCapacity
		{
			get
			{
				return this.MaximumLoad - base.ConsumedLoad - this.CommittedLoad + this.ReusableCapacity;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000848C File Offset: 0x0000668C
		// (set) Token: 0x06000291 RID: 657 RVA: 0x000084DF File Offset: 0x000066DF
		public bool CanAcceptBalancingLoad
		{
			get
			{
				if (this.canAcceptBalancingLoad != null)
				{
					return this.canAcceptBalancingLoad.Value;
				}
				return this.Children.OfType<LoadContainer>().Any((LoadContainer c) => c.CanAcceptBalancingLoad);
			}
			set
			{
				this.canAcceptBalancingLoad = new bool?(value);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000084F8 File Offset: 0x000066F8
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000854B File Offset: 0x0000674B
		public bool CanAcceptRegularLoad
		{
			get
			{
				if (this.canAcceptRegularLoad != null)
				{
					return this.canAcceptRegularLoad.Value;
				}
				return this.Children.OfType<LoadContainer>().Any((LoadContainer c) => c.CanAcceptRegularLoad);
			}
			set
			{
				this.canAcceptRegularLoad = new bool?(value);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000855C File Offset: 0x0000675C
		public List<LoadEntity> Children
		{
			get
			{
				if (this.children == null)
				{
					List<LoadEntity> value = new List<LoadEntity>();
					Interlocked.CompareExchange<List<LoadEntity>>(ref this.children, value, null);
				}
				return this.children;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000858B File Offset: 0x0000678B
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00008593 File Offset: 0x00006793
		public LoadMetricStorage CommittedLoad
		{
			get
			{
				return this.committedLoad;
			}
			set
			{
				this.committedLoad = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000859C File Offset: 0x0000679C
		// (set) Token: 0x06000298 RID: 664 RVA: 0x000085A4 File Offset: 0x000067A4
		[DataMember]
		public IAllocationConstraint Constraint { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000085AD File Offset: 0x000067AD
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000085B5 File Offset: 0x000067B5
		[DataMember]
		public ContainerType ContainerType { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000085BE File Offset: 0x000067BE
		// (set) Token: 0x0600029C RID: 668 RVA: 0x000085C6 File Offset: 0x000067C6
		[DataMember]
		public ExDateTime DataRetrievedTimestamp { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000085D0 File Offset: 0x000067D0
		// (set) Token: 0x0600029E RID: 670 RVA: 0x000085EB File Offset: 0x000067EB
		[DataMember(EmitDefaultValue = true, IsRequired = false, Order = 1)]
		public DateTime DataRetrievedTimestampUtc
		{
			get
			{
				return this.DataRetrievedTimestamp.UniversalTime;
			}
			set
			{
				this.DataRetrievedTimestamp = (ExDateTime)value.ToUniversalTime();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000085FF File Offset: 0x000067FF
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00008607 File Offset: 0x00006807
		public LoadMetricStorage MaximumLoad
		{
			get
			{
				return this.maximumLoad;
			}
			set
			{
				this.maximumLoad = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00008610 File Offset: 0x00006810
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00008618 File Offset: 0x00006818
		[DataMember]
		public int RelativeLoadWeight { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008621 File Offset: 0x00006821
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00008629 File Offset: 0x00006829
		public LoadMetricStorage ReusableCapacity
		{
			get
			{
				return this.reusableCapacity;
			}
			set
			{
				this.reusableCapacity = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		public LoadEntity this[Guid objectGuid]
		{
			get
			{
				LoadEntity result;
				lock (this.Children.GetSyncRoot<LoadEntity>())
				{
					result = this.Children.FirstOrDefault((LoadEntity child) => child.Guid == objectGuid);
				}
				return result;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000086C0 File Offset: 0x000068C0
		public override void Accept(ILoadEntityVisitor visitor)
		{
			if (!visitor.Visit(this))
			{
				return;
			}
			lock (this.Children.GetSyncRoot<LoadEntity>())
			{
				foreach (LoadEntity loadEntity in this.Children)
				{
					loadEntity.Accept(visitor);
				}
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000876C File Offset: 0x0000696C
		public void AddChild(LoadEntity child)
		{
			lock (this.Children.GetSyncRoot<LoadEntity>())
			{
				if (this.Children.All((LoadEntity le) => le.Guid != child.Guid))
				{
					this.Children.Add(child);
					child.Parent = this;
				}
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000087F8 File Offset: 0x000069F8
		public override void ConvertFromSerializationFormat()
		{
			base.ConsumedLoad.ConvertFromSerializationFormat();
			this.MaximumLoad.ConvertFromSerializationFormat();
			this.ReusableCapacity.ConvertFromSerializationFormat();
			this.CommittedLoad.ConvertFromSerializationFormat();
			foreach (LoadEntity loadEntity in this.Children)
			{
				loadEntity.ConvertFromSerializationFormat();
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00008890 File Offset: 0x00006A90
		public override long GetAggregateConsumedLoad(LoadMetric metric)
		{
			long num = base.ConsumedLoad[metric];
			if (this.children.Count == 0)
			{
				return num;
			}
			return num + this.Children.Sum((LoadEntity child) => child.GetAggregateConsumedLoad(metric));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000088FC File Offset: 0x00006AFC
		public long GetAggregateMaximumLoad(LoadMetric metric)
		{
			long num = this.MaximumLoad[metric];
			if (this.children.Count == 0)
			{
				return num;
			}
			return num + this.Children.OfType<LoadContainer>().Sum((LoadContainer child) => child.GetAggregateMaximumLoad(metric));
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00008958 File Offset: 0x00006B58
		public LoadContainer GetShallowCopy()
		{
			LoadContainer loadContainer = new LoadContainer(base.DirectoryObject, this.ContainerType);
			loadContainer.MaximumLoad = new LoadMetricStorage(this.MaximumLoad);
			loadContainer.ConsumedLoad = new LoadMetricStorage(base.ConsumedLoad);
			loadContainer.ReusableCapacity = new LoadMetricStorage(this.ReusableCapacity);
			loadContainer.CommittedLoad = new LoadMetricStorage(this.CommittedLoad);
			loadContainer.RelativeLoadWeight = this.RelativeLoadWeight;
			loadContainer.DataRetrievedTimestamp = this.DataRetrievedTimestamp;
			loadContainer.CanAcceptBalancingLoad = this.CanAcceptBalancingLoad;
			loadContainer.CanAcceptRegularLoad = this.CanAcceptRegularLoad;
			loadContainer.Constraint = this.Constraint.CloneForContainer(loadContainer);
			return loadContainer;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008A1C File Offset: 0x00006C1C
		public void RemoveChild(Guid guid)
		{
			lock (this.Children.GetSyncRoot<LoadEntity>())
			{
				this.Children.RemoveAll((LoadEntity child) => child.Guid == guid);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00008A88 File Offset: 0x00006C88
		public HeatMapCapacityData ToCapacityData()
		{
			DirectoryIdentity directoryObjectIdentity = base.DirectoryObjectIdentity;
			long aggregateConsumedLoad = this.GetAggregateConsumedLoad(ConsumerMailboxSize.Instance);
			long value = this.GetAggregateConsumedLoad(PhysicalSize.Instance) - aggregateConsumedLoad;
			long aggregateMaximumLoad = this.GetAggregateMaximumLoad(PhysicalSize.Instance);
			long aggregateConsumedLoad2 = this.GetAggregateConsumedLoad(LogicalSize.Instance);
			long aggregateConsumedLoad3 = this.GetAggregateConsumedLoad(ConsumerMailboxCount.Instance);
			HeatMapCapacityData heatMapCapacityData = new HeatMapCapacityData();
			heatMapCapacityData.TotalCapacity = PhysicalSize.Instance.ToByteQuantifiedSize(aggregateMaximumLoad);
			heatMapCapacityData.ConsumerSize = ConsumerMailboxSize.Instance.ToByteQuantifiedSize(aggregateConsumedLoad);
			heatMapCapacityData.OrganizationSize = PhysicalSize.Instance.ToByteQuantifiedSize(value);
			heatMapCapacityData.LogicalSize = LogicalSize.Instance.ToByteQuantifiedSize(aggregateConsumedLoad2);
			heatMapCapacityData.Identity = directoryObjectIdentity;
			heatMapCapacityData.TotalMailboxCount = aggregateConsumedLoad3;
			heatMapCapacityData.RetrievedTimestamp = this.DataRetrievedTimestampUtc;
			heatMapCapacityData.LoadMetrics = new LoadMetricStorage();
			foreach (LoadMetric metric in base.ConsumedLoad.Metrics)
			{
				heatMapCapacityData.LoadMetrics[metric] = this.GetAggregateConsumedLoad(metric);
			}
			return heatMapCapacityData;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public override LoadEntity ToSerializationFormat(bool convertBandToBandData)
		{
			LoadContainer shallowCopy = this.GetShallowCopy();
			shallowCopy.ConsumedLoad.ConvertToSerializationMetrics(convertBandToBandData);
			shallowCopy.ReusableCapacity.ConvertToSerializationMetrics(convertBandToBandData);
			shallowCopy.MaximumLoad.ConvertToSerializationMetrics(convertBandToBandData);
			shallowCopy.CommittedLoad.ConvertToSerializationMetrics(convertBandToBandData);
			foreach (LoadEntity loadEntity in this.Children)
			{
				shallowCopy.AddChild(loadEntity.ToSerializationFormat(convertBandToBandData));
			}
			return shallowCopy;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008C48 File Offset: 0x00006E48
		public override string ToString()
		{
			return string.Format("LoadContainer{{Name={0},GUID={1},Type={2},ConsumedLoad={3}}}", new object[]
			{
				base.Name,
				base.Guid,
				this.ContainerType,
				base.ConsumedLoad
			});
		}

		// Token: 0x040000BA RID: 186
		[DataMember]
		private bool? canAcceptBalancingLoad;

		// Token: 0x040000BB RID: 187
		[DataMember]
		private bool? canAcceptRegularLoad;

		// Token: 0x040000BC RID: 188
		[DataMember]
		private List<LoadEntity> children;

		// Token: 0x040000BD RID: 189
		[DataMember]
		private LoadMetricStorage committedLoad;

		// Token: 0x040000BE RID: 190
		[DataMember]
		private LoadMetricStorage maximumLoad;

		// Token: 0x040000BF RID: 191
		[DataMember]
		private LoadMetricStorage reusableCapacity;
	}
}
