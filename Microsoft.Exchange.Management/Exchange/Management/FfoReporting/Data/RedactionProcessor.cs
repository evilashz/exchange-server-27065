using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Management.FfoReporting.Common;

namespace Microsoft.Exchange.Management.FfoReporting.Data
{
	// Token: 0x02000401 RID: 1025
	internal class RedactionProcessor : IDataProcessor
	{
		// Token: 0x06002416 RID: 9238 RVA: 0x00090394 File Offset: 0x0008E594
		private RedactionProcessor(Type targetType)
		{
			this.propertiesNeedingRedaction = Schema.Utilities.GetProperties<RedactAttribute>(targetType);
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000903A8 File Offset: 0x0008E5A8
		internal static RedactionProcessor Create<TRedactionTargetType>()
		{
			return new RedactionProcessor(typeof(TRedactionTargetType));
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000903B9 File Offset: 0x0008E5B9
		public object Process(object input)
		{
			if (this.propertiesNeedingRedaction.Count > 0)
			{
				Schema.Utilities.Redact(input, this.propertiesNeedingRedaction);
			}
			return input;
		}

		// Token: 0x04001CD0 RID: 7376
		private readonly IList<Tuple<PropertyInfo, RedactAttribute>> propertiesNeedingRedaction;
	}
}
