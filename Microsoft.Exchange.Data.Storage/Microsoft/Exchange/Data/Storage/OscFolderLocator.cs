using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000505 RID: 1285
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OscFolderLocator
	{
		// Token: 0x060037A1 RID: 14241 RVA: 0x000E01A4 File Offset: 0x000DE3A4
		public OscFolderLocator(IMailboxSession session) : this(session, new XSOFactory())
		{
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x000E01B2 File Offset: 0x000DE3B2
		internal OscFolderLocator(IMailboxSession session, IXSOFactory xsoFactory)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			this.session = session;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000E01E0 File Offset: 0x000DE3E0
		public StoreObjectId Find(string provider, string userId)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			Util.ThrowOnNullOrEmptyArgument(userId, "userId");
			string networkId;
			if (OscProviderRegistry.TryGetNetworkId(provider, out networkId))
			{
				return this.Find(provider, userId, networkId);
			}
			return this.Find(provider, userId, string.Empty);
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000E0224 File Offset: 0x000DE424
		internal StoreObjectId Find(string provider, string userId, string networkId)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			Util.ThrowOnNullOrEmptyArgument(userId, "userId");
			Guid guid;
			if (!OscProviderRegistry.TryGetGuidFromName(provider, out guid))
			{
				OscFolderLocator.Tracer.TraceError<string>((long)this.GetHashCode(), "Folder locator: cannot find folder for unknown provider: {0}", provider);
				throw new ObjectNotFoundException(ServerStrings.UnknownOscProvider(provider));
			}
			OscNetworkMoniker arg = new OscNetworkMoniker(guid, networkId, userId);
			foreach (IStorePropertyBag storePropertyBag in new OscProviderCandidateFolderEnumerator(this.session, guid, this.xsoFactory))
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(FolderSchema.Id, null);
				if (valueOrDefault == null)
				{
					OscFolderLocator.Tracer.TraceError((long)this.GetHashCode(), "Folder locator: skipping folder with invalid id");
				}
				else
				{
					OscFolderLocator.Tracer.TraceDebug<string, OscNetworkMoniker, StoreObjectId>((long)this.GetHashCode(), "Folder locator: looking for ContactSync FAI for provider '{0}' and moniker '{1}' in folder '{2}'", provider, arg, valueOrDefault.ObjectId);
					foreach (IStorePropertyBag item in new OscContactSyncFAIEnumerator(this.session, valueOrDefault.ObjectId, this.xsoFactory))
					{
						foreach (OscNetworkMoniker oscNetworkMoniker in new OscFolderContactSourcesEnumerator(item))
						{
							OscFolderLocator.Tracer.TraceDebug<OscNetworkMoniker, StoreObjectId>((long)this.GetHashCode(), "Folder locator: found network moniker '{0}' in folder '{1}'", oscNetworkMoniker, valueOrDefault.ObjectId);
							if (arg.Equals(oscNetworkMoniker))
							{
								OscFolderLocator.Tracer.TraceDebug<string, StoreObjectId>((long)this.GetHashCode(), "Folder locator: found folder for provider '{0}'.  Folder id is '{1}'", provider, valueOrDefault.ObjectId);
								return valueOrDefault.ObjectId;
							}
						}
					}
				}
			}
			OscFolderLocator.Tracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Folder locator: folder for provider: {0}; user id: {1}; network id: {2}; not found.", provider, userId, networkId);
			throw new ObjectNotFoundException(ServerStrings.OscFolderForProviderNotFound(provider));
		}

		// Token: 0x04001D89 RID: 7561
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04001D8A RID: 7562
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001D8B RID: 7563
		private readonly IMailboxSession session;
	}
}
