using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F30 RID: 3888
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplicationCheckResultToStringCaseNotHandled : LocalizedException
	{
		// Token: 0x0600AAE5 RID: 43749 RVA: 0x0028E2E0 File Offset: 0x0028C4E0
		public ReplicationCheckResultToStringCaseNotHandled(ReplicationCheckResultEnum result) : base(Strings.ReplicationCheckResultToStringCaseNotHandled(result))
		{
			this.result = result;
		}

		// Token: 0x0600AAE6 RID: 43750 RVA: 0x0028E2F5 File Offset: 0x0028C4F5
		public ReplicationCheckResultToStringCaseNotHandled(ReplicationCheckResultEnum result, Exception innerException) : base(Strings.ReplicationCheckResultToStringCaseNotHandled(result), innerException)
		{
			this.result = result;
		}

		// Token: 0x0600AAE7 RID: 43751 RVA: 0x0028E30B File Offset: 0x0028C50B
		protected ReplicationCheckResultToStringCaseNotHandled(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.result = (ReplicationCheckResultEnum)info.GetValue("result", typeof(ReplicationCheckResultEnum));
		}

		// Token: 0x0600AAE8 RID: 43752 RVA: 0x0028E335 File Offset: 0x0028C535
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("result", this.result);
		}

		// Token: 0x17003742 RID: 14146
		// (get) Token: 0x0600AAE9 RID: 43753 RVA: 0x0028E355 File Offset: 0x0028C555
		public ReplicationCheckResultEnum Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040060A8 RID: 24744
		private readonly ReplicationCheckResultEnum result;
	}
}
