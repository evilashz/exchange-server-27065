using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000906 RID: 2310
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[DesignerCategory("code")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueAppMetadata
	{
		// Token: 0x17002769 RID: 10089
		// (get) Token: 0x06006EF5 RID: 28405 RVA: 0x001767C7 File Offset: 0x001749C7
		// (set) Token: 0x06006EF6 RID: 28406 RVA: 0x001767CF File Offset: 0x001749CF
		[XmlElement(Order = 0)]
		public AppMetadataValue AppMetadata
		{
			get
			{
				return this.appMetadataField;
			}
			set
			{
				this.appMetadataField = value;
			}
		}

		// Token: 0x04004815 RID: 18453
		private AppMetadataValue appMetadataField;
	}
}
