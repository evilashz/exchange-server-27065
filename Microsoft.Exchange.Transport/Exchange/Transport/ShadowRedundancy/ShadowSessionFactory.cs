using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000394 RID: 916
	internal class ShadowSessionFactory
	{
		// Token: 0x06002867 RID: 10343 RVA: 0x0009DC57 File Offset: 0x0009BE57
		public ShadowSessionFactory(ShadowRedundancyManager shadowRedundancyManager)
		{
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.configurationSource = shadowRedundancyManager.Configuration;
			this.transportConfigPicker = new TransportConfigBasedHubPicker(this.configurationSource);
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0009DC84 File Offset: 0x0009BE84
		public virtual IShadowSession GetShadowSession(ISmtpInSession inSession, bool isBdat)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSessionFactory.GetShadowSession");
			if (inSession == null)
			{
				throw new ArgumentNullException("inSession");
			}
			if (!this.configurationSource.Enabled || this.configurationSource.CompatibilityVersion != ShadowRedundancyCompatibilityVersion.E15 || Components.Configuration.ProcessTransportRole != ProcessTransportRole.Hub)
			{
				return ShadowSessionFactory.nullSessionInstance;
			}
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<bool>((long)this.GetHashCode(), "ShadowSessionFactory.GetShadowSession returning new ShadowSession isBdat={0}", isBdat);
			if (isBdat)
			{
				return this.GetBdatSession(inSession);
			}
			return this.GetDataSession(inSession);
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0009DD0B File Offset: 0x0009BF0B
		private ShadowHubPickerBase GetShadowPicker()
		{
			if (this.transportConfigPicker.Enabled)
			{
				return this.transportConfigPicker;
			}
			return new RoutingBasedHubPicker(this.configurationSource, Components.RoutingComponent.MailRouter);
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0009DD36 File Offset: 0x0009BF36
		private ShadowBdatSession GetBdatSession(ISmtpInSession inSession)
		{
			return new ShadowBdatSession(inSession, this.shadowRedundancyManager, this.GetShadowPicker(), inSession.SmtpInServer.SmtpOutConnectionHandler);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0009DD55 File Offset: 0x0009BF55
		private ShadowDataSession GetDataSession(ISmtpInSession inSession)
		{
			return new ShadowDataSession(inSession, this.shadowRedundancyManager, this.GetShadowPicker(), inSession.SmtpInServer.SmtpOutConnectionHandler);
		}

		// Token: 0x04001469 RID: 5225
		private static readonly NullShadowSession nullSessionInstance = new NullShadowSession();

		// Token: 0x0400146A RID: 5226
		private readonly IShadowRedundancyConfigurationSource configurationSource;

		// Token: 0x0400146B RID: 5227
		private readonly TransportConfigBasedHubPicker transportConfigPicker;

		// Token: 0x0400146C RID: 5228
		private readonly ShadowRedundancyManager shadowRedundancyManager;
	}
}
