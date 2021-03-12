using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x020001A1 RID: 417
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class PropertyBagExtensions
	{
		// Token: 0x0600084A RID: 2122 RVA: 0x0001D080 File Offset: 0x0001B280
		public static IEnumerable<PropertyTag> WithNoValue(this IPropertyBag propertyBag, IEnumerable<PropertyTag> propertyTags)
		{
			foreach (PropertyTag propertyTag in propertyTags)
			{
				PropertyValue propertyValue = propertyBag.GetAnnotatedProperty(propertyTag).PropertyValue;
				if (!PropertyBagExtensions.IsValuePresent(propertyValue))
				{
					yield return propertyTag;
				}
			}
			yield break;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001D0A4 File Offset: 0x0001B2A4
		private static bool IsValuePresent(PropertyValue propertyValue)
		{
			return !propertyValue.IsError || (ErrorCode)propertyValue.Value == (ErrorCode)2147746565U;
		}
	}
}
