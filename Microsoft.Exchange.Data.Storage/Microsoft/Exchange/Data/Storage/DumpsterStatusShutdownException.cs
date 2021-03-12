using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000C8 RID: 200
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DumpsterStatusShutdownException : LocalizedException
	{
		// Token: 0x06001266 RID: 4710 RVA: 0x000679EE File Offset: 0x00065BEE
		public DumpsterStatusShutdownException() : base(ServerStrings.DumpsterStatusShutdownException)
		{
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000679FB File Offset: 0x00065BFB
		public DumpsterStatusShutdownException(Exception innerException) : base(ServerStrings.DumpsterStatusShutdownException, innerException)
		{
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00067A09 File Offset: 0x00065C09
		protected DumpsterStatusShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00067A13 File Offset: 0x00065C13
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
