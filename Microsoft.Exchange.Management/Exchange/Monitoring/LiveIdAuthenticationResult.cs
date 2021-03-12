using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000024 RID: 36
	[Serializable]
	public class LiveIdAuthenticationResult
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00005D38 File Offset: 0x00003F38
		public LiveIdAuthenticationResult()
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005D40 File Offset: 0x00003F40
		public LiveIdAuthenticationResult(LiveIdAuthenticationResultEnum result)
		{
			this.result = result;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005D4F File Offset: 0x00003F4F
		public LiveIdAuthenticationResultEnum Value
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005D58 File Offset: 0x00003F58
		public override string ToString()
		{
			string text = string.Empty;
			switch (this.result)
			{
			case LiveIdAuthenticationResultEnum.Undefined:
				text = Strings.LiveIdAuthenticationResultUndefined;
				break;
			case LiveIdAuthenticationResultEnum.Success:
				text = Strings.LiveIdAuthenticationResultSuccess;
				break;
			case LiveIdAuthenticationResultEnum.Failure:
				text = Strings.LiveIdAuthenticationResultFailure;
				break;
			}
			return text;
		}

		// Token: 0x040000B5 RID: 181
		private LiveIdAuthenticationResultEnum result;
	}
}
