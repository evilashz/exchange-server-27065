using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000018 RID: 24
	internal sealed class NspiQueryColumnsDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000056D5 File Offset: 0x000038D5
		public NspiQueryColumnsDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiQueryColumnsFlags flags, NspiQueryColumnsMapiFlags mapiFlags) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.mapiFlags = mapiFlags;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000056F2 File Offset: 0x000038F2
		public NspiStatus End(out PropertyTag[] columns)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			columns = this.returnColumns;
			return base.Status;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000570E File Offset: 0x0000390E
		protected override string TaskName
		{
			get
			{
				return "NspiQueryColumns";
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005715 File Offset: 0x00003915
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiQueryColumnsFlags, NspiQueryColumnsMapiFlags>((long)base.ContextHandle, "{0} params: flags={1}, mapiFlags={2}", this.TaskName, this.flags, this.mapiFlags);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000576C File Offset: 0x0000396C
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			IList<PropTag> mapiPropTagList = null;
			base.NspiContextCallWrapper("QueryColumns", () => this.Context.QueryColumns(this.mapiFlags, out mapiPropTagList));
			if (base.Status == NspiStatus.Success && mapiPropTagList != null)
			{
				this.returnColumns = ConvertHelper.ConvertFromMapiPropTags(mapiPropTagList.ToArray<PropTag>());
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000057D0 File Offset: 0x000039D0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiQueryColumnsDispatchTask>(this);
		}

		// Token: 0x04000080 RID: 128
		private const string Name = "NspiQueryColumns";

		// Token: 0x04000081 RID: 129
		private readonly NspiQueryColumnsFlags flags;

		// Token: 0x04000082 RID: 130
		private readonly NspiQueryColumnsMapiFlags mapiFlags;

		// Token: 0x04000083 RID: 131
		private PropertyTag[] returnColumns;
	}
}
