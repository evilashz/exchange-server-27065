using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations
{
	// Token: 0x020000A2 RID: 162
	[XmlType(Namespace = "ItemOperations", TypeName = "Options")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Options
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000A520 File Offset: 0x00008720
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000A528 File Offset: 0x00008728
		[XmlElement(ElementName = "MIMESupport", Namespace = "AirSync")]
		public byte? MimeSupport { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000A531 File Offset: 0x00008731
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000A539 File Offset: 0x00008739
		[XmlElement(ElementName = "BodyPreference", Namespace = "AirSyncBase")]
		public BodyPreference BodyPreference { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A542 File Offset: 0x00008742
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000A54A File Offset: 0x0000874A
		[XmlElement(ElementName = "Password")]
		public string Password { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000A553 File Offset: 0x00008753
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000A55B File Offset: 0x0000875B
		[XmlElement(ElementName = "Range")]
		public string Range { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000A564 File Offset: 0x00008764
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000A56C File Offset: 0x0000876C
		[XmlElement(ElementName = "Schema")]
		public Schema Schema { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000A575 File Offset: 0x00008775
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000A57D File Offset: 0x0000877D
		[XmlElement(ElementName = "UserName")]
		public string UserName { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000A588 File Offset: 0x00008788
		[XmlIgnore]
		public bool MimeSupportSpecified
		{
			get
			{
				return this.MimeSupport != null;
			}
		}
	}
}
