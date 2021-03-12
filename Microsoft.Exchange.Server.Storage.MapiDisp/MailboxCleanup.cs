using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x0200000D RID: 13
	public static class MailboxCleanup
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00036510 File Offset: 0x00034710
		internal static void Initialize()
		{
			if (MailboxCleanup.markExpiredDisconnectedMailboxesForSynchronizationWithDS == null)
			{
				MailboxCleanup.markExpiredDisconnectedMailboxesForSynchronizationWithDS = MaintenanceHandler.RegisterDatabaseMaintenance(MailboxCleanup.MarkExpiredDisconnectedMailboxesForSynchronizationWithDSMaintenanceId, RequiredMaintenanceResourceType.DirectoryServiceAndStore, new MaintenanceHandler.DatabaseMaintenanceDelegate(MailboxCleanup.MarkExpiredDisabledMailboxesForSynchronizationWithDS), "MailboxCleanup.MarkExpiredDisabledMailboxesForSynchronizationWithDS");
				MailboxCleanup.markIdleUserAccessibleMailboxesForSynchronizationWithDS = MaintenanceHandler.RegisterDatabaseMaintenance(MailboxCleanup.MarkIdleUserAccessibleMailboxesForSynchronizationWithDSMaintenanceId, RequiredMaintenanceResourceType.DirectoryServiceAndStore, new MaintenanceHandler.DatabaseMaintenanceDelegate(MailboxCleanup.MarkIdleUserAccessibleMailboxesForSynchronizationWithDS), "MailboxCleanup.MarkIdleUserAccessibleMailboxesForSynchronizationWithDS");
				Mailbox.InitializeSynchronizeWithDSMailboxMaintenance(MailboxCleanup.SynchronizeMailboxWithDSMaintenanceId, RequiredMaintenanceResourceType.DirectoryServiceAndStore, new MaintenanceHandler.MailboxMaintenanceDelegate(MailboxCleanup.SynchronizeDirectoryAndMailboxTable), "MailboxCleanup.SynchronizeDirectoryAndMailboxTable");
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00036582 File Offset: 0x00034782
		internal static void MountedEventHandler(Context context, StoreDatabase database)
		{
			MailboxCleanup.markExpiredDisconnectedMailboxesForSynchronizationWithDS.ScheduleMarkForMaintenance(context, ConfigurationSchema.DiscoverPotentiallyExpiredMailboxesTaskPeriod.Value);
			MailboxCleanup.markIdleUserAccessibleMailboxesForSynchronizationWithDS.ScheduleMarkForMaintenance(context, ConfigurationSchema.DiscoverPotentiallyDisabledMailboxesTaskPeriod.Value);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000365AE File Offset: 0x000347AE
		public static void MarkExpiredDisconnectedMailboxesForSynchronizationWithDSForTest(Context context)
		{
			MailboxCleanup.markExpiredDisconnectedMailboxesForSynchronizationWithDS.MarkForMaintenance(context);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000365BC File Offset: 0x000347BC
		public static void MarkIdleUserAccessibleMailboxesForSynchronizationWithDSForTest(Context context)
		{
			MailboxCleanup.markIdleUserAccessibleMailboxesForSynchronizationWithDS.MarkForMaintenance(context);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000365CC File Offset: 0x000347CC
		private static void MarkExpiredDisabledMailboxesForSynchronizationWithDS(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			DateTime deletedOnThreshold = DateTime.UtcNow - databaseInfo.MailboxRetentionPeriod;
			IMailboxListRestriction mailboxListRestriction = new MailboxCleanup.MailboxListRestrictionExpiredDisconnectedMailbox(deletedOnThreshold);
			MailboxCleanup.MarkMailboxesForSynchronizationWithDS(context, databaseInfo, mailboxListRestriction, out completed);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000365FC File Offset: 0x000347FC
		private static void MarkIdleUserAccessibleMailboxesForSynchronizationWithDS(Context context, DatabaseInfo databaseInfo, out bool completed)
		{
			TimeSpan value = ConfigurationSchema.IdleAccessibleMailboxTime.Value;
			DateTime idleTimeThreshold = DateTime.UtcNow - ((TimeSpan.Compare(value, databaseInfo.MailboxRetentionPeriod) < 0) ? value : databaseInfo.MailboxRetentionPeriod);
			IMailboxListRestriction mailboxListRestriction = new MailboxCleanup.MailboxListRestrictionIdleUserAccessibleMailbox(idleTimeThreshold);
			MailboxCleanup.MarkMailboxesForSynchronizationWithDS(context, databaseInfo, mailboxListRestriction, out completed);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00036658 File Offset: 0x00034858
		private static void MarkMailboxesForSynchronizationWithDS(Context context, DatabaseInfo databaseInfo, IMailboxListRestriction mailboxListRestriction, out bool completed)
		{
			completed = true;
			MailboxTable mailboxTable = Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseSchema.MailboxTable(context.Database);
			using (MailboxList mailboxList = new MailboxList(context, new Column[]
			{
				mailboxTable.MailboxNumber
			}, context.Database, mailboxListRestriction))
			{
				using (Reader reader = mailboxList.OpenList())
				{
					while (reader.Read())
					{
						int @int = reader.GetInt32(mailboxTable.MailboxNumber);
						bool flag;
						MaintenanceHandler.ApplyMaintenanceToOneMailbox(context, @int, ExecutionDiagnostics.OperationSource.MailboxCleanup, false, delegate(Context ctx, MailboxState mbxState)
						{
							Mailbox.SynchronizeWithDSMailboxMaintenance.MarkForMaintenance(ctx, mbxState);
						}, out flag);
						if (!flag)
						{
							completed = false;
						}
					}
				}
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00036718 File Offset: 0x00034918
		public static void ReconnectMailboxToADObject(Context context, Mailbox mailbox, MailboxInfo directoryMailboxInfo)
		{
			MailboxStatus status = mailbox.GetStatus(context);
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(status != MailboxStatus.SoftDeleted, "Cannot reconnect soft deleted mailboxes.");
			bool flag = MailboxStatus.Disabled == status;
			if (flag)
			{
				mailbox.MakeUserAccessible(context);
			}
			mailbox.SetDirectoryPersonalInfoOnMailbox(context, directoryMailboxInfo);
			mailbox.Save(context);
			if (flag)
			{
				MailboxReconnectedNotificationEvent nev = NotificationEvents.CreateMailboxReconnectedNotificationEvent(context, mailbox);
				context.RiseNotificationEvent(nev);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxReconnected, new object[]
				{
					directoryMailboxInfo.MailboxGuid,
					context.Database.MdbName,
					directoryMailboxInfo.OwnerDisplayName
				});
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000367DC File Offset: 0x000349DC
		private static void SynchronizeDirectoryAndMailboxTable(Context context, DatabaseInfo databaseInfo, MailboxInfo directoryMailboxInfo, MailboxState mailboxState, bool explicitRpcCall)
		{
			if (databaseInfo.IsRecoveryDatabase)
			{
				if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<string>(61415L, "Recovery database {0}.", context.Database.MdbName);
				}
				return;
			}
			if (!mailboxState.IsValid)
			{
				mailboxState = MailboxStateCache.Get(context, mailboxState.MailboxNumber);
				if (!mailboxState.IsAccessible)
				{
					if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid>(53223L, "Mailbox {0} is not accessible (active or disconnected).", mailboxState.MailboxGuid);
					}
					return;
				}
			}
			else if (!mailboxState.IsAccessible)
			{
				if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid>(36839L, "Mailbox {0} is not accessible (active or disconnected).", mailboxState.MailboxGuid);
				}
				return;
			}
			InTransitStatus inTransitStatus = InTransitInfo.GetInTransitStatus(mailboxState);
			if (inTransitStatus != InTransitStatus.NotInTransit)
			{
				if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid>(47079L, "Mailbox {0} is in transit.", mailboxState.MailboxGuid);
				}
				return;
			}
			using (Mailbox mailbox = Mailbox.OpenMailbox(context, mailboxState))
			{
				if (PropertyBagHelpers.TestPropertyFlags(context, mailbox, PropTag.Mailbox.MailboxFlags, 16, 16))
				{
					return;
				}
			}
			if (!mailboxState.IsUserAccessible)
			{
				if (mailboxState.IsDisconnected)
				{
					using (Mailbox mailbox2 = Mailbox.OpenMailbox(context, mailboxState))
					{
						string displayName = mailbox2.GetDisplayName(context);
						if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid, string, string>(60344L, "Mailbox {0} / {1} is {2}.", mailboxState.MailboxGuid, displayName, mailboxState.IsDisabled ? "disabled" : "soft deleted");
						}
						if (mailboxState.IsDisabled && directoryMailboxInfo != null)
						{
							if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid, string>(52152L, "Activate mailbox {0} / {1}.", mailboxState.MailboxGuid, displayName);
							}
							MailboxCleanup.ReconnectMailboxToADObject(context, mailbox2, directoryMailboxInfo);
							mailbox2.Disconnect();
							return;
						}
						if ((directoryMailboxInfo == null && mailboxState.IsDisabled) || mailboxState.IsSoftDeleted)
						{
							DateTime? deletedOn = mailbox2.GetDeletedOn(context);
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(deletedOn != null, "there must be DeletedOn if mailbox is not active");
							if (deletedOn.Value + databaseInfo.MailboxRetentionPeriod <= DateTime.UtcNow)
							{
								if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid, string>(46008L, "Hard delete mailbox {0} / {1}.", mailboxState.MailboxGuid, displayName);
								}
								MailboxDeletedNotificationEvent nev = NotificationEvents.CreateMailboxDeletedNotificationEvent(context, mailbox2);
								mailbox2.HardDelete(context);
								context.RiseNotificationEvent(nev);
								Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxPurged, new object[]
								{
									mailboxState.MailboxGuid,
									displayName,
									context.Database.MdbName
								});
								return;
							}
							int num = (int)(deletedOn.Value + databaseInfo.MailboxRetentionPeriod - DateTime.UtcNow).TotalDays + 1;
							Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxRetention, new object[]
							{
								mailboxState.MailboxGuid,
								displayName,
								context.Database.MdbName,
								num
							});
							return;
						}
					}
				}
				return;
			}
			if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid>(63463L, "Mailbox {0} is active.", mailboxState.MailboxGuid);
			}
			if (directoryMailboxInfo == null)
			{
				using (Mailbox mailbox3 = Mailbox.OpenMailbox(context, mailboxState))
				{
					string displayName = mailbox3.GetDisplayName(context);
					if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid, string>(43960L, "Disable mailbox {0} / {1}.", mailboxState.MailboxGuid, displayName);
					}
					MailboxDisconnectedNotificationEvent nev2 = NotificationEvents.CreateMailboxDisconnectedNotificationEvent(context, mailbox3);
					mailbox3.Disable(context);
					context.RiseNotificationEvent(nev2);
					int num = (int)databaseInfo.MailboxRetentionPeriod.TotalDays;
					Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MailboxRetention, new object[]
					{
						mailboxState.MailboxGuid,
						displayName,
						context.Database.MdbName,
						num
					});
					return;
				}
			}
			if (explicitRpcCall)
			{
				using (Mailbox mailbox4 = Mailbox.OpenMailbox(context, mailboxState))
				{
					mailbox4.SetDirectoryPersonalInfoOnMailbox(context, directoryMailboxInfo);
					if ((mailboxState.MailboxType == MailboxInfo.MailboxType.PublicFolderPrimary && directoryMailboxInfo.Type == MailboxInfo.MailboxType.PublicFolderSecondary) || (mailboxState.MailboxType == MailboxInfo.MailboxType.PublicFolderSecondary && directoryMailboxInfo.Type == MailboxInfo.MailboxType.PublicFolderPrimary))
					{
						mailbox4.SetProperty(context, PropTag.Mailbox.MailboxType, (int)directoryMailboxInfo.Type);
						mailbox4.SetProperty(context, PropTag.Mailbox.MailboxTypeDetail, (int)directoryMailboxInfo.TypeDetail);
						context.RegisterStateAction(delegate(Context ctx)
						{
							mailboxState.MailboxType = directoryMailboxInfo.Type;
							mailboxState.MailboxTypeDetail = directoryMailboxInfo.TypeDetail;
						}, null);
					}
					mailbox4.Save(context);
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00036DE0 File Offset: 0x00034FE0
		public static void SynchronizeDirectoryAndMailboxTable(Context context, Guid mailboxGuid, bool explicitRpcCall)
		{
			Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshDatabaseInfo(context, context.Database.MdbGuid);
			DatabaseInfo databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(context, context.Database.MdbGuid);
			context.InitializeMailboxExclusiveOperation(mailboxGuid, ExecutionDiagnostics.OperationSource.AdminRpc, MapiContext.MailboxLockTimeout);
			ErrorCode errorCode = context.StartMailboxOperation(MailboxCreation.DontAllow, true, false);
			if (errorCode == ErrorCodeValue.NotFound)
			{
				DiagnosticContext.TraceLocation((LID)46940U);
				return;
			}
			if (errorCode != ErrorCode.NoError)
			{
				throw new StoreException((LID)55132U, errorCode, "Failed to lock mailbox");
			}
			MailboxInfo directoryMailboxInfo = null;
			if (context.LockedMailboxState.IsUserAccessible || context.LockedMailboxState.IsDisabled)
			{
				TenantHint tenantHint = context.LockedMailboxState.TenantHint;
				int mailboxNumber = context.LockedMailboxState.MailboxNumber;
				context.EndMailboxOperation(false);
				if (!MailboxCleanup.GetMailboxInfoFromAD(context, tenantHint, context.Database.MdbGuid, mailboxGuid, out directoryMailboxInfo))
				{
					if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid>(46812L, "Could not read mailbox info from AD for mailbox {0}.", mailboxGuid);
					}
					DiagnosticContext.TraceLocation((LID)42716U);
					return;
				}
				context.InitializeMailboxExclusiveOperation(mailboxNumber, ExecutionDiagnostics.OperationSource.AdminRpc, MapiContext.MailboxLockTimeout);
				errorCode = context.StartMailboxOperation();
				if (errorCode != ErrorCode.NoError)
				{
					DiagnosticContext.TraceLocation((LID)38620U);
					return;
				}
			}
			bool commit = false;
			try
			{
				MailboxCleanup.SynchronizeDirectoryAndMailboxTable(context, databaseInfo, directoryMailboxInfo, context.LockedMailboxState, explicitRpcCall);
				commit = true;
			}
			finally
			{
				context.EndMailboxOperation(commit);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00036F68 File Offset: 0x00035168
		public static void SynchronizeDirectoryAndMailboxTable(Context context, MailboxState mailboxState, out bool completed)
		{
			completed = false;
			bool flag = mailboxState.IsUserAccessible || mailboxState.IsDisabled;
			context.EndMailboxOperation(true);
			Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshDatabaseInfo(context, context.Database.MdbGuid);
			DatabaseInfo databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(context, context.Database.MdbGuid);
			MailboxInfo directoryMailboxInfo = null;
			if (flag && !MailboxCleanup.GetMailboxInfoFromAD(context, mailboxState.TenantHint, context.Database.MdbGuid, mailboxState.MailboxGuid, out directoryMailboxInfo))
			{
				if (ExTraceGlobals.SyncMailboxWithDSTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.SyncMailboxWithDSTracer.TraceDebug<Guid>(52956L, "Could not read mailbox info from AD for mailbox {0}.", mailboxState.MailboxGuid);
				}
				DiagnosticContext.TraceLocation((LID)63196U);
				return;
			}
			ErrorCode first = context.StartMailboxOperation();
			if (first != ErrorCode.NoError)
			{
				DiagnosticContext.TraceLocation((LID)41180U);
				return;
			}
			MailboxCleanup.SynchronizeDirectoryAndMailboxTable(context, databaseInfo, directoryMailboxInfo, mailboxState, false);
			completed = true;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00037050 File Offset: 0x00035250
		private static bool GetMailboxInfoFromAD(Context context, TenantHint tenantHint, Guid mdbGuid, Guid mailboxGuid, out MailboxInfo directoryMailboxInfo)
		{
			bool result = false;
			directoryMailboxInfo = null;
			try
			{
				Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.RefreshMailboxInfo(context, mailboxGuid);
				directoryMailboxInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetMailboxInfo(context, tenantHint, mailboxGuid, GetMailboxInfoFlags.IgnoreHomeMdb | GetMailboxInfoFlags.BypassSharedCache);
				if (directoryMailboxInfo.MdbGuid != mdbGuid)
				{
					directoryMailboxInfo = null;
				}
				result = true;
			}
			catch (MailboxNotFoundException exception)
			{
				context.OnExceptionCatch(exception);
				result = true;
			}
			catch (DirectoryTransientErrorException ex)
			{
				context.OnExceptionCatch(ex);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TransientADError, new object[]
				{
					ex
				});
			}
			catch (DirectoryPermanentErrorException ex2)
			{
				context.OnExceptionCatch(ex2);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PermanentADError, new object[]
				{
					ex2
				});
			}
			catch (DirectoryInfoCorruptException ex3)
			{
				context.OnExceptionCatch(ex3);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PermanentADError, new object[]
				{
					ex3
				});
			}
			catch (UnsupportedRecipientTypeException ex4)
			{
				context.OnExceptionCatch(ex4);
				Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_PermanentADError, new object[]
				{
					ex4
				});
			}
			return result;
		}

		// Token: 0x04000153 RID: 339
		public static readonly Guid MarkExpiredDisconnectedMailboxesForSynchronizationWithDSMaintenanceId = new Guid("{285abee5-b82c-4849-ac49-beaf265f3b46}");

		// Token: 0x04000154 RID: 340
		public static readonly Guid MarkIdleUserAccessibleMailboxesForSynchronizationWithDSMaintenanceId = new Guid("{82d947ed-87da-4389-b4fa-af51d947f16e}");

		// Token: 0x04000155 RID: 341
		public static readonly Guid SynchronizeMailboxWithDSMaintenanceId = new Guid("{8b5c9cf4-109d-46b2-a050-d509f4c58e03}");

		// Token: 0x04000156 RID: 342
		private static IDatabaseMaintenance markExpiredDisconnectedMailboxesForSynchronizationWithDS;

		// Token: 0x04000157 RID: 343
		private static IDatabaseMaintenance markIdleUserAccessibleMailboxesForSynchronizationWithDS;

		// Token: 0x0200000E RID: 14
		private class MailboxListRestrictionExpiredDisconnectedMailbox : IMailboxListRestriction
		{
			// Token: 0x06000257 RID: 599 RVA: 0x000371AF File Offset: 0x000353AF
			public MailboxListRestrictionExpiredDisconnectedMailbox(DateTime deletedOnThreshold)
			{
				this.deletedOnThreshold = deletedOnThreshold;
			}

			// Token: 0x06000258 RID: 600 RVA: 0x000371C0 File Offset: 0x000353C0
			public SearchCriteria Filter(Context context)
			{
				MailboxTable mailboxTable = Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseSchema.MailboxTable(context.Database);
				return Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					Factory.CreateSearchCriteriaOr(new SearchCriteria[]
					{
						Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(3)),
						Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(4))
					}),
					Factory.CreateSearchCriteriaCompare(mailboxTable.DeletedOn, SearchCriteriaCompare.SearchRelOp.LessThan, Factory.CreateConstantColumn(this.deletedOnThreshold))
				});
			}

			// Token: 0x06000259 RID: 601 RVA: 0x00037249 File Offset: 0x00035449
			public Index Index(MailboxTable mailboxTable)
			{
				return mailboxTable.MailboxTablePK;
			}

			// Token: 0x04000159 RID: 345
			private readonly DateTime deletedOnThreshold;
		}

		// Token: 0x0200000F RID: 15
		internal class MailboxListRestrictionIdleUserAccessibleMailbox : IMailboxListRestriction
		{
			// Token: 0x0600025A RID: 602 RVA: 0x00037251 File Offset: 0x00035451
			public MailboxListRestrictionIdleUserAccessibleMailbox(DateTime idleTimeThreshold)
			{
				this.idleTimeThreshold = idleTimeThreshold;
			}

			// Token: 0x0600025B RID: 603 RVA: 0x00037260 File Offset: 0x00035460
			public SearchCriteria Filter(Context context)
			{
				MailboxTable mailboxTable = Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseSchema.MailboxTable(context.Database);
				if (AddLastMaintenanceTimeToMailbox.IsReady(context, context.Database))
				{
					return Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
					{
						Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(2)),
						Factory.CreateSearchCriteriaOr(new SearchCriteria[]
						{
							Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
							{
								Factory.CreateSearchCriteriaCompare(mailboxTable.LastMailboxMaintenanceTime, SearchCriteriaCompare.SearchRelOp.NotEqual, Factory.CreateConstantColumn(DateTime.MinValue)),
								Factory.CreateSearchCriteriaCompare(mailboxTable.LastMailboxMaintenanceTime, SearchCriteriaCompare.SearchRelOp.LessThan, Factory.CreateConstantColumn(this.idleTimeThreshold))
							}),
							Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
							{
								Factory.CreateSearchCriteriaCompare(mailboxTable.LastMailboxMaintenanceTime, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(DateTime.MinValue)),
								Factory.CreateSearchCriteriaCompare(mailboxTable.LastLogonTime, SearchCriteriaCompare.SearchRelOp.LessThan, Factory.CreateConstantColumn(this.idleTimeThreshold))
							})
						})
					});
				}
				return Factory.CreateSearchCriteriaAnd(new SearchCriteria[]
				{
					Factory.CreateSearchCriteriaCompare(mailboxTable.Status, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(2)),
					Factory.CreateSearchCriteriaCompare(mailboxTable.LastLogonTime, SearchCriteriaCompare.SearchRelOp.LessThan, Factory.CreateConstantColumn(this.idleTimeThreshold))
				});
			}

			// Token: 0x0600025C RID: 604 RVA: 0x000373AA File Offset: 0x000355AA
			public Index Index(MailboxTable mailboxTable)
			{
				return mailboxTable.MailboxTablePK;
			}

			// Token: 0x0400015A RID: 346
			private readonly DateTime idleTimeThreshold;
		}
	}
}
