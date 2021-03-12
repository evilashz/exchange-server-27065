using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000041 RID: 65
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapAuthenticationException : OperationLevelPermanentException
	{
		// Token: 0x06000138 RID: 312 RVA: 0x00003D90 File Offset: 0x00001F90
		public ImapAuthenticationException(string imapAuthenticationErrorMsg, string authMechanismName, RetryPolicy retryPolicy) : base(CXStrings.ImapAuthenticationErrorMsg(imapAuthenticationErrorMsg, authMechanismName, retryPolicy))
		{
			this.imapAuthenticationErrorMsg = imapAuthenticationErrorMsg;
			this.authMechanismName = authMechanismName;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00003DBA File Offset: 0x00001FBA
		public ImapAuthenticationException(string imapAuthenticationErrorMsg, string authMechanismName, RetryPolicy retryPolicy, Exception innerException) : base(CXStrings.ImapAuthenticationErrorMsg(imapAuthenticationErrorMsg, authMechanismName, retryPolicy), innerException)
		{
			this.imapAuthenticationErrorMsg = imapAuthenticationErrorMsg;
			this.authMechanismName = authMechanismName;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003DE8 File Offset: 0x00001FE8
		protected ImapAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.imapAuthenticationErrorMsg = (string)info.GetValue("imapAuthenticationErrorMsg", typeof(string));
			this.authMechanismName = (string)info.GetValue("authMechanismName", typeof(string));
			this.retryPolicy = (RetryPolicy)info.GetValue("retryPolicy", typeof(RetryPolicy));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00003E60 File Offset: 0x00002060
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("imapAuthenticationErrorMsg", this.imapAuthenticationErrorMsg);
			info.AddValue("authMechanismName", this.authMechanismName);
			info.AddValue("retryPolicy", this.retryPolicy);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00003EAD File Offset: 0x000020AD
		public string ImapAuthenticationErrorMsg
		{
			get
			{
				return this.imapAuthenticationErrorMsg;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003EB5 File Offset: 0x000020B5
		public string AuthMechanismName
		{
			get
			{
				return this.authMechanismName;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00003EBD File Offset: 0x000020BD
		public RetryPolicy RetryPolicy
		{
			get
			{
				return this.retryPolicy;
			}
		}

		// Token: 0x040000D9 RID: 217
		private readonly string imapAuthenticationErrorMsg;

		// Token: 0x040000DA RID: 218
		private readonly string authMechanismName;

		// Token: 0x040000DB RID: 219
		private readonly RetryPolicy retryPolicy;
	}
}
