using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Win32;

namespace Microsoft.Exchange.PowerShell.RbacHostingTools
{
	// Token: 0x02000002 RID: 2
	public static class Culture
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static bool IsSupportedCulture(CultureInfo culture)
		{
			return Culture.supportedCultureInfos.Contains(culture);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DD File Offset: 0x000002DD
		public static string[] GetOneLetterDayNames()
		{
			return Culture.GetOneLetterDayNames(CultureInfo.CurrentCulture);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E9 File Offset: 0x000002E9
		public static string[] GetOneLetterDayNames(CultureInfo culture)
		{
			return Culture.oneLetterDayNamesMap[culture.Name];
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		internal static string ValidateLanguageTag(string tag)
		{
			if (tag.Length < 1 || tag.Length > 44)
			{
				return null;
			}
			int num = 0;
			while (num < tag.Length && char.IsWhiteSpace(tag[num]))
			{
				num++;
			}
			if (num == tag.Length)
			{
				return null;
			}
			int num2 = num;
			for (int i = 0; i < 3; i++)
			{
				int num3 = 0;
				while (num3 < 8 && num2 < tag.Length && ((tag[num2] >= 'a' && tag[num2] <= 'z') || (tag[num2] >= 'A' && tag[num2] <= 'Z')))
				{
					num3++;
					num2++;
				}
				if (num2 == tag.Length || tag[num2] != '-')
				{
					break;
				}
				num2++;
			}
			if (num2 != tag.Length && tag[num2] != ';' && !char.IsWhiteSpace(tag[num2]))
			{
				return null;
			}
			return tag.Substring(num, num2 - num);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021E4 File Offset: 0x000003E4
		internal static CultureInfo GetServerCulture()
		{
			if (Culture.serverCulture == null)
			{
				int lcid = 0;
				bool flag = false;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("ENU", 1033);
						if (value is int)
						{
							lcid = (int)value;
							flag = true;
						}
					}
				}
				if (flag)
				{
					if (Culture.IsSupportedCulture(lcid))
					{
						Culture.serverCulture = Culture.GetCultureInfoInstance(lcid);
					}
					else
					{
						Culture.serverCulture = Culture.GetCultureInfoInstance(1033);
					}
				}
				else
				{
					Culture.serverCulture = Culture.GetCultureInfoInstance(1033);
				}
			}
			return Culture.serverCulture;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002294 File Offset: 0x00000494
		internal static CultureInfo GetCultureInfoInstance(int lcid)
		{
			return new CultureInfo(lcid)
			{
				DateTimeFormat = 
				{
					Calendar = new GregorianCalendar()
				}
			};
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022B9 File Offset: 0x000004B9
		internal static List<CultureInfo> GetSupportedCultures()
		{
			return Culture.supportedCultureInfos;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022C0 File Offset: 0x000004C0
		internal static bool IsSupportedCulture(int lcid)
		{
			return lcid > 0 && Culture.IsSupportedCulture(CultureInfo.GetCultureInfo(lcid));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022D4 File Offset: 0x000004D4
		internal static CultureInfo GetPreferredCulture(IEnumerable<CultureInfo> languages)
		{
			foreach (CultureInfo cultureInfo in languages)
			{
				if (Culture.IsSupportedCulture(cultureInfo))
				{
					return Culture.GetCultureInfoInstance(cultureInfo.LCID);
				}
			}
			return null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002330 File Offset: 0x00000530
		internal static CultureInfo GetDefaultCulture(HttpContext context)
		{
			string[] array = (context != null) ? context.Request.UserLanguages : null;
			if (array != null)
			{
				int num = Math.Min(array.Length, 5);
				for (int i = 0; i < num; i++)
				{
					string text = Culture.ValidateLanguageTag(array[i]);
					if (!string.IsNullOrEmpty(text))
					{
						try
						{
							CultureInfo cultureInfoByIetfLanguageTag = CultureInfo.GetCultureInfoByIetfLanguageTag(text);
							if (Culture.IsSupportedCulture(cultureInfoByIetfLanguageTag))
							{
								return Culture.GetCultureInfoInstance(cultureInfoByIetfLanguageTag.LCID);
							}
						}
						catch (ArgumentException)
						{
						}
					}
				}
			}
			return Culture.GetServerCulture();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023DC File Offset: 0x000005DC
		private static Dictionary<string, string[]> LoadOneLetterDayNamesMap()
		{
			Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			string[] array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					229,
					101
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					0,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					140,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					9,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					219,
					86
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					148,
					78
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					109,
					81
				})
			};
			dictionary["zh-MO"] = array;
			dictionary["zh-TW"] = array;
			dictionary["zh-CN"] = array;
			dictionary["zh-SG"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					45,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					70,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					43,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					49,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					46,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					51,
					6
				})
			};
			dictionary["ar"] = array;
			dictionary["ar-SA"] = array;
			dictionary["ar-IQ"] = array;
			dictionary["ar-EG"] = array;
			dictionary["ar-LY"] = array;
			dictionary["ar-DZ"] = array;
			dictionary["ar-MA"] = array;
			dictionary["ar-TN"] = array;
			dictionary["ar-OM"] = array;
			dictionary["ar-YE"] = array;
			dictionary["ar-SY"] = array;
			dictionary["ar-JO"] = array;
			dictionary["ar-LB"] = array;
			dictionary["ar-KW"] = array;
			dictionary["ar-AE"] = array;
			dictionary["ar-BH"] = array;
			dictionary["ar-QA"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					204,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					47,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					51,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					134,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					126,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					52,
					6
				})
			};
			dictionary["fa"] = array;
			dictionary["fa-IR"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					208,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					209,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					210,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					211,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					212,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					213,
					5
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					233,
					5
				})
			};
			dictionary["he"] = array;
			dictionary["he-IL"] = array;
			dictionary["hi"] = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					48,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					56,
					9,
					75,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					46,
					9,
					2,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					9,
					65,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					23,
					9,
					65,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					54,
					9,
					65,
					9
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					54,
					9
				})
			};
			dictionary["th"] = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					45,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					8,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					45,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					30,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					30,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					40,
					14
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					42,
					14
				})
			};
			array = new string[]
			{
				"A",
				"R",
				"Z",
				"G",
				"O",
				"L",
				"I"
			};
			dictionary["eu"] = array;
			dictionary["eu-ES"] = array;
			array = new string[]
			{
				"D",
				"L",
				"M",
				"X",
				"J",
				"V",
				"S"
			};
			dictionary["ca"] = array;
			dictionary["ca-ES"] = array;
			array = new string[]
			{
				"s",
				"m",
				"t",
				"w",
				"t",
				"f",
				"s"
			};
			dictionary["vi"] = array;
			dictionary["vi-VN"] = array;
			array = new string[]
			{
				Encoding.Unicode.GetString(new byte[]
				{
					39,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					126,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					69,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					40,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					44,
					6
				}),
				Encoding.Unicode.GetString(new byte[]
				{
					71,
					6
				})
			};
			dictionary["ur"] = array;
			dictionary["ur-PK"] = array;
			for (int i = 0; i < Culture.supportedCultureInfos.Count; i++)
			{
				if (!dictionary.ContainsKey(Culture.supportedCultureInfos[i].Name))
				{
					string[] abbreviatedDayNames = Culture.supportedCultureInfos[i].DateTimeFormat.AbbreviatedDayNames;
					array = new string[7];
					for (int j = 0; j < abbreviatedDayNames.Length; j++)
					{
						array[j] = abbreviatedDayNames[j][0].ToString();
					}
					dictionary[Culture.supportedCultureInfos[i].Name] = array;
				}
			}
			return dictionary;
		}

		// Token: 0x04000001 RID: 1
		private const int FailoverServerLcid = 1033;

		// Token: 0x04000002 RID: 2
		private const int LanguageThreshold = 5;

		// Token: 0x04000003 RID: 3
		private static readonly List<CultureInfo> supportedCultureInfos = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));

		// Token: 0x04000004 RID: 4
		private static readonly Dictionary<string, string[]> oneLetterDayNamesMap = Culture.LoadOneLetterDayNamesMap();

		// Token: 0x04000005 RID: 5
		private static CultureInfo serverCulture = null;
	}
}
