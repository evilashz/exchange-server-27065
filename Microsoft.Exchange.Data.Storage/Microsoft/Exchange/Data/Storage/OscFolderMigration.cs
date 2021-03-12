using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000506 RID: 1286
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OscFolderMigration
	{
		// Token: 0x060037A6 RID: 14246 RVA: 0x000E044C File Offset: 0x000DE64C
		public OscFolderMigration(IMailboxSession session, IOscContactSourcesForContactParser oscContactSourcesParser) : this(session, oscContactSourcesParser, new XSOFactory())
		{
		}

		// Token: 0x060037A7 RID: 14247 RVA: 0x000E045C File Offset: 0x000DE65C
		internal OscFolderMigration(IMailboxSession session, IOscContactSourcesForContactParser oscContactSourcesParser, IXSOFactory xsoFactory)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("oscContactSourcesParser", oscContactSourcesParser);
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			if (session.MailboxOwner != null && session.MailboxOwner.MailboxInfo.IsArchive)
			{
				throw new ArgumentException("Archive mailbox is not supported.", "session");
			}
			this.session = session;
			this.xsoFactory = xsoFactory;
			this.oscContactSourcesParser = oscContactSourcesParser;
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x000E04CF File Offset: 0x000DE6CF
		public void Migrate(StoreObjectId folder)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			this.MigrateFolder(folder);
			this.MigrateContacts(folder);
		}

		// Token: 0x060037A9 RID: 14249 RVA: 0x000E04EC File Offset: 0x000DE6EC
		private void MigrateFolder(StoreObjectId folderId)
		{
			using (IFolder folder = this.xsoFactory.BindToFolder(this.session, folderId))
			{
				folder[FolderSchema.IsPeopleConnectSyncFolder] = true;
				ExtendedFolderFlags valueOrDefault = folder.GetValueOrDefault<ExtendedFolderFlags>(FolderSchema.ExtendedFolderFlags, (ExtendedFolderFlags)0);
				if ((valueOrDefault & ExtendedFolderFlags.ReadOnly) == (ExtendedFolderFlags)0)
				{
					folder[FolderSchema.ExtendedFolderFlags] = (valueOrDefault | ExtendedFolderFlags.ReadOnly);
				}
				folder.Save();
			}
			this.session.ContactFolders.MyContactFolders.Add(folderId);
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000E0580 File Offset: 0x000DE780
		private void MigrateContacts(StoreObjectId folder)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (IStorePropertyBag storePropertyBag in new OscFolderMigration.OscContactsEnumerator(this.session, folder, this.xsoFactory))
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				string valueOrDefault2 = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.DisplayName, string.Empty);
				if (valueOrDefault == null)
				{
					OscFolderMigration.Tracer.TraceError((long)this.GetHashCode(), "OSC folder migration: skipping contact with blank id.");
				}
				else
				{
					string valueOrDefault3 = storePropertyBag.GetValueOrDefault<string>(ItemSchema.CloudId, string.Empty);
					if (!string.IsNullOrEmpty(valueOrDefault3))
					{
						if (hashSet.Contains(valueOrDefault3))
						{
							OscFolderMigration.Tracer.TraceDebug<string, StoreObjectId, string>((long)this.GetHashCode(), "OSC folder migration: contact {0} (id={1}) has cloud id: {2} but that id is already assigned to another contact.", valueOrDefault2, valueOrDefault.ObjectId, valueOrDefault3);
							this.ClearCloudIdFromContact(valueOrDefault, valueOrDefault2);
						}
						else
						{
							OscFolderMigration.Tracer.TraceDebug<string, StoreObjectId, string>((long)this.GetHashCode(), "OSC folder migration: contact {0} (id={1}) already has cloud id: {2}", valueOrDefault2, valueOrDefault.ObjectId, valueOrDefault3);
							hashSet.Add(valueOrDefault3);
						}
					}
					else
					{
						byte[] valueOrDefault4 = storePropertyBag.GetValueOrDefault<byte[]>(ContactSchema.OscContactSourcesForContact, null);
						if (valueOrDefault4 == null || valueOrDefault4.Length == 0)
						{
							OscFolderMigration.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "OSC folder migration: contact {0} (id={1}) does NOT have cloud id and the OscContactSources property is blank.  Cannot stamp cloud id.", valueOrDefault2, valueOrDefault.ObjectId);
						}
						else
						{
							try
							{
								OscNetworkProperties oscNetworkProperties = this.oscContactSourcesParser.ReadOscContactSource(valueOrDefault4);
								if (oscNetworkProperties == null || string.IsNullOrEmpty(oscNetworkProperties.NetworkUserId))
								{
									OscFolderMigration.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "OSC folder migration: cannot stamp cloud id on contact {0} (id={1}) because the OscContactSources property is blank.", valueOrDefault2, valueOrDefault.ObjectId);
								}
								else if (hashSet.Contains(oscNetworkProperties.NetworkUserId))
								{
									OscFolderMigration.Tracer.TraceDebug<string, string, StoreObjectId>((long)this.GetHashCode(), "OSC folder migration: cannot stamp cloud id '{0}' on contact {1} (id={2}) because that cloud id is already assigned to a different contact.", oscNetworkProperties.NetworkUserId, valueOrDefault2, valueOrDefault.ObjectId);
								}
								else
								{
									this.AssignCloudIdToContact(valueOrDefault, oscNetworkProperties.NetworkUserId, valueOrDefault2);
									hashSet.Add(oscNetworkProperties.NetworkUserId);
								}
							}
							catch (OscContactSourcesForContactParseException arg)
							{
								OscFolderMigration.Tracer.TraceError<string, StoreObjectId, OscContactSourcesForContactParseException>((long)this.GetHashCode(), "OSC folder migration: failed to parse OscContactSources of contact {0} (id={1}).  Exception: {2}", valueOrDefault2, valueOrDefault.ObjectId, arg);
							}
						}
					}
				}
			}
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000E07B8 File Offset: 0x000DE9B8
		private void AssignCloudIdToContact(VersionedId contact, string cloudId, string displayName)
		{
			using (IContact contact2 = this.xsoFactory.BindToContact(this.session, contact, null))
			{
				contact2[ItemSchema.CloudId] = cloudId;
				contact2.Save(SaveMode.ResolveConflicts);
				OscFolderMigration.Tracer.TraceDebug<string, string, StoreObjectId>((long)this.GetHashCode(), "OSC folder migration: stamped cloud id '{0}' on contact {1} (id={2})", cloudId, displayName, contact.ObjectId);
			}
		}

		// Token: 0x060037AC RID: 14252 RVA: 0x000E0828 File Offset: 0x000DEA28
		private void ClearCloudIdFromContact(VersionedId contact, string displayName)
		{
			using (IContact contact2 = this.xsoFactory.BindToContact(this.session, contact, null))
			{
				contact2.Delete(ItemSchema.CloudId);
				contact2.Save(SaveMode.ResolveConflicts);
				OscFolderMigration.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "OSC folder migration: cleared cloud id of contact {0} (id={1})", displayName, contact.ObjectId);
			}
		}

		// Token: 0x04001D8C RID: 7564
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04001D8D RID: 7565
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001D8E RID: 7566
		private readonly IMailboxSession session;

		// Token: 0x04001D8F RID: 7567
		private readonly IOscContactSourcesForContactParser oscContactSourcesParser;

		// Token: 0x02000507 RID: 1287
		private sealed class OscContactsEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
		{
			// Token: 0x060037AE RID: 14254 RVA: 0x000E08A4 File Offset: 0x000DEAA4
			internal OscContactsEnumerator(IMailboxSession session, StoreObjectId folder, IXSOFactory xsoFactory)
			{
				ArgumentValidator.ThrowIfNull("session", session);
				ArgumentValidator.ThrowIfNull("folder", folder);
				ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
				this.session = session;
				this.folder = folder;
				this.xsoFactory = xsoFactory;
			}

			// Token: 0x060037AF RID: 14255 RVA: 0x000E0BFC File Offset: 0x000DEDFC
			public IEnumerator<IStorePropertyBag> GetEnumerator()
			{
				using (IFolder folder = this.xsoFactory.BindToFolder(this.session, this.folder))
				{
					using (IQueryResult query = folder.IItemQuery(ItemQueryType.None, null, OscFolderMigration.OscContactsEnumerator.SortByItemClassAscending, OscFolderMigration.OscContactsEnumerator.PropertiesToLoad))
					{
						if (!query.SeekToCondition(SeekReference.OriginBeginning, OscFolderMigration.OscContactsEnumerator.ItemClassIsContact, SeekToConditionFlags.AllowExtendedFilters))
						{
							OscFolderMigration.OscContactsEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "Contacts enumerator: no contacts in this folder.");
							yield break;
						}
						IStorePropertyBag[] contacts = query.GetPropertyBags(100);
						while (contacts.Length > 0)
						{
							foreach (IStorePropertyBag contact in contacts)
							{
								string itemClass = contact.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, string.Empty);
								if (string.IsNullOrEmpty(itemClass))
								{
									OscFolderMigration.OscContactsEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "Contacts enumerator: skipping item with blank item class.");
								}
								else
								{
									if (!ObjectClass.IsContact(itemClass))
									{
										OscFolderMigration.OscContactsEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "Contacts enumerator: no further contacts in this folder.");
										yield break;
									}
									yield return contact;
								}
							}
							contacts = query.GetPropertyBags(100);
						}
					}
				}
				yield break;
			}

			// Token: 0x060037B0 RID: 14256 RVA: 0x000E0C18 File Offset: 0x000DEE18
			IEnumerator IEnumerable.GetEnumerator()
			{
				throw new NotSupportedException("Must use the generic version of GetEnumerator.");
			}

			// Token: 0x04001D90 RID: 7568
			private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

			// Token: 0x04001D91 RID: 7569
			private static readonly SortBy[] SortByItemClassAscending = new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
			};

			// Token: 0x04001D92 RID: 7570
			private static readonly TextFilter ItemClassIsContact = new TextFilter(StoreObjectSchema.ItemClass, "IPM.Contact", MatchOptions.Prefix, MatchFlags.IgnoreCase);

			// Token: 0x04001D93 RID: 7571
			private static readonly PropertyDefinition[] PropertiesToLoad = new PropertyDefinition[]
			{
				ItemSchema.Id,
				StoreObjectSchema.ItemClass,
				StoreObjectSchema.DisplayName,
				ItemSchema.CloudId,
				ContactSchema.OscContactSourcesForContact
			};

			// Token: 0x04001D94 RID: 7572
			private readonly IXSOFactory xsoFactory;

			// Token: 0x04001D95 RID: 7573
			private readonly IMailboxSession session;

			// Token: 0x04001D96 RID: 7574
			private readonly StoreObjectId folder;
		}
	}
}
