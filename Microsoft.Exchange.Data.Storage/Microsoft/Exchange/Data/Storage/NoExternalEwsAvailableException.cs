using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000F6 RID: 246
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class NoExternalEwsAvailableException : StoragePermanentException
	{
		// Token: 0x0600135F RID: 4959 RVA: 0x0006940D File Offset: 0x0006760D
		public NoExternalEwsAvailableException() : base(ServerStrings.NoExternalEwsAvailableException)
		{
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0006941A File Offset: 0x0006761A
		public NoExternalEwsAvailableException(Exception innerException) : base(ServerStrings.NoExternalEwsAvailableException, innerException)
		{
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00069428 File Offset: 0x00067628
		protected NoExternalEwsAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00069432 File Offset: 0x00067632
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
