using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000828 RID: 2088
	[Serializable]
	internal sealed class DataClassificationIdentityMatcher : ClassificationIdentityMatcher<DataClassificationPresentationObject>
	{
		// Token: 0x06004839 RID: 18489 RVA: 0x00128D96 File Offset: 0x00126F96
		private DataClassificationIdentityMatcher(string searchName, string rawSearchName, MatchOptions matchOptions) : base(searchName, rawSearchName, matchOptions)
		{
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00128DA4 File Offset: 0x00126FA4
		internal static DataClassificationIdentityMatcher CreateFrom(TextFilter nameQueryFilter, string rawSearchName)
		{
			DataClassificationIdentityMatcher result = null;
			if (nameQueryFilter != null)
			{
				result = new DataClassificationIdentityMatcher(nameQueryFilter.Text, rawSearchName, nameQueryFilter.MatchOptions);
			}
			return result;
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00128DF0 File Offset: 0x00126FF0
		protected override bool EvaluateMatch(DataClassificationPresentationObject dataClassificationPresentationObject, Func<string, CultureInfo, CompareOptions, bool> objectPropertyMatchOperator)
		{
			ExAssert.RetailAssert(dataClassificationPresentationObject != null, "The data classification presentation object passed to the wilcard matcher must not be null");
			bool flag = false;
			DataClassificationObjectId dataClassificationObjectId = (DataClassificationObjectId)dataClassificationPresentationObject.Identity;
			if (dataClassificationObjectId != null && !string.IsNullOrEmpty(dataClassificationObjectId.Name))
			{
				flag = objectPropertyMatchOperator(dataClassificationObjectId.Name, DataClassificationIdentityMatcher.cultureInfoForMatchingGuidIdentity.Value, CompareOptions.OrdinalIgnoreCase);
			}
			if (!flag && !string.IsNullOrEmpty(dataClassificationPresentationObject.Name))
			{
				flag = objectPropertyMatchOperator(dataClassificationPresentationObject.Name, dataClassificationPresentationObject.DefaultCulture, CompareOptions.IgnoreCase);
			}
			if (!flag)
			{
				flag = dataClassificationPresentationObject.AllLocalizedNames.Any((KeyValuePair<CultureInfo, string> localizedNameDetails) => objectPropertyMatchOperator(localizedNameDetails.Value, localizedNameDetails.Key, CompareOptions.IgnoreCase));
			}
			return flag;
		}

		// Token: 0x04002BF4 RID: 11252
		private static readonly Lazy<CultureInfo> cultureInfoForMatchingGuidIdentity = new Lazy<CultureInfo>(() => new CultureInfo("en-US"), LazyThreadSafetyMode.PublicationOnly);
	}
}
