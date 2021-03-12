using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200016C RID: 364
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidComplianceRuleActionException : LocalizedException
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x00035D21 File Offset: 0x00033F21
		public InvalidComplianceRuleActionException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00035D2A File Offset: 0x00033F2A
		public InvalidComplianceRuleActionException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00035D34 File Offset: 0x00033F34
		protected InvalidComplianceRuleActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00035D3E File Offset: 0x00033F3E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
