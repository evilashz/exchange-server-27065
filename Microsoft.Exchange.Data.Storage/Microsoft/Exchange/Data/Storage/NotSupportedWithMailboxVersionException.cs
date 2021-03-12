using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000FE RID: 254
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NotSupportedWithMailboxVersionException : StoragePermanentException
	{
		// Token: 0x06001382 RID: 4994 RVA: 0x00069660 File Offset: 0x00067860
		public NotSupportedWithMailboxVersionException() : base(ServerStrings.NotSupportedWithMailboxVersionException)
		{
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0006966D File Offset: 0x0006786D
		public NotSupportedWithMailboxVersionException(Exception innerException) : base(ServerStrings.NotSupportedWithMailboxVersionException, innerException)
		{
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0006967B File Offset: 0x0006787B
		protected NotSupportedWithMailboxVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00069685 File Offset: 0x00067885
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
