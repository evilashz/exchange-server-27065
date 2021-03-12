using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000211 RID: 529
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(SearchFolderType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(TasksFolderType))]
	[Serializable]
	public class FolderType : BaseFolderType
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00026139 File Offset: 0x00024339
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x00026141 File Offset: 0x00024341
		public PermissionSetType PermissionSet
		{
			get
			{
				return this.permissionSetField;
			}
			set
			{
				this.permissionSetField = value;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x0002614A File Offset: 0x0002434A
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00026152 File Offset: 0x00024352
		public int UnreadCount
		{
			get
			{
				return this.unreadCountField;
			}
			set
			{
				this.unreadCountField = value;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0002615B File Offset: 0x0002435B
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00026163 File Offset: 0x00024363
		[XmlIgnore]
		public bool UnreadCountSpecified
		{
			get
			{
				return this.unreadCountFieldSpecified;
			}
			set
			{
				this.unreadCountFieldSpecified = value;
			}
		}

		// Token: 0x04000E80 RID: 3712
		private PermissionSetType permissionSetField;

		// Token: 0x04000E81 RID: 3713
		private int unreadCountField;

		// Token: 0x04000E82 RID: 3714
		private bool unreadCountFieldSpecified;
	}
}
