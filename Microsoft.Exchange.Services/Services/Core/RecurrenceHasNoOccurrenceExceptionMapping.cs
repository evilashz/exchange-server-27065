using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000225 RID: 549
	internal class RecurrenceHasNoOccurrenceExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E2B RID: 3627 RVA: 0x00045666 File Offset: 0x00043866
		public RecurrenceHasNoOccurrenceExceptionMapping() : base(typeof(RecurrenceHasNoOccurrenceException), ResponseCodeType.ErrorRecurrenceHasNoOccurrence, CoreResources.IDs.ErrorRecurrenceHasNoOccurrence)
		{
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00045684 File Offset: 0x00043884
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			RecurrenceHasNoOccurrenceException ex = base.VerifyExceptionType<RecurrenceHasNoOccurrenceException>(exception);
			return new Dictionary<string, string>
			{
				{
					"EffectiveStartDate",
					ex.EffectiveStartDate.ToString()
				},
				{
					"EffectiveEndDate",
					ex.EffectiveEndDate.ToString()
				}
			};
		}

		// Token: 0x04000AF8 RID: 2808
		private const string EffectiveStartDate = "EffectiveStartDate";

		// Token: 0x04000AF9 RID: 2809
		private const string EffectiveEndDate = "EffectiveEndDate";
	}
}
