using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public class PlayOnPhoneGreetingRequest : PlayOnPhoneUserRequest
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000260DF File Offset: 0x000242DF
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x000260E7 File Offset: 0x000242E7
		public UMGreetingType GreetingType
		{
			get
			{
				return this.greetingType;
			}
			set
			{
				this.greetingType = value;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000260F0 File Offset: 0x000242F0
		internal override string GetFriendlyName()
		{
			return Strings.PlayOnPhoneGreetingRequest;
		}

		// Token: 0x04000577 RID: 1399
		private UMGreetingType greetingType;
	}
}
