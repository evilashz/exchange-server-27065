using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200008F RID: 143
	[Cmdlet("Get", "MessageCategory", DefaultParameterSetName = "Identity")]
	public sealed class GetMessageCategory : GetTenantADObjectWithIdentityTaskBase<MessageCategoryIdParameter, MessageCategory>
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00029306 File Offset: 0x00027506
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x0002931D File Offset: 0x0002751D
		[Parameter(Mandatory = false)]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00029330 File Offset: 0x00027530
		protected override void WriteResult(IConfigurable dataObject)
		{
			base.WriteResult(dataObject);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0002933C File Offset: 0x0002753C
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession tenantGlobalCatalogSession = base.TenantGlobalCatalogSession;
			MailboxIdParameter mailboxIdParameter = null;
			if (this.Identity != null)
			{
				if (this.Identity.InternalMessageCategoryId != null)
				{
					mailboxIdParameter = new MailboxIdParameter(this.Identity.InternalMessageCategoryId.MailboxOwnerId);
				}
				else
				{
					mailboxIdParameter = this.Identity.RawMailbox;
				}
			}
			if (mailboxIdParameter != null && this.Mailbox != null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorConflictingMailboxes), ErrorCategory.InvalidArgument, this.Identity);
			}
			if (mailboxIdParameter == null)
			{
				ADObjectId executingUserId;
				base.TryGetExecutingUserId(out executingUserId);
				mailboxIdParameter = (this.Mailbox ?? MailboxTaskHelper.ResolveMailboxIdentity(executingUserId, new Task.ErrorLoggerDelegate(base.WriteError)));
			}
			ADUser aduser = (ADUser)base.GetDataObject<ADUser>(mailboxIdParameter, tenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxNotFound(mailboxIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(mailboxIdParameter.ToString())));
			if (this.Identity != null && this.Identity.InternalMessageCategoryId == null)
			{
				this.Identity.InternalMessageCategoryId = new MessageCategoryId(aduser.Id, this.Identity.RawCategoryName, this.Identity.RawCategoryId);
			}
			base.VerifyIsWithinScopes(tenantGlobalCatalogSession, aduser, true, new DataAccessTask<MessageCategory>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
			Exception ex = null;
			ExchangeErrorCategory category = (ExchangeErrorCategory)0;
			MessageCategoryDataProvider result = null;
			try
			{
				result = new MessageCategoryDataProvider(base.SessionSettings, aduser, "Get-MessageCategory");
			}
			catch (MapiPermanentException ex2)
			{
				ex = ex2;
				category = ExchangeErrorCategory.ServerOperation;
			}
			catch (MapiRetryableException ex3)
			{
				ex = ex3;
				category = ExchangeErrorCategory.ServerTransient;
			}
			if (ex != null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorUnableToAccessMessageCategoriesGenericMessage(ex.Message), ex), (ErrorCategory)category, aduser.Identity);
			}
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000294F0 File Offset: 0x000276F0
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00029503 File Offset: 0x00027703
		protected override void InternalStateReset()
		{
			this.CleanupDataProvider();
			base.InternalStateReset();
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00029511 File Offset: 0x00027711
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.CleanupDataProvider();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00029528 File Offset: 0x00027728
		private void CleanupDataProvider()
		{
			IDisposable disposable = base.DataSession as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}
}
