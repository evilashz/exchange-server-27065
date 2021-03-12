using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000062 RID: 98
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MapiAclTableAdapter
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x000386FA File Offset: 0x000368FA
		internal MapiAclTableAdapter(IModifyTable modifyTable)
		{
			Util.ThrowOnNullArgument(modifyTable, "modifyTable");
			this.modifyTable = modifyTable;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00038714 File Offset: 0x00036914
		internal StoreSession Session
		{
			get
			{
				return this.modifyTable.Session;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00038724 File Offset: 0x00036924
		internal AclTableEntry[] GetAll()
		{
			List<AclTableEntry> list = new List<AclTableEntry>();
			using (IQueryResult queryResult = this.modifyTable.GetQueryResult(null, MapiAclTableAdapter.PropertiesToRead))
			{
				bool flag;
				do
				{
					object[][] rows = queryResult.GetRows(int.MaxValue, out flag);
					foreach (object[] row in rows)
					{
						list.Add(MapiAclTableAdapter.LoadFromRawData(row));
					}
				}
				while (flag);
			}
			this.allEntriesCached = list.ToArray();
			return list.ToArray();
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000387B4 File Offset: 0x000369B4
		internal AclTableEntry GetByMemberId(long memberId)
		{
			if (this.allEntriesCached == null)
			{
				this.GetAll();
			}
			foreach (AclTableEntry aclTableEntry in this.allEntriesCached)
			{
				if (aclTableEntry.MemberId == memberId)
				{
					return aclTableEntry;
				}
			}
			return null;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000387F9 File Offset: 0x000369F9
		internal ADParticipantEntryId TryGetParticipantEntryId(byte[] memberEntryId)
		{
			if (memberEntryId != null)
			{
				return ParticipantEntryId.TryFromEntryId(memberEntryId) as ADParticipantEntryId;
			}
			return null;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0003880C File Offset: 0x00036A0C
		internal ExternalUser TryGetExternalUser(byte[] memberEntryId, ref ExternalUserCollection externalUsers)
		{
			if (memberEntryId != null)
			{
				MailboxSession mailboxSession = this.Session as MailboxSession;
				if (externalUsers == null && mailboxSession != null)
				{
					externalUsers = mailboxSession.GetExternalUsers();
				}
				if (externalUsers != null)
				{
					try
					{
						byte[] binaryForm = null;
						StoreSession session = this.Session;
						bool flag = false;
						try
						{
							if (session != null)
							{
								session.BeginMapiCall();
								session.BeginServerHealthCall();
								flag = true;
							}
							if (StorageGlobals.MapiTestHookBeforeCall != null)
							{
								StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
							}
							binaryForm = MapiStore.GetLocalDirectorySIDFromAddressBookEntryId(memberEntryId);
						}
						catch (MapiPermanentException ex)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.InvalidPermissionsEntry, ex, session, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("ACL table has invalid entry id.", new object[0]),
								ex
							});
						}
						catch (MapiRetryableException ex2)
						{
							throw StorageGlobals.TranslateMapiException(ServerStrings.InvalidPermissionsEntry, ex2, session, this, "{0}. MapiException = {1}.", new object[]
							{
								string.Format("ACL table has invalid entry id.", new object[0]),
								ex2
							});
						}
						finally
						{
							try
							{
								if (session != null)
								{
									session.EndMapiCall();
									if (flag)
									{
										session.EndServerHealthCall();
									}
								}
							}
							finally
							{
								if (StorageGlobals.MapiTestHookAfterCall != null)
								{
									StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
								}
							}
						}
						SecurityIdentifier sid = new SecurityIdentifier(binaryForm, 0);
						return externalUsers.FindExternalUser(sid);
					}
					catch (ObjectNotFoundException)
					{
					}
				}
			}
			return null;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00038980 File Offset: 0x00036B80
		internal void AddPermissionEntry(byte[] memberEntryId, MemberRights memberRights)
		{
			this.modifyTable.AddRow(new PropValue[]
			{
				new PropValue(PermissionSchema.MemberEntryId, memberEntryId),
				new PropValue(PermissionSchema.MemberRights, memberRights)
			});
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000389D4 File Offset: 0x00036BD4
		internal void ModifyPermissionEntry(long memberId, MemberRights memberRights)
		{
			this.modifyTable.ModifyRow(new PropValue[]
			{
				new PropValue(PermissionSchema.MemberId, memberId),
				new PropValue(PermissionSchema.MemberRights, memberRights)
			});
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00038A2C File Offset: 0x00036C2C
		internal void RemovePermissionEntry(long memberId)
		{
			this.modifyTable.RemoveRow(new PropValue[]
			{
				new PropValue(PermissionSchema.MemberId, memberId)
			});
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00038A68 File Offset: 0x00036C68
		internal void ApplyPendingChanges(bool suppressRestriction)
		{
			if (suppressRestriction)
			{
				this.modifyTable.SuppressRestriction();
			}
			this.modifyTable.ApplyPendingChanges();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00038A84 File Offset: 0x00036C84
		private static AclTableEntry LoadFromRawData(object[] row)
		{
			return new AclTableEntry(PropertyBag.CheckPropertyValue<long>(InternalSchema.MemberId, row[0]), PropertyBag.CheckPropertyValue<byte[]>(InternalSchema.MemberEntryId, row[1], null), PropertyBag.CheckPropertyValue<string>(InternalSchema.MemberName, row[2], string.Empty), PropertyBag.CheckPropertyValue<MemberRights>(InternalSchema.MemberRights, row[3], MemberRights.None));
		}

		// Token: 0x040001EE RID: 494
		private static readonly PropertyDefinition[] PropertiesToRead = new PropertyDefinition[]
		{
			InternalSchema.MemberId,
			InternalSchema.MemberEntryId,
			InternalSchema.MemberName,
			InternalSchema.MemberRights
		};

		// Token: 0x040001EF RID: 495
		private readonly IModifyTable modifyTable;

		// Token: 0x040001F0 RID: 496
		private AclTableEntry[] allEntriesCached;

		// Token: 0x02000063 RID: 99
		private enum PropertiesToReadIndex
		{
			// Token: 0x040001F2 RID: 498
			MemberId,
			// Token: 0x040001F3 RID: 499
			MemberEntryId,
			// Token: 0x040001F4 RID: 500
			MemberName,
			// Token: 0x040001F5 RID: 501
			MemberRights
		}
	}
}
