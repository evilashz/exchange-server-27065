using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public class UMDPCustomPromptRpcRequest : PromptPreviewRpcRequest
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x00026688 File Offset: 0x00024888
		public UMDPCustomPromptRpcRequest(UMDialPlan dp, string promptFileName)
		{
			this.DialPlan = dp;
			this.PromptFileName = promptFileName;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002669E File Offset: 0x0002489E
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x000266A6 File Offset: 0x000248A6
		public string PromptFileName { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x000266AF File Offset: 0x000248AF
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x000266B7 File Offset: 0x000248B7
		public UMDialPlan DialPlan { get; set; }

		// Token: 0x06000A0A RID: 2570 RVA: 0x000266C0 File Offset: 0x000248C0
		internal override string GetFriendlyName()
		{
			return Strings.AutoAttendantCustomPromptRequest;
		}
	}
}
