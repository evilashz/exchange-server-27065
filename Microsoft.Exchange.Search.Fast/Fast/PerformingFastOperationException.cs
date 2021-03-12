using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000036 RID: 54
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PerformingFastOperationException : ComponentFailedPermanentException
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0000F38F File Offset: 0x0000D58F
		public PerformingFastOperationException() : base(Strings.PerformingFastOperationException)
		{
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000F39C File Offset: 0x0000D59C
		public PerformingFastOperationException(Exception innerException) : base(Strings.PerformingFastOperationException, innerException)
		{
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000F3AA File Offset: 0x0000D5AA
		protected PerformingFastOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
