using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x0200007D RID: 125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class stringWithEncodingType
	{
		// Token: 0x06000594 RID: 1428 RVA: 0x0001952F File Offset: 0x0001772F
		internal stringWithEncodingType()
		{
			this.encodingField = "0";
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00019542 File Offset: 0x00017742
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0001954A File Offset: 0x0001774A
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

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00019553 File Offset: 0x00017753
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001955B File Offset: 0x0001775B
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

		// Token: 0x04000301 RID: 769
		private string encodingField;

		// Token: 0x04000302 RID: 770
		private string valueField;
	}
}
