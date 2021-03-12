using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000012 RID: 18
	internal sealed class NspiGetNamesFromIDsDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004F71 File Offset: 0x00003171
		public NspiGetNamesFromIDsDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetNamesFromIDsFlags flags, Guid? guid, PropertyTag[] propertyTags) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.guid = guid;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004F8E File Offset: 0x0000318E
		public NspiStatus End(out PropertyTag[] propertyTags, out SafeRpcMemoryHandle namesHandle)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			propertyTags = null;
			namesHandle = null;
			return base.Status;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004FA8 File Offset: 0x000031A8
		protected override string TaskName
		{
			get
			{
				return "NspiGetNamesFromIDs";
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004FB0 File Offset: 0x000031B0
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiGetNamesFromIDsFlags, string>((long)base.ContextHandle, "{0} params: flags={1}, guid={2}", this.TaskName, this.flags, (this.guid != null) ? this.guid.Value.ToString() : "<null>");
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00005019 File Offset: 0x00003219
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			base.NspiContextCallWrapper("GetNamesFromIDs", () => NspiStatus.NotSupported);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005049 File Offset: 0x00003249
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetNamesFromIDsDispatchTask>(this);
		}

		// Token: 0x04000061 RID: 97
		private const string Name = "NspiGetNamesFromIDs";

		// Token: 0x04000062 RID: 98
		private readonly NspiGetNamesFromIDsFlags flags;

		// Token: 0x04000063 RID: 99
		private readonly Guid? guid;
	}
}
