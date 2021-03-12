using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public abstract class MessageStore : MapiObject
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00005931 File Offset: 0x00003B31
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MessageStore>(this);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005939 File Offset: 0x00003B39
		internal override MapiProp RawMapiEntry
		{
			get
			{
				throw new NotImplementedException("The property RawMapiEntry is not implemented.");
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005945 File Offset: 0x00003B45
		internal override void Read(bool keepUnmanagedResources)
		{
			throw new NotImplementedException("The method Read is not implemented.");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005951 File Offset: 0x00003B51
		internal override void Save(bool keepUnmanagedResources)
		{
			throw new NotImplementedException("The method Save is not implemented.");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000595D File Offset: 0x00003B5D
		public MessageStore()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00005965 File Offset: 0x00003B65
		internal MessageStore(MessageStoreId mapiObjectId, MapiSession mapiSession) : base(mapiObjectId, mapiSession)
		{
		}
	}
}
