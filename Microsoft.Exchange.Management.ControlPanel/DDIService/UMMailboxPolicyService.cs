using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020004D5 RID: 1237
	public class UMMailboxPolicyService : DDICodeBehind
	{
		// Token: 0x06003C81 RID: 15489 RVA: 0x000B5DC8 File Offset: 0x000B3FC8
		public static string ToDaysString(object value)
		{
			string result = string.Empty;
			if (!DDIHelper.IsEmptyValue(value))
			{
				Unlimited<EnhancedTimeSpan> unlimited = (Unlimited<EnhancedTimeSpan>)value;
				result = (unlimited.IsUnlimited ? unlimited.ToString() : unlimited.Value.Days.ToString());
			}
			return result;
		}
	}
}
