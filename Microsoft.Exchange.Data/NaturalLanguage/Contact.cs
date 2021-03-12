using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000072 RID: 114
	public class Contact : IPositionedExtraction
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000DDCF File Offset: 0x0000BFCF
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000DDD7 File Offset: 0x0000BFD7
		public Person Person { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		public Business Business { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000DDF1 File Offset: 0x0000BFF1
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000DDF9 File Offset: 0x0000BFF9
		public Phone[] Phones { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000DE02 File Offset: 0x0000C002
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000DE0A File Offset: 0x0000C00A
		public Url[] Urls { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000DE13 File Offset: 0x0000C013
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000DE1B File Offset: 0x0000C01B
		public Email[] Emails { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000DE24 File Offset: 0x0000C024
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public Address[] Addresses { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000DE35 File Offset: 0x0000C035
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000DE3D File Offset: 0x0000C03D
		public string ContactString { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000DE46 File Offset: 0x0000C046
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000DE4E File Offset: 0x0000C04E
		[DefaultValue(-1)]
		[XmlAttribute]
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
			set
			{
				this._startIndex = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000DE57 File Offset: 0x0000C057
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000DE5F File Offset: 0x0000C05F
		[XmlAttribute]
		[DefaultValue(EmailPosition.LatestReply)]
		public EmailPosition Position { get; set; }

		// Token: 0x0400017A RID: 378
		private int _startIndex = -1;
	}
}
