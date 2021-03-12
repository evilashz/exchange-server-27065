using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB3 RID: 2739
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCookieException : LocalizedException
	{
		// Token: 0x06008040 RID: 32832 RVA: 0x001A5016 File Offset: 0x001A3216
		public InvalidCookieException() : base(DirectoryStrings.InvalidCookieException)
		{
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x001A5023 File Offset: 0x001A3223
		public InvalidCookieException(Exception innerException) : base(DirectoryStrings.InvalidCookieException, innerException)
		{
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x001A5031 File Offset: 0x001A3231
		protected InvalidCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x001A503B File Offset: 0x001A323B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
