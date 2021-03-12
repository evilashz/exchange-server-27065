using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F3D RID: 3901
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxSearchTaskException : LocalizedException
	{
		// Token: 0x0600AB2B RID: 43819 RVA: 0x0028EAB0 File Offset: 0x0028CCB0
		public MailboxSearchTaskException(string failure) : base(Strings.MailboxSearchTaskException(failure))
		{
			this.failure = failure;
		}

		// Token: 0x0600AB2C RID: 43820 RVA: 0x0028EAC5 File Offset: 0x0028CCC5
		public MailboxSearchTaskException(string failure, Exception innerException) : base(Strings.MailboxSearchTaskException(failure), innerException)
		{
			this.failure = failure;
		}

		// Token: 0x0600AB2D RID: 43821 RVA: 0x0028EADB File Offset: 0x0028CCDB
		protected MailboxSearchTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600AB2E RID: 43822 RVA: 0x0028EB05 File Offset: 0x0028CD05
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x17003754 RID: 14164
		// (get) Token: 0x0600AB2F RID: 43823 RVA: 0x0028EB20 File Offset: 0x0028CD20
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040060BA RID: 24762
		private readonly string failure;
	}
}
