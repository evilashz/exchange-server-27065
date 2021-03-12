using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core
{
	// Token: 0x020000BF RID: 191
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DocProcessCanceledException : OperationFailedException
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00012E23 File Offset: 0x00011023
		public DocProcessCanceledException() : base(Strings.DocProcessCanceled)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00012E30 File Offset: 0x00011030
		public DocProcessCanceledException(Exception innerException) : base(Strings.DocProcessCanceled, innerException)
		{
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00012E3E File Offset: 0x0001103E
		protected DocProcessCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00012E48 File Offset: 0x00011048
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
