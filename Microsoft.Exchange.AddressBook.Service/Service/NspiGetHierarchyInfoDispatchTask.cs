using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200000F RID: 15
	internal sealed class NspiGetHierarchyInfoDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00004B11 File Offset: 0x00002D11
		public NspiGetHierarchyInfoDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetHierarchyInfoFlags flags, NspiState state, int version) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.version = version;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004B30 File Offset: 0x00002D30
		public NspiStatus End(out int codePage, out int version, out PropertyValue[][] rowset)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			codePage = this.returnCodePage;
			version = this.returnVersion;
			rowset = this.returnRowset;
			return base.Status;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004B5C File Offset: 0x00002D5C
		protected override string TaskName
		{
			get
			{
				return "NspiGetHierarchyInfo";
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004B63 File Offset: 0x00002D63
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiGetHierarchyInfoFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004BC4 File Offset: 0x00002DC4
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			PropRowSet mapiRowset = null;
			uint localVersion = (uint)this.version;
			base.NspiContextCallWrapper("GetHierarchyInfo", () => this.Context.GetHierarchyInfo(this.flags, this.NspiState, ref localVersion, out mapiRowset));
			this.returnVersion = (int)localVersion;
			if (base.Status == NspiStatus.Success)
			{
				this.returnCodePage = base.NspiState.CodePage;
				this.returnRowset = ConvertHelper.ConvertFromMapiPropRowSet(mapiRowset, this.returnCodePage);
				NspiDispatchTask.NspiTracer.TraceDebug<int>((long)base.ContextHandle, "Rows returned: {0}", (mapiRowset == null) ? 0 : mapiRowset.Rows.Count);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004C7B File Offset: 0x00002E7B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetHierarchyInfoDispatchTask>(this);
		}

		// Token: 0x0400004D RID: 77
		private const string Name = "NspiGetHierarchyInfo";

		// Token: 0x0400004E RID: 78
		private readonly NspiGetHierarchyInfoFlags flags;

		// Token: 0x0400004F RID: 79
		private readonly int version;

		// Token: 0x04000050 RID: 80
		private int returnCodePage;

		// Token: 0x04000051 RID: 81
		private int returnVersion;

		// Token: 0x04000052 RID: 82
		private PropertyValue[][] returnRowset;
	}
}
