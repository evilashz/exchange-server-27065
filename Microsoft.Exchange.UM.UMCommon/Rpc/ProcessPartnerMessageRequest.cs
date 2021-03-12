using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.Rpc
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public class ProcessPartnerMessageRequest : UMVersionedRpcRequest
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001C5BC File Offset: 0x0001A7BC
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
			set
			{
				this.mailboxGuid = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001C5CD File Offset: 0x0001A7CD
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001C5D5 File Offset: 0x0001A7D5
		public Guid TenantGuid
		{
			get
			{
				return this.tenantGuid;
			}
			set
			{
				this.tenantGuid = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001C5DE File Offset: 0x0001A7DE
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001C5E6 File Offset: 0x0001A7E6
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001C5EF File Offset: 0x0001A7EF
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001C5F7 File Offset: 0x0001A7F7
		internal ProcessPartnerMessageDelegate ProcessPartnerMessage { get; set; }

		// Token: 0x06000731 RID: 1841 RVA: 0x0001C600 File Offset: 0x0001A800
		internal override string GetFriendlyName()
		{
			return Strings.ProcessPartnerMessageRequest;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001C60C File Offset: 0x0001A80C
		internal override UMRpcResponse Execute()
		{
			this.ProcessPartnerMessage(this);
			return null;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001C61C File Offset: 0x0001A81C
		protected override void LogErrorEvent(Exception exception)
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMPartnerMessageRpcRequestError, null, new object[]
			{
				this.GetFriendlyName(),
				CommonUtil.ToEventLogString(exception)
			});
		}

		// Token: 0x0400041C RID: 1052
		private Guid mailboxGuid;

		// Token: 0x0400041D RID: 1053
		private Guid tenantGuid;

		// Token: 0x0400041E RID: 1054
		private string itemId;
	}
}
