using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E1F RID: 3615
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotFindSchemaClassException : LocalizedException
	{
		// Token: 0x0600A59F RID: 42399 RVA: 0x0028656A File Offset: 0x0028476A
		public CannotFindSchemaClassException(string objclass, string schemaDN, string server) : base(Strings.CannotFindSchemaClassException(objclass, schemaDN, server))
		{
			this.objclass = objclass;
			this.schemaDN = schemaDN;
			this.server = server;
		}

		// Token: 0x0600A5A0 RID: 42400 RVA: 0x0028658F File Offset: 0x0028478F
		public CannotFindSchemaClassException(string objclass, string schemaDN, string server, Exception innerException) : base(Strings.CannotFindSchemaClassException(objclass, schemaDN, server), innerException)
		{
			this.objclass = objclass;
			this.schemaDN = schemaDN;
			this.server = server;
		}

		// Token: 0x0600A5A1 RID: 42401 RVA: 0x002865B8 File Offset: 0x002847B8
		protected CannotFindSchemaClassException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objclass = (string)info.GetValue("objclass", typeof(string));
			this.schemaDN = (string)info.GetValue("schemaDN", typeof(string));
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600A5A2 RID: 42402 RVA: 0x0028662D File Offset: 0x0028482D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objclass", this.objclass);
			info.AddValue("schemaDN", this.schemaDN);
			info.AddValue("server", this.server);
		}

		// Token: 0x17003640 RID: 13888
		// (get) Token: 0x0600A5A3 RID: 42403 RVA: 0x0028666A File Offset: 0x0028486A
		public string Objclass
		{
			get
			{
				return this.objclass;
			}
		}

		// Token: 0x17003641 RID: 13889
		// (get) Token: 0x0600A5A4 RID: 42404 RVA: 0x00286672 File Offset: 0x00284872
		public string SchemaDN
		{
			get
			{
				return this.schemaDN;
			}
		}

		// Token: 0x17003642 RID: 13890
		// (get) Token: 0x0600A5A5 RID: 42405 RVA: 0x0028667A File Offset: 0x0028487A
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04005FA6 RID: 24486
		private readonly string objclass;

		// Token: 0x04005FA7 RID: 24487
		private readonly string schemaDN;

		// Token: 0x04005FA8 RID: 24488
		private readonly string server;
	}
}
