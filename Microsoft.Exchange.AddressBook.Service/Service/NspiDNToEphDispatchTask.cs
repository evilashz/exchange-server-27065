using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200000E RID: 14
	internal sealed class NspiDNToEphDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000049FF File Offset: 0x00002BFF
		public NspiDNToEphDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiDNToEphFlags flags, string[] legacyDNs) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.legacyDNs = legacyDNs;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004A1C File Offset: 0x00002C1C
		public NspiStatus End(out int[] mids)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			mids = this.returnMids;
			return base.Status;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004A38 File Offset: 0x00002C38
		protected override string TaskName
		{
			get
			{
				return "NspiDNToEph";
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004A3F File Offset: 0x00002C3F
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiDNToEphFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004A90 File Offset: 0x00002C90
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			if (this.legacyDNs != null && this.legacyDNs.Length > 100000)
			{
				throw new NspiException(NspiStatus.TooBig, "Too many legacyDN values");
			}
			int[] localMids = null;
			base.NspiContextCallWrapper("DNToEph", () => this.Context.DNToEph(this.legacyDNs, out localMids));
			if (base.Status == NspiStatus.Success)
			{
				this.returnMids = localMids;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004B09 File Offset: 0x00002D09
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiDNToEphDispatchTask>(this);
		}

		// Token: 0x04000048 RID: 72
		private const string Name = "NspiDNToEph";

		// Token: 0x04000049 RID: 73
		private const int MaxLegacyDNs = 100000;

		// Token: 0x0400004A RID: 74
		private readonly NspiDNToEphFlags flags;

		// Token: 0x0400004B RID: 75
		private readonly string[] legacyDNs;

		// Token: 0x0400004C RID: 76
		private int[] returnMids;
	}
}
