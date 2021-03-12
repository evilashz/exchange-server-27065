using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000037 RID: 55
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IMAPGmailNotSupportedException : LocalizedException
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00005AEB File Offset: 0x00003CEB
		public IMAPGmailNotSupportedException() : base(Strings.IMAPGmailNotSupportedException)
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public IMAPGmailNotSupportedException(Exception innerException) : base(Strings.IMAPGmailNotSupportedException, innerException)
		{
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005B06 File Offset: 0x00003D06
		protected IMAPGmailNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005B10 File Offset: 0x00003D10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
