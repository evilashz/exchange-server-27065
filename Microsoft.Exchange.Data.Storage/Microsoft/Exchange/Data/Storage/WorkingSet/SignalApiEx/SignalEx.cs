using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.SignalApiEx
{
	// Token: 0x02000EE5 RID: 3813
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SignalEx : Signal
	{
		// Token: 0x06008391 RID: 33681 RVA: 0x0023BB54 File Offset: 0x00239D54
		internal SignalEx(BinaryReader reader, IUnpacker unpacker) : base(reader, unpacker)
		{
			this.disposed = false;
		}

		// Token: 0x06008392 RID: 33682 RVA: 0x0023BB65 File Offset: 0x00239D65
		public override void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06008393 RID: 33683 RVA: 0x0023BB74 File Offset: 0x00239D74
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (base.Actor != null)
					{
						ExchangeItem exchangeItem = base.Actor as ExchangeItem;
						if (exchangeItem != null && exchangeItem.Item != null)
						{
							exchangeItem.Item.Dispose();
						}
						base.Actor = null;
					}
					if (base.Action != null)
					{
						ExchangeItem exchangeItem2 = base.Action.Item as ExchangeItem;
						if (exchangeItem2 != null && exchangeItem2.Item != null)
						{
							exchangeItem2.Item.Dispose();
						}
						base.Action = null;
					}
					base.ItemToDelete = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x040057FE RID: 22526
		private bool disposed;
	}
}
