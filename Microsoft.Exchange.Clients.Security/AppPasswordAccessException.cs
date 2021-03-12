using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000016 RID: 22
	public class AppPasswordAccessException : LiveClientException
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000323A File Offset: 0x0000143A
		public AppPasswordAccessException() : base(new LocalizedString(null), null)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003249 File Offset: 0x00001449
		public Strings.IDs ErrorMessageStringId
		{
			get
			{
				return -1220450835;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003250 File Offset: 0x00001450
		public string ErrorMessage
		{
			get
			{
				return Strings.AppPasswordAccessErrorMessage;
			}
		}
	}
}
