using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000743 RID: 1859
	[Serializable]
	public class PublicDatabasePresentationObject : MailEnabledOrgPerson
	{
		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x06005A94 RID: 23188 RVA: 0x0013DDA9 File Offset: 0x0013BFA9
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return PublicDatabasePresentationObject.schema;
			}
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x0013DDB0 File Offset: 0x0013BFB0
		public PublicDatabasePresentationObject()
		{
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x0013DDB8 File Offset: 0x0013BFB8
		internal PublicDatabasePresentationObject(ADPublicDatabase dataObject) : base(dataObject)
		{
		}

		// Token: 0x04003CCC RID: 15564
		private static PublicDatabasePresentationObjectSchema schema = ObjectSchema.GetInstance<PublicDatabasePresentationObjectSchema>();
	}
}
