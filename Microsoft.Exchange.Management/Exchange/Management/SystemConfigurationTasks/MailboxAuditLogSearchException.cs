using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02001141 RID: 4417
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxAuditLogSearchException : LocalizedException
	{
		// Token: 0x0600B528 RID: 46376 RVA: 0x0029DBD7 File Offset: 0x0029BDD7
		public MailboxAuditLogSearchException(string error, Exception ex) : base(Strings.ErrorMailboxAuditLogSearchFailed(error, ex))
		{
			this.error = error;
			this.ex = ex;
		}

		// Token: 0x0600B529 RID: 46377 RVA: 0x0029DBF4 File Offset: 0x0029BDF4
		public MailboxAuditLogSearchException(string error, Exception ex, Exception innerException) : base(Strings.ErrorMailboxAuditLogSearchFailed(error, ex), innerException)
		{
			this.error = error;
			this.ex = ex;
		}

		// Token: 0x0600B52A RID: 46378 RVA: 0x0029DC14 File Offset: 0x0029BE14
		protected MailboxAuditLogSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
			this.ex = (Exception)info.GetValue("ex", typeof(Exception));
		}

		// Token: 0x0600B52B RID: 46379 RVA: 0x0029DC69 File Offset: 0x0029BE69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003941 RID: 14657
		// (get) Token: 0x0600B52C RID: 46380 RVA: 0x0029DC95 File Offset: 0x0029BE95
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17003942 RID: 14658
		// (get) Token: 0x0600B52D RID: 46381 RVA: 0x0029DC9D File Offset: 0x0029BE9D
		public Exception Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040062A7 RID: 25255
		private readonly string error;

		// Token: 0x040062A8 RID: 25256
		private readonly Exception ex;
	}
}
