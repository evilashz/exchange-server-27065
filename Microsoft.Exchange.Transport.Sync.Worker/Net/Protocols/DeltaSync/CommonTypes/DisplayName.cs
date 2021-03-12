using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000080 RID: 128
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DisplayName
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x000195A4 File Offset: 0x000177A4
		internal DisplayName()
		{
			this.encodingField = "0";
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x000195B7 File Offset: 0x000177B7
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x000195BF File Offset: 0x000177BF
		internal string charset
		{
			get
			{
				return this.charsetField;
			}
			set
			{
				this.charsetField = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x000195C8 File Offset: 0x000177C8
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x000195D0 File Offset: 0x000177D0
		[DefaultValue("0")]
		internal string encoding
		{
			get
			{
				return this.encodingField;
			}
			set
			{
				this.encodingField = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000195D9 File Offset: 0x000177D9
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x000195E1 File Offset: 0x000177E1
		[XmlText]
		internal string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x0400030A RID: 778
		private string charsetField;

		// Token: 0x0400030B RID: 779
		private string encodingField;

		// Token: 0x0400030C RID: 780
		private string valueField;
	}
}
