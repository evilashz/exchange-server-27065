using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200016B RID: 363
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorRuleNotFoundException : LocalizedException
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00035CA9 File Offset: 0x00033EA9
		public ErrorRuleNotFoundException(string ruleId) : base(Strings.ErrorRuleNotFound(ruleId))
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00035CBE File Offset: 0x00033EBE
		public ErrorRuleNotFoundException(string ruleId, Exception innerException) : base(Strings.ErrorRuleNotFound(ruleId), innerException)
		{
			this.ruleId = ruleId;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00035CD4 File Offset: 0x00033ED4
		protected ErrorRuleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ruleId = (string)info.GetValue("ruleId", typeof(string));
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00035CFE File Offset: 0x00033EFE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ruleId", this.ruleId);
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00035D19 File Offset: 0x00033F19
		public string RuleId
		{
			get
			{
				return this.ruleId;
			}
		}

		// Token: 0x04000674 RID: 1652
		private readonly string ruleId;
	}
}
