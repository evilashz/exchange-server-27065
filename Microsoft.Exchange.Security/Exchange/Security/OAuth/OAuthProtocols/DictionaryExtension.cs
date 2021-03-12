using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000E5 RID: 229
	internal static class DictionaryExtension
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x00035AC5 File Offset: 0x00033CC5
		private static string NullEncode(string value)
		{
			return value;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00035AC8 File Offset: 0x00033CC8
		public static void Decode(this IDictionary<string, string> self, string encodedDictionary)
		{
			self.Decode(encodedDictionary, '&', '=', DictionaryExtension.DefaultDecoder, DictionaryExtension.DefaultDecoder, false);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00035AE0 File Offset: 0x00033CE0
		public static void Decode(this IDictionary<string, string> self, string encodedDictionary, DictionaryExtension.Encoder decoder)
		{
			self.Decode(encodedDictionary, '&', '=', decoder, decoder, false);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00035AF0 File Offset: 0x00033CF0
		public static void Decode(this IDictionary<string, string> self, string encodedDictionary, char separator, char keyValueSplitter, bool endsWithSeparator)
		{
			self.Decode(encodedDictionary, separator, keyValueSplitter, DictionaryExtension.DefaultDecoder, DictionaryExtension.DefaultDecoder, endsWithSeparator);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00035B08 File Offset: 0x00033D08
		public static void Decode(this IDictionary<string, string> self, string encodedDictionary, char separator, char keyValueSplitter, DictionaryExtension.Encoder keyDecoder, DictionaryExtension.Encoder valueDecoder, bool endsWithSeparator)
		{
			if (encodedDictionary == null)
			{
				throw new ArgumentNullException("encodedDictionary");
			}
			if (keyDecoder == null)
			{
				throw new ArgumentNullException("keyDecoder");
			}
			if (valueDecoder == null)
			{
				throw new ArgumentNullException("valueDecoder");
			}
			if (endsWithSeparator && encodedDictionary.LastIndexOf(separator) == encodedDictionary.Length - 1)
			{
				encodedDictionary = encodedDictionary.Substring(0, encodedDictionary.Length - 1);
			}
			foreach (string text in encodedDictionary.Split(new char[]
			{
				separator
			}))
			{
				string[] array2 = text.Split(new char[]
				{
					keyValueSplitter
				});
				if ((array2.Length == 1 || array2.Length > 2) && !string.IsNullOrEmpty(array2[0]))
				{
					throw new ArgumentException("The request is not properly formatted.", "encodedDictionary");
				}
				if (array2.Length != 2)
				{
					throw new ArgumentException("The request is not properly formatted.", "encodedDictionary");
				}
				string text2 = keyDecoder(array2[0].Trim());
				string value = valueDecoder(array2[1].Trim().Trim(new char[]
				{
					'"'
				}));
				try
				{
					self.Add(text2, value);
				}
				catch (ArgumentException)
				{
					string message = string.Format(CultureInfo.InvariantCulture, "The request is not properly formatted. The parameter '{0}' is duplicated.", new object[]
					{
						text2
					});
					throw new ArgumentException(message, "encodedDictionary");
				}
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00035C6C File Offset: 0x00033E6C
		public static void DecodeFromJson(this IDictionary<string, string> self, string encodedDictionary)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(encodedDictionary) as Dictionary<string, object>;
			if (dictionary == null)
			{
				throw new ArgumentException("Invalid request format.", "encodedDictionary");
			}
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				if (keyValuePair.Value == null)
				{
					self.Add(keyValuePair.Key, null);
				}
				else if (keyValuePair.Value is object[])
				{
					self.Add(keyValuePair.Key, javaScriptSerializer.Serialize(keyValuePair.Value));
				}
				else
				{
					self.Add(keyValuePair.Key, keyValuePair.Value.ToString());
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00035D38 File Offset: 0x00033F38
		public static string Encode(this IDictionary<string, string> self)
		{
			return self.Encode('&', '=', DictionaryExtension.DefaultEncoder, DictionaryExtension.DefaultEncoder, false);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00035D4F File Offset: 0x00033F4F
		public static string Encode(this IDictionary<string, string> self, DictionaryExtension.Encoder encoder)
		{
			return self.Encode('&', '=', encoder, encoder, false);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00035D5E File Offset: 0x00033F5E
		public static string Encode(this IDictionary<string, string> self, char separator, char keyValueSplitter, bool endsWithSeparator)
		{
			return self.Encode(separator, keyValueSplitter, DictionaryExtension.DefaultEncoder, DictionaryExtension.DefaultEncoder, endsWithSeparator);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00035D74 File Offset: 0x00033F74
		public static string Encode(this IDictionary<string, string> self, char separator, char keyValueSplitter, DictionaryExtension.Encoder keyEncoder, DictionaryExtension.Encoder valueEncoder, bool endsWithSeparator)
		{
			if (keyEncoder == null)
			{
				throw new ArgumentNullException("keyEncoder");
			}
			if (valueEncoder == null)
			{
				throw new ArgumentNullException("valueEncoder");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in self)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(separator);
				}
				stringBuilder.AppendFormat("{0}{1}{2}", keyEncoder(keyValuePair.Key), keyValueSplitter, valueEncoder(keyValuePair.Value));
			}
			if (endsWithSeparator)
			{
				stringBuilder.Append(separator);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00035E28 File Offset: 0x00034028
		public static string EncodeToJson(this IDictionary<string, string> self)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(self);
		}

		// Token: 0x04000745 RID: 1861
		public const char DefaultSeparator = '&';

		// Token: 0x04000746 RID: 1862
		public const char DefaultKeyValueSeparator = '=';

		// Token: 0x04000747 RID: 1863
		public static DictionaryExtension.Encoder DefaultDecoder = new DictionaryExtension.Encoder(HttpUtility.UrlDecode);

		// Token: 0x04000748 RID: 1864
		public static DictionaryExtension.Encoder DefaultEncoder = new DictionaryExtension.Encoder(HttpUtility.UrlEncode);

		// Token: 0x04000749 RID: 1865
		public static DictionaryExtension.Encoder NullEncoder = new DictionaryExtension.Encoder(DictionaryExtension.NullEncode);

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x060007C1 RID: 1985
		public delegate string Encoder(string input);
	}
}
