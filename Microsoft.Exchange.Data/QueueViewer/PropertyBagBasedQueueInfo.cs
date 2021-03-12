using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	public class PropertyBagBasedQueueInfo : ExtensibleQueueInfo
	{
		// Token: 0x060016B7 RID: 5815 RVA: 0x0004712C File Offset: 0x0004532C
		internal PropertyBagBasedQueueInfo(QueueIdentity identity) : base(new QueueInfoPropertyBag())
		{
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = identity;
			this.NextHopDomain = identity.NextHopDomain;
			this.propertyBag[ExtensibleQueueInfoSchema.PriorityDescriptions] = base.PriorityDescriptions;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00047178 File Offset: 0x00045378
		private PropertyBagBasedQueueInfo(PropertyStreamReader reader) : base(new QueueInfoPropertyBag())
		{
			KeyValuePair<string, object> item;
			reader.Read(out item);
			if (!string.Equals("NumProperties", item.Key, StringComparison.OrdinalIgnoreCase))
			{
				throw new SerializationException(string.Format("Cannot deserialize PropertyBagBasedQueueInfo. Expected property NumProperties, but found '{0}'", item.Key));
			}
			int value = PropertyStreamReader.GetValue<int>(item);
			for (int i = 0; i < value; i++)
			{
				reader.Read(out item);
				if (string.Equals(ExtensibleQueueInfoSchema.Identity.Name, item.Key, StringComparison.OrdinalIgnoreCase))
				{
					QueueIdentity value2 = QueueIdentity.Parse(PropertyStreamReader.GetValue<string>(item));
					this[this.propertyBag.ObjectIdentityPropertyDefinition] = value2;
				}
				else if (string.Equals(ExtensibleQueueInfoSchema.DeliveryType.Name, item.Key, StringComparison.OrdinalIgnoreCase))
				{
					DeliveryType deliveryType = (DeliveryType)PropertyStreamReader.GetValue<int>(item);
					this.propertyBag[ExtensibleQueueInfoSchema.DeliveryType] = deliveryType;
				}
				else if (string.Equals(ExtensibleQueueInfoSchema.Status.Name, item.Key, StringComparison.OrdinalIgnoreCase))
				{
					QueueStatus value3 = (QueueStatus)PropertyStreamReader.GetValue<int>(item);
					this.propertyBag[ExtensibleQueueInfoSchema.Status] = value3;
				}
				else if (string.Equals(ExtensibleQueueInfoSchema.RiskLevel.Name, item.Key, StringComparison.OrdinalIgnoreCase))
				{
					RiskLevel value4 = (RiskLevel)PropertyStreamReader.GetValue<int>(item);
					this.propertyBag[ExtensibleQueueInfoSchema.RiskLevel] = value4;
				}
				else if (string.Equals(ExtensibleQueueInfoSchema.NextHopCategory.Name, item.Key, StringComparison.OrdinalIgnoreCase))
				{
					NextHopCategory value5 = (NextHopCategory)PropertyStreamReader.GetValue<int>(item);
					this.propertyBag[ExtensibleQueueInfoSchema.NextHopCategory] = value5;
				}
				else
				{
					PropertyDefinition fieldByName = PropertyBagBasedQueueInfo.schema.GetFieldByName(item.Key);
					if (fieldByName != null)
					{
						this.propertyBag.SetField((QueueViewerPropertyDefinition<ExtensibleQueueInfo>)fieldByName, item.Value);
					}
					else
					{
						ExTraceGlobals.SerializationTracer.TraceWarning<string>(0L, "Cannot convert key index '{0}' into a property in the ExtensibleQueueInfo schema", item.Key);
					}
				}
			}
			if (this.propertyBag[ExtensibleQueueInfoSchema.NextHopDomain] != null)
			{
				QueueIdentity queueIdentity = (QueueIdentity)this[this.propertyBag.ObjectIdentityPropertyDefinition];
				queueIdentity.NextHopDomain = (string)this.propertyBag[ExtensibleQueueInfoSchema.NextHopDomain];
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000473A5 File Offset: 0x000455A5
		public override bool IsDeliveryQueue()
		{
			return base.QueueIdentity.Type == QueueType.Delivery;
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000473B5 File Offset: 0x000455B5
		public override bool IsSubmissionQueue()
		{
			return base.QueueIdentity.Type == QueueType.Submission;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x000473C5 File Offset: 0x000455C5
		public override bool IsPoisonQueue()
		{
			return base.QueueIdentity.Type == QueueType.Poison;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x000473D5 File Offset: 0x000455D5
		public override bool IsShadowQueue()
		{
			return base.QueueIdentity.Type == QueueType.Shadow;
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000473E5 File Offset: 0x000455E5
		// (set) Token: 0x060016BE RID: 5822 RVA: 0x000473FC File Offset: 0x000455FC
		public override DeliveryType DeliveryType
		{
			get
			{
				return (DeliveryType)this.propertyBag[ExtensibleQueueInfoSchema.DeliveryType];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.DeliveryType] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00047414 File Offset: 0x00045614
		// (set) Token: 0x060016C0 RID: 5824 RVA: 0x0004742B File Offset: 0x0004562B
		public override string NextHopDomain
		{
			get
			{
				return (string)this.propertyBag[ExtensibleQueueInfoSchema.NextHopDomain];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.NextHopDomain] = value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060016C1 RID: 5825 RVA: 0x0004743E File Offset: 0x0004563E
		// (set) Token: 0x060016C2 RID: 5826 RVA: 0x00047455 File Offset: 0x00045655
		public override string TlsDomain
		{
			get
			{
				return (string)this.propertyBag[ExtensibleQueueInfoSchema.TlsDomain];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.TlsDomain] = value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060016C3 RID: 5827 RVA: 0x00047468 File Offset: 0x00045668
		// (set) Token: 0x060016C4 RID: 5828 RVA: 0x0004747F File Offset: 0x0004567F
		public override Guid NextHopConnector
		{
			get
			{
				return (Guid)this.propertyBag[ExtensibleQueueInfoSchema.NextHopConnector];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.NextHopConnector] = value;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060016C5 RID: 5829 RVA: 0x00047497 File Offset: 0x00045697
		// (set) Token: 0x060016C6 RID: 5830 RVA: 0x000474AE File Offset: 0x000456AE
		public override QueueStatus Status
		{
			get
			{
				return (QueueStatus)this.propertyBag[ExtensibleQueueInfoSchema.Status];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.Status] = value;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000474C6 File Offset: 0x000456C6
		// (set) Token: 0x060016C8 RID: 5832 RVA: 0x000474DD File Offset: 0x000456DD
		public override int MessageCount
		{
			get
			{
				return (int)this.propertyBag[ExtensibleQueueInfoSchema.MessageCount];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.MessageCount] = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000474F5 File Offset: 0x000456F5
		// (set) Token: 0x060016CA RID: 5834 RVA: 0x0004750C File Offset: 0x0004570C
		public override string LastError
		{
			get
			{
				return (string)this.propertyBag[ExtensibleQueueInfoSchema.LastError];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.LastError] = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x0004751F File Offset: 0x0004571F
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x00047536 File Offset: 0x00045736
		public override int RetryCount
		{
			get
			{
				return (int)this.propertyBag[ExtensibleQueueInfoSchema.RetryCount];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.RetryCount] = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x0004754E File Offset: 0x0004574E
		// (set) Token: 0x060016CE RID: 5838 RVA: 0x00047565 File Offset: 0x00045765
		public override DateTime? LastRetryTime
		{
			get
			{
				return (DateTime?)this.propertyBag[ExtensibleQueueInfoSchema.LastRetryTime];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.LastRetryTime] = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0004757D File Offset: 0x0004577D
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x00047594 File Offset: 0x00045794
		public override DateTime? NextRetryTime
		{
			get
			{
				return (DateTime?)this.propertyBag[ExtensibleQueueInfoSchema.NextRetryTime];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.NextRetryTime] = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000475AC File Offset: 0x000457AC
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x000475C3 File Offset: 0x000457C3
		public override DateTime? FirstRetryTime
		{
			get
			{
				return (DateTime?)this.propertyBag[ExtensibleQueueInfoSchema.FirstRetryTime];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.FirstRetryTime] = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000475DB File Offset: 0x000457DB
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x000475F2 File Offset: 0x000457F2
		public override int DeferredMessageCount
		{
			get
			{
				return (int)this.propertyBag[ExtensibleQueueInfoSchema.DeferredMessageCount];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.DeferredMessageCount] = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0004760A File Offset: 0x0004580A
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x00047621 File Offset: 0x00045821
		public override int LockedMessageCount
		{
			get
			{
				return (int)this.propertyBag[ExtensibleQueueInfoSchema.LockedMessageCount];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.LockedMessageCount] = value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x00047639 File Offset: 0x00045839
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x00047650 File Offset: 0x00045850
		public override int[] MessageCountsPerPriority
		{
			get
			{
				return (int[])this.propertyBag[ExtensibleQueueInfoSchema.MessageCountsPerPriority];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.MessageCountsPerPriority] = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00047663 File Offset: 0x00045863
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x0004767A File Offset: 0x0004587A
		public override int[] DeferredMessageCountsPerPriority
		{
			get
			{
				return (int[])this.propertyBag[ExtensibleQueueInfoSchema.DeferredMessageCountsPerPriority];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.DeferredMessageCountsPerPriority] = value;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0004768D File Offset: 0x0004588D
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x000476A4 File Offset: 0x000458A4
		public override RiskLevel RiskLevel
		{
			get
			{
				return (RiskLevel)this.propertyBag[ExtensibleQueueInfoSchema.RiskLevel];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.RiskLevel] = value;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x000476BC File Offset: 0x000458BC
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x000476D3 File Offset: 0x000458D3
		public override int OutboundIPPool
		{
			get
			{
				return (int)this.propertyBag[ExtensibleQueueInfoSchema.OutboundIPPool];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.OutboundIPPool] = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x000476EB File Offset: 0x000458EB
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x00047702 File Offset: 0x00045902
		public override NextHopCategory NextHopCategory
		{
			get
			{
				return (NextHopCategory)this.propertyBag[ExtensibleQueueInfoSchema.NextHopCategory];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.NextHopCategory] = value;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0004771A File Offset: 0x0004591A
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x00047731 File Offset: 0x00045931
		public override double IncomingRate
		{
			get
			{
				return (double)this.propertyBag[ExtensibleQueueInfoSchema.IncomingRate];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.IncomingRate] = value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00047749 File Offset: 0x00045949
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x00047760 File Offset: 0x00045960
		public override double OutgoingRate
		{
			get
			{
				return (double)this.propertyBag[ExtensibleQueueInfoSchema.OutgoingRate];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.OutgoingRate] = value;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00047778 File Offset: 0x00045978
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0004778F File Offset: 0x0004598F
		public override double Velocity
		{
			get
			{
				return (double)this.propertyBag[ExtensibleQueueInfoSchema.Velocity];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.Velocity] = value;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x000477A7 File Offset: 0x000459A7
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x000477BE File Offset: 0x000459BE
		public override string OverrideSource
		{
			get
			{
				return (string)this.propertyBag[ExtensibleQueueInfoSchema.OverrideSource];
			}
			internal set
			{
				this.propertyBag[ExtensibleQueueInfoSchema.OverrideSource] = value;
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x000477D1 File Offset: 0x000459D1
		internal static PropertyBagBasedQueueInfo CreateFromByteStream(PropertyStreamReader reader)
		{
			return new PropertyBagBasedQueueInfo(reader);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x000477DC File Offset: 0x000459DC
		internal void ToByteArray(ref byte[] bytes, ref int offset)
		{
			bool flag = this.DeferredMessageCountsPerPriority != null && this.DeferredMessageCountsPerPriority.Length > 0;
			bool flag2 = this.MessageCountsPerPriority != null && this.MessageCountsPerPriority.Length > 0;
			int num = 17 + ((this.LastRetryTime != null) ? 1 : 0) + ((this.NextRetryTime != null) ? 1 : 0) + ((this.FirstRetryTime != null) ? 1 : 0) + (flag ? 1 : 0) + (flag2 ? 1 : 0) + ((flag2 || flag) ? 1 : 0) + (string.IsNullOrEmpty(this.OverrideSource) ? 0 : 1);
			int num2 = 0;
			PropertyStreamWriter.WritePropertyKeyValue("NumProperties", StreamPropertyType.Int32, num, ref bytes, ref offset);
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.Identity.Name, StreamPropertyType.String, this.Identity.ToString(), ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.DeliveryType.Name, StreamPropertyType.Int32, (int)this.DeliveryType, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.NextHopDomain.Name, StreamPropertyType.String, this.NextHopDomain, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.NextHopConnector.Name, StreamPropertyType.Guid, this.NextHopConnector, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.Status.Name, StreamPropertyType.Int32, (int)this.Status, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.MessageCount.Name, StreamPropertyType.Int32, this.MessageCount, ref bytes, ref offset);
			num2++;
			if (flag2)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.MessageCountsPerPriority.Name, StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array, this.MessageCountsPerPriority, ref bytes, ref offset);
				num2++;
			}
			if (flag || flag2)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.PriorityDescriptions.Name, StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.UInt32 | StreamPropertyType.Array, base.PriorityDescriptions, ref bytes, ref offset);
				num2++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.LastError.Name, StreamPropertyType.String, this.LastError, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.RetryCount.Name, StreamPropertyType.Int32, this.RetryCount, ref bytes, ref offset);
			num2++;
			if (this.LastRetryTime != null)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.LastRetryTime.Name, StreamPropertyType.DateTime, this.LastRetryTime.Value, ref bytes, ref offset);
				num2++;
			}
			if (this.NextRetryTime != null)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.NextRetryTime.Name, StreamPropertyType.DateTime, this.NextRetryTime.Value, ref bytes, ref offset);
				num2++;
			}
			if (this.FirstRetryTime != null)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.FirstRetryTime.Name, StreamPropertyType.DateTime, this.FirstRetryTime.Value, ref bytes, ref offset);
				num2++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.TlsDomain.Name, StreamPropertyType.String, this.TlsDomain, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.DeferredMessageCount.Name, StreamPropertyType.Int32, this.DeferredMessageCount, ref bytes, ref offset);
			num2++;
			if (flag)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.DeferredMessageCountsPerPriority.Name, StreamPropertyType.Null | StreamPropertyType.Bool | StreamPropertyType.SByte | StreamPropertyType.Array, this.DeferredMessageCountsPerPriority, ref bytes, ref offset);
				num2++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.LockedMessageCount.Name, StreamPropertyType.Int32, this.LockedMessageCount, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.RiskLevel.Name, StreamPropertyType.Int32, (int)this.RiskLevel, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.OutboundIPPool.Name, StreamPropertyType.Int32, this.OutboundIPPool, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.NextHopCategory.Name, StreamPropertyType.Int32, (int)this.NextHopCategory, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.IncomingRate.Name, StreamPropertyType.Double, this.IncomingRate, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.OutgoingRate.Name, StreamPropertyType.Double, this.OutgoingRate, ref bytes, ref offset);
			num2++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.Velocity.Name, StreamPropertyType.Double, this.Velocity, ref bytes, ref offset);
			num2++;
			if (!string.IsNullOrEmpty(this.OverrideSource))
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleQueueInfoSchema.OverrideSource.Name, StreamPropertyType.String, this.OverrideSource, ref bytes, ref offset);
				num2++;
			}
		}

		// Token: 0x04000D25 RID: 3365
		private const string NumPropertiesKey = "NumProperties";

		// Token: 0x04000D26 RID: 3366
		private static ExtensibleQueueInfoSchema schema = ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>();
	}
}
