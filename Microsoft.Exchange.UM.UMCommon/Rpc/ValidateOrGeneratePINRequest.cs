using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public class ValidateOrGeneratePINRequest : UpdateUMMailboxRequest
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x00031546 File Offset: 0x0002F746
		public ValidateOrGeneratePINRequest()
		{
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003154E File Offset: 0x0002F74E
		internal ValidateOrGeneratePINRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00031557 File Offset: 0x0002F757
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x0003155F File Offset: 0x0002F75F
		public string PIN
		{
			get
			{
				return this.pin;
			}
			set
			{
				this.pin = value;
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00031568 File Offset: 0x0002F768
		internal override UMRpcResponse Execute()
		{
			return Utils.ValidateOrGeneratePIN(base.User, this.pin);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003157B File Offset: 0x0002F77B
		internal override string GetFriendlyName()
		{
			return Strings.ValidateOrGeneratePINRequest;
		}

		// Token: 0x040006D4 RID: 1748
		private string pin;
	}
}
