using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DE9 RID: 3561
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RetentionPolicyTagTaskException : LocalizedException
	{
		// Token: 0x0600A481 RID: 42113 RVA: 0x00284636 File Offset: 0x00282836
		public RetentionPolicyTagTaskException(string reason) : base(Strings.RetentionPolicyTagTaskException(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600A482 RID: 42114 RVA: 0x0028464B File Offset: 0x0028284B
		public RetentionPolicyTagTaskException(string reason, Exception innerException) : base(Strings.RetentionPolicyTagTaskException(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600A483 RID: 42115 RVA: 0x00284661 File Offset: 0x00282861
		protected RetentionPolicyTagTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600A484 RID: 42116 RVA: 0x0028468B File Offset: 0x0028288B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170035FA RID: 13818
		// (get) Token: 0x0600A485 RID: 42117 RVA: 0x002846A6 File Offset: 0x002828A6
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04005F60 RID: 24416
		private readonly string reason;
	}
}
