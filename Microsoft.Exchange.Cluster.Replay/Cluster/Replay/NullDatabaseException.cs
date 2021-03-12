using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000491 RID: 1169
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NullDatabaseException : TransientException
	{
		// Token: 0x06002C84 RID: 11396 RVA: 0x000BF855 File Offset: 0x000BDA55
		public NullDatabaseException() : base(ReplayStrings.NullDatabaseException)
		{
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000BF862 File Offset: 0x000BDA62
		public NullDatabaseException(Exception innerException) : base(ReplayStrings.NullDatabaseException, innerException)
		{
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000BF870 File Offset: 0x000BDA70
		protected NullDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000BF87A File Offset: 0x000BDA7A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
