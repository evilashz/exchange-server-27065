using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Assistants.TopN;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.TopN
{
	// Token: 0x02000187 RID: 391
	internal class TopNAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x0005C29C File Offset: 0x0005A49C
		public TopNAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0005C2A7 File Offset: 0x0005A4A7
		public void OnWorkCycleCheckpoint()
		{
			TopNAssistant.Tracer.TraceDebug<TopNAssistant>((long)this.GetHashCode(), "{0}: OnWorkCycleCheckpoint.", this);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0005C2C0 File Offset: 0x0005A4C0
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			try
			{
				ExDateTime now = ExDateTime.Now;
				if (!mailboxSession.Capabilities.CanHaveUserConfigurationManager)
				{
					TopNAssistant.Tracer.TraceDebug<TopNAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Skipping mailbox {1} for TopN because it can't have user configuration manager. Possibly an alternate mailbox.", this, mailboxSession.MailboxOwner);
				}
				else
				{
					MailboxScanner mailboxScanner = new MailboxScanner(mailboxSession, this);
					mailboxScanner.ProcessMailbox();
					ExDateTime now2 = ExDateTime.Now;
					TopNPerf.TimeToProcessLastMailbox.RawValue = (long)(now2 - now).TotalMilliseconds;
					TopNPerf.NumberOfMailboxesProcessed.Increment();
				}
			}
			catch (StoragePermanentException innerException)
			{
				throw new SkipException(innerException);
			}
			catch (StorageTransientException innerException2)
			{
				throw new TransientMailboxException(innerException2);
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0005C37C File Offset: 0x0005A57C
		public void OnWindowEnd()
		{
			TopNAssistant.Tracer.TraceDebug<TopNAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd entered.", this);
			TopNAssistant.Tracer.TraceDebug<TopNAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd exited.", this);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0005C3AC File Offset: 0x0005A5AC
		protected override void OnShutdownInternal()
		{
			this.isShutdown = true;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0005C3B5 File Offset: 0x0005A5B5
		internal void ThrowIfShuttingDown(IExchangePrincipal mailboxOwner)
		{
			if (this.isShutdown)
			{
				TopNAssistant.Tracer.TraceDebug<TopNAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Shutdown called during processing of mailbox '{1}'.", this, mailboxOwner);
				throw new ShutdownException();
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0005C3F3 File Offset: 0x0005A5F3
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0005C3FB File Offset: 0x0005A5FB
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0005C403 File Offset: 0x0005A603
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x040009E1 RID: 2529
		private bool isShutdown;

		// Token: 0x040009E2 RID: 2530
		private static readonly Trace Tracer = ExTraceGlobals.TopNAssistantTracer;

		// Token: 0x040009E3 RID: 2531
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
