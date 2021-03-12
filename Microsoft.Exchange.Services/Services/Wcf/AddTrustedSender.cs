using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200090B RID: 2315
	internal class AddTrustedSender : ServiceCommand<bool>
	{
		// Token: 0x0600431D RID: 17181 RVA: 0x000E014C File Offset: 0x000DE34C
		public AddTrustedSender(CallContext callContext, ItemId itemId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(itemId, "itemId", "AddTrustedSender::AddTrustedSender");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.itemId = itemId;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000E017D File Offset: 0x000DE37D
		protected override bool InternalExecute()
		{
			return this.InternalAddTrustedSender(this.itemId);
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000E018C File Offset: 0x000DE38C
		private bool InternalAddTrustedSender(ItemId itemId)
		{
			JunkEmailRule junkEmailRule = this.session.JunkEmailRule;
			bool flag = false;
			bool result;
			using (Item item = Item.Bind(this.session, IdConverter.EwsIdToMessageStoreObjectId(itemId.Id)))
			{
				string value = (string)item[MessageItemSchema.SenderSmtpAddress];
				JunkEmailCollection.ValidationProblem validationProblem = JunkEmailCollection.ValidationProblem.NoError;
				try
				{
					validationProblem = junkEmailRule.TrustedSenderEmailCollection.TryAdd(value);
				}
				catch (JunkEmailValidationException ex)
				{
					validationProblem = ex.Problem;
				}
				finally
				{
					switch (validationProblem)
					{
					case JunkEmailCollection.ValidationProblem.NoError:
					case JunkEmailCollection.ValidationProblem.Duplicate:
						junkEmailRule.Save();
						flag = true;
						break;
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x0400271C RID: 10012
		private readonly MailboxSession session;

		// Token: 0x0400271D RID: 10013
		private ItemId itemId;
	}
}
