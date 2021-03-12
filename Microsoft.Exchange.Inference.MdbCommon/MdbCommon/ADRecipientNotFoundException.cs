using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x0200002C RID: 44
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ADRecipientNotFoundException : OperationFailedException
	{
		// Token: 0x06000103 RID: 259 RVA: 0x0000586C File Offset: 0x00003A6C
		public ADRecipientNotFoundException() : base(Strings.AdRecipientNotFound)
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005879 File Offset: 0x00003A79
		public ADRecipientNotFoundException(Exception innerException) : base(Strings.AdRecipientNotFound, innerException)
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005887 File Offset: 0x00003A87
		protected ADRecipientNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005891 File Offset: 0x00003A91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
