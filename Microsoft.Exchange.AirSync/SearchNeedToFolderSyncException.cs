using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000245 RID: 581
	[Serializable]
	internal class SearchNeedToFolderSyncException : AirSyncPermanentException
	{
		// Token: 0x06001543 RID: 5443 RVA: 0x0007C831 File Offset: 0x0007AA31
		internal SearchNeedToFolderSyncException() : base(StatusCode.Sync_NotificationsNotProvisioned, false)
		{
			base.ErrorStringForProtocolLogger = "FolderSyncFirst";
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0007C847 File Offset: 0x0007AA47
		protected SearchNeedToFolderSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
