using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000085 RID: 133
	public class EmailSet : ExtractionSet<Email>
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000E353 File Offset: 0x0000C553
		public EmailSet() : base(new EmailSerializer())
		{
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000E360 File Offset: 0x0000C560
		public static implicit operator EmailSet(Email[] emails)
		{
			return new EmailSet
			{
				Extractions = emails
			};
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E37B File Offset: 0x0000C57B
		public static XmlSerializer Serializer
		{
			get
			{
				return EmailSet.serializer;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E382 File Offset: 0x0000C582
		public override XmlSerializer GetSerializer()
		{
			return EmailSet.serializer;
		}

		// Token: 0x04000195 RID: 405
		private static XmlSerializer serializer = new XmlSerializer(typeof(EmailSet));
	}
}
