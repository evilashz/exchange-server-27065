using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.CommonTypes
{
	// Token: 0x02000082 RID: 130
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FolderId
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001961B File Offset: 0x0001781B
		internal FolderId()
		{
			this.isClientIdField = 0;
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0001962A File Offset: 0x0001782A
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00019632 File Offset: 0x00017832
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0001963B File Offset: 0x0001783B
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00019643 File Offset: 0x00017843
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

		// Token: 0x0400030F RID: 783
		private byte isClientIdField;

		// Token: 0x04000310 RID: 784
		private string valueField;
	}
}
