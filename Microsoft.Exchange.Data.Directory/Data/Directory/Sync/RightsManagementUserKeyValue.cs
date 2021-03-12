using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000927 RID: 2343
	[XmlType(Namespace = "http://schemas.microsoft.com/online/directoryservices/change/2008/11")]
	[GeneratedCode("svcutil", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RightsManagementUserKeyValue
	{
		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x06006F90 RID: 28560 RVA: 0x00176CEC File Offset: 0x00174EEC
		// (set) Token: 0x06006F91 RID: 28561 RVA: 0x00176CF4 File Offset: 0x00174EF4
		[XmlElement(DataType = "base64Binary", Order = 0)]
		public byte[] Key
		{
			get
			{
				return this.keyField;
			}
			set
			{
				this.keyField = value;
			}
		}

		// Token: 0x170027A9 RID: 10153
		// (get) Token: 0x06006F92 RID: 28562 RVA: 0x00176CFD File Offset: 0x00174EFD
		// (set) Token: 0x06006F93 RID: 28563 RVA: 0x00176D05 File Offset: 0x00174F05
		[XmlAttribute]
		public int Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x0400486C RID: 18540
		private byte[] keyField;

		// Token: 0x0400486D RID: 18541
		private int versionField;
	}
}
