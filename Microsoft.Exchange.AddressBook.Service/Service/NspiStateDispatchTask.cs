using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200000A RID: 10
	internal abstract class NspiStateDispatchTask : NspiDispatchTask
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000044CF File Offset: 0x000026CF
		public NspiStateDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiState state) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.state = state;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000044E4 File Offset: 0x000026E4
		protected NspiState NspiState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000044EC File Offset: 0x000026EC
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			if (this.state == null)
			{
				throw new NspiException(NspiStatus.InvalidParameter, "State cannot be null");
			}
			this.TraceNspiState();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004514 File Offset: 0x00002714
		protected void TraceNspiState()
		{
			if (NspiDispatchTask.NspiTracer.IsTraceEnabled(TraceType.DebugTrace) && this.state != null)
			{
				NspiDispatchTask.NspiTracer.TraceDebug<int, int, int>((long)base.ContextHandle, "SortType {0}, ContainerId {1} (0x{1:X}), CurrentRecord {2} (0x{2:X})", this.state.SortType, this.state.ContainerId, this.state.CurrentRecord);
				NspiDispatchTask.NspiTracer.TraceDebug<int, int, int>((long)base.ContextHandle, "  TotalRecords {0} (0x{0:X}), Position {1} (0x{1:X}), Delta {2} (0x{2:X})", this.state.TotalRecords, this.state.Position, this.state.Delta);
				NspiDispatchTask.NspiTracer.TraceDebug<int, int, int>((long)base.ContextHandle, "  CodePage 0x{0:X4}, SortLocale 0x{1:X4}, TemplateLocale 0x{2:X4}", this.state.CodePage, this.state.SortLocale, this.state.TemplateLocale);
			}
		}

		// Token: 0x0400003A RID: 58
		private readonly NspiState state;
	}
}
