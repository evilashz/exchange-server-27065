using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.MdbCommon
{
	// Token: 0x0200002A RID: 42
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingMailboxOwnerPropertyException : OperationFailedException
	{
		// Token: 0x060000FB RID: 251 RVA: 0x0000580E File Offset: 0x00003A0E
		public MissingMailboxOwnerPropertyException() : base(Strings.MissingMailboxOwnerProperty)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000581B File Offset: 0x00003A1B
		public MissingMailboxOwnerPropertyException(Exception innerException) : base(Strings.MissingMailboxOwnerProperty, innerException)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005829 File Offset: 0x00003A29
		protected MissingMailboxOwnerPropertyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005833 File Offset: 0x00003A33
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
