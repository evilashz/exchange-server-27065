using System;
using Microsoft.Exchange.Rpc.MailboxSearch;

namespace Microsoft.Exchange.InfoWorker.Common.SearchService
{
	// Token: 0x02000249 RID: 585
	internal class MailboxSearchClient : MailboxSearchRpcClient
	{
		// Token: 0x060010D2 RID: 4306 RVA: 0x0004CEC7 File Offset: 0x0004B0C7
		internal MailboxSearchClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0004CED0 File Offset: 0x0004B0D0
		internal int LastErrorCode
		{
			get
			{
				return this.lastErrorCode;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0004CED8 File Offset: 0x0004B0D8
		internal void Start(SearchId searchId, Guid ownerGuid)
		{
			SearchErrorInfo searchErrorInfo = null;
			base.Start(searchId, ownerGuid, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0004CF18 File Offset: 0x0004B118
		internal void StartEx(SearchId searchId, string ownerId)
		{
			SearchErrorInfo searchErrorInfo = null;
			base.StartEx(searchId, ownerId, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0004CF58 File Offset: 0x0004B158
		internal SearchStatus GetStatus(SearchId searchId)
		{
			SearchErrorInfo searchErrorInfo = null;
			SearchStatus status = base.GetStatus(searchId, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
			if (searchErrorInfo.ErrorCode == 262658)
			{
				return null;
			}
			return status;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		internal void Abort(SearchId searchId, Guid userGuid)
		{
			SearchErrorInfo searchErrorInfo = null;
			base.Abort(searchId, userGuid, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0004CFE8 File Offset: 0x0004B1E8
		internal void AbortEx(SearchId searchId, string userId)
		{
			SearchErrorInfo searchErrorInfo = null;
			base.AbortEx(searchId, userId, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0004D028 File Offset: 0x0004B228
		internal void Remove(SearchId searchId, bool removeLogs)
		{
			SearchErrorInfo searchErrorInfo = null;
			base.Remove(searchId, removeLogs, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0004D068 File Offset: 0x0004B268
		internal void UpdateStatus(SearchId searchId)
		{
			SearchErrorInfo searchErrorInfo = null;
			base.UpdateStatus(searchId, out searchErrorInfo);
			this.lastErrorCode = searchErrorInfo.ErrorCode;
			if (searchErrorInfo.Failed)
			{
				throw new SearchServerException(searchErrorInfo.ErrorCode, searchErrorInfo.Message);
			}
		}

		// Token: 0x04000B5C RID: 2908
		private int lastErrorCode;
	}
}
