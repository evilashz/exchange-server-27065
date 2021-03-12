using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ContactLinking
{
	// Token: 0x0200014B RID: 331
	public abstract class ContactLinkingBaseCmdLet : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000370AC File Offset: 0x000352AC
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x000370C3 File Offset: 0x000352C3
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override MailboxIdParameter Identity
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000370D6 File Offset: 0x000352D6
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x000370DE File Offset: 0x000352DE
		internal ContactLinkingPerformanceTracker PerformanceTracker { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000BD9 RID: 3033
		protected abstract string UserAgent { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x000370E7 File Offset: 0x000352E7
		protected virtual bool OwnsPerformanceTrackerLifeCycle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BDB RID: 3035
		internal abstract void ContactLinkingOperation(MailboxSession mailboxSession);

		// Token: 0x06000BDC RID: 3036 RVA: 0x000370EC File Offset: 0x000352EC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			CmdletProxy.ThrowExceptionIfProxyIsNeeded(base.CurrentTaskContext, this.DataObject, false, this.ConfirmationMessage, null);
			try
			{
				using (MailboxSession mailboxSession = ContactLinkingBaseCmdLet.OpenMailboxSessionAsAdmin(this.DataObject, this.UserAgent))
				{
					this.PerformanceTracker = new ContactLinkingPerformanceTracker(mailboxSession);
					if (this.OwnsPerformanceTrackerLifeCycle)
					{
						this.PerformanceTracker.Start();
					}
					this.ContactLinkingOperation(mailboxSession);
				}
			}
			catch (Exception ex)
			{
				TaskLogger.LogError(ex);
				this.WriteError(ex, ErrorCategory.NotSpecified, this.Identity, true);
			}
			finally
			{
				if (this.OwnsPerformanceTrackerLifeCycle && this.PerformanceTracker != null)
				{
					this.PerformanceTracker.Stop();
				}
				this.WritePerformanceData();
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000371C8 File Offset: 0x000353C8
		private static MailboxSession OpenMailboxSessionAsAdmin(ADUser user, string userAgent)
		{
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromADUser(user, null);
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, userAgent);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000371EC File Offset: 0x000353EC
		private static PSObject CreateResultObject(MailboxIdParameter mailbox, ILogEvent logEvent)
		{
			PSObject psobject = new PSObject();
			psobject.Properties.Add(new PSNoteProperty("Mailbox", mailbox));
			foreach (KeyValuePair<string, object> keyValuePair in logEvent.GetEventData())
			{
				psobject.Properties.Add(new PSNoteProperty(keyValuePair.Key, keyValuePair.Value));
			}
			return psobject;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00037270 File Offset: 0x00035470
		private void WritePerformanceData()
		{
			if (this.PerformanceTracker != null)
			{
				ILogEvent logEvent = this.PerformanceTracker.GetLogEvent();
				base.WriteObject(ContactLinkingBaseCmdLet.CreateResultObject(this.Identity, logEvent));
			}
		}
	}
}
