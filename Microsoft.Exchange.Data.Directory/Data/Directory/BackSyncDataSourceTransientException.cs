using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB8 RID: 2744
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BackSyncDataSourceTransientException : LocalizedException
	{
		// Token: 0x06008057 RID: 32855 RVA: 0x001A51DC File Offset: 0x001A33DC
		public BackSyncDataSourceTransientException() : base(DirectoryStrings.BackSyncDataSourceTransientErrorMessage)
		{
		}

		// Token: 0x06008058 RID: 32856 RVA: 0x001A51E9 File Offset: 0x001A33E9
		public BackSyncDataSourceTransientException(Exception innerException) : base(DirectoryStrings.BackSyncDataSourceTransientErrorMessage, innerException)
		{
		}

		// Token: 0x06008059 RID: 32857 RVA: 0x001A51F7 File Offset: 0x001A33F7
		protected BackSyncDataSourceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600805A RID: 32858 RVA: 0x001A5201 File Offset: 0x001A3401
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
