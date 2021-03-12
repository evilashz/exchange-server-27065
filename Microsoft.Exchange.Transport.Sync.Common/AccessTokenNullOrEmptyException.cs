using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200004A RID: 74
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AccessTokenNullOrEmptyException : LocalizedException
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00006159 File Offset: 0x00004359
		public AccessTokenNullOrEmptyException() : base(Strings.AccessTokenNullOrEmpty)
		{
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006166 File Offset: 0x00004366
		public AccessTokenNullOrEmptyException(Exception innerException) : base(Strings.AccessTokenNullOrEmpty, innerException)
		{
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00006174 File Offset: 0x00004374
		protected AccessTokenNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000617E File Offset: 0x0000437E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
