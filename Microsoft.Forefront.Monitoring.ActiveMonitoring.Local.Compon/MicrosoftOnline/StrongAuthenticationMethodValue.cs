using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000A6 RID: 166
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class StrongAuthenticationMethodValue
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x0001EE46 File Offset: 0x0001D046
		public StrongAuthenticationMethodValue()
		{
			this.defaultField = false;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001EE55 File Offset: 0x0001D055
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0001EE5D File Offset: 0x0001D05D
		[XmlAttribute]
		public int MethodType
		{
			get
			{
				return this.methodTypeField;
			}
			set
			{
				this.methodTypeField = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0001EE66 File Offset: 0x0001D066
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0001EE6E File Offset: 0x0001D06E
		[XmlAttribute]
		[DefaultValue(false)]
		public bool Default
		{
			get
			{
				return this.defaultField;
			}
			set
			{
				this.defaultField = value;
			}
		}

		// Token: 0x040002FE RID: 766
		private int methodTypeField;

		// Token: 0x040002FF RID: 767
		private bool defaultField;
	}
}
