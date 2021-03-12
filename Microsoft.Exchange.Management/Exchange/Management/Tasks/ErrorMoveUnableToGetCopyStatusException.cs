using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EEA RID: 3818
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorMoveUnableToGetCopyStatusException : LocalizedException
	{
		// Token: 0x0600A978 RID: 43384 RVA: 0x0028BCA5 File Offset: 0x00289EA5
		public ErrorMoveUnableToGetCopyStatusException(string server, string errorMsg) : base(Strings.ErrorMoveUnableToGetCopyStatusException(server, errorMsg))
		{
			this.server = server;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A979 RID: 43385 RVA: 0x0028BCC2 File Offset: 0x00289EC2
		public ErrorMoveUnableToGetCopyStatusException(string server, string errorMsg, Exception innerException) : base(Strings.ErrorMoveUnableToGetCopyStatusException(server, errorMsg), innerException)
		{
			this.server = server;
			this.errorMsg = errorMsg;
		}

		// Token: 0x0600A97A RID: 43386 RVA: 0x0028BCE0 File Offset: 0x00289EE0
		protected ErrorMoveUnableToGetCopyStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.server = (string)info.GetValue("server", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x0600A97B RID: 43387 RVA: 0x0028BD35 File Offset: 0x00289F35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("server", this.server);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x170036ED RID: 14061
		// (get) Token: 0x0600A97C RID: 43388 RVA: 0x0028BD61 File Offset: 0x00289F61
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170036EE RID: 14062
		// (get) Token: 0x0600A97D RID: 43389 RVA: 0x0028BD69 File Offset: 0x00289F69
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04006053 RID: 24659
		private readonly string server;

		// Token: 0x04006054 RID: 24660
		private readonly string errorMsg;
	}
}
