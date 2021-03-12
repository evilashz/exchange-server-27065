using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000156 RID: 342
	internal class GrammarUtils
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002B9A4 File Offset: 0x00029BA4
		internal static ResourceManager GrammarResourceManager
		{
			get
			{
				if (GrammarUtils.grammarResources == null)
				{
					lock (GrammarUtils.resourceLock)
					{
						if (GrammarUtils.grammarResources == null)
						{
							GrammarUtils.grammarResources = new ResourceManager("Microsoft.Exchange.UM.Grammars.Grammars.Strings", Assembly.Load("Microsoft.Exchange.UM.Grammars"));
						}
					}
				}
				return GrammarUtils.grammarResources;
			}
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002BA0C File Offset: 0x00029C0C
		internal static string GetLocString(string grammarKeywordName, CultureInfo culture)
		{
			string @string = GrammarUtils.GrammarResourceManager.GetString(grammarKeywordName, culture);
			if (@string == null)
			{
				throw new MowaGrammarException(Strings.InvalidGrammarResourceId(grammarKeywordName));
			}
			return @string;
		}

		// Token: 0x0400093E RID: 2366
		private static ResourceManager grammarResources;

		// Token: 0x0400093F RID: 2367
		private static object resourceLock = new object();
	}
}
