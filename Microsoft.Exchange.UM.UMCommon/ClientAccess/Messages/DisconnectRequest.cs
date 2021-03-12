using System;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class DisconnectRequest : RequestBase
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000E180 File Offset: 0x0000C380
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000E188 File Offset: 0x0000C388
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

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E191 File Offset: 0x0000C391
		internal override string GetFriendlyName()
		{
			return Strings.DisconnectRequest;
		}

		// Token: 0x040002C0 RID: 704
		private string callId;
	}
}
