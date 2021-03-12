using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x02000155 RID: 341
	[Serializable]
	public class SavePINRequest : UpdateUMMailboxRequest
	{
		// Token: 0x06000AFE RID: 2814 RVA: 0x00029425 File Offset: 0x00027625
		public SavePINRequest()
		{
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0002942D File Offset: 0x0002762D
		internal SavePINRequest(ADUser user) : base(user)
		{
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00029436 File Offset: 0x00027636
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x0002943E File Offset: 0x0002763E
		public string PIN
		{
			get
			{
				return this.pin;
			}
			set
			{
				this.pin = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x00029447 File Offset: 0x00027647
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x0002944F File Offset: 0x0002764F
		public bool Expired
		{
			get
			{
				return this.expired;
			}
			set
			{
				this.expired = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00029458 File Offset: 0x00027658
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00029460 File Offset: 0x00027660
		public bool LockedOut
		{
			get
			{
				return this.lockedOut;
			}
			set
			{
				this.lockedOut = value;
			}
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00029469 File Offset: 0x00027669
		internal override UMRpcResponse Execute()
		{
			Utils.SetUserPassword(base.User, this.pin, this.expired, this.lockedOut);
			return null;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00029489 File Offset: 0x00027689
		internal override string GetFriendlyName()
		{
			return Strings.SavePINRequest;
		}

		// Token: 0x040005DD RID: 1501
		private string pin;

		// Token: 0x040005DE RID: 1502
		private bool expired;

		// Token: 0x040005DF RID: 1503
		private bool lockedOut;
	}
}
