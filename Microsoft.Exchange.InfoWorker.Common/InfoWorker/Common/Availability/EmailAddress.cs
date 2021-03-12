using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200007A RID: 122
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EmailAddress
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000E569 File Offset: 0x0000C769
		public EmailAddress()
		{
			this.smtpAddress = SmtpAddress.Empty;
			this.Init();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000E582 File Offset: 0x0000C782
		public EmailAddress(string name, string address, string routingType)
		{
			this.Init();
			this.name = name;
			this.address = address;
			if (!string.IsNullOrEmpty(routingType))
			{
				this.routingType = routingType;
			}
			this.smtpAddress = SmtpAddress.Empty;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public EmailAddress(string name, string address) : this(name, address, "SMTP")
		{
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000E5C7 File Offset: 0x0000C7C7
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000E5CF File Offset: 0x0000C7CF
		[DataMember]
		[XmlElement]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000E5E0 File Offset: 0x0000C7E0
		[XmlElement]
		[DataMember]
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000E5EC File Offset: 0x0000C7EC
		public string Domain
		{
			get
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(this.routingType, "SMTP"))
				{
					if (this.smtpAddress == SmtpAddress.Empty)
					{
						this.smtpAddress = new SmtpAddress(this.address);
					}
					return this.smtpAddress.Domain;
				}
				return null;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000E640 File Offset: 0x0000C840
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000E648 File Offset: 0x0000C848
		[XmlElement]
		[DataMember]
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.routingType = value;
				}
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E659 File Offset: 0x0000C859
		public override string ToString()
		{
			return string.Format("<{0}>{1}:{2}", this.name, this.routingType, this.address);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E677 File Offset: 0x0000C877
		[OnDeserializing]
		private void Init(StreamingContext context)
		{
			this.Init();
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E67F File Offset: 0x0000C87F
		private void Init()
		{
			this.routingType = "SMTP";
		}

		// Token: 0x040001E3 RID: 483
		private const string SmtpType = "SMTP";

		// Token: 0x040001E4 RID: 484
		private string name;

		// Token: 0x040001E5 RID: 485
		private string address;

		// Token: 0x040001E6 RID: 486
		private string routingType;

		// Token: 0x040001E7 RID: 487
		private SmtpAddress smtpAddress;
	}
}
