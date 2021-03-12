using System;
using System.Management.Automation;
using Microsoft.Exchange.Management.IisTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ClientAccess
{
	// Token: 0x020000CA RID: 202
	[LocDescription(Strings.IDs.InstallCafeIisWebServiceExtensions)]
	[Cmdlet("Install", "CafeIisWebServiceExtensions")]
	public sealed class InstallCafeWebExtensions : InstallIisWebServiceExtensions
	{
		// Token: 0x17000220 RID: 544
		protected override IisWebServiceExtension this[int i]
		{
			get
			{
				return CafeIisWebServiceExtension.AllExtensions[i];
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001AAB6 File Offset: 0x00018CB6
		protected override int ExtensionCount
		{
			get
			{
				return CafeIisWebServiceExtension.AllExtensions.Length;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001AABF File Offset: 0x00018CBF
		protected override string HostName
		{
			get
			{
				return "localhost";
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001AAC6 File Offset: 0x00018CC6
		protected override string GroupDescription
		{
			get
			{
				return Strings.GetLocalizedString((Strings.IDs)3143233303U);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001AAD7 File Offset: 0x00018CD7
		protected override string GroupID
		{
			get
			{
				return "MSExchangeCafe";
			}
		}
	}
}
