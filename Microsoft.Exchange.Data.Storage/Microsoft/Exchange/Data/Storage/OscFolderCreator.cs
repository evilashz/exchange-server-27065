using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000503 RID: 1283
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OscFolderCreator
	{
		// Token: 0x06003793 RID: 14227 RVA: 0x000DFBD5 File Offset: 0x000DDDD5
		public OscFolderCreator(MailboxSession session) : this(session, new XSOFactory())
		{
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000DFBE4 File Offset: 0x000DDDE4
		internal OscFolderCreator(MailboxSession session, IXSOFactory xsoFactory)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			if (session.MailboxOwner != null && session.MailboxOwner.MailboxInfo.IsArchive)
			{
				throw new ArgumentException("Archive mailbox is not supported.", "session");
			}
			this.session = session;
			this.xsoFactory = xsoFactory;
			this.networkToFolderMap = new Dictionary<OscNetworkMoniker, OscFolderCreateResult>();
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000DFC50 File Offset: 0x000DDE50
		public OscFolderCreateResult Create(string provider, string userId)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			Util.ThrowOnNullOrEmptyArgument(userId, "userId");
			Guid guidFromName = OscProviderRegistry.GetGuidFromName(provider);
			string networkId;
			if (OscProviderRegistry.TryGetNetworkId(provider, out networkId))
			{
				return this.Create(provider, guidFromName, userId, networkId);
			}
			return this.Create(provider, guidFromName, userId, string.Empty);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000DFCA0 File Offset: 0x000DDEA0
		internal OscFolderCreateResult Create(string provider, Guid providerGuid, string userId, string networkId)
		{
			OscNetworkMoniker oscNetworkMoniker = new OscNetworkMoniker(providerGuid, networkId, userId);
			OscFolderCreateResult oscFolderCreateResult;
			if (this.networkToFolderMap.TryGetValue(oscNetworkMoniker, out oscFolderCreateResult))
			{
				OscFolderCreator.Tracer.TraceDebug((long)this.GetHashCode(), "Folder creator: folder for provider '{0}' (GUID={1}), user id '{2}', and network id '{3}' found in cache.  Folder is '{4}'.", new object[]
				{
					provider,
					providerGuid,
					userId,
					networkId,
					oscFolderCreateResult
				});
				return oscFolderCreateResult;
			}
			OscFolderCreateResult result;
			try
			{
				StoreObjectId storeObjectId = this.FindExistingFolder(provider, userId, networkId);
				OscFolderCreator.Tracer.TraceDebug((long)this.GetHashCode(), "Folder creator: folder for provider '{0}' (GUID={1}), user id '{2}', and network id '{3}' ALREADY exists with id '{4}'.", new object[]
				{
					provider,
					providerGuid,
					userId,
					networkId,
					storeObjectId
				});
				result = this.AddFolderToCache(new OscFolderCreateResult(storeObjectId, false), oscNetworkMoniker);
			}
			catch (ObjectNotFoundException)
			{
				OscFolderCreator.Tracer.TraceDebug((long)this.GetHashCode(), "Folder creator: folder for provider '{0}' (GUID={1}), user id '{2}', and network id '{3}' doesn't exist.", new object[]
				{
					provider,
					providerGuid,
					userId,
					networkId
				});
				StoreObjectId folderId = this.CreateWhenFolderDoesntExist(provider, providerGuid, userId, networkId);
				result = this.AddFolderToCache(new OscFolderCreateResult(folderId, true), oscNetworkMoniker);
			}
			return result;
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000DFDD0 File Offset: 0x000DDFD0
		private StoreObjectId FindExistingFolder(string provider, string userId, string networkId)
		{
			OscFolderCreator.Tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Folder creator: looking for existing folder for provider: {0}; user id: {1}; network id: {2}", provider, userId, networkId);
			return new OscFolderLocator(this.session, this.xsoFactory).Find(provider, userId, networkId);
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x000DFE04 File Offset: 0x000DE004
		private StoreObjectId CreateWhenFolderDoesntExist(string provider, Guid providerGuid, string userId, string networkId)
		{
			StoreObjectId parentFolderId = this.GetParentFolderId(providerGuid);
			foreach (string text in new OscFolderDisplayNameGenerator(providerGuid, 10))
			{
				try
				{
					return this.CreateFolderWithDisplayName(text, parentFolderId, provider, providerGuid, userId, networkId);
				}
				catch (ObjectExistedException)
				{
					OscFolderCreator.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Folder creator: caught ObjectExistedException when attempting to create folder with display name '{0}' for provider '{1}'", text, provider);
				}
			}
			throw new CannotCreateOscFolderBecauseOfConflictException(provider);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000DFE94 File Offset: 0x000DE094
		private StoreObjectId CreateFolderWithDisplayName(string displayName, StoreObjectId parentFolder, string provider, Guid providerGuid, string userId, string networkId)
		{
			StoreObjectId objectId;
			using (Folder folder = Folder.Create(this.session, parentFolder, StoreObjectType.ContactsFolder, displayName, CreateMode.CreateNew))
			{
				folder[FolderSchema.IsPeopleConnectSyncFolder] = true;
				folder[FolderSchema.ExtendedFolderFlags] = ExtendedFolderFlags.ReadOnly;
				folder.Save();
				folder.Load(new PropertyDefinition[]
				{
					FolderSchema.Id
				});
				this.CreateContactSyncFAI(folder.Id.ObjectId, displayName, new OscNetworkMoniker(providerGuid, networkId, userId));
				this.session.ContactFolders.MyContactFolders.Add(folder.Id.ObjectId);
				OscFolderCreator.Tracer.TraceDebug((long)this.GetHashCode(), "Folder creator: successfully created folder with display name '{0}' and id '{1}' for provider '{2}' (GUID={3}), user id '{4}', and network id '{5}'", new object[]
				{
					displayName,
					folder.Id.ObjectId,
					provider,
					providerGuid,
					userId,
					networkId
				});
				objectId = folder.Id.ObjectId;
			}
			return objectId;
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000DFFA0 File Offset: 0x000DE1A0
		private void CreateContactSyncFAI(StoreObjectId folderId, string folderDisplayName, OscNetworkMoniker networkMoniker)
		{
			using (MessageItem messageItem = MessageItem.CreateAssociated(this.session, folderId))
			{
				messageItem.ClassName = "IPM.Microsoft.OSC.ContactSync";
				messageItem[MessageItemSchema.OscContactSources] = new string[]
				{
					networkMoniker.ToString()
				};
				messageItem.Save(SaveMode.ResolveConflicts);
				OscFolderCreator.Tracer.TraceDebug<string, StoreObjectId, OscNetworkMoniker>((long)this.GetHashCode(), "Folder creator: successfully created ContactSync FAI in folder='{0}' (ID='{1}') with network moniker='{2}'", folderDisplayName, folderId, networkMoniker);
			}
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000E0028 File Offset: 0x000DE228
		private StoreObjectId GetParentFolderId(Guid provider)
		{
			return this.session.GetDefaultFolderId(OscProviderRegistry.GetParentFolder(provider));
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000E003B File Offset: 0x000DE23B
		private OscFolderCreateResult AddFolderToCache(OscFolderCreateResult folder, OscNetworkMoniker networkMoniker)
		{
			this.networkToFolderMap[networkMoniker] = folder;
			return folder;
		}

		// Token: 0x04001D83 RID: 7555
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04001D84 RID: 7556
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001D85 RID: 7557
		private readonly MailboxSession session;

		// Token: 0x04001D86 RID: 7558
		private readonly Dictionary<OscNetworkMoniker, OscFolderCreateResult> networkToFolderMap;
	}
}
