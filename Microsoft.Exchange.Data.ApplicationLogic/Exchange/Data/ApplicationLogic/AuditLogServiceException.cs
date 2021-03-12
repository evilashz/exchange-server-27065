using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200000B RID: 11
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AuditLogServiceException : AuditLogException
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000035B6 File Offset: 0x000017B6
		public AuditLogServiceException(string responseclass, string code, string error) : base(Strings.FailedToAccessAuditLog(responseclass, code, error))
		{
			this.responseclass = responseclass;
			this.code = code;
			this.error = error;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000035DB File Offset: 0x000017DB
		public AuditLogServiceException(string responseclass, string code, string error, Exception innerException) : base(Strings.FailedToAccessAuditLog(responseclass, code, error), innerException)
		{
			this.responseclass = responseclass;
			this.code = code;
			this.error = error;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003604 File Offset: 0x00001804
		protected AuditLogServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.responseclass = (string)info.GetValue("responseclass", typeof(string));
			this.code = (string)info.GetValue("code", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003679 File Offset: 0x00001879
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("responseclass", this.responseclass);
			info.AddValue("code", this.code);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000036B6 File Offset: 0x000018B6
		public string Responseclass
		{
			get
			{
				return this.responseclass;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000036BE File Offset: 0x000018BE
		public string Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000036C6 File Offset: 0x000018C6
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000067 RID: 103
		private readonly string responseclass;

		// Token: 0x04000068 RID: 104
		private readonly string code;

		// Token: 0x04000069 RID: 105
		private readonly string error;
	}
}
