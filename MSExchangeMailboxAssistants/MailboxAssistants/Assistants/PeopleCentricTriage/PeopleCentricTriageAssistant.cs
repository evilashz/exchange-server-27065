using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.ApplicationLogic.PeopleCentricTriage;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PeopleCentricTriage
{
	// Token: 0x02000219 RID: 537
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class PeopleCentricTriageAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x000761BC File Offset: 0x000743BC
		public PeopleCentricTriageAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName, PeopleCentricTriageConfiguration configuration, IPerformanceDataLogger perfLogger, ITracer tracer) : base(databaseInfo, name, nonLocalizedName)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.configuration = configuration;
			this.perfLogger = perfLogger;
			this.tracer = tracer;
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0007620E File Offset: 0x0007440E
		public void OnWorkCycleCheckpoint()
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "PeopleCentricTriageAssistant: work cycle checkpoint.");
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00076227 File Offset: 0x00074427
		protected override void OnShutdownInternal()
		{
			this.tracer.TraceDebug((long)this.GetHashCode(), "PeopleCentricTriageAssistant: shutting down.");
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00076240 File Offset: 0x00074440
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (invokeArgs == null)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "PeopleCentricTriageAssistant: invoked with invalid args.");
				return;
			}
			new MailboxProcessor(this.configuration, this.CreatePeopleIKnowPublisherFactory(), this.perfLogger, this.tracer).Process(new MailboxProcessorRequest
			{
				MailboxSession = invokeArgs.StoreSession,
				IsFlightEnabled = PeopleCentricTriageAssistant.GetFlightEnabled(invokeArgs),
				DiagnosticsText = PeopleCentricTriageAssistant.GetDiagnosticsText(invokeArgs)
			});
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000762B8 File Offset: 0x000744B8
		private static string GetDiagnosticsText(InvokeArgs args)
		{
			return string.Format(CultureInfo.InvariantCulture, "{{ Mailbox Display Name:{0}; Mailbox Owner:{1}; Activity Id:{2}; }}", new object[]
			{
				args.MailboxData.DisplayName,
				(args.StoreSession != null && args.StoreSession.MailboxOwner != null) ? args.StoreSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() : null,
				args.ActivityId
			});
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00076338 File Offset: 0x00074538
		private static bool GetFlightEnabled(InvokeArgs args)
		{
			return args.StoreSession != null && args.StoreSession.MailboxOwner != null && args.StoreSession.MailboxOwner.GetConfiguration().OwaClientServer.PeopleCentricTriage.Enabled;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0007637E File Offset: 0x0007457E
		private IPeopleIKnowPublisherFactory CreatePeopleIKnowPublisherFactory()
		{
			return new PeopleIKnowPublisherFactory(XSOFactory.Default, this.tracer, this.GetHashCode());
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00076396 File Offset: 0x00074596
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0007639E File Offset: 0x0007459E
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000763A6 File Offset: 0x000745A6
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000C57 RID: 3159
		private readonly PeopleCentricTriageConfiguration configuration;

		// Token: 0x04000C58 RID: 3160
		private readonly IPerformanceDataLogger perfLogger;

		// Token: 0x04000C59 RID: 3161
		private readonly ITracer tracer;
	}
}
