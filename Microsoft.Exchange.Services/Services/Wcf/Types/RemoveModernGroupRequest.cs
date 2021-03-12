using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x020009FD RID: 2557
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveModernGroupRequest : BaseRequest
	{
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06004843 RID: 18499 RVA: 0x0010159B File Offset: 0x000FF79B
		// (set) Token: 0x06004844 RID: 18500 RVA: 0x001015A3 File Offset: 0x000FF7A3
		[DataMember(Name = "SmtpAddress", IsRequired = false)]
		public string SmtpAddress { get; set; }

		// Token: 0x06004845 RID: 18501 RVA: 0x001015AC File Offset: 0x000FF7AC
		internal void ValidateRequest()
		{
			if (string.IsNullOrEmpty(this.SmtpAddress) || !Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(this.SmtpAddress))
			{
				ExTraceGlobals.ModernGroupsTracer.TraceDebug<string>((long)this.GetHashCode(), "Invalid smtp address {0}", this.SmtpAddress);
				throw FaultExceptionUtilities.CreateFault(new InvalidRequestException(), FaultParty.Sender);
			}
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x001015FB File Offset: 0x000FF7FB
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00101605 File Offset: 0x000FF805
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}
	}
}
