using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000417 RID: 1047
	[DataContract]
	public class MailRoutingDomain : BaseRow
	{
		// Token: 0x0600352C RID: 13612 RVA: 0x000A5775 File Offset: 0x000A3975
		public MailRoutingDomain(AcceptedDomain domain) : base(domain)
		{
			this.acceptedDomain = domain;
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x0600352D RID: 13613 RVA: 0x000A5785 File Offset: 0x000A3985
		// (set) Token: 0x0600352E RID: 13614 RVA: 0x000A5797 File Offset: 0x000A3997
		[DataMember]
		public string DomainName
		{
			get
			{
				return this.acceptedDomain.DomainName.ToString();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x0600352F RID: 13615 RVA: 0x000A579E File Offset: 0x000A399E
		// (set) Token: 0x06003530 RID: 13616 RVA: 0x000A57B5 File Offset: 0x000A39B5
		[DataMember]
		public string DomainType
		{
			get
			{
				return this.acceptedDomain.DomainType.ToString();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06003531 RID: 13617 RVA: 0x000A57BC File Offset: 0x000A39BC
		// (set) Token: 0x06003532 RID: 13618 RVA: 0x000A57C4 File Offset: 0x000A39C4
		[DataMember]
		public string DomainTypeString
		{
			get
			{
				return this.GetDomainTypeString();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000A57CB File Offset: 0x000A39CB
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000A57D3 File Offset: 0x000A39D3
		[DataMember]
		public string CaptionText
		{
			get
			{
				return this.DomainName;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000A57DC File Offset: 0x000A39DC
		private string GetDomainTypeString()
		{
			if (this.acceptedDomain.DomainType == AcceptedDomainType.Authoritative)
			{
				return Strings.AuthoritativeString;
			}
			if (this.acceptedDomain.DomainType == AcceptedDomainType.InternalRelay)
			{
				return Strings.InternalRelayString;
			}
			return this.acceptedDomain.DomainType.ToString();
		}

		// Token: 0x04002572 RID: 9586
		private AcceptedDomain acceptedDomain;
	}
}
