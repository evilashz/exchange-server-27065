using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000025 RID: 37
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NullDocumentProcessingContextException : OperationFailedException
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00005723 File Offset: 0x00003923
		public NullDocumentProcessingContextException() : base(Strings.NullDocumentProcessingContext)
		{
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005730 File Offset: 0x00003930
		public NullDocumentProcessingContextException(Exception innerException) : base(Strings.NullDocumentProcessingContext, innerException)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000573E File Offset: 0x0000393E
		protected NullDocumentProcessingContextException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005748 File Offset: 0x00003948
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
