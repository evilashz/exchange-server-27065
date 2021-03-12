using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000418 RID: 1048
	[XmlType("EmptyFolderType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EmptyFolderRequest : BaseRequest
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0009FB62 File Offset: 0x0009DD62
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x0009FB6A File Offset: 0x0009DD6A
		[DataMember(Name = "FolderIds", IsRequired = true, Order = 1)]
		[XmlArray("FolderIds", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("DistinguishedFolderId", typeof(DistinguishedFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("FolderId", typeof(FolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public BaseFolderId[] FolderIds { get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0009FB73 File Offset: 0x0009DD73
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0009FB7B File Offset: 0x0009DD7B
		[XmlAttribute("DeleteType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[IgnoreDataMember]
		public DisposalType DeleteType { get; set; }

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0009FB84 File Offset: 0x0009DD84
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0009FB96 File Offset: 0x0009DD96
		[XmlIgnore]
		[DataMember(Name = "DeleteType", IsRequired = true, Order = 2)]
		public string DeleteTypeString
		{
			get
			{
				return this.DeleteType.ToString();
			}
			set
			{
				this.DeleteType = (DisposalType)Enum.Parse(typeof(DisposalType), value);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x0009FBB3 File Offset: 0x0009DDB3
		// (set) Token: 0x06001E16 RID: 7702 RVA: 0x0009FBBB File Offset: 0x0009DDBB
		[XmlAttribute("DeleteSubFolders", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "DeleteSubFolders", IsRequired = true, Order = 3)]
		public bool DeleteSubFolders { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0009FBC4 File Offset: 0x0009DDC4
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x0009FBCC File Offset: 0x0009DDCC
		[XmlIgnore]
		[DataMember(Name = "AllowSearchFolder", IsRequired = false)]
		public bool AllowSearchFolder { get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0009FBD5 File Offset: 0x0009DDD5
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x0009FBDD File Offset: 0x0009DDDD
		[DateTimeString]
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string ClientLastSyncTime { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0009FBE6 File Offset: 0x0009DDE6
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0009FBEE File Offset: 0x0009DDEE
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public bool SuppressReadReceipt { get; set; }

		// Token: 0x06001E1D RID: 7709 RVA: 0x0009FBF7 File Offset: 0x0009DDF7
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new EmptyFolder(callContext, this);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x0009FC00 File Offset: 0x0009DE00
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.FolderIds == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderIdListHierarchyOperations(callContext, this.FolderIds);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0009FC18 File Offset: 0x0009DE18
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			if (this.FolderIds == null || this.FolderIds.Length < taskStep)
			{
				return null;
			}
			return base.GetResourceKeysForFolderIdHierarchyOperations(true, callContext, this.FolderIds[taskStep]);
		}
	}
}
