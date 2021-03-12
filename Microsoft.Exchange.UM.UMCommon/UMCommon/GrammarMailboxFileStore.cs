using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200009F RID: 159
	internal sealed class GrammarMailboxFileStore : DirectoryMailboxFileStore
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x00015A6C File Offset: 0x00013C6C
		private GrammarMailboxFileStore(OrganizationId orgId, Guid mbxGuid) : base(orgId, mbxGuid, "SpeechGrammars")
		{
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00015A7B File Offset: 0x00013C7B
		public static GrammarMailboxFileStore FromMailboxGuid(OrganizationId orgId, Guid mbxGuid)
		{
			return new GrammarMailboxFileStore(orgId, mbxGuid);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00015A84 File Offset: 0x00013C84
		public static ADUser GetOrganizationMailboxForRouting(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			ADUser organizationMailboxByCapability = GrammarMailboxFileStore.GetOrganizationMailboxByCapability(orgId, OrganizationCapability.UMGrammarReady);
			if (organizationMailboxByCapability == null)
			{
				organizationMailboxByCapability = GrammarMailboxFileStore.GetOrganizationMailboxByCapability(orgId, OrganizationCapability.UMGrammar);
			}
			if (organizationMailboxByCapability != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, 0, "GrammarMailboxFileStore.GetOrganizationMailboxForRouting - orgId='{0}', finalChoice='{1}'", new object[]
				{
					orgId,
					organizationMailboxByCapability.DistinguishedName
				});
			}
			return organizationMailboxByCapability;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00015AE0 File Offset: 0x00013CE0
		public void UploadGrammars(Dictionary<string, List<string>> grammars, CultureInfo culture, MailboxSession mbxSession, ThrowIfOperationCanceled throwIfOperationCanceled)
		{
			ValidateArgument.NotNull(grammars, "grammars");
			ValidateArgument.NotNull(mbxSession, "mbxSession");
			ValidateArgument.NotNull(throwIfOperationCanceled, "throwIfOperationCanceled");
			string text = (culture != null) ? culture.Name : string.Empty;
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.UploadGrammars - Culture='{0}'", new object[]
			{
				text
			});
			string text2 = Path.Combine(Path.GetTempPath(), this.GetUniqueTmpFileName());
			Directory.CreateDirectory(text2);
			try
			{
				List<string> list;
				if (!string.IsNullOrEmpty(text))
				{
					list = new List<string>
					{
						text
					};
				}
				else
				{
					list = new List<string>(grammars.Keys);
				}
				foreach (string text3 in list)
				{
					ExTraceGlobals.UMGrammarGeneratorTracer.TraceDebug<string>((long)this.GetHashCode(), "GrammarMailboxFileStore.UploadGrammars - cultureName='{0}'", text3);
					List<string> list2 = grammars[text3];
					foreach (string text4 in list2)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.UploadGrammars - filePath='{0}'", new object[]
						{
							text4
						});
						string text5;
						string text6;
						this.Compress(text4, text2, out text5, out text6);
						string text7 = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
						{
							text5,
							text3,
							"1.0"
						});
						CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.UploadGrammars - compressedFilePath='{0}', compressedFileName='{1}', fileSetId='{2}'", new object[]
						{
							text6,
							text5,
							text7
						});
						base.UploadFile(text6, text7, mbxSession);
						File.Delete(text6);
						throwIfOperationCanceled();
					}
				}
			}
			finally
			{
				base.DeleteFolder(text2);
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00015D1C File Offset: 0x00013F1C
		public string DownloadGrammar(string fileName, CultureInfo culture, DateTime threshold)
		{
			ValidateArgument.NotNullOrEmpty(fileName, "fileName");
			ValidateArgument.NotNull(culture, "culture");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Download - fileName='{0}', culture='{1}', threshold='{2}'", new object[]
			{
				fileName,
				culture,
				threshold
			});
			string fileSetId = string.Format(CultureInfo.InvariantCulture, "{0}_{1}_{2}", new object[]
			{
				fileName + ".gz",
				culture.Name,
				"1.0"
			});
			string text = null;
			using (MailboxSession mailboxSession = DirectoryMailboxFileStore.GetMailboxSession(base.OrgId, base.MailboxGuid))
			{
				text = base.DownloadLatestFile(fileSetId, threshold, mailboxSession);
			}
			string result = null;
			if (text != null)
			{
				try
				{
					result = this.Decompress(text);
				}
				finally
				{
					base.DeleteFolder(Path.GetDirectoryName(text));
				}
			}
			return result;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00015E18 File Offset: 0x00014018
		private void Compress(string filePath, string compressedFileDirectory, out string compressedFileName, out string compressedFilePath)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Compress - filePath='{0}', compressedFileDir='{1}'", new object[]
			{
				filePath,
				compressedFileDirectory
			});
			string fileName = Path.GetFileName(filePath);
			compressedFileName = fileName + ".gz";
			compressedFilePath = Path.Combine(compressedFileDirectory, compressedFileName);
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Compress - fileName='{0}', compressedFileName='{1}', compressedFilePath='{2}'", new object[]
			{
				fileName,
				compressedFileName,
				compressedFilePath
			});
			try
			{
				using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
				{
					using (FileStream fileStream2 = new FileStream(compressedFilePath, FileMode.Create))
					{
						using (GZipStream gzipStream = new GZipStream(fileStream2, CompressionMode.Compress))
						{
							fileStream.CopyTo(gzipStream);
						}
					}
				}
			}
			catch (IOException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Compress - Error compressing to compressedFilePath='{0}', error='{1}'", new object[]
				{
					compressedFilePath,
					ex
				});
				base.DeleteFile(compressedFilePath);
				throw;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00015F64 File Offset: 0x00014164
		private string Decompress(string compressedFilePath)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Decompress - compressedFilePath='{0}'", new object[]
			{
				compressedFilePath
			});
			string text = Path.Combine(Path.GetTempPath(), this.GetUniqueTmpFileName());
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Decompress - uncompressedFilePath='{0}'", new object[]
			{
				text
			});
			try
			{
				using (FileStream fileStream = new FileStream(compressedFilePath, FileMode.Open))
				{
					using (FileStream fileStream2 = new FileStream(text, FileMode.Create))
					{
						using (GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Decompress))
						{
							gzipStream.CopyTo(fileStream2);
						}
					}
				}
			}
			catch (IOException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this.GetHashCode(), "GrammarMailboxFileStore.Decompress - Error decompressing to uncompressedFilePath='{0}', error='{1}'", new object[]
				{
					text,
					ex
				});
				base.DeleteFile(text);
				throw;
			}
			return text;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00016088 File Offset: 0x00014288
		private string GetUniqueTmpFileName()
		{
			return string.Format(CultureInfo.InvariantCulture, "UM-{0}", new object[]
			{
				Guid.NewGuid().ToString()
			});
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000160C4 File Offset: 0x000142C4
		private static ADUser GetOrganizationMailboxByCapability(OrganizationId orgId, OrganizationCapability capability)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			ADUser result = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, 0, "GrammarMailboxFileStore.GetOrganizationMailboxByCapability - orgId='{0}', Trying {1} flag", new object[]
			{
				orgId,
				capability
			});
			if (CommonConstants.UseDataCenterCallRouting)
			{
				ADUser[] allUsers = OrganizationMailbox.FindByOrganizationId(orgId, capability);
				result = GrammarMailboxFileStore.PickADUser(allUsers);
			}
			else
			{
				List<ADUser> list = null;
				List<ADUser> allUsers2 = null;
				bool flag = OrganizationMailbox.TryFindByOrganizationId(orgId, LocalServer.GetServer().ServerSite, capability, out list, out allUsers2);
				if (flag)
				{
					if (list.Count > 0)
					{
						result = GrammarMailboxFileStore.PickADUser(list);
					}
					else
					{
						result = GrammarMailboxFileStore.PickADUser(allUsers2);
					}
				}
			}
			return result;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00016160 File Offset: 0x00014360
		private static ADUser PickADUser(IList<ADUser> allUsers)
		{
			if (allUsers != null && allUsers.Count > 0)
			{
				int index = Interlocked.Increment(ref GrammarMailboxFileStore.mailboxCounter) % allUsers.Count;
				return allUsers[index];
			}
			return null;
		}

		// Token: 0x04000361 RID: 865
		private const string FileSetIdFormat = "{0}_{1}_{2}";

		// Token: 0x04000362 RID: 866
		private const string UniqueTmpFileNameFormat = "UM-{0}";

		// Token: 0x04000363 RID: 867
		private const string CompressedFileExt = ".gz";

		// Token: 0x04000364 RID: 868
		internal const string FolderName = "SpeechGrammars";

		// Token: 0x04000365 RID: 869
		public const string GrammarVersion = "1.0";

		// Token: 0x04000366 RID: 870
		private static int mailboxCounter;
	}
}
