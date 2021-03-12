using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200113D RID: 4413
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToCreateAdminAuditLogItemException : AdminAuditLogException
	{
		// Token: 0x0600B515 RID: 46357 RVA: 0x0029DA34 File Offset: 0x0029BC34
		public FailedToCreateAdminAuditLogItemException(string responseclass, string code, string error) : base(Strings.FailedToCreateAdminAuditLogItem(responseclass, code, error))
		{
			this.responseclass = responseclass;
			this.code = code;
			this.error = error;
		}

		// Token: 0x0600B516 RID: 46358 RVA: 0x0029DA59 File Offset: 0x0029BC59
		public FailedToCreateAdminAuditLogItemException(string responseclass, string code, string error, Exception innerException) : base(Strings.FailedToCreateAdminAuditLogItem(responseclass, code, error), innerException)
		{
			this.responseclass = responseclass;
			this.code = code;
			this.error = error;
		}

		// Token: 0x0600B517 RID: 46359 RVA: 0x0029DA80 File Offset: 0x0029BC80
		protected FailedToCreateAdminAuditLogItemException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.responseclass = (string)info.GetValue("responseclass", typeof(string));
			this.code = (string)info.GetValue("code", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B518 RID: 46360 RVA: 0x0029DAF5 File Offset: 0x0029BCF5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("responseclass", this.responseclass);
			info.AddValue("code", this.code);
			info.AddValue("error", this.error);
		}

		// Token: 0x1700393E RID: 14654
		// (get) Token: 0x0600B519 RID: 46361 RVA: 0x0029DB32 File Offset: 0x0029BD32
		public string Responseclass
		{
			get
			{
				return this.responseclass;
			}
		}

		// Token: 0x1700393F RID: 14655
		// (get) Token: 0x0600B51A RID: 46362 RVA: 0x0029DB3A File Offset: 0x0029BD3A
		public string Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x17003940 RID: 14656
		// (get) Token: 0x0600B51B RID: 46363 RVA: 0x0029DB42 File Offset: 0x0029BD42
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040062A4 RID: 25252
		private readonly string responseclass;

		// Token: 0x040062A5 RID: 25253
		private readonly string code;

		// Token: 0x040062A6 RID: 25254
		private readonly string error;
	}
}
