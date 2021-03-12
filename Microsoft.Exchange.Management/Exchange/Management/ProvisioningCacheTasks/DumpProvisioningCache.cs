using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningCacheTasks
{
	// Token: 0x02000644 RID: 1604
	[Cmdlet("Dump", "ProvisioningCache", DefaultParameterSetName = "OrganizationCache", SupportsShouldProcess = true)]
	public sealed class DumpProvisioningCache : ProvisioningCacheDiagnosticBase
	{
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000E8948 File Offset: 0x000E6B48
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationDumpProvisioningCache(base.Server.ToString(), base.Application);
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000E8960 File Offset: 0x000E6B60
		protected override void ProcessReceivedData(byte[] buffer, int bufLen)
		{
			Exception ex = null;
			CachedEntryPresentationObject sendToPipeline = CachedEntryPresentationObject.TryFromReceivedData(buffer, bufLen, out ex);
			if (ex != null)
			{
				this.WriteWarning(new LocalizedString(ex.Message));
				return;
			}
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000E8995 File Offset: 0x000E6B95
		internal override DiagnosticType GetDiagnosticType()
		{
			return DiagnosticType.Dump;
		}
	}
}
