using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE9 RID: 2793
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RealmFormatInvalidException : LocalizedException
	{
		// Token: 0x0600813D RID: 33085 RVA: 0x001A649B File Offset: 0x001A469B
		public RealmFormatInvalidException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600813E RID: 33086 RVA: 0x001A64A4 File Offset: 0x001A46A4
		public RealmFormatInvalidException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600813F RID: 33087 RVA: 0x001A64AE File Offset: 0x001A46AE
		protected RealmFormatInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008140 RID: 33088 RVA: 0x001A64B8 File Offset: 0x001A46B8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
