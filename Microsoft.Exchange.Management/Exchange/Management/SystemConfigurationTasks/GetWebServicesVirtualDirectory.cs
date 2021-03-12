using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C19 RID: 3097
	[Cmdlet("Get", "WebServicesVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetWebServicesVirtualDirectory : GetExchangeServiceVirtualDirectory<ADWebServicesVirtualDirectory>
	{
		// Token: 0x060074B1 RID: 29873 RVA: 0x001DC1EC File Offset: 0x001DA3EC
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADWebServicesVirtualDirectory adwebServicesVirtualDirectory = (ADWebServicesVirtualDirectory)dataObject;
			adwebServicesVirtualDirectory.GzipLevel = Gzip.GetGzipLevel(adwebServicesVirtualDirectory.MetabasePath);
			adwebServicesVirtualDirectory.CertificateAuthentication = base.GetCertificateAuthentication(dataObject, "Management");
			adwebServicesVirtualDirectory.LiveIdNegotiateAuthentication = base.GetLiveIdNegotiateAuthentication(dataObject, "Nego2");
			TaskLogger.LogExit();
		}
	}
}
