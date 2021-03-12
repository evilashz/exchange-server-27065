using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200044C RID: 1100
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FullServerSeedInProgressException : SeederServerException
	{
		// Token: 0x06002B11 RID: 11025 RVA: 0x000BCCCD File Offset: 0x000BAECD
		public FullServerSeedInProgressException() : base(ReplayStrings.FullServerSeedInProgressException)
		{
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000BCCDF File Offset: 0x000BAEDF
		public FullServerSeedInProgressException(Exception innerException) : base(ReplayStrings.FullServerSeedInProgressException, innerException)
		{
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000BCCF2 File Offset: 0x000BAEF2
		protected FullServerSeedInProgressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000BCCFC File Offset: 0x000BAEFC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
