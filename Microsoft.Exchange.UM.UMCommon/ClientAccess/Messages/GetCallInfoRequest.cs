using System;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200007F RID: 127
	[Serializable]
	public class GetCallInfoRequest : RequestBase
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000F2C7 File Offset: 0x0000D4C7
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0000F2CF File Offset: 0x0000D4CF
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

		// Token: 0x06000468 RID: 1128 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		internal override string GetFriendlyName()
		{
			return Strings.GetCallInfoRequest;
		}

		// Token: 0x040002F1 RID: 753
		private string callId;
	}
}
