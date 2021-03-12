using System;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000053 RID: 83
	public abstract class DeliveryAgent : Agent
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060001EA RID: 490 RVA: 0x00006582 File Offset: 0x00004782
		// (remove) Token: 0x060001EB RID: 491 RVA: 0x00006590 File Offset: 0x00004790
		protected event OpenConnectionEventHandler OnOpenConnection
		{
			add
			{
				base.AddHandler("OnOpenConnection", value);
			}
			remove
			{
				base.RemoveHandler("OnOpenConnection");
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060001EC RID: 492 RVA: 0x0000659D File Offset: 0x0000479D
		// (remove) Token: 0x060001ED RID: 493 RVA: 0x000065AB File Offset: 0x000047AB
		protected event DeliverMailItemEventHandler OnDeliverMailItem
		{
			add
			{
				base.AddHandler("OnDeliverMailItem", value);
			}
			remove
			{
				base.RemoveHandler("OnDeliverMailItem");
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060001EE RID: 494 RVA: 0x000065B8 File Offset: 0x000047B8
		// (remove) Token: 0x060001EF RID: 495 RVA: 0x000065C6 File Offset: 0x000047C6
		protected event CloseConnectionEventHandler OnCloseConnection
		{
			add
			{
				base.AddHandler("OnCloseConnection", value);
			}
			remove
			{
				base.RemoveHandler("OnCloseConnection");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000065D3 File Offset: 0x000047D3
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x000065DB File Offset: 0x000047DB
		internal override object HostState
		{
			get
			{
				return base.HostStateInternal;
			}
			set
			{
				base.HostStateInternal = value;
				((SmtpServer)base.HostStateInternal).AssociatedAgent = this;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000065F5 File Offset: 0x000047F5
		internal override void AsyncComplete()
		{
			this.Cleanup();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006600 File Offset: 0x00004800
		internal override void Invoke(string eventTopic, object source, object e)
		{
			Delegate @delegate = (Delegate)base.Handlers[eventTopic];
			if (@delegate == null)
			{
				return;
			}
			DeliverableMailItem mailItem = DeliveryAgent.GetMailItem(e);
			if (mailItem != null)
			{
				((SmtpServer)this.HostState).AddressBook.RecipientCache = mailItem.RecipientCache;
			}
			if (eventTopic != null)
			{
				if (!(eventTopic == "OnOpenConnection"))
				{
					if (!(eventTopic == "OnDeliverMailItem"))
					{
						if (!(eventTopic == "OnCloseConnection"))
						{
							goto IL_B4;
						}
						((CloseConnectionEventHandler)@delegate)((CloseConnectionEventSource)source, (CloseConnectionEventArgs)e);
					}
					else
					{
						((DeliverMailItemEventHandler)@delegate)((DeliverMailItemEventSource)source, (DeliverMailItemEventArgs)e);
					}
				}
				else
				{
					((OpenConnectionEventHandler)@delegate)((OpenConnectionEventSource)source, (OpenConnectionEventArgs)e);
				}
				if (base.Synchronous)
				{
					this.Cleanup();
				}
				return;
			}
			IL_B4:
			throw new InvalidOperationException("Internal error: unsupported event topic: " + (eventTopic ?? "null"));
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000066EC File Offset: 0x000048EC
		private static DeliverableMailItem GetMailItem(object e)
		{
			OpenConnectionEventArgs openConnectionEventArgs = e as OpenConnectionEventArgs;
			if (openConnectionEventArgs != null)
			{
				return openConnectionEventArgs.DeliverableMailItem;
			}
			DeliverMailItemEventArgs deliverMailItemEventArgs = e as DeliverMailItemEventArgs;
			if (deliverMailItemEventArgs != null)
			{
				return deliverMailItemEventArgs.DeliverableMailItem;
			}
			return null;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000671C File Offset: 0x0000491C
		private void Cleanup()
		{
			((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
		}
	}
}
