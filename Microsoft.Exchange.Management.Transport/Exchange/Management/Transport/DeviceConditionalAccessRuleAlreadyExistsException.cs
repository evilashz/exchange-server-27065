using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000158 RID: 344
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DeviceConditionalAccessRuleAlreadyExistsException : LocalizedException
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x000355CC File Offset: 0x000337CC
		public DeviceConditionalAccessRuleAlreadyExistsException(string ruleName) : base(Strings.DeviceConditionalAccessRuleAlreadyExists(ruleName))
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000355E1 File Offset: 0x000337E1
		public DeviceConditionalAccessRuleAlreadyExistsException(string ruleName, Exception innerException) : base(Strings.DeviceConditionalAccessRuleAlreadyExists(ruleName), innerException)
		{
			this.ruleName = ruleName;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000355F7 File Offset: 0x000337F7
		protected DeviceConditionalAccessRuleAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleName = (string)info.GetValue("ruleName", typeof(string));
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00035621 File Offset: 0x00033821
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleName", this.ruleName);
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0003563C File Offset: 0x0003383C
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x04000668 RID: 1640
		private readonly string ruleName;
	}
}
