using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021F RID: 543
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoValidResultsException : LocalizedException
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x00039EE4 File Offset: 0x000380E4
		public NoValidResultsException() : base(Strings.NoValidResultsException)
		{
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00039EF1 File Offset: 0x000380F1
		public NoValidResultsException(Exception innerException) : base(Strings.NoValidResultsException, innerException)
		{
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00039EFF File Offset: 0x000380FF
		protected NoValidResultsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00039F09 File Offset: 0x00038109
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
