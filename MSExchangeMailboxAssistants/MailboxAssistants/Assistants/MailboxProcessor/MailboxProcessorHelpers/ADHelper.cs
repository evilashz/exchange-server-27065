using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorHelpers
{
	// Token: 0x02000239 RID: 569
	internal static class ADHelper
	{
		// Token: 0x06001573 RID: 5491 RVA: 0x00079F18 File Offset: 0x00078118
		public static IRecipientSession GetAdRecipientSession(Guid externalDirectoryOrganizationId)
		{
			ADSessionSettings sessionSettings = (externalDirectoryOrganizationId != Guid.Empty) ? ADSessionSettings.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId) : ADSessionSettings.FromRootOrgScopeSet();
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 31, "GetAdRecipientSession", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\MailboxProcessor\\MailboxProcessorHelpers\\ADHelper.cs");
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00079F5C File Offset: 0x0007815C
		public static IRecipientSession GetAdRecipientSession(TenantPartitionHint tenantPartitionHint)
		{
			Guid externalDirectoryOrganizationId = (tenantPartitionHint != null) ? tenantPartitionHint.GetExternalDirectoryOrganizationId() : TenantPartitionHint.ExternalDirectoryOrganizationIdForRootOrg;
			return ADHelper.GetAdRecipientSession(externalDirectoryOrganizationId);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00079F80 File Offset: 0x00078180
		public static ADUser GetADUserFromMailboxGuid(Guid mailboxGuid, TenantPartitionHint partitionHint)
		{
			IRecipientSession adRecipientSession;
			try
			{
				adRecipientSession = ADHelper.GetAdRecipientSession(partitionHint);
			}
			catch (ADTransientException ex)
			{
				MailboxProcessorAssistantType.Tracer.TraceError<string>(0L, "Caught AD exception: {0}", ex.Message);
				throw;
			}
			return adRecipientSession.FindByExchangeGuidIncludingArchive(mailboxGuid) as ADUser;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00079FCC File Offset: 0x000781CC
		public static ADUser GetADUserFromMailboxGuid(Guid mailboxGuid, Guid externalDirectoryOrganizationId)
		{
			IRecipientSession adRecipientSession;
			try
			{
				adRecipientSession = ADHelper.GetAdRecipientSession(externalDirectoryOrganizationId);
			}
			catch (ADTransientException ex)
			{
				MailboxProcessorAssistantType.Tracer.TraceError<string>(0L, "Caught AD exception: {0}", ex.Message);
				throw;
			}
			return adRecipientSession.FindByExchangeGuidIncludingArchive(mailboxGuid) as ADUser;
		}
	}
}
