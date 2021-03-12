using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EEE RID: 3822
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetCopyStatusRpcException : LocalizedException
	{
		// Token: 0x0600A98F RID: 43407 RVA: 0x0028BF86 File Offset: 0x0028A186
		public GetCopyStatusRpcException(string server, string databaseName, string errorMessage) : base(Strings.GetCopyStatusRpcException(server, databaseName, errorMessage))
		{
			this.server = server;
			this.databaseName = databaseName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A990 RID: 43408 RVA: 0x0028BFAB File Offset: 0x0028A1AB
		public GetCopyStatusRpcException(string server, string databaseName, string errorMessage, Exception innerException) : base(Strings.GetCopyStatusRpcException(server, databaseName, errorMessage), innerException)
		{
			this.server = server;
			this.databaseName = databaseName;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600A991 RID: 43409 RVA: 0x0028BFD4 File Offset: 0x0028A1D4
		protected GetCopyStatusRpcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600A992 RID: 43410 RVA: 0x0028C049 File Offset: 0x0028A249
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x170036F4 RID: 14068
		// (get) Token: 0x0600A993 RID: 43411 RVA: 0x0028C086 File Offset: 0x0028A286
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170036F5 RID: 14069
		// (get) Token: 0x0600A994 RID: 43412 RVA: 0x0028C08E File Offset: 0x0028A28E
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x170036F6 RID: 14070
		// (get) Token: 0x0600A995 RID: 43413 RVA: 0x0028C096 File Offset: 0x0028A296
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x0400605A RID: 24666
		private readonly string server;

		// Token: 0x0400605B RID: 24667
		private readonly string databaseName;

		// Token: 0x0400605C RID: 24668
		private readonly string errorMessage;
	}
}
