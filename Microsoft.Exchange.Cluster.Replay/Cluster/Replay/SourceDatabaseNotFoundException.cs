using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003BB RID: 955
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceDatabaseNotFoundException : TransientException
	{
		// Token: 0x060027FE RID: 10238 RVA: 0x000B6F76 File Offset: 0x000B5176
		public SourceDatabaseNotFoundException(Guid g, string sourceServer) : base(ReplayStrings.SourceDatabaseNotFound(g, sourceServer))
		{
			this.g = g;
			this.sourceServer = sourceServer;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000B6F93 File Offset: 0x000B5193
		public SourceDatabaseNotFoundException(Guid g, string sourceServer, Exception innerException) : base(ReplayStrings.SourceDatabaseNotFound(g, sourceServer), innerException)
		{
			this.g = g;
			this.sourceServer = sourceServer;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000B6FB4 File Offset: 0x000B51B4
		protected SourceDatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.g = (Guid)info.GetValue("g", typeof(Guid));
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000B7009 File Offset: 0x000B5209
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("g", this.g);
			info.AddValue("sourceServer", this.sourceServer);
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x000B703A File Offset: 0x000B523A
		public Guid G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002803 RID: 10243 RVA: 0x000B7042 File Offset: 0x000B5242
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x040013B9 RID: 5049
		private readonly Guid g;

		// Token: 0x040013BA RID: 5050
		private readonly string sourceServer;
	}
}
