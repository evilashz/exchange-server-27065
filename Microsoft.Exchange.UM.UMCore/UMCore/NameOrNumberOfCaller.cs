using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200018E RID: 398
	internal class NameOrNumberOfCaller
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00032FBC File Offset: 0x000311BC
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x00032FC4 File Offset: 0x000311C4
		internal PhoneNumber CallerId { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00032FCD File Offset: 0x000311CD
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x00032FD5 File Offset: 0x000311D5
		internal string CallerName { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00032FDE File Offset: 0x000311DE
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00032FE6 File Offset: 0x000311E6
		internal object EmailSender { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x00032FEF File Offset: 0x000311EF
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x00032FF7 File Offset: 0x000311F7
		internal NameOrNumberOfCaller.TypeOfVoiceCall TypeOfCall { get; private set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00033000 File Offset: 0x00031200
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x00033008 File Offset: 0x00031208
		internal ExDateTime MessageReceivedTime { get; set; }

		// Token: 0x06000BCB RID: 3019 RVA: 0x00033011 File Offset: 0x00031211
		internal NameOrNumberOfCaller(NameOrNumberOfCaller.TypeOfVoiceCall typeOfCall)
		{
			this.TypeOfCall = typeOfCall;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00033020 File Offset: 0x00031220
		internal void ClearProperties()
		{
			this.CallerId = null;
			this.CallerName = null;
			this.MessageReceivedTime = ExDateTime.MinValue;
			this.EmailSender = null;
		}

		// Token: 0x0200018F RID: 399
		internal enum TypeOfVoiceCall
		{
			// Token: 0x040009E7 RID: 2535
			MissedCall,
			// Token: 0x040009E8 RID: 2536
			VoicemailCall
		}
	}
}
