using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200011C RID: 284
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RPCOperationAbortedBecauseOfAnotherRPCThreadException : LocalizedException
	{
		// Token: 0x0600141A RID: 5146 RVA: 0x0006A3F5 File Offset: 0x000685F5
		public RPCOperationAbortedBecauseOfAnotherRPCThreadException() : base(ServerStrings.RPCOperationAbortedBecauseOfAnotherRPCThread)
		{
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0006A402 File Offset: 0x00068602
		public RPCOperationAbortedBecauseOfAnotherRPCThreadException(Exception innerException) : base(ServerStrings.RPCOperationAbortedBecauseOfAnotherRPCThread, innerException)
		{
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0006A410 File Offset: 0x00068610
		protected RPCOperationAbortedBecauseOfAnotherRPCThreadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0006A41A File Offset: 0x0006861A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
