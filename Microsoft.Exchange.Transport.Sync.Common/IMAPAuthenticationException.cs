using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000036 RID: 54
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IMAPAuthenticationException : IMAPException
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00005AB2 File Offset: 0x00003CB2
		public IMAPAuthenticationException() : base(Strings.IMAPAuthenticationException)
		{
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public IMAPAuthenticationException(Exception innerException) : base(Strings.IMAPAuthenticationException, innerException)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005AD7 File Offset: 0x00003CD7
		protected IMAPAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005AE1 File Offset: 0x00003CE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
