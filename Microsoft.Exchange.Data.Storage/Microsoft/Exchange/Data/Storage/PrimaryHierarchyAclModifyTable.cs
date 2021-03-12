using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000944 RID: 2372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PrimaryHierarchyAclModifyTable : DisposableObject, IModifyTable, IDisposable
	{
		// Token: 0x06005850 RID: 22608 RVA: 0x0016B33D File Offset: 0x0016953D
		public PrimaryHierarchyAclModifyTable(RPCPrimaryHierarchyProvider primaryHierarchyProvider, CoreFolder coreFolder, IModifyTable permissionsTable, ModifyTableOptions options)
		{
			this.coreFolder = coreFolder;
			this.options = options;
			this.primaryHierarchyProvider = primaryHierarchyProvider;
			this.currentPermissionsTable = permissionsTable;
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x0016B36D File Offset: 0x0016956D
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PrimaryHierarchyAclModifyTable>(this);
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x0016B375 File Offset: 0x00169575
		public void Clear()
		{
			this.CheckDisposed(null);
			this.replaceAllRows = true;
			this.pendingModifyOperations.Clear();
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x0016B390 File Offset: 0x00169590
		public void AddRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingChange(ModifyTableOperationType.Add, propValues);
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x0016B3A1 File Offset: 0x001695A1
		public void ModifyRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingChange(ModifyTableOperationType.Modify, propValues);
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x0016B3B2 File Offset: 0x001695B2
		public void RemoveRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingChange(ModifyTableOperationType.Remove, propValues);
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x0016B3C3 File Offset: 0x001695C3
		public IQueryResult GetQueryResult(QueryFilter queryFilter, ICollection<PropertyDefinition> columns)
		{
			throw new NotSupportedException("GetQueryResult not supported on PrimaryHierarchyAclModifyTable");
		}

		// Token: 0x06005857 RID: 22615 RVA: 0x0016B3D8 File Offset: 0x001695D8
		public void ApplyPendingChanges()
		{
			this.CheckDisposed(null);
			AclTableEntry.ModifyOperation[] array = (from op in this.pendingModifyOperations
			select AclTableEntry.ModifyOperation.FromModifyTableOperation(op)).ToArray<AclTableEntry.ModifyOperation>();
			using (IQueryResult queryResult = this.currentPermissionsTable.GetQueryResult(null, new PropertyDefinition[]
			{
				PermissionSchema.MemberId,
				PermissionSchema.MemberEntryId
			}))
			{
				bool flag;
				object[][] rows = queryResult.GetRows(queryResult.EstimatedRowCount, out flag);
				for (int i = 0; i < array.Length; i++)
				{
					AclTableEntry.ModifyOperation modifyOperation = array[i];
					switch (modifyOperation.Operation)
					{
					case ModifyTableOperationType.Modify:
					case ModifyTableOperationType.Remove:
						if (modifyOperation.Entry.MemberId != -1L && modifyOperation.Entry.MemberId != 0L)
						{
							array[i] = new AclTableEntry.ModifyOperation(array[i].Operation, new AclTableEntry(array[i].Entry.MemberId, PrimaryHierarchyAclModifyTable.GetMemberEntryId(modifyOperation.Entry.MemberId, rows), array[i].Entry.MemberName, array[i].Entry.MemberRights));
						}
						break;
					}
				}
			}
			this.primaryHierarchyProvider.ModifyPermissions(this.coreFolder.Id, array, this.options, this.replaceAllRows);
			this.replaceAllRows = false;
			this.pendingModifyOperations.Clear();
		}

		// Token: 0x06005858 RID: 22616 RVA: 0x0016B554 File Offset: 0x00169754
		public void SuppressRestriction()
		{
			throw new NotSupportedException("SuppressRestriction not supported on PrimaryHierarchyAclModifyTable");
		}

		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x06005859 RID: 22617 RVA: 0x0016B560 File Offset: 0x00169760
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed(null);
				return this.coreFolder.Session;
			}
		}

		// Token: 0x0600585A RID: 22618 RVA: 0x0016B574 File Offset: 0x00169774
		private static byte[] GetMemberEntryId(long memberId, object[][] permissionRows)
		{
			foreach (object[] array in permissionRows)
			{
				if ((long)array[0] == memberId)
				{
					return (byte[])array[1];
				}
			}
			throw new ObjectNotFoundException(new LocalizedString(string.Format("Missing AclTableEntry with MemberId - {0}", memberId)));
		}

		// Token: 0x0600585B RID: 22619 RVA: 0x0016B5C7 File Offset: 0x001697C7
		private void AddPendingChange(ModifyTableOperationType operation, PropValue[] propValues)
		{
			this.pendingModifyOperations.Add(new ModifyTableOperation(operation, propValues));
		}

		// Token: 0x04003019 RID: 12313
		private readonly RPCPrimaryHierarchyProvider primaryHierarchyProvider;

		// Token: 0x0400301A RID: 12314
		private readonly IModifyTable currentPermissionsTable;

		// Token: 0x0400301B RID: 12315
		private readonly CoreFolder coreFolder;

		// Token: 0x0400301C RID: 12316
		private readonly ModifyTableOptions options;

		// Token: 0x0400301D RID: 12317
		private readonly List<ModifyTableOperation> pendingModifyOperations = new List<ModifyTableOperation>();

		// Token: 0x0400301E RID: 12318
		private bool replaceAllRows;
	}
}
