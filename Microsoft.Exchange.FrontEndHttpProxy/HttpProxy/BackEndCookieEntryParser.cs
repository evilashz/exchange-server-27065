using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200003A RID: 58
	internal static class BackEndCookieEntryParser
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000A138 File Offset: 0x00008338
		public static BackEndCookieEntryBase Parse(string entryValue)
		{
			BackEndCookieEntryBase result = null;
			if (!BackEndCookieEntryParser.TryParse(entryValue, out result))
			{
				throw new InvalidBackEndCookieException();
			}
			return result;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A158 File Offset: 0x00008358
		public static bool TryParse(string entryValue, out BackEndCookieEntryBase cookieEntry)
		{
			string text = null;
			return BackEndCookieEntryParser.TryParse(entryValue, out cookieEntry, out text);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A170 File Offset: 0x00008370
		public static bool TryParse(string entryValue, out BackEndCookieEntryBase cookieEntry, out string clearCookie)
		{
			cookieEntry = null;
			clearCookie = null;
			if (string.IsNullOrEmpty(entryValue))
			{
				return false;
			}
			bool result;
			try
			{
				string text = BackEndCookieEntryParser.UnObscurify(entryValue);
				clearCookie = text;
				string[] array = text.Split(BackEndCookieEntryParser.CookieSeparators);
				if (array.Length < 4)
				{
					result = false;
				}
				else
				{
					BackEndCookieEntryType backEndCookieEntryType;
					if (!BackEndCookieEntryBase.TryGetBackEndCookieEntryTypeFromString(array[0], out backEndCookieEntryType))
					{
						backEndCookieEntryType = (BackEndCookieEntryType)Enum.Parse(typeof(BackEndCookieEntryType), array[0], true);
					}
					ExDateTime expiryTime;
					if (!BackEndCookieEntryParser.TryParseDateTime(array[3], out expiryTime))
					{
						result = false;
					}
					else
					{
						switch (backEndCookieEntryType)
						{
						case BackEndCookieEntryType.Server:
							cookieEntry = new BackEndServerCookieEntry(array[1], int.Parse(array[2]), expiryTime);
							result = true;
							break;
						case BackEndCookieEntryType.Database:
						{
							Guid database = new Guid(array[1]);
							string text2 = string.IsNullOrEmpty(array[2]) ? null : array[2];
							if (array.Length == 5)
							{
								string resourceForest = string.IsNullOrEmpty(array[4]) ? null : array[4];
								cookieEntry = new BackEndDatabaseResourceForestCookieEntry(database, text2, resourceForest, expiryTime);
							}
							else
							{
								cookieEntry = new BackEndDatabaseCookieEntry(database, text2, expiryTime);
							}
							result = true;
							break;
						}
						default:
							result = false;
							break;
						}
					}
				}
			}
			catch (ArgumentException)
			{
				result = false;
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A29C File Offset: 0x0000849C
		internal static string UnObscurify(string obscureString)
		{
			byte[] array = Convert.FromBase64String(obscureString);
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				byte[] array3 = array2;
				int num = i;
				byte[] array4 = array;
				int num2 = i;
				array3[num] = (array4[num2] ^= BackEndCookieEntryBase.ObfuscateValue);
			}
			return BackEndCookieEntryBase.Encoding.GetString(array2);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A2F4 File Offset: 0x000084F4
		private static bool TryParseDateTime(string dateTimeString, out ExDateTime dateTime)
		{
			if (!string.IsNullOrEmpty(dateTimeString))
			{
				try
				{
					dateTime = ExDateTime.Parse(dateTimeString);
					return true;
				}
				catch (ArgumentException)
				{
				}
				catch (FormatException)
				{
				}
				try
				{
					dateTime = ExDateTime.Parse(dateTimeString, CultureInfo.InvariantCulture);
					return true;
				}
				catch (ArgumentException)
				{
				}
				catch (FormatException)
				{
				}
			}
			dateTime = default(ExDateTime);
			return false;
		}

		// Token: 0x040000EA RID: 234
		private static readonly char[] CookieSeparators = new char[]
		{
			'~'
		};
	}
}
