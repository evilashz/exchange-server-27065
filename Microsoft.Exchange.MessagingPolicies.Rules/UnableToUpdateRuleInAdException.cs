using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000AB RID: 171
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToUpdateRuleInAdException : ExchangeConfigurationException
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x00018139 File Offset: 0x00016339
		public UnableToUpdateRuleInAdException() : base(TransportRulesStrings.UnableToUpdateRuleInAd)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00018146 File Offset: 0x00016346
		public UnableToUpdateRuleInAdException(Exception innerException) : base(TransportRulesStrings.UnableToUpdateRuleInAd, innerException)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00018154 File Offset: 0x00016354
		protected UnableToUpdateRuleInAdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001815E File Offset: 0x0001635E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
