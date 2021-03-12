using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F47 RID: 3911
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddAccessRuleUnauthorizedAccessException : LocalizedException
	{
		// Token: 0x0600AB5C RID: 43868 RVA: 0x0028EF17 File Offset: 0x0028D117
		public AddAccessRuleUnauthorizedAccessException(string thumbprint) : base(Strings.AddAccessRuleUnauthorizedAccess(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB5D RID: 43869 RVA: 0x0028EF2C File Offset: 0x0028D12C
		public AddAccessRuleUnauthorizedAccessException(string thumbprint, Exception innerException) : base(Strings.AddAccessRuleUnauthorizedAccess(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB5E RID: 43870 RVA: 0x0028EF42 File Offset: 0x0028D142
		protected AddAccessRuleUnauthorizedAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB5F RID: 43871 RVA: 0x0028EF6C File Offset: 0x0028D16C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x1700375D RID: 14173
		// (get) Token: 0x0600AB60 RID: 43872 RVA: 0x0028EF87 File Offset: 0x0028D187
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060C3 RID: 24771
		private readonly string thumbprint;
	}
}
