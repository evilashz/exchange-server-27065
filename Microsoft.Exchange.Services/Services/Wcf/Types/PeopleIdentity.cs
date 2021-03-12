using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009F8 RID: 2552
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PeopleIdentity : INamedIdentity
	{
		// Token: 0x06004819 RID: 18457 RVA: 0x001011E0 File Offset: 0x000FF3E0
		public PeopleIdentity(string displayName, string legacyDN, string smtpAddress, int addressOrigin, string routingType)
		{
			this.DisplayName = displayName;
			this.Address = legacyDN;
			this.SmtpAddress = smtpAddress;
			this.RoutingType = routingType;
			this.AddressOrigin = addressOrigin;
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x0600481A RID: 18458 RVA: 0x0010120D File Offset: 0x000FF40D
		// (set) Token: 0x0600481B RID: 18459 RVA: 0x00101215 File Offset: 0x000FF415
		[DataMember]
		public string DisplayName { get; private set; }

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x0010121E File Offset: 0x000FF41E
		// (set) Token: 0x0600481D RID: 18461 RVA: 0x00101226 File Offset: 0x000FF426
		[DataMember]
		public string Address { get; private set; }

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x0010122F File Offset: 0x000FF42F
		// (set) Token: 0x0600481F RID: 18463 RVA: 0x00101237 File Offset: 0x000FF437
		[DataMember]
		public string SmtpAddress { get; private set; }

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x00101240 File Offset: 0x000FF440
		// (set) Token: 0x06004821 RID: 18465 RVA: 0x00101248 File Offset: 0x000FF448
		[DataMember]
		public string RoutingType { get; private set; }

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06004822 RID: 18466 RVA: 0x00101251 File Offset: 0x000FF451
		// (set) Token: 0x06004823 RID: 18467 RVA: 0x00101259 File Offset: 0x000FF459
		[DataMember]
		public int AddressOrigin { get; private set; }

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06004824 RID: 18468 RVA: 0x00101264 File Offset: 0x000FF464
		string INamedIdentity.Identity
		{
			get
			{
				if (this.RoutingType == "SMTP")
				{
					return string.Concat(new string[]
					{
						"\"",
						this.DisplayName,
						"\"<",
						this.SmtpAddress,
						">"
					});
				}
				return this.Address;
			}
		}
	}
}
