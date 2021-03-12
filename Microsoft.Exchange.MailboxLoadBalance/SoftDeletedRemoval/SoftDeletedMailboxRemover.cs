using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxLoadBalance.Anchor;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.LoadBalance;
using Microsoft.Exchange.MailboxLoadBalance.Logging;
using Microsoft.Exchange.MailboxLoadBalance.Logging.SoftDeletedMailboxRemoval;
using Microsoft.Exchange.MailboxLoadBalance.ServiceSupport;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x02000101 RID: 257
	internal class SoftDeletedMailboxRemover
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x00015789 File Offset: 0x00013989
		public SoftDeletedMailboxRemover(DirectoryDatabase database, LoadBalanceAnchorContext context, ByteQuantifiedSize targetDatabaseSize, ObjectLogCollector logCollector)
		{
			this.database = database;
			this.context = context;
			this.targetDatabaseSize = targetDatabaseSize;
			this.logCollector = logCollector;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00015994 File Offset: 0x00013B94
		public void RemoveFromDatabase()
		{
			ByteQuantifiedSize currentSize = this.database.GetSize().CurrentPhysicalSize;
			ByteQuantifiedSize currentSize2 = currentSize;
			List<NonConnectedMailbox> source = this.database.GetDisconnectedMailboxes().ToList<NonConnectedMailbox>();
			this.context.Logger.LogInformation("Beginning soft-deleted mailbox cleanup on database '{0}'", new object[]
			{
				this.database.Name
			});
			this.context.Logger.LogInformation("Current size of database '{0}' is {1}, with a target size of {2}", new object[]
			{
				this.database.Name,
				currentSize,
				this.targetDatabaseSize
			});
			int totalCountRemoved = 0;
			ByteQuantifiedSize totalSizeRemoved = default(ByteQuantifiedSize);
			IOperationRetryManager operationRetryManager = LoadBalanceOperationRetryManager.Create(1, TimeSpan.Zero, this.context.Logger);
			using (IEnumerator<NonConnectedMailbox> enumerator = (from o in source
			orderby o.DisconnectDate
			select o).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NonConnectedMailbox softDeletedMailbox = enumerator.Current;
					if (currentSize < this.targetDatabaseSize)
					{
						this.context.Logger.LogInformation("Current size of database '{0}' is below max size allowed by soft deleted threshold. Done deleting soft deleted mailboxes", new object[]
						{
							this.database.Name
						});
						break;
					}
					if (softDeletedMailbox.IsSoftDeleted)
					{
						Exception exception = null;
						bool success = false;
						SoftDeletedMailboxRemovalLogEntry entry = new SoftDeletedMailboxRemovalLogEntry();
						entry[SoftDeletedMailboxRemovalLogEntrySchema.MailboxGuid] = softDeletedMailbox.Guid;
						entry[SoftDeletedMailboxRemovalLogEntrySchema.DatabaseName] = this.database.Name;
						entry[SoftDeletedMailboxRemovalLogEntrySchema.TargetDatabaseSize] = this.targetDatabaseSize;
						entry[SoftDeletedMailboxRemovalLogEntrySchema.CurrentDatabaseSize] = currentSize;
						entry[SoftDeletedMailboxRemovalLogEntrySchema.OriginalDatabaseSize] = currentSize2;
						entry[SoftDeletedMailboxRemovalLogEntrySchema.MailboxSize] = softDeletedMailbox.PhysicalSize;
						OperationRetryManagerResult operationRetryManagerResult = operationRetryManager.TryRun(delegate
						{
							this.context.Logger.LogVerbose("Removing SoftDeleted Mailbox from database '{0}'. MailboxGuid: {1} MailboxSize: {2}", new object[]
							{
								this.database.Name,
								softDeletedMailbox.Guid,
								softDeletedMailbox.PhysicalSize
							});
							SoftDeleteMailboxRemovalCheckRemoval mailboxRemovalReadiness = this.GetMailboxRemovalReadiness(softDeletedMailbox);
							entry[SoftDeletedMailboxRemovalLogEntrySchema.RemovalAllowed] = mailboxRemovalReadiness.CanRemove;
							entry[SoftDeletedMailboxRemovalLogEntrySchema.RemovalDisallowedReason] = mailboxRemovalReadiness.Reason;
							if (mailboxRemovalReadiness.CanRemove)
							{
								success = this.context.TryRemoveSoftDeletedMailbox(softDeletedMailbox.Guid, this.database.Guid, out exception);
								if (success)
								{
									currentSize -= softDeletedMailbox.PhysicalSize;
									totalCountRemoved++;
									totalSizeRemoved += softDeletedMailbox.PhysicalSize;
									this.context.Logger.LogVerbose("Mailbox '{0}' removed", new object[]
									{
										softDeletedMailbox.Guid
									});
								}
							}
						});
						if (!operationRetryManagerResult.Succeeded)
						{
							exception = operationRetryManagerResult.Exception;
						}
						entry[SoftDeletedMailboxRemovalLogEntrySchema.Removed] = success;
						entry[SoftDeletedMailboxRemovalLogEntrySchema.Error] = exception;
						this.logCollector.LogObject<SoftDeletedMailboxRemovalLogEntry>(entry);
					}
				}
			}
			this.context.Logger.LogInformation("Approximate final size of database '{0}' is {1}. Target size was {2}. {3} total mailboxes with total size of {4} deleted.", new object[]
			{
				this.database.Name,
				currentSize,
				this.targetDatabaseSize,
				totalCountRemoved,
				totalSizeRemoved
			});
			this.context.Logger.LogInformation("Finished soft-deleted mailbox cleanup on database '{0}'", new object[]
			{
				this.database.Name
			});
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00015D4C File Offset: 0x00013F4C
		protected virtual SoftDeleteMailboxRemovalCheckRemoval GetMailboxRemovalReadiness(NonConnectedMailbox mailbox)
		{
			DirectoryIdentity directoryIdentity = DirectoryIdentity.CreateMailboxIdentity(mailbox.Guid, mailbox.OrganizationId, DirectoryObjectType.Mailbox);
			DirectoryIdentity identity = this.database.Identity;
			DirectoryDatabase databaseForMailbox;
			try
			{
				databaseForMailbox = this.context.Directory.GetDatabaseForMailbox(directoryIdentity);
			}
			catch (RecipientNotFoundException)
			{
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval("Recipient for mailbox '{0}' and organizationId '{1}' could not be found so removal can not take place", new object[]
				{
					directoryIdentity,
					mailbox.Identity.OrganizationId
				});
			}
			if (databaseForMailbox == null)
			{
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval("Could not identify a database for mailbox '{0}' and organizationId '{1}'", new object[]
				{
					directoryIdentity,
					mailbox.Identity.OrganizationId
				});
			}
			SoftDeletedRemovalData data = new SoftDeletedRemovalData(identity, databaseForMailbox.Identity, directoryIdentity, mailbox.ItemCount, mailbox.DisconnectDate);
			SoftDeleteMailboxRemovalCheckRemoval result;
			using (ILoadBalanceService loadBalanceClientForDatabase = this.context.ClientFactory.GetLoadBalanceClientForDatabase(databaseForMailbox))
			{
				result = loadBalanceClientForDatabase.CheckSoftDeletedMailboxRemoval(data);
			}
			return result;
		}

		// Token: 0x040002F9 RID: 761
		private readonly LoadBalanceAnchorContext context;

		// Token: 0x040002FA RID: 762
		private readonly DirectoryDatabase database;

		// Token: 0x040002FB RID: 763
		private readonly ObjectLogCollector logCollector;

		// Token: 0x040002FC RID: 764
		private readonly ByteQuantifiedSize targetDatabaseSize;
	}
}
