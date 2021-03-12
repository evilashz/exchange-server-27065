using System;
using Microsoft.Exchange.Data.Transport.StoreDriver;

namespace Microsoft.Exchange.Data.Transport.StoreDriverDelivery
{
	// Token: 0x020000A8 RID: 168
	internal abstract class StoreDriverDeliveryAgent : Agent
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00008AED File Offset: 0x00006CED
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00008AF5 File Offset: 0x00006CF5
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

		// Token: 0x060003B8 RID: 952 RVA: 0x00008B0F File Offset: 0x00006D0F
		internal override void AsyncComplete()
		{
			((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060003B9 RID: 953 RVA: 0x00008B27 File Offset: 0x00006D27
		// (remove) Token: 0x060003BA RID: 954 RVA: 0x00008B35 File Offset: 0x00006D35
		protected event InitializedMessageEventHandler OnInitializedMessage
		{
			add
			{
				base.AddHandler("OnInitializedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnInitializedMessage");
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060003BB RID: 955 RVA: 0x00008B42 File Offset: 0x00006D42
		// (remove) Token: 0x060003BC RID: 956 RVA: 0x00008B50 File Offset: 0x00006D50
		protected event PromotedMessageEventHandler OnPromotedMessage
		{
			add
			{
				base.AddHandler("OnPromotedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnPromotedMessage");
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060003BD RID: 957 RVA: 0x00008B5D File Offset: 0x00006D5D
		// (remove) Token: 0x060003BE RID: 958 RVA: 0x00008B6B File Offset: 0x00006D6B
		protected event CreatedMessageEventHandler OnCreatedMessage
		{
			add
			{
				base.AddHandler("OnCreatedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnCreatedMessage");
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060003BF RID: 959 RVA: 0x00008B78 File Offset: 0x00006D78
		// (remove) Token: 0x060003C0 RID: 960 RVA: 0x00008B86 File Offset: 0x00006D86
		protected event DeliveredMessageEventHandler OnDeliveredMessage
		{
			add
			{
				base.AddHandler("OnDeliveredMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnDeliveredMessage");
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060003C1 RID: 961 RVA: 0x00008B93 File Offset: 0x00006D93
		// (remove) Token: 0x060003C2 RID: 962 RVA: 0x00008BA1 File Offset: 0x00006DA1
		protected event CompletedMessageEventHandler OnCompletedMessage
		{
			add
			{
				base.AddHandler("OnCompletedMessage", value);
			}
			remove
			{
				base.RemoveHandler("OnCompletedMessage");
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00008BB0 File Offset: 0x00006DB0
		internal override void Invoke(string eventTopic, object source, object e)
		{
			Delegate @delegate = (Delegate)base.Handlers[eventTopic];
			if (@delegate == null)
			{
				return;
			}
			StoreDriverEventSource source2 = (StoreDriverEventSource)source;
			StoreDriverDeliveryEventArgs storeDriverDeliveryEventArgs = (StoreDriverDeliveryEventArgs)e;
			((SmtpServer)this.HostState).AddressBook.RecipientCache = storeDriverDeliveryEventArgs.MailItem.RecipientCache;
			if (eventTopic != null)
			{
				if (!(eventTopic == "OnInitializedMessage"))
				{
					if (!(eventTopic == "OnPromotedMessage"))
					{
						if (!(eventTopic == "OnCreatedMessage"))
						{
							if (!(eventTopic == "OnDeliveredMessage"))
							{
								if (!(eventTopic == "OnCompletedMessage"))
								{
									goto IL_DA;
								}
								((CompletedMessageEventHandler)@delegate)(source2, storeDriverDeliveryEventArgs);
							}
							else
							{
								((DeliveredMessageEventHandler)@delegate)(source2, storeDriverDeliveryEventArgs);
							}
						}
						else
						{
							((CreatedMessageEventHandler)@delegate)(source2, storeDriverDeliveryEventArgs);
						}
					}
					else
					{
						((PromotedMessageEventHandler)@delegate)(source2, storeDriverDeliveryEventArgs);
					}
				}
				else
				{
					((InitializedMessageEventHandler)@delegate)(source2, storeDriverDeliveryEventArgs);
				}
				if (base.Synchronous)
				{
					((SmtpServer)this.HostState).AddressBook.RecipientCache = null;
				}
				return;
			}
			IL_DA:
			throw new InvalidOperationException("Internal error: unsupported event topic: " + (eventTopic ?? "null"));
		}
	}
}
