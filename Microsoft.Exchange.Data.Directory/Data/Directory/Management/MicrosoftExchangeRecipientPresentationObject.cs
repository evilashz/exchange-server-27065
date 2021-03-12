using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200073F RID: 1855
	[Serializable]
	public class MicrosoftExchangeRecipientPresentationObject : MailEnabledRecipient
	{
		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x06005A1B RID: 23067 RVA: 0x0013CE6D File Offset: 0x0013B06D
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MicrosoftExchangeRecipientPresentationObject.schema;
			}
		}

		// Token: 0x06005A1C RID: 23068 RVA: 0x0013CE74 File Offset: 0x0013B074
		public MicrosoftExchangeRecipientPresentationObject()
		{
		}

		// Token: 0x06005A1D RID: 23069 RVA: 0x0013CE7C File Offset: 0x0013B07C
		public MicrosoftExchangeRecipientPresentationObject(ADMicrosoftExchangeRecipient dataObject) : base(dataObject)
		{
		}

		// Token: 0x04003C63 RID: 15459
		private static MicrosoftExchangeRecipientPresentationObjectSchema schema = ObjectSchema.GetInstance<MicrosoftExchangeRecipientPresentationObjectSchema>();
	}
}
