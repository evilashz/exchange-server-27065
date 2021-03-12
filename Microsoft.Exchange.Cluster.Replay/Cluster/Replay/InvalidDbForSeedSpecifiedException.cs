using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200043B RID: 1083
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDbForSeedSpecifiedException : SeedPrepareException
	{
		// Token: 0x06002AB6 RID: 10934 RVA: 0x000BC21B File Offset: 0x000BA41B
		public InvalidDbForSeedSpecifiedException() : base(ReplayStrings.InvalidDbForSeedSpecifiedException)
		{
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x000BC22D File Offset: 0x000BA42D
		public InvalidDbForSeedSpecifiedException(Exception innerException) : base(ReplayStrings.InvalidDbForSeedSpecifiedException, innerException)
		{
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x000BC240 File Offset: 0x000BA440
		protected InvalidDbForSeedSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x000BC24A File Offset: 0x000BA44A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
