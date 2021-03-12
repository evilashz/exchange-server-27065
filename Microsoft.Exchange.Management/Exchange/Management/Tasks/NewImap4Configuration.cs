using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200062F RID: 1583
	[Cmdlet("New", "ImapSettings")]
	[LocDescription(Strings.IDs.NewImap4ConfigurationTask)]
	public sealed class NewImap4Configuration : NewPopImapConfiguration<Imap4AdConfiguration>
	{
		// Token: 0x060037DD RID: 14301 RVA: 0x000E7E14 File Offset: 0x000E6014
		public NewImap4Configuration()
		{
			this.DataObject.Banner = "The Microsoft Exchange IMAP4 service is ready.";
			this.DataObject.UnencryptedOrTLSBindings = new MultiValuedProperty<IPBinding>(new IPBinding[]
			{
				new IPBinding("0.0.0.0:143"),
				new IPBinding("0000:0000:0000:0000:0000:0000:0.0.0.0:143")
			});
			this.DataObject.SSLBindings = new MultiValuedProperty<IPBinding>(new IPBinding[]
			{
				new IPBinding("0.0.0.0:993"),
				new IPBinding("0000:0000:0000:0000:0000:0000:0.0.0.0:993")
			});
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			if (!string.IsNullOrEmpty(localComputerFqdn))
			{
				this.DataObject.InternalConnectionSettings = new MultiValuedProperty<ProtocolConnectionSettings>(new ProtocolConnectionSettings[]
				{
					new ProtocolConnectionSettings(localComputerFqdn + ":143:TLS"),
					new ProtocolConnectionSettings(localComputerFqdn + ":993:SSL")
				});
			}
			this.DataObject.ProxyTargetPort = 1993;
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x060037DE RID: 14302 RVA: 0x000E7EF8 File Offset: 0x000E60F8
		protected override string ProtocolName
		{
			get
			{
				return "Imap4";
			}
		}
	}
}
