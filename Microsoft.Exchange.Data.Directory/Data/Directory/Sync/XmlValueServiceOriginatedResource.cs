using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000938 RID: 2360
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DebuggerStepThrough]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueServiceOriginatedResource
	{
		// Token: 0x170027BF RID: 10175
		// (get) Token: 0x06006FCD RID: 28621 RVA: 0x00176EEB File Offset: 0x001750EB
		// (set) Token: 0x06006FCE RID: 28622 RVA: 0x00176EF3 File Offset: 0x001750F3
		[XmlElement(Order = 0)]
		public ServiceOriginatedResourceValue Resource
		{
			get
			{
				return this.resourceField;
			}
			set
			{
				this.resourceField = value;
			}
		}

		// Token: 0x04004891 RID: 18577
		private ServiceOriginatedResourceValue resourceField;
	}
}
