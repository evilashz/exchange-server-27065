using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000113 RID: 275
	public sealed class VariantConfigurationMexAgentsComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C9B RID: 3227 RVA: 0x0001E28C File Offset: 0x0001C48C
		internal VariantConfigurationMexAgentsComponent() : base("MexAgents")
		{
			base.Add(new VariantConfigurationSection("MexAgents.settings.ini", "TrustedMailAgents_CrossPremisesHeadersPreserved", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MexAgents.settings.ini", "TrustedMailAgents_AcceptAnyRecipientOnPremises", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MexAgents.settings.ini", "TrustedMailAgents_StampOriginatorOrgForMsitConnector", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MexAgents.settings.ini", "TrustedMailAgents_HandleCrossPremisesProbe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MexAgents.settings.ini", "TrustedMailAgents_CheckOutboundDeliveryTypeSmtpConnector", typeof(IFeature), false));
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0001E344 File Offset: 0x0001C544
		public VariantConfigurationSection TrustedMailAgents_CrossPremisesHeadersPreserved
		{
			get
			{
				return base["TrustedMailAgents_CrossPremisesHeadersPreserved"];
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0001E351 File Offset: 0x0001C551
		public VariantConfigurationSection TrustedMailAgents_AcceptAnyRecipientOnPremises
		{
			get
			{
				return base["TrustedMailAgents_AcceptAnyRecipientOnPremises"];
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0001E35E File Offset: 0x0001C55E
		public VariantConfigurationSection TrustedMailAgents_StampOriginatorOrgForMsitConnector
		{
			get
			{
				return base["TrustedMailAgents_StampOriginatorOrgForMsitConnector"];
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0001E36B File Offset: 0x0001C56B
		public VariantConfigurationSection TrustedMailAgents_HandleCrossPremisesProbe
		{
			get
			{
				return base["TrustedMailAgents_HandleCrossPremisesProbe"];
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0001E378 File Offset: 0x0001C578
		public VariantConfigurationSection TrustedMailAgents_CheckOutboundDeliveryTypeSmtpConnector
		{
			get
			{
				return base["TrustedMailAgents_CheckOutboundDeliveryTypeSmtpConnector"];
			}
		}
	}
}
