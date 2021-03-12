using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotAccessOptionsWithBEParamOrCookieException : LocalizedException
	{
		// Token: 0x06001877 RID: 6263 RVA: 0x0004B877 File Offset: 0x00049A77
		public CannotAccessOptionsWithBEParamOrCookieException() : base(Strings.CannotAccessOptionsWithBEParamOrCookieMessage)
		{
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0004B884 File Offset: 0x00049A84
		public CannotAccessOptionsWithBEParamOrCookieException(Exception innerException) : base(Strings.CannotAccessOptionsWithBEParamOrCookieMessage, innerException)
		{
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0004B892 File Offset: 0x00049A92
		protected CannotAccessOptionsWithBEParamOrCookieException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0004B89C File Offset: 0x00049A9C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
