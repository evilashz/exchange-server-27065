using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001DE RID: 478
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMGrayException : LocalizedException
	{
		// Token: 0x06000F77 RID: 3959 RVA: 0x000367CE File Offset: 0x000349CE
		public UMGrayException() : base(Strings.UMGray)
		{
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x000367DB File Offset: 0x000349DB
		public UMGrayException(Exception innerException) : base(Strings.UMGray, innerException)
		{
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x000367E9 File Offset: 0x000349E9
		protected UMGrayException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x000367F3 File Offset: 0x000349F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
