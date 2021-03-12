using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200076D RID: 1901
	[Serializable]
	public class SystemAttendantMailboxPresentationObject : MailEnabledRecipient
	{
		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x06005D61 RID: 23905 RVA: 0x001423D8 File Offset: 0x001405D8
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SystemAttendantMailboxPresentationObject.schema;
			}
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x001423DF File Offset: 0x001405DF
		public SystemAttendantMailboxPresentationObject()
		{
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x001423E7 File Offset: 0x001405E7
		public SystemAttendantMailboxPresentationObject(ADSystemAttendantMailbox dataObject) : base(dataObject)
		{
		}

		// Token: 0x04003F1C RID: 16156
		private static SystemAttendantMailboxPresentationObjectSchema schema = ObjectSchema.GetInstance<SystemAttendantMailboxPresentationObjectSchema>();
	}
}
