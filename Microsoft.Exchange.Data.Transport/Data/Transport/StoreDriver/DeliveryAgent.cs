using System;

namespace Microsoft.Exchange.Data.Transport.StoreDriver
{
	// Token: 0x0200009F RID: 159
	internal abstract class DeliveryAgent : StoreDriverAgent
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600038D RID: 909 RVA: 0x000087C5 File Offset: 0x000069C5
		// (remove) Token: 0x0600038E RID: 910 RVA: 0x000087D3 File Offset: 0x000069D3
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

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600038F RID: 911 RVA: 0x000087E0 File Offset: 0x000069E0
		// (remove) Token: 0x06000390 RID: 912 RVA: 0x000087EE File Offset: 0x000069EE
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

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000391 RID: 913 RVA: 0x000087FB File Offset: 0x000069FB
		// (remove) Token: 0x06000392 RID: 914 RVA: 0x00008809 File Offset: 0x00006A09
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

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000393 RID: 915 RVA: 0x00008816 File Offset: 0x00006A16
		// (remove) Token: 0x06000394 RID: 916 RVA: 0x00008824 File Offset: 0x00006A24
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

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000395 RID: 917 RVA: 0x00008831 File Offset: 0x00006A31
		// (remove) Token: 0x06000396 RID: 918 RVA: 0x0000883F File Offset: 0x00006A3F
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

		// Token: 0x06000397 RID: 919 RVA: 0x0000884C File Offset: 0x00006A4C
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
