using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.JunkEmailOptions;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.JunkEmailOptions
{
	// Token: 0x02000117 RID: 279
	internal sealed class JunkEmailOptionsCommiterAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x06000B57 RID: 2903 RVA: 0x00049394 File Offset: 0x00047594
		public JunkEmailOptionsCommiterAssistantType()
		{
			if (Configuration.IsTestConfiguration)
			{
				JunkEmailOptionsCommiterAssistantType.Tracer.TraceDebug((long)this.GetHashCode(), "Assistant is running with test configuration.");
			}
			this.workCycle = Configuration.UpdateInterval;
			JunkEmailOptionsCommiterAssistantType.Tracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "Assistant is running with update interval: {0}.", this.workCycle);
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x000493EB File Offset: 0x000475EB
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.JunkEmailOptionsCommitterAssistant;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x000493EE File Offset: 0x000475EE
		public LocalizedString Name
		{
			get
			{
				return Strings.jeoName;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x000493F5 File Offset: 0x000475F5
		public string NonLocalizedName
		{
			get
			{
				return "JunkEmailOptionsCommitterAssistant";
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x000493FC File Offset: 0x000475FC
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.JunkEmailOptionsCommitterAssistant;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000493FF File Offset: 0x000475FF
		public TimeSpan WorkCycle
		{
			get
			{
				return this.workCycle;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x00049407 File Offset: 0x00047607
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return this.workCycle;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0004940F File Offset: 0x0004760F
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForJunkEmailAssistant;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x00049416 File Offset: 0x00047616
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return JunkEmailOptionsCommiterAssistantType.ExtendedProps;
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00049420 File Offset: 0x00047620
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			if (mailboxInformation.IsPublicFolderMailbox())
			{
				return false;
			}
			object mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.JunkEmailSafeListDirty);
			if (mailboxProperty == null || (int)mailboxProperty < 1)
			{
				JunkEmailOptionsCommiterAssistantType.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox {0} is not interesting.  Either doesn't have JunkEmailSafeListDirty or number of attempts is zero.", mailboxInformation.DisplayName);
				return false;
			}
			JunkEmailOptionsCommiterAssistantType.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Mailbox {0} is interesting.", mailboxInformation.DisplayName);
			return true;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0004948A File Offset: 0x0004768A
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new JunkEmailOptionsCommiterAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0004949E File Offset: 0x0004769E
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x04000713 RID: 1811
		internal const string AssistantName = "JunkEmailOptionsCommitterAssistant";

		// Token: 0x04000714 RID: 1812
		private static readonly Trace Tracer = ExTraceGlobals.JEOAssistantTracer;

		// Token: 0x04000715 RID: 1813
		private static readonly PropertyTagPropertyDefinition[] ExtendedProps = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.JunkEmailSafeListDirty
		};

		// Token: 0x04000716 RID: 1814
		private readonly TimeSpan workCycle;
	}
}
