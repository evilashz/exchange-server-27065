using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000096 RID: 150
	[DataContract]
	public class FormatBarState
	{
		// Token: 0x06001BCD RID: 7117 RVA: 0x0005799C File Offset: 0x00055B9C
		public FormatBarState(string fontName, int fontSize, FontFlags fontFlags, string fontColor)
		{
			this.sFontName = fontName;
			this.iFontSize = fontSize;
			this.fBold = ((fontFlags & FontFlags.Bold) == FontFlags.Bold);
			this.fItalics = ((fontFlags & FontFlags.Italic) == FontFlags.Italic);
			this.fUnderline = ((fontFlags & FontFlags.Underline) == FontFlags.Underline);
			this.sTextColor = fontColor;
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06001BCE RID: 7118 RVA: 0x000579E9 File Offset: 0x00055BE9
		// (set) Token: 0x06001BCF RID: 7119 RVA: 0x000579F1 File Offset: 0x00055BF1
		[DataMember]
		public string sFontName { get; private set; }

		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06001BD0 RID: 7120 RVA: 0x000579FA File Offset: 0x00055BFA
		// (set) Token: 0x06001BD1 RID: 7121 RVA: 0x00057A02 File Offset: 0x00055C02
		[DataMember]
		public int iFontSize { get; private set; }

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00057A0B File Offset: 0x00055C0B
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00057A13 File Offset: 0x00055C13
		[DataMember]
		public bool fBold { get; private set; }

		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x00057A1C File Offset: 0x00055C1C
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x00057A24 File Offset: 0x00055C24
		[DataMember]
		public bool fItalics { get; private set; }

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x06001BD6 RID: 7126 RVA: 0x00057A2D File Offset: 0x00055C2D
		// (set) Token: 0x06001BD7 RID: 7127 RVA: 0x00057A35 File Offset: 0x00055C35
		[DataMember]
		public bool fUnderline { get; private set; }

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x06001BD8 RID: 7128 RVA: 0x00057A3E File Offset: 0x00055C3E
		// (set) Token: 0x06001BD9 RID: 7129 RVA: 0x00057A46 File Offset: 0x00055C46
		[DataMember]
		public string sTextColor { get; private set; }

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x00057A4F File Offset: 0x00055C4F
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x00057A57 File Offset: 0x00055C57
		[DataMember]
		public string sHighlightColor { get; private set; }
	}
}
