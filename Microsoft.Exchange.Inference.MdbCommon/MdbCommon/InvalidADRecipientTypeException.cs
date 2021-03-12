using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x0200002B RID: 43
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidADRecipientTypeException : OperationFailedException
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000583D File Offset: 0x00003A3D
		public InvalidADRecipientTypeException() : base(Strings.InvalidAdRecipient)
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000584A File Offset: 0x00003A4A
		public InvalidADRecipientTypeException(Exception innerException) : base(Strings.InvalidAdRecipient, innerException)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005858 File Offset: 0x00003A58
		protected InvalidADRecipientTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005862 File Offset: 0x00003A62
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
