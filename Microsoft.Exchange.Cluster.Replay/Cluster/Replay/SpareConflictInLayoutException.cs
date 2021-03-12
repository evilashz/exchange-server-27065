using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000515 RID: 1301
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SpareConflictInLayoutException : DatabaseCopyLayoutException
	{
		// Token: 0x06002F7F RID: 12159 RVA: 0x000C5AD9 File Offset: 0x000C3CD9
		public SpareConflictInLayoutException(int spares) : base(ReplayStrings.SpareConflictInLayoutException(spares))
		{
			this.spares = spares;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000C5AF3 File Offset: 0x000C3CF3
		public SpareConflictInLayoutException(int spares, Exception innerException) : base(ReplayStrings.SpareConflictInLayoutException(spares), innerException)
		{
			this.spares = spares;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000C5B0E File Offset: 0x000C3D0E
		protected SpareConflictInLayoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.spares = (int)info.GetValue("spares", typeof(int));
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000C5B38 File Offset: 0x000C3D38
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("spares", this.spares);
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x000C5B53 File Offset: 0x000C3D53
		public int Spares
		{
			get
			{
				return this.spares;
			}
		}

		// Token: 0x040015D2 RID: 5586
		private readonly int spares;
	}
}
