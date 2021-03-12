using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D3 RID: 723
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(PullSubscriptionRequestType))]
	[XmlInclude(typeof(PushSubscriptionRequestType))]
	[DebuggerStepThrough]
	[Serializable]
	public abstract class BaseSubscriptionRequestType
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x00027E6F File Offset: 0x0002606F
		// (set) Token: 0x06001880 RID: 6272 RVA: 0x00027E77 File Offset: 0x00026077
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderIdType), IsNullable = false)]
		public BaseFolderIdType[] FolderIds
		{
			get
			{
				return this.folderIdsField;
			}
			set
			{
				this.folderIdsField = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00027E80 File Offset: 0x00026080
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x00027E88 File Offset: 0x00026088
		[XmlArrayItem("EventType", IsNullable = false)]
		public NotificationEventTypeType[] EventTypes
		{
			get
			{
				return this.eventTypesField;
			}
			set
			{
				this.eventTypesField = value;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00027E91 File Offset: 0x00026091
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x00027E99 File Offset: 0x00026099
		public string Watermark
		{
			get
			{
				return this.watermarkField;
			}
			set
			{
				this.watermarkField = value;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00027EA2 File Offset: 0x000260A2
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x00027EAA File Offset: 0x000260AA
		[XmlAttribute]
		public bool SubscribeToAllFolders
		{
			get
			{
				return this.subscribeToAllFoldersField;
			}
			set
			{
				this.subscribeToAllFoldersField = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00027EB3 File Offset: 0x000260B3
		// (set) Token: 0x06001888 RID: 6280 RVA: 0x00027EBB File Offset: 0x000260BB
		[XmlIgnore]
		public bool SubscribeToAllFoldersSpecified
		{
			get
			{
				return this.subscribeToAllFoldersFieldSpecified;
			}
			set
			{
				this.subscribeToAllFoldersFieldSpecified = value;
			}
		}

		// Token: 0x0400109D RID: 4253
		private BaseFolderIdType[] folderIdsField;

		// Token: 0x0400109E RID: 4254
		private NotificationEventTypeType[] eventTypesField;

		// Token: 0x0400109F RID: 4255
		private string watermarkField;

		// Token: 0x040010A0 RID: 4256
		private bool subscribeToAllFoldersField;

		// Token: 0x040010A1 RID: 4257
		private bool subscribeToAllFoldersFieldSpecified;
	}
}
