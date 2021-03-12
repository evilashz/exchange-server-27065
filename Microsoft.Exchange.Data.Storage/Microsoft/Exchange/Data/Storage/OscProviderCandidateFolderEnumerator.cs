using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200050A RID: 1290
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OscProviderCandidateFolderEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060037BF RID: 14271 RVA: 0x000E0DC4 File Offset: 0x000DEFC4
		internal OscProviderCandidateFolderEnumerator(IMailboxSession session, Guid provider, IXSOFactory xsoFactory)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			this.session = session;
			this.provider = provider;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000E0FE0 File Offset: 0x000DF1E0
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			foreach (IStorePropertyBag candidate in this.EnumerateCandidatesThatMatchDefaultNamingConvention())
			{
				yield return candidate;
			}
			foreach (IStorePropertyBag candidate2 in this.EnumerateAllContactFolders())
			{
				yield return candidate2;
			}
			yield break;
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000E14C0 File Offset: 0x000DF6C0
		private IEnumerable<IStorePropertyBag> EnumerateCandidatesThatMatchDefaultNamingConvention()
		{
			string defaultFolderDisplayName;
			if (!OscProviderRegistry.TryGetDefaultFolderDisplayName(this.provider, out defaultFolderDisplayName))
			{
				OscProviderCandidateFolderEnumerator.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Candidate folder enumerator: provider {0} is unknown.  Cannot enumerate candidates that match naming convention", this.provider);
			}
			else
			{
				TextFilter displayNameStartsWithProviderName = new TextFilter(FolderSchema.DisplayName, defaultFolderDisplayName, MatchOptions.Prefix, MatchFlags.IgnoreCase);
				OscProviderCandidateFolderEnumerator.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "Candidate folder enumerator: the default folder display name for provider {0} is {1}", this.provider, defaultFolderDisplayName);
				using (IFolder rootFolder = this.xsoFactory.BindToFolder(this.session, this.session.GetDefaultFolderId(DefaultFolderType.Root)))
				{
					using (IQueryResult subFoldersQuery = rootFolder.IFolderQuery(FolderQueryFlags.None, null, OscProviderCandidateFolderEnumerator.SortByDisplayNameAscending, OscProviderCandidateFolderEnumerator.FolderPropertiesToLoad))
					{
						if (!subFoldersQuery.SeekToCondition(SeekReference.OriginBeginning, displayNameStartsWithProviderName, SeekToConditionFlags.AllowExtendedFilters))
						{
							OscProviderCandidateFolderEnumerator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Candidate folder enumerator: SeekToCondition = false.  No folder has display name matching {0}", defaultFolderDisplayName);
							yield break;
						}
						IStorePropertyBag[] folders = subFoldersQuery.GetPropertyBags(10);
						while (folders.Length > 0)
						{
							foreach (IStorePropertyBag folder in folders)
							{
								string displayName = folder.GetValueOrDefault<string>(FolderSchema.DisplayName, string.Empty);
								VersionedId folderId = folder.GetValueOrDefault<VersionedId>(FolderSchema.Id, null);
								if (folderId == null)
								{
									OscProviderCandidateFolderEnumerator.Tracer.TraceError<string>((long)this.GetHashCode(), "Candidate folder enumerator: skipping bogus folder '{0}' because it has a blank id.", displayName);
								}
								else
								{
									string containerClass = folder.GetValueOrDefault<string>(StoreObjectSchema.ContainerClass, string.Empty);
									if (string.IsNullOrEmpty(containerClass) || !ObjectClass.IsContactsFolder(containerClass))
									{
										OscProviderCandidateFolderEnumerator.Tracer.TraceDebug<string, VersionedId>((long)this.GetHashCode(), "Candidate folder enumerator: skipping folder '{0}' (ID={1}) because it's not a contacts folder.", displayName, folderId);
									}
									else
									{
										if (string.IsNullOrEmpty(displayName) || !displayName.StartsWith(defaultFolderDisplayName, StringComparison.OrdinalIgnoreCase))
										{
											OscProviderCandidateFolderEnumerator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Candidate folder enumerator: we've iterated past candidates that follow the naming convention.  Current folder is '{0}'", displayName);
											yield break;
										}
										OscProviderCandidateFolderEnumerator.Tracer.TraceDebug<string, VersionedId>((long)this.GetHashCode(), "Candidate folder enumerator: folder: {0}; id: {1}; is a good candidate.", displayName, folderId);
										yield return folder;
									}
								}
							}
							folders = subFoldersQuery.GetPropertyBags(10);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000E14DD File Offset: 0x000DF6DD
		private IEnumerable<IStorePropertyBag> EnumerateAllContactFolders()
		{
			return new ContactFoldersEnumerator(this.session, this.xsoFactory, ContactFoldersEnumeratorOptions.SkipHiddenFolders, new PropertyDefinition[0]);
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000E14F7 File Offset: 0x000DF6F7
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001D9A RID: 7578
		private static readonly Trace Tracer = ExTraceGlobals.OutlookSocialConnectorInteropTracer;

		// Token: 0x04001D9B RID: 7579
		private static readonly SortBy[] SortByDisplayNameAscending = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x04001D9C RID: 7580
		private static readonly PropertyDefinition[] FolderPropertiesToLoad = new PropertyDefinition[]
		{
			StoreObjectSchema.ContainerClass,
			FolderSchema.DisplayName,
			FolderSchema.Id
		};

		// Token: 0x04001D9D RID: 7581
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001D9E RID: 7582
		private readonly IMailboxSession session;

		// Token: 0x04001D9F RID: 7583
		private readonly Guid provider;
	}
}
