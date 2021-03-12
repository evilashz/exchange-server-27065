using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000446 RID: 1094
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetPersonaType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetPersonaRequest : BaseRequest
	{
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x000A1893 File Offset: 0x0009FA93
		// (set) Token: 0x06002019 RID: 8217 RVA: 0x000A189B File Offset: 0x0009FA9B
		[DataMember(Name = "PersonaId", IsRequired = false)]
		[XmlElement("PersonaId")]
		public ItemId PersonaId
		{
			get
			{
				return this.personaId;
			}
			set
			{
				this.personaId = value;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x000A18A4 File Offset: 0x0009FAA4
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x000A18AC File Offset: 0x0009FAAC
		[XmlElement("EmailAddress")]
		[DataMember(Name = "EmailAddress", IsRequired = false)]
		public EmailAddressWrapper EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000A18B5 File Offset: 0x0009FAB5
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x000A18BD File Offset: 0x0009FABD
		[XmlElement("ParentFolderId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "ParentFolderId", IsRequired = false, EmitDefaultValue = false)]
		public TargetFolderId ParentFolderId { get; set; }

		// Token: 0x0600201E RID: 8222 RVA: 0x000A18C6 File Offset: 0x0009FAC6
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetPersona(callContext, this);
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000A18CF File Offset: 0x0009FACF
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000A18D2 File Offset: 0x0009FAD2
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x04001429 RID: 5161
		private ItemId personaId;

		// Token: 0x0400142A RID: 5162
		private EmailAddressWrapper emailAddress;
	}
}
