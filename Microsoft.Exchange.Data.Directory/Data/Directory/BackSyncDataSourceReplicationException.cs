using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ABA RID: 2746
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BackSyncDataSourceReplicationException : LocalizedException
	{
		// Token: 0x06008060 RID: 32864 RVA: 0x001A5283 File Offset: 0x001A3483
		public BackSyncDataSourceReplicationException() : base(DirectoryStrings.BackSyncDataSourceReplicationErrorMessage)
		{
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x001A5290 File Offset: 0x001A3490
		public BackSyncDataSourceReplicationException(Exception innerException) : base(DirectoryStrings.BackSyncDataSourceReplicationErrorMessage, innerException)
		{
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x001A529E File Offset: 0x001A349E
		protected BackSyncDataSourceReplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x001A52A8 File Offset: 0x001A34A8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
