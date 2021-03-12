using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE6 RID: 2790
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RuleValidationException : ADScopeException
	{
		// Token: 0x06008131 RID: 33073 RVA: 0x001A6426 File Offset: 0x001A4626
		public RuleValidationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x001A642F File Offset: 0x001A462F
		public RuleValidationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x001A6439 File Offset: 0x001A4639
		protected RuleValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x001A6443 File Offset: 0x001A4643
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
