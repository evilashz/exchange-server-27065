using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B4C RID: 2892
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPersonaModernGroupMembershipRequest : BaseRequest
	{
		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060051EF RID: 20975 RVA: 0x0010B0DF File Offset: 0x001092DF
		// (set) Token: 0x060051F0 RID: 20976 RVA: 0x0010B0E7 File Offset: 0x001092E7
		[DataMember(Name = "SmtpAddress", IsRequired = true)]
		public string SmtpAddress { get; set; }

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0010B0F0 File Offset: 0x001092F0
		// (set) Token: 0x060051F2 RID: 20978 RVA: 0x0010B0F8 File Offset: 0x001092F8
		internal ProxyAddress ProxyAddress { get; private set; }

		// Token: 0x060051F3 RID: 20979 RVA: 0x0010B104 File Offset: 0x00109304
		internal void ValidateRequest()
		{
			if (string.IsNullOrEmpty(this.SmtpAddress) || !Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(this.SmtpAddress))
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid smtp address {0}", this.SmtpAddress);
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			if (this.PagingOptions == null)
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug((long)this.GetHashCode(), "Paging options are missing.");
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
			this.ProxyAddress = new SmtpProxyAddress(this.SmtpAddress, true);
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0010B18F File Offset: 0x0010938F
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0010B199 File Offset: 0x00109399
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x04002DC2 RID: 11714
		[DataMember(Name = "PagingOptions", IsRequired = true)]
		public IndexedPageView PagingOptions;
	}
}
