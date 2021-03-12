using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.HttpProxy;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200005A RID: 90
	public static class LocalizedStrings
	{
		// Token: 0x060002EB RID: 747 RVA: 0x000125D0 File Offset: 0x000107D0
		public static string GetHtmlEncoded(Strings.IDs localizedID)
		{
			string name = Culture.GetUserCulture().Name;
			return LocalizedStrings.GetHtmlEncodedInternal(name, localizedID);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000125F0 File Offset: 0x000107F0
		public static string GetHtmlEncoded(Strings.IDs localizedID, CultureInfo userCulture)
		{
			string name = userCulture.Name;
			return LocalizedStrings.GetHtmlEncodedInternal(name, localizedID);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001260B File Offset: 0x0001080B
		public static string GetHtmlEncodedFromKey(string key, Strings.IDs localizedId)
		{
			return LocalizedStrings.GetHtmlEncodedInternal(key, localizedId);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00012614 File Offset: 0x00010814
		internal static string GetHtmlEncodedInternal(string key, Strings.IDs localizedID)
		{
			Dictionary<uint, string> dictionary = null;
			object obj = LocalizedStrings.htmlEncodedStringsCollection[key];
			if (obj == null)
			{
				lock (LocalizedStrings.htmlEncodedStringsCollection)
				{
					if (LocalizedStrings.htmlEncodedStringsCollection[key] == null)
					{
						Strings.IDs[] array = (Strings.IDs[])Enum.GetValues(typeof(Strings.IDs));
						dictionary = new Dictionary<uint, string>(array.Length);
						for (int i = 0; i < array.Length; i++)
						{
							dictionary[array[i]] = EncodingUtilities.HtmlEncode(Strings.GetLocalizedString(array[i]));
						}
						LocalizedStrings.htmlEncodedStringsCollection[key] = dictionary;
					}
					else
					{
						dictionary = (Dictionary<uint, string>)LocalizedStrings.htmlEncodedStringsCollection[key];
					}
					goto IL_A9;
				}
			}
			dictionary = (Dictionary<uint, string>)obj;
			IL_A9:
			return dictionary[localizedID];
		}

		// Token: 0x040001D7 RID: 471
		private static Hashtable htmlEncodedStringsCollection = new Hashtable();
	}
}
