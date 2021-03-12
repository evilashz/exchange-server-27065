using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000081 RID: 129
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParentId
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x000195EA File Offset: 0x000177EA
		internal ParentId()
		{
			this.isClientIdField = 0;
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000195F9 File Offset: 0x000177F9
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00019601 File Offset: 0x00017801
		[DefaultValue(typeof(byte), "0")]
		internal byte isClientId
		{
			get
			{
				return this.isClientIdField;
			}
			set
			{
				this.isClientIdField = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001960A File Offset: 0x0001780A
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00019612 File Offset: 0x00017812
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

		// Token: 0x0400030D RID: 781
		private byte isClientIdField;

		// Token: 0x0400030E RID: 782
		private string valueField;
	}
}
