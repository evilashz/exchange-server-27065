using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000035 RID: 53
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoFASTNodesFoundException : ComponentFailedPermanentException
	{
		// Token: 0x060002A0 RID: 672 RVA: 0x0000F360 File Offset: 0x0000D560
		public NoFASTNodesFoundException() : base(Strings.NoFASTNodesFound)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000F36D File Offset: 0x0000D56D
		public NoFASTNodesFoundException(Exception innerException) : base(Strings.NoFASTNodesFound, innerException)
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000F37B File Offset: 0x0000D57B
		protected NoFASTNodesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F385 File Offset: 0x0000D585
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
