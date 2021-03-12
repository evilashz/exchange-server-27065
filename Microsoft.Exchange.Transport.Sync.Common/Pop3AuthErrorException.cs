using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200003C RID: 60
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class Pop3AuthErrorException : LocalizedException
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00005C47 File Offset: 0x00003E47
		public Pop3AuthErrorException() : base(Strings.Pop3AuthErrorException)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005C54 File Offset: 0x00003E54
		public Pop3AuthErrorException(Exception innerException) : base(Strings.Pop3AuthErrorException, innerException)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005C62 File Offset: 0x00003E62
		protected Pop3AuthErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005C6C File Offset: 0x00003E6C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
