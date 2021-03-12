using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000AA RID: 170
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RuleNotInAdException : ExchangeConfigurationException
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0001810A File Offset: 0x0001630A
		public RuleNotInAdException() : base(TransportRulesStrings.RuleNotInAd)
		{
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00018117 File Offset: 0x00016317
		public RuleNotInAdException(Exception innerException) : base(TransportRulesStrings.RuleNotInAd, innerException)
		{
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00018125 File Offset: 0x00016325
		protected RuleNotInAdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001812F File Offset: 0x0001632F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
