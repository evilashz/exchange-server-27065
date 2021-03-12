using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200090F RID: 2319
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class XmlValueStringKeyValuePair
	{
		// Token: 0x1700277C RID: 10108
		// (get) Token: 0x06006F24 RID: 28452 RVA: 0x00176952 File Offset: 0x00174B52
		// (set) Token: 0x06006F25 RID: 28453 RVA: 0x0017695A File Offset: 0x00174B5A
		[XmlElement(Order = 0)]
		public StringKeyValuePairValue StringKeyValuePair
		{
			get
			{
				return this.stringKeyValuePairField;
			}
			set
			{
				this.stringKeyValuePairField = value;
			}
		}

		// Token: 0x04004828 RID: 18472
		private StringKeyValuePairValue stringKeyValuePairField;
	}
}
