using System;
using System.Collections;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Reminders;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Reminders
{
	// Token: 0x02000259 RID: 601
	internal class ReminderTimeCalculatorContextFactory : IReminderTimeCalculatorContextFactory
	{
		// Token: 0x06001662 RID: 5730 RVA: 0x0007E04C File Offset: 0x0007C24C
		public ReminderTimeCalculatorContextFactory(IMailboxSession session)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			this.session = session;
			this.hours = this.LoadWorkingHours();
			this.firstDayOfWeek = this.LoadFirstDayOfWeek();
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0007E07E File Offset: 0x0007C27E
		public ReminderTimeCalculatorContext Create()
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "ReminderTimeCalculatorContextFactory.Create()");
			return new ReminderTimeCalculatorContext(this.hours, this.firstDayOfWeek, this.session.ExTimeZone);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0007E0B0 File Offset: 0x0007C2B0
		internal static ExTimeZone LoadRegionalConfiguration(ADUser adUser, MailboxSession session)
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "ReminderTimeCalculatorContext.LoadRegionalConfiguration");
			MailboxStoreTypeProvider mailboxStoreTypeProvider = new MailboxStoreTypeProvider(adUser);
			mailboxStoreTypeProvider.MailboxSession = session;
			MailboxRegionalConfiguration mailboxRegionalConfiguration = null;
			try
			{
				mailboxRegionalConfiguration = (MailboxRegionalConfiguration)mailboxStoreTypeProvider.Read<MailboxRegionalConfiguration>(session.MailboxOwner.ObjectId);
			}
			catch (FormatException arg)
			{
				ExTraceGlobals.GeneralTracer.TraceError<IExchangePrincipal, FormatException>(0L, "User '{0}' doesn't have a valid regional configuration - {1}", session.MailboxOwner, arg);
			}
			if (mailboxRegionalConfiguration != null && mailboxRegionalConfiguration.TimeZone != null)
			{
				ExTraceGlobals.HeuristicsTracer.TraceDebug<string, IExchangePrincipal>(0L, "Found time zone {0} for User '{0}'", mailboxRegionalConfiguration.TimeZone.ExTimeZone.ToString(), session.MailboxOwner);
				return mailboxRegionalConfiguration.TimeZone.ExTimeZone;
			}
			ExTraceGlobals.HeuristicsTracer.TraceDebug<IExchangePrincipal>(0L, "User '{0}' doesn't have a valid regional configuration - using UTC as default time zone", session.MailboxOwner);
			return ExTimeZone.UtcTimeZone;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0007E184 File Offset: 0x0007C384
		private System.DayOfWeek LoadFirstDayOfWeek()
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "ReminderTimeCalculatorContext.LoadFirstDayOfWeek");
			using (IReadableUserConfiguration readOnlyMailboxConfiguration = UserConfigurationHelper.GetReadOnlyMailboxConfiguration(this.session as MailboxSession, "OWA.UserOptions", UserConfigurationTypes.Dictionary, false))
			{
				if (readOnlyMailboxConfiguration != null)
				{
					IDictionary dictionary = readOnlyMailboxConfiguration.GetDictionary();
					object obj = dictionary[MailboxCalendarConfigurationSchema.WeekStartDay.Name];
					if (obj != null)
					{
						ExTraceGlobals.HeuristicsTracer.TraceDebug(0L, "Found first day of week: {0}", new object[]
						{
							obj
						});
						return (System.DayOfWeek)obj;
					}
					ExTraceGlobals.HeuristicsTracer.TraceDebug(0L, "WeekStartDay is null on config object");
				}
			}
			ExTraceGlobals.HeuristicsTracer.TraceDebug(0L, "Unable to determine first day of week");
			return System.DayOfWeek.Sunday;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0007E244 File Offset: 0x0007C444
		private StorageWorkingHours LoadWorkingHours()
		{
			ExTraceGlobals.HeuristicsTracer.TraceFunction(0L, "ReminderTimeCalculatorContext.LoadWorkingHours");
			StorageWorkingHours result = null;
			try
			{
				result = StorageWorkingHours.LoadFrom(this.session as MailboxSession, this.session.GetDefaultFolderId(DefaultFolderType.Calendar));
			}
			catch (AccessDeniedException ex)
			{
				ExTraceGlobals.HeuristicsTracer.TraceError<string>(0L, "AccessDenied in a call of loading working hours : {0}", ex.Message);
			}
			catch (ArgumentNullException ex2)
			{
				ExTraceGlobals.HeuristicsTracer.TraceError<string>(0L, "Argument exception in a call of loading working hours : {0}", ex2.Message);
			}
			catch (ObjectNotFoundException ex3)
			{
				ExTraceGlobals.HeuristicsTracer.TraceError<string>(0L, "ObjectNotFoundException exception in a call of loading working hours : {0}", ex3.Message);
			}
			catch (WorkingHoursXmlMalformedException ex4)
			{
				ExTraceGlobals.HeuristicsTracer.TraceError<string>(0L, "WorkingHoursXmlMalformedException exception in a call of loading working hours : {0}", ex4.Message);
			}
			catch (CorruptDataException ex5)
			{
				ExTraceGlobals.HeuristicsTracer.TraceError<string>(0L, "CorruptDataException exception in a call of loading working hours : {0}", ex5.Message);
			}
			return result;
		}

		// Token: 0x04000D21 RID: 3361
		private readonly IMailboxSession session;

		// Token: 0x04000D22 RID: 3362
		private readonly StorageWorkingHours hours;

		// Token: 0x04000D23 RID: 3363
		private readonly System.DayOfWeek firstDayOfWeek;
	}
}
