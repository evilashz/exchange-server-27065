using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200103A RID: 4154
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxCreationException : LocalizedException
	{
		// Token: 0x0600AFDA RID: 45018 RVA: 0x00294FAD File Offset: 0x002931AD
		public MailboxCreationException(string user, string errorMessage) : base(Strings.ErrorMailboxCreationFailure(user, errorMessage))
		{
			this.user = user;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AFDB RID: 45019 RVA: 0x00294FCA File Offset: 0x002931CA
		public MailboxCreationException(string user, string errorMessage, Exception innerException) : base(Strings.ErrorMailboxCreationFailure(user, errorMessage), innerException)
		{
			this.user = user;
			this.errorMessage = errorMessage;
		}

		// Token: 0x0600AFDC RID: 45020 RVA: 0x00294FE8 File Offset: 0x002931E8
		protected MailboxCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x0600AFDD RID: 45021 RVA: 0x0029503D File Offset: 0x0029323D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x1700380F RID: 14351
		// (get) Token: 0x0600AFDE RID: 45022 RVA: 0x00295069 File Offset: 0x00293269
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17003810 RID: 14352
		// (get) Token: 0x0600AFDF RID: 45023 RVA: 0x00295071 File Offset: 0x00293271
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x04006175 RID: 24949
		private readonly string user;

		// Token: 0x04006176 RID: 24950
		private readonly string errorMessage;
	}
}
