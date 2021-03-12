using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000910 RID: 2320
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class StringKeyValuePairValue
	{
		// Token: 0x1700277D RID: 10109
		// (get) Token: 0x06006F27 RID: 28455 RVA: 0x0017696B File Offset: 0x00174B6B
		// (set) Token: 0x06006F28 RID: 28456 RVA: 0x00176973 File Offset: 0x00174B73
		[XmlAttribute]
		public string Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x1700277E RID: 10110
		// (get) Token: 0x06006F29 RID: 28457 RVA: 0x0017697C File Offset: 0x00174B7C
		// (set) Token: 0x06006F2A RID: 28458 RVA: 0x00176984 File Offset: 0x00174B84
		[XmlAttribute]
		public string Value
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

		// Token: 0x04004829 RID: 18473
		private string keyField;

		// Token: 0x0400482A RID: 18474
		private string valueField;
	}
}
