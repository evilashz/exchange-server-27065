using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002E8 RID: 744
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(PermissionType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlInclude(typeof(CalendarPermissionType))]
	[Serializable]
	public abstract class BasePermissionType
	{
		// Token: 0x04001288 RID: 4744
		public UserIdType UserId;

		// Token: 0x04001289 RID: 4745
		public bool CanCreateItems;

		// Token: 0x0400128A RID: 4746
		[XmlIgnore]
		public bool CanCreateItemsSpecified;

		// Token: 0x0400128B RID: 4747
		public bool CanCreateSubFolders;

		// Token: 0x0400128C RID: 4748
		[XmlIgnore]
		public bool CanCreateSubFoldersSpecified;

		// Token: 0x0400128D RID: 4749
		public bool IsFolderOwner;

		// Token: 0x0400128E RID: 4750
		[XmlIgnore]
		public bool IsFolderOwnerSpecified;

		// Token: 0x0400128F RID: 4751
		public bool IsFolderVisible;

		// Token: 0x04001290 RID: 4752
		[XmlIgnore]
		public bool IsFolderVisibleSpecified;

		// Token: 0x04001291 RID: 4753
		public bool IsFolderContact;

		// Token: 0x04001292 RID: 4754
		[XmlIgnore]
		public bool IsFolderContactSpecified;

		// Token: 0x04001293 RID: 4755
		public PermissionActionType EditItems;

		// Token: 0x04001294 RID: 4756
		[XmlIgnore]
		public bool EditItemsSpecified;

		// Token: 0x04001295 RID: 4757
		public PermissionActionType DeleteItems;

		// Token: 0x04001296 RID: 4758
		[XmlIgnore]
		public bool DeleteItemsSpecified;
	}
}
