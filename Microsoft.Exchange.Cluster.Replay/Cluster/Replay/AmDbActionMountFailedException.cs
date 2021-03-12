using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200046A RID: 1130
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbActionMountFailedException : AmDbActionException
	{
		// Token: 0x06002BAB RID: 11179 RVA: 0x000BDD7B File Offset: 0x000BBF7B
		public AmDbActionMountFailedException() : base(ReplayStrings.AmDbActionMountFailedException)
		{
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000BDD8D File Offset: 0x000BBF8D
		public AmDbActionMountFailedException(Exception innerException) : base(ReplayStrings.AmDbActionMountFailedException, innerException)
		{
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000BDDA0 File Offset: 0x000BBFA0
		protected AmDbActionMountFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000BDDAA File Offset: 0x000BBFAA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
