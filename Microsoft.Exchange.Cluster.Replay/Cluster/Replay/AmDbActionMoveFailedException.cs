using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200046C RID: 1132
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionMoveFailedException : AmDbActionException
	{
		// Token: 0x06002BB3 RID: 11187 RVA: 0x000BDDED File Offset: 0x000BBFED
		public AmDbActionMoveFailedException() : base(ReplayStrings.AmDbActionMoveFailedException)
		{
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000BDDFF File Offset: 0x000BBFFF
		public AmDbActionMoveFailedException(Exception innerException) : base(ReplayStrings.AmDbActionMoveFailedException, innerException)
		{
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000BDE12 File Offset: 0x000BC012
		protected AmDbActionMoveFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000BDE1C File Offset: 0x000BC01C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
