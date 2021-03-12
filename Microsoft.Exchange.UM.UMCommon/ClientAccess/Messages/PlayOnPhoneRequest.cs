using System;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public abstract class PlayOnPhoneRequest : RequestBase
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00023B7F File Offset: 0x00021D7F
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x00023B87 File Offset: 0x00021D87
		public string DialString
		{
			get
			{
				return this.dialString;
			}
			set
			{
				this.dialString = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00023B90 File Offset: 0x00021D90
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00023B98 File Offset: 0x00021D98
		internal UMOperationResult EndResult
		{
			get
			{
				return this.endResult;
			}
			set
			{
				this.endResult = value;
			}
		}

		// Token: 0x0400051B RID: 1307
		private string dialString;

		// Token: 0x0400051C RID: 1308
		private UMOperationResult endResult;
	}
}
