using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200063E RID: 1598
	[Cmdlet("New", "PopSettings")]
	[LocDescription(Strings.IDs.NewPop3ConfigurationTask)]
	public sealed class NewPop3Configuration : NewPopImapConfiguration<Pop3AdConfiguration>
	{
		// Token: 0x06003817 RID: 14359 RVA: 0x000E8220 File Offset: 0x000E6420
		public NewPop3Configuration()
		{
			this.DataObject.Banner = "The Microsoft Exchange POP3 service is ready.";
			this.DataObject.UnencryptedOrTLSBindings = new MultiValuedProperty<IPBinding>(new IPBinding[]
			{
				new IPBinding("0.0.0.0:110"),
				new IPBinding("0000:0000:0000:0000:0000:0000:0.0.0.0:110")
			});
			this.DataObject.SSLBindings = new MultiValuedProperty<IPBinding>(new IPBinding[]
			{
				new IPBinding("0.0.0.0:995"),
				new IPBinding("0000:0000:0000:0000:0000:0000:0.0.0.0:995")
			});
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			if (!string.IsNullOrEmpty(localComputerFqdn))
			{
				this.DataObject.InternalConnectionSettings = new MultiValuedProperty<ProtocolConnectionSettings>(new ProtocolConnectionSettings[]
				{
					new ProtocolConnectionSettings(localComputerFqdn + ":110:TLS"),
					new ProtocolConnectionSettings(localComputerFqdn + ":995:SSL")
				});
			}
			this.DataObject.ProxyTargetPort = 1995;
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000E8304 File Offset: 0x000E6504
		protected override string ProtocolName
		{
			get
			{
				return "Pop3";
			}
		}
	}
}
