using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000067 RID: 103
	[Serializable]
	public class DisableUMMailboxRequest : UMRpcRequest
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000E07F File Offset: 0x0000C27F
		public DisableUMMailboxRequest()
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E087 File Offset: 0x0000C287
		internal DisableUMMailboxRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000E090 File Offset: 0x0000C290
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000E098 File Offset: 0x0000C298
		public bool KeepProperties
		{
			get
			{
				return this.keepProperties;
			}
			set
			{
				this.keepProperties = value;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000E0A1 File Offset: 0x0000C2A1
		internal override UMRpcResponse Execute()
		{
			Utils.ResetUMMailbox(base.User, this.keepProperties);
			return null;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000E0B5 File Offset: 0x0000C2B5
		internal override string GetFriendlyName()
		{
			return Strings.DisableUMMailboxRequest;
		}

		// Token: 0x040002BB RID: 699
		private bool keepProperties;
	}
}
