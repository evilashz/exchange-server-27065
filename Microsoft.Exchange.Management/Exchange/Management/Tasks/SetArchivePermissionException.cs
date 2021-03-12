using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FC2 RID: 4034
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SetArchivePermissionException : LocalizedException
	{
		// Token: 0x0600ADA0 RID: 44448 RVA: 0x00291F89 File Offset: 0x00290189
		public SetArchivePermissionException(string user, Exception exception) : base(Strings.SetArchivePermissionException(user, exception))
		{
			this.user = user;
			this.exception = exception;
		}

		// Token: 0x0600ADA1 RID: 44449 RVA: 0x00291FA6 File Offset: 0x002901A6
		public SetArchivePermissionException(string user, Exception exception, Exception innerException) : base(Strings.SetArchivePermissionException(user, exception), innerException)
		{
			this.user = user;
			this.exception = exception;
		}

		// Token: 0x0600ADA2 RID: 44450 RVA: 0x00291FC4 File Offset: 0x002901C4
		protected SetArchivePermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.exception = (Exception)info.GetValue("exception", typeof(Exception));
		}

		// Token: 0x0600ADA3 RID: 44451 RVA: 0x00292019 File Offset: 0x00290219
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("exception", this.exception);
		}

		// Token: 0x170037B5 RID: 14261
		// (get) Token: 0x0600ADA4 RID: 44452 RVA: 0x00292045 File Offset: 0x00290245
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x170037B6 RID: 14262
		// (get) Token: 0x0600ADA5 RID: 44453 RVA: 0x0029204D File Offset: 0x0029024D
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x0400611B RID: 24859
		private readonly string user;

		// Token: 0x0400611C RID: 24860
		private readonly Exception exception;
	}
}
