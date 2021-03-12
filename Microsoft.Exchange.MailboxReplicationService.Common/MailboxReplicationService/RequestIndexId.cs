using System;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	public sealed class RequestIndexId : XMLSerializableBase
	{
		// Token: 0x0600138B RID: 5003 RVA: 0x0002C63B File Offset: 0x0002A83B
		public RequestIndexId() : this(RequestIndexLocation.ADLegacy)
		{
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0002C644 File Offset: 0x0002A844
		public RequestIndexId(RequestIndexLocation location)
		{
			this.Location = location;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0002C653 File Offset: 0x0002A853
		public RequestIndexId(ADObjectId mailbox) : this(RequestIndexLocation.Mailbox)
		{
			this.mailbox = mailbox;
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0002C663 File Offset: 0x0002A863
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0002C66B File Offset: 0x0002A86B
		[XmlIgnore]
		public RequestIndexLocation Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0002C674 File Offset: 0x0002A874
		[XmlIgnore]
		public Type RequestIndexEntryType
		{
			get
			{
				switch (this.location)
				{
				case RequestIndexLocation.AD:
					return typeof(MRSRequestWrapper);
				case RequestIndexLocation.UserMailbox:
					return typeof(AggregatedAccountConfigurationWrapper);
				case RequestIndexLocation.Mailbox:
					return typeof(MRSRequestMailboxEntry);
				case RequestIndexLocation.UserMailboxList:
					return typeof(AggregatedAccountListConfigurationWrapper);
				}
				return null;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0002C6D1 File Offset: 0x0002A8D1
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0002C6D9 File Offset: 0x0002A8D9
		[XmlElement(ElementName = "Location")]
		public int LocationInt
		{
			get
			{
				return (int)this.Location;
			}
			set
			{
				this.Location = (RequestIndexLocation)value;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0002C6E2 File Offset: 0x0002A8E2
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0002C6EA File Offset: 0x0002A8EA
		[XmlIgnore]
		public ADObjectId Mailbox
		{
			get
			{
				return this.mailbox;
			}
			set
			{
				this.mailbox = value;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0002C6F3 File Offset: 0x0002A8F3
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x0002C700 File Offset: 0x0002A900
		[DefaultValue(null)]
		[XmlElement(ElementName = "Mailbox", IsNullable = true)]
		public ADObjectIdXML MailboxXml
		{
			get
			{
				return ADObjectIdXML.Serialize(this.Mailbox);
			}
			set
			{
				this.Mailbox = ADObjectIdXML.Deserialize(value);
			}
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0002C710 File Offset: 0x0002A910
		public override bool Equals(object obj)
		{
			RequestIndexId requestIndexId = obj as RequestIndexId;
			return requestIndexId != null && this.Location == requestIndexId.Location && this.Mailbox == requestIndexId.Mailbox;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0002C748 File Offset: 0x0002A948
		public override int GetHashCode()
		{
			if (this.Mailbox != null)
			{
				return this.Mailbox.GetHashCode();
			}
			return this.LocationInt;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0002C764 File Offset: 0x0002A964
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Location);
			if (this.Mailbox != null)
			{
				stringBuilder.AppendFormat(";{0}", this.Mailbox);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000A12 RID: 2578
		private RequestIndexLocation location;

		// Token: 0x04000A13 RID: 2579
		private ADObjectId mailbox;
	}
}
