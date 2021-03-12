using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000188 RID: 392
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorOnlyAllowInEopException : LocalizedException
	{
		// Token: 0x06000F98 RID: 3992 RVA: 0x0003665F File Offset: 0x0003485F
		public ErrorOnlyAllowInEopException() : base(Strings.ErrorOnlyAllowInEop)
		{
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003666C File Offset: 0x0003486C
		public ErrorOnlyAllowInEopException(Exception innerException) : base(Strings.ErrorOnlyAllowInEop, innerException)
		{
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003667A File Offset: 0x0003487A
		protected ErrorOnlyAllowInEopException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x00036684 File Offset: 0x00034884
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
