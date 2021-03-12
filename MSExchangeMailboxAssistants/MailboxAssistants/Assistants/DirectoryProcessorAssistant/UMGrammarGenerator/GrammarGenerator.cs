using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001BB RID: 443
	internal class GrammarGenerator : DirectoryProcessorBaseTask
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x00063AE2 File Offset: 0x00061CE2
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.GrammarGeneratorTracer;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00063AE9 File Offset: 0x00061CE9
		public bool NoError
		{
			get
			{
				return null == this.exception;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x00063AF4 File Offset: 0x00061CF4
		private bool DoBulkUpload
		{
			get
			{
				return base.RunData.UserCount < GrammarGenerator.bulkUploadThreshold;
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00063B08 File Offset: 0x00061D08
		public GrammarGenerator(RunData runData, ICollection<DirectoryProcessorMailboxData> mailboxesToProcess) : base(runData)
		{
			ValidateArgument.NotNull(mailboxesToProcess, "mailboxesToProcess");
			this.mailboxesToProcess = mailboxesToProcess;
			int grammarCultureCount = UMGrammarTenantCache.Instance.GetGrammarCultureCount();
			this.grammars = new Dictionary<string, List<string>>(grammarCultureCount);
			this.normalizationCaches = new Dictionary<string, NormalizationCache>(grammarCultureCount);
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x00063B51 File Offset: 0x00061D51
		public override bool ShouldDeferFinalize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00063B54 File Offset: 0x00061D54
		public override void FinalizeMe(DirectoryProcessorBaseTaskContext taskContext)
		{
			base.Logger.TraceDebug(null, "Entering GrammarGenerator.FinalizeMe", new object[0]);
			if (this.NoError)
			{
				List<GrammarFileMetadata> grammarFileMetadata = this.GetGrammarFileMetadata();
				this.WriteGrammarGenerationManifest(grammarFileMetadata);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationSuccessful, null, new object[]
				{
					base.RunData.TenantId,
					base.RunData.RunId
				});
				this.SetUMGrammarReadyFlag();
			}
			else
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationFailed, null, new object[]
				{
					base.RunData.TenantId,
					base.RunData.RunId,
					CommonUtil.ToEventLogString(this.exception)
				});
			}
			base.FinalizeMe(taskContext);
			base.Logger.TraceDebug(null, "Exiting GrammarGenerator.FinalizeMe", new object[0]);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00063C38 File Offset: 0x00061E38
		public override bool ShouldRun(RecipientType recipientType)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.ShouldRun recipientType='{0}', orgId='{1}'", new object[]
			{
				recipientType,
				base.OrgId
			});
			if (!this.mailboxesToProcess.Contains(new DirectoryProcessorMailboxData(base.OrgId, base.DatabaseGuid, base.MailboxGuid)))
			{
				base.Logger.TraceDebug(this, "GrammarGenerator.ShouldRun mailboxGuid='{0}', orgId='{1}' should not be processed", new object[]
				{
					base.MailboxGuid,
					base.OrgId
				});
				return false;
			}
			if (RecipientType.Group == recipientType)
			{
				base.Logger.TraceDebug(this, "GrammarGenerator.ShouldRun is false for groups", new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00063CE4 File Offset: 0x00061EE4
		public override bool ShouldWatson(Exception e)
		{
			return !(e is IOException) && !(e is UnauthorizedAccessException);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00063CFC File Offset: 0x00061EFC
		protected override DirectoryProcessorBaseTaskContext DoChunkWork(DirectoryProcessorBaseTaskContext context, RecipientType recipientType)
		{
			ValidateArgument.NotNull(context, "context");
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.DoChunkWork recipientType='{0}'", new object[]
			{
				recipientType
			});
			if ((RecipientType.Group == recipientType && (context.TaskStatus & TaskStatus.DLADCrawlerFailed) != TaskStatus.NoError) || (RecipientType.User == recipientType && (context.TaskStatus & TaskStatus.UserADCrawlerFailed) != TaskStatus.NoError))
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationSkippedNoADFile, null, new object[]
				{
					base.TenantId,
					base.RunId,
					recipientType
				});
				this.exception = new GrammarGeneratorADException();
				return null;
			}
			if (!this.NoError)
			{
				base.Logger.TraceDebug(this, "GrammarGenerator.DoChunkWork - Skipping rest of grammar generation because of previous error='{0}'", new object[]
				{
					this.exception
				});
				return null;
			}
			GrammarGeneratorTaskContext grammarGeneratorTaskContext = context as GrammarGeneratorTaskContext;
			if (grammarGeneratorTaskContext == null)
			{
				base.Logger.TraceDebug(this, "First time GrammarGenerator.DoChunkWork is called", new object[0]);
				grammarGeneratorTaskContext = this.InitializeTask(recipientType, context);
			}
			DirectoryProcessorBaseTaskContext result = null;
			CultureInfo nextCultureToProcess = grammarGeneratorTaskContext.GetNextCultureToProcess();
			if (nextCultureToProcess != null)
			{
				base.Logger.TraceDebug(this, "GrammarGenerator.DoChunkWork - Processing culture='{0}'", new object[]
				{
					nextCultureToProcess
				});
				base.Logger.SetMetadataValues(this, recipientType, nextCultureToProcess.Name);
				this.GenerateGrammar(nextCultureToProcess, grammarGeneratorTaskContext.GrammarGeneratorInstance);
				result = grammarGeneratorTaskContext;
			}
			else if (this.DoBulkUpload)
			{
				base.Logger.TraceDebug(this, "GrammarGenerator.DoChunkWork - Do bulk upload", new object[0]);
				this.UploadResults(null);
			}
			return result;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00063E70 File Offset: 0x00062070
		private GrammarGeneratorTaskContext InitializeTask(RecipientType recipientType, DirectoryProcessorBaseTaskContext context)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.InitializeTask recipientType='{0}'", new object[]
			{
				recipientType
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationStarted, null, new object[]
			{
				base.TenantId,
				base.RunId,
				recipientType.ToString()
			});
			GrammarFileDistributionShare.CreateDirectoryProcessorFolder();
			IGrammarGeneratorInterface grammarGeneratorInstance = null;
			if (recipientType != RecipientType.User)
			{
				if (recipientType != RecipientType.Group)
				{
					ExAssert.RetailAssert(false, "Unsupported recipient type");
				}
				else
				{
					grammarGeneratorInstance = new DLGrammarGenerator(base.Logger);
				}
			}
			else
			{
				grammarGeneratorInstance = new UserGrammarGenerator(base.Logger, base.OrgId);
			}
			CultureInfo[] grammarCultures = UMGrammarTenantCache.Instance.GetGrammarCultures();
			return new GrammarGeneratorTaskContext(context.MailboxData, context.Job, context.TaskQueue, context.Step, context.TaskStatus, grammarGeneratorInstance, grammarCultures, base.Logger, context.RunData, context.DeferredFinalizeTasks);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00063F64 File Offset: 0x00062164
		private List<string> GenerateGrammar(CultureInfo c, IGrammarGeneratorInterface generatorInstance)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.GenerateGrammar culture='{0}'", new object[]
			{
				c
			});
			string runFolderPath = base.RunData.RunFolderPath;
			string adentriesFileName = generatorInstance.ADEntriesFileName;
			List<string> list = null;
			try
			{
				string entriesFilePath = ADCrawler.GetEntriesFilePath(runFolderPath, adentriesFileName);
				List<DirectoryGrammar> grammarList = generatorInstance.GetGrammarList();
				list = new List<string>(grammarList.Count);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationWritingGrammarEntriesStarted, null, new object[]
				{
					adentriesFileName,
					c.Name,
					base.TenantId,
					base.RunId
				});
				string grammarFileFolderPath = GrammarFileDistributionShare.GetGrammarFileFolderPath(base.OrgId, base.MailboxGuid, base.RunId, c);
				Directory.CreateDirectory(grammarFileFolderPath);
				string recognizerId = SpeechRecognizerInfo.GetRecognizerId(c);
				if (recognizerId != null)
				{
					string grammarFolderPath = GrammarFileDistributionShare.GetGrammarFolderPath(base.OrgId, base.MailboxGuid);
					INormalizationCacheFileStore cacheFileStore = NormalizationCacheMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
					using (SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine(recognizerId))
					{
						using (XmlReader xmlReader = XmlReader.Create(entriesFilePath))
						{
							using (GrammarGenerationLog grammarGenerationLog = new GrammarGenerationLog(grammarFileFolderPath, base.Logger))
							{
								NameNormalizer nameNormalizer = new NameNormalizer(c, speechRecognitionEngine, adentriesFileName, grammarFolderPath, base.Logger, cacheFileStore);
								this.InitializeGrammars(grammarFileFolderPath, c, grammarList);
								if (xmlReader.ReadToFollowing("ADEntry"))
								{
									for (;;)
									{
										ADEntry adentry = this.LoadADEntry(xmlReader, nameNormalizer, grammarGenerationLog);
										if (adentry != null && !this.WriteADEntryToGrammars(adentry, grammarList))
										{
											break;
										}
										base.RunData.ThrowIfShuttingDown();
										if (!xmlReader.ReadToFollowing("ADEntry"))
										{
											goto IL_18A;
										}
									}
									base.Logger.TraceError(this, "GrammarGenerator.Run - Cannot accept more entries in any of the grammar files", new object[0]);
								}
								IL_18A:
								list.AddRange(this.CompleteGrammars(grammarList));
								this.UploadResults(list, nameNormalizer, c);
							}
						}
						goto IL_204;
					}
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationMissingCulture, null, new object[]
				{
					base.TenantId,
					c.Name,
					Utils.GetLocalHostFqdn()
				});
				IL_204:
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationWritingGrammarEntriesCompleted, null, new object[]
				{
					adentriesFileName,
					c.Name,
					base.TenantId,
					base.RunId
				});
			}
			catch (Exception ex)
			{
				base.Logger.TraceError(this, "GrammarGenerator.Run - Exception='{0}'", new object[]
				{
					ex
				});
				this.exception = ex;
				throw;
			}
			return list;
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0006424C File Offset: 0x0006244C
		private void InitializeGrammars(string grammarFolderPath, CultureInfo c, List<DirectoryGrammar> grammarList)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.InitializeGrammars grammarFolderPath='{0}', culture='{1}'", new object[]
			{
				grammarFolderPath,
				c
			});
			foreach (DirectoryGrammar directoryGrammar in grammarList)
			{
				string fileName = Utilities.GetFileName(directoryGrammar.FileName, ".grxml");
				string path = Path.Combine(grammarFolderPath, fileName);
				directoryGrammar.InitializeGrammar(path, c);
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000642D8 File Offset: 0x000624D8
		private ADEntry LoadADEntry(XmlReader entryReader, NameNormalizer nameNormalizer, GrammarGenerationLog generationLog)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.LoadADEntry", new object[0]);
			ADEntry result = null;
			string text = entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[2].Name);
			string attribute = entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[0].Name);
			string attribute2 = entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[1].Name);
			Guid guid = new Guid(entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[3].Name));
			RecipientType recipientType = (RecipientType)Enum.Parse(typeof(RecipientType), entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[4].Name));
			string attribute3 = entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[6].Name);
			string attribute4 = entryReader.GetAttribute(GrammarRecipientHelper.LookupProperties[9].Name);
			base.Logger.TraceDebug(this, "GrammarGenerator.LoadADEntry - displayName='{0}', phoneticDisplayName='{1}', smtpAddress='{2}', objectGuid='{3}', recipientType='{4}', dialPlanGuid='{5}', AddressListMembership='{6}'", new object[]
			{
				attribute,
				attribute2,
				text,
				guid,
				recipientType,
				attribute3,
				attribute4
			});
			if (RecipientType.DynamicDistributionGroup != recipientType)
			{
				List<string> list = new List<string>(2);
				if (!string.IsNullOrEmpty(attribute))
				{
					list.Add(attribute);
				}
				if (!string.IsNullOrEmpty(attribute2))
				{
					list.Add(attribute2);
				}
				list = NormalizationHelper.GetNormalizedNames(list, nameNormalizer, recipientType, generationLog);
				if (list != null)
				{
					base.Logger.TraceDebug(this, "GrammarGenerator.LoadADEntry - Valid names found for entry", new object[0]);
					text = GrammarRecipientHelper.GetNormalizedEmailAddress(text);
					Guid dialPlanGuid = string.IsNullOrEmpty(attribute3) ? Guid.Empty : new Guid(attribute3);
					List<Guid> list2 = new List<Guid>();
					if (!string.IsNullOrEmpty(attribute4))
					{
						char[] separator = new char[]
						{
							','
						};
						string[] array = attribute4.Split(separator);
						foreach (string g in array)
						{
							list2.Add(new Guid(g));
						}
					}
					result = new ADEntry(list, text, guid, recipientType, dialPlanGuid, list2);
				}
			}
			return result;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000644D4 File Offset: 0x000626D4
		private bool WriteADEntryToGrammars(ADEntry entry, List<DirectoryGrammar> grammarList)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.WriteADEntryToGrammars", new object[0]);
			bool result = false;
			foreach (DirectoryGrammar directoryGrammar in grammarList)
			{
				directoryGrammar.WriteADEntry(entry);
				if (!directoryGrammar.MaxEntriesExceeded)
				{
					base.Logger.TraceDebug(this, "Entering GrammarGenerator.WriteADEntryToGrammars - {0} can accept more entries", new object[]
					{
						directoryGrammar.FilePath
					});
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00064568 File Offset: 0x00062768
		private List<string> CompleteGrammars(List<DirectoryGrammar> grammarList)
		{
			base.Logger.TraceDebug(this, "Entering GrammarGenerator.CompleteGrammars", new object[0]);
			List<string> list = new List<string>(grammarList.Count);
			foreach (DirectoryGrammar directoryGrammar in grammarList)
			{
				list.Add(directoryGrammar.CompleteGrammar());
			}
			foreach (DirectoryGrammar directoryGrammar2 in grammarList)
			{
				if (directoryGrammar2.MaxEntriesExceeded)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarFileMaxEntriesExceeded, null, new object[]
					{
						directoryGrammar2.FilePath,
						base.RunData.TenantId,
						base.RunData.RunId
					});
				}
			}
			return list;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00064668 File Offset: 0x00062868
		private List<GrammarFileMetadata> GetGrammarFileMetadata()
		{
			List<GrammarFileMetadata> list = new List<GrammarFileMetadata>(this.grammars.Keys.Count);
			foreach (List<string> list2 in this.grammars.Values)
			{
				foreach (string text in list2)
				{
					base.Logger.TraceDebug(this, "GrammarGenerationManager.GetGrammarFileMetadata - path='{0}'", new object[]
					{
						text
					});
					string text2 = text.Substring(text.IndexOf(base.RunId.ToString()));
					FileInfo fileInfo = new FileInfo(text);
					string sha1Hash = this.GetSHA1Hash(text);
					list.Add(new GrammarFileMetadata(text2.ToString(), sha1Hash, fileInfo.Length));
				}
			}
			return list;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0006477C File Offset: 0x0006297C
		private string GetSHA1Hash(string path)
		{
			string result;
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (SHA1Cng sha1Cng = new SHA1Cng())
				{
					byte[] array = sha1Cng.ComputeHash(fileStream);
					StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.AppendFormat("{0:x2}", array[i]);
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000649C0 File Offset: 0x00062BC0
		internal static void CleanUpOldGrammarRuns(RunData runData, Trace Tracer)
		{
			Utils.TryDiskOperation(delegate
			{
				Guid runId = Guid.Empty;
				try
				{
					string grammarManifestPath = GrammarFileDistributionShare.GetGrammarManifestPath(runData.OrgId, runData.MailboxGuid);
					GrammarGenerationMetadata grammarGenerationMetadata = GrammarGenerationMetadata.Deserialize(grammarManifestPath);
					runId = grammarGenerationMetadata.RunId;
				}
				catch (Exception obj)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_ReadLastSuccessRunIDFailed, null, new object[]
					{
						runData.TenantId,
						runData.RunId,
						CommonUtil.ToEventLogString(obj)
					});
				}
				string grammarGenerationRunFolderPath = GrammarFileDistributionShare.GetGrammarGenerationRunFolderPath(runData.OrgId, runData.MailboxGuid, runId);
				string grammarFolderPath = GrammarFileDistributionShare.GetGrammarFolderPath(runData.OrgId, runData.MailboxGuid);
				string[] directories = Directory.GetDirectories(grammarFolderPath);
				foreach (string text in directories)
				{
					string fileName = Path.GetFileName(text);
					Guid b;
					if (Guid.TryParse(fileName, out b) && runData.RunId != b && string.Compare(text, grammarGenerationRunFolderPath, StringComparison.OrdinalIgnoreCase) != 0)
					{
						Utilities.DebugTrace(Tracer, "Deleting run directory '{0}'", new object[]
						{
							text
						});
						Directory.Delete(text, true);
					}
				}
			}, delegate(Exception exception)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarGenerationCleanupFailed, null, new object[]
				{
					runData.TenantId,
					runData.RunId,
					CommonUtil.ToEventLogString(exception)
				});
			});
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00064A00 File Offset: 0x00062C00
		private void WriteGrammarGenerationManifest(List<GrammarFileMetadata> grammarFiles)
		{
			GrammarGenerationMetadata metadata = new GrammarGenerationMetadata(1, base.TenantId, base.RunId, Utils.GetLocalHostFqdn(), "15.00.1497.010", base.RunStartTime, grammarFiles);
			string grammarManifestPath = GrammarFileDistributionShare.GetGrammarManifestPath(base.OrgId, base.MailboxGuid);
			GrammarGenerationMetadata.Serialize(metadata, grammarManifestPath);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00064A4C File Offset: 0x00062C4C
		private void UploadResults(List<string> grammarFilePaths, NameNormalizer nameNormalizer, CultureInfo culture)
		{
			this.grammars.Add(culture.Name, grammarFilePaths);
			nameNormalizer.UpdateDiskCache();
			this.normalizationCaches.Add(culture.Name, nameNormalizer.NormalizationCache);
			if (!this.DoBulkUpload)
			{
				base.Logger.TraceDebug(this, "GrammarGenerator.UploadResults, upload results for culture='{0}'", new object[]
				{
					culture
				});
				this.UploadResults(culture);
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00064AB4 File Offset: 0x00062CB4
		private void UploadResults(CultureInfo culture)
		{
			Exception ex = null;
			try
			{
				using (MailboxSession mailboxSession = DirectoryMailboxFileStore.GetMailboxSession(base.OrgId, base.MailboxGuid))
				{
					GrammarMailboxFileStore grammarMailboxFileStore = GrammarMailboxFileStore.FromMailboxGuid(base.OrgId, base.MailboxGuid);
					grammarMailboxFileStore.UploadGrammars(this.grammars, culture, mailboxSession, new ThrowIfOperationCanceled(this.ThrowIfShuttingDown));
					this.UploadNormalizationCaches(culture, mailboxSession);
				}
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_GrammarFileUploadToSystemMailboxFailed, null, new object[]
					{
						base.RunData.TenantId,
						base.RunData.MailboxGuid,
						base.RunData.RunId,
						CommonUtil.ToEventLogString(ex)
					});
				}
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00064BBC File Offset: 0x00062DBC
		private void UploadNormalizationCaches(CultureInfo culture, MailboxSession mbxSession)
		{
			if (culture != null)
			{
				this.normalizationCaches[culture.Name].UploadCacheFile(mbxSession);
				return;
			}
			foreach (string key in this.normalizationCaches.Keys)
			{
				this.normalizationCaches[key].UploadCacheFile(mbxSession);
				base.RunData.ThrowIfShuttingDown();
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00064C48 File Offset: 0x00062E48
		private void ThrowIfShuttingDown()
		{
			base.RunData.ThrowIfShuttingDown();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00064C58 File Offset: 0x00062E58
		private void SetUMGrammarReadyFlag()
		{
			Exception ex = null;
			try
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), base.OrgId, null, false);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 979, "SetUMGrammarReadyFlag", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\DirectoryProcessor\\UmGrammarGenerator\\GrammarGenerator.cs");
				ADUser aduser = tenantOrRootOrgRecipientSession.FindByExchangeGuid(base.MailboxGuid) as ADUser;
				if (aduser != null && !aduser.PersistedCapabilities.Contains(Capability.OrganizationCapabilityUMGrammarReady))
				{
					aduser.PersistedCapabilities.Add(Capability.OrganizationCapabilityUMGrammarReady);
					tenantOrRootOrgRecipientSession.Save(aduser);
				}
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (ADOperationException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (ex != null)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SetUMGrammarReadyFlagFailed, null, new object[]
					{
						base.RunData.TenantId,
						base.RunData.MailboxGuid,
						base.RunData.RunId,
						CommonUtil.ToEventLogString(ex)
					});
				}
			}
		}

		// Token: 0x04000ABD RID: 2749
		private static int bulkUploadThreshold = 10000;

		// Token: 0x04000ABE RID: 2750
		private readonly ICollection<DirectoryProcessorMailboxData> mailboxesToProcess;

		// Token: 0x04000ABF RID: 2751
		private Exception exception;

		// Token: 0x04000AC0 RID: 2752
		private Dictionary<string, List<string>> grammars;

		// Token: 0x04000AC1 RID: 2753
		private Dictionary<string, NormalizationCache> normalizationCaches;
	}
}
