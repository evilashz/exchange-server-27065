using System;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x0200001C RID: 28
	internal class DeliveryPoisonHandler : PoisonHandler<DeliveryPoisonContext>
	{
		// Token: 0x060001AA RID: 426 RVA: 0x0000970F File Offset: 0x0000790F
		public DeliveryPoisonHandler(TimeSpan poisonEntryExpiryWindow, int maxPoisonEntries) : base("Delivery", poisonEntryExpiryWindow, maxPoisonEntries)
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00009720 File Offset: 0x00007920
		public override void SavePoisonContext()
		{
			try
			{
				base.SavePoisonContext();
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_PoisonMessageSaveFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009764 File Offset: 0x00007964
		public override void MarkPoisonMessageHandled(string poisonId)
		{
			try
			{
				base.MarkPoisonMessageHandled(poisonId);
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_PoisonMessageMarkFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000097AC File Offset: 0x000079AC
		public override void Load()
		{
			try
			{
				base.Load();
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_PoisonMessageLoadFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new TransportComponentLoadFailedException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00009800 File Offset: 0x00007A00
		protected override bool IsMessagePoison(CrashProperties crashProperties)
		{
			return !this.IsExpired(crashProperties) && base.IsMessagePoison(crashProperties);
		}

		// Token: 0x040000B4 RID: 180
		private const string DeliveryRegistrySuffix = "Delivery";
	}
}
