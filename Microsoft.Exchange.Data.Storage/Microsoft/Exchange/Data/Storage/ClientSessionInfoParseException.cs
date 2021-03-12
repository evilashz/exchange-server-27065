using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000101 RID: 257
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ClientSessionInfoParseException : StoragePermanentException
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x00069731 File Offset: 0x00067931
		public ClientSessionInfoParseException() : base(ServerStrings.idClientSessionInfoParseException)
		{
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0006973E File Offset: 0x0006793E
		public ClientSessionInfoParseException(Exception innerException) : base(ServerStrings.idClientSessionInfoParseException, innerException)
		{
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0006974C File Offset: 0x0006794C
		protected ClientSessionInfoParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00069756 File Offset: 0x00067956
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
