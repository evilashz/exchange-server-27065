using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB2 RID: 2738
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTenantFullSyncCookieException : LocalizedException
	{
		// Token: 0x0600803C RID: 32828 RVA: 0x001A4FE7 File Offset: 0x001A31E7
		public InvalidTenantFullSyncCookieException() : base(DirectoryStrings.InvalidTenantFullSyncCookieException)
		{
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x001A4FF4 File Offset: 0x001A31F4
		public InvalidTenantFullSyncCookieException(Exception innerException) : base(DirectoryStrings.InvalidTenantFullSyncCookieException, innerException)
		{
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x001A5002 File Offset: 0x001A3202
		protected InvalidTenantFullSyncCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x001A500C File Offset: 0x001A320C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
