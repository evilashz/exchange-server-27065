using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Assistants.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxIssueDetectors;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorHelpers;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor
{
	// Token: 0x0200023B RID: 571
	internal sealed class MailboxProcessorAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001578 RID: 5496 RVA: 0x0007A104 File Offset: 0x00078304
		public MailboxProcessorAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0007A11C File Offset: 0x0007831C
		public static XElement GetMailboxProcessorAssistantDiagnosticInfo(DiagnosticsArgument arguments)
		{
			XElement xelement = new XElement("MailboxProcessorAssistant");
			if (arguments.HasArgument("mailboxprocessorscantime"))
			{
				MailboxProcessorAssistant.AddDatabasesScanTimeToDiagnosticXml(xelement);
			}
			if (arguments.HasArgument("lockedmailboxes"))
			{
				xelement.Add(MailboxLockedDetector.GetLockedMailboxesDiagnosticInfo());
			}
			return xelement;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0007A170 File Offset: 0x00078370
		public override List<MailboxData> GetMailboxesToProcess()
		{
			if (!AssistantsService.AssistantsLastScanTimes.ContainsKey(WorkloadType.MailboxProcessorAssistant))
			{
				AssistantsService.AssistantsLastScanTimes[WorkloadType.MailboxProcessorAssistant] = new Dictionary<string, DateTime>();
			}
			AssistantsService.AssistantsLastScanTimes[WorkloadType.MailboxProcessorAssistant][base.DatabaseInfo.DatabaseName] = DateTime.UtcNow;
			MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "MailboxProcessorAssistant is starting to process mailboxes on {0}", new object[]
			{
				base.DatabaseInfo.DisplayName
			});
			this.RegisterMailboxProcessorsForCurrentWorkcycle();
			this.mailboxProcessorStartWorkcycleActions();
			HashSet<PropTag> hashSet = new HashSet<PropTag>();
			foreach (IMailboxProcessor mailboxProcessor in from processor in this.mailboxProcessors
			where processor.IsEnabled
			select processor)
			{
				hashSet.AddRange(mailboxProcessor.RequiredMailboxTableProperties);
			}
			return MailboxTableQueryUtils.GetMailboxTable(hashSet.ToArray<PropTag>(), base.DatabaseInfo);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0007A284 File Offset: 0x00078484
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			foreach (IMailboxProcessor mailboxProcessor in from processor in this.mailboxProcessors
			where processor.IsEnabled
			select processor)
			{
				try
				{
					mailboxProcessor.ProcessSingleMailbox(invokeArgs.MailboxData);
				}
				catch (LocalizedException ex)
				{
					MailboxProcessorAssistantType.Tracer.TraceError((long)this.GetHashCode(), "Processor of type {0} threw and exception of type {1} with message {2}. Was processing mailbox {3}.", new object[]
					{
						mailboxProcessor.GetType().ToString(),
						ex.GetType().ToString(),
						ex.Message,
						invokeArgs.MailboxData.DisplayName
					});
					this.DisableMailboxProcessor(mailboxProcessor);
					mailboxProcessor.OnStopWorkcycle();
				}
			}
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0007A368 File Offset: 0x00078568
		public void OnWorkCycleCheckpoint()
		{
			this.mailboxProcessorStopWorkcycleActions();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0007A378 File Offset: 0x00078578
		private static void AddDatabasesScanTimeToDiagnosticXml(XElement diagnosticElement)
		{
			if (AssistantsService.AssistantsLastScanTimes.ContainsKey(WorkloadType.MailboxProcessorAssistant))
			{
				foreach (KeyValuePair<string, DateTime> keyValuePair in AssistantsService.AssistantsLastScanTimes[WorkloadType.MailboxProcessorAssistant])
				{
					XElement xelement = new XElement("MailboxProcessorLastScan");
					xelement.Add(new XElement("MailboxDatabase", keyValuePair.Key));
					xelement.Add(new XElement("LastScan", keyValuePair.Value.ToString(CultureInfo.InvariantCulture)));
					diagnosticElement.Add(xelement);
				}
			}
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0007A440 File Offset: 0x00078640
		private void RegisterMailboxProcessorsForCurrentWorkcycle()
		{
			this.mailboxProcessorConstructors = null;
			this.mailboxProcessorConstructors = (MailboxProcessorAssistant.MailboxProcessorConstructor)Delegate.Combine(this.mailboxProcessorConstructors, new MailboxProcessorAssistant.MailboxProcessorConstructor(() => new MailboxLockedDetector()));
			MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "{0} mailbox processors were registered for current workcycle.", new object[]
			{
				this.mailboxProcessorConstructors.GetInvocationList().Count<Delegate>()
			});
			this.RefreshRegisteredMailboxProcessors();
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0007A4C0 File Offset: 0x000786C0
		private void RefreshRegisteredMailboxProcessors()
		{
			this.mailboxProcessors.Clear();
			this.mailboxProcessorStartWorkcycleActions = null;
			this.mailboxProcessorStopWorkcycleActions = null;
			foreach (MailboxProcessorAssistant.MailboxProcessorConstructor mailboxProcessorConstructor in this.mailboxProcessorConstructors.GetInvocationList())
			{
				IMailboxProcessor mailboxProcessor = mailboxProcessorConstructor();
				this.EnableMailboxProcessor(mailboxProcessor);
				this.mailboxProcessors.Add(mailboxProcessor);
			}
			MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "{0} of actual mailbox processors were instantiated for the current workcycle.", new object[]
			{
				this.mailboxProcessors.Count
			});
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0007A554 File Offset: 0x00078754
		private void EnableMailboxProcessor(IMailboxProcessor processor)
		{
			if (processor.IsEnabled)
			{
				MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox processor of type {0} already enabled for current workcycle.", new object[]
				{
					processor.GetType().ToString()
				});
				return;
			}
			processor.IsEnabled = true;
			this.mailboxProcessorStartWorkcycleActions = (MailboxProcessorAssistant.MailboxProcessorOnStartStopAction)Delegate.Combine(this.mailboxProcessorStartWorkcycleActions, new MailboxProcessorAssistant.MailboxProcessorOnStartStopAction(processor.OnStartWorkcycle));
			this.mailboxProcessorStopWorkcycleActions = (MailboxProcessorAssistant.MailboxProcessorOnStartStopAction)Delegate.Combine(this.mailboxProcessorStopWorkcycleActions, new MailboxProcessorAssistant.MailboxProcessorOnStartStopAction(processor.OnStopWorkcycle));
			MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox processor of type {0} was enabled for current workcycle.", new object[]
			{
				processor.GetType().ToString()
			});
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0007A608 File Offset: 0x00078808
		private void DisableMailboxProcessor(IMailboxProcessor processor)
		{
			if (!processor.IsEnabled)
			{
				MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox processor of type {0} already disabled for current workcycle.", new object[]
				{
					processor.GetType().ToString()
				});
				return;
			}
			processor.IsEnabled = false;
			this.mailboxProcessorStartWorkcycleActions = (MailboxProcessorAssistant.MailboxProcessorOnStartStopAction)Delegate.Remove(this.mailboxProcessorStartWorkcycleActions, new MailboxProcessorAssistant.MailboxProcessorOnStartStopAction(processor.OnStartWorkcycle));
			this.mailboxProcessorStopWorkcycleActions = (MailboxProcessorAssistant.MailboxProcessorOnStartStopAction)Delegate.Remove(this.mailboxProcessorStopWorkcycleActions, new MailboxProcessorAssistant.MailboxProcessorOnStartStopAction(processor.OnStopWorkcycle));
			MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox processor of type {0} was disabled for current workcycle.", new object[]
			{
				processor.GetType().ToString()
			});
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0007A6B9 File Offset: 0x000788B9
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0007A6C1 File Offset: 0x000788C1
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x0007A6C9 File Offset: 0x000788C9
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000CB8 RID: 3256
		private MailboxProcessorAssistant.MailboxProcessorConstructor mailboxProcessorConstructors;

		// Token: 0x04000CB9 RID: 3257
		private MailboxProcessorAssistant.MailboxProcessorOnStartStopAction mailboxProcessorStartWorkcycleActions;

		// Token: 0x04000CBA RID: 3258
		private MailboxProcessorAssistant.MailboxProcessorOnStartStopAction mailboxProcessorStopWorkcycleActions;

		// Token: 0x04000CBB RID: 3259
		private readonly List<IMailboxProcessor> mailboxProcessors = new List<IMailboxProcessor>();

		// Token: 0x0200023C RID: 572
		// (Invoke) Token: 0x06001589 RID: 5513
		private delegate IMailboxProcessor MailboxProcessorConstructor();

		// Token: 0x0200023D RID: 573
		// (Invoke) Token: 0x0600158D RID: 5517
		private delegate void MailboxProcessorOnStartStopAction();
	}
}
