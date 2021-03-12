using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000401 RID: 1025
	[XmlType(TypeName = "ConvertIdType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[KnownType(typeof(AlternatePublicFolderId))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(AlternatePublicFolderItemId))]
	[KnownType(typeof(AlternateId))]
	[Serializable]
	public class ConvertIdRequest : BaseRequest
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x0009EECF File Offset: 0x0009D0CF
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0009EED7 File Offset: 0x0009D0D7
		[IgnoreDataMember]
		[XmlAttribute]
		public IdFormat DestinationFormat
		{
			get
			{
				return this.destinationFormat;
			}
			set
			{
				this.destinationFormat = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x0009EEE0 File Offset: 0x0009D0E0
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x0009EEF2 File Offset: 0x0009D0F2
		[DataMember(Name = "DestinationFormat", IsRequired = true, Order = 1)]
		[XmlIgnore]
		public string DestinationFormatString
		{
			get
			{
				return this.destinationFormat.ToString();
			}
			set
			{
				this.destinationFormat = (IdFormat)Enum.Parse(typeof(IdFormat), value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x0009EF0F File Offset: 0x0009D10F
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x0009EF17 File Offset: 0x0009D117
		[XmlArrayItem("AlternateId", typeof(AlternateId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("AlternatePublicFolderId", typeof(AlternatePublicFolderId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("AlternatePublicFolderItemId", typeof(AlternatePublicFolderItemId), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[DataMember(IsRequired = true, Order = 2)]
		public AlternateIdBase[] SourceIds
		{
			get
			{
				return this.sourceIds;
			}
			set
			{
				this.sourceIds = value;
			}
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x0009EF20 File Offset: 0x0009D120
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new ConvertId(callContext, this);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x0009EF29 File Offset: 0x0009D129
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0009EF2C File Offset: 0x0009D12C
		internal override ResourceKey[] GetResources(CallContext callContext, int currentStep)
		{
			return null;
		}

		// Token: 0x04001305 RID: 4869
		private IdFormat destinationFormat;

		// Token: 0x04001306 RID: 4870
		private AlternateIdBase[] sourceIds;
	}
}
