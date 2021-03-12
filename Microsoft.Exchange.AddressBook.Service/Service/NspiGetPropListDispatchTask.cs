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
	// Token: 0x02000013 RID: 19
	internal sealed class NspiGetPropListDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00005051 File Offset: 0x00003251
		public NspiGetPropListDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetPropListFlags flags, int mid, int codePage) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.mid = mid;
			this.codePage = codePage;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005076 File Offset: 0x00003276
		public NspiStatus End(out PropertyTag[] propertyTags)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			propertyTags = this.returnPropertyTags;
			return base.Status;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005092 File Offset: 0x00003292
		protected override string TaskName
		{
			get
			{
				return "NspiGetPropList";
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000509C File Offset: 0x0000329C
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug((long)base.ContextHandle, "{0} params: flags={1}, mid={2}, codePage={3}", new object[]
			{
				this.TaskName,
				this.flags,
				this.mid,
				this.codePage
			});
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000513C File Offset: 0x0000333C
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			IList<PropTag> mapiPropTagList = null;
			base.NspiContextCallWrapper("GetPropList", () => this.Context.GetPropList(this.flags, this.mid, (uint)this.codePage, out mapiPropTagList));
			if (base.Status == NspiStatus.Success && mapiPropTagList != null)
			{
				this.returnPropertyTags = ConvertHelper.ConvertFromMapiPropTags(mapiPropTagList.ToArray<PropTag>());
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000051A0 File Offset: 0x000033A0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetPropListDispatchTask>(this);
		}

		// Token: 0x04000065 RID: 101
		private const string Name = "NspiGetPropList";

		// Token: 0x04000066 RID: 102
		private readonly NspiGetPropListFlags flags;

		// Token: 0x04000067 RID: 103
		private readonly int mid;

		// Token: 0x04000068 RID: 104
		private readonly int codePage;

		// Token: 0x04000069 RID: 105
		private PropertyTag[] returnPropertyTags;
	}
}
