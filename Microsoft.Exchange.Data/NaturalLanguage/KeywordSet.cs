using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000082 RID: 130
	public class KeywordSet : ExtractionSet<Keyword>
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0000E26F File Offset: 0x0000C46F
		public KeywordSet() : base(new KeywordSerializer())
		{
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000E27C File Offset: 0x0000C47C
		public static implicit operator KeywordSet(Keyword[] keywords)
		{
			return new KeywordSet
			{
				Extractions = keywords
			};
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000E297 File Offset: 0x0000C497
		public static XmlSerializer Serializer
		{
			get
			{
				return KeywordSet.serializer;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000E29E File Offset: 0x0000C49E
		public override XmlSerializer GetSerializer()
		{
			return KeywordSet.serializer;
		}

		// Token: 0x04000192 RID: 402
		private static XmlSerializer serializer = new XmlSerializer(typeof(KeywordSet));
	}
}
