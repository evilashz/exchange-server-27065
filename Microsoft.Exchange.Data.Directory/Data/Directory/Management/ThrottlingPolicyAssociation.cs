using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000776 RID: 1910
	[Serializable]
	public class ThrottlingPolicyAssociation : ADPresentationObject
	{
		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06005DFB RID: 24059 RVA: 0x00143875 File Offset: 0x00141A75
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return ThrottlingPolicyAssociation.schema;
			}
		}

		// Token: 0x06005DFC RID: 24060 RVA: 0x0014387C File Offset: 0x00141A7C
		public ThrottlingPolicyAssociation()
		{
		}

		// Token: 0x06005DFD RID: 24061 RVA: 0x00143884 File Offset: 0x00141A84
		public ThrottlingPolicyAssociation(ADRecipient dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005DFE RID: 24062 RVA: 0x0014388D File Offset: 0x00141A8D
		internal static ThrottlingPolicyAssociation FromDataObject(ADRecipient dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new ThrottlingPolicyAssociation(dataObject);
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06005DFF RID: 24063 RVA: 0x0014389A File Offset: 0x00141A9A
		public ADObjectId ObjectId
		{
			get
			{
				return (ADObjectId)this[ThrottlingPolicyAssociationSchema.ObjectId];
			}
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x06005E00 RID: 24064 RVA: 0x001438AC File Offset: 0x00141AAC
		public ADObjectId ThrottlingPolicyId
		{
			get
			{
				return (ADObjectId)this[ThrottlingPolicyAssociationSchema.ThrottlingPolicyId];
			}
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x06005E01 RID: 24065 RVA: 0x001438BE File Offset: 0x00141ABE
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x04003F8A RID: 16266
		private static ThrottlingPolicyAssociationSchema schema = ObjectSchema.GetInstance<ThrottlingPolicyAssociationSchema>();
	}
}
