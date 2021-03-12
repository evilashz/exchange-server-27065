using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.Tools
{
	// Token: 0x02001249 RID: 4681
	[GeneratedCode("xsd", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CustomSupportedVersion : SupportedVersion
	{
		// Token: 0x17003B80 RID: 15232
		// (get) Token: 0x0600BC5F RID: 48223 RVA: 0x002ACFB0 File Offset: 0x002AB1B0
		// (set) Token: 0x0600BC60 RID: 48224 RVA: 0x002ACFB8 File Offset: 0x002AB1B8
		[XmlAttribute]
		public string minTenantVersion
		{
			get
			{
				return this.minTenantVersionField;
			}
			set
			{
				this.minTenantVersionField = value;
			}
		}

		// Token: 0x17003B81 RID: 15233
		// (get) Token: 0x0600BC61 RID: 48225 RVA: 0x002ACFC1 File Offset: 0x002AB1C1
		// (set) Token: 0x0600BC62 RID: 48226 RVA: 0x002ACFC9 File Offset: 0x002AB1C9
		[XmlAttribute]
		public string maxTenantVersion
		{
			get
			{
				return this.maxTenantVersionField;
			}
			set
			{
				this.maxTenantVersionField = value;
			}
		}

		// Token: 0x04006733 RID: 26419
		private string minTenantVersionField;

		// Token: 0x04006734 RID: 26420
		private string maxTenantVersionField;
	}
}
