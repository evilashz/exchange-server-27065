using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200033F RID: 831
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ActivityMessageAdapter : DisposableObject, IMessage, IDisposable
	{
		// Token: 0x060024D9 RID: 9433 RVA: 0x00094C88 File Offset: 0x00092E88
		public ActivityMessageAdapter(Action<MemoryPropertyBag> saveAction)
		{
			Util.ThrowOnNullArgument(saveAction, "saveAction");
			this.saveAction = saveAction;
			this.propertyBag = new MemoryPropertyBag();
			this.propertyBagAdapter = new ActivityPropertyBagAdapter(this.propertyBag);
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00094CBE File Offset: 0x00092EBE
		public ActivityMessageAdapter(MemoryPropertyBag propertyBag)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			this.propertyBag = propertyBag;
			this.propertyBagAdapter = new ActivityPropertyBagAdapter(this.propertyBag);
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x00094CE9 File Offset: 0x00092EE9
		public IPropertyBag PropertyBag
		{
			get
			{
				return this.propertyBagAdapter;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00094CF1 File Offset: 0x00092EF1
		public bool IsAssociated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x00094D94 File Offset: 0x00092F94
		public IEnumerable<IRecipient> GetRecipients()
		{
			yield break;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00094DB1 File Offset: 0x00092FB1
		public IRecipient CreateRecipient()
		{
			throw new NotSupportedException("Activities are not supposed to have any recipients.");
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00094DBD File Offset: 0x00092FBD
		public void RemoveRecipient(int rowId)
		{
			throw new NotSupportedException("Cannot remove recipient. Activities are not supposed to have any recipients.");
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00094E6C File Offset: 0x0009306C
		public IEnumerable<IAttachmentHandle> GetAttachments()
		{
			yield break;
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00094E89 File Offset: 0x00093089
		public IAttachment CreateAttachment()
		{
			throw new NotSupportedException("Activities are not supposed to have any attachments.");
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00094E95 File Offset: 0x00093095
		public void Save()
		{
			if (this.saveAction != null)
			{
				this.saveAction(this.propertyBag);
			}
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00094EB0 File Offset: 0x000930B0
		public void SetLongTermId(StoreLongTermId longTermId)
		{
			throw new NotSupportedException("Activities are not supposed to have any long term ID.");
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00094EBC File Offset: 0x000930BC
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ActivityMessageAdapter>(this);
		}

		// Token: 0x04001659 RID: 5721
		private readonly MemoryPropertyBag propertyBag;

		// Token: 0x0400165A RID: 5722
		private readonly ActivityPropertyBagAdapter propertyBagAdapter;

		// Token: 0x0400165B RID: 5723
		private readonly Action<MemoryPropertyBag> saveAction;
	}
}
