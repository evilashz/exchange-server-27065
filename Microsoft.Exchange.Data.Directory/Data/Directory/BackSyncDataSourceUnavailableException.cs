using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB7 RID: 2743
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BackSyncDataSourceUnavailableException : LocalizedException
	{
		// Token: 0x06008053 RID: 32851 RVA: 0x001A51AD File Offset: 0x001A33AD
		public BackSyncDataSourceUnavailableException() : base(DirectoryStrings.BackSyncDataSourceUnavailableMessage)
		{
		}

		// Token: 0x06008054 RID: 32852 RVA: 0x001A51BA File Offset: 0x001A33BA
		public BackSyncDataSourceUnavailableException(Exception innerException) : base(DirectoryStrings.BackSyncDataSourceUnavailableMessage, innerException)
		{
		}

		// Token: 0x06008055 RID: 32853 RVA: 0x001A51C8 File Offset: 0x001A33C8
		protected BackSyncDataSourceUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008056 RID: 32854 RVA: 0x001A51D2 File Offset: 0x001A33D2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
