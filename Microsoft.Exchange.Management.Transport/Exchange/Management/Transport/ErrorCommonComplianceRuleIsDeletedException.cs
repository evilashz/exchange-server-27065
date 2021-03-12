using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200016F RID: 367
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorCommonComplianceRuleIsDeletedException : LocalizedException
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x00035E38 File Offset: 0x00034038
		public ErrorCommonComplianceRuleIsDeletedException(string name) : base(Strings.ErrorCommonComplianceRuleIsDeleted(name))
		{
			this.name = name;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x00035E4D File Offset: 0x0003404D
		public ErrorCommonComplianceRuleIsDeletedException(string name, Exception innerException) : base(Strings.ErrorCommonComplianceRuleIsDeleted(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00035E63 File Offset: 0x00034063
		protected ErrorCommonComplianceRuleIsDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00035E8D File Offset: 0x0003408D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x00035EA8 File Offset: 0x000340A8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000677 RID: 1655
		private readonly string name;
	}
}
