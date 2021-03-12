using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F2 RID: 242
	[KnownType(typeof(SmtpDomainRow))]
	[DataContract]
	public class SmtpDomainRow : BaseRow
	{
		// Token: 0x06001E92 RID: 7826 RVA: 0x0005C134 File Offset: 0x0005A334
		public SmtpDomainRow(SmtpDomain smtpDomain) : base(new Identity(smtpDomain.Domain, smtpDomain.Domain), null)
		{
			AutoDiscoverSmtpDomain autoDiscoverSmtpDomain = smtpDomain as AutoDiscoverSmtpDomain;
			if (autoDiscoverSmtpDomain != null)
			{
				this.autodiscover = autoDiscoverSmtpDomain.AutoDiscover;
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0005C16F File Offset: 0x0005A36F
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0005C17C File Offset: 0x0005A37C
		[DataMember]
		public string Domain
		{
			get
			{
				return base.Identity.DisplayName;
			}
			set
			{
			}
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x0005C17E File Offset: 0x0005A37E
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x0005C19F File Offset: 0x0005A39F
		[DataMember]
		public string Value
		{
			get
			{
				if (!this.autodiscover)
				{
					return this.Domain;
				}
				return "autod:" + this.Domain;
			}
			set
			{
			}
		}

		// Token: 0x04001C13 RID: 7187
		private const string AutodiscoverDomainPrefx = "autod:";

		// Token: 0x04001C14 RID: 7188
		private readonly bool autodiscover;
	}
}
