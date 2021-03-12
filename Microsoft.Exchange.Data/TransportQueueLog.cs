using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Security.Compliance;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200028E RID: 654
	[Serializable]
	public class TransportQueueLog : ConfigurableObject
	{
		// Token: 0x060017A9 RID: 6057 RVA: 0x0004A388 File Offset: 0x00048588
		public TransportQueueLog() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0004A398 File Offset: 0x00048598
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.QueueId.ToString());
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0004A3BE File Offset: 0x000485BE
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x0004A3D0 File Offset: 0x000485D0
		[XmlAttribute]
		public Guid QueueId
		{
			get
			{
				return (Guid)this[TransportQueueLogSchema.QueueIdProperty];
			}
			set
			{
				this[TransportQueueLogSchema.QueueIdProperty] = value;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0004A3E3 File Offset: 0x000485E3
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x0004A3F5 File Offset: 0x000485F5
		[XmlAttribute]
		public string QueueName
		{
			get
			{
				return (string)this[TransportQueueLogSchema.QueueNameProperty];
			}
			set
			{
				this[TransportQueueLogSchema.QueueNameProperty] = value;
				this.QueueId = TransportQueueLog.GenerateGuidFromString(value);
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0004A40F File Offset: 0x0004860F
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x0004A421 File Offset: 0x00048621
		[XmlAttribute]
		public DateTime SnapshotDatetime
		{
			get
			{
				return (DateTime)this[TransportQueueLogSchema.SnapshotDatetimeProperty];
			}
			set
			{
				this[TransportQueueLogSchema.SnapshotDatetimeProperty] = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0004A434 File Offset: 0x00048634
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x0004A446 File Offset: 0x00048646
		[XmlAttribute]
		public string TlsDomain
		{
			get
			{
				return (string)this[TransportQueueLogSchema.TlsDomainProperty];
			}
			set
			{
				this[TransportQueueLogSchema.TlsDomainProperty] = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0004A454 File Offset: 0x00048654
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x0004A466 File Offset: 0x00048666
		[XmlAttribute]
		public string NextHopDomain
		{
			get
			{
				return (string)this[TransportQueueLogSchema.NextHopDomainProperty];
			}
			set
			{
				this[TransportQueueLogSchema.NextHopDomainProperty] = value;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0004A474 File Offset: 0x00048674
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x0004A486 File Offset: 0x00048686
		[XmlAttribute]
		public string NextHopKey
		{
			get
			{
				return (string)this[TransportQueueLogSchema.NextHopKeyProperty];
			}
			set
			{
				this[TransportQueueLogSchema.NextHopKeyProperty] = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0004A494 File Offset: 0x00048694
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x0004A4A6 File Offset: 0x000486A6
		[XmlAttribute]
		public Guid NextHopConnector
		{
			get
			{
				return (Guid)this[TransportQueueLogSchema.NextHopConnectorProperty];
			}
			set
			{
				this[TransportQueueLogSchema.NextHopConnectorProperty] = value;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0004A4B9 File Offset: 0x000486B9
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x0004A4CB File Offset: 0x000486CB
		[XmlAttribute]
		public string NextHopCategory
		{
			get
			{
				return (string)this[TransportQueueLogSchema.NextHopCategoryProperty];
			}
			set
			{
				this[TransportQueueLogSchema.NextHopCategoryProperty] = value;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0004A4D9 File Offset: 0x000486D9
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x0004A4EB File Offset: 0x000486EB
		[XmlAttribute]
		public string DeliveryType
		{
			get
			{
				return (string)this[TransportQueueLogSchema.DeliveryTypeProperty];
			}
			set
			{
				this[TransportQueueLogSchema.DeliveryTypeProperty] = value;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0004A4F9 File Offset: 0x000486F9
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x0004A50B File Offset: 0x0004870B
		[XmlAttribute]
		public string RiskLevel
		{
			get
			{
				return (string)this[TransportQueueLogSchema.RiskLevelProperty];
			}
			set
			{
				this[TransportQueueLogSchema.RiskLevelProperty] = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0004A519 File Offset: 0x00048719
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x0004A52B File Offset: 0x0004872B
		[XmlAttribute]
		public string OutboundIPPool
		{
			get
			{
				return (string)this[TransportQueueLogSchema.OutboundIPPoolProperty];
			}
			set
			{
				this[TransportQueueLogSchema.OutboundIPPoolProperty] = value;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0004A539 File Offset: 0x00048739
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x0004A54B File Offset: 0x0004874B
		[XmlAttribute]
		public string Status
		{
			get
			{
				return (string)this[TransportQueueLogSchema.StatusProperty];
			}
			set
			{
				this[TransportQueueLogSchema.StatusProperty] = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0004A559 File Offset: 0x00048759
		// (set) Token: 0x060017C4 RID: 6084 RVA: 0x0004A56B File Offset: 0x0004876B
		[XmlAttribute]
		public string LastError
		{
			get
			{
				return (string)this[TransportQueueLogSchema.LastErrorProperty];
			}
			set
			{
				this[TransportQueueLogSchema.LastErrorProperty] = value;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0004A579 File Offset: 0x00048779
		// (set) Token: 0x060017C6 RID: 6086 RVA: 0x0004A58B File Offset: 0x0004878B
		[XmlAttribute]
		public int MessageCount
		{
			get
			{
				return (int)this[TransportQueueLogSchema.MessageCountProperty];
			}
			set
			{
				this[TransportQueueLogSchema.MessageCountProperty] = value;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0004A59E File Offset: 0x0004879E
		// (set) Token: 0x060017C8 RID: 6088 RVA: 0x0004A5B0 File Offset: 0x000487B0
		[XmlAttribute]
		public int DeferredMessageCount
		{
			get
			{
				return (int)this[TransportQueueLogSchema.DeferredMessageCountProperty];
			}
			set
			{
				this[TransportQueueLogSchema.DeferredMessageCountProperty] = value;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0004A5C3 File Offset: 0x000487C3
		// (set) Token: 0x060017CA RID: 6090 RVA: 0x0004A5D5 File Offset: 0x000487D5
		[XmlAttribute]
		public int LockedMessageCount
		{
			get
			{
				return (int)this[TransportQueueLogSchema.LockedMessageCountProperty];
			}
			set
			{
				this[TransportQueueLogSchema.LockedMessageCountProperty] = value;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0004A5E8 File Offset: 0x000487E8
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x0004A5FA File Offset: 0x000487FA
		[XmlAttribute]
		public double IncomingRate
		{
			get
			{
				return (double)this[TransportQueueLogSchema.IncomingRateProperty];
			}
			set
			{
				this[TransportQueueLogSchema.IncomingRateProperty] = value;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0004A60D File Offset: 0x0004880D
		// (set) Token: 0x060017CE RID: 6094 RVA: 0x0004A61F File Offset: 0x0004881F
		[XmlAttribute]
		public double OutgoingRate
		{
			get
			{
				return (double)this[TransportQueueLogSchema.OutgoingRateProperty];
			}
			set
			{
				this[TransportQueueLogSchema.OutgoingRateProperty] = value;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0004A632 File Offset: 0x00048832
		// (set) Token: 0x060017D0 RID: 6096 RVA: 0x0004A644 File Offset: 0x00048844
		[XmlAttribute]
		public double Velocity
		{
			get
			{
				return (double)this[TransportQueueLogSchema.VelocityProperty];
			}
			set
			{
				this[TransportQueueLogSchema.VelocityProperty] = value;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x0004A657 File Offset: 0x00048857
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TransportQueueLog.SchemaObject;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0004A65E File Offset: 0x0004885E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0004A67C File Offset: 0x0004887C
		public static MultiValuedProperty<TransportQueueLog> Parse(string stringXml)
		{
			MultiValuedProperty<TransportQueueLog> queueLogs = new MultiValuedProperty<TransportQueueLog>();
			if (!string.IsNullOrWhiteSpace(stringXml))
			{
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(stringXml), new XmlReaderSettings
				{
					ConformanceLevel = ConformanceLevel.Fragment
				}))
				{
					TransportQueueLogs transportQueueLogs = (TransportQueueLogs)TransportQueueLogs.Serializer.Deserialize(xmlReader);
					if (transportQueueLogs != null && transportQueueLogs.Count > 0)
					{
						transportQueueLogs.ForEach(delegate(TransportQueueLog i)
						{
							queueLogs.Add(i);
						});
					}
				}
			}
			return queueLogs;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0004A718 File Offset: 0x00048918
		private static Guid GenerateGuidFromString(string inputString)
		{
			Guid result;
			using (MessageDigestForNonCryptographicPurposes messageDigestForNonCryptographicPurposes = new MessageDigestForNonCryptographicPurposes())
			{
				result = new Guid(messageDigestForNonCryptographicPurposes.ComputeHash(Encoding.Default.GetBytes(inputString)));
			}
			return result;
		}

		// Token: 0x04000DC6 RID: 3526
		private static readonly TransportQueueLogSchema SchemaObject = ObjectSchema.GetInstance<TransportQueueLogSchema>();
	}
}
