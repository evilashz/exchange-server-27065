using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000B7 RID: 183
	internal class QueryableThrottleGovernor : QueryableGovernor
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001B445 File Offset: 0x00019645
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001B457 File Offset: 0x00019657
		public QueryableThrottle Throttle
		{
			get
			{
				return (QueryableThrottle)this[QueryableThrottleGovernorObjectSchema.Throttle];
			}
			set
			{
				this[QueryableThrottleGovernorObjectSchema.Throttle] = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001B465 File Offset: 0x00019665
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return QueryableThrottleGovernor.schema;
			}
		}

		// Token: 0x04000354 RID: 852
		private static readonly ObjectSchema schema = ObjectSchema.GetInstance(typeof(QueryableThrottleGovernorObjectSchema));
	}
}
