using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D6 RID: 214
	internal class WnsAudio
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00015DF8 File Offset: 0x00013FF8
		public WnsAudio(WnsSound? sound = null, bool? loop = null, bool? silent = null)
		{
			this.Sound = sound;
			this.Loop = loop;
			this.Silent = silent;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00015E15 File Offset: 0x00014015
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00015E1D File Offset: 0x0001401D
		public WnsSound? Sound { get; private set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00015E26 File Offset: 0x00014026
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00015E2E File Offset: 0x0001402E
		public bool? Loop { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00015E37 File Offset: 0x00014037
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00015E3F File Offset: 0x0001403F
		public bool? Silent { get; private set; }

		// Token: 0x060006FA RID: 1786 RVA: 0x00015E48 File Offset: 0x00014048
		public override string ToString()
		{
			return string.Format("{{src:{0}; loop:{1}; silent:{2}}}", this.Sound.ToNullableString<WnsSound>(), this.Loop.ToNullableString<bool>(), this.Silent.ToNullableString<bool>());
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00015E78 File Offset: 0x00014078
		internal void WriteWnsPayload(WnsPayloadWriter wpw)
		{
			ArgumentValidator.ThrowIfNull("wpw", wpw);
			wpw.WriteElementStart("audio", false);
			wpw.WriteSoundAttribute("src", this.Sound, true);
			wpw.WriteAttribute<bool>("loop", this.Loop, true);
			wpw.WriteAttribute<bool>("silent", this.Silent, true);
			wpw.WriteElementEnd();
		}
	}
}
