using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000917 RID: 2327
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class SelectionConditionValue
	{
		// Token: 0x17002788 RID: 10120
		// (get) Token: 0x06006F44 RID: 28484 RVA: 0x00176A6C File Offset: 0x00174C6C
		// (set) Token: 0x06006F45 RID: 28485 RVA: 0x00176A74 File Offset: 0x00174C74
		[XmlArray(Order = 0)]
		[XmlArrayItem("Value", IsNullable = false)]
		public string[] Values
		{
			get
			{
				return this.valuesField;
			}
			set
			{
				this.valuesField = value;
			}
		}

		// Token: 0x17002789 RID: 10121
		// (get) Token: 0x06006F46 RID: 28486 RVA: 0x00176A7D File Offset: 0x00174C7D
		// (set) Token: 0x06006F47 RID: 28487 RVA: 0x00176A85 File Offset: 0x00174C85
		[XmlAttribute]
		public int Claim
		{
			get
			{
				return this.claimField;
			}
			set
			{
				this.claimField = value;
			}
		}

		// Token: 0x1700278A RID: 10122
		// (get) Token: 0x06006F48 RID: 28488 RVA: 0x00176A8E File Offset: 0x00174C8E
		// (set) Token: 0x06006F49 RID: 28489 RVA: 0x00176A96 File Offset: 0x00174C96
		[XmlAttribute]
		public int Operator
		{
			get
			{
				return this.operatorField;
			}
			set
			{
				this.operatorField = value;
			}
		}

		// Token: 0x04004834 RID: 18484
		private string[] valuesField;

		// Token: 0x04004835 RID: 18485
		private int claimField;

		// Token: 0x04004836 RID: 18486
		private int operatorField;
	}
}
