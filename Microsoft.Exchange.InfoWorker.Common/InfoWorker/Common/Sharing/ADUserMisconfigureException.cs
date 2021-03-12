using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000262 RID: 610
	[Serializable]
	public sealed class ADUserMisconfigureException : SharingSynchronizationException
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x00050C29 File Offset: 0x0004EE29
		public ADUserMisconfigureException() : base(Strings.ADUserMisconfiguredException)
		{
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00050C36 File Offset: 0x0004EE36
		public ADUserMisconfigureException(Exception innerException) : base(Strings.ADUserMisconfiguredException, innerException)
		{
		}
	}
}
