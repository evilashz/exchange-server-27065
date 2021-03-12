using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002DE RID: 734
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxAssociationType
	{
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x000280E8 File Offset: 0x000262E8
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x000280F0 File Offset: 0x000262F0
		public GroupLocatorType Group
		{
			get
			{
				return this.groupField;
			}
			set
			{
				this.groupField = value;
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x000280F9 File Offset: 0x000262F9
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x00028101 File Offset: 0x00026301
		public UserLocatorType User
		{
			get
			{
				return this.userField;
			}
			set
			{
				this.userField = value;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0002810A File Offset: 0x0002630A
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x00028112 File Offset: 0x00026312
		public bool IsMember
		{
			get
			{
				return this.isMemberField;
			}
			set
			{
				this.isMemberField = value;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0002811B File Offset: 0x0002631B
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00028123 File Offset: 0x00026323
		[XmlIgnore]
		public bool IsMemberSpecified
		{
			get
			{
				return this.isMemberFieldSpecified;
			}
			set
			{
				this.isMemberFieldSpecified = value;
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0002812C File Offset: 0x0002632C
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00028134 File Offset: 0x00026334
		public DateTime JoinDate
		{
			get
			{
				return this.joinDateField;
			}
			set
			{
				this.joinDateField = value;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x0002813D File Offset: 0x0002633D
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x00028145 File Offset: 0x00026345
		[XmlIgnore]
		public bool JoinDateSpecified
		{
			get
			{
				return this.joinDateFieldSpecified;
			}
			set
			{
				this.joinDateFieldSpecified = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x0002814E File Offset: 0x0002634E
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x00028156 File Offset: 0x00026356
		public bool IsPin
		{
			get
			{
				return this.isPinField;
			}
			set
			{
				this.isPinField = value;
			}
		}

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0002815F File Offset: 0x0002635F
		// (set) Token: 0x060018D9 RID: 6361 RVA: 0x00028167 File Offset: 0x00026367
		[XmlIgnore]
		public bool IsPinSpecified
		{
			get
			{
				return this.isPinFieldSpecified;
			}
			set
			{
				this.isPinFieldSpecified = value;
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x00028170 File Offset: 0x00026370
		// (set) Token: 0x060018DB RID: 6363 RVA: 0x00028178 File Offset: 0x00026378
		public string JoinedBy
		{
			get
			{
				return this.joinedByField;
			}
			set
			{
				this.joinedByField = value;
			}
		}

		// Token: 0x040010C7 RID: 4295
		private GroupLocatorType groupField;

		// Token: 0x040010C8 RID: 4296
		private UserLocatorType userField;

		// Token: 0x040010C9 RID: 4297
		private bool isMemberField;

		// Token: 0x040010CA RID: 4298
		private bool isMemberFieldSpecified;

		// Token: 0x040010CB RID: 4299
		private DateTime joinDateField;

		// Token: 0x040010CC RID: 4300
		private bool joinDateFieldSpecified;

		// Token: 0x040010CD RID: 4301
		private bool isPinField;

		// Token: 0x040010CE RID: 4302
		private bool isPinFieldSpecified;

		// Token: 0x040010CF RID: 4303
		private string joinedByField;
	}
}
