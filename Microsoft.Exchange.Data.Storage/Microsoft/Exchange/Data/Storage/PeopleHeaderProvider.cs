using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000518 RID: 1304
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PeopleHeaderProvider
	{
		// Token: 0x060037FD RID: 14333 RVA: 0x000E2510 File Offset: 0x000E0710
		protected PeopleHeaderProvider()
		{
			this.localeHeaders = new Dictionary<int, List<PeopleHeader>>();
			IEnumerable<FieldInfo> enumerable = from x in typeof(PeopleHeaderProvider).GetTypeInfo().DeclaredFields
			where x.IsStatic && !x.IsPublic
			select x;
			foreach (FieldInfo fieldInfo in enumerable)
			{
				if (!(fieldInfo.FieldType != typeof(List<PeopleHeader>)))
				{
					PeopleHeaderProvider.SupportedLocaleAttribute[] array = (PeopleHeaderProvider.SupportedLocaleAttribute[])fieldInfo.GetCustomAttributes(typeof(PeopleHeaderProvider.SupportedLocaleAttribute), false);
					foreach (PeopleHeaderProvider.SupportedLocaleAttribute supportedLocaleAttribute in array)
					{
						this.localeHeaders.Add(supportedLocaleAttribute.LCID, (List<PeopleHeader>)fieldInfo.GetValue(null));
					}
				}
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000E2610 File Offset: 0x000E0810
		public static PeopleHeaderProvider Instance
		{
			get
			{
				return PeopleHeaderProvider.instance;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060037FF RID: 14335 RVA: 0x000E2617 File Offset: 0x000E0817
		protected Dictionary<int, List<PeopleHeader>> LocaleHeaders
		{
			get
			{
				return this.localeHeaders;
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000E2620 File Offset: 0x000E0820
		public string GetHeader(string inputString, CultureInfo cultureInfo)
		{
			List<PeopleHeader> localeHeaderList = this.GetLocaleHeaderList(cultureInfo);
			for (int i = localeHeaderList.Count - 1; i >= 0; i--)
			{
				PeopleHeader peopleHeader = localeHeaderList[i];
				if (cultureInfo.CompareInfo.Compare(inputString, peopleHeader.StartChar, PeopleStringUtils.StringCompareOptions) >= 0)
				{
					return peopleHeader.DisplayName;
				}
			}
			string message = string.Format(CultureInfo.InvariantCulture, "List of headers should cover the entire unicode range. Input String: {0}, LCID: {1}, LocaleName: {2}", new object[]
			{
				inputString,
				LocaleMap.GetLcidFromCulture(cultureInfo),
				cultureInfo.Name
			});
			PeopleHeaderProvider.Tracer.TraceError(0L, message);
			return PeopleHeaderProvider.FirstHeader.DisplayName;
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000E26C0 File Offset: 0x000E08C0
		protected List<PeopleHeader> GetLocaleHeaderList(CultureInfo cultureInfo)
		{
			List<PeopleHeader> result = null;
			if (!this.localeHeaders.TryGetValue(LocaleMap.GetLcidFromCulture(cultureInfo), out result))
			{
				result = this.fallbackHeaders;
			}
			return result;
		}

		// Token: 0x04001DD0 RID: 7632
		public static readonly PeopleHeader FirstHeader = new PeopleHeader("#", null);

		// Token: 0x04001DD1 RID: 7633
		public static readonly PeopleHeader LastHeader = new PeopleHeader("*", "α");

		// Token: 0x04001DD2 RID: 7634
		private static readonly Trace Tracer = ExTraceGlobals.PersonTracer;

		// Token: 0x04001DD3 RID: 7635
		[PeopleHeaderProvider.SupportedLocaleAttribute(1046)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(3084)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1031)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1027)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1050)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1086)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(2110)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1033)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(2070)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(3081)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(4105)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1043)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1124)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1069)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1036)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(2057)]
		private static readonly List<PeopleHeader> ASCII_LOWER = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DD4 RID: 7636
		[PeopleHeaderProvider.SupportedLocaleAttribute(1029)]
		private static readonly List<PeopleHeader> CZECH = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("č", "č"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("ch", "ch"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("ř", "ř"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("š", "š"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("ž", "ž"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DD5 RID: 7637
		[PeopleHeaderProvider.SupportedLocaleAttribute(1030)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1044)]
		private static readonly List<PeopleHeader> DANISH_NORWEGIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("æ", "æ"),
			new PeopleHeader("ø", "ø"),
			new PeopleHeader("å", "å"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DD6 RID: 7638
		[PeopleHeaderProvider.SupportedLocaleAttribute(1061)]
		private static readonly List<PeopleHeader> ESTONIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("A", "A"),
			new PeopleHeader("B", "B"),
			new PeopleHeader("C", "C"),
			new PeopleHeader("D", "D"),
			new PeopleHeader("E", "E"),
			new PeopleHeader("F", "F"),
			new PeopleHeader("G", "G"),
			new PeopleHeader("H", "H"),
			new PeopleHeader("I", "I"),
			new PeopleHeader("J", "J"),
			new PeopleHeader("K", "K"),
			new PeopleHeader("L", "L"),
			new PeopleHeader("M", "M"),
			new PeopleHeader("N", "N"),
			new PeopleHeader("O", "O"),
			new PeopleHeader("P", "P"),
			new PeopleHeader("Q", "Q"),
			new PeopleHeader("R", "R"),
			new PeopleHeader("S", "S"),
			new PeopleHeader("Š", "Š"),
			new PeopleHeader("Z", "Z"),
			new PeopleHeader("Ž", "Ž"),
			new PeopleHeader("T", "T"),
			new PeopleHeader("U", "U"),
			new PeopleHeader("V", "V"),
			new PeopleHeader("Õ", "Õ"),
			new PeopleHeader("Ä", "Ä"),
			new PeopleHeader("Ö", "Ö"),
			new PeopleHeader("Ü", "Ü"),
			new PeopleHeader("X", "X"),
			new PeopleHeader("Y", "Y"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DD7 RID: 7639
		[PeopleHeaderProvider.SupportedLocaleAttribute(1110)]
		private static readonly List<PeopleHeader> GALICIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("A", "A"),
			new PeopleHeader("B", "B"),
			new PeopleHeader("C", "C"),
			new PeopleHeader("D", "D"),
			new PeopleHeader("E", "E"),
			new PeopleHeader("F", "F"),
			new PeopleHeader("G", "G"),
			new PeopleHeader("H", "H"),
			new PeopleHeader("I", "I"),
			new PeopleHeader("J", "J"),
			new PeopleHeader("K", "K"),
			new PeopleHeader("L", "L"),
			new PeopleHeader("M", "M"),
			new PeopleHeader("N", "N"),
			new PeopleHeader("O", "O"),
			new PeopleHeader("P", "P"),
			new PeopleHeader("Q", "Q"),
			new PeopleHeader("R", "R"),
			new PeopleHeader("S", "S"),
			new PeopleHeader("T", "T"),
			new PeopleHeader("U", "U"),
			new PeopleHeader("V", "V"),
			new PeopleHeader("W", "W"),
			new PeopleHeader("X", "X"),
			new PeopleHeader("Y", "Y"),
			new PeopleHeader("Z", "Z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DD8 RID: 7640
		[PeopleHeaderProvider.SupportedLocaleAttribute(1038)]
		private static readonly List<PeopleHeader> HUNGARIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("ö", "ö"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("ü", "ü"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DD9 RID: 7641
		[PeopleHeaderProvider.SupportedLocaleAttribute(1039)]
		private static readonly List<PeopleHeader> ICELANDIC = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("á", "á"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("ð", "ð"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("é", "é"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("í", "í"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("ó", "ó"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("ú", "ú"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("ý", "ý"),
			new PeopleHeader("þ", "þ"),
			new PeopleHeader("æ", "æ"),
			new PeopleHeader("ö", "ö"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DDA RID: 7642
		[PeopleHeaderProvider.SupportedLocaleAttribute(1062)]
		private static readonly List<PeopleHeader> LATVIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("ā", "ā"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("č", "č"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("ē", "ē"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("ģ", "ģ"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("ī", "ī"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("ķ", "ķ"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("ļ", "ļ"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("ņ", "ņ"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("š", "š"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("ū", "ū"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("ž", "ž"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DDB RID: 7643
		[PeopleHeaderProvider.SupportedLocaleAttribute(1045)]
		private static readonly List<PeopleHeader> POLISH = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("ą", "ą"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("ć", "ć"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("ę", "ę"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("ł", "ł"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("ń", "ń"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("ó", "ó"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("ś", "ś"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("ź", "ź"),
			new PeopleHeader("ż", "ż"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DDC RID: 7644
		[PeopleHeaderProvider.SupportedLocaleAttribute(1048)]
		private static readonly List<PeopleHeader> ROMANIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("ă", "ă"),
			new PeopleHeader("â", "â"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("î", "î"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("ş", "ş"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("ţ", "ţ"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DDD RID: 7645
		[PeopleHeaderProvider.SupportedLocaleAttribute(2074)]
		private static readonly List<PeopleHeader> SERBIANLATN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("A", "A"),
			new PeopleHeader("B", "B"),
			new PeopleHeader("C", "C"),
			new PeopleHeader("Č", "Č"),
			new PeopleHeader("Ć", "Ć"),
			new PeopleHeader("D", "D"),
			new PeopleHeader("Đ", "Đ"),
			new PeopleHeader("E", "E"),
			new PeopleHeader("F", "F"),
			new PeopleHeader("G", "G"),
			new PeopleHeader("H", "H"),
			new PeopleHeader("I", "I"),
			new PeopleHeader("J", "J"),
			new PeopleHeader("K", "K"),
			new PeopleHeader("L", "L"),
			new PeopleHeader("M", "M"),
			new PeopleHeader("N", "N"),
			new PeopleHeader("O", "O"),
			new PeopleHeader("P", "P"),
			new PeopleHeader("Q", "Q"),
			new PeopleHeader("R", "R"),
			new PeopleHeader("S", "S"),
			new PeopleHeader("Š", "Š"),
			new PeopleHeader("T", "T"),
			new PeopleHeader("U", "U"),
			new PeopleHeader("V", "V"),
			new PeopleHeader("W", "W"),
			new PeopleHeader("X", "X"),
			new PeopleHeader("Y", "Y"),
			new PeopleHeader("Z", "Z"),
			new PeopleHeader("Ž", "Ž"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DDE RID: 7646
		[PeopleHeaderProvider.SupportedLocaleAttribute(1051)]
		private static readonly List<PeopleHeader> SLOVAKIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("č", "č"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("ch", "ch"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("š", "š"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("ž", "ž"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DDF RID: 7647
		[PeopleHeaderProvider.SupportedLocaleAttribute(1060)]
		private static readonly List<PeopleHeader> SLOVENIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("č", "č"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("š", "š"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("ž", "ž"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE0 RID: 7648
		[PeopleHeaderProvider.SupportedLocaleAttribute(14346)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(20490)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(16394)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(13322)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(15370)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(6154)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(10250)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(9226)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(5130)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(17418)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(7178)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(8202)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(12298)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(3082)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(4106)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(18442)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(2058)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(11274)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(19466)]
		private static readonly List<PeopleHeader> SPANISHMODRN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("ñ", "ñ"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE1 RID: 7649
		[PeopleHeaderProvider.SupportedLocaleAttribute(1035)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(2077)]
		[PeopleHeaderProvider.SupportedLocaleAttribute(1053)]
		private static readonly List<PeopleHeader> FINNISH_SWEDISH = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("å", "å"),
			new PeopleHeader("ä", "ä"),
			new PeopleHeader("ö", "ö"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE2 RID: 7650
		[PeopleHeaderProvider.SupportedLocaleAttribute(1055)]
		private static readonly List<PeopleHeader> TURKISH = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("ç", "ç"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("ğ", "ğ"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("ı", "ı"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("ö", "ö"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("ş", "ş"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("ü", "ü"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE3 RID: 7651
		[PeopleHeaderProvider.SupportedLocaleAttribute(1063)]
		private static readonly List<PeopleHeader> LITHUANIAN = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("ą", "ą"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("č", "č"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("ę", "ę"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("į", "į"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("š", "š"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("ų", "ų"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("z", "z"),
			new PeopleHeader("ž", "ž"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE4 RID: 7652
		[PeopleHeaderProvider.SupportedLocaleAttribute(1066)]
		private static readonly List<PeopleHeader> VIETNAMESE = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("ă", "ă"),
			new PeopleHeader("â", "â"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("đ", "đ"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("ê", "ê"),
			new PeopleHeader("g", "g"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("ô", "ô"),
			new PeopleHeader("ơ", "ơ"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("ư", "ư"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE5 RID: 7653
		[PeopleHeaderProvider.SupportedLocaleAttribute(1106)]
		private static readonly List<PeopleHeader> WELSH = new List<PeopleHeader>
		{
			PeopleHeaderProvider.FirstHeader,
			new PeopleHeader("a", "a"),
			new PeopleHeader("b", "b"),
			new PeopleHeader("c", "c"),
			new PeopleHeader("d", "d"),
			new PeopleHeader("e", "e"),
			new PeopleHeader("f", "f"),
			new PeopleHeader("h", "h"),
			new PeopleHeader("i", "i"),
			new PeopleHeader("j", "j"),
			new PeopleHeader("k", "k"),
			new PeopleHeader("l", "l"),
			new PeopleHeader("m", "m"),
			new PeopleHeader("n", "n"),
			new PeopleHeader("o", "o"),
			new PeopleHeader("p", "p"),
			new PeopleHeader("q", "q"),
			new PeopleHeader("r", "r"),
			new PeopleHeader("s", "s"),
			new PeopleHeader("t", "t"),
			new PeopleHeader("u", "u"),
			new PeopleHeader("v", "v"),
			new PeopleHeader("w", "w"),
			new PeopleHeader("x", "x"),
			new PeopleHeader("y", "y"),
			new PeopleHeader("z", "z"),
			PeopleHeaderProvider.LastHeader
		};

		// Token: 0x04001DE6 RID: 7654
		private static readonly PeopleHeaderProvider instance = new PeopleHeaderProvider();

		// Token: 0x04001DE7 RID: 7655
		private readonly List<PeopleHeader> fallbackHeaders = PeopleHeaderProvider.ASCII_LOWER;

		// Token: 0x04001DE8 RID: 7656
		private readonly Dictionary<int, List<PeopleHeader>> localeHeaders;

		// Token: 0x02000519 RID: 1305
		[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
		private class SupportedLocaleAttribute : Attribute
		{
			// Token: 0x06003804 RID: 14340 RVA: 0x000E5904 File Offset: 0x000E3B04
			public SupportedLocaleAttribute(int lcid)
			{
				this.lcid = lcid;
			}

			// Token: 0x17001157 RID: 4439
			// (get) Token: 0x06003805 RID: 14341 RVA: 0x000E5913 File Offset: 0x000E3B13
			public int LCID
			{
				get
				{
					return this.lcid;
				}
			}

			// Token: 0x04001DEA RID: 7658
			private readonly int lcid;
		}
	}
}
