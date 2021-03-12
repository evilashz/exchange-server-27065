using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB1 RID: 2737
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidMainStreamCookieException : LocalizedException
	{
		// Token: 0x06008038 RID: 32824 RVA: 0x001A4FB8 File Offset: 0x001A31B8
		public InvalidMainStreamCookieException() : base(DirectoryStrings.InvalidMainStreamCookieException)
		{
		}

		// Token: 0x06008039 RID: 32825 RVA: 0x001A4FC5 File Offset: 0x001A31C5
		public InvalidMainStreamCookieException(Exception innerException) : base(DirectoryStrings.InvalidMainStreamCookieException, innerException)
		{
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x001A4FD3 File Offset: 0x001A31D3
		protected InvalidMainStreamCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x001A4FDD File Offset: 0x001A31DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
