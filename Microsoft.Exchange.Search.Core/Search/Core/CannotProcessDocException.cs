using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000C0 RID: 192
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotProcessDocException : OperationFailedException
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00012E52 File Offset: 0x00011052
		public CannotProcessDocException() : base(Strings.CannotProcessDoc)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00012E5F File Offset: 0x0001105F
		public CannotProcessDocException(Exception innerException) : base(Strings.CannotProcessDoc, innerException)
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00012E6D File Offset: 0x0001106D
		protected CannotProcessDocException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00012E77 File Offset: 0x00011077
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
