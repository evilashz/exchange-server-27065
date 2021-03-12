using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.External
{
	// Token: 0x0200002B RID: 43
	internal class PublicFolderSourceConverter : ISourceConverter
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00011444 File Offset: 0x0000F644
		public IEnumerable<SearchSource> Convert(ISearchPolicy policy, IEnumerable<SearchSource> sources)
		{
			Recorder.Trace(5L, TraceType.InfoTrace, "PublicFolderSourceConverter.Convert Sources:", sources);
			IConfigurationSession configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, policy.RecipientSession.SessionSettings, 46, "Convert", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\MailboxSearch\\WebService\\External\\PublicFolderSourceConverter.cs");
			using (PublicFolderDataProvider provider = new PublicFolderDataProvider(configurationSession, "Get-PublicFolder", Guid.Empty))
			{
				foreach (SearchSource source in sources)
				{
					if (source.SourceType == SourceType.PublicFolder)
					{
						Recorder.Trace(5L, TraceType.InfoTrace, new object[]
						{
							"PublicFolderSourceConverter.Convert Source:",
							source.ReferenceId,
							"PublicFolderSourceConverter.Type:",
							source.SourceType
						});
						PublicFolder publicFolder = null;
						try
						{
							PublicFolderId publicFolderId2;
							if (this.IsPath(source.ReferenceId))
							{
								Recorder.Trace(5L, TraceType.InfoTrace, "PublicFolderSourceConverter.Convert From Path:", source.ReferenceId);
								publicFolderId2 = new PublicFolderId(new MapiFolderPath(source.ReferenceId));
							}
							else
							{
								Recorder.Trace(5L, TraceType.InfoTrace, "PublicFolderSourceConverter.Convert From StoreId:", source.ReferenceId);
								StoreObjectId storeObjectId = StoreObjectId.FromHexEntryId(source.ReferenceId);
								publicFolderId2 = new PublicFolderId(storeObjectId);
							}
							if (publicFolderId2 != null)
							{
								Recorder.Trace(5L, TraceType.InfoTrace, "PublicFolderSourceConverter.Convert PublicFolderId:", publicFolderId2);
								publicFolder = (PublicFolder)provider.Read<PublicFolder>(publicFolderId2);
							}
						}
						catch (FormatException)
						{
							Recorder.Trace(5L, TraceType.InfoTrace, "PublicFolderSourceConverter.Convert FormatException, Source:", source);
						}
						if (publicFolder != null)
						{
							yield return this.GetSource(source, publicFolder);
						}
						else
						{
							Recorder.Trace(5L, TraceType.InfoTrace, "PublicFolderSourceConverter.Convert Failed, Source:", source);
							yield return source;
						}
					}
					else if (source.SourceType == SourceType.AllPublicFolders)
					{
						Recorder.Trace(5L, TraceType.InfoTrace, new object[]
						{
							"PublicFolderSourceConverter.Convert Source:",
							source.ReferenceId,
							"Type:",
							source.SourceType
						});
						PublicFolderId publicFolderId = new PublicFolderId(MapiFolderPath.IpmSubtreeRoot);
						IEnumerable<PublicFolder> folders = provider.FindPaged<PublicFolder>(null, publicFolderId, true, null, 50);
						foreach (PublicFolder publicFolder2 in folders)
						{
							if (publicFolder2.FolderPath != MapiFolderPath.IpmSubtreeRoot)
							{
								yield return this.GetSource(source, publicFolder2);
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00011470 File Offset: 0x0000F670
		private SearchSource GetSource(SearchSource source, PublicFolder publicFolder)
		{
			SearchSource searchSource = source.Clone();
			searchSource.OriginalReferenceId = publicFolder.FolderPath.ToString();
			searchSource.ReferenceId = publicFolder.ContentMailboxGuid.ToString();
			searchSource.FolderSpec = this.GetFolderSpec(publicFolder);
			searchSource.SourceLocation = SourceLocation.PrimaryOnly;
			searchSource.SourceType = SourceType.MailboxGuid;
			return searchSource;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000114CB File Offset: 0x0000F6CB
		private string GetFolderSpec(PublicFolder folder)
		{
			return folder.InternalFolderIdentity.ToBase64String();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000114D8 File Offset: 0x0000F6D8
		private bool IsPath(string s)
		{
			return !string.IsNullOrEmpty(s) && s.StartsWith("\\");
		}

		// Token: 0x040000F6 RID: 246
		private const string ProviderAction = "Get-PublicFolder";
	}
}
