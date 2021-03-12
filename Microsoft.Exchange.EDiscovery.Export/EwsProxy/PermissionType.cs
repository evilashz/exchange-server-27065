using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200020B RID: 523
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PermissionType : BasePermissionType
	{
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00025F70 File Offset: 0x00024170
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x00025F78 File Offset: 0x00024178
		public PermissionReadAccessType ReadItems
		{
			get
			{
				return this.readItemsField;
			}
			set
			{
				this.readItemsField = value;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00025F81 File Offset: 0x00024181
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00025F89 File Offset: 0x00024189
		[XmlIgnore]
		public bool ReadItemsSpecified
		{
			get
			{
				return this.readItemsFieldSpecified;
			}
			set
			{
				this.readItemsFieldSpecified = value;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00025F92 File Offset: 0x00024192
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x00025F9A File Offset: 0x0002419A
		public PermissionLevelType PermissionLevel
		{
			get
			{
				return this.permissionLevelField;
			}
			set
			{
				this.permissionLevelField = value;
			}
		}

		// Token: 0x04000E59 RID: 3673
		private PermissionReadAccessType readItemsField;

		// Token: 0x04000E5A RID: 3674
		private bool readItemsFieldSpecified;

		// Token: 0x04000E5B RID: 3675
		private PermissionLevelType permissionLevelField;
	}
}
