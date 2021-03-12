using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B2 RID: 690
	[Cmdlet("Restore", "DetailsTemplate", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RestoreDetailsTemplate : SystemConfigurationObjectActionTask<DetailsTemplateIdParameter, DetailsTemplate>
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001854 RID: 6228 RVA: 0x000671F7 File Offset: 0x000653F7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRestoreDetailsTemplate(this.Identity.ToString());
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0006720C File Offset: 0x0006540C
		protected override IConfigurable PrepareDataObject()
		{
			DetailsTemplate detailsTemplate = (DetailsTemplate)base.PrepareDataObject();
			detailsTemplate[DetailsTemplateSchema.TemplateBlob] = detailsTemplate[DetailsTemplateSchema.TemplateBlobOriginal];
			detailsTemplate.MAPIPropertiesDictionary = MAPIPropertiesDictionaryFactory.GetPropertiesDictionary();
			detailsTemplate.BlobToPages();
			return detailsTemplate;
		}
	}
}
