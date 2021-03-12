using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.FaultInjection;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.UMReporting
{
	// Token: 0x020001C5 RID: 453
	internal sealed class UMReportingAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001173 RID: 4467 RVA: 0x00066353 File Offset: 0x00064553
		public UMReportingAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0006635E File Offset: 0x0006455E
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00066360 File Offset: 0x00064560
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			string text = mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			try
			{
				CallIdTracer.TraceDebug(UMReportingAssistant.Tracer, this.GetHashCode(), "UMReportingAssistant invoked. MDB {0}, UserPrincipalName {1}", new object[]
				{
					base.DatabaseInfo.DisplayName,
					text
				});
				UMReportUtil.DoAggregation(mailboxSession);
				UMReportUtil.SetMailboxExtendedProperty(mailboxSession, false);
				CallIdTracer.TraceDebug(UMReportingAssistant.Tracer, this.GetHashCode(), "UMReportingAssistant returning. MDB {0}, UserPrincipalName {1}", new object[]
				{
					base.DatabaseInfo.DisplayName,
					text
				});
			}
			catch (UnableToFindUMReportDataException ex)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnableToFindUMReportData, null, new object[]
				{
					text,
					CommonUtil.ToEventLogString(ex)
				});
				this.TraceException(text, ex);
				UMReportUtil.SetMailboxExtendedProperty(mailboxSession, false);
			}
			catch (CorruptDataException ex2)
			{
				UmGlobals.ExEvent.LogEvent(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId, UMEventLogConstants.Tuple_FatalErrorDuringAggregation, text, text, CommonUtil.ToEventLogString(ex2));
				this.TraceException(text, ex2);
				bool flag = true;
				FaultInjectionUtils.FaultInjectChangeValue<bool>(3945147709U, ref flag);
				if (flag)
				{
					ExceptionHandling.SendWatsonWithExtraData(ex2, false);
				}
				UMReportUtil.TryDeleteUMReportConfig(mailboxSession);
			}
			catch (DataSourceOperationException ex3)
			{
				this.TraceException(text, ex3);
				throw new SkipException(ex3);
			}
			catch (StoragePermanentException ex4)
			{
				this.TraceException(text, ex4);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_PermanentErrorDuringAggregation, null, new object[]
				{
					text,
					CommonUtil.ToEventLogString(ex4)
				});
				throw new SkipException(ex4);
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x00066538 File Offset: 0x00064738
		private void TraceException(string mailboxOwner, Exception ex)
		{
			CallIdTracer.TraceError(UMReportingAssistant.Tracer, this.GetHashCode(), "UMReportingAssistant: Error occurred during aggregation in mailbox {0}, exception = {1}", new object[]
			{
				mailboxOwner,
				ex
			});
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0006657B File Offset: 0x0006477B
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00066583 File Offset: 0x00064783
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0006658B File Offset: 0x0006478B
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000AE4 RID: 2788
		private static readonly Trace Tracer = ExTraceGlobals.UMReportsTracer;
	}
}
