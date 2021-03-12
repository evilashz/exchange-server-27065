using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003BB RID: 955
	[DataContract(Name = "FindMembersInUnifiedGroup", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindMembersInUnifiedGroupRequest : BaseRequest
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x00076A58 File Offset: 0x00074C58
		// (set) Token: 0x06001EA5 RID: 7845 RVA: 0x00076A60 File Offset: 0x00074C60
		[DataMember(Name = "SmtpAddress", IsRequired = true)]
		public string SmtpAddress { get; set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x00076A69 File Offset: 0x00074C69
		// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x00076A71 File Offset: 0x00074C71
		[DataMember(Name = "Filter", IsRequired = false)]
		public string Filter { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00076A7A File Offset: 0x00074C7A
		internal ProxyAddress ProxyAddress
		{
			get
			{
				return new SmtpProxyAddress(this.SmtpAddress, true);
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00076A88 File Offset: 0x00074C88
		internal override void Validate()
		{
			if (string.IsNullOrEmpty(this.SmtpAddress))
			{
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (this.Filter == null)
			{
				this.Filter = string.Empty;
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00076AB6 File Offset: 0x00074CB6
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00076AC0 File Offset: 0x00074CC0
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
