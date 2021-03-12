using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000035 RID: 53
	internal class MailStorageNotFoundException : LocalizedException
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00006B3B File Offset: 0x00004D3B
		public MailStorageNotFoundException() : base(Strings.descFailedToGetUserOofPolicy)
		{
		}
	}
}
