using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200017C RID: 380
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidComplianceRulePredicateException : LocalizedException
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x000363D1 File Offset: 0x000345D1
		public InvalidComplianceRulePredicateException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000363DA File Offset: 0x000345DA
		public InvalidComplianceRulePredicateException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x000363E4 File Offset: 0x000345E4
		protected InvalidComplianceRulePredicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x000363EE File Offset: 0x000345EE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
