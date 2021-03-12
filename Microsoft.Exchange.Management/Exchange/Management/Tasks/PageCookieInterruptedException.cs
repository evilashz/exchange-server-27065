using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010CB RID: 4299
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PageCookieInterruptedException : LocalizedException
	{
		// Token: 0x0600B2ED RID: 45805 RVA: 0x0029A89B File Offset: 0x00298A9B
		public PageCookieInterruptedException() : base(Strings.PageCookieInterruptedException)
		{
		}

		// Token: 0x0600B2EE RID: 45806 RVA: 0x0029A8A8 File Offset: 0x00298AA8
		public PageCookieInterruptedException(Exception innerException) : base(Strings.PageCookieInterruptedException, innerException)
		{
		}

		// Token: 0x0600B2EF RID: 45807 RVA: 0x0029A8B6 File Offset: 0x00298AB6
		protected PageCookieInterruptedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B2F0 RID: 45808 RVA: 0x0029A8C0 File Offset: 0x00298AC0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
