using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ClientAccess.Messages
{
	// Token: 0x02000068 RID: 104
	[Serializable]
	public abstract class RequestBase : UMVersionedRpcRequest
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000E0C1 File Offset: 0x0000C2C1
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000E0C9 File Offset: 0x0000C2C9
		public string ProxyAddress
		{
			get
			{
				return this.proxyAddress;
			}
			set
			{
				this.proxyAddress = value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000E0D2 File Offset: 0x0000C2D2
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000E0DA File Offset: 0x0000C2DA
		public Guid UserObjectGuid
		{
			get
			{
				return this.userObjectGuid;
			}
			set
			{
				this.userObjectGuid = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000E0E3 File Offset: 0x0000C2E3
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000E0EB File Offset: 0x0000C2EB
		public Guid TenantGuid { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		internal ProcessRequestDelegate ProcessRequest { get; set; }

		// Token: 0x060003FA RID: 1018 RVA: 0x0000E108 File Offset: 0x0000C308
		internal override UMRpcResponse Execute()
		{
			UMRpcResponse result;
			try
			{
				result = this.ProcessRequest(this);
			}
			catch (LocalizedException exception)
			{
				result = new ErrorResponse(exception);
			}
			return result;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000E140 File Offset: 0x0000C340
		protected override void LogErrorEvent(Exception exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PoPRequestError, null, new object[]
			{
				this.GetFriendlyName(),
				CommonUtil.ToEventLogString(exception)
			});
		}

		// Token: 0x040002BC RID: 700
		private string proxyAddress;

		// Token: 0x040002BD RID: 701
		private Guid userObjectGuid;
	}
}
