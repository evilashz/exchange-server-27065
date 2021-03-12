using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000016 RID: 22
	[Cmdlet("Get", "Contact", DefaultParameterSetName = "Identity")]
	public sealed class GetContact : GetRecipientBase<ContactIdParameter, ADContact>
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000139 RID: 313 RVA: 0x0000742C File Offset: 0x0000562C
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetContact.SortPropertiesArray;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007433 File Offset: 0x00005633
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return this.RecipientTypeDetails;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000743B File Offset: 0x0000563B
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ContactSchema>();
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007442 File Offset: 0x00005642
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00007459 File Offset: 0x00005659
		[ValidateNotNullOrEmpty]
		[Parameter]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(GetContact.AllowedRecipientTypeDetails, value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007478 File Offset: 0x00005678
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			ADContact adcontact = (ADContact)dataObject;
			if (adcontact.RecipientTypeDetails == Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailForestContact)
			{
				this.numMailForestContact++;
			}
			return new Contact(adcontact);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000074B3 File Offset: 0x000056B3
		protected override void InternalProcessRecord()
		{
			this.numMailForestContact = 0;
			base.InternalProcessRecord();
			if (this.numMailForestContact > 0)
			{
				this.WriteWarning(Strings.MailForestContactFound(this.numMailForestContact));
			}
		}

		// Token: 0x04000027 RID: 39
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.Contact,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailContact,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailForestContact
		};

		// Token: 0x04000028 RID: 40
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			OrgPersonPresentationObjectSchema.DisplayName,
			OrgPersonPresentationObjectSchema.FirstName,
			OrgPersonPresentationObjectSchema.LastName,
			OrgPersonPresentationObjectSchema.Office,
			OrgPersonPresentationObjectSchema.City
		};

		// Token: 0x04000029 RID: 41
		private int numMailForestContact;
	}
}
