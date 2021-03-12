using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class stringWithEncodingType1
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x000194A9 File Offset: 0x000176A9
		internal stringWithEncodingType1()
		{
			this.encodingField = "0";
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000194BC File Offset: 0x000176BC
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x000194C4 File Offset: 0x000176C4
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000194CD File Offset: 0x000176CD
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x000194D5 File Offset: 0x000176D5
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

		// Token: 0x040002FC RID: 764
		private string encodingField;

		// Token: 0x040002FD RID: 765
		private string valueField;
	}
}
