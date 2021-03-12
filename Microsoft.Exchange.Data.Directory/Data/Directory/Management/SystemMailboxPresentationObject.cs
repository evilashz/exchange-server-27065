using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200076F RID: 1903
	[Serializable]
	public class SystemMailboxPresentationObject : MailEnabledRecipient
	{
		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x06005D67 RID: 23911 RVA: 0x0014240B File Offset: 0x0014060B
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SystemMailboxPresentationObject.schema;
			}
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00142412 File Offset: 0x00140612
		public SystemMailboxPresentationObject()
		{
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x0014241A File Offset: 0x0014061A
		public SystemMailboxPresentationObject(ADSystemMailbox dataObject) : base(dataObject)
		{
		}

		// Token: 0x04003F1D RID: 16157
		private static SystemMailboxPresentationObjectSchema schema = ObjectSchema.GetInstance<SystemMailboxPresentationObjectSchema>();
	}
}
