using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200044E RID: 1102
	[XmlType("GetServiceConfigurationRequest", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetServiceConfigurationRequest : BaseRequest
	{
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x000A1A52 File Offset: 0x0009FC52
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x000A1A5A File Offset: 0x0009FC5A
		[XmlElement("ActingAs")]
		public EmailAddressWrapper ActingAs
		{
			get
			{
				return this.actingAs;
			}
			set
			{
				this.actingAs = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x000A1A63 File Offset: 0x0009FC63
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x000A1A6B File Offset: 0x0009FC6B
		[XmlAnyElement("ConfigurationRequestDetails")]
		public XmlElement ConfigurationRequestDetails { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002058 RID: 8280 RVA: 0x000A1A74 File Offset: 0x0009FC74
		// (set) Token: 0x06002059 RID: 8281 RVA: 0x000A1A7C File Offset: 0x0009FC7C
		[XmlArrayItem("ConfigurationName", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArray("RequestedConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public string[] ConfigurationTypes
		{
			get
			{
				return this.configurationTypes;
			}
			set
			{
				this.configurationTypes = value;
			}
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000A1A85 File Offset: 0x0009FC85
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetServiceConfiguration(callContext, this);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000A1A9B File Offset: 0x0009FC9B
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.configurationTypes == null)
			{
				return null;
			}
			if (Array.Exists<string>(this.configurationTypes, (string compare) => compare == "UnifiedMessagingConfiguration"))
			{
				return callContext.GetServerInfoForEffectiveCaller();
			}
			return null;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000A1AD9 File Offset: 0x0009FCD9
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x0400143E RID: 5182
		internal const string ActingAsElementName = "ActingAs";

		// Token: 0x0400143F RID: 5183
		internal const string RequestedConfigurationElementName = "RequestedConfiguration";

		// Token: 0x04001440 RID: 5184
		internal const string ConfigurationTypeElementName = "ConfigurationName";

		// Token: 0x04001441 RID: 5185
		internal const string ConfigurationRequestDetailsElementName = "ConfigurationRequestDetails";

		// Token: 0x04001442 RID: 5186
		private EmailAddressWrapper actingAs;

		// Token: 0x04001443 RID: 5187
		private string[] configurationTypes;
	}
}
