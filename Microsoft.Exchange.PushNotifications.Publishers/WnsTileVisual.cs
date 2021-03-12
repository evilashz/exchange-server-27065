using System;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000FA RID: 250
	internal class WnsTileVisual : WnsVisual
	{
		// Token: 0x06000814 RID: 2068 RVA: 0x00018C9F File Offset: 0x00016E9F
		public WnsTileVisual(WnsTileBinding binding, WnsTileBinding[] extraBindings = null, string language = null, string baseUri = null, WnsBranding? branding = null, bool? addImageQuery = null) : base(language, baseUri, branding, addImageQuery)
		{
			this.Binding = binding;
			this.ExtraBindings = extraBindings;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00018CBC File Offset: 0x00016EBC
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x00018CC4 File Offset: 0x00016EC4
		public WnsTileBinding Binding { get; private set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00018CCD File Offset: 0x00016ECD
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x00018CD5 File Offset: 0x00016ED5
		public WnsTileBinding[] ExtraBindings { get; private set; }

		// Token: 0x06000819 RID: 2073 RVA: 0x00018CDE File Offset: 0x00016EDE
		public override string ToString()
		{
			return string.Format("{{binding:{0}; extraBindings:{1}; {2}}}", this.Binding.ToString(), this.ExtraBindings.ToNullableString(null), base.ToString());
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00018D08 File Offset: 0x00016F08
		internal override void WriteWnsBindings(WnsPayloadWriter wpw)
		{
			this.Binding.WriteWnsPayload(wpw);
			if (this.ExtraBindings != null)
			{
				foreach (WnsTileBinding wnsTileBinding in this.ExtraBindings)
				{
					wnsTileBinding.WriteWnsPayload(wpw);
				}
			}
		}
	}
}
