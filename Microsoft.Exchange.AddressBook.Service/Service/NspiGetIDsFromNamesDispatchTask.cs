using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000010 RID: 16
	internal sealed class NspiGetIDsFromNamesDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004C83 File Offset: 0x00002E83
		public NspiGetIDsFromNamesDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetIDsFromNamesFlags flags, int mapiFlags, int nameCount, IntPtr names) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.mapiFlags = mapiFlags;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public NspiStatus End(out PropertyTag[] propertyTags)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			propertyTags = null;
			return base.Status;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004CB7 File Offset: 0x00002EB7
		protected override string TaskName
		{
			get
			{
				return "NspiGetIDsFromNames";
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004CBE File Offset: 0x00002EBE
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiGetIDsFromNamesFlags, int>((long)base.ContextHandle, "{0} params: flags={1}, mapiFlags={2}", this.TaskName, this.flags, this.mapiFlags);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004CEF File Offset: 0x00002EEF
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			base.NspiContextCallWrapper("GetIDsFromNames", () => NspiStatus.NotSupported);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004D1F File Offset: 0x00002F1F
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetIDsFromNamesDispatchTask>(this);
		}

		// Token: 0x04000053 RID: 83
		private const string Name = "NspiGetIDsFromNames";

		// Token: 0x04000054 RID: 84
		private readonly NspiGetIDsFromNamesFlags flags;

		// Token: 0x04000055 RID: 85
		private readonly int mapiFlags;
	}
}
