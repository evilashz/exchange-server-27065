using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000F45 RID: 3909
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AddAccessRuleCryptographicException : LocalizedException
	{
		// Token: 0x0600AB52 RID: 43858 RVA: 0x0028EE27 File Offset: 0x0028D027
		public AddAccessRuleCryptographicException(string thumbprint) : base(Strings.AddAccessRuleCryptographic(thumbprint))
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB53 RID: 43859 RVA: 0x0028EE3C File Offset: 0x0028D03C
		public AddAccessRuleCryptographicException(string thumbprint, Exception innerException) : base(Strings.AddAccessRuleCryptographic(thumbprint), innerException)
		{
			this.thumbprint = thumbprint;
		}

		// Token: 0x0600AB54 RID: 43860 RVA: 0x0028EE52 File Offset: 0x0028D052
		protected AddAccessRuleCryptographicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.thumbprint = (string)info.GetValue("thumbprint", typeof(string));
		}

		// Token: 0x0600AB55 RID: 43861 RVA: 0x0028EE7C File Offset: 0x0028D07C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("thumbprint", this.thumbprint);
		}

		// Token: 0x1700375B RID: 14171
		// (get) Token: 0x0600AB56 RID: 43862 RVA: 0x0028EE97 File Offset: 0x0028D097
		public string Thumbprint
		{
			get
			{
				return this.thumbprint;
			}
		}

		// Token: 0x040060C1 RID: 24769
		private readonly string thumbprint;
	}
}
