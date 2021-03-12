using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000050 RID: 80
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncEngineSyncStorageProviderCreationException : LocalizedException
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00006361 File Offset: 0x00004561
		public SyncEngineSyncStorageProviderCreationException() : base(Strings.SyncEngineSyncStorageProviderCreationException)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000636E File Offset: 0x0000456E
		public SyncEngineSyncStorageProviderCreationException(Exception innerException) : base(Strings.SyncEngineSyncStorageProviderCreationException, innerException)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000637C File Offset: 0x0000457C
		protected SyncEngineSyncStorageProviderCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006386 File Offset: 0x00004586
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
