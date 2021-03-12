using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000045 RID: 69
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ImapUnsupportedAuthenticationException : OperationLevelTransientException
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00004102 File Offset: 0x00002302
		public ImapUnsupportedAuthenticationException(string authErrorMsg, string authMechanismName, RetryPolicy retryPolicy) : base(CXStrings.ImapUnsupportedAuthenticationErrorMsg(authErrorMsg, authMechanismName, retryPolicy))
		{
			this.authErrorMsg = authErrorMsg;
			this.authMechanismName = authMechanismName;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000412C File Offset: 0x0000232C
		public ImapUnsupportedAuthenticationException(string authErrorMsg, string authMechanismName, RetryPolicy retryPolicy, Exception innerException) : base(CXStrings.ImapUnsupportedAuthenticationErrorMsg(authErrorMsg, authMechanismName, retryPolicy), innerException)
		{
			this.authErrorMsg = authErrorMsg;
			this.authMechanismName = authMechanismName;
			this.retryPolicy = retryPolicy;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004158 File Offset: 0x00002358
		protected ImapUnsupportedAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.authErrorMsg = (string)info.GetValue("authErrorMsg", typeof(string));
			this.authMechanismName = (string)info.GetValue("authMechanismName", typeof(string));
			this.retryPolicy = (RetryPolicy)info.GetValue("retryPolicy", typeof(RetryPolicy));
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000041D0 File Offset: 0x000023D0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("authErrorMsg", this.authErrorMsg);
			info.AddValue("authMechanismName", this.authMechanismName);
			info.AddValue("retryPolicy", this.retryPolicy);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000421D File Offset: 0x0000241D
		public string AuthErrorMsg
		{
			get
			{
				return this.authErrorMsg;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00004225 File Offset: 0x00002425
		public string AuthMechanismName
		{
			get
			{
				return this.authMechanismName;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000422D File Offset: 0x0000242D
		public RetryPolicy RetryPolicy
		{
			get
			{
				return this.retryPolicy;
			}
		}

		// Token: 0x040000E1 RID: 225
		private readonly string authErrorMsg;

		// Token: 0x040000E2 RID: 226
		private readonly string authMechanismName;

		// Token: 0x040000E3 RID: 227
		private readonly RetryPolicy retryPolicy;
	}
}
