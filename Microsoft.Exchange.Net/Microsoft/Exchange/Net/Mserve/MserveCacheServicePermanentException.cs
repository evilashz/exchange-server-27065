using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020000FB RID: 251
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MserveCacheServicePermanentException : LocalizedException
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x0001693A File Offset: 0x00014B3A
		public MserveCacheServicePermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00016943 File Offset: 0x00014B43
		public MserveCacheServicePermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001694D File Offset: 0x00014B4D
		protected MserveCacheServicePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00016957 File Offset: 0x00014B57
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
