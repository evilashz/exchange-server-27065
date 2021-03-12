using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.MailboxProcessor;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor
{
	// Token: 0x0200023E RID: 574
	internal sealed class MailboxProcessorAssistantType : AdminRpcAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0007A6D1 File Offset: 0x000788D1
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.MailboxProcessorAssistant;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0007A6D5 File Offset: 0x000788D5
		public LocalizedString Name
		{
			get
			{
				return Strings.mailboxProcessorAssistantName;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0007A6DC File Offset: 0x000788DC
		public string NonLocalizedName
		{
			get
			{
				return "Mailbox Processor Assistant";
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0007A6E3 File Offset: 0x000788E3
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new MailboxProcessorAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0007A6F7 File Offset: 0x000788F7
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.MailboxProcessorAssistant;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0007A6FB File Offset: 0x000788FB
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0007A6FE File Offset: 0x000788FE
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return Array<PropertyTagPropertyDefinition>.Empty;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0007A705 File Offset: 0x00078905
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.MailboxProcessorWorkCycle.Read();
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0007A711 File Offset: 0x00078911
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.MailboxProcessorWorkCycle.Read();
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0007A71D File Offset: 0x0007891D
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0007A71F File Offset: 0x0007891F
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			return true;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0007A722 File Offset: 0x00078922
		public static void TraceInformation(long id, string formatString, params object[] args)
		{
			MailboxProcessorAssistantType.Tracer.TraceInformation(0, id, formatString, args);
		}

		// Token: 0x04000CBF RID: 3263
		internal const string AssistantName = "Mailbox Processor Assistant";

		// Token: 0x04000CC0 RID: 3264
		internal static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
