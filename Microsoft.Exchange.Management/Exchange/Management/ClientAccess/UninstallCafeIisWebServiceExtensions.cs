using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClientAccess
{
	// Token: 0x020000D0 RID: 208
	[LocDescription(Strings.IDs.UninstallCafeIisWebServiceExtensions)]
	[Cmdlet("Uninstall", "CafeIisWebServiceExtensions")]
	public sealed class UninstallCafeIisWebServiceExtensions : UninstallIisWebServiceExtensions
	{
		// Token: 0x1700022D RID: 557
		protected override IisWebServiceExtension this[int i]
		{
			get
			{
				return CafeIisWebServiceExtension.AllExtensions[i];
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001B05E File Offset: 0x0001925E
		protected override int ExtensionCount
		{
			get
			{
				return CafeIisWebServiceExtension.AllExtensions.Length;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001B067 File Offset: 0x00019267
		protected override string HostName
		{
			get
			{
				return "localhost";
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0001B06E File Offset: 0x0001926E
		protected override string GroupDescription
		{
			get
			{
				return Strings.GetLocalizedString((Strings.IDs)3143233303U);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001B07F File Offset: 0x0001927F
		protected override string GroupID
		{
			get
			{
				return "MSExchangeCafe";
			}
		}
	}
}
