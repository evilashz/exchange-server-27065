using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200009A RID: 154
	internal static class GrammarFileDistributionShare
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00014FA8 File Offset: 0x000131A8
		public static string GetOrgPath(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			string tenantFolderName = GrammarFileDistributionShare.GetTenantFolderName(orgId);
			return Path.Combine(GrammarFileDistributionShare.GetDirectoryProcessorFolderPath(), tenantFolderName);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00014FD2 File Offset: 0x000131D2
		public static string GetGrammarFolderPath(OrganizationId orgId, Guid mbxGuid)
		{
			return Path.Combine(GrammarFileDistributionShare.GetOrgPath(orgId), mbxGuid.ToString(), "SpeechGrammars");
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00014FF1 File Offset: 0x000131F1
		public static string GetDtmfMapFolderPath(OrganizationId orgId, Guid mbxGuid)
		{
			return Path.Combine(GrammarFileDistributionShare.GetOrgPath(orgId), mbxGuid.ToString(), "DtmfMap");
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00015010 File Offset: 0x00013210
		public static string GetGrammarManifestPath(OrganizationId orgId, Guid mbxGuid)
		{
			string grammarFolderPath = GrammarFileDistributionShare.GetGrammarFolderPath(orgId, mbxGuid);
			return Path.Combine(grammarFolderPath, "grammars.xml");
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00015030 File Offset: 0x00013230
		public static string GetGrammarGenerationRunFolderPath(OrganizationId orgId, Guid mbxGuid, Guid runId)
		{
			string grammarFolderPath = GrammarFileDistributionShare.GetGrammarFolderPath(orgId, mbxGuid);
			return Path.Combine(grammarFolderPath, runId.ToString());
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00015058 File Offset: 0x00013258
		public static string GetGrammarFileFolderPath(OrganizationId orgId, Guid mbxGuid, Guid runId, CultureInfo culture)
		{
			string grammarGenerationRunFolderPath = GrammarFileDistributionShare.GetGrammarGenerationRunFolderPath(orgId, mbxGuid, runId);
			return Path.Combine(grammarGenerationRunFolderPath, culture.Name);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001507C File Offset: 0x0001327C
		public static Guid GetMailboxGuid(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid orgId='{0}'", new object[]
			{
				orgId
			});
			Guid guid = Guid.Empty;
			string orgPath = GrammarFileDistributionShare.GetOrgPath(orgId);
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid orgPath='{0}'", new object[]
			{
				orgPath
			});
			string[] array = null;
			if (Directory.Exists(orgPath))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid orgPath='{0}' exists", new object[]
				{
					orgPath
				});
				array = Directory.GetDirectories(orgPath);
			}
			if (array != null && array.Length > 0)
			{
				if (array.Length == 1)
				{
					string text = array[0];
					CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid - only dir='{0}'", new object[]
					{
						text
					});
					guid = GrammarFileDistributionShare.ExtractFolderGuid(text);
				}
				else
				{
					DateTime t = DateTime.MinValue;
					foreach (string text2 in array)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid dir='{0}'", new object[]
						{
							text2
						});
						Guid guid2 = GrammarFileDistributionShare.ExtractFolderGuid(text2);
						string grammarManifestPath = GrammarFileDistributionShare.GetGrammarManifestPath(orgId, guid2);
						if (File.Exists(grammarManifestPath))
						{
							DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(grammarManifestPath);
							CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid manifestPath='{0}', fileLastWriteTimeUtc='{1}'", new object[]
							{
								grammarManifestPath,
								lastWriteTimeUtc
							});
							if (lastWriteTimeUtc > t)
							{
								guid = guid2;
								t = lastWriteTimeUtc;
							}
						}
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.GetMailboxGuid mailboxGuid='{0}'", new object[]
			{
				guid
			});
			return guid;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00015224 File Offset: 0x00013424
		public static bool SpeechGrammarsFolderExists(OrganizationId orgId)
		{
			bool result = false;
			string orgPath = GrammarFileDistributionShare.GetOrgPath(orgId);
			Exception ex = null;
			try
			{
				string[] directories = Directory.GetDirectories(orgPath, "SpeechGrammars", SearchOption.AllDirectories);
				result = (directories.Length > 0);
			}
			catch (UnauthorizedAccessException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.SpeechGrammarsFolderExists ex='{0}'", new object[]
					{
						ex
					});
				}
			}
			return result;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000152B0 File Offset: 0x000134B0
		public static void CreateDirectoryProcessorFolder()
		{
			string directoryProcessorFolderPath = GrammarFileDistributionShare.GetDirectoryProcessorFolderPath();
			GrammarFileDistributionShare.CreateDirectoryProcessorDirectory(directoryProcessorFolderPath);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000152CC File Offset: 0x000134CC
		private static string GetTenantFolderName(OrganizationId orgId)
		{
			ValidateArgument.NotNull(orgId, "orgId");
			string result;
			if (orgId.OrganizationalUnit != null)
			{
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(orgId);
				string text = iadsystemConfigurationLookup.GetExternalDirectoryOrganizationId().ToString();
				result = text;
			}
			else
			{
				result = "Enterprise";
			}
			return result;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00015313 File Offset: 0x00013513
		private static string GetDirectoryProcessorFolderPath()
		{
			return Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\DirectoryProcessor");
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00015324 File Offset: 0x00013524
		private static Guid ExtractFolderGuid(string dirPath)
		{
			Guid empty = Guid.Empty;
			string[] array = dirPath.Split(new char[]
			{
				Path.DirectorySeparatorChar
			});
			if (!Guid.TryParse(array[array.Length - 1], out empty))
			{
				empty = Guid.Empty;
			}
			return empty;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001540C File Offset: 0x0001360C
		private static bool CreateDirectoryProcessorDirectory(string path)
		{
			return Utils.TryDiskOperation(delegate
			{
				Directory.CreateDirectory(path);
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.CreateDirectoryProcessorDirectory succeeded: Path='{0}'", new object[]
				{
					path
				});
			}, delegate(Exception e)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, null, "GrammarFileDistributionShare.CreateDirectoryProcessorDirectory failed: Path='{0}', Error='{1}'", new object[]
				{
					path,
					e
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UnableToCreateDirectoryProcessorDirectory, null, new object[]
				{
					path,
					CommonUtil.ToEventLogString(e)
				});
			});
		}

		// Token: 0x04000345 RID: 837
		private const string DirectoryProcessorFolder = "UnifiedMessaging\\DirectoryProcessor";

		// Token: 0x04000346 RID: 838
		private const string SpeechGrammarsFolderName = "SpeechGrammars";

		// Token: 0x04000347 RID: 839
		private const string DtmfMapFolderName = "DtmfMap";

		// Token: 0x04000348 RID: 840
		private const string EnterpriseGrammarFolderName = "Enterprise";

		// Token: 0x04000349 RID: 841
		private const string GrammarManifestFileName = "grammars.xml";
	}
}
