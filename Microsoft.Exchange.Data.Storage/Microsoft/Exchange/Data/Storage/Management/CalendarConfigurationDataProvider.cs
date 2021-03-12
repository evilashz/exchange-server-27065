using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A5C RID: 2652
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CalendarConfigurationDataProvider : XsoDictionaryDataProvider
	{
		// Token: 0x060060D6 RID: 24790 RVA: 0x001988B8 File Offset: 0x00196AB8
		public CalendarConfigurationDataProvider(ExchangePrincipal mailboxOwner, string action) : base(mailboxOwner, action, new Func<MailboxSession, string, UserConfigurationTypes, bool, IUserConfiguration>(UserConfigurationHelper.GetCalendarConfiguration), new Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration>(UserConfigurationHelper.GetReadOnlyCalendarConfiguration))
		{
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x001988DA File Offset: 0x00196ADA
		public CalendarConfigurationDataProvider(MailboxSession session) : base(session, new Func<MailboxSession, string, UserConfigurationTypes, bool, IUserConfiguration>(UserConfigurationHelper.GetCalendarConfiguration), new Func<MailboxSession, string, UserConfigurationTypes, bool, IReadableUserConfiguration>(UserConfigurationHelper.GetReadOnlyCalendarConfiguration))
		{
		}

		// Token: 0x060060D8 RID: 24792 RVA: 0x001988FB File Offset: 0x00196AFB
		internal CalendarConfigurationDataProvider()
		{
		}

		// Token: 0x060060D9 RID: 24793 RVA: 0x00198903 File Offset: 0x00196B03
		public static bool IsCalendarConfigurationClass(string objectClass)
		{
			return "IPM.Configuration.Calendar".Equals(objectClass, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x00198914 File Offset: 0x00196B14
		protected override void InternalDelete(ConfigurableObject instance)
		{
			StoreId defaultFolderId = base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			if (defaultFolderId != null)
			{
				for (int i = 2; i > 0; i--)
				{
					OperationResult operationResult = base.MailboxSession.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
					{
						"Calendar"
					});
					if (operationResult == OperationResult.Succeeded)
					{
						return;
					}
				}
			}
		}

		// Token: 0x060060DB RID: 24795 RVA: 0x00198964 File Offset: 0x00196B64
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<CalendarConfigurationDataProvider>(this);
		}

		// Token: 0x04003707 RID: 14087
		internal const string CalendarConfigurationName = "Calendar";

		// Token: 0x04003708 RID: 14088
		private const string CalendarConfigurationClass = "IPM.Configuration.Calendar";
	}
}
