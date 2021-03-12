using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200091A RID: 2330
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueSharedKeyReference
	{
		// Token: 0x1700278F RID: 10127
		// (get) Token: 0x06006F55 RID: 28501 RVA: 0x00176AFB File Offset: 0x00174CFB
		// (set) Token: 0x06006F56 RID: 28502 RVA: 0x00176B03 File Offset: 0x00174D03
		[XmlElement(Order = 0)]
		public SharedKeyReferenceValue SharedKeyReference
		{
			get
			{
				return this.sharedKeyReferenceField;
			}
			set
			{
				this.sharedKeyReferenceField = value;
			}
		}

		// Token: 0x0400483B RID: 18491
		private SharedKeyReferenceValue sharedKeyReferenceField;
	}
}
