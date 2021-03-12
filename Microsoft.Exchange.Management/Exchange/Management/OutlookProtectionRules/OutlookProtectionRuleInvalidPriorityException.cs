using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.OutlookProtectionRules
{
	// Token: 0x020010CD RID: 4301
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OutlookProtectionRuleInvalidPriorityException : LocalizedException
	{
		// Token: 0x0600B2F6 RID: 45814 RVA: 0x0029A942 File Offset: 0x00298B42
		public OutlookProtectionRuleInvalidPriorityException() : base(Strings.OutlookProtectionRuleInvalidPriority)
		{
		}

		// Token: 0x0600B2F7 RID: 45815 RVA: 0x0029A94F File Offset: 0x00298B4F
		public OutlookProtectionRuleInvalidPriorityException(Exception innerException) : base(Strings.OutlookProtectionRuleInvalidPriority, innerException)
		{
		}

		// Token: 0x0600B2F8 RID: 45816 RVA: 0x0029A95D File Offset: 0x00298B5D
		protected OutlookProtectionRuleInvalidPriorityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B2F9 RID: 45817 RVA: 0x0029A967 File Offset: 0x00298B67
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
