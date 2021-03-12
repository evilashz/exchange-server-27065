using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	public sealed class SharingFolderNotFoundException : SharingSynchronizationException
	{
		// Token: 0x06001181 RID: 4481 RVA: 0x00050BF3 File Offset: 0x0004EDF3
		public SharingFolderNotFoundException() : base(Strings.SharingFolderNotFoundException)
		{
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00050C00 File Offset: 0x0004EE00
		public SharingFolderNotFoundException(Exception innerException) : base(Strings.SharingFolderNotFoundException, innerException)
		{
		}
	}
}
