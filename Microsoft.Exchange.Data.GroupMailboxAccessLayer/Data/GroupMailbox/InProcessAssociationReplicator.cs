using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.GroupMailbox
{
	// Token: 0x0200002C RID: 44
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class InProcessAssociationReplicator : IAssociationReplicator
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000A27A File Offset: 0x0000847A
		public InProcessAssociationReplicator(IExtensibleLogger logger, IMailboxAssociationPerformanceTracker performanceTracker, OpenAsAdminOrSystemServiceBudgetTypeType budgetType = OpenAsAdminOrSystemServiceBudgetTypeType.RunAsBackgroundLoad)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			ArgumentValidator.ThrowIfNull("performanceTracker", performanceTracker);
			this.Logger = logger;
			this.PerformanceTracker = performanceTracker;
			this.budgetType = budgetType;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A2B0 File Offset: 0x000084B0
		public bool ReplicateAssociation(IAssociationAdaptor masterAdaptor, params MailboxAssociation[] associations)
		{
			ArgumentValidator.ThrowIfNull("associations", associations);
			ArgumentValidator.ThrowIfOutOfRange<int>("associations", associations.Length, 1, 1);
			ArgumentValidator.ThrowIfNull("masterAdaptor", masterAdaptor);
			MailboxAssociation mailboxAssociation = associations[0];
			InProcessAssociationReplicator.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "InProcessAssociationReplicator::ReplicateAssociations: {0}", mailboxAssociation);
			this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.CommandExecution>
			{
				{
					MailboxAssociationLogSchema.CommandExecution.Command,
					"InProcessAssociationReplicator"
				},
				{
					MailboxAssociationLogSchema.CommandExecution.GroupMailbox,
					mailboxAssociation.Group
				},
				{
					MailboxAssociationLogSchema.CommandExecution.UserMailboxes,
					mailboxAssociation.User
				}
			});
			this.PerformanceTracker.IncrementAssociationReplicationAttempts();
			MailboxLocator slaveMailboxLocator = masterAdaptor.GetSlaveMailboxLocator(mailboxAssociation);
			bool flag = false;
			try
			{
				if (slaveMailboxLocator.IsValidReplicationTarget())
				{
					flag = this.ExecuteReplicationToMailbox(masterAdaptor, mailboxAssociation);
				}
				else
				{
					InProcessAssociationReplicator.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "InProcessAssociationReplicator.ReplicateAssociation. Marking association for not replication given target type. Association = {0}", mailboxAssociation);
					this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.CommandExecution>
					{
						{
							MailboxAssociationLogSchema.CommandExecution.Command,
							"InProcessAssociationReplicator.MEU.Poison"
						},
						{
							MailboxAssociationLogSchema.CommandExecution.GroupMailbox,
							mailboxAssociation.Group
						},
						{
							MailboxAssociationLogSchema.CommandExecution.UserMailboxes,
							mailboxAssociation.User
						}
					});
					mailboxAssociation.SyncedVersion = int.MaxValue;
					mailboxAssociation.LastSyncError = "Target mailbox is not capable of replication";
					flag = true;
				}
			}
			catch (MailboxNotFoundException ex)
			{
				if (this.DeleteMailboxNotFoundAssociation(mailboxAssociation, masterAdaptor, ex))
				{
					InProcessAssociationReplicator.Tracer.TraceDebug((long)this.GetHashCode(), "InProcessAssociationReplicator.ReplicateAssociation. Succeeded deleting association for not found mailbox, reporting successful replication and skipping SaveAssociationAfterReplicationAttempt.");
					return true;
				}
				this.ProcessFailure(mailboxAssociation, ex.ToString());
			}
			if (!flag)
			{
				masterAdaptor.AssociationStore.SaveMailboxAsOutOfSync();
			}
			this.SaveAssociationAfterReplicationAttempt(masterAdaptor, mailboxAssociation);
			return flag;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A430 File Offset: 0x00008630
		internal static UpdateMailboxAssociationType CreateUpdateMailboxAssociationType(IMailboxLocator master, MailboxAssociation association)
		{
			ArgumentValidator.ThrowIfNull("association", association);
			return new UpdateMailboxAssociationType
			{
				Master = InProcessAssociationReplicator.CreateMasterMailboxData(master),
				Association = EwsAssociationDataConverter.Convert(association)
			};
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A468 File Offset: 0x00008668
		internal static MasterMailboxType CreateMasterMailboxData(IMailboxLocator master)
		{
			ArgumentValidator.ThrowIfNull("master", master);
			ADUser aduser = master.FindAdUser();
			MasterMailboxType masterMailboxType = new MasterMailboxType
			{
				Alias = aduser.Alias,
				DisplayName = aduser.DisplayName,
				SmtpAddress = aduser.PrimarySmtpAddress.ToString(),
				MailboxType = master.LocatorType
			};
			GroupMailboxLocator groupMailboxLocator = master as GroupMailboxLocator;
			if (groupMailboxLocator != null)
			{
				MailboxUrls mailboxUrls = new MailboxUrls(ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite), false);
				masterMailboxType.GroupType = EwsAssociationDataConverter.Convert(groupMailboxLocator.GetGroupType());
				masterMailboxType.GroupTypeSpecified = true;
				masterMailboxType.Description = ((aduser.Description != null && aduser.Description.Count > 0) ? aduser.Description[0] : string.Empty);
				masterMailboxType.Photo = groupMailboxLocator.GetThumbnailPhoto();
				masterMailboxType.SharePointUrl = ((aduser.SharePointUrl != null) ? aduser.SharePointUrl.ToString() : string.Empty);
				masterMailboxType.InboxUrl = mailboxUrls.InboxUrl;
				masterMailboxType.CalendarUrl = mailboxUrls.CalendarUrl;
				masterMailboxType.DomainController = aduser.OriginatingServer;
			}
			return masterMailboxType;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A590 File Offset: 0x00008790
		private static string GetEwsVersionNumber(MailboxAssociationEwsBinding ewsBinding)
		{
			ServerVersionInfo serverVersionInfoValue = ewsBinding.ServerVersionInfoValue;
			return new Version(serverVersionInfoValue.MajorVersion, serverVersionInfoValue.MinorVersion, serverVersionInfoValue.MajorBuildNumber, serverVersionInfoValue.MinorBuildNumber).ToString();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A5C8 File Offset: 0x000087C8
		private static bool ShouldStopReplicatingAssociation(MailboxAssociation association)
		{
			bool flag = association.SyncAttempts > 30 && association.JoinDate.Add(InProcessAssociationReplicator.TimeRequiredAfterJoinToStopAssociationReplicationAttempts) < ExDateTime.UtcNow;
			InProcessAssociationReplicator.Tracer.TraceDebug<bool, MailboxAssociation>(0L, "InProcessAssociationReplicator.ShouldStopReplicatingAssociation. Stop replicating = {0}. Association = {1}.", flag, association);
			return flag;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A614 File Offset: 0x00008814
		private MailboxAssociationEwsBinding CreateMailboxAssociationEwsBinding(IMailboxLocator master, MailboxAssociation association)
		{
			MailboxLocator mailboxLocator;
			if (master is GroupMailboxLocator)
			{
				mailboxLocator = association.User;
			}
			else
			{
				mailboxLocator = association.Group;
			}
			ADUser user = mailboxLocator.FindAdUser();
			return new MailboxAssociationEwsBinding(user, this.budgetType);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A7BC File Offset: 0x000089BC
		private bool ExecuteReplicationToMailbox(IAssociationAdaptor masterAdaptor, MailboxAssociation association)
		{
			bool replicationSucceeded = false;
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						MailboxAssociationEwsBinding.ExecuteEwsOperationWithRetry("ReplicateAssociation", delegate
						{
							using (MailboxAssociationEwsBinding mailboxAssociationEwsBinding = this.CreateMailboxAssociationEwsBinding(masterAdaptor.MasterLocator, association))
							{
								UpdateMailboxAssociationType updateMailboxAssociation = InProcessAssociationReplicator.CreateUpdateMailboxAssociationType(masterAdaptor.MasterLocator, association);
								UpdateMailboxAssociationResponseType response = mailboxAssociationEwsBinding.UpdateMailboxAssociation(updateMailboxAssociation);
								replicationSucceeded = this.ProcessResponse(mailboxAssociationEwsBinding, response, association, masterAdaptor.AssociationStore.MailboxLocator);
							}
						});
					}
					catch (MailboxNotFoundException exception4)
					{
						replicationSucceeded = this.DeleteMailboxNotFoundAssociation(association, masterAdaptor, exception4);
						if (!replicationSucceeded)
						{
							exception = exception4;
						}
					}
					catch (BackEndLocatorException exception5)
					{
						exception = exception5;
					}
					catch (WebException exception6)
					{
						exception = exception6;
					}
					catch (InvalidOperationException exception7)
					{
						exception = exception7;
					}
					catch (LogonAsNetworkServiceException exception8)
					{
						if (!ExEnvironment.IsTest)
						{
							throw;
						}
						exception = exception8;
					}
				});
			}
			catch (GrayException exception)
			{
				GrayException exception9;
				exception = exception9;
			}
			catch (SoapException exception2)
			{
				exception = exception2;
			}
			catch (IOException exception3)
			{
				exception = exception3;
			}
			if (exception != null)
			{
				this.ProcessFailure(association, exception.ToString());
				replicationSucceeded = false;
			}
			return replicationSucceeded;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A890 File Offset: 0x00008A90
		private bool ProcessResponse(MailboxAssociationEwsBinding ewsBinding, UpdateMailboxAssociationResponseType response, MailboxAssociation association, IMailboxLocator mailboxLocator)
		{
			if (response != null && response.ResponseMessages != null && response.ResponseMessages.Items != null && response.ResponseMessages.Items.Length > 0)
			{
				foreach (ResponseMessageType responseMessageType in response.ResponseMessages.Items)
				{
					if (responseMessageType.ResponseClass == ResponseClassType.Success)
					{
						InProcessAssociationReplicator.Tracer.TraceDebug<MailboxAssociation>((long)this.GetHashCode(), "ReplicateAssociation succeeded. Association {0}", association);
						this.ProcessSuccess(association, InProcessAssociationReplicator.GetEwsVersionNumber(ewsBinding), mailboxLocator);
						return true;
					}
					string failureDescription = string.Format(CultureInfo.InvariantCulture, "ReplicateAssociation Failed. Association {0}. ResponseClass={1}, ResponseCode={2}, MessageText={3}", new object[]
					{
						association,
						responseMessageType.ResponseClass,
						responseMessageType.ResponseCode,
						responseMessageType.MessageText
					});
					this.ProcessFailure(association, failureDescription);
				}
			}
			else
			{
				string failureDescription2 = string.Format(CultureInfo.InvariantCulture, "ReplicateAssociation Failed with empty response. Association {0}.", new object[]
				{
					association
				});
				this.ProcessFailure(association, failureDescription2);
			}
			return false;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A9AD File Offset: 0x00008BAD
		private void ProcessSuccess(MailboxAssociation association, string slaveMailboxVersion, IMailboxLocator mailboxLocator)
		{
			association.SyncedIdentityHash = mailboxLocator.IdentityHash;
			association.SyncedVersion = association.CurrentVersion;
			association.SyncedSchemaVersion = slaveMailboxVersion;
			association.SyncAttempts = 0;
			association.LastSyncError = string.Empty;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		private bool DeleteMailboxNotFoundAssociation(MailboxAssociation association, IAssociationAdaptor masterAdaptor, MailboxNotFoundException exception)
		{
			bool flag = association.JoinDate.Add(InProcessAssociationReplicator.TimeRequiredAfterJoinToStopAssociationReplicationAttempts) < ExDateTime.UtcNow;
			InProcessAssociationReplicator.Tracer.TraceDebug<bool, MailboxNotFoundException, MailboxAssociation>((long)this.GetHashCode(), "InProcessAssociationReplicator.DeleteMailboxNotFoundAssociation. Should delete = {0}. Exception = {1}. Association = {2}", flag, exception, association);
			if (flag)
			{
				this.LogWarning("InProcessAssociationReplicator.DeleteMailboxNotFoundAssociation", string.Format("Deleting association given that target mailbox was not found and it is not a recently joined group. Association='{0}'. Exception='{1}'", association, exception));
				masterAdaptor.DeleteAssociation(association);
				return true;
			}
			return false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AA48 File Offset: 0x00008C48
		private void ProcessFailure(MailboxAssociation association, string failureDescription)
		{
			association.LastSyncError = failureDescription;
			association.SyncAttempts++;
			if (InProcessAssociationReplicator.ShouldStopReplicatingAssociation(association))
			{
				this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.CommandExecution>
				{
					{
						MailboxAssociationLogSchema.CommandExecution.Command,
						"InProcessAssociationReplicator.Poison"
					},
					{
						MailboxAssociationLogSchema.CommandExecution.GroupMailbox,
						association.Group
					},
					{
						MailboxAssociationLogSchema.CommandExecution.UserMailboxes,
						association.User
					}
				});
				association.SyncedVersion = int.MaxValue;
			}
			this.PerformanceTracker.IncrementFailedAssociationReplications();
			this.LogError(failureDescription);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AB38 File Offset: 0x00008D38
		private void SaveAssociationAfterReplicationAttempt(IAssociationAdaptor masterAdaptor, MailboxAssociation association)
		{
			Exception exceptionToLog = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						masterAdaptor.SaveSyncState(association);
					}
					catch (StoragePermanentException exceptionToLog2)
					{
						exceptionToLog = exceptionToLog2;
					}
					catch (MapiPermanentException exceptionToLog3)
					{
						exceptionToLog = exceptionToLog3;
					}
					catch (StorageTransientException exceptionToLog4)
					{
						exceptionToLog = exceptionToLog4;
					}
				});
			}
			catch (GrayException exceptionToLog)
			{
				GrayException exceptionToLog5;
				exceptionToLog = exceptionToLog5;
			}
			if (exceptionToLog != null)
			{
				this.LogError("SaveAssociationAfterReplicationAttempt", exceptionToLog.ToString());
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		private void LogError(string errorDescription)
		{
			this.LogError("InProcessAssociationReplicator", errorDescription);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		private void LogError(string context, string errorDescription)
		{
			InProcessAssociationReplicator.Tracer.TraceError((long)this.GetHashCode(), errorDescription);
			this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Error>
			{
				{
					MailboxAssociationLogSchema.Error.Context,
					context
				},
				{
					MailboxAssociationLogSchema.Error.Exception,
					errorDescription
				}
			});
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AC08 File Offset: 0x00008E08
		private void LogWarning(string context, string warningMessage)
		{
			InProcessAssociationReplicator.Tracer.TraceWarning((long)this.GetHashCode(), warningMessage);
			this.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Warning>
			{
				{
					MailboxAssociationLogSchema.Warning.Context,
					context
				},
				{
					MailboxAssociationLogSchema.Warning.Message,
					warningMessage
				}
			});
		}

		// Token: 0x040000A0 RID: 160
		private const int NumberOfFailedAttemptsToStopAssociationReplication = 30;

		// Token: 0x040000A1 RID: 161
		protected readonly IExtensibleLogger Logger;

		// Token: 0x040000A2 RID: 162
		protected readonly IMailboxAssociationPerformanceTracker PerformanceTracker;

		// Token: 0x040000A3 RID: 163
		private static readonly TimeSpan TimeRequiredAfterJoinToStopAssociationReplicationAttempts = TimeSpan.FromDays(1.0);

		// Token: 0x040000A4 RID: 164
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationTracer;

		// Token: 0x040000A5 RID: 165
		private readonly OpenAsAdminOrSystemServiceBudgetTypeType budgetType;
	}
}
