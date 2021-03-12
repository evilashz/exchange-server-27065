using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B1 RID: 689
	[Cmdlet("Get", "DetailsTemplate", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class GetDetailsTemplate : GetSystemConfigurationObjectTask<DetailsTemplateIdParameter, DetailsTemplate>
	{
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0006718E File Offset: 0x0006538E
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00067194 File Offset: 0x00065394
		protected override void WriteResult(IConfigurable dataObject)
		{
			DetailsTemplate detailsTemplate = dataObject as DetailsTemplate;
			if (detailsTemplate.Language != null)
			{
				detailsTemplate.MAPIPropertiesDictionary = MAPIPropertiesDictionaryFactory.GetPropertiesDictionary();
				detailsTemplate.BlobToPages();
				if (this.Identity == null || !detailsTemplate.Identity.Equals(this.Identity.RawIdentity))
				{
					detailsTemplate.MAPIPropertiesDictionary = null;
				}
				base.WriteResult(dataObject);
			}
		}
	}
}
