using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000081 RID: 129
	public class MeetingSet : ExtractionSet<Meeting>
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000E223 File Offset: 0x0000C423
		public MeetingSet() : base(new MeetingSerializer())
		{
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000E230 File Offset: 0x0000C430
		public static implicit operator MeetingSet(Meeting[] meetings)
		{
			return new MeetingSet
			{
				Extractions = meetings
			};
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000E24B File Offset: 0x0000C44B
		public static XmlSerializer Serializer
		{
			get
			{
				return MeetingSet.serializer;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000E252 File Offset: 0x0000C452
		public override XmlSerializer GetSerializer()
		{
			return MeetingSet.serializer;
		}

		// Token: 0x04000191 RID: 401
		private static XmlSerializer serializer = new XmlSerializer(typeof(MeetingSet));
	}
}
