using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000038 RID: 56
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IMAPInvalidItemException : IMAPException
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00005B1A File Offset: 0x00003D1A
		public IMAPInvalidItemException() : base(Strings.IMAPInvalidItemException)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005B2C File Offset: 0x00003D2C
		public IMAPInvalidItemException(Exception innerException) : base(Strings.IMAPInvalidItemException, innerException)
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005B3F File Offset: 0x00003D3F
		protected IMAPInvalidItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005B49 File Offset: 0x00003D49
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
