using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D1 RID: 721
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class StreamingSubscriptionRequestType
	{
		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x00027E23 File Offset: 0x00026023
		// (set) Token: 0x06001877 RID: 6263 RVA: 0x00027E2B File Offset: 0x0002602B
		[XmlArrayItem("FolderId", typeof(FolderIdType), IsNullable = false)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderIdType), IsNullable = false)]
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

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x00027E34 File Offset: 0x00026034
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x00027E3C File Offset: 0x0002603C
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

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x00027E45 File Offset: 0x00026045
		// (set) Token: 0x0600187B RID: 6267 RVA: 0x00027E4D File Offset: 0x0002604D
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

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x00027E56 File Offset: 0x00026056
		// (set) Token: 0x0600187D RID: 6269 RVA: 0x00027E5E File Offset: 0x0002605E
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

		// Token: 0x04001091 RID: 4241
		private BaseFolderIdType[] folderIdsField;

		// Token: 0x04001092 RID: 4242
		private NotificationEventTypeType[] eventTypesField;

		// Token: 0x04001093 RID: 4243
		private bool subscribeToAllFoldersField;

		// Token: 0x04001094 RID: 4244
		private bool subscribeToAllFoldersFieldSpecified;
	}
}
