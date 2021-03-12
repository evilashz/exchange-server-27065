using System;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000134 RID: 308
	[Serializable]
	public class PlayOnPhoneResponse : ResponseBase
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00026129 File Offset: 0x00024329
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00026131 File Offset: 0x00024331
		public string CallId
		{
			get
			{
				return this.callId;
			}
			set
			{
				this.callId = value;
			}
		}

		// Token: 0x04000579 RID: 1401
		private string callId;
	}
}
