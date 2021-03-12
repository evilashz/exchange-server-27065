using System;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003EC RID: 1004
	internal sealed class RmsLatencyTracker : IRmsLatencyTracker
	{
		// Token: 0x06002DB4 RID: 11700 RVA: 0x000B7C70 File Offset: 0x000B5E70
		public RmsLatencyTracker(LatencyTracker latencyTracker)
		{
			this.latencyTracker = latencyTracker;
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000B7C80 File Offset: 0x000B5E80
		public void BeginTrackRmsLatency(RmsOperationType operation)
		{
			LatencyComponent latencyComponent = RmsLatencyTracker.GetLatencyComponent(operation);
			if (latencyComponent != LatencyComponent.None)
			{
				LatencyTracker.BeginTrackLatency(latencyComponent, this.latencyTracker);
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000B7CA4 File Offset: 0x000B5EA4
		public void EndTrackRmsLatency(RmsOperationType operation)
		{
			LatencyComponent latencyComponent = RmsLatencyTracker.GetLatencyComponent(operation);
			if (latencyComponent != LatencyComponent.None)
			{
				LatencyTracker.EndTrackLatency(latencyComponent, this.latencyTracker);
			}
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000B7CC8 File Offset: 0x000B5EC8
		public void EndAndBeginTrackRmsLatency(RmsOperationType endOperation, RmsOperationType beginOperation)
		{
			LatencyComponent latencyComponent = RmsLatencyTracker.GetLatencyComponent(endOperation);
			LatencyComponent latencyComponent2 = RmsLatencyTracker.GetLatencyComponent(beginOperation);
			if (latencyComponent2 != LatencyComponent.None && latencyComponent != LatencyComponent.None)
			{
				LatencyTracker.EndAndBeginTrackLatency(latencyComponent, latencyComponent2, this.latencyTracker);
				return;
			}
			if (latencyComponent != LatencyComponent.None)
			{
				LatencyTracker.EndTrackLatency(latencyComponent, this.latencyTracker);
				return;
			}
			if (latencyComponent2 != LatencyComponent.None)
			{
				LatencyTracker.BeginTrackLatency(latencyComponent2, this.latencyTracker);
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000B7D18 File Offset: 0x000B5F18
		private static LatencyComponent GetLatencyComponent(RmsOperationType operation)
		{
			switch (operation)
			{
			case RmsOperationType.AcquireLicense:
				return LatencyComponent.RmsAcquireLicense;
			case RmsOperationType.AcquireTemplates:
				return LatencyComponent.RmsAcquireTemplates;
			case RmsOperationType.AcquireTemplateInfo:
				return LatencyComponent.RmsAcquireTemplateInfo;
			case RmsOperationType.AcquireServerBoxRac:
				return LatencyComponent.RmsAcquireServerBoxRac;
			case RmsOperationType.AcquireClc:
				return LatencyComponent.RmsAcquireClc;
			case RmsOperationType.AcquirePrelicense:
				return LatencyComponent.RmsAcquirePrelicense;
			case RmsOperationType.FindServiceLocations:
				return LatencyComponent.RmsFindServiceLocation;
			case RmsOperationType.AcquireCertificationMexData:
				return LatencyComponent.RmsAcquireCertificationMexData;
			case RmsOperationType.AcquireServerLicensingMexData:
				return LatencyComponent.RmsAcquireServerLicensingMexData;
			case RmsOperationType.AcquireB2BRac:
				return LatencyComponent.RmsAcquireB2BRac;
			case RmsOperationType.AcquireB2BLicense:
				return LatencyComponent.RmsAcquireB2BLicense;
			case RmsOperationType.RequestDelegationToken:
				return LatencyComponent.RmsRequestDelegationToken;
			default:
				return LatencyComponent.None;
			}
		}

		// Token: 0x040016CD RID: 5837
		private LatencyTracker latencyTracker;
	}
}
