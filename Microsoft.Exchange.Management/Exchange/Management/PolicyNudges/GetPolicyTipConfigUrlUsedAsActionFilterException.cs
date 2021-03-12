using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x0200117A RID: 4474
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetPolicyTipConfigUrlUsedAsActionFilterException : LocalizedException
	{
		// Token: 0x0600B64A RID: 46666 RVA: 0x0029F89C File Offset: 0x0029DA9C
		public GetPolicyTipConfigUrlUsedAsActionFilterException() : base(Strings.GetPolicyTipConfigUrlUsedAsActionFilter)
		{
		}

		// Token: 0x0600B64B RID: 46667 RVA: 0x0029F8A9 File Offset: 0x0029DAA9
		public GetPolicyTipConfigUrlUsedAsActionFilterException(Exception innerException) : base(Strings.GetPolicyTipConfigUrlUsedAsActionFilter, innerException)
		{
		}

		// Token: 0x0600B64C RID: 46668 RVA: 0x0029F8B7 File Offset: 0x0029DAB7
		protected GetPolicyTipConfigUrlUsedAsActionFilterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B64D RID: 46669 RVA: 0x0029F8C1 File Offset: 0x0029DAC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
