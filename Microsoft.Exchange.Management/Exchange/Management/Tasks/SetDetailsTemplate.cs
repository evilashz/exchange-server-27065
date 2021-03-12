using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B3 RID: 691
	[Cmdlet("Set", "DetailsTemplate", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDetailsTemplate : SetSystemConfigurationObjectTask<DetailsTemplateIdParameter, DetailsTemplate>
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x00067255 File Offset: 0x00065455
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDetailsTemplate(this.Identity.ToString());
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00067268 File Offset: 0x00065468
		protected override void InternalValidate()
		{
			DetailsTemplate instance = this.Instance;
			instance[DetailsTemplateSchema.Pages] = instance.Pages;
			if (instance.TemplateType.Equals("Mailbox Agent"))
			{
				base.WriteError(new InvalidOperationException(Strings.DetailsTemplateMailboxAgent), ErrorCategory.InvalidData, null);
			}
			base.InternalValidate();
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000672BC File Offset: 0x000654BC
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			base.StampChangesOn(dataObject);
			DetailsTemplate detailsTemplate = (DetailsTemplate)dataObject;
			if (detailsTemplate.IsModified(DetailsTemplateSchema.Pages))
			{
				detailsTemplate.PagesToBlob();
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000672EC File Offset: 0x000654EC
		protected override IConfigurable ResolveDataObject()
		{
			DetailsTemplate detailsTemplate = (DetailsTemplate)base.ResolveDataObject();
			detailsTemplate.MAPIPropertiesDictionary = MAPIPropertiesDictionaryFactory.GetPropertiesDictionary();
			detailsTemplate.BlobToPages();
			return detailsTemplate;
		}
	}
}
