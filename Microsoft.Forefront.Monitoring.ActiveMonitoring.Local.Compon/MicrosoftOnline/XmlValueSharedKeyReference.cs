using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.MicrosoftOnline
{
	// Token: 0x020000B0 RID: 176
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class XmlValueSharedKeyReference
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001EFE7 File Offset: 0x0001D1E7
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0001EFEF File Offset: 0x0001D1EF
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

		// Token: 0x04000311 RID: 785
		private SharedKeyReferenceValue sharedKeyReferenceField;
	}
}
