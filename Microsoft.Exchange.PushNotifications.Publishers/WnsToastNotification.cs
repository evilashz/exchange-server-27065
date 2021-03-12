using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000FC RID: 252
	internal class WnsToastNotification : WnsXmlNotification
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x00018DCE File Offset: 0x00016FCE
		public WnsToastNotification(string appId, OrganizationId tenantId, string deviceUri, WnsToastVisual visual, WnsToastDuration? duration = null, string launch = null, WnsAudio audio = null) : base(appId, tenantId, deviceUri)
		{
			this.Visual = visual;
			this.Duration = duration;
			this.Launch = launch;
			this.Audio = audio;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00018DF9 File Offset: 0x00016FF9
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x00018E01 File Offset: 0x00017001
		public WnsToastVisual Visual { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00018E0A File Offset: 0x0001700A
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x00018E12 File Offset: 0x00017012
		public WnsToastDuration? Duration { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00018E1B File Offset: 0x0001701B
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x00018E23 File Offset: 0x00017023
		public string Launch { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00018E2C File Offset: 0x0001702C
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x00018E34 File Offset: 0x00017034
		public WnsAudio Audio { get; private set; }

		// Token: 0x0600082B RID: 2091 RVA: 0x00018E3D File Offset: 0x0001703D
		protected override void RunValidationCheck(List<LocalizedString> errors)
		{
			base.RunValidationCheck(errors);
			base.ValidateTemplate(this.Visual.Binding, errors);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00018E58 File Offset: 0x00017058
		protected override void WriteWnsXmlPayload(WnsPayloadWriter wpw)
		{
			ArgumentValidator.ThrowIfNull("wpw", wpw);
			wpw.WriteElementStart("toast", true);
			wpw.WriteAttribute<WnsToastDuration>("duration", this.Duration, true);
			wpw.WriteAttribute("launch", this.Launch, true);
			wpw.WriteAttributesEnd();
			this.Visual.WriteWnsPayload(wpw);
			if (this.Audio != null)
			{
				this.Audio.WriteWnsPayload(wpw);
			}
			wpw.WriteElementEnd();
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00018ECC File Offset: 0x000170CC
		protected override void PrepareWnsRequest(WnsRequest wnsRequest)
		{
			base.PrepareWnsRequest(wnsRequest);
			wnsRequest.WnsType = "wns/toast";
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00018EE0 File Offset: 0x000170E0
		protected override string InternalToFullString()
		{
			return string.Format("{0}; visual:{1}; duration:{2}; launch:{3}; audio:{4}", new object[]
			{
				base.InternalToFullString(),
				this.Visual.ToNullableString(null),
				this.Duration.ToNullableString<WnsToastDuration>(),
				this.Launch.ToNullableString(),
				this.Audio.ToNullableString(null)
			});
		}
	}
}
