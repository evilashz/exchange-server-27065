using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020000FA RID: 250
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MserveCacheServiceTransientException : TransientException
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x00016913 File Offset: 0x00014B13
		public MserveCacheServiceTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001691C File Offset: 0x00014B1C
		public MserveCacheServiceTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00016926 File Offset: 0x00014B26
		protected MserveCacheServiceTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00016930 File Offset: 0x00014B30
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
