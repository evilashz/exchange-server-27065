using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200004E RID: 78
	internal class ElcFolderSubAssistant : ElcSubAssistant
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x000109A4 File Offset: 0x0000EBA4
		public ElcFolderSubAssistant(DatabaseInfo databaseInfo, ELCAssistantType elcAssistantType, ELCHealthMonitor healthMonitor) : base(databaseInfo, healthMonitor)
		{
			base.ElcAssistantType = elcAssistantType;
			this.elcAuditLog = new ElcAuditLog(databaseInfo);
			this.folderProvisioner = new FolderProvisioner(databaseInfo, this.elcAuditLog, this);
			this.enforcerManager = new EnforcerManager(databaseInfo, this.elcAuditLog, this);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000109F2 File Offset: 0x0000EBF2
		public override void Dispose()
		{
			if (this.elcAuditLog != null)
			{
				this.elcAuditLog.Close();
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010A07 File Offset: 0x0000EC07
		public override string ToString()
		{
			if (base.DebugString == null)
			{
				base.DebugString = "ELC Folder assistant for " + base.DatabaseInfo.ToString();
			}
			return base.DebugString;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010A34 File Offset: 0x0000EC34
		internal override void OnWindowBegin()
		{
			ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowBegin entered.", this);
			this.folderProvisioner.OnWindowBegin();
			this.enforcerManager.OnWindowBegin();
			this.elcAuditLog.Open(base.ElcAssistantType.ToString());
			ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowBegin exited.", this);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010A9C File Offset: 0x0000EC9C
		internal void Invoke(MailboxSession mailboxSession, MailboxData mailboxData)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.InvokeInternal(mailboxSession);
			}
			finally
			{
				mailboxData.StatisticsLogEntry.FolderSubAssistantProcessingTime = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010ADC File Offset: 0x0000ECDC
		internal void InvokeInternal(MailboxSession mailboxSession)
		{
			try
			{
				if (mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
				{
					ElcSubAssistant.Tracer.TraceError<ElcFolderSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Mailbox owner {1} is archive mailbox. Skipping MRM version 1.", this, mailboxSession.MailboxOwner);
				}
				else
				{
					ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Collecting ELC information for mailbox '{1}'.", this, mailboxSession.MailboxOwner);
					ElcUserFolderInformation elcUserFolderInformation = new ElcUserFolderInformation(mailboxSession, base.ElcAssistantType.AdCache.GetAllFolders(mailboxSession.MailboxOwner));
					try
					{
						elcUserFolderInformation.Build();
					}
					catch (ELCNoMatchingOrgFoldersException ex)
					{
						ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant, ELCNoMatchingOrgFoldersException>((long)this.GetHashCode(), "{0}: An org folder attached to this user could not be found in the list of all folders. Exception: {1}. ", this, ex);
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidElcDataInAD, null, new object[]
						{
							mailboxSession.MailboxOwner,
							ex.ToString()
						});
						throw;
					}
					if (!elcUserFolderInformation.NeedsElcEnforcement())
					{
						ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Mailbox '{1}' does not require ELC processing.", this, mailboxSession.MailboxOwner);
					}
					else
					{
						base.ThrowIfShuttingDown(mailboxSession.MailboxOwner);
						if (this.elcAuditLog.LoggingEnabled)
						{
							this.elcAuditLog.Append("Starting to process mailbox: " + mailboxSession.MailboxOwner, base.ElcAssistantType.ToString());
						}
						this.folderProvisioner.Invoke(elcUserFolderInformation);
						ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Invoke of FolderProvisioner has been called for mailbox '{1}'.", this, mailboxSession.MailboxOwner);
						List<AdFolderData> userAdFolders = elcUserFolderInformation.UserAdFolders;
						if (userAdFolders == null || userAdFolders.Count == 0)
						{
							ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: No policies exist in AD for mailbox '{1}'. Skipping this mailbox.", this, mailboxSession.MailboxOwner);
						}
						else
						{
							ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Policies exist in AD for mailbox '{1}'. Processing this mailbox.", this, mailboxSession.MailboxOwner);
							this.enforcerManager.Invoke(mailboxSession, elcUserFolderInformation);
						}
					}
				}
			}
			catch (DataValidationException ex2)
			{
				ElcSubAssistant.Tracer.TraceError<ElcFolderSubAssistant, DataValidationException, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Invalid data in the AD '{1}' found while processing mailbox '{2}'. Skipping the mailbox.", this, ex2, mailboxSession.MailboxOwner);
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_InvalidElcDataInAD, null, new object[]
				{
					mailboxSession.MailboxOwner,
					ex2.ToString()
				});
				throw new SkipException(ex2);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010D24 File Offset: 0x0000EF24
		internal override void OnWindowEnd()
		{
			ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd entered.", this);
			this.folderProvisioner.OnWindowEnd();
			this.enforcerManager.OnWindowEnd();
			this.elcAuditLog.Close();
			ElcSubAssistant.Tracer.TraceDebug<ElcFolderSubAssistant>((long)this.GetHashCode(), "{0}: OnWindowEnd exited.", this);
		}

		// Token: 0x0400024D RID: 589
		private FolderProvisioner folderProvisioner;

		// Token: 0x0400024E RID: 590
		private EnforcerManager enforcerManager;

		// Token: 0x0400024F RID: 591
		private ElcAuditLog elcAuditLog;
	}
}
