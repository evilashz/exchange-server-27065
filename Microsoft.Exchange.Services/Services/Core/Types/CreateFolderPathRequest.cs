using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000405 RID: 1029
	[XmlType("CreateFolderPathType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class CreateFolderPathRequest : BaseRequest
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0009F01A File Offset: 0x0009D21A
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0009F022 File Offset: 0x0009D222
		[XmlElement("ParentFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "ParentFolderId", IsRequired = true, Order = 1)]
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0009F02B File Offset: 0x0009D22B
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x0009F033 File Offset: 0x0009D233
		[DataMember(Name = "RelativeFolderPath", IsRequired = true, Order = 2)]
		[XmlArrayItem("CalendarFolder", typeof(CalendarFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArray("RelativeFolderPath")]
		[XmlArrayItem("ContactsFolder", typeof(ContactsFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("Folder", typeof(FolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("TasksFolder", typeof(TasksFolderType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderType[] RelativeFolderPath { get; set; }

		// Token: 0x06001D39 RID: 7481 RVA: 0x0009F03C File Offset: 0x0009D23C
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new CreateFolderPath(callContext, this);
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0009F045 File Offset: 0x0009D245
		internal override bool IsHierarchicalOperation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x0009F048 File Offset: 0x0009D248
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return BaseRequest.GetServerInfoForFolderIdHierarchyOperations(callContext, this.ParentFolderId.BaseFolderId);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x0009F05B File Offset: 0x0009D25B
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysForFolderIdHierarchyOperations(true, callContext, this.ParentFolderId.BaseFolderId);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x0009F070 File Offset: 0x0009D270
		internal override void Validate()
		{
			base.Validate();
			if (this.RelativeFolderPath == null || this.RelativeFolderPath.Length == 0)
			{
				throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidIdMalformed), FaultParty.Sender);
			}
			foreach (BaseFolderType baseFolderType in this.RelativeFolderPath)
			{
				if (baseFolderType.StoreObjectType == StoreObjectType.SearchFolder || baseFolderType.StoreObjectType == StoreObjectType.OutlookSearchFolder)
				{
					throw FaultExceptionUtilities.CreateFault(new ServiceInvalidOperationException(ResponseCodeType.ErrorInvalidFolderTypeForOperation), FaultParty.Sender);
				}
			}
		}

		// Token: 0x04001312 RID: 4882
		internal const string ParentFolderIdElementName = "ParentFolderId";

		// Token: 0x04001313 RID: 4883
		internal const string RelativePathElementName = "RelativeFolderPath";
	}
}
