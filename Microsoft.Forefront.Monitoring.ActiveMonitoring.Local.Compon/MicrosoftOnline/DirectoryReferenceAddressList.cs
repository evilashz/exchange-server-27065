using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x02000110 RID: 272
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[Serializable]
	public class DirectoryReferenceAddressList : DirectoryReference
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002019B File Offset: 0x0001E39B
		// (set) Token: 0x06000804 RID: 2052 RVA: 0x000201A3 File Offset: 0x0001E3A3
		[XmlAttribute]
		public DirectoryObjectClassAddressList TargetClass
		{
			get
			{
				return this.targetClassField;
			}
			set
			{
				this.targetClassField = value;
			}
		}

		// Token: 0x04000435 RID: 1077
		private DirectoryObjectClassAddressList targetClassField;
	}
}
