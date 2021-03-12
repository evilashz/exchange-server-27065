using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClientAccess
{
	// Token: 0x020000CB RID: 203
	[LocDescription(Strings.IDs.InstallClientAccessIisWebServiceExtensions)]
	[Cmdlet("Install", "ClientAccessIisWebServiceExtensions")]
	public sealed class InstallClientAccessWebExtensions : InstallIisWebServiceExtensions
	{
		// Token: 0x17000225 RID: 549
		protected override IisWebServiceExtension this[int i]
		{
			get
			{
				return IisWebServiceExtension.AllExtensions[i];
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		protected override int ExtensionCount
		{
			get
			{
				return IisWebServiceExtension.AllExtensions.Length;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001AB01 File Offset: 0x00018D01
		protected override string HostName
		{
			get
			{
				return "localhost";
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001AB08 File Offset: 0x00018D08
		protected override string GroupDescription
		{
			get
			{
				return Strings.GetLocalizedString(Strings.IDs.ClientAccessIisWebServiceExtensionsDescription);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001AB19 File Offset: 0x00018D19
		protected override string GroupID
		{
			get
			{
				return "MSExchangeClientAccess";
			}
		}
	}
}
