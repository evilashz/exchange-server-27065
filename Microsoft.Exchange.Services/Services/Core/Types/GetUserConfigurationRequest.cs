using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000459 RID: 1113
	[XmlType("GetUserConfigurationRequestType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUserConfigurationRequest : BaseRequest
	{
		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000A1E19 File Offset: 0x000A0019
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x000A1E21 File Offset: 0x000A0021
		[DataMember(IsRequired = true)]
		[XmlElement("UserConfigurationName")]
		public UserConfigurationNameType UserConfigurationName { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000A1E2A File Offset: 0x000A002A
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x000A1E32 File Offset: 0x000A0032
		[IgnoreDataMember]
		[XmlElement]
		public UserConfigurationProperties UserConfigurationProperties { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x000A1E3B File Offset: 0x000A003B
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x000A1E48 File Offset: 0x000A0048
		[XmlIgnore]
		[DataMember(Name = "UserConfigurationProperties", IsRequired = true)]
		public string UserConfigurationPropertiesString
		{
			get
			{
				return EnumUtilities.ToString<UserConfigurationProperties>(this.UserConfigurationProperties);
			}
			set
			{
				this.UserConfigurationProperties = EnumUtilities.Parse<UserConfigurationProperties>(value);
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000A1E56 File Offset: 0x000A0056
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetUserConfiguration(callContext, this);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000A1E5F File Offset: 0x000A005F
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.UserConfigurationName == null || this.UserConfigurationName.BaseFolderId == null)
			{
				return null;
			}
			return BaseRequest.GetServerInfoForFolderId(callContext, this.UserConfigurationName.BaseFolderId);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000A1E89 File Offset: 0x000A0089
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
