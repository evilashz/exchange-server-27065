using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000CD RID: 205
	[DataContract(Name = "User")]
	[XmlType("User")]
	[KnownType(typeof(User))]
	public class User
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000C24F File Offset: 0x0000A44F
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000C257 File Offset: 0x0000A457
		[DataMember(Name = "Name", EmitDefaultValue = true)]
		[XmlAttribute("name")]
		public string Name { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000C260 File Offset: 0x0000A460
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0000C268 File Offset: 0x0000A468
		[DataMember(Name = "SmtpAddress", EmitDefaultValue = true)]
		[XmlAttribute("Smtp")]
		public string SmtpAddress { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000C271 File Offset: 0x0000A471
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0000C279 File Offset: 0x0000A479
		[DataMember(Name = "SipAddress", EmitDefaultValue = true)]
		[XmlAttribute("Sip")]
		public string SipAddress { get; set; }
	}
}
