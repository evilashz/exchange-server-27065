using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000086 RID: 134
	public abstract class GetMailUserBase<TIdentity> : GetRecipientWithAddressListBase<TIdentity, ADUser> where TIdentity : MailUserIdParameterBase, new()
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x00027E34 File Offset: 0x00026034
		public GetMailUserBase()
		{
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00027E3C File Offset: 0x0002603C
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<MailUserSchema>();
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x00027E43 File Offset: 0x00026043
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetMailUserBase<MailUserIdParameter>.SortPropertiesArray;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00027E4A File Offset: 0x0002604A
		protected override string SystemAddressListRdn
		{
			get
			{
				return "All Mail Users(VLV)";
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00027E51 File Offset: 0x00026051
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return MailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x040001EF RID: 495
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ADObjectSchema.Name,
			MailEnabledRecipientSchema.DisplayName,
			MailEnabledRecipientSchema.Alias
		};
	}
}
