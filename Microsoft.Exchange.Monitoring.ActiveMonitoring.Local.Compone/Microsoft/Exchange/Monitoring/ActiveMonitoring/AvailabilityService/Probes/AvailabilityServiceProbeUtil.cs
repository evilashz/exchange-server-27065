using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.AvailabilityService.Probes
{
	// Token: 0x0200001E RID: 30
	public static class AvailabilityServiceProbeUtil
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00009C34 File Offset: 0x00007E34
		static AvailabilityServiceProbeUtil()
		{
			AvailabilityServiceProbeUtil.KnownErrors[333.ToString()] = AvailabilityServiceProbeUtil.FailingComponent.XSO;
			AvailabilityServiceProbeUtil.KnownErrors["AutoDiscover Failed"] = AvailabilityServiceProbeUtil.FailingComponent.AutoD;
			AvailabilityServiceProbeUtil.KnownErrors[259.ToString()] = AvailabilityServiceProbeUtil.FailingComponent.Ignore;
			AvailabilityServiceProbeUtil.KnownErrors[261.ToString()] = AvailabilityServiceProbeUtil.FailingComponent.Ignore;
			AvailabilityServiceProbeUtil.KnownErrors[264.ToString()] = AvailabilityServiceProbeUtil.FailingComponent.Ignore;
			AvailabilityServiceProbeUtil.KnownErrors["No Active Copy Database On Mailbox"] = AvailabilityServiceProbeUtil.FailingComponent.Ignore;
			AvailabilityServiceProbeUtil.KnownErrors["Server In Maintenance"] = AvailabilityServiceProbeUtil.FailingComponent.Ignore;
			AvailabilityServiceProbeUtil.KnownErrors["Local Request Must Be ActiveCopy"] = AvailabilityServiceProbeUtil.FailingComponent.Ignore;
			AvailabilityServiceProbeUtil.KnownErrors["Server Too Busy"] = AvailabilityServiceProbeUtil.FailingComponent.EWS;
			AvailabilityServiceProbeUtil.KnownErrors[382.ToString()] = AvailabilityServiceProbeUtil.FailingComponent.EWS;
			AvailabilityServiceProbeUtil.KnownErrors[74.ToString()] = AvailabilityServiceProbeUtil.FailingComponent.EWS;
			AvailabilityServiceProbeUtil.KnownErrors["Probe Time Out"] = AvailabilityServiceProbeUtil.FailingComponent.Monitoring;
		}

		// Token: 0x040000C7 RID: 199
		public const string NoActiveCopyDatabaseOnMailbox = "No Active Copy Database On Mailbox";

		// Token: 0x040000C8 RID: 200
		public const string ServerInMaintenance = "Server In Maintenance";

		// Token: 0x040000C9 RID: 201
		public const string LocalRequestMustBeActiveCopy = "Local Request Must Be ActiveCopy";

		// Token: 0x040000CA RID: 202
		public const string ProbeTimeOut = "Probe Time Out";

		// Token: 0x040000CB RID: 203
		public const string ServerTooBusy = "Server Too Busy";

		// Token: 0x040000CC RID: 204
		public const string AutoDFailed = "AutoDiscover Failed";

		// Token: 0x040000CD RID: 205
		public static Dictionary<string, AvailabilityServiceProbeUtil.FailingComponent> KnownErrors = new Dictionary<string, AvailabilityServiceProbeUtil.FailingComponent>();

		// Token: 0x0200001F RID: 31
		public enum FailingComponent
		{
			// Token: 0x040000CF RID: 207
			Unknown = 1,
			// Token: 0x040000D0 RID: 208
			EWS,
			// Token: 0x040000D1 RID: 209
			XSO,
			// Token: 0x040000D2 RID: 210
			AutoD,
			// Token: 0x040000D3 RID: 211
			Monitoring,
			// Token: 0x040000D4 RID: 212
			AvailabilityService,
			// Token: 0x040000D5 RID: 213
			Ignore
		}
	}
}
