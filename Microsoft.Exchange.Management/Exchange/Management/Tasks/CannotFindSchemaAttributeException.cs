using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E1E RID: 3614
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindSchemaAttributeException : LocalizedException
	{
		// Token: 0x0600A598 RID: 42392 RVA: 0x00286451 File Offset: 0x00284651
		public CannotFindSchemaAttributeException(string attr, string schemaDN, string server) : base(Strings.CannotFindSchemaAttributeException(attr, schemaDN, server))
		{
			this.attr = attr;
			this.schemaDN = schemaDN;
			this.server = server;
		}

		// Token: 0x0600A599 RID: 42393 RVA: 0x00286476 File Offset: 0x00284676
		public CannotFindSchemaAttributeException(string attr, string schemaDN, string server, Exception innerException) : base(Strings.CannotFindSchemaAttributeException(attr, schemaDN, server), innerException)
		{
			this.attr = attr;
			this.schemaDN = schemaDN;
			this.server = server;
		}

		// Token: 0x0600A59A RID: 42394 RVA: 0x002864A0 File Offset: 0x002846A0
		protected CannotFindSchemaAttributeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.attr = (string)info.GetValue("attr", typeof(string));
			this.schemaDN = (string)info.GetValue("schemaDN", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600A59B RID: 42395 RVA: 0x00286515 File Offset: 0x00284715
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("attr", this.attr);
			info.AddValue("schemaDN", this.schemaDN);
			info.AddValue("server", this.server);
		}

		// Token: 0x1700363D RID: 13885
		// (get) Token: 0x0600A59C RID: 42396 RVA: 0x00286552 File Offset: 0x00284752
		public string Attr
		{
			get
			{
				return this.attr;
			}
		}

		// Token: 0x1700363E RID: 13886
		// (get) Token: 0x0600A59D RID: 42397 RVA: 0x0028655A File Offset: 0x0028475A
		public string SchemaDN
		{
			get
			{
				return this.schemaDN;
			}
		}

		// Token: 0x1700363F RID: 13887
		// (get) Token: 0x0600A59E RID: 42398 RVA: 0x00286562 File Offset: 0x00284762
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04005FA3 RID: 24483
		private readonly string attr;

		// Token: 0x04005FA4 RID: 24484
		private readonly string schemaDN;

		// Token: 0x04005FA5 RID: 24485
		private readonly string server;
	}
}
