using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000039 RID: 57
	internal class PushNotificationPublisherSettings : PushNotificationSettingsBase
	{
		// Token: 0x0600022C RID: 556 RVA: 0x000085AC File Offset: 0x000067AC
		public PushNotificationPublisherSettings(string appId, bool enabled, Version minimumVersion, Version maximumVersion, int queueSize, int numberOfChannels, int addTimeout) : base(appId)
		{
			this.Enabled = enabled;
			this.MinimumVersion = minimumVersion;
			this.MaximumVersion = maximumVersion;
			this.QueueSize = queueSize;
			this.NumberOfChannels = numberOfChannels;
			this.AddTimeout = addTimeout;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000085E3 File Offset: 0x000067E3
		// (set) Token: 0x0600022E RID: 558 RVA: 0x000085EB File Offset: 0x000067EB
		public bool Enabled { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000085F4 File Offset: 0x000067F4
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000085FC File Offset: 0x000067FC
		public Version MinimumVersion { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00008605 File Offset: 0x00006805
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000860D File Offset: 0x0000680D
		public Version MaximumVersion { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00008616 File Offset: 0x00006816
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000861E File Offset: 0x0000681E
		public int QueueSize { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008627 File Offset: 0x00006827
		// (set) Token: 0x06000236 RID: 566 RVA: 0x0000862F File Offset: 0x0000682F
		public int NumberOfChannels { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00008638 File Offset: 0x00006838
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00008640 File Offset: 0x00006840
		public int AddTimeout { get; private set; }

		// Token: 0x06000239 RID: 569 RVA: 0x00008649 File Offset: 0x00006849
		public override string ToString()
		{
			return base.AppId;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008654 File Offset: 0x00006854
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			if (this.MinimumVersion != null && this.MaximumVersion != null && this.MaximumVersion.CompareTo(this.MinimumVersion) < 0)
			{
				errors.Add(Strings.ValidationErrorRangeVersion(this.MinimumVersion, this.MaximumVersion));
			}
			if (this.QueueSize <= 0)
			{
				errors.Add(Strings.ValidationErrorPositiveInteger("QueueSize", this.QueueSize));
			}
			if (this.NumberOfChannels <= 0)
			{
				errors.Add(Strings.ValidationErrorPositiveInteger("NumberOfChannels", this.NumberOfChannels));
			}
			if (this.AddTimeout < 0)
			{
				errors.Add(Strings.ValidationErrorNonNegativeInteger("AddTimeout", this.AddTimeout));
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000870C File Offset: 0x0000690C
		protected override bool RunSuitabilityCheck()
		{
			bool result = base.RunSuitabilityCheck();
			if (!this.Enabled)
			{
				string name = base.GetType().Name;
				PushNotificationsCrimsonEvents.DisabledApp.Log<string, string>(base.AppId, name);
				ExTraceGlobals.PublisherManagerTracer.TraceWarning<string, string>((long)this.GetHashCode(), "App '{0}' is currently marked as disabled by '{1}'.", base.AppId, name);
				result = false;
			}
			Version executingVersion = ExchangeSetupContext.GetExecutingVersion();
			if (!this.ValidateVersionSupport(executingVersion))
			{
				string text = string.Format("({0} - {1})", this.MinimumVersion, this.MaximumVersion);
				PushNotificationsCrimsonEvents.UnsupportedVersion.Log<string, string>(base.AppId, text);
				ExTraceGlobals.PublisherManagerTracer.TraceWarning<string, string, Version>((long)this.GetHashCode(), "App '{0}' is not enabled because its version range {1} doesn't support current executing version {2}.", base.AppId, text, executingVersion);
				result = false;
			}
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000087BC File Offset: 0x000069BC
		private bool ValidateVersionSupport(Version version)
		{
			return (this.MinimumVersion == null || version.CompareTo(this.MinimumVersion) >= 0) && (this.MaximumVersion == null || version.CompareTo(this.MaximumVersion) <= 0);
		}
	}
}
