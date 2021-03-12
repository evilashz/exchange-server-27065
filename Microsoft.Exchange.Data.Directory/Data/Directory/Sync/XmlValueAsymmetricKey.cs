using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200091C RID: 2332
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueAsymmetricKey
	{
		// Token: 0x17002793 RID: 10131
		// (get) Token: 0x06006F5F RID: 28511 RVA: 0x00176B4F File Offset: 0x00174D4F
		// (set) Token: 0x06006F60 RID: 28512 RVA: 0x00176B57 File Offset: 0x00174D57
		[XmlElement(Order = 0)]
		public AsymmetricKeyValue AsymmetricKey
		{
			get
			{
				return this.asymmetricKeyField;
			}
			set
			{
				this.asymmetricKeyField = value;
			}
		}

		// Token: 0x0400483F RID: 18495
		private AsymmetricKeyValue asymmetricKeyField;
	}
}
