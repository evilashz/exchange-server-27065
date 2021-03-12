using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000157 RID: 343
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DeviceConfigurationRuleAlreadyExistsException : LocalizedException
	{
		// Token: 0x06000EB7 RID: 3767 RVA: 0x00035554 File Offset: 0x00033754
		public DeviceConfigurationRuleAlreadyExistsException(string ruleName) : base(Strings.DeviceConfigurationRuleAlreadyExists(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00035569 File Offset: 0x00033769
		public DeviceConfigurationRuleAlreadyExistsException(string ruleName, Exception innerException) : base(Strings.DeviceConfigurationRuleAlreadyExists(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003557F File Offset: 0x0003377F
		protected DeviceConfigurationRuleAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000355A9 File Offset: 0x000337A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000355C4 File Offset: 0x000337C4
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04000667 RID: 1639
		private readonly string ruleName;
	}
}
