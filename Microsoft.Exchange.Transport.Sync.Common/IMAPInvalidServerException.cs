using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000039 RID: 57
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class IMAPInvalidServerException : IMAPException
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x00005B53 File Offset: 0x00003D53
		public IMAPInvalidServerException() : base(Strings.IMAPInvalidServerException)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005B65 File Offset: 0x00003D65
		public IMAPInvalidServerException(Exception innerException) : base(Strings.IMAPInvalidServerException, innerException)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00005B78 File Offset: 0x00003D78
		protected IMAPInvalidServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005B82 File Offset: 0x00003D82
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
