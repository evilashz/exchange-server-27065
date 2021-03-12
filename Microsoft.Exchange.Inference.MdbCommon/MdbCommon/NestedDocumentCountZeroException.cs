using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000026 RID: 38
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NestedDocumentCountZeroException : OperationFailedException
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00005752 File Offset: 0x00003952
		public NestedDocumentCountZeroException() : base(Strings.NestedDocumentCountZero)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000575F File Offset: 0x0000395F
		public NestedDocumentCountZeroException(Exception innerException) : base(Strings.NestedDocumentCountZero, innerException)
		{
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000576D File Offset: 0x0000396D
		protected NestedDocumentCountZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005777 File Offset: 0x00003977
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
