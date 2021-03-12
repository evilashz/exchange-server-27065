using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000492 RID: 1170
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullDbCopyException : TransientException
	{
		// Token: 0x06002C88 RID: 11400 RVA: 0x000BF884 File Offset: 0x000BDA84
		public NullDbCopyException() : base(ReplayStrings.NullDbCopyException)
		{
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000BF891 File Offset: 0x000BDA91
		public NullDbCopyException(Exception innerException) : base(ReplayStrings.NullDbCopyException, innerException)
		{
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000BF89F File Offset: 0x000BDA9F
		protected NullDbCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000BF8A9 File Offset: 0x000BDAA9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
