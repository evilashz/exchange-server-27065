using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200024E RID: 590
	[DataContract]
	public class AcceptedDomain : DropDownItemData
	{
		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x0007FD2A File Offset: 0x0007DF2A
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x0007FD32 File Offset: 0x0007DF32
		public AuthenticationType? AuthenticationType { get; set; }

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x0007FD3B File Offset: 0x0007DF3B
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x0007FD43 File Offset: 0x0007DF43
		public AcceptedDomainType DomainType { get; set; }

		// Token: 0x06002897 RID: 10391 RVA: 0x0007FD4C File Offset: 0x0007DF4C
		public AcceptedDomain(AcceptedDomain domain) : base(domain)
		{
			string text = domain.DomainName.IsStar ? domain.DomainName.ToString() : domain.DomainName.SmtpDomain.ToString();
			base.Text = text;
			base.Value = text;
			base.Selected = domain.Default;
			this.AuthenticationType = domain.AuthenticationType;
			this.DomainType = domain.DomainType;
		}
	}
}
