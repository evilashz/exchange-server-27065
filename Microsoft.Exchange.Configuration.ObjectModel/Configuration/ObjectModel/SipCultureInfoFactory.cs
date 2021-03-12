using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000265 RID: 613
	internal static class SipCultureInfoFactory
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x0004EB38 File Offset: 0x0004CD38
		internal static SipCultureInfo CreateInstance(CultureInfo parentCulture, string languageCode)
		{
			if (parentCulture is SipCultureInfoBase)
			{
				throw new ArgumentException(Strings.SipCultureInfoArgumentCheckFailure);
			}
			SipCulture key = new SipCulture(parentCulture, languageCode);
			SipCultureInfo sipCultureInfo = null;
			lock (SipCultureInfoFactory.sipCultureMap)
			{
				if (SipCultureInfoFactory.sipCultureMap.ContainsKey(key))
				{
					sipCultureInfo = SipCultureInfoFactory.sipCultureMap[key];
				}
				else
				{
					sipCultureInfo = new SipCultureInfo(parentCulture, languageCode);
					SipCultureInfoFactory.sipCultureMap.Add(key, sipCultureInfo);
				}
			}
			return sipCultureInfo;
		}

		// Token: 0x04000667 RID: 1639
		private static Dictionary<SipCulture, SipCultureInfo> sipCultureMap = new Dictionary<SipCulture, SipCultureInfo>(1);
	}
}
