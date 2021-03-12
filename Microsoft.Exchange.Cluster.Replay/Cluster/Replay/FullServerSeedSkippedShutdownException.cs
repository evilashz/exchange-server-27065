using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200044D RID: 1101
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FullServerSeedSkippedShutdownException : SeederServerException
	{
		// Token: 0x06002B15 RID: 11029 RVA: 0x000BCD06 File Offset: 0x000BAF06
		public FullServerSeedSkippedShutdownException() : base(ReplayStrings.FullServerSeedSkippedShutdownException)
		{
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000BCD18 File Offset: 0x000BAF18
		public FullServerSeedSkippedShutdownException(Exception innerException) : base(ReplayStrings.FullServerSeedSkippedShutdownException, innerException)
		{
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000BCD2B File Offset: 0x000BAF2B
		protected FullServerSeedSkippedShutdownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000BCD35 File Offset: 0x000BAF35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
