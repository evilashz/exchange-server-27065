using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000017 RID: 23
	internal sealed class NspiModPropsDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x000055DD File Offset: 0x000037DD
		public NspiModPropsDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiModPropsFlags flags, NspiState state, PropertyTag[] propertyTags, PropertyValue[] row) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.propertyTags = propertyTags;
			this.row = row;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005604 File Offset: 0x00003804
		public NspiStatus End()
		{
			base.CheckDisposed();
			base.CheckCompletion();
			return base.Status;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005618 File Offset: 0x00003818
		protected override string TaskName
		{
			get
			{
				return "NspiModProps";
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000561F File Offset: 0x0000381F
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiModPropsFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005674 File Offset: 0x00003874
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			PropRow mapiRow = ConvertHelper.ConvertToMapiPropRow(this.row);
			PropTag[] mapiPropTags = ConvertHelper.ConvertToMapiPropTags(this.propertyTags);
			base.NspiContextCallWrapper("ModProps", () => this.Context.ModProps(this.NspiState, mapiPropTags, mapiRow));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000056CD File Offset: 0x000038CD
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiModPropsDispatchTask>(this);
		}

		// Token: 0x0400007C RID: 124
		private const string Name = "NspiModProps";

		// Token: 0x0400007D RID: 125
		private readonly NspiModPropsFlags flags;

		// Token: 0x0400007E RID: 126
		private readonly PropertyTag[] propertyTags;

		// Token: 0x0400007F RID: 127
		private readonly PropertyValue[] row;
	}
}
