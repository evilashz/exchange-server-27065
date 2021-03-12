using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000015 RID: 21
	internal sealed class NspiGetTemplateInfoDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000052E8 File Offset: 0x000034E8
		public NspiGetTemplateInfoDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetTemplateInfoFlags flags, int type, string legacyDN, int codePage, int locale) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.type = type;
			this.legacyDN = legacyDN;
			this.codePage = codePage;
			this.locale = locale;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000531D File Offset: 0x0000351D
		public NspiStatus End(out int codePage, out PropertyValue[] row)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			codePage = this.returnCodePage;
			row = this.returnRow;
			return base.Status;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005341 File Offset: 0x00003541
		protected override string TaskName
		{
			get
			{
				return "NspiGetTemplateInfo";
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005348 File Offset: 0x00003548
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug((long)base.ContextHandle, "{0} params: flags={1}, type={2}, legacyDN={3}, codePage={4}, locale={5}", new object[]
			{
				this.TaskName,
				this.flags,
				this.type,
				this.legacyDN,
				this.codePage,
				this.locale
			});
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005424 File Offset: 0x00003624
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			PropRow mapiRow = null;
			base.NspiContextCallWrapper("GetTemplateInfo", () => this.Context.GetTemplateInfo(this.flags, (uint)this.type, this.legacyDN, (uint)this.codePage, (uint)this.locale, out mapiRow));
			if (base.Status == NspiStatus.Success)
			{
				this.returnCodePage = this.codePage;
				this.returnRow = ConvertHelper.ConvertFromMapiPropRow(mapiRow, this.returnCodePage);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000548D File Offset: 0x0000368D
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetTemplateInfoDispatchTask>(this);
		}

		// Token: 0x0400006F RID: 111
		private const string Name = "NspiGetTemplateInfo";

		// Token: 0x04000070 RID: 112
		private readonly NspiGetTemplateInfoFlags flags;

		// Token: 0x04000071 RID: 113
		private readonly int type;

		// Token: 0x04000072 RID: 114
		private readonly string legacyDN;

		// Token: 0x04000073 RID: 115
		private readonly int codePage;

		// Token: 0x04000074 RID: 116
		private readonly int locale;

		// Token: 0x04000075 RID: 117
		private int returnCodePage;

		// Token: 0x04000076 RID: 118
		private PropertyValue[] returnRow;
	}
}
