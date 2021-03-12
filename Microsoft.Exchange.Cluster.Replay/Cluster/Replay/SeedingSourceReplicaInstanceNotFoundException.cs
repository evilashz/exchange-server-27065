using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000417 RID: 1047
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeedingSourceReplicaInstanceNotFoundException : LocalizedException
	{
		// Token: 0x060029E3 RID: 10723 RVA: 0x000BA68E File Offset: 0x000B888E
		public SeedingSourceReplicaInstanceNotFoundException(Guid guid, string sourceServer) : base(ReplayStrings.SeedingSourceReplicaInstanceNotFoundException(guid, sourceServer))
		{
			this.guid = guid;
			this.sourceServer = sourceServer;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000BA6AB File Offset: 0x000B88AB
		public SeedingSourceReplicaInstanceNotFoundException(Guid guid, string sourceServer, Exception innerException) : base(ReplayStrings.SeedingSourceReplicaInstanceNotFoundException(guid, sourceServer), innerException)
		{
			this.guid = guid;
			this.sourceServer = sourceServer;
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000BA6CC File Offset: 0x000B88CC
		protected SeedingSourceReplicaInstanceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x000BA721 File Offset: 0x000B8921
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
			info.AddValue("sourceServer", this.sourceServer);
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000BA752 File Offset: 0x000B8952
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000BA75A File Offset: 0x000B895A
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x0400142E RID: 5166
		private readonly Guid guid;

		// Token: 0x0400142F RID: 5167
		private readonly string sourceServer;
	}
}
