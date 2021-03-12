using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000379 RID: 889
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ResolveNamesType : BaseRequestType
	{
		// Token: 0x06001C2E RID: 7214 RVA: 0x00029D79 File Offset: 0x00027F79
		public ResolveNamesType()
		{
			this.searchScopeField = ResolveNamesSearchScopeType.ActiveDirectoryContacts;
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00029D88 File Offset: 0x00027F88
		// (set) Token: 0x06001C30 RID: 7216 RVA: 0x00029D90 File Offset: 0x00027F90
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderIdType[] ParentFolderIds
		{
			get
			{
				return this.parentFolderIdsField;
			}
			set
			{
				this.parentFolderIdsField = value;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00029D99 File Offset: 0x00027F99
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x00029DA1 File Offset: 0x00027FA1
		public string UnresolvedEntry
		{
			get
			{
				return this.unresolvedEntryField;
			}
			set
			{
				this.unresolvedEntryField = value;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00029DAA File Offset: 0x00027FAA
		// (set) Token: 0x06001C34 RID: 7220 RVA: 0x00029DB2 File Offset: 0x00027FB2
		[XmlAttribute]
		public bool ReturnFullContactData
		{
			get
			{
				return this.returnFullContactDataField;
			}
			set
			{
				this.returnFullContactDataField = value;
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x00029DBB File Offset: 0x00027FBB
		// (set) Token: 0x06001C36 RID: 7222 RVA: 0x00029DC3 File Offset: 0x00027FC3
		[XmlAttribute]
		[DefaultValue(ResolveNamesSearchScopeType.ActiveDirectoryContacts)]
		public ResolveNamesSearchScopeType SearchScope
		{
			get
			{
				return this.searchScopeField;
			}
			set
			{
				this.searchScopeField = value;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00029DCC File Offset: 0x00027FCC
		// (set) Token: 0x06001C38 RID: 7224 RVA: 0x00029DD4 File Offset: 0x00027FD4
		[XmlAttribute]
		public DefaultShapeNamesType ContactDataShape
		{
			get
			{
				return this.contactDataShapeField;
			}
			set
			{
				this.contactDataShapeField = value;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00029DDD File Offset: 0x00027FDD
		// (set) Token: 0x06001C3A RID: 7226 RVA: 0x00029DE5 File Offset: 0x00027FE5
		[XmlIgnore]
		public bool ContactDataShapeSpecified
		{
			get
			{
				return this.contactDataShapeFieldSpecified;
			}
			set
			{
				this.contactDataShapeFieldSpecified = value;
			}
		}

		// Token: 0x040012AB RID: 4779
		private BaseFolderIdType[] parentFolderIdsField;

		// Token: 0x040012AC RID: 4780
		private string unresolvedEntryField;

		// Token: 0x040012AD RID: 4781
		private bool returnFullContactDataField;

		// Token: 0x040012AE RID: 4782
		private ResolveNamesSearchScopeType searchScopeField;

		// Token: 0x040012AF RID: 4783
		private DefaultShapeNamesType contactDataShapeField;

		// Token: 0x040012B0 RID: 4784
		private bool contactDataShapeFieldSpecified;
	}
}
