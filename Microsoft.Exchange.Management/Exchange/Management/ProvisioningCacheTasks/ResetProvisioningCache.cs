using System;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningCacheTasks
{
	// Token: 0x02000645 RID: 1605
	[Cmdlet("Reset", "ProvisioningCache", DefaultParameterSetName = "OrganizationCache", SupportsShouldProcess = true)]
	public sealed class ResetProvisioningCache : ProvisioningCacheDiagnosticBase
	{
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000E89A0 File Offset: 0x000E6BA0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationResetProvisioningCache(base.Server.ToString(), base.Application);
			}
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000E89B8 File Offset: 0x000E6BB8
		protected override void ProcessReceivedData(byte[] buffer, int bufLen)
		{
			string @string = Encoding.UTF8.GetString(buffer, 0, bufLen);
			base.WriteObject(@string);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000E89DA File Offset: 0x000E6BDA
		internal override DiagnosticType GetDiagnosticType()
		{
			return DiagnosticType.Reset;
		}
	}
}
