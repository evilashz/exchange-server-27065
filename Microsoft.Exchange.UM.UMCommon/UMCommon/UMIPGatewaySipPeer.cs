using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000178 RID: 376
	internal class UMIPGatewaySipPeer : UMSipPeer
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0002BB88 File Offset: 0x00029D88
		public UMIPGatewaySipPeer(UMIPGateway gateway, bool useMutualTLS) : base(gateway.Address, gateway.Port, gateway.OutcallsAllowed && !gateway.Simulator, useMutualTLS, gateway.IPAddressFamily)
		{
			ValidateArgument.NotNull(gateway, "gateway");
			this.gateway = gateway;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002BBD4 File Offset: 0x00029DD4
		public UMIPGatewaySipPeer(UMIPGateway gateway, UMDialPlan dialPlan) : this(gateway, dialPlan.VoIPSecurity != UMVoIPSecurityType.Unsecured)
		{
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002BBEC File Offset: 0x00029DEC
		public override bool IsOcs
		{
			get
			{
				bool result = false;
				MultiValuedProperty<UMHuntGroup> huntGroups = this.gateway.HuntGroups;
				if (huntGroups != null && huntGroups.Count > 0)
				{
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(this.gateway.OrganizationId);
					UMDialPlan dialPlanFromId = iadsystemConfigurationLookup.GetDialPlanFromId(huntGroups[0].UMDialPlan);
					result = (dialPlanFromId.URIType == UMUriType.SipName);
				}
				return result;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0002BC42 File Offset: 0x00029E42
		public override string Name
		{
			get
			{
				return this.gateway.Name;
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002BC50 File Offset: 0x00029E50
		public override UMIPGateway ToUMIPGateway(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			ExAssert.RetailAssert(orgId.Equals(this.gateway.OrganizationId), "orgId='{0}' does not match this.gateway.OrganizationId='{1}'", new object[]
			{
				orgId,
				this.gateway.OrganizationId
			});
			return this.gateway;
		}

		// Token: 0x04000679 RID: 1657
		private UMIPGateway gateway;
	}
}
