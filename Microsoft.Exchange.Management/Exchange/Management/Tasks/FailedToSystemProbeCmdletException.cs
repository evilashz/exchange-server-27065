using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001198 RID: 4504
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToSystemProbeCmdletException : LocalizedException
	{
		// Token: 0x0600B6EE RID: 46830 RVA: 0x002A0B5B File Offset: 0x0029ED5B
		public FailedToSystemProbeCmdletException(string failure) : base(Strings.FailedToSystemProbeCmdlet(failure))
		{
			this.failure = failure;
		}

		// Token: 0x0600B6EF RID: 46831 RVA: 0x002A0B70 File Offset: 0x0029ED70
		public FailedToSystemProbeCmdletException(string failure, Exception innerException) : base(Strings.FailedToSystemProbeCmdlet(failure), innerException)
		{
			this.failure = failure;
		}

		// Token: 0x0600B6F0 RID: 46832 RVA: 0x002A0B86 File Offset: 0x0029ED86
		protected FailedToSystemProbeCmdletException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B6F1 RID: 46833 RVA: 0x002A0BB0 File Offset: 0x0029EDB0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x170039AB RID: 14763
		// (get) Token: 0x0600B6F2 RID: 46834 RVA: 0x002A0BCB File Offset: 0x0029EDCB
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x04006311 RID: 25361
		private readonly string failure;
	}
}
