using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x0200007A RID: 122
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class stringWithCharSetType
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x00019463 File Offset: 0x00017663
		internal stringWithCharSetType()
		{
			this.encodingField = "0";
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00019476 File Offset: 0x00017676
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001947E File Offset: 0x0001767E
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00019487 File Offset: 0x00017687
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001948F File Offset: 0x0001768F
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00019498 File Offset: 0x00017698
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000194A0 File Offset: 0x000176A0
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

		// Token: 0x040002F9 RID: 761
		private string charsetField;

		// Token: 0x040002FA RID: 762
		private string encodingField;

		// Token: 0x040002FB RID: 763
		private string valueField;
	}
}
