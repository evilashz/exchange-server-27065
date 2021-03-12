using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000155 RID: 341
	public static class LocalizedStrings
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x00051C80 File Offset: 0x0004FE80
		public static string GetHtmlEncoded(Strings.IDs localizedID)
		{
			string name = Culture.GetUserCulture().Name;
			return LocalizedStrings.GetHtmlEncodedInternal(name, localizedID);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00051CA0 File Offset: 0x0004FEA0
		public static string GetHtmlEncoded(Strings.IDs localizedID, CultureInfo userCulture)
		{
			string name = userCulture.Name;
			return LocalizedStrings.GetHtmlEncodedInternal(name, localizedID);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00051CBB File Offset: 0x0004FEBB
		public static string GetHtmlEncodedFromKey(string key, Strings.IDs localizedId)
		{
			return LocalizedStrings.GetHtmlEncodedInternal(key, localizedId);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00051CC4 File Offset: 0x0004FEC4
		public static string GetNonEncoded(Strings.IDs localizedId)
		{
			return Strings.GetLocalizedString(localizedId);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00051CCC File Offset: 0x0004FECC
		public static string GetJavascriptEncoded(Strings.IDs localizedId)
		{
			string name = Culture.GetUserCulture().Name;
			return LocalizedStrings.GetJavascriptEncodedInternal(name, localizedId);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00051CEB File Offset: 0x0004FEEB
		public static string GetJavascriptEncodedFromKey(string key, Strings.IDs localizedID)
		{
			return LocalizedStrings.GetJavascriptEncodedInternal(key, localizedID);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00051CF4 File Offset: 0x0004FEF4
		public static string GetJavascriptEncodedInternal(string key, Strings.IDs localizedID)
		{
			Dictionary<uint, string> dictionary = null;
			object obj = LocalizedStrings.javascriptEncodedStringsCollection[key];
			if (obj == null)
			{
				lock (LocalizedStrings.javascriptEncodedStringsCollection)
				{
					if (LocalizedStrings.javascriptEncodedStringsCollection[key] == null)
					{
						Strings.IDs[] array = (Strings.IDs[])Enum.GetValues(typeof(Strings.IDs));
						dictionary = new Dictionary<uint, string>(array.Length);
						for (int i = 0; i < array.Length; i++)
						{
							dictionary[array[i]] = Utilities.JavascriptEncode(Strings.GetLocalizedString(array[i]));
						}
						LocalizedStrings.javascriptEncodedStringsCollection[key] = dictionary;
					}
					else
					{
						dictionary = (Dictionary<uint, string>)LocalizedStrings.javascriptEncodedStringsCollection[key];
					}
					goto IL_A9;
				}
			}
			dictionary = (Dictionary<uint, string>)obj;
			IL_A9:
			return dictionary[localizedID];
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00051DC4 File Offset: 0x0004FFC4
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
							dictionary[array[i]] = Utilities.HtmlEncode(Strings.GetLocalizedString(array[i]));
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

		// Token: 0x0400086C RID: 2156
		private static Hashtable htmlEncodedStringsCollection = new Hashtable();

		// Token: 0x0400086D RID: 2157
		private static Hashtable javascriptEncodedStringsCollection = new Hashtable();
	}
}
