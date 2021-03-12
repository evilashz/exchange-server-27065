using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000454 RID: 1108
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GranularReplicationTerminatedException : TransientException
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x000BD1E9 File Offset: 0x000BB3E9
		public GranularReplicationTerminatedException(string reason) : base(ReplayStrings.GranularReplicationTerminated(reason))
		{
			this.reason = reason;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000BD1FE File Offset: 0x000BB3FE
		public GranularReplicationTerminatedException(string reason, Exception innerException) : base(ReplayStrings.GranularReplicationTerminated(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000BD214 File Offset: 0x000BB414
		protected GranularReplicationTerminatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000BD23E File Offset: 0x000BB43E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000BD259 File Offset: 0x000BB459
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04001493 RID: 5267
		private readonly string reason;
	}
}
