using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Data;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000280 RID: 640
	[Serializable]
	public class PropertyBagBasedMessageInfo : ExtensibleMessageInfo
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x000462C4 File Offset: 0x000444C4
		internal PropertyBagBasedMessageInfo(long identity, QueueIdentity queueIdentity) : base(identity, queueIdentity, new MessageInfoPropertyBag())
		{
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x000462D4 File Offset: 0x000444D4
		private PropertyBagBasedMessageInfo(PropertyStreamReader reader, Version sourceVersion) : base(new MessageInfoPropertyBag())
		{
			KeyValuePair<string, object> keyValuePair;
			reader.Read(out keyValuePair);
			if (!string.Equals("NumProperties", keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
			{
				throw new SerializationException(string.Format("Cannot deserialize PropertyBagBasedMessageInfo. Expected property NumProperties, but found property '{0}'", keyValuePair.Key));
			}
			int value = PropertyStreamReader.GetValue<int>(keyValuePair);
			for (int i = 0; i < value; i++)
			{
				reader.Read(out keyValuePair);
				if (string.Equals(ExtensibleMessageInfoSchema.Identity.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					MessageIdentity value2 = MessageIdentity.Create(sourceVersion, keyValuePair, reader);
					this.propertyBag[ExtensibleMessageInfoSchema.Identity] = value2;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.Status.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					MessageStatus value3 = (MessageStatus)PropertyStreamReader.GetValue<int>(keyValuePair);
					this.propertyBag[ExtensibleMessageInfoSchema.Status] = value3;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.Size.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					ByteQuantifiedSize byteQuantifiedSize = new ByteQuantifiedSize(PropertyStreamReader.GetValue<ulong>(keyValuePair));
					this.propertyBag[ExtensibleMessageInfoSchema.Size] = byteQuantifiedSize;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.MessageLatency.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					EnhancedTimeSpan enhancedTimeSpan = EnhancedTimeSpan.Parse(PropertyStreamReader.GetValue<string>(keyValuePair));
					this.propertyBag[ExtensibleMessageInfoSchema.MessageLatency] = enhancedTimeSpan;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.ExternalDirectoryOrganizationId.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					Guid value4 = PropertyStreamReader.GetValue<Guid>(keyValuePair);
					this.propertyBag[ExtensibleMessageInfoSchema.ExternalDirectoryOrganizationId] = value4;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.Directionality.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					MailDirectionality value5 = (MailDirectionality)PropertyStreamReader.GetValue<int>(keyValuePair);
					this.propertyBag[ExtensibleMessageInfoSchema.Directionality] = value5;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.Recipients.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					int value6 = PropertyStreamReader.GetValue<int>(keyValuePair);
					RecipientInfo[] array = new RecipientInfo[value6];
					for (int j = 0; j < value6; j++)
					{
						RecipientInfo recipientInfo = RecipientInfo.Create(reader);
						array[j] = recipientInfo;
					}
					this.propertyBag[ExtensibleMessageInfoSchema.Recipients] = array;
				}
				else if (string.Equals(ExtensibleMessageInfoSchema.ComponentLatency.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
				{
					int value7 = PropertyStreamReader.GetValue<int>(keyValuePair);
					ComponentLatencyInfo[] array2 = new ComponentLatencyInfo[value7];
					for (int k = 0; k < value7; k++)
					{
						ComponentLatencyInfo componentLatencyInfo = ComponentLatencyInfo.Create(reader);
						array2[k] = componentLatencyInfo;
					}
					this.propertyBag[ExtensibleMessageInfoSchema.ComponentLatency] = array2;
				}
				else
				{
					PropertyDefinition fieldByName = PropertyBagBasedMessageInfo.schema.GetFieldByName(keyValuePair.Key);
					if (fieldByName != null)
					{
						this.propertyBag.SetField((QueueViewerPropertyDefinition<ExtensibleMessageInfo>)fieldByName, keyValuePair.Value);
					}
					else
					{
						ExTraceGlobals.SerializationTracer.TraceWarning<string>(0L, "Cannot convert key index '{0}' into a property in the ExtensibleMessageInfo schema", keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000465BF File Offset: 0x000447BF
		// (set) Token: 0x0600164C RID: 5708 RVA: 0x000465D1 File Offset: 0x000447D1
		public override string Subject
		{
			get
			{
				return (string)this[ExtensibleMessageInfoSchema.Subject];
			}
			internal set
			{
				this[ExtensibleMessageInfoSchema.Subject] = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000465DF File Offset: 0x000447DF
		// (set) Token: 0x0600164E RID: 5710 RVA: 0x000465F6 File Offset: 0x000447F6
		public override string InternetMessageId
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.InternetMessageId];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.InternetMessageId] = value;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00046609 File Offset: 0x00044809
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0004661B File Offset: 0x0004481B
		public override string FromAddress
		{
			get
			{
				return (string)this[ExtensibleMessageInfoSchema.FromAddress];
			}
			internal set
			{
				this[ExtensibleMessageInfoSchema.FromAddress] = value;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00046629 File Offset: 0x00044829
		// (set) Token: 0x06001652 RID: 5714 RVA: 0x00046640 File Offset: 0x00044840
		public override MessageStatus Status
		{
			get
			{
				return (MessageStatus)this.propertyBag[ExtensibleMessageInfoSchema.Status];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.Status] = value;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x00046658 File Offset: 0x00044858
		// (set) Token: 0x06001654 RID: 5716 RVA: 0x0004666F File Offset: 0x0004486F
		public override ByteQuantifiedSize Size
		{
			get
			{
				return (ByteQuantifiedSize)this.propertyBag[ExtensibleMessageInfoSchema.Size];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.Size] = value;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00046687 File Offset: 0x00044887
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x0004669E File Offset: 0x0004489E
		public override string MessageSourceName
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.MessageSourceName];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.MessageSourceName] = value;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x000466B1 File Offset: 0x000448B1
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x000466C8 File Offset: 0x000448C8
		public override IPAddress SourceIP
		{
			get
			{
				return (IPAddress)this.propertyBag[ExtensibleMessageInfoSchema.SourceIP];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.SourceIP] = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000466DB File Offset: 0x000448DB
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x000466F2 File Offset: 0x000448F2
		public override int SCL
		{
			get
			{
				return (int)this.propertyBag[ExtensibleMessageInfoSchema.SCL];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.SCL] = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0004670A File Offset: 0x0004490A
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x00046721 File Offset: 0x00044921
		public override DateTime DateReceived
		{
			get
			{
				return (DateTime)this.propertyBag[ExtensibleMessageInfoSchema.DateReceived];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.DateReceived] = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x00046739 File Offset: 0x00044939
		// (set) Token: 0x0600165E RID: 5726 RVA: 0x00046750 File Offset: 0x00044950
		public override DateTime? ExpirationTime
		{
			get
			{
				return (DateTime?)this.propertyBag[ExtensibleMessageInfoSchema.ExpirationTime];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.ExpirationTime] = value;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600165F RID: 5727 RVA: 0x00046768 File Offset: 0x00044968
		// (set) Token: 0x06001660 RID: 5728 RVA: 0x0004677F File Offset: 0x0004497F
		internal override int LastErrorCode
		{
			get
			{
				return (int)this.propertyBag[ExtensibleMessageInfoSchema.LastErrorCode];
			}
			set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.LastErrorCode] = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00046798 File Offset: 0x00044998
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x000467F5 File Offset: 0x000449F5
		public override string LastError
		{
			get
			{
				string text = (string)this.propertyBag[ExtensibleMessageInfoSchema.LastError];
				if (text != null)
				{
					return text;
				}
				if (base.Queue.Type == QueueType.Unreachable)
				{
					return StatusCodeConverter.UnreachableReasonToString((UnreachableReason)this.LastErrorCode);
				}
				if (base.Queue.Type == QueueType.Submission)
				{
					return StatusCodeConverter.DeferReasonToString((DeferReason)this.LastErrorCode);
				}
				return null;
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.LastError] = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001663 RID: 5731 RVA: 0x00046808 File Offset: 0x00044A08
		// (set) Token: 0x06001664 RID: 5732 RVA: 0x0004681F File Offset: 0x00044A1F
		public override int RetryCount
		{
			get
			{
				return (int)this.propertyBag[ExtensibleMessageInfoSchema.RetryCount];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.RetryCount] = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00046837 File Offset: 0x00044A37
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x00046849 File Offset: 0x00044A49
		public override RecipientInfo[] Recipients
		{
			get
			{
				return (RecipientInfo[])this[ExtensibleMessageInfoSchema.Recipients];
			}
			internal set
			{
				this[ExtensibleMessageInfoSchema.Recipients] = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00046857 File Offset: 0x00044A57
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x0004686E File Offset: 0x00044A6E
		public override ComponentLatencyInfo[] ComponentLatency
		{
			get
			{
				return (ComponentLatencyInfo[])this.propertyBag[ExtensibleMessageInfoSchema.ComponentLatency];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.ComponentLatency] = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00046881 File Offset: 0x00044A81
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x00046898 File Offset: 0x00044A98
		public override EnhancedTimeSpan MessageLatency
		{
			get
			{
				return (EnhancedTimeSpan)this.propertyBag[ExtensibleMessageInfoSchema.MessageLatency];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.MessageLatency] = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x000468B0 File Offset: 0x00044AB0
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x000468C7 File Offset: 0x00044AC7
		public override string DeferReason
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.DeferReason];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.DeferReason] = value;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000468DA File Offset: 0x00044ADA
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x000468F1 File Offset: 0x00044AF1
		public override string LockReason
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.LockReason];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.LockReason] = value;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00046904 File Offset: 0x00044B04
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0004691B File Offset: 0x00044B1B
		internal override bool IsProbeMessage
		{
			get
			{
				return (bool)this.propertyBag[ExtensibleMessageInfoSchema.IsProbeMessage];
			}
			set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.IsProbeMessage] = value;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00046933 File Offset: 0x00044B33
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0004694A File Offset: 0x00044B4A
		public override string Priority
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.Priority];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.Priority] = value;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004695D File Offset: 0x00044B5D
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x00046974 File Offset: 0x00044B74
		public override Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)this.propertyBag[ExtensibleMessageInfoSchema.ExternalDirectoryOrganizationId];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.ExternalDirectoryOrganizationId] = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0004698C File Offset: 0x00044B8C
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x000469A3 File Offset: 0x00044BA3
		public override MailDirectionality Directionality
		{
			get
			{
				return (MailDirectionality)this.propertyBag[ExtensibleMessageInfoSchema.Directionality];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.Directionality] = value;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000469BB File Offset: 0x00044BBB
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x000469D2 File Offset: 0x00044BD2
		public override string OriginalFromAddress
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.OriginalFromAddress];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.OriginalFromAddress] = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000469E5 File Offset: 0x00044BE5
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x000469FC File Offset: 0x00044BFC
		public override string AccountForest
		{
			get
			{
				return (string)this.propertyBag[ExtensibleMessageInfoSchema.AccountForest];
			}
			internal set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.AccountForest] = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00046A0F File Offset: 0x00044C0F
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x00046A26 File Offset: 0x00044C26
		internal override int OutboundIPPool
		{
			get
			{
				return (int)this.propertyBag[ExtensibleMessageInfoSchema.OutboundIPPool];
			}
			set
			{
				this.propertyBag[ExtensibleMessageInfoSchema.OutboundIPPool] = value;
			}
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00046A3E File Offset: 0x00044C3E
		internal static PropertyBagBasedMessageInfo CreateFromByteStream(PropertyStreamReader reader, Version sourceVersion)
		{
			return new PropertyBagBasedMessageInfo(reader, sourceVersion);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00046A48 File Offset: 0x00044C48
		internal void ToByteArray(Version targetVersion, ref byte[] bytes, ref int offset)
		{
			int num = 17 + ((!string.IsNullOrEmpty(this.Subject)) ? 1 : 0) + ((this.SourceIP != null) ? 1 : 0) + ((this.ExpirationTime != null) ? 1 : 0) + ((this.DateReceived != DateTime.MinValue) ? 1 : 0);
			int num2;
			if (this.propertyBag.Contains(ExtensibleMessageInfoSchema.MessageLatency))
			{
				EnhancedTimeSpan messageLatency = this.MessageLatency;
				num2 = 1;
			}
			else
			{
				num2 = 0;
			}
			int num3 = num + num2 + ((!string.IsNullOrEmpty(this.LastError)) ? 1 : 0) + ((this.Recipients != null && this.Recipients.Length > 0) ? 1 : 0) + ((this.ComponentLatency != null && this.ComponentLatency.Length > 0) ? 1 : 0) + ((this.OutboundIPPool > 0) ? 1 : 0) + ((this.ExternalDirectoryOrganizationId != Guid.Empty) ? 1 : 0);
			int num4 = 0;
			PropertyStreamWriter.WritePropertyKeyValue("NumProperties", StreamPropertyType.Int32, num3, ref bytes, ref offset);
			num4++;
			base.MessageIdentity.ToByteArray(targetVersion, ref bytes, ref offset);
			num4++;
			if (!string.IsNullOrEmpty(this.Subject))
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Subject.Name, StreamPropertyType.String, this.Subject, ref bytes, ref offset);
				num4++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.InternetMessageId.Name, StreamPropertyType.String, this.InternetMessageId, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.FromAddress.Name, StreamPropertyType.String, this.FromAddress, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Status.Name, StreamPropertyType.Int32, (int)this.Status, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Size.Name, StreamPropertyType.UInt64, this.Size.ToBytes(), ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.MessageSourceName.Name, StreamPropertyType.String, this.MessageSourceName, ref bytes, ref offset);
			num4++;
			if (this.SourceIP != null)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.SourceIP.Name, StreamPropertyType.IPAddress, this.SourceIP, ref bytes, ref offset);
				num4++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.SCL.Name, StreamPropertyType.Int32, this.SCL, ref bytes, ref offset);
			num4++;
			if (this.DateReceived != DateTime.MinValue)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.DateReceived.Name, StreamPropertyType.DateTime, this.DateReceived, ref bytes, ref offset);
				num4++;
			}
			if (this.ExpirationTime != null)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.ExpirationTime.Name, StreamPropertyType.DateTime, this.ExpirationTime, ref bytes, ref offset);
				num4++;
			}
			if (!string.IsNullOrEmpty(this.LastError))
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.LastError.Name, StreamPropertyType.String, this.LastError, ref bytes, ref offset);
				num4++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.LastErrorCode.Name, StreamPropertyType.Int32, this.LastErrorCode, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.RetryCount.Name, StreamPropertyType.Int32, this.RetryCount, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.MessageSourceName.Name, StreamPropertyType.String, this.MessageSourceName, ref bytes, ref offset);
			num4++;
			if (this.propertyBag.Contains(ExtensibleMessageInfoSchema.MessageLatency))
			{
				EnhancedTimeSpan messageLatency2 = this.MessageLatency;
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.MessageLatency.Name, StreamPropertyType.String, this.MessageLatency.ToString(), ref bytes, ref offset);
				num4++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.LockReason.Name, StreamPropertyType.String, this.LockReason, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.IsProbeMessage.Name, StreamPropertyType.Bool, this.IsProbeMessage, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.DeferReason.Name, StreamPropertyType.String, this.DeferReason, ref bytes, ref offset);
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Priority.Name, StreamPropertyType.String, this.Priority, ref bytes, ref offset);
			num4++;
			if (this.OutboundIPPool > 0)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.OutboundIPPool.Name, StreamPropertyType.Int32, this.OutboundIPPool, ref bytes, ref offset);
				num4++;
			}
			if (this.ExternalDirectoryOrganizationId != Guid.Empty)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.ExternalDirectoryOrganizationId.Name, StreamPropertyType.Guid, this.ExternalDirectoryOrganizationId, ref bytes, ref offset);
				num4++;
			}
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Directionality.Name, StreamPropertyType.Int32, this.Directionality, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.OriginalFromAddress.Name, StreamPropertyType.String, this.OriginalFromAddress, ref bytes, ref offset);
			num4++;
			PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.AccountForest.Name, StreamPropertyType.String, this.AccountForest, ref bytes, ref offset);
			num4++;
			if (this.Recipients != null && this.Recipients.Length > 0)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.Recipients.Name, StreamPropertyType.Int32, this.Recipients.Length, ref bytes, ref offset);
				foreach (RecipientInfo recipientInfo in this.Recipients)
				{
					recipientInfo.ToByteArray(ref bytes, ref offset);
				}
				num4++;
			}
			if (this.ComponentLatency != null && this.ComponentLatency.Length > 0)
			{
				PropertyStreamWriter.WritePropertyKeyValue(ExtensibleMessageInfoSchema.ComponentLatency.Name, StreamPropertyType.Int32, this.ComponentLatency.Length, ref bytes, ref offset);
				foreach (ComponentLatencyInfo componentLatencyInfo in this.ComponentLatency)
				{
					componentLatencyInfo.ToByteArray(ref bytes, ref offset);
				}
				num4++;
			}
		}

		// Token: 0x04000D22 RID: 3362
		private const string NumPropertiesKey = "NumProperties";

		// Token: 0x04000D23 RID: 3363
		private static ExtensibleMessageInfoSchema schema = ObjectSchema.GetInstance<ExtensibleMessageInfoSchema>();
	}
}
