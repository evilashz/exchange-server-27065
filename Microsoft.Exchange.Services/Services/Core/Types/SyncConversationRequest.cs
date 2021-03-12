using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200048C RID: 1164
	[XmlType(TypeName = "SyncConversationType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class SyncConversationRequest : BaseRequest
	{
		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x000A32D0 File Offset: 0x000A14D0
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x000A32D8 File Offset: 0x000A14D8
		[DataMember(Name = "SyncState", IsRequired = false)]
		[XmlElement("SyncState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string SyncState { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000A32E1 File Offset: 0x000A14E1
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x000A32E9 File Offset: 0x000A14E9
		[DataMember(Name = "MaxChangesReturned", IsRequired = true)]
		[XmlElement("MaxChangesReturned", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public int MaxChangesReturned { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000A32F2 File Offset: 0x000A14F2
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x000A32FA File Offset: 0x000A14FA
		[XmlElement("NumberOfDays", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "NumberOfDays", EmitDefaultValue = false, IsRequired = false)]
		public int NumberOfDays { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000A3303 File Offset: 0x000A1503
		// (set) Token: 0x06002298 RID: 8856 RVA: 0x000A330B File Offset: 0x000A150B
		[DataMember(Name = "MinimumCount", EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("MinimumCount", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public int MinimumCount { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x000A3314 File Offset: 0x000A1514
		// (set) Token: 0x0600229A RID: 8858 RVA: 0x000A331C File Offset: 0x000A151C
		[DataMember(Name = "MaximumCount", EmitDefaultValue = false, IsRequired = false)]
		[XmlElement("MaximumCount", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public int MaximumCount { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000A3325 File Offset: 0x000A1525
		// (set) Token: 0x0600229C RID: 8860 RVA: 0x000A332D File Offset: 0x000A152D
		[XmlElement("FolderIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("FolderId", typeof(TargetFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(Name = "FolderIds", IsRequired = true)]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderId[] FolderIds { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x000A3336 File Offset: 0x000A1536
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x000A333E File Offset: 0x000A153E
		[DataMember(Name = "IsPartialFolderList", IsRequired = false)]
		[XmlElement("IsPartialFolderList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool IsPartialFolderList { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000A3347 File Offset: 0x000A1547
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x000A334F File Offset: 0x000A154F
		[DataMember(Name = "DoQuickSync", IsRequired = false)]
		[XmlElement("DoQuickSync", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public bool DoQuickSync { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000A3358 File Offset: 0x000A1558
		// (set) Token: 0x060022A2 RID: 8866 RVA: 0x000A3360 File Offset: 0x000A1560
		[XmlElement]
		[DataMember(Name = "ConversationShape", IsRequired = false)]
		public ConversationResponseShape ConversationShape { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x000A3369 File Offset: 0x000A1569
		// (set) Token: 0x060022A4 RID: 8868 RVA: 0x000A3371 File Offset: 0x000A1571
		[XmlIgnore]
		[DataMember(Name = "ShapeName", IsRequired = false)]
		public string ShapeName { get; set; }

		// Token: 0x060022A5 RID: 8869 RVA: 0x000A337A File Offset: 0x000A157A
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new SyncConversation(callContext, this);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x000A3383 File Offset: 0x000A1583
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.FolderIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdList(callContext, this.FolderIds);
		}

		// Token: 0x060022A7 RID: 8871 RVA: 0x000A339B File Offset: 0x000A159B
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
