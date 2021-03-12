using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200007E RID: 126
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MessageChangeAdaptor : BaseObject, IMessageChange, IDisposable
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00022684 File Offset: 0x00020884
		internal MessageChangeAdaptor(IPropertyBag messageHeaderPropertyBag, MessageAdaptor message)
		{
			this.messageHeaderPropertyBag = messageHeaderPropertyBag;
			this.message = message;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0002269A File Offset: 0x0002089A
		public IMessage Message
		{
			get
			{
				base.CheckDisposed();
				return this.message;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000226A8 File Offset: 0x000208A8
		public int MessageSize
		{
			get
			{
				base.CheckDisposed();
				return this.messageHeaderPropertyBag.GetAnnotatedProperty(PropertyTag.MessageSize).PropertyValue.GetServerValue<int>();
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x000226D8 File Offset: 0x000208D8
		public bool IsAssociatedMessage
		{
			get
			{
				base.CheckDisposed();
				return this.messageHeaderPropertyBag.GetAnnotatedProperty(PropertyTag.Associated).PropertyValue.GetServerValue<bool>();
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00022708 File Offset: 0x00020908
		public IPropertyBag MessageHeaderPropertyBag
		{
			get
			{
				base.CheckDisposed();
				return this.messageHeaderPropertyBag;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00022716 File Offset: 0x00020916
		public IMessageChangePartial PartialChange
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0002271F File Offset: 0x0002091F
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.message);
			base.InternalDispose();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00022732 File Offset: 0x00020932
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageChangeAdaptor>(this);
		}

		// Token: 0x040001DD RID: 477
		private readonly IPropertyBag messageHeaderPropertyBag;

		// Token: 0x040001DE RID: 478
		private readonly IMessage message;
	}
}
