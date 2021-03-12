using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001D8 RID: 472
	internal class ADHandler : RequestIndexEntryHandler<MRSRequestWrapper>
	{
		// Token: 0x0600137B RID: 4987 RVA: 0x0002BD1C File Offset: 0x00029F1C
		static ADHandler()
		{
			ADHandler.RelativePublicFolderMailboxMigrationContainer = new ADObjectId("CN=PublicFolderMailboxMigrationRequests");
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		public override MRSRequestWrapper CreateRequestIndexEntryFromRequestJob(RequestJobBase requestJob, IConfigurationSession session)
		{
			return new MRSRequestWrapper(session, requestJob.RequestType, ADHandler.GenerateCommonName(requestJob.Name, requestJob.SourceAlias, requestJob.TargetAlias))
			{
				Name = requestJob.Name,
				RequestGuid = requestJob.RequestGuid,
				Status = requestJob.Status,
				Flags = requestJob.Flags,
				RemoteHostName = requestJob.RemoteHostName,
				BatchName = requestJob.BatchName,
				SourceMDB = requestJob.SourceDatabase,
				TargetMDB = requestJob.TargetDatabase,
				StorageMDB = requestJob.WorkItemQueueMdb,
				FilePath = requestJob.FilePath,
				TargetUserId = requestJob.TargetUserId,
				SourceUserId = requestJob.SourceUserId,
				OrganizationId = requestJob.OrganizationId
			};
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0002BE8E File Offset: 0x0002A08E
		public override void Delete(RequestIndexEntryProvider requestIndexEntryProvider, MRSRequestWrapper instance)
		{
			ADHandler.Delete(requestIndexEntryProvider.ConfigSession, instance);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0002BE9C File Offset: 0x0002A09C
		public override MRSRequestWrapper[] Find(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			return ADHandler.Find(requestIndexEntryProvider.ConfigSession, filter, rootId, deepSearch, sortBy);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public override IEnumerable<MRSRequestWrapper> FindPaged(RequestIndexEntryProvider requestIndexEntryProvider, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			QueryFilter filter2 = ADHandler.ConvertFilter(filter);
			return requestIndexEntryProvider.ConfigSession.FindPaged<MRSRequestWrapper>(filter2, rootId, deepSearch, sortBy, pageSize);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0002BED7 File Offset: 0x0002A0D7
		public override MRSRequestWrapper Read(RequestIndexEntryProvider requestIndexEntryProvider, RequestIndexEntryObjectId identity)
		{
			return ADHandler.Read(requestIndexEntryProvider.ConfigSession, identity.RequestGuid, identity.RequestType);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0002BEF0 File Offset: 0x0002A0F0
		public override void Save(RequestIndexEntryProvider requestIndexEntryProvider, MRSRequestWrapper instance)
		{
			ADHandler.Save(requestIndexEntryProvider.ConfigSession, instance);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0002BEFE File Offset: 0x0002A0FE
		internal static void Delete(IConfigurationSession configSession, MRSRequestWrapper instance)
		{
			configSession.Delete(instance);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0002BF08 File Offset: 0x0002A108
		internal static MRSRequestWrapper[] Find(IConfigurationSession configSession, QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy)
		{
			QueryFilter filter2 = ADHandler.ConvertFilter(filter);
			return (MRSRequestWrapper[])configSession.Find<MRSRequestWrapper>(filter2, rootId, deepSearch, sortBy);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0002BF2C File Offset: 0x0002A12C
		internal static MRSRequestWrapper Read(IConfigurationSession configSession, Guid requestGuid, MRSRequestType type)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveRequestGuid, requestGuid);
			ADObjectId rootId = ADHandler.GetRootId(configSession, type);
			IEnumerable<MRSRequestWrapper> enumerable = configSession.FindPaged<MRSRequestWrapper>(filter, rootId, !configSession.SessionSettings.IsTenantScoped, null, 2);
			MRSRequestWrapper result = null;
			using (IEnumerator<MRSRequestWrapper> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					result = enumerator.Current;
					if (enumerator.MoveNext())
					{
						throw new RequestGuidNotUniquePermanentException(requestGuid.ToString(), type.ToString());
					}
				}
			}
			return result;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0002BFD0 File Offset: 0x0002A1D0
		internal static void Save(IConfigurationSession configSession, MRSRequestWrapper instance)
		{
			try
			{
				configSession.Save(instance);
			}
			catch (ADOperationException)
			{
				bool flag = true;
				ADObjectId parent = instance.Id.Parent;
				ADObjectId parent2 = parent.Parent;
				if (configSession.Read<Container>(parent2) == null)
				{
					Container container = new Container();
					container.SetId(parent2);
					container.OrganizationId = (instance.OrganizationId ?? OrganizationId.ForestWideOrgId);
					configSession.Save(container);
					flag = false;
				}
				Container container2 = null;
				if (flag)
				{
					container2 = configSession.Read<Container>(parent);
				}
				if (container2 == null)
				{
					container2 = new Container();
					container2.SetId(parent);
					container2.OrganizationId = (instance.OrganizationId ?? OrganizationId.ForestWideOrgId);
					configSession.Save(container2);
				}
				configSession.Save(instance);
			}
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0002C090 File Offset: 0x0002A290
		internal static ADObjectId GetRootId(IConfigurationSession configSession, MRSRequestType type)
		{
			if (!configSession.SessionSettings.IsTenantScoped && CommonUtils.IsMultiTenantEnabled())
			{
				return null;
			}
			ADObjectId descendantId = configSession.GetOrgContainerId().GetDescendantId(ADHandler.RelativeMRSContainer);
			return descendantId.GetDescendantId(ADHandler.GetRelativeContainerId(type));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
		internal static ADObjectId GetRelativeContainerId(MRSRequestType type)
		{
			ADObjectId result = null;
			if (type == MRSRequestType.Merge)
			{
				result = ADHandler.RelativeMergeContainer;
			}
			else if (type == MRSRequestType.MailboxImport)
			{
				result = ADHandler.RelativeMailboxImportContainer;
			}
			else if (type == MRSRequestType.MailboxExport)
			{
				result = ADHandler.RelativeMailboxExportContainer;
			}
			else if (type == MRSRequestType.MailboxRestore)
			{
				result = ADHandler.RelativeMailboxRestoreContainer;
			}
			else if (type == MRSRequestType.PublicFolderMove)
			{
				result = ADHandler.RelativePublicFolderMoveContainer;
			}
			else if (type == MRSRequestType.PublicFolderMigration)
			{
				result = ADHandler.RelativePublicFolderMigrationContainer;
			}
			else if (type == MRSRequestType.PublicFolderMailboxMigration)
			{
				result = ADHandler.RelativePublicFolderMailboxMigrationContainer;
			}
			else if (type == MRSRequestType.Sync)
			{
				result = ADHandler.RelativeSyncContainer;
			}
			else if (type == MRSRequestType.FolderMove)
			{
				result = ADHandler.RelativeFolderMoveContainer;
			}
			return result;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0002C150 File Offset: 0x0002A350
		private static QueryFilter ConvertFilter(QueryFilter filter)
		{
			QueryFilter result = null;
			if (filter != null)
			{
				List<QueryFilter> list = new List<QueryFilter>();
				if (!(filter is RequestIndexEntryQueryFilter))
				{
					throw new ArgumentException("This provider only supports RequestIndexEntryQueryFilters", "filter");
				}
				RequestIndexEntryQueryFilter requestIndexEntryQueryFilter = (RequestIndexEntryQueryFilter)filter;
				if (requestIndexEntryQueryFilter.IndexId.Location != RequestIndexLocation.AD)
				{
					throw new ArgumentException("ADHandler only supports objects in the location \"AD\".");
				}
				if (requestIndexEntryQueryFilter.RequestGuid != Guid.Empty)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveRequestGuid, requestIndexEntryQueryFilter.RequestGuid));
				}
				if (requestIndexEntryQueryFilter.RequestQueueId != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveStorageMDB, requestIndexEntryQueryFilter.RequestQueueId));
				}
				if (requestIndexEntryQueryFilter.RequestType != MRSRequestType.Move)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MRSRequestType, requestIndexEntryQueryFilter.RequestType));
				}
				if (requestIndexEntryQueryFilter.RequestName != null)
				{
					if (requestIndexEntryQueryFilter.RequestName.Equals(string.Empty))
					{
						list.Add(new NotFilter(new ExistsFilter(MRSRequestSchema.DisplayName)));
					}
					else if (requestIndexEntryQueryFilter.WildcardedNameSearch)
					{
						list.Add(new TextFilter(MRSRequestSchema.DisplayName, requestIndexEntryQueryFilter.RequestName, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
					}
					else
					{
						list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.DisplayName, requestIndexEntryQueryFilter.RequestName));
					}
				}
				if (requestIndexEntryQueryFilter.MailboxId != null)
				{
					if (requestIndexEntryQueryFilter.LooseMailboxSearch)
					{
						list.Add(QueryFilter.OrTogether(new QueryFilter[]
						{
							new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveSourceUser, requestIndexEntryQueryFilter.MailboxId),
							new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveTargetUser, requestIndexEntryQueryFilter.MailboxId)
						}));
					}
					else
					{
						list.Add(QueryFilter.OrTogether(new QueryFilter[]
						{
							QueryFilter.AndTogether(new QueryFilter[]
							{
								new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveSourceUser, requestIndexEntryQueryFilter.MailboxId),
								new BitMaskAndFilter(MRSRequestSchema.MailboxMoveFlags, 4UL)
							}),
							QueryFilter.AndTogether(new QueryFilter[]
							{
								new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveTargetUser, requestIndexEntryQueryFilter.MailboxId),
								new BitMaskAndFilter(MRSRequestSchema.MailboxMoveFlags, 8UL)
							})
						}));
					}
				}
				if (requestIndexEntryQueryFilter.DBId != null)
				{
					if (requestIndexEntryQueryFilter.LooseMailboxSearch)
					{
						list.Add(QueryFilter.OrTogether(new QueryFilter[]
						{
							new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveSourceMDB, requestIndexEntryQueryFilter.DBId),
							new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveTargetMDB, requestIndexEntryQueryFilter.DBId)
						}));
					}
					else
					{
						list.Add(QueryFilter.OrTogether(new QueryFilter[]
						{
							QueryFilter.AndTogether(new QueryFilter[]
							{
								new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveSourceMDB, requestIndexEntryQueryFilter.DBId),
								new BitMaskAndFilter(MRSRequestSchema.MailboxMoveFlags, 4UL)
							}),
							QueryFilter.AndTogether(new QueryFilter[]
							{
								new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveTargetMDB, requestIndexEntryQueryFilter.DBId),
								new BitMaskAndFilter(MRSRequestSchema.MailboxMoveFlags, 8UL)
							})
						}));
					}
				}
				if (requestIndexEntryQueryFilter.BatchName != null)
				{
					if (requestIndexEntryQueryFilter.BatchName.Equals(string.Empty))
					{
						list.Add(new NotFilter(new ExistsFilter(MRSRequestSchema.MailboxMoveBatchName)));
					}
					else
					{
						list.Add(new TextFilter(MRSRequestSchema.MailboxMoveBatchName, requestIndexEntryQueryFilter.BatchName, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
					}
				}
				if (requestIndexEntryQueryFilter.SourceMailbox != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveSourceUser, requestIndexEntryQueryFilter.SourceMailbox));
				}
				if (requestIndexEntryQueryFilter.TargetMailbox != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveTargetUser, requestIndexEntryQueryFilter.TargetMailbox));
				}
				if (requestIndexEntryQueryFilter.SourceDatabase != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveSourceMDB, requestIndexEntryQueryFilter.SourceDatabase));
				}
				if (requestIndexEntryQueryFilter.TargetDatabase != null)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveTargetMDB, requestIndexEntryQueryFilter.TargetDatabase));
				}
				if (requestIndexEntryQueryFilter.Status != RequestStatus.None)
				{
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MRSRequestSchema.MailboxMoveStatus, requestIndexEntryQueryFilter.Status));
				}
				if (requestIndexEntryQueryFilter.Flags != RequestFlags.None)
				{
					list.Add(new BitMaskAndFilter(MRSRequestSchema.MailboxMoveFlags, (ulong)((long)requestIndexEntryQueryFilter.Flags)));
				}
				if (requestIndexEntryQueryFilter.NotFlags != RequestFlags.None)
				{
					list.Add(new NotFilter(new BitMaskAndFilter(MRSRequestSchema.MailboxMoveFlags, (ulong)((long)requestIndexEntryQueryFilter.NotFlags))));
				}
				if (list.Count > 1)
				{
					result = QueryFilter.AndTogether(list.ToArray());
				}
				else if (list.Count == 1)
				{
					result = list[0];
				}
			}
			return result;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0002C58C File Offset: 0x0002A78C
		private static string GenerateCommonName(string displayName, string sourceAlias, string targetAlias)
		{
			string text;
			if (!string.IsNullOrEmpty(sourceAlias) && !string.IsNullOrEmpty(targetAlias))
			{
				text = string.Format("{0}.{1}-{2}", sourceAlias, targetAlias, displayName);
			}
			else if (!string.IsNullOrEmpty(sourceAlias))
			{
				text = string.Format("{0}-{1}", sourceAlias, displayName);
			}
			else if (!string.IsNullOrEmpty(targetAlias))
			{
				text = string.Format("{0}-{1}", targetAlias, displayName);
			}
			else
			{
				text = string.Format("{0}", displayName);
			}
			if (text.Length > 32)
			{
				text = text.Substring(text.Length - 32, 32);
			}
			return string.Format("{0}{1}", Guid.NewGuid().ToString("n"), text).Trim();
		}

		// Token: 0x04000A02 RID: 2562
		private static readonly ADObjectId RelativeMRSContainer = new ADObjectId("CN=Mailbox Replication");

		// Token: 0x04000A03 RID: 2563
		private static readonly ADObjectId RelativeMergeContainer = new ADObjectId("CN=MergeRequests");

		// Token: 0x04000A04 RID: 2564
		private static readonly ADObjectId RelativeMailboxImportContainer = new ADObjectId("CN=MailboxImportRequests");

		// Token: 0x04000A05 RID: 2565
		private static readonly ADObjectId RelativeMailboxExportContainer = new ADObjectId("CN=MailboxExportRequests");

		// Token: 0x04000A06 RID: 2566
		private static readonly ADObjectId RelativeMailboxRestoreContainer = new ADObjectId("CN=MailboxRestoreRequests");

		// Token: 0x04000A07 RID: 2567
		private static readonly ADObjectId RelativePublicFolderMoveContainer = new ADObjectId("CN=PublicFolderMoveRequests");

		// Token: 0x04000A08 RID: 2568
		private static readonly ADObjectId RelativePublicFolderMigrationContainer = new ADObjectId("CN=PublicFolderMigrationRequests");

		// Token: 0x04000A09 RID: 2569
		private static readonly ADObjectId RelativePublicFolderMailboxMigrationContainer;

		// Token: 0x04000A0A RID: 2570
		private static readonly ADObjectId RelativeSyncContainer = new ADObjectId("CN=SyncRequests");

		// Token: 0x04000A0B RID: 2571
		private static readonly ADObjectId RelativeFolderMoveContainer = new ADObjectId("CN=FolderMoveRequests");
	}
}
