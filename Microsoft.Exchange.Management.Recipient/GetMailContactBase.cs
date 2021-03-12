using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000079 RID: 121
	public abstract class GetMailContactBase : GetRecipientWithAddressListBase<MailContactIdParameter, ADContact>
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x000268BF File Offset: 0x00024ABF
		public GetMailContactBase()
		{
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x000268C7 File Offset: 0x00024AC7
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetMailContactBase.SortPropertiesArray;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x000268CE File Offset: 0x00024ACE
		protected override string SystemAddressListRdn
		{
			get
			{
				return "All Contacts(VLV)";
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x000268D5 File Offset: 0x00024AD5
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return this.RecipientTypeDetails;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x000268DD File Offset: 0x00024ADD
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailContactSchema>();
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000268E4 File Offset: 0x00024AE4
		// (set) Token: 0x060008EF RID: 2287 RVA: 0x000268FB File Offset: 0x00024AFB
		[Parameter]
		[ValidateNotNullOrEmpty]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(GetMailContactBase.AllowedRecipientTypeDetails, value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002691C File Offset: 0x00024B1C
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
			return new MailContact((ADContact)dataObject);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002695C File Offset: 0x00024B5C
		protected override void InternalProcessRecord()
		{
			this.numMailForestContact = 0;
			base.InternalProcessRecord();
			if (this.numMailForestContact > 0)
			{
				this.WriteWarning(Strings.MailForestContactFound(this.numMailForestContact));
			}
		}

		// Token: 0x040001E9 RID: 489
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = new RecipientTypeDetails[]
		{
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailContact,
			Microsoft.Exchange.Data.Directory.Recipient.RecipientTypeDetails.MailForestContact
		};

		// Token: 0x040001EA RID: 490
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			MailEnabledRecipientSchema.Alias,
			MailEnabledRecipientSchema.DisplayName
		};

		// Token: 0x040001EB RID: 491
		private int numMailForestContact;
	}
}
