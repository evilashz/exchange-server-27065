using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ProvisioningAssistant
{
	// Token: 0x02000144 RID: 324
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnableToGenerateValidPassword : LocalizedException
	{
		// Token: 0x06000D29 RID: 3369 RVA: 0x00051FCA File Offset: 0x000501CA
		public UnableToGenerateValidPassword(string userName) : base(Strings.UnableToGenerateValidPassword(userName))
		{
			this.userName = userName;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00051FDF File Offset: 0x000501DF
		public UnableToGenerateValidPassword(string userName, Exception innerException) : base(Strings.UnableToGenerateValidPassword(userName), innerException)
		{
			this.userName = userName;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00051FF5 File Offset: 0x000501F5
		protected UnableToGenerateValidPassword(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0005201F File Offset: 0x0005021F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0005203A File Offset: 0x0005023A
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x0400083F RID: 2111
		private readonly string userName;
	}
}
