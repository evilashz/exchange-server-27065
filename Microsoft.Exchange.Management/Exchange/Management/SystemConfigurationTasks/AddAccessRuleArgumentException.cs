using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F46 RID: 3910
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddAccessRuleArgumentException : LocalizedException
	{
		// Token: 0x0600AB57 RID: 43863 RVA: 0x0028EE9F File Offset: 0x0028D09F
		public AddAccessRuleArgumentException(string thumbprint) : base(Strings.AddAccessRuleArgument(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB58 RID: 43864 RVA: 0x0028EEB4 File Offset: 0x0028D0B4
		public AddAccessRuleArgumentException(string thumbprint, Exception innerException) : base(Strings.AddAccessRuleArgument(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB59 RID: 43865 RVA: 0x0028EECA File Offset: 0x0028D0CA
		protected AddAccessRuleArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB5A RID: 43866 RVA: 0x0028EEF4 File Offset: 0x0028D0F4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x1700375C RID: 14172
		// (get) Token: 0x0600AB5B RID: 43867 RVA: 0x0028EF0F File Offset: 0x0028D10F
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060C2 RID: 24770
		private readonly string thumbprint;
	}
}
