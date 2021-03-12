using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000051 RID: 81
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SourceDatabaseNotFoundException : TransientException
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000952E File Offset: 0x0000772E
		public SourceDatabaseNotFoundException(Guid g, string sourceServer) : base(Strings.SourceDatabaseNotFound(g, sourceServer))
		{
			this.g = g;
			this.sourceServer = sourceServer;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000954B File Offset: 0x0000774B
		public SourceDatabaseNotFoundException(Guid g, string sourceServer, Exception innerException) : base(Strings.SourceDatabaseNotFound(g, sourceServer), innerException)
		{
			this.g = g;
			this.sourceServer = sourceServer;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000956C File Offset: 0x0000776C
		protected SourceDatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.g = (Guid)info.GetValue("g", typeof(Guid));
			this.sourceServer = (string)info.GetValue("sourceServer", typeof(string));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000095C1 File Offset: 0x000077C1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("g", this.g);
			info.AddValue("sourceServer", this.sourceServer);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000095F2 File Offset: 0x000077F2
		public Guid G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000095FA File Offset: 0x000077FA
		public string SourceServer
		{
			get
			{
				return this.sourceServer;
			}
		}

		// Token: 0x0400016A RID: 362
		private readonly Guid g;

		// Token: 0x0400016B RID: 363
		private readonly string sourceServer;
	}
}
