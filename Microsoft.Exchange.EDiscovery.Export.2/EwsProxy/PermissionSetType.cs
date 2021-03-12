using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000210 RID: 528
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PermissionSetType
	{
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0002610F File Offset: 0x0002430F
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x00026117 File Offset: 0x00024317
		[XmlArrayItem("Permission", IsNullable = false)]
		public PermissionType[] Permissions
		{
			get
			{
				return this.permissionsField;
			}
			set
			{
				this.permissionsField = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00026120 File Offset: 0x00024320
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x00026128 File Offset: 0x00024328
		[XmlArrayItem("UnknownEntry", IsNullable = false)]
		public string[] UnknownEntries
		{
			get
			{
				return this.unknownEntriesField;
			}
			set
			{
				this.unknownEntriesField = value;
			}
		}

		// Token: 0x04000E7E RID: 3710
		private PermissionType[] permissionsField;

		// Token: 0x04000E7F RID: 3711
		private string[] unknownEntriesField;
	}
}
