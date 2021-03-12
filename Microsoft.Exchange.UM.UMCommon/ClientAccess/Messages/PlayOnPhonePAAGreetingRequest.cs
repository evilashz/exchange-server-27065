using System;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000115 RID: 277
	[Serializable]
	public class PlayOnPhonePAAGreetingRequest : PlayOnPhoneUserRequest
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00023C16 File Offset: 0x00021E16
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x00023C1E File Offset: 0x00021E1E
		public Guid Identity
		{
			get
			{
				return this.paaIdentity;
			}
			set
			{
				this.paaIdentity = value;
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00023C27 File Offset: 0x00021E27
		internal override string GetFriendlyName()
		{
			return Strings.PlayOnPhonePAAGreetingRequest;
		}

		// Token: 0x0400051E RID: 1310
		private Guid paaIdentity;
	}
}
