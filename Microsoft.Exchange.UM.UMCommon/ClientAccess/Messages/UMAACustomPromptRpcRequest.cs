using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x0200013B RID: 315
	[Serializable]
	public class UMAACustomPromptRpcRequest : UMAutoAttendantPromptRpcRequest
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x0002665B File Offset: 0x0002485B
		public UMAACustomPromptRpcRequest(UMAutoAttendant aa, string promptFileName) : base(aa)
		{
			this.PromptFileName = promptFileName;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002666B File Offset: 0x0002486B
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00026673 File Offset: 0x00024873
		public string PromptFileName { get; private set; }

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002667C File Offset: 0x0002487C
		internal override string GetFriendlyName()
		{
			return Strings.AutoAttendantCustomPromptRequest;
		}
	}
}
