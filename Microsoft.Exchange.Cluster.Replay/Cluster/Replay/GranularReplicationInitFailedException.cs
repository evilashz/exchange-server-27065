using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000456 RID: 1110
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GranularReplicationInitFailedException : TransientException
	{
		// Token: 0x06002B45 RID: 11077 RVA: 0x000BD290 File Offset: 0x000BB490
		public GranularReplicationInitFailedException(string reason) : base(ReplayStrings.GranularReplicationInitFailed(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000BD2A5 File Offset: 0x000BB4A5
		public GranularReplicationInitFailedException(string reason, Exception innerException) : base(ReplayStrings.GranularReplicationInitFailed(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000BD2BB File Offset: 0x000BB4BB
		protected GranularReplicationInitFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000BD2E5 File Offset: 0x000BB4E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000BD300 File Offset: 0x000BB500
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04001494 RID: 5268
		private readonly string reason;
	}
}
