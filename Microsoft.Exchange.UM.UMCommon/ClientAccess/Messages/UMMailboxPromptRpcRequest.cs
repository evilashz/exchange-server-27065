using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200013F RID: 319
	[Serializable]
	public class UMMailboxPromptRpcRequest : PromptPreviewRpcRequest
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x00026823 File Offset: 0x00024A23
		public static UMMailboxPromptRpcRequest CreateAwayPromptRequest(ADUser userId, string displayName)
		{
			return new UMMailboxPromptRpcRequest(userId, false, true, displayName);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002682E File Offset: 0x00024A2E
		public static UMMailboxPromptRpcRequest CreateVoicemailPromptRequest(ADUser userId, string displayName)
		{
			return new UMMailboxPromptRpcRequest(userId, false, false, displayName);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00026839 File Offset: 0x00024A39
		public static UMMailboxPromptRpcRequest CreateCustomAwayPromptRequest(ADUser userId)
		{
			return new UMMailboxPromptRpcRequest(userId, true, true);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00026843 File Offset: 0x00024A43
		public static UMMailboxPromptRpcRequest CreateCustomVoicemailPromptRequest(ADUser userId)
		{
			return new UMMailboxPromptRpcRequest(userId, true, false);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002684D File Offset: 0x00024A4D
		private UMMailboxPromptRpcRequest(ADUser userId, bool customFlag, bool awayFlag, string displayName)
		{
			this.User = userId;
			this.CustomFlag = customFlag;
			this.AwayFlag = awayFlag;
			this.DisplayName = displayName;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00026872 File Offset: 0x00024A72
		private UMMailboxPromptRpcRequest(ADUser userId, bool customFlag, bool awayFlag)
		{
			this.User = userId;
			this.CustomFlag = customFlag;
			this.AwayFlag = awayFlag;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002688F File Offset: 0x00024A8F
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00026897 File Offset: 0x00024A97
		public bool CustomFlag { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x000268A0 File Offset: 0x00024AA0
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x000268A8 File Offset: 0x00024AA8
		public bool AwayFlag { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x000268B1 File Offset: 0x00024AB1
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x000268B9 File Offset: 0x00024AB9
		public string DisplayName { get; private set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000268C2 File Offset: 0x00024AC2
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x000268CA File Offset: 0x00024ACA
		public ADUser User { get; private set; }

		// Token: 0x06000A28 RID: 2600 RVA: 0x000268D3 File Offset: 0x00024AD3
		internal override string GetFriendlyName()
		{
			return Strings.UMMailboxPromptRequest;
		}
	}
}
