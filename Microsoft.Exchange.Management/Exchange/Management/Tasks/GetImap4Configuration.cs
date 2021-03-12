using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200062D RID: 1581
	[LocDescription(Strings.IDs.GetImap4ConfigurationTask)]
	[Cmdlet("Get", "ImapSettings")]
	public sealed class GetImap4Configuration : GetPopImapConfiguration<Imap4AdConfiguration>
	{
	}
}
