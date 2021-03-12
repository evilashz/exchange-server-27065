using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002CE RID: 718
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DelegatePermissionsType
	{
		// Token: 0x04001231 RID: 4657
		public DelegateFolderPermissionLevelType CalendarFolderPermissionLevel;

		// Token: 0x04001232 RID: 4658
		[XmlIgnore]
		public bool CalendarFolderPermissionLevelSpecified;

		// Token: 0x04001233 RID: 4659
		public DelegateFolderPermissionLevelType TasksFolderPermissionLevel;

		// Token: 0x04001234 RID: 4660
		[XmlIgnore]
		public bool TasksFolderPermissionLevelSpecified;

		// Token: 0x04001235 RID: 4661
		public DelegateFolderPermissionLevelType InboxFolderPermissionLevel;

		// Token: 0x04001236 RID: 4662
		[XmlIgnore]
		public bool InboxFolderPermissionLevelSpecified;

		// Token: 0x04001237 RID: 4663
		public DelegateFolderPermissionLevelType ContactsFolderPermissionLevel;

		// Token: 0x04001238 RID: 4664
		[XmlIgnore]
		public bool ContactsFolderPermissionLevelSpecified;

		// Token: 0x04001239 RID: 4665
		public DelegateFolderPermissionLevelType NotesFolderPermissionLevel;

		// Token: 0x0400123A RID: 4666
		[XmlIgnore]
		public bool NotesFolderPermissionLevelSpecified;

		// Token: 0x0400123B RID: 4667
		public DelegateFolderPermissionLevelType JournalFolderPermissionLevel;

		// Token: 0x0400123C RID: 4668
		[XmlIgnore]
		public bool JournalFolderPermissionLevelSpecified;
	}
}
