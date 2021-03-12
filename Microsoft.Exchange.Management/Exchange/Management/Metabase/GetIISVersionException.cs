using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DE0 RID: 3552
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetIISVersionException : DataSourceOperationException
	{
		// Token: 0x0600A44E RID: 42062 RVA: 0x00284011 File Offset: 0x00282211
		public GetIISVersionException(string server) : base(Strings.GetIISVersionException(server))
		{
			this.server = server;
		}

		// Token: 0x0600A44F RID: 42063 RVA: 0x00284026 File Offset: 0x00282226
		public GetIISVersionException(string server, Exception innerException) : base(Strings.GetIISVersionException(server), innerException)
		{
			this.server = server;
		}

		// Token: 0x0600A450 RID: 42064 RVA: 0x0028403C File Offset: 0x0028223C
		protected GetIISVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
		}

		// Token: 0x0600A451 RID: 42065 RVA: 0x00284066 File Offset: 0x00282266
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
		}

		// Token: 0x170035EB RID: 13803
		// (get) Token: 0x0600A452 RID: 42066 RVA: 0x00284081 File Offset: 0x00282281
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x04005F51 RID: 24401
		private readonly string server;
	}
}
