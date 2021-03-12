using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000084 RID: 132
	public class UrlSet : ExtractionSet<Url>
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000E307 File Offset: 0x0000C507
		public UrlSet() : base(new UrlSerializer())
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000E314 File Offset: 0x0000C514
		public static implicit operator UrlSet(Url[] urls)
		{
			return new UrlSet
			{
				Extractions = urls
			};
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000E32F File Offset: 0x0000C52F
		public static XmlSerializer Serializer
		{
			get
			{
				return UrlSet.serializer;
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000E336 File Offset: 0x0000C536
		public override XmlSerializer GetSerializer()
		{
			return UrlSet.serializer;
		}

		// Token: 0x04000194 RID: 404
		private static XmlSerializer serializer = new XmlSerializer(typeof(UrlSet));
	}
}
