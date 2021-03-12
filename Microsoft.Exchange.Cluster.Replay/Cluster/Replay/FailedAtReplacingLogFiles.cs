using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004A5 RID: 1189
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedAtReplacingLogFiles : TransientException
	{
		// Token: 0x06002CF3 RID: 11507 RVA: 0x000C059D File Offset: 0x000BE79D
		public FailedAtReplacingLogFiles() : base(ReplayStrings.FailedAtReplacingLogFiles)
		{
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000C05AA File Offset: 0x000BE7AA
		public FailedAtReplacingLogFiles(Exception innerException) : base(ReplayStrings.FailedAtReplacingLogFiles, innerException)
		{
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000C05B8 File Offset: 0x000BE7B8
		protected FailedAtReplacingLogFiles(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000C05C2 File Offset: 0x000BE7C2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
