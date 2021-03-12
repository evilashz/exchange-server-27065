using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.EhfHybridMailflow
{
	// Token: 0x02000874 RID: 2164
	[Cmdlet("Get", "HybridMailflow")]
	public sealed class GetHybridMailflow : HybridMailflowTaskBase
	{
		// Token: 0x06004B08 RID: 19208 RVA: 0x001373E4 File Offset: 0x001355E4
		protected override void InternalProcessRecord()
		{
			if (base.OriginalInboundConnector == null)
			{
				base.WriteVerbose(base.NullInboundConnectorMessage);
				return;
			}
			if (base.OriginalOutboundConnector == null)
			{
				base.WriteVerbose(base.NullOutboundConnectorMessage);
				return;
			}
			base.WriteObject(HybridMailflowTaskBase.ConvertToHybridMailflowConfiguration(base.OriginalInboundConnector, base.OriginalOutboundConnector));
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00137434 File Offset: 0x00135634
		private HybridMailflowConfiguration GetHybridMailflowSettingsFromMock()
		{
			HybridMailflowConfiguration result;
			if (base.Organization != null)
			{
				result = new HybridMailflowConfiguration(new List<SmtpDomainWithSubdomains>
				{
					new SmtpDomainWithSubdomains("contoso.com"),
					new SmtpDomainWithSubdomains("test.contoso.com")
				}, new List<IPRange>(), new Fqdn("mail.contoso.com"), "*.contoso.com", new bool?(false), new bool?(false));
			}
			else
			{
				result = new HybridMailflowConfiguration(new List<SmtpDomainWithSubdomains>(), new List<IPRange>(), new Fqdn("mail.fabrikam.com"), "*.fabrikam.com", new bool?(true), new bool?(false));
			}
			return result;
		}

		// Token: 0x04002D07 RID: 11527
		private const string OperationName = "Get-HybridMailflow";
	}
}
