using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000C3 RID: 195
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuthenticationException : LocalizedException
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x00012515 File Offset: 0x00010715
		public AuthenticationException() : base(AuthenticationStrings.AuthenticationException)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012522 File Offset: 0x00010722
		public AuthenticationException(Exception innerException) : base(AuthenticationStrings.AuthenticationException, innerException)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012530 File Offset: 0x00010730
		protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001253A File Offset: 0x0001073A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
