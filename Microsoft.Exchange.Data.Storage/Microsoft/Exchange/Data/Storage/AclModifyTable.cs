using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000799 RID: 1945
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AclModifyTable : DisposableObject, IModifyTable, IDisposable
	{
		// Token: 0x0600492F RID: 18735 RVA: 0x00132245 File Offset: 0x00130445
		internal AclModifyTable(CoreFolder coreFolder, ModifyTableOptions options, IModifyTableRestriction modifyTableRestriction, bool useSecurityDescriptorOnly) : this(coreFolder, options, modifyTableRestriction, useSecurityDescriptorOnly, true)
		{
		}

		// Token: 0x06004930 RID: 18736 RVA: 0x00132254 File Offset: 0x00130454
		internal AclModifyTable(CoreFolder coreFolder, ModifyTableOptions options, IModifyTableRestriction modifyTableRestriction, bool useSecurityDescriptorOnly, bool loadTableEntries)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.coreFolder = coreFolder;
				this.options = options;
				this.modifyTableRestriction = modifyTableRestriction;
				this.recipientSession = coreFolder.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
				this.useSecurityDescriptorOnly = useSecurityDescriptorOnly;
				if (loadTableEntries)
				{
					this.Load();
				}
				else
				{
					this.replaceAllRows = true;
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06004931 RID: 18737 RVA: 0x001322F0 File Offset: 0x001304F0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AclModifyTable>(this);
		}

		// Token: 0x06004932 RID: 18738 RVA: 0x001322F8 File Offset: 0x001304F8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				Util.DisposeIfPresent(this.externalUsers);
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004933 RID: 18739 RVA: 0x00132360 File Offset: 0x00130560
		public static byte[] BuildAclTableBlob(StoreSession session, RawSecurityDescriptor securityDescriptor, RawSecurityDescriptor freeBusySecurityDescriptor)
		{
			IRecipientSession adrecipientSession = session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
			ExternalUserCollection externalUserCollectionToDispose = null;
			bool flag;
			string canonicalErrorInformation;
			List<AclTableEntry> source;
			try
			{
				source = AclModifyTable.BuildAclTableFromSecurityDescriptor(securityDescriptor, freeBusySecurityDescriptor, new LazilyInitialized<ExternalUserCollection>(delegate()
				{
					MailboxSession mailboxSession = session as MailboxSession;
					externalUserCollectionToDispose = ((mailboxSession != null) ? mailboxSession.GetExternalUsers() : null);
					return externalUserCollectionToDispose;
				}), adrecipientSession, new AclTableIdMap(), out flag, out canonicalErrorInformation);
			}
			finally
			{
				Util.DisposeIfPresent(externalUserCollectionToDispose);
			}
			if (!flag)
			{
				ExTraceGlobals.StorageTracer.TraceError(0L, "Cannot build blob ACL table blob with non-canonical SD");
				throw new NonCanonicalACLException(canonicalErrorInformation);
			}
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSD = new FolderSecurity.AclTableAndSecurityDescriptorProperty(new ArraySegment<byte>(AclModifyTable.SerializeTableEntries(source)), source.ToDictionary((AclTableEntry tableEntry) => tableEntry.SecurityIdentifier, delegate(AclTableEntry tableEntry)
			{
				if (!tableEntry.IsGroup)
				{
					return FolderSecurity.SecurityIdentifierType.User;
				}
				return FolderSecurity.SecurityIdentifierType.Group;
			}), SecurityDescriptor.FromRawSecurityDescriptor(securityDescriptor), SecurityDescriptor.FromRawSecurityDescriptor(freeBusySecurityDescriptor));
			return AclModifyTable.SerializeAclTableAndSecurityDecscriptor(aclTableAndSD);
		}

		// Token: 0x06004934 RID: 18740 RVA: 0x00132464 File Offset: 0x00130664
		public static FolderSecurity.AclTableAndSecurityDescriptorProperty ReadAclTableAndSecurityDescriptor(ICorePropertyBag propertyBag)
		{
			byte[] buffer;
			using (Stream stream = propertyBag.OpenPropertyStream(CoreFolderSchema.AclTableAndSecurityDescriptor, PropertyOpenMode.ReadOnly))
			{
				FolderPropertyStream stream2 = stream as FolderPropertyStream;
				buffer = Util.StreamHandler.ReadBytesFromStream(stream2);
			}
			return FolderSecurity.AclTableAndSecurityDescriptorProperty.Parse(buffer);
		}

		// Token: 0x06004935 RID: 18741 RVA: 0x001324B0 File Offset: 0x001306B0
		public static void SetFolderSecurityDescriptor(CoreFolder folder, RawSecurityDescriptor securityDescriptor)
		{
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclModifyTable.ReadAclTableAndSecurityDescriptor(folder.PropertyBag);
			byte[] propertyToSet = AclModifyTable.BuildAclTableBlob(folder.Session, securityDescriptor, (aclTableAndSecurityDescriptorProperty.FreeBusySecurityDescriptor != null) ? aclTableAndSecurityDescriptorProperty.FreeBusySecurityDescriptor.ToRawSecurityDescriptorThrow() : null);
			AclModifyTable.WriteFolderAclTable(folder, propertyToSet);
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x001324F4 File Offset: 0x001306F4
		public static void SetFolderFreeBusySecurityDescriptor(CoreFolder folder, RawSecurityDescriptor freeBusySecurityDescriptor)
		{
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclModifyTable.ReadAclTableAndSecurityDescriptor(folder.PropertyBag);
			byte[] propertyToSet = AclModifyTable.BuildAclTableBlob(folder.Session, (aclTableAndSecurityDescriptorProperty.SecurityDescriptor != null) ? aclTableAndSecurityDescriptorProperty.SecurityDescriptor.ToRawSecurityDescriptorThrow() : null, freeBusySecurityDescriptor);
			AclModifyTable.WriteFolderAclTable(folder, propertyToSet);
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x00132537 File Offset: 0x00130737
		public void Clear()
		{
			this.CheckDisposed(null);
			this.replaceAllRows = true;
			this.pendingModifyOperations.Clear();
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x00132552 File Offset: 0x00130752
		public void AddRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingChange(ModifyTableOperationType.Add, propValues);
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x00132563 File Offset: 0x00130763
		public void ModifyRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingChange(ModifyTableOperationType.Modify, propValues);
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x00132574 File Offset: 0x00130774
		public void RemoveRow(params PropValue[] propValues)
		{
			this.CheckDisposed(null);
			this.AddPendingChange(ModifyTableOperationType.Remove, propValues);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x00132585 File Offset: 0x00130785
		public IQueryResult GetQueryResult(QueryFilter queryFilter, ICollection<PropertyDefinition> columns)
		{
			this.CheckDisposed(null);
			return new AclQueryResult(this.Session, this.tableEntries, (this.options & ModifyTableOptions.ExtendedPermissionInformation) != ModifyTableOptions.None, (this.options & ModifyTableOptions.FreeBusyAware) == ModifyTableOptions.None, columns);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x00132628 File Offset: 0x00130828
		public void ApplyPendingChanges()
		{
			this.CheckDisposed(null);
			bool flag = (this.options & ModifyTableOptions.ExtendedPermissionInformation) == ModifyTableOptions.ExtendedPermissionInformation;
			IRecipientSession recipientSession = this.recipientSession;
			if (flag)
			{
				this.recipientSession = null;
			}
			try
			{
				if (!this.propertyTableRestrictionSuppressed && this.modifyTableRestriction != null)
				{
					this.modifyTableRestriction.Enforce(this, this.pendingModifyOperations);
				}
				if (this.replaceAllRows)
				{
					this.tableEntries.Clear();
				}
				List<long> list = new List<long>(1);
				AclTableEntry.ModifyOperation[] array = (from op in this.pendingModifyOperations
				select AclTableEntry.ModifyOperation.FromModifyTableOperation(op)).ToArray<AclTableEntry.ModifyOperation>();
				for (int i = 0; i < array.Length; i++)
				{
					AclTableEntry.ModifyOperation aclModifyOperation = array[i];
					if ((aclModifyOperation.Entry.MemberRights & (MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed)) != MemberRights.None && (this.options & ModifyTableOptions.FreeBusyAware) != ModifyTableOptions.FreeBusyAware)
					{
						throw new InvalidParamException(new LocalizedString("F/B unaware clients sent F/B rights"));
					}
					switch (aclModifyOperation.Operation)
					{
					case ModifyTableOperationType.Add:
					{
						SecurityIdentifier securityIdentifier = null;
						List<SecurityIdentifier> sidHistory = null;
						bool flag2 = false;
						string memberName = null;
						string arg;
						if (flag)
						{
							arg = AclHelper.LegacyDnFromEntryId(aclModifyOperation.Entry.MemberEntryId);
							bool flag3 = false;
							bool flag4 = false;
							bool flag5 = false;
							foreach (PropValue propValue in this.pendingModifyOperations[i].Properties)
							{
								if (propValue.Property == PermissionSchema.MemberIsGroup)
								{
									flag3 = true;
									flag2 = (bool)propValue.Value;
								}
								else if (propValue.Property == PermissionSchema.MemberSecurityIdentifier)
								{
									flag5 = true;
									securityIdentifier = new SecurityIdentifier((byte[])propValue.Value, 0);
								}
								else if (propValue.Property == PermissionSchema.MemberName)
								{
									flag4 = true;
									memberName = (string)propValue.Value;
								}
							}
							if (!flag3 || !flag4 || !flag5)
							{
								throw new InvalidOperationException(string.Format("Required property is missing. IsGroupFound={0}, DisplayNameFound={1}, SecurityIdentifierFound={2}", flag3, flag4, flag5));
							}
						}
						else if (!AclHelper.TryGetUserFromEntryId(aclModifyOperation.Entry.MemberEntryId, this.Session, this.recipientSession, new LazilyInitialized<ExternalUserCollection>(() => this.GetExternalUsers(this.Session)), out arg, out securityIdentifier, out sidHistory, out flag2, out memberName))
						{
							ExTraceGlobals.StorageTracer.TraceWarning<string>(0L, "Cannot find recipient for LegDN {0}, skip this entry", arg);
							break;
						}
						aclModifyOperation.Entry.SetSecurityIdentifier(securityIdentifier, flag2);
						aclModifyOperation.Entry.SetMemberId(AclModifyTable.GetIdForSecurityIdentifier(securityIdentifier, sidHistory, this.coreFolder.AclTableIdMap));
						aclModifyOperation.Entry.SetMemberName(memberName);
						int num = this.tableEntries.FindIndex((AclTableEntry aclTableEntry) => aclTableEntry.MemberId == aclModifyOperation.Entry.MemberId);
						if (num != -1)
						{
							this.tableEntries.RemoveAt(num);
						}
						this.FixRightsIfNeeded(aclModifyOperation.Entry);
						if (flag2)
						{
							this.tableEntries.Add(aclModifyOperation.Entry);
						}
						else if (this.tableEntries.Count == 0 || this.tableEntries[0].MemberId != 0L)
						{
							this.tableEntries.Insert(0, aclModifyOperation.Entry);
						}
						else
						{
							this.tableEntries.Insert(1, aclModifyOperation.Entry);
						}
						break;
					}
					case ModifyTableOperationType.Modify:
					{
						if (this.replaceAllRows && aclModifyOperation.Entry.MemberId != -1L && aclModifyOperation.Entry.MemberId != 0L)
						{
							throw new InvalidParamException(new LocalizedString("Modify with ReplaceAllRows"));
						}
						AclTableEntry aclTableEntry2 = this.tableEntries.Find((AclTableEntry aclTableEntry) => aclTableEntry.MemberId == aclModifyOperation.Entry.MemberId);
						if (aclTableEntry2 == null)
						{
							if (aclModifyOperation.Entry.MemberId == -1L)
							{
								aclTableEntry2 = AclModifyTable.BuildAnonymousDefaultEntry();
								this.tableEntries.Add(aclTableEntry2);
							}
							else
							{
								if (aclModifyOperation.Entry.MemberId != 0L)
								{
									throw new ObjectNotFoundException(new LocalizedString("AclTableEntry not found"));
								}
								aclTableEntry2 = AclModifyTable.BuildEveryoneDefaultEntry(MemberRights.FreeBusySimple);
								this.tableEntries.Add(aclTableEntry2);
							}
						}
						this.FixRightsIfNeeded(aclModifyOperation.Entry);
						aclTableEntry2.MemberRights = aclModifyOperation.Entry.MemberRights;
						break;
					}
					case ModifyTableOperationType.Remove:
					{
						if (this.replaceAllRows)
						{
							throw new InvalidParamException(new LocalizedString("Remove with ReplaceAllRows"));
						}
						bool flag6 = false;
						for (int k = i + 1; k < array.Length; k++)
						{
							if (array[k].Operation == ModifyTableOperationType.Modify && aclModifyOperation.Entry.MemberId == array[k].Entry.MemberId)
							{
								flag6 = true;
							}
						}
						if (!flag6)
						{
							int num2 = this.tableEntries.FindIndex((AclTableEntry aclTableEntry) => aclTableEntry.MemberId == aclModifyOperation.Entry.MemberId);
							if (num2 == -1)
							{
								if (!list.Contains(aclModifyOperation.Entry.MemberId))
								{
									throw new ObjectNotFoundException(new LocalizedString("AclTableEntry not found"));
								}
							}
							else
							{
								list.Add(aclModifyOperation.Entry.MemberId);
								this.tableEntries.RemoveAt(num2);
							}
						}
						break;
					}
					}
				}
				this.replaceAllRows = false;
				this.pendingModifyOperations.Clear();
				this.Save();
			}
			finally
			{
				this.recipientSession = recipientSession;
			}
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x00132BE4 File Offset: 0x00130DE4
		public void SuppressRestriction()
		{
			this.CheckDisposed(null);
			this.propertyTableRestrictionSuppressed = true;
		}

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x00132BF4 File Offset: 0x00130DF4
		public StoreSession Session
		{
			get
			{
				this.CheckDisposed(null);
				return this.coreFolder.Session;
			}
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x00132C08 File Offset: 0x00130E08
		private static FolderSecurity.SecurityIdentifierType GetSecurityIdentifierType(IRecipientSession recipientSession, SecurityIdentifier securityIdentifier)
		{
			if (ExternalUser.IsExternalUserSid(securityIdentifier))
			{
				return FolderSecurity.SecurityIdentifierType.User;
			}
			ADRecipient adrecipient = recipientSession.FindBySid(securityIdentifier);
			if (adrecipient == null)
			{
				return FolderSecurity.SecurityIdentifierType.Unknown;
			}
			if (!AclHelper.IsGroupRecipientType(adrecipient.RecipientType))
			{
				return FolderSecurity.SecurityIdentifierType.User;
			}
			return FolderSecurity.SecurityIdentifierType.Group;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00132C3C File Offset: 0x00130E3C
		private static AclTableEntry BuildEveryoneDefaultEntry(MemberRights rights)
		{
			AclTableEntry aclTableEntry = new AclTableEntry(0L, Array<byte>.Empty, string.Empty, rights);
			aclTableEntry.SetSecurityIdentifier(new SecurityIdentifier(WellKnownSidType.WorldSid, null), false);
			return aclTableEntry;
		}

		// Token: 0x06004941 RID: 18753 RVA: 0x00132C6C File Offset: 0x00130E6C
		private static AclTableEntry BuildAnonymousDefaultEntry()
		{
			AclTableEntry aclTableEntry = new AclTableEntry(-1L, Array<byte>.Empty, "Anonymous", MemberRights.None);
			aclTableEntry.SetSecurityIdentifier(new SecurityIdentifier(WellKnownSidType.AnonymousSid, null), false);
			return aclTableEntry;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x00132C9C File Offset: 0x00130E9C
		private static byte[] SerializeTableEntries(List<AclTableEntry> tableEntries)
		{
			byte[] result;
			using (BinarySerializer binarySerializer = new BinarySerializer())
			{
				FolderSecurity.AclTableEntry.SerializeTableEntries<AclTableEntry>(tableEntries, binarySerializer.Writer);
				result = binarySerializer.ToArray();
			}
			return result;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x00132CE0 File Offset: 0x00130EE0
		private static long GetIdForSecurityIdentifier(SecurityIdentifier securityIdentifier, List<SecurityIdentifier> sidHistory, AclTableIdMap aclTableIdMap)
		{
			if (securityIdentifier.IsWellKnown(WellKnownSidType.WorldSid))
			{
				return 0L;
			}
			if (securityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
			{
				return -1L;
			}
			return aclTableIdMap.GetIdForSecurityIdentifier(securityIdentifier, sidHistory);
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x00132D50 File Offset: 0x00130F50
		private static List<AclTableEntry> BuildAclTableFromSecurityDescriptor(RawSecurityDescriptor securityDescriptor, RawSecurityDescriptor freeBusySecurityDescriptor, LazilyInitialized<ExternalUserCollection> lazilyInitializedExternalUserCollection, IRecipientSession recipientSession, AclTableIdMap aclTableIdMap, out bool isCanonical, out string canonicalErrorInformation)
		{
			FolderSecurity.AnnotatedAceList annotatedAceList = new FolderSecurity.AnnotatedAceList(securityDescriptor, freeBusySecurityDescriptor, (SecurityIdentifier securityIdentifier) => AclModifyTable.GetSecurityIdentifierType(recipientSession, securityIdentifier));
			isCanonical = annotatedAceList.IsCanonical(out canonicalErrorInformation);
			IList<FolderSecurity.SecurityIdentifierAndFolderRights> list;
			if (isCanonical)
			{
				list = annotatedAceList.GetSecurityIdentifierAndRightsList();
			}
			else
			{
				ExTraceGlobals.StorageTracer.TraceWarning<string, string>(0L, "Got non canonical SD: {0}, ErrorInfo: ", securityDescriptor.GetSddlForm(AccessControlSections.All), canonicalErrorInformation);
				list = Array<FolderSecurity.SecurityIdentifierAndFolderRights>.Empty;
			}
			List<AclTableEntry> list2 = new List<AclTableEntry>(list.Count + 1);
			foreach (FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights in list)
			{
				MemberRights memberRights = (MemberRights)(securityIdentifierAndFolderRights.AllowRights & ~(MemberRights)securityIdentifierAndFolderRights.DenyRights);
				bool flag = false;
				bool flag2 = false;
				byte[] entryId;
				string text;
				List<SecurityIdentifier> list3;
				SecurityIdentifier securityIdentifier;
				if (securityIdentifierAndFolderRights.SecurityIdentifier.IsWellKnown(WellKnownSidType.WorldSid))
				{
					entryId = Array<byte>.Empty;
					text = string.Empty;
					string legacyDN = string.Empty;
					securityIdentifier = securityIdentifierAndFolderRights.SecurityIdentifier;
					list3 = null;
					flag2 = true;
				}
				else if (securityIdentifierAndFolderRights.SecurityIdentifier.IsWellKnown(WellKnownSidType.AnonymousSid))
				{
					entryId = Array<byte>.Empty;
					text = "Anonymous";
					securityIdentifier = securityIdentifierAndFolderRights.SecurityIdentifier;
					list3 = null;
				}
				else if (ExternalUser.IsExternalUserSid(securityIdentifierAndFolderRights.SecurityIdentifier))
				{
					ExternalUser externalUser = AclHelper.TryGetExternalUser(securityIdentifierAndFolderRights.SecurityIdentifier, lazilyInitializedExternalUserCollection);
					if (externalUser == null)
					{
						ExTraceGlobals.StorageTracer.TraceWarning<SecurityIdentifier>(0L, "Cannot find external user with SID {0}, build entry from information we have", securityIdentifierAndFolderRights.SecurityIdentifier);
						string text2 = AclHelper.CreateLocalUserStrignRepresentation(securityIdentifierAndFolderRights.SecurityIdentifier);
						text = text2;
					}
					else
					{
						text = externalUser.Name;
						string legacyDN = externalUser.LegacyDn;
					}
					entryId = AddressBookEntryId.MakeAddressBookEntryIDFromLocalDirectorySid(securityIdentifierAndFolderRights.SecurityIdentifier);
					securityIdentifier = securityIdentifierAndFolderRights.SecurityIdentifier;
					list3 = null;
				}
				else
				{
					MiniRecipient miniRecipient = recipientSession.FindMiniRecipientBySid<MiniRecipient>(securityIdentifierAndFolderRights.SecurityIdentifier, Array<PropertyDefinition>.Empty);
					string legacyDN;
					if (miniRecipient == null)
					{
						ExTraceGlobals.StorageTracer.TraceWarning<SecurityIdentifier>(0L, "Cannot find recipient with SID {0}, build entry from the information we have", securityIdentifierAndFolderRights.SecurityIdentifier);
						flag = (securityIdentifierAndFolderRights.SecurityIdentifierType == FolderSecurity.SecurityIdentifierType.Group);
						string text3 = AclHelper.CreateNTUserStrignRepresentation(securityIdentifierAndFolderRights.SecurityIdentifier);
						text = text3;
						legacyDN = text3;
						securityIdentifier = securityIdentifierAndFolderRights.SecurityIdentifier;
						list3 = null;
					}
					else
					{
						flag = AclHelper.IsGroupRecipientType(miniRecipient.RecipientType);
						if (string.IsNullOrEmpty(miniRecipient.DisplayName))
						{
							text = AclHelper.CreateNTUserStrignRepresentation(securityIdentifierAndFolderRights.SecurityIdentifier);
						}
						else
						{
							text = miniRecipient.DisplayName;
						}
						if (string.IsNullOrEmpty(miniRecipient.LegacyExchangeDN))
						{
							legacyDN = text;
						}
						else
						{
							legacyDN = miniRecipient.LegacyExchangeDN;
						}
						SecurityIdentifier masterAccountSid = miniRecipient.MasterAccountSid;
						if (masterAccountSid != null && !masterAccountSid.IsWellKnown(WellKnownSidType.SelfSid))
						{
							securityIdentifier = masterAccountSid;
							list3 = null;
						}
						else
						{
							securityIdentifier = miniRecipient.Sid;
							MultiValuedProperty<SecurityIdentifier> sidHistory = miniRecipient.SidHistory;
							if (sidHistory != null && sidHistory.Count != 0)
							{
								list3 = new List<SecurityIdentifier>(sidHistory);
							}
							else
							{
								list3 = null;
							}
						}
					}
					entryId = AddressBookEntryId.MakeAddressBookEntryID(legacyDN, flag);
				}
				AclTableEntry aclTableEntry = list2.Find((AclTableEntry entry) => entry.SecurityIdentifier == securityIdentifier);
				if (aclTableEntry == null && list3 != null)
				{
					using (List<SecurityIdentifier>.Enumerator enumerator2 = list3.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							SecurityIdentifier sid = enumerator2.Current;
							aclTableEntry = list2.Find((AclTableEntry entry) => entry.SecurityIdentifier == sid);
							if (aclTableEntry != null)
							{
								break;
							}
						}
					}
				}
				if (aclTableEntry == null)
				{
					aclTableEntry = new AclTableEntry(AclModifyTable.GetIdForSecurityIdentifier(securityIdentifier, list3, aclTableIdMap), entryId, text, memberRights);
					aclTableEntry.SetSecurityIdentifier(securityIdentifier, flag);
					if (flag2)
					{
						list2.Insert(0, aclTableEntry);
					}
					else
					{
						list2.Add(aclTableEntry);
					}
				}
				else
				{
					aclTableEntry.MemberRights &= memberRights;
					if (aclTableEntry.IsGroup != flag)
					{
						throw new NonCanonicalACLException(annotatedAceList.CreateErrorInformation((LID)35788U, new int[0]));
					}
					aclTableEntry.SetSecurityIdentifier(securityIdentifier, flag);
				}
			}
			return list2;
		}

		// Token: 0x06004945 RID: 18757 RVA: 0x00133194 File Offset: 0x00131394
		private static byte[] SerializeAclTableAndSecurityDecscriptor(FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSD)
		{
			if (aclTableAndSD.SecurityDescriptor != null && aclTableAndSD.SecurityDescriptor.BinaryForm != null && aclTableAndSD.SecurityDescriptor.BinaryForm.Length > 31744)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>(0L, "Folder SD exceeds allowed size: {0}", aclTableAndSD.SecurityDescriptor.BinaryForm.Length);
				throw new ACLTooBigException();
			}
			if (aclTableAndSD.FreeBusySecurityDescriptor != null && aclTableAndSD.FreeBusySecurityDescriptor.BinaryForm != null && aclTableAndSD.FreeBusySecurityDescriptor.BinaryForm.Length > 31744)
			{
				ExTraceGlobals.StorageTracer.TraceError<int>(0L, "Folder F/B SD exceeds allowed size: {0}", aclTableAndSD.FreeBusySecurityDescriptor.BinaryForm.Length);
				throw new ACLTooBigException();
			}
			return aclTableAndSD.Serialize();
		}

		// Token: 0x06004946 RID: 18758 RVA: 0x00133244 File Offset: 0x00131444
		private List<AclTableEntry> ParseTableEntries(ArraySegment<byte> tableSegment)
		{
			List<AclTableEntry> result;
			using (BinaryDeserializer binaryDeserializer = new BinaryDeserializer(tableSegment))
			{
				List<AclTableEntry> list = FolderSecurity.AclTableEntry.ParseTableEntries<AclTableEntry>(binaryDeserializer.Reader, new Func<BinaryReader, AclTableEntry>(AclTableEntry.Parse));
				HashSet<string> hashSet = null;
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						AclTableEntry aclTableEntry = list[i];
						if (aclTableEntry.MemberEntryId != null && aclTableEntry.MemberEntryId.Length != 0)
						{
							if (hashSet == null)
							{
								hashSet = new HashSet<string>();
							}
							string text = AclHelper.LegacyDnFromEntryId(aclTableEntry.MemberEntryId);
							if (!hashSet.Add(text.ToLower()))
							{
								return null;
							}
						}
						if (aclTableEntry.MemberId != 0L && aclTableEntry.MemberId != -1L)
						{
							aclTableEntry.SetMemberId(AclModifyTable.GetIdForSecurityIdentifier(aclTableEntry.SecurityIdentifier, null, this.coreFolder.AclTableIdMap));
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x0013333E File Offset: 0x0013153E
		private List<AclTableEntry> BuildAclTableFromSecurityDescriptor(out bool isCanonical, out string canonicalErrorInformation)
		{
			return AclModifyTable.BuildAclTableFromSecurityDescriptor(this.securityDescriptor, this.freeBusySecurityDescriptor, new LazilyInitialized<ExternalUserCollection>(() => this.GetExternalUsers(this.Session)), this.recipientSession, this.coreFolder.AclTableIdMap, out isCanonical, out canonicalErrorInformation);
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x00133378 File Offset: 0x00131578
		private ExternalUserCollection GetExternalUsers(StoreSession session)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			if (this.externalUsers == null && mailboxSession != null)
			{
				this.externalUsers = mailboxSession.GetExternalUsers();
			}
			return this.externalUsers;
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x001333AC File Offset: 0x001315AC
		internal static void WriteFolderAclTable(CoreFolder coreFolder, byte[] propertyToSet)
		{
			using (FolderPropertyStream folderPropertyStream = (FolderPropertyStream)coreFolder.PropertyBag.OpenPropertyStream(CoreFolderSchema.AclTableAndSecurityDescriptor, PropertyOpenMode.Modify))
			{
				folderPropertyStream.Write(propertyToSet, 0, propertyToSet.Length);
			}
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x001333F8 File Offset: 0x001315F8
		private void Load()
		{
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSecurityDescriptorProperty = AclModifyTable.ReadAclTableAndSecurityDescriptor(this.coreFolder.PropertyBag);
			if (aclTableAndSecurityDescriptorProperty.SecurityDescriptor == null)
			{
				return;
			}
			this.securityDescriptor = aclTableAndSecurityDescriptorProperty.SecurityDescriptor.ToRawSecurityDescriptorThrow();
			this.freeBusySecurityDescriptor = ((aclTableAndSecurityDescriptorProperty.FreeBusySecurityDescriptor != null) ? aclTableAndSecurityDescriptorProperty.FreeBusySecurityDescriptor.ToRawSecurityDescriptorThrow() : null);
			List<AclTableEntry> list = null;
			if (aclTableAndSecurityDescriptorProperty.SerializedAclTable.Count != 0 && !this.useSecurityDescriptorOnly)
			{
				list = this.ParseTableEntries(aclTableAndSecurityDescriptorProperty.SerializedAclTable);
			}
			if (list == null)
			{
				bool flag;
				string canonicalErrorInformation;
				this.tableEntries = this.BuildAclTableFromSecurityDescriptor(out flag, out canonicalErrorInformation);
				if (!flag && (this.options & ModifyTableOptions.ExtendedPermissionInformation) == ModifyTableOptions.ExtendedPermissionInformation)
				{
					ExTraceGlobals.StorageTracer.TraceError(0L, "Cannot build blob ACL table blob with non-canonical SD");
					throw new NonCanonicalACLException(canonicalErrorInformation);
				}
			}
			else
			{
				this.tableEntries = list;
			}
			if (this.tableEntries.Count == 0 || this.tableEntries[0].MemberId != 0L)
			{
				MemberRights rights = (this.freeBusySecurityDescriptor == null) ? MemberRights.FreeBusySimple : MemberRights.None;
				this.tableEntries.Insert(0, AclModifyTable.BuildEveryoneDefaultEntry(rights));
			}
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x001334FC File Offset: 0x001316FC
		private void Save()
		{
			List<FolderSecurity.SecurityIdentifierAndFolderRights> list = new List<FolderSecurity.SecurityIdentifierAndFolderRights>(this.tableEntries.Count);
			foreach (AclTableEntry aclTableEntry in this.tableEntries)
			{
				list.Add(new FolderSecurity.SecurityIdentifierAndFolderRights(aclTableEntry.SecurityIdentifier, (FolderSecurity.ExchangeFolderRights)aclTableEntry.MemberRights, aclTableEntry.IsGroup ? FolderSecurity.SecurityIdentifierType.Group : FolderSecurity.SecurityIdentifierType.User));
				aclTableEntry.MemberRights = (MemberRights)FolderSecurity.NormalizeFolderRights((FolderSecurity.ExchangeFolderRights)aclTableEntry.MemberRights);
			}
			byte[] array = AclModifyTable.SerializeTableEntries(this.tableEntries);
			RawAcl rawAcl = FolderSecurity.AnnotatedAceList.BuildFolderCanonicalAceList(list);
			if (this.securityDescriptor != null)
			{
				this.securityDescriptor.DiscretionaryAcl = rawAcl;
			}
			else
			{
				this.securityDescriptor = FolderSecurity.AclTableAndSecurityDescriptorProperty.CreateFolderSecurityDescriptor(rawAcl).ToRawSecurityDescriptorThrow();
			}
			RawAcl rawAcl2 = FolderSecurity.AnnotatedAceList.BuildFreeBusyCanonicalAceList(list);
			if (this.freeBusySecurityDescriptor != null)
			{
				this.freeBusySecurityDescriptor.DiscretionaryAcl = rawAcl2;
			}
			else if ((this.options & ModifyTableOptions.FreeBusyAware) == ModifyTableOptions.FreeBusyAware)
			{
				this.freeBusySecurityDescriptor = FolderSecurity.AclTableAndSecurityDescriptorProperty.CreateFolderSecurityDescriptor(rawAcl2).ToRawSecurityDescriptorThrow();
			}
			Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType> dictionary = new Dictionary<SecurityIdentifier, FolderSecurity.SecurityIdentifierType>(list.Count);
			foreach (FolderSecurity.SecurityIdentifierAndFolderRights securityIdentifierAndFolderRights in list)
			{
				if (dictionary.ContainsKey(securityIdentifierAndFolderRights.SecurityIdentifier))
				{
					throw new InvalidParamException(new LocalizedString(string.Format("SID {0} is not unique.", securityIdentifierAndFolderRights.SecurityIdentifier)));
				}
				dictionary.Add(securityIdentifierAndFolderRights.SecurityIdentifier, securityIdentifierAndFolderRights.SecurityIdentifierType);
			}
			FolderSecurity.AclTableAndSecurityDescriptorProperty aclTableAndSD = new FolderSecurity.AclTableAndSecurityDescriptorProperty(new ArraySegment<byte>(array), dictionary, SecurityDescriptor.FromRawSecurityDescriptor(this.securityDescriptor), SecurityDescriptor.FromRawSecurityDescriptor(this.freeBusySecurityDescriptor));
			this.coreFolder.OnBeforeFolderSave();
			AclModifyTable.WriteFolderAclTable(this.coreFolder, AclModifyTable.SerializeAclTableAndSecurityDecscriptor(aclTableAndSD));
			this.coreFolder.OnAfterFolderSave();
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x001336D8 File Offset: 0x001318D8
		private void AddPendingChange(ModifyTableOperationType operation, PropValue[] propValues)
		{
			this.pendingModifyOperations.Add(new ModifyTableOperation(operation, propValues));
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x001336EC File Offset: 0x001318EC
		private void FixRightsIfNeeded(AclTableEntry tableEntry)
		{
			if ((tableEntry.MemberRights & MemberRights.FreeBusyDetailed) == MemberRights.FreeBusyDetailed)
			{
				tableEntry.MemberRights |= MemberRights.FreeBusySimple;
			}
			if ((this.options & ModifyTableOptions.FreeBusyAware) != ModifyTableOptions.FreeBusyAware)
			{
				if (tableEntry.MemberId == -1L)
				{
					tableEntry.MemberRights &= ~(MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed);
				}
				else
				{
					tableEntry.MemberRights |= MemberRights.FreeBusySimple;
					if ((tableEntry.MemberRights & MemberRights.ReadAny) == MemberRights.ReadAny)
					{
						tableEntry.MemberRights |= MemberRights.FreeBusyDetailed;
					}
				}
			}
			if ((tableEntry.MemberRights & (MemberRights.ReadAny | MemberRights.Owner)) != MemberRights.None)
			{
				tableEntry.MemberRights |= MemberRights.Visible;
			}
			if ((tableEntry.MemberRights & MemberRights.DeleteAny) != MemberRights.None)
			{
				tableEntry.MemberRights |= MemberRights.DeleteOwned;
			}
			if ((tableEntry.MemberRights & MemberRights.EditAny) != MemberRights.None)
			{
				tableEntry.MemberRights |= MemberRights.EditOwned;
			}
		}

		// Token: 0x0400277E RID: 10110
		internal const int MaxSecurityDescriptorSize = 31744;

		// Token: 0x0400277F RID: 10111
		private readonly CoreFolder coreFolder;

		// Token: 0x04002780 RID: 10112
		private readonly ModifyTableOptions options;

		// Token: 0x04002781 RID: 10113
		private readonly IModifyTableRestriction modifyTableRestriction;

		// Token: 0x04002782 RID: 10114
		private readonly bool useSecurityDescriptorOnly;

		// Token: 0x04002783 RID: 10115
		private IRecipientSession recipientSession;

		// Token: 0x04002784 RID: 10116
		private List<AclTableEntry> tableEntries = new List<AclTableEntry>();

		// Token: 0x04002785 RID: 10117
		private RawSecurityDescriptor securityDescriptor;

		// Token: 0x04002786 RID: 10118
		private RawSecurityDescriptor freeBusySecurityDescriptor;

		// Token: 0x04002787 RID: 10119
		private bool replaceAllRows;

		// Token: 0x04002788 RID: 10120
		private List<ModifyTableOperation> pendingModifyOperations = new List<ModifyTableOperation>();

		// Token: 0x04002789 RID: 10121
		private bool propertyTableRestrictionSuppressed;

		// Token: 0x0400278A RID: 10122
		private ExternalUserCollection externalUsers;
	}
}
