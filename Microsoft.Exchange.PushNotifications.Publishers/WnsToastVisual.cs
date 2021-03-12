using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000FD RID: 253
	internal class WnsToastVisual : WnsVisual
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00018F42 File Offset: 0x00017142
		public WnsToastVisual(WnsToastBinding binding, string language = null, string baseUri = null, WnsBranding? branding = null, bool? addImageQuery = null) : base(language, baseUri, branding, addImageQuery)
		{
			this.Binding = binding;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00018F57 File Offset: 0x00017157
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00018F5F File Offset: 0x0001715F
		public WnsToastBinding Binding { get; private set; }

		// Token: 0x06000832 RID: 2098 RVA: 0x00018F68 File Offset: 0x00017168
		public override string ToString()
		{
			return string.Format("{{binding:{0}; {1}}}", this.Binding.ToString(), base.ToString());
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00018F85 File Offset: 0x00017185
		internal override void WriteWnsBindings(WnsPayloadWriter wpw)
		{
			this.Binding.WriteWnsPayload(wpw);
		}
	}
}
