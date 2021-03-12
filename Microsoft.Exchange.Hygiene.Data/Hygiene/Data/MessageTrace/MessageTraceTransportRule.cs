using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000182 RID: 386
	internal class MessageTraceTransportRule : ADObject
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00031B43 File Offset: 0x0002FD43
		public override ObjectId Identity
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00031B4B File Offset: 0x0002FD4B
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00031B5D File Offset: 0x0002FD5D
		public Guid DLPPolicyId
		{
			get
			{
				return (Guid)this[MessageTraceTransportRuleSchema.DLPPolicyIdProp];
			}
			set
			{
				this[MessageTraceTransportRuleSchema.DLPPolicyIdProp] = value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00031B70 File Offset: 0x0002FD70
		internal override ADObjectSchema Schema
		{
			get
			{
				return MessageTraceTransportRule.schema;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00031B77 File Offset: 0x0002FD77
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MessageTraceTransportRule.mostDerivedClass;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00031B7E File Offset: 0x0002FD7E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x04000735 RID: 1845
		private static readonly MessageTraceTransportRuleSchema schema = ObjectSchema.GetInstance<MessageTraceTransportRuleSchema>();

		// Token: 0x04000736 RID: 1846
		private static string mostDerivedClass = "MessageTraceTransportRule";
	}
}
