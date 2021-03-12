using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A4 RID: 420
	[DataContract]
	internal class PlayOnPhoneNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0003AC8C File Offset: 0x00038E8C
		public PlayOnPhoneNotificationPayload()
		{
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003AC94 File Offset: 0x00038E94
		public PlayOnPhoneNotificationPayload(string callState)
		{
			this.CallState = callState;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003ACA3 File Offset: 0x00038EA3
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0003ACAB File Offset: 0x00038EAB
		public string CallState
		{
			get
			{
				return this.callState;
			}
			set
			{
				this.callState = value;
			}
		}

		// Token: 0x04000929 RID: 2345
		[DataMember(EmitDefaultValue = false)]
		private string callState;
	}
}
