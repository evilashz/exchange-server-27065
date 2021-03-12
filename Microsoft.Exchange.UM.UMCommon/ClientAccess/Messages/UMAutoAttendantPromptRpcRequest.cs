using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000138 RID: 312
	[Serializable]
	public class UMAutoAttendantPromptRpcRequest : PromptPreviewRpcRequest
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x00026605 File Offset: 0x00024805
		public UMAutoAttendantPromptRpcRequest(UMAutoAttendant aa)
		{
			this.AutoAttendant = aa;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00026614 File Offset: 0x00024814
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0002661C File Offset: 0x0002481C
		public UMAutoAttendant AutoAttendant { get; private set; }

		// Token: 0x060009FC RID: 2556 RVA: 0x00026625 File Offset: 0x00024825
		internal override string GetFriendlyName()
		{
			return Strings.AutoAttendantPromptRequest;
		}
	}
}
