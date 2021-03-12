using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000159 RID: 345
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DeviceTenantRuleAlreadyExistsException : LocalizedException
	{
		// Token: 0x06000EC1 RID: 3777 RVA: 0x00035644 File Offset: 0x00033844
		public DeviceTenantRuleAlreadyExistsException(string ruleName) : base(Strings.DeviceTenantRuleAlreadyExists(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00035659 File Offset: 0x00033859
		public DeviceTenantRuleAlreadyExistsException(string ruleName, Exception innerException) : base(Strings.DeviceTenantRuleAlreadyExists(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003566F File Offset: 0x0003386F
		protected DeviceTenantRuleAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00035699 File Offset: 0x00033899
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000356B4 File Offset: 0x000338B4
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04000669 RID: 1641
		private readonly string ruleName;
	}
}
