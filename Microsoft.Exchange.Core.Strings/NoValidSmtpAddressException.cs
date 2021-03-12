using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Core
{
	// Token: 0x0200001C RID: 28
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoValidSmtpAddressException : LocalizedException
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public NoValidSmtpAddressException(int ruleId) : base(CoreStrings.NoValidSmtpAddress(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000D1C2 File Offset: 0x0000B3C2
		public NoValidSmtpAddressException(int ruleId, Exception innerException) : base(CoreStrings.NoValidSmtpAddress(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		protected NoValidSmtpAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (int)info.GetValue("ruleId", typeof(int));
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000D202 File Offset: 0x0000B402
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000D21D File Offset: 0x0000B41D
		public int RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000366 RID: 870
		private readonly int ruleId;
	}
}
