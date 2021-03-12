using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200092E RID: 2350
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[Serializable]
	public class XmlValueStsAddress
	{
		// Token: 0x170027B1 RID: 10161
		// (get) Token: 0x06006FA9 RID: 28585 RVA: 0x00176DBD File Offset: 0x00174FBD
		// (set) Token: 0x06006FAA RID: 28586 RVA: 0x00176DC5 File Offset: 0x00174FC5
		[XmlElement(Order = 0)]
		public StsAddressValue StsAddress
		{
			get
			{
				return this.stsAddressField;
			}
			set
			{
				this.stsAddressField = value;
			}
		}

		// Token: 0x04004875 RID: 18549
		private StsAddressValue stsAddressField;
	}
}
