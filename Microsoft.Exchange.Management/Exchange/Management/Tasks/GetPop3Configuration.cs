using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200063C RID: 1596
	[LocDescription(Strings.IDs.GetPop3ConfigurationTask)]
	[Cmdlet("Get", "PopSettings")]
	public sealed class GetPop3Configuration : GetPopImapConfiguration<Pop3AdConfiguration>
	{
	}
}
