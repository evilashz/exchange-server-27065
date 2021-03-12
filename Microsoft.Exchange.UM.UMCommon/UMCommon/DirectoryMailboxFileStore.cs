using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000092 RID: 146
	internal abstract class DirectoryMailboxFileStore
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00013F00 File Offset: 0x00012100
		public DirectoryMailboxFileStore(OrganizationId orgId, Guid mbxGuid, string folderName)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			ValidateArgument.NotNullOrEmpty(folderName, "folderName");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "DirectoryMailboxFileStore constructor - orgId='{0}', mbxGuid='{1}', folderName='{2}'", new object[]
			{
				orgId,
				mbxGuid,
				folderName
			});
			this.mbxFileStore = new MailboxFileStore(folderName);
			this.OrgId = orgId;
			this.MailboxGuid = mbxGuid;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00013F76 File Offset: 0x00012176
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x00013F7E File Offset: 0x0001217E
		private protected OrganizationId OrgId { protected get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00013F87 File Offset: 0x00012187
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00013F8F File Offset: 0x0001218F
		private protected Guid MailboxGuid { protected get; private set; }

		// Token: 0x0600052D RID: 1325 RVA: 0x00013F98 File Offset: 0x00012198
		public static MailboxSession GetMailboxSession(OrganizationId orgId, Guid mbxGuid)
		{
			ADSessionSettings adSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), orgId, null, false);
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromMailboxGuid(adSettings, mbxGuid, null);
			return MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=UM;Action=DirectoryProcessor");
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013FCC File Offset: 0x000121CC
		protected void UploadFile(string filePath, string fileSetId, MailboxSession mbxSession)
		{
			ValidateArgument.NotNullOrEmpty(filePath, "filePath");
			ValidateArgument.NotNullOrEmpty(fileSetId, "fileSetId");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "DirectoryMailboxFileStore.UploadFile - filePath='{0}', fileSetId='{1}'", new object[]
			{
				filePath,
				fileSetId
			});
			string[] sources = new string[]
			{
				filePath
			};
			this.mbxFileStore.Upload(sources, fileSetId, mbxSession);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00014040 File Offset: 0x00012240
		protected string DownloadLatestFile(string fileSetId, DateTime threshold, MailboxSession mbxSession)
		{
			ValidateArgument.NotNullOrEmpty(fileSetId, "fileSetId");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "DirectoryMailboxFileStore.DownloadLatestFile - fileSetId='{0}', threshold='{1}'", new object[]
			{
				fileSetId,
				threshold
			});
			FileSetItem current = this.mbxFileStore.GetCurrent(fileSetId, mbxSession);
			string text = null;
			if (current != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "DirectoryMailboxFileStore.DownloadLatestFile - Found fileSetId='{0}', time='{1}'", new object[]
				{
					fileSetId,
					current.Time.UniversalTime
				});
				if (current.Time.UniversalTime > threshold)
				{
					List<string> list = this.mbxFileStore.Download(current, mbxSession);
					if (list.Count > 0)
					{
						text = list[0];
						CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "DirectoryMailboxFileStore.DownloadLatestFile - fileSetId='{0}', filePath='{1}'", new object[]
						{
							fileSetId,
							text
						});
					}
				}
			}
			return text;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001414E File Offset: 0x0001234E
		protected void DeleteFileFromMailbox(string fileSetId, MailboxSession mailboxSession)
		{
			ValidateArgument.NotNullOrEmpty(fileSetId, "fileSetId");
			ValidateArgument.NotNull(mailboxSession, "mailboxSession");
			ExTraceGlobals.UMGrammarGeneratorTracer.TraceDebug<string>((long)this.GetHashCode(), "DirectoryMailboxFileStore.DeleteFileFromMailbox - fileSetId='{0}'", fileSetId);
			this.mbxFileStore.RemoveAll(fileSetId, mailboxSession);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00014192 File Offset: 0x00012392
		protected void DeleteFile(string filePath)
		{
			this.HandleDeleteExceptions(filePath, delegate(string name)
			{
				File.Delete(name);
			});
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000141C1 File Offset: 0x000123C1
		protected void DeleteFolder(string folderPath)
		{
			this.HandleDeleteExceptions(folderPath, delegate(string name)
			{
				Directory.Delete(name, true);
			});
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000141E8 File Offset: 0x000123E8
		private void HandleDeleteExceptions(string name, Action<string> deleteAction)
		{
			Exception ex = null;
			try
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, 0, "Deleting '{0}'", new object[]
				{
					name
				});
				deleteAction(name);
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				CallIdTracer.TraceError(ExTraceGlobals.UtilTracer, 0, "Failed to delete '{0}'. Exception : '{1}'", new object[]
				{
					name,
					ex
				});
			}
		}

		// Token: 0x0400032C RID: 812
		internal const string MailboxClientString = "Client=UM;Action=DirectoryProcessor";

		// Token: 0x0400032D RID: 813
		private MailboxFileStore mbxFileStore;
	}
}
