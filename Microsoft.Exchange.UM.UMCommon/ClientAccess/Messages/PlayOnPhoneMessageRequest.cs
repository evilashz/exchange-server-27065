using System;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public class PlayOnPhoneMessageRequest : PlayOnPhoneUserRequest
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00026104 File Offset: 0x00024304
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002610C File Offset: 0x0002430C
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
			set
			{
				this.objectId = value;
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00026115 File Offset: 0x00024315
		internal override string GetFriendlyName()
		{
			return Strings.PlayOnPhoneMessageRequest;
		}

		// Token: 0x04000578 RID: 1400
		private string objectId;
	}
}
