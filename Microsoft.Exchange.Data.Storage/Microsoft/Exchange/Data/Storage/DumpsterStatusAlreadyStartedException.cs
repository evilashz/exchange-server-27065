using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C9 RID: 201
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DumpsterStatusAlreadyStartedException : LocalizedException
	{
		// Token: 0x0600126A RID: 4714 RVA: 0x00067A1D File Offset: 0x00065C1D
		public DumpsterStatusAlreadyStartedException() : base(ServerStrings.DumpsterStatusAlreadyStartedException)
		{
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00067A2A File Offset: 0x00065C2A
		public DumpsterStatusAlreadyStartedException(Exception innerException) : base(ServerStrings.DumpsterStatusAlreadyStartedException, innerException)
		{
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00067A38 File Offset: 0x00065C38
		protected DumpsterStatusAlreadyStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00067A42 File Offset: 0x00065C42
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
