using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006EF RID: 1775
	[Serializable]
	public class Contact : OrgPersonPresentationObject
	{
		// Token: 0x17001B7E RID: 7038
		// (get) Token: 0x0600534D RID: 21325 RVA: 0x001307B6 File Offset: 0x0012E9B6
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return Contact.schema;
			}
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x001307BD File Offset: 0x0012E9BD
		public Contact()
		{
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x001307C5 File Offset: 0x0012E9C5
		public Contact(ADContact dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x001307CE File Offset: 0x0012E9CE
		internal static Contact FromDataObject(ADContact dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new Contact(dataObject);
		}

		// Token: 0x17001B7F RID: 7039
		// (get) Token: 0x06005351 RID: 21329 RVA: 0x001307DB File Offset: 0x0012E9DB
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[ContactSchema.OrganizationalUnit];
			}
		}

		// Token: 0x04003830 RID: 14384
		private static ContactSchema schema = ObjectSchema.GetInstance<ContactSchema>();
	}
}
