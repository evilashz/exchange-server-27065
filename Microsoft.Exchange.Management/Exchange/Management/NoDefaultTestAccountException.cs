using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x0200112D RID: 4397
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoDefaultTestAccountException : LocalizedException
	{
		// Token: 0x0600B4CF RID: 46287 RVA: 0x0029D5AE File Offset: 0x0029B7AE
		public NoDefaultTestAccountException() : base(Strings.NoDefaultTestAccount)
		{
		}

		// Token: 0x0600B4D0 RID: 46288 RVA: 0x0029D5BB File Offset: 0x0029B7BB
		public NoDefaultTestAccountException(Exception innerException) : base(Strings.NoDefaultTestAccount, innerException)
		{
		}

		// Token: 0x0600B4D1 RID: 46289 RVA: 0x0029D5C9 File Offset: 0x0029B7C9
		protected NoDefaultTestAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B4D2 RID: 46290 RVA: 0x0029D5D3 File Offset: 0x0029B7D3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
