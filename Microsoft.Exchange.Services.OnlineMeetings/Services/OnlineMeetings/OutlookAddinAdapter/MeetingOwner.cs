using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C9 RID: 201
	[DataContract(Name = "MeetingOwner")]
	[KnownType(typeof(MeetingOwner))]
	[XmlType("MeetingOwner")]
	public class MeetingOwner
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000C074 File Offset: 0x0000A274
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000C07C File Offset: 0x0000A27C
		[XmlAttribute("Smtp")]
		[DataMember(Name = "Smtp", EmitDefaultValue = true)]
		public string Smtp { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0000C085 File Offset: 0x0000A285
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0000C08D File Offset: 0x0000A28D
		[DataMember(Name = "Sip", EmitDefaultValue = true)]
		[XmlAttribute("Sip")]
		public string Sip { get; set; }

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000C098 File Offset: 0x0000A298
		public static MeetingOwner ConvertFrom(string organizerUri)
		{
			return new MeetingOwner
			{
				Smtp = organizerUri,
				Sip = organizerUri
			};
		}
	}
}
