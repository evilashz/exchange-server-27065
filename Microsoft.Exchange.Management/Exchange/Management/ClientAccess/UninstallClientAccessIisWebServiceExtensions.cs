using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClientAccess
{
	// Token: 0x020000D1 RID: 209
	[Cmdlet("Uninstall", "ClientAccessIisWebServiceExtensions")]
	[LocDescription(Strings.IDs.UninstallClientAccessIisWebServiceExtensions)]
	public sealed class UninstallClientAccessIisWebServiceExtensions : UninstallIisWebServiceExtensions
	{
		// Token: 0x17000232 RID: 562
		protected override IisWebServiceExtension this[int i]
		{
			get
			{
				return IisWebServiceExtension.AllExtensions[i];
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0001B0A0 File Offset: 0x000192A0
		protected override int ExtensionCount
		{
			get
			{
				return IisWebServiceExtension.AllExtensions.Length;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0001B0A9 File Offset: 0x000192A9
		protected override string HostName
		{
			get
			{
				return "localhost";
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001B0B0 File Offset: 0x000192B0
		protected override string GroupDescription
		{
			get
			{
				return Strings.GetLocalizedString(Strings.IDs.ClientAccessIisWebServiceExtensionsDescription);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0001B0C1 File Offset: 0x000192C1
		protected override string GroupID
		{
			get
			{
				return "MSExchangeClientAccess";
			}
		}
	}
}
