using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x02000028 RID: 40
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidDocumentInTrainingSetException : OperationFailedException
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000057B0 File Offset: 0x000039B0
		public InvalidDocumentInTrainingSetException() : base(Strings.InvalidDocumentInTrainingSet)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000057BD File Offset: 0x000039BD
		public InvalidDocumentInTrainingSetException(Exception innerException) : base(Strings.InvalidDocumentInTrainingSet, innerException)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000057CB File Offset: 0x000039CB
		protected InvalidDocumentInTrainingSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000057D5 File Offset: 0x000039D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
