using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200100B RID: 4107
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DlpPolicyScriptExecutionException : LocalizedException
	{
		// Token: 0x0600AEF7 RID: 44791 RVA: 0x00293BBB File Offset: 0x00291DBB
		public DlpPolicyScriptExecutionException(string error) : base(Strings.DlpPolicyScriptExecutionError(error))
		{
			this.error = error;
		}

		// Token: 0x0600AEF8 RID: 44792 RVA: 0x00293BD0 File Offset: 0x00291DD0
		public DlpPolicyScriptExecutionException(string error, Exception innerException) : base(Strings.DlpPolicyScriptExecutionError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600AEF9 RID: 44793 RVA: 0x00293BE6 File Offset: 0x00291DE6
		protected DlpPolicyScriptExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600AEFA RID: 44794 RVA: 0x00293C10 File Offset: 0x00291E10
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170037E8 RID: 14312
		// (get) Token: 0x0600AEFB RID: 44795 RVA: 0x00293C2B File Offset: 0x00291E2B
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400614E RID: 24910
		private readonly string error;
	}
}
