using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.OAB;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.OAB;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.OABGenerator
{
	// Token: 0x020001E1 RID: 481
	internal sealed class OABGenerator : DisposeTrackableBase
	{
		// Token: 0x06001277 RID: 4727 RVA: 0x0006A086 File Offset: 0x00068286
		public OABGenerator(IConfigurationSession perOrgAdSystemConfigSession, OfflineAddressBook oab, SecurityIdentifier mailboxSid, string mailboxDomain, Action abortProcessingOnShutdown)
		{
			this.perOrgAdSystemConfigSession = perOrgAdSystemConfigSession;
			this.offlineAddressBook = oab;
			this.mailboxSid = mailboxSid;
			this.mailboxDomain = mailboxDomain;
			this.abortProcessingOnShutdown = abortProcessingOnShutdown;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0006A0B5 File Offset: 0x000682B5
		public OABGenerator(IConfigurationSession perOrgAdSystemConfigSession, OfflineAddressBook oab, SecurityIdentifier mailboxSid, string mailboxDomain) : this(perOrgAdSystemConfigSession, oab, mailboxSid, mailboxDomain, delegate()
		{
		})
		{
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x0006A0DF File Offset: 0x000682DF
		public GenerationStats Stats
		{
			get
			{
				return this.stats;
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0006A0E8 File Offset: 0x000682E8
		public void Initialize()
		{
			this.habRootLegacyDN = this.GetHabRoot();
			this.propertyManager = new PropertyManager(this.offlineAddressBook, this.mailboxSid, this.mailboxDomain, !string.IsNullOrEmpty(this.habRootLegacyDN));
			this.fileSet = new FileSet();
			this.oabDirectory = Path.Combine(Globals.OabFolderPath, this.offlineAddressBook.ExchangeObjectId.ToString());
			this.addressListFiles = new List<OABFile>();
			this.addressListDiffFiles = new List<List<OABFile>>();
			this.templateFiles = new List<OABFile>();
			this.stats = new GenerationStats
			{
				OfflineAddressBook = this.offlineAddressBook,
				Tenant = ((this.offlineAddressBook.OrganizationalUnitRoot != null) ? this.offlineAddressBook.OrganizationalUnitRoot.ToCanonicalName() : this.offlineAddressBook.Id.DomainId.ToString()),
				HABEnabled = !string.IsNullOrEmpty(this.habRootLegacyDN)
			};
			this.mailboxFileStore = new MailboxFileStore("OAB", this.stats, OABLogger.Instance);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0006A204 File Offset: 0x00068404
		public AssistantTaskContext PrepareFilesForOABGeneration(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = new AssistantStep(this.GenerateOrLinkTemplateFiles);
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.PrepareFilesForOABGeneration: start", new object[0]);
			MailboxSession mailboxSession = oabgeneratorTaskContext.Args.StoreSession as MailboxSession;
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("PrepareFilesForOABGeneration", this.stats))
						{
							using (new CpuPerformanceTracker("PrepareFilesForOABGeneration", this.stats))
							{
								OABGeneratorTaskContext oabgeneratorTaskContext2 = oabgeneratorTaskContext;
								oabgeneratorTaskContext2.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Combine(oabgeneratorTaskContext2.Cleanup, new Action<OABGeneratorTaskContext>(this.CleanupTemporaryAndOldFiles));
								OABGeneratorTaskContext oabgeneratorTaskContext3 = oabgeneratorTaskContext;
								oabgeneratorTaskContext3.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Combine(oabgeneratorTaskContext3.Cleanup, new Action<OABGeneratorTaskContext>(this.RegisterEndTime));
								this.RegisterStartTime();
								this.CreateOABDirectory();
								this.DownloadFilesFromMailbox(mailboxSession);
								this.previousManifest = OABManifest.LoadFromFile(Path.Combine(this.oabDirectory, "oab.xml"));
								this.changed = false;
								this.addressLists = new Queue<ADObjectId>(this.offlineAddressBook.AddressLists);
								if (this.addressLists.Count > 0)
								{
									this.currentAddressList = this.addressLists.Dequeue();
								}
								else
								{
									OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.PrepareFilesForOABGeneration: the OAB {0} contains no address lists.", new object[]
									{
										this.offlineAddressBook.Id
									});
									oabstep = oabgeneratorTaskContext.ReturnStep.Pop();
								}
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.PrepareFilesForOABGeneration: finish", new object[0]);
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0006A44C File Offset: 0x0006864C
		public AssistantTaskContext GenerateOrLinkTemplateFiles(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = new AssistantStep(this.BeginGeneratingAddressListFiles);
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.GenerateOrLinkTemplateFiles: start", new object[0]);
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("GenerateOrLinkTemplateFiles", this.stats))
						{
							using (new CpuPerformanceTracker("GenerateOrLinkTemplateFiles", this.stats))
							{
								if (OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled)
								{
									this.LinkToTemplateFiles();
								}
								else
								{
									this.GenerateTemplateFiles();
								}
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.GenerateOrLinkTemplateFiles: finish", new object[0]);
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0006A57C File Offset: 0x0006877C
		public AssistantTaskContext BeginGeneratingAddressListFiles(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = new AssistantStep(this.FinishGeneratingAddressListFiles);
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.BeginGeneratingAddressListFiles: start", new object[0]);
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("BeginGeneratingAddressListFiles", this.stats))
						{
							using (new CpuPerformanceTracker("BeginGeneratingAddressListFiles", this.stats))
							{
								this.addressListFile = new OABFile(null, OABDataFileType.Full);
								this.addressListFile.DnToUseInOABFile = (AddressBookBase.IsGlobalAddressListId(this.currentAddressList) ? "/" : ("/guid=" + BitConverter.ToString(this.currentAddressList.ObjectGuid.ToByteArray()).Replace("-", "")));
								int num = this.currentAddressList.ToString().IndexOf('\\');
								this.addressListFile.NameToUseInOABFile = ((num == -1) ? ("\\" + this.currentAddressList.ToString()) : this.currentAddressList.ToString().Substring(num));
								this.addressListFile.DnOfTheHabRoot = this.habRootLegacyDN;
								this.oldOabFile = null;
								OABGeneratorTaskContext oabgeneratorTaskContext2 = oabgeneratorTaskContext;
								oabgeneratorTaskContext2.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Combine(oabgeneratorTaskContext2.Cleanup, new Action<OABGeneratorTaskContext>(this.CloseOldOABFileUncompressedFileStream));
								if (this.TryGetPreviousVersionOfFile(this.addressListFile, true, out this.oldOabFile))
								{
									this.addressListFile.IsContinuationOfSequence = true;
									this.addressListFile.Guid = this.oldOabFile.Guid;
									this.addressListFile.SequenceNumber = this.oldOabFile.SequenceNumber;
								}
								else
								{
									this.addressListFile.IsContinuationOfSequence = false;
									this.addressListFile.Guid = Guid.NewGuid();
									this.addressListFile.SequenceNumber = 1U;
									OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.BeginGeneratingAddressListFiles:Creating new guid: {0}", new object[]
									{
										this.addressListFile.Guid.ToString()
									});
								}
								this.addressListFileGenerator = new AddressListFileGenerator(this.currentAddressList, this.addressListFile, this.propertyManager, this.fileSet, this.stats, this.abortProcessingOnShutdown);
								oabgeneratorTaskContext.ReturnStep.Push(new AssistantStep(this.FinishGeneratingAddressListFiles));
								oabstep = new AssistantStep(this.addressListFileGenerator.ProcessOnePageOfADResults);
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.BeginGeneratingAddressListFiles: finish", new object[0]);
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0006A8B8 File Offset: 0x00068AB8
		public AssistantTaskContext FinishGeneratingAddressListFiles(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = new AssistantStep(this.Publish);
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.FinishGeneratingAddressListFiles: start", new object[0]);
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("FinishGeneratingAddressListFiles", this.stats))
						{
							using (new CpuPerformanceTracker("FinishGeneratingAddressListFiles", this.stats))
							{
								this.addressListFile.UncompressedFileStream = this.addressListFileGenerator.UncompressedSortedFlatFile;
								using (new StopwatchPerformanceTracker("FinishGeneratingAddressListFiles.CompressGeneratedFiles", this.stats))
								{
									using (new CpuPerformanceTracker("FinishGeneratingAddressListFiles.CompressGeneratedFiles", this.stats))
									{
										this.addressListFile.Compress(this.fileSet, this.stats, "FinishGeneratingAddressListFiles.CompressGeneratedFiles");
									}
								}
								this.addressListFiles.Add(this.addressListFile);
								this.changed |= this.GenerateDiffFile(this.addressListFile, this.oldOabFile);
								this.CloseOldOABFileUncompressedFileStream(oabgeneratorTaskContext);
								OABGeneratorTaskContext oabgeneratorTaskContext2 = oabgeneratorTaskContext;
								oabgeneratorTaskContext2.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Remove(oabgeneratorTaskContext2.Cleanup, new Action<OABGeneratorTaskContext>(this.CloseOldOABFileUncompressedFileStream));
								if (this.addressLists.Count > 0)
								{
									this.currentAddressList = this.addressLists.Dequeue();
									oabstep = new AssistantStep(this.BeginGeneratingAddressListFiles);
								}
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.FinishGeneratingAddressListFiles: finish", new object[0]);
				OABLogger.AddressListGuid = Guid.Empty;
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x0006AB44 File Offset: 0x00068D44
		public AssistantTaskContext Publish(AssistantTaskContext assistantTaskContext)
		{
			OABGeneratorTaskContext oabgeneratorTaskContext = assistantTaskContext as OABGeneratorTaskContext;
			AssistantStep oabstep = oabgeneratorTaskContext.ReturnStep.Pop();
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.Publish: start", new object[0]);
			MailboxSession mailboxSession = oabgeneratorTaskContext.Args.StoreSession as MailboxSession;
			try
			{
				using (new StopwatchPerformanceTracker("Total", this.stats))
				{
					using (new CpuPerformanceTracker("Total", this.stats))
					{
						using (new StopwatchPerformanceTracker("Publish", this.stats))
						{
							using (new CpuPerformanceTracker("Publish", this.stats))
							{
								bool flag = true;
								if (this.changed)
								{
									try
									{
										this.PublishOabToDistribPoint();
										this.GenerateManifest();
										this.UploadFilesToMailbox(mailboxSession);
										goto IL_100;
									}
									catch (Exception ex)
									{
										OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.Publish: failed to fully published a changed OAB (Guid='{0}', Sequence={1}) because of {2}:{3}", new object[]
										{
											this.addressListFile.Guid,
											this.addressListFile.SequenceNumber,
											ex.GetType().ToString(),
											ex.Message
										});
										flag = false;
										goto IL_100;
									}
								}
								this.TouchManifest();
								IL_100:
								this.stats.UnnecessaryGeneration = !this.changed;
								if (flag)
								{
									this.CleanupTemporaryAndOldFiles(oabgeneratorTaskContext);
								}
								OABGeneratorTaskContext oabgeneratorTaskContext2 = oabgeneratorTaskContext;
								oabgeneratorTaskContext2.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Remove(oabgeneratorTaskContext2.Cleanup, new Action<OABGeneratorTaskContext>(this.CleanupTemporaryAndOldFiles));
								this.RegisterEndTime(oabgeneratorTaskContext);
								OABGeneratorTaskContext oabgeneratorTaskContext3 = oabgeneratorTaskContext;
								oabgeneratorTaskContext3.Cleanup = (Action<OABGeneratorTaskContext>)Delegate.Remove(oabgeneratorTaskContext3.Cleanup, new Action<OABGeneratorTaskContext>(this.RegisterEndTime));
							}
						}
					}
				}
			}
			finally
			{
				OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.Publish: finish", new object[0]);
				OABLogger.OabGuid = Guid.Empty;
				OABLogger.TraceId = 0;
				oabgeneratorTaskContext.OABStep = oabstep;
			}
			return OABGeneratorTaskContext.FromOABGeneratorTaskContext(oabgeneratorTaskContext);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0006ADC0 File Offset: 0x00068FC0
		public void CloseOldOABFileUncompressedFileStream(OABGeneratorTaskContext context)
		{
			if (this.oldOabFile != null && this.oldOabFile.UncompressedFileStream != null)
			{
				this.oldOabFile.UncompressedFileStream.Close();
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0006ADE7 File Offset: 0x00068FE7
		private static int MakeHRFromErrorCode(int errorCode)
		{
			return -2147024896 | errorCode;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0006ADF0 File Offset: 0x00068FF0
		private string GetHabRoot()
		{
			Organization orgContainer = this.perOrgAdSystemConfigSession.GetOrgContainer();
			if (orgContainer == null)
			{
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GetHabRoot: unable to get the organization container object", new object[0]);
				return null;
			}
			ADObjectId adobjectId;
			if (OABVariantConfigurationSettings.IsMultitenancyEnabled)
			{
				ExchangeOrganizationalUnit exchangeOrganizationalUnit = null;
				IEnumerable<ExchangeOrganizationalUnit> objects = new OrganizationalUnitIdParameter(orgContainer.OrganizationId.OrganizationalUnit).GetObjects<ExchangeOrganizationalUnit>(null, this.perOrgAdSystemConfigSession);
				IEnumerator<ExchangeOrganizationalUnit> enumerator = objects.GetEnumerator();
				if (enumerator.MoveNext())
				{
					exchangeOrganizationalUnit = enumerator.Current;
				}
				if (exchangeOrganizationalUnit == null)
				{
					OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GetHabRoot: OU could not be found", new object[0]);
					return null;
				}
				adobjectId = (ADObjectId)exchangeOrganizationalUnit[ExchangeOrganizationalUnitSchema.HABRootDepartmentLink];
			}
			else
			{
				adobjectId = orgContainer.HierarchicalAddressBookRoot;
			}
			if (adobjectId == null)
			{
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GetHabRoot: organization container object does not have HAB root set", new object[0]);
				return null;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, this.perOrgAdSystemConfigSession.SessionSettings, 698, "GetHabRoot", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\OABGenerator\\OABGenerator.cs");
			ADRecipient adrecipient = tenantOrRootOrgRecipientSession.Read(adobjectId);
			if (adrecipient == null)
			{
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GetHabRoot: unable to read the root HAB object", new object[0]);
				return null;
			}
			return adrecipient.LegacyExchangeDN;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0006AEF6 File Offset: 0x000690F6
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.fileSet != null)
				{
					this.fileSet.Dispose();
					this.fileSet = null;
				}
				if (this.propertyManager != null)
				{
					this.propertyManager.Dispose();
					this.propertyManager = null;
				}
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0006AF2F File Offset: 0x0006912F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OABGenerator>(this);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0006AF37 File Offset: 0x00069137
		private void RegisterStartTime()
		{
			this.stats.StartTimestamp = DateTime.UtcNow;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0006AF4C File Offset: 0x0006914C
		internal void RegisterEndTime(OABGeneratorTaskContext context)
		{
			this.offlineAddressBook.LastFailedTime = null;
			this.offlineAddressBook.LastTouchedTime = new DateTime?(this.stats.FinishTimestamp = DateTime.UtcNow);
			this.offlineAddressBook.LastNumberOfRecords = new int?(this.stats.TotalNumberOfRecords);
			this.offlineAddressBook.ManifestVersion = this.currentManifest.GetVersion();
			OABGeneratorMailboxData oabgeneratorMailboxData = context.MailboxData as OABGeneratorMailboxData;
			this.offlineAddressBook.LastGeneratingData = new OfflineAddressBookLastGeneratingData
			{
				MailboxGuid = ((oabgeneratorMailboxData.MailboxGuid == Guid.Empty) ? null : new Guid?(oabgeneratorMailboxData.MailboxGuid)),
				DatabaseGuid = ((oabgeneratorMailboxData.DatabaseGuid == Guid.Empty) ? null : new Guid?(oabgeneratorMailboxData.DatabaseGuid)),
				ServerFqdn = NativeHelpers.GetLocalComputerFqdn(false)
			};
			OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.RegisterEndTime: updating object {0} with LastTouchedTime={1}, LastNumberOfRecords={2}, ManifestVersion={3}", new object[]
			{
				this.offlineAddressBook.Id,
				this.offlineAddressBook.LastTouchedTime,
				this.offlineAddressBook.LastNumberOfRecords,
				this.offlineAddressBook.ManifestVersion
			});
			try
			{
				this.perOrgAdSystemConfigSession.Save(this.offlineAddressBook);
				this.stats.GenerationSucceeded = true;
			}
			catch (ADNoSuchObjectException ex)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "Update OAB object in AD failed due to exception, {0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0006B154 File Offset: 0x00069354
		private bool TryGetPreviousVersionOfFile(OABFile currentFile, bool openOldFile, out OABFile oldOabFile)
		{
			oldOabFile = null;
			bool flag = true;
			try
			{
				if (this.previousManifest == null || this.previousManifest.AddressLists == null)
				{
					OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.TryGetPreviousVersionOfFile: there is no usable previous manifest", new object[0]);
					flag = false;
					return flag;
				}
				OABManifestAddressList oabmanifestAddressList = Array.Find<OABManifestAddressList>(this.previousManifest.AddressLists, (OABManifestAddressList addressList) => StringComparer.OrdinalIgnoreCase.Equals(addressList.DN, currentFile.DnToUseInOABFile));
				if (oabmanifestAddressList == null || oabmanifestAddressList.Files == null)
				{
					OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.TryGetPreviousVersionOfFile: unable to find matching address list {0} in previous manifest file", new object[]
					{
						currentFile.DnToUseInOABFile
					});
					flag = false;
					return flag;
				}
				OABManifestFile oabmanifestFile = Array.Find<OABManifestFile>(oabmanifestAddressList.Files, (OABManifestFile file) => file.Type == currentFile.OABDataFileType && (currentFile.SequenceNumber == 1U || currentFile.SequenceNumber == file.Sequence));
				if (oabmanifestFile == null)
				{
					OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.TryGetPreviousVersionOfFile: unable to find Full OAB file entry in previous manifest file", new object[0]);
					flag = false;
					return flag;
				}
				oldOabFile = new OABFile(null, currentFile.OABDataFileType);
				try
				{
					oldOabFile.Guid = new Guid(oabmanifestAddressList.Id);
					oldOabFile.CompressedFileSize = oabmanifestFile.CompressedSize;
					oldOabFile.UncompressedFileSize = oabmanifestFile.UncompressedSize;
					oldOabFile.CompressedFileHash = oabmanifestFile.Hash;
					oldOabFile.SequenceNumber = oabmanifestFile.Sequence;
				}
				catch (SystemException ex)
				{
					OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.TryGetPreviousVersionOfFile: unable to get properties from previous manifest file due to exception: {0}", new object[]
					{
						ex
					});
					flag = false;
					return flag;
				}
				if (openOldFile)
				{
					string text = Path.Combine(this.oabDirectory, oabmanifestFile.FileName);
					string text2 = text.Replace(".lzx", ".flt");
					if (!File.Exists(text2) && File.Exists(text))
					{
						try
						{
							using (FileStream fileStream = new FileStream(text, FileMode.Open))
							{
								using (IOCostStream iocostStream = new IOCostStream(new OABDecompressStream(fileStream)))
								{
									using (IOCostStream iocostStream2 = new IOCostStream(new FileStream(text2, FileMode.CreateNew)))
									{
										using (new FileSystemPerformanceTracker("BeginGeneratingAddressListFiles.GetPreviousFiles", iocostStream, this.Stats))
										{
											using (new FileSystemPerformanceTracker("BeginGeneratingAddressListFiles.GetPreviousFiles", iocostStream2, this.Stats))
											{
												CopyStreamResult copyStreamResult = OABGenerator.streamCopier.CopyStream(iocostStream, iocostStream2);
												OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.TryGetPreviousVersionOfFile: decompressed file {0} to file {1}. Stats: {2}", new object[]
												{
													text,
													text2,
													copyStreamResult
												});
											}
										}
									}
								}
							}
						}
						catch (IOException ex2)
						{
							OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.TryGetPreviousVersionOfFile: unable to decompress previous compressed OAB file {0} due to exception: {1}. OABGen will be skipped now. Should be picked up during next work cycle", new object[]
							{
								text,
								ex2
							});
							throw new SkipException(ex2);
						}
					}
					try
					{
						oldOabFile.UncompressedFileStream = File.Open(text2, FileMode.Open, FileAccess.Read);
					}
					catch (IOException ex3)
					{
						OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.TryGetPreviousVersionOfFile: unable to open previous uncompressed OAB file {0} due to exception: {1}. OABGen will be skipped now. Should be picked up during next work cycle", new object[]
						{
							text2,
							ex3
						});
						throw new SkipException(ex3);
					}
				}
			}
			finally
			{
				if (!flag)
				{
					oldOabFile = null;
				}
			}
			return flag;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0006B530 File Offset: 0x00069730
		private void GenerateManifest()
		{
			List<OABManifestAddressList> list = new List<OABManifestAddressList>(this.addressListFiles.Count);
			for (int i = 0; i < this.addressListFiles.Count; i++)
			{
				List<OABManifestFile> list2 = new List<OABManifestFile>();
				OABFile oabfile = this.addressListFiles[i];
				list2.Add(OABGenerator.CreateOABManifestFile(oabfile));
				foreach (OABFile oabFile in this.addressListDiffFiles[i])
				{
					list2.Add(OABGenerator.CreateOABManifestFile(oabFile));
				}
				foreach (OABFile oabfile2 in this.templateFiles)
				{
					OABManifestFile oabmanifestFile = OABGenerator.CreateOABManifestFile(oabfile2);
					oabmanifestFile.Langid = new int?(oabfile2.Lcid);
					list2.Add(oabmanifestFile);
				}
				OABManifestAddressList item = new OABManifestAddressList
				{
					Id = oabfile.Guid.ToString(),
					DN = oabfile.DnToUseInOABFile,
					Name = oabfile.NameToUseInOABFile,
					Files = list2.ToArray()
				};
				list.Add(item);
			}
			OABManifest manifest = new OABManifest
			{
				AddressLists = list.ToArray()
			};
			this.currentManifest = manifest;
			this.WriteManifest(manifest);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0006B6BC File Offset: 0x000698BC
		private static OABManifestFile CreateOABManifestFile(OABFile oabFile)
		{
			return new OABManifestFile
			{
				Type = oabFile.OABDataFileType,
				Sequence = oabFile.SequenceNumber,
				Version = 32.ToString(),
				CompressedSize = oabFile.CompressedFileSize,
				UncompressedSize = oabFile.UncompressedFileSize,
				Hash = oabFile.CompressedFileHash,
				FileName = oabFile.PublishedFileName
			};
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0006B728 File Offset: 0x00069928
		private void TouchManifest()
		{
			this.currentManifest = this.previousManifest;
			this.WriteManifest(this.currentManifest);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0006B744 File Offset: 0x00069944
		private void WriteManifest(OABManifest manifest)
		{
			string text = Path.Combine(this.oabDirectory, "oab.xml");
			OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.WriteManifest: writing to manifest file {0}, version {1}:\n\r{2}", new object[]
			{
				text,
				manifest.GetVersion().ToString(),
				manifest
			});
			string text2 = Path.Combine(this.oabDirectory, Path.GetRandomFileName());
			using (FileStream fileStream = new FileStream(text2, FileMode.Create))
			{
				manifest.Serialize(fileStream);
			}
			this.SafeFileReplace(text2, text);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0006B7D0 File Offset: 0x000699D0
		private void PublishOabToDistribPoint()
		{
			using (new StopwatchPerformanceTracker("Publish.PublishToDistribPoint", this.stats))
			{
				using (new CpuPerformanceTracker("Publish.PublishToDistribPoint", this.stats))
				{
					for (int i = 0; i < this.addressListFiles.Count; i++)
					{
						this.PublishFileToDistribPoint(this.addressListFiles[i]);
						if (this.addressListDiffFiles[i].Count > 0)
						{
							this.PublishFileToDistribPoint(this.addressListDiffFiles[i][0]);
						}
						foreach (OABFile oabfile in this.templateFiles)
						{
							oabfile.Guid = this.addressListFiles[i].Guid;
							oabfile.SequenceNumber = this.addressListFiles[i].SequenceNumber;
							if (!OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled)
							{
								this.PublishFileToDistribPoint(oabfile);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0006B910 File Offset: 0x00069B10
		private void CleanupTemporaryAndOldFiles(OABGeneratorTaskContext context)
		{
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			uint num5 = 0U;
			for (int i = 0; i < this.addressListFiles.Count; i++)
			{
				for (uint num6 = this.addressListFiles[i].SequenceNumber - 1U; num6 > 0U; num6 -= 1U)
				{
					if (this.addressListFiles[i].PublishedFileName != null)
					{
						if (this.DeleteOldFile(this.addressListFiles[i].PublishedFileName, num6))
						{
							num += 1U;
						}
						else
						{
							num5 += 1U;
						}
					}
					if (this.addressListFiles[i].PublishedFileName != null)
					{
						if (this.DeleteOldFile(this.addressListFiles[i].PublishedFileName.Replace(".lzx", ".flt"), num6))
						{
							num2 += 1U;
						}
						else
						{
							num5 += 1U;
						}
					}
					this.addressListFiles[i].OABDataFileType = OABDataFileType.Diff;
					uint num7 = this.CalculateOldestSequenceNumberToKeep(this.addressListFiles[i].SequenceNumber);
					for (uint num8 = num7 - 1U; num8 >= 2U; num8 -= 1U)
					{
						if (this.DeleteOldFile(this.addressListFiles[i].PublishedFileName, num8))
						{
							num3 += 1U;
						}
						else
						{
							num5 += 1U;
						}
					}
					this.addressListFiles[i].OABDataFileType = OABDataFileType.Full;
					if (!OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled)
					{
						foreach (OABFile oabfile in this.templateFiles)
						{
							oabfile.Guid = this.addressListFiles[i].Guid;
							oabfile.SequenceNumber = num6;
							if (this.DeleteOldFile(oabfile.PublishedFileName, num6))
							{
								num4 += 1U;
							}
							else
							{
								num5 += 1U;
							}
						}
					}
				}
			}
			OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.CleanupTemporaryAndOldFiles: number of deleted files: compressed files: {0}, uncompressed files: {1}, diff files: {2}, template files: {3}, failed deletes: {4}", new object[]
			{
				num,
				num2,
				num3,
				num4,
				num5
			});
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0006BB44 File Offset: 0x00069D44
		private bool DeleteOldFile(string filename, uint sequenceNumber)
		{
			string text = Path.Combine(this.oabDirectory, string.Format("{0}{1}{2}", filename.Substring(0, filename.LastIndexOf('-') + 1), sequenceNumber, Path.GetExtension(filename)));
			bool result;
			try
			{
				File.Delete(text);
				result = true;
			}
			catch (IOException ex)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.DeleteOldFile: unable to delete file {0} due exception: {1}", new object[]
				{
					text,
					ex
				});
				result = false;
			}
			return result;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0006BBC0 File Offset: 0x00069DC0
		private void PublishFileToDistribPoint(OABFile oabFile)
		{
			string text = Path.Combine(this.oabDirectory, oabFile.PublishedFileName);
			if (oabFile.IsPublished(this.oabDirectory))
			{
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.PublishFileToDistribPoint: file already published: {0}", new object[]
				{
					text
				});
				return;
			}
			File.Delete(text);
			if (oabFile.OABDataFileType == OABDataFileType.TemplateWin || oabFile.OABDataFileType == OABDataFileType.TemplateMac)
			{
				this.CopyFileWithRetries(oabFile.CompressedFileStream, text);
			}
			else if (oabFile.OABDataFileType == OABDataFileType.Full)
			{
				this.MoveFileWithRetriesAndDetachFromFileSet(oabFile.CompressedFileStream, text);
				string text2 = Path.Combine(this.oabDirectory, text.Replace(".lzx", ".flt"));
				File.Delete(text2);
				this.MoveFileWithRetriesAndDetachFromFileSet(oabFile.UncompressedFileStream, text2);
			}
			else
			{
				this.MoveFileWithRetriesAndDetachFromFileSet(oabFile.UncompressedFileStream, text);
			}
			OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.PublishFileToDistribPoint: published file: {0}", new object[]
			{
				text
			});
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0006BC98 File Offset: 0x00069E98
		private void CreateOABDirectory()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(this.oabDirectory);
			if (!directoryInfo.Exists)
			{
				DirectoryInfo directoryInfo2 = new DirectoryInfo(Globals.OabFolderPath);
				DirectorySecurity accessControl = directoryInfo2.GetAccessControl();
				accessControl.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier("AU"), FileSystemRights.Read, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
				Directory.CreateDirectory(this.oabDirectory, accessControl);
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.CreateOABDirectory: created new directory {0}", new object[]
				{
					this.oabDirectory
				});
				return;
			}
			OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.CreateOABDirectory: using directory {0} created on {1}", new object[]
			{
				this.oabDirectory,
				directoryInfo.CreationTime
			});
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0006BD40 File Offset: 0x00069F40
		private void LinkToTemplateFiles()
		{
			string text = Path.Combine(Globals.OabFolderPath, "templates");
			string text2 = Path.Combine(this.oabDirectory, "templates");
			OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.LinkToTemplateFiles: linking directory {0} to {1}", new object[]
			{
				text,
				text2
			});
			SymbolicLink.CreateDirectorySymbolicLink(text2, text);
			this.templateFiles.AddRange(OABGenerator.PredefinedTemplates);
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0006BDA1 File Offset: 0x00069FA1
		private static IEnumerable<OABFile> PredefinedTemplates
		{
			get
			{
				if (OABGenerator.predefinedTemplates == null)
				{
					OABGenerator.predefinedTemplates = OABGenerator.GetPredefinedTemplates();
				}
				return OABGenerator.predefinedTemplates;
			}
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0006BDBC File Offset: 0x00069FBC
		private static IEnumerable<OABFile> GetPredefinedTemplates()
		{
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.GetPredefinedTemplates: start", new object[0]);
			string path = Path.Combine(Globals.OabFolderPath, "templates");
			string text = Path.Combine(path, "predefinedTemplateManifest.xml");
			List<OABFile> list = new List<OABFile>(50);
			using (FileStream fileStream = new FileStream(text, FileMode.Open))
			{
				OABManifest oabmanifest = OABManifest.Deserialize(fileStream);
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GetPredefinedTemplates: loaded predefined templates from file {0}:\n\r{1}", new object[]
				{
					text,
					oabmanifest
				});
				if (oabmanifest.AddressLists != null && oabmanifest.AddressLists.Length >= 1)
				{
					foreach (OABManifestFile oabmanifestFile in oabmanifest.AddressLists[0].Files)
					{
						OABFile oabfile = new OABFile(null, oabmanifestFile.Type);
						oabfile.SequenceNumber = oabmanifestFile.Sequence;
						oabfile.CompressedFileHash = oabmanifestFile.Hash;
						oabfile.UncompressedFileSize = oabmanifestFile.UncompressedSize;
						oabfile.CompressedFileSize = oabmanifestFile.CompressedSize;
						oabfile.Lcid = oabmanifestFile.Langid.Value;
						oabfile.PublishedFileName = oabmanifestFile.FileName;
						OABLogger.LogRecord(TraceType.DebugTrace, "Adding file entry from predefinedTemplateManifest.xml: {0}", new object[]
						{
							oabfile.PublishedFileName
						});
						list.Add(oabfile);
					}
				}
			}
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.GetPredefinedTemplates: end", new object[0]);
			return list;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0006BF48 File Offset: 0x0006A148
		private void GenerateTemplateFiles()
		{
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.GenerateTemplateFiles: start", new object[0]);
			this.abortProcessingOnShutdown();
			using (new StopwatchPerformanceTracker("GenerateOrLinkTemplateFiles.GenerateTemplateFiles", this.stats))
			{
				using (new CpuPerformanceTracker("GenerateOrLinkTemplateFiles.GenerateTemplateFiles", this.stats))
				{
					ADPagedReader<ADRawEntry> adpagedReader = this.perOrgAdSystemConfigSession.FindPagedADRawEntry(this.perOrgAdSystemConfigSession.GetOrgContainerId().GetDescendantId(AddressTemplate.ContainerId), QueryScope.OneLevel, null, null, 0, Globals.RawEntryPropertyDefinitions);
					foreach (ADRawEntry adrawEntry in adpagedReader)
					{
						OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GenerateTemplateFiles: processing template container: {0}", new object[]
						{
							adrawEntry.Id
						});
						OABDataFileType[] array = new OABDataFileType[]
						{
							OABDataFileType.TemplateWin,
							OABDataFileType.TemplateMac
						};
						foreach (OABDataFileType oabDataFileType in array)
						{
							TemplateFileGenerator templateFileGenerator = new TemplateFileGenerator(this.perOrgAdSystemConfigSession, adrawEntry, oabDataFileType, this.stats);
							DateTime utcNow = DateTime.UtcNow;
							OABFile oabfile = templateFileGenerator.GenerateTemplateFile(this.fileSet);
							this.stats.IODuration += DateTime.UtcNow.Subtract(utcNow);
							if (oabfile != null)
							{
								this.templateFiles.Add(oabfile);
							}
						}
					}
				}
			}
			OABLogger.LogRecord(TraceType.FunctionTrace, "OABGenerator.GenerateTemplateFiles: end", new object[0]);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0006C120 File Offset: 0x0006A320
		private bool GenerateDiffFile(OABFile addressListFile, OABFile oldFile)
		{
			this.abortProcessingOnShutdown();
			bool result;
			using (new StopwatchPerformanceTracker("FinishGeneratingAddressListFiles.GenerateDiffFiles", this.stats))
			{
				using (new CpuPerformanceTracker("FinishGeneratingAddressListFiles.GenerateDiffFiles", this.stats))
				{
					List<OABFile> list = new List<OABFile>();
					this.addressListDiffFiles.Add(list);
					OABFile oabfile = null;
					if (oldFile != null)
					{
						DiffFileGenerator diffFileGenerator = new DiffFileGenerator(oldFile, addressListFile, this.abortProcessingOnShutdown, this.stats);
						oabfile = diffFileGenerator.GenerateDiffFile(this.fileSet);
						if (oabfile != null)
						{
							OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GenerateDiffFile: generated diff file: {0}", new object[]
							{
								oabfile.PublishedFileName
							});
							list.Add(oabfile);
						}
						else
						{
							OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GenerateDiffFile: no diff generated, new and old OAB files are the same.", new object[0]);
						}
					}
					else
					{
						OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.GenerateDiffFile: no previous OAB file, no diff to generate", new object[0]);
					}
					this.FindPreviousDiffFiles(addressListFile, list);
					result = (oldFile == null || oabfile != null);
				}
			}
			return result;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0006C234 File Offset: 0x0006A434
		private uint CalculateOldestSequenceNumberToKeep(uint currentSequenceNumber)
		{
			if (this.offlineAddressBook.DiffRetentionPeriod == null || this.offlineAddressBook.DiffRetentionPeriod.Value.IsUnlimited)
			{
				return 2U;
			}
			int num = (T)this.offlineAddressBook.DiffRetentionPeriod.Value;
			TimeSpan timeSpan = AssistantConfiguration.OABGeneratorWorkCycle.Read();
			uint num2 = (uint)(num * 24 * 60 / (int)((uint)timeSpan.TotalMinutes));
			if (currentSequenceNumber >= num2 + 2U)
			{
				return currentSequenceNumber - num2 + 1U;
			}
			return 2U;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
		private void FindPreviousDiffFiles(OABFile addressListFile, List<OABFile> previousDiffFiles)
		{
			this.abortProcessingOnShutdown();
			uint sequenceNumber = addressListFile.SequenceNumber;
			addressListFile.OABDataFileType = OABDataFileType.Diff;
			uint num = this.CalculateOldestSequenceNumberToKeep(sequenceNumber);
			for (uint num2 = sequenceNumber; num2 >= num; num2 -= 1U)
			{
				addressListFile.SequenceNumber = num2;
				OABFile item;
				if (this.TryGetPreviousVersionOfFile(addressListFile, false, out item))
				{
					previousDiffFiles.Add(item);
				}
			}
			addressListFile.SequenceNumber = sequenceNumber;
			addressListFile.OABDataFileType = OABDataFileType.Full;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0006C328 File Offset: 0x0006A528
		private void UploadFilesToMailbox(MailboxSession mailboxSession)
		{
			using (new StopwatchPerformanceTracker("Publish.UploadFilesToMailbox", this.stats))
			{
				using (new CpuPerformanceTracker("Publish.UploadFilesToMailbox", this.stats))
				{
					using (new StorePerformanceTracker("Publish.UploadFilesToMailbox", this.stats))
					{
						List<string> list = new List<string>(this.addressListFiles.Count);
						list.Add(Path.Combine(this.oabDirectory, "oab.xml"));
						for (int i = 0; i < this.addressListFiles.Count; i++)
						{
							list.Add(Path.Combine(this.oabDirectory, this.addressListFiles[i].PublishedFileName));
							foreach (OABFile oabfile in this.addressListDiffFiles[i])
							{
								list.Add(Path.Combine(this.oabDirectory, oabfile.PublishedFileName));
							}
						}
						if (!OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled)
						{
							foreach (OABFile oabfile2 in this.templateFiles)
							{
								list.Add(Path.Combine(this.oabDirectory, oabfile2.PublishedFileName));
							}
						}
						foreach (string text in list)
						{
							OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.UploadFilesToMailbox: selected OAB file {0} to upload to the mailbox", new object[]
							{
								text
							});
						}
						string fileSetId = this.offlineAddressBook.ExchangeObjectId.ToString();
						try
						{
							this.mailboxFileStore.Upload(list, fileSetId, mailboxSession, this.abortProcessingOnShutdown);
						}
						catch (LocalizedException ex)
						{
							OABLogger.LogRecord(TraceType.ErrorTrace, "OABFileManager.UploadFilesToMailbox: unable to upload OAB files to mailbox due to exception: {0}", new object[]
							{
								ex
							});
							try
							{
								this.mailboxFileStore.RemoveAll(fileSetId, mailboxSession);
							}
							catch (LocalizedException ex2)
							{
								OABLogger.LogRecord(TraceType.ErrorTrace, "OABFileManager.UploadFilesToMailbox: unable to remove old OAB files from the mailbox due to exception: {0}", new object[]
								{
									ex2
								});
							}
						}
						OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.UploadFilesToMailbox: uploaded {0} OAB files to the mailbox", new object[]
						{
							list.Count
						});
					}
				}
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0006C648 File Offset: 0x0006A848
		private void DownloadFilesFromMailbox(MailboxSession mailboxSession)
		{
			this.abortProcessingOnShutdown();
			using (new StopwatchPerformanceTracker("PrepareFilesForOABGeneration.DownloadFilesFromMailbox", this.stats))
			{
				using (new CpuPerformanceTracker("PrepareFilesForOABGeneration.DownloadFilesFromMailbox", this.stats))
				{
					using (new StorePerformanceTracker("PrepareFilesForOABGeneration.DownloadFilesFromMailbox", this.stats))
					{
						string fileSetId = this.offlineAddressBook.ExchangeObjectId.ToString();
						FileSetItem current;
						try
						{
							current = this.mailboxFileStore.GetCurrent(fileSetId, mailboxSession);
						}
						catch (LocalizedException ex)
						{
							OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.DownloadFilesFromMailbox: unable to find OAB files from mailbox due to exception: {0}", new object[]
							{
								ex
							});
							return;
						}
						if (current == null)
						{
							OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.DownloadFilesFromMailbox: not downloading OAB files from the mailbox because there are OAB files in the mailbox.", new object[0]);
						}
						else
						{
							string text = Path.Combine(this.oabDirectory, "oab.xml");
							if (File.Exists(text))
							{
								OABManifest oabmanifest = OABManifest.LoadFromFile(text);
								OABManifest oabmanifest2 = OABManifest.LoadFromMailbox(fileSetId, mailboxSession);
								if (oabmanifest != null && oabmanifest2 != null)
								{
									OfflineAddressBookManifestVersion version = oabmanifest.GetVersion();
									OfflineAddressBookManifestVersion version2 = oabmanifest2.GetVersion();
									if (version != null && version.Equals(version2))
									{
										OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.DownloadFilesFromMailbox: not downloading OAB files from the mailbox because the manifest on disk is the same version as the one in the mailbox, version={0}", new object[]
										{
											version.ToString()
										});
										return;
									}
									OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.DownloadFilesFromMailbox: downloading OAB files from the mailbox because the manifest on disk doesn't match. MailboxVersion={0}, DiskVersion={1}", new object[]
									{
										version2.ToString(),
										version.ToString()
									});
								}
							}
							string text2 = null;
							List<string> list;
							try
							{
								list = this.mailboxFileStore.Download(current, mailboxSession, Globals.AlternateTempFilePath, true, this.abortProcessingOnShutdown);
								if (list != null && list.Count > 0)
								{
									text2 = Path.GetDirectoryName(list[0]);
								}
							}
							catch (LocalizedException ex2)
							{
								OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.DownloadFilesFromMailbox: unable to download OAB files from mailbox due exception: {0}", new object[]
								{
									ex2
								});
								return;
							}
							try
							{
								OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.DownloadFilesFromMailbox: downloaded {0} OAB files from the mailbox.", new object[]
								{
									list.Count
								});
								if (!this.IsValidFileSet(list))
								{
									OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.DownloadFilesFromMailbox: downloaded OAB files from mailbox are corrupt", new object[0]);
								}
								else
								{
									OABGenerator.DeleteFromDirectory(this.oabDirectory, false);
									while (list.Count > 0)
									{
										string text3 = list[0];
										string text4 = Path.Combine(this.oabDirectory, Path.GetFileName(text3));
										this.SafeFileReplace(text3, text4);
										OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.DownloadFilesFromMailbox: moving OAB file from {0} to distribution point {1}.", new object[]
										{
											text3,
											text4
										});
										list.RemoveAt(0);
									}
								}
							}
							finally
							{
								if (!string.IsNullOrEmpty(text2))
								{
									OABGenerator.DeleteFromDirectory(text2, true);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0006C9F8 File Offset: 0x0006ABF8
		private static void DeleteFromDirectory(string directoryPath, bool deleteDirectory)
		{
			if (!Directory.Exists(directoryPath))
			{
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.DeleteFromDirectory: directory doesn't exist '{0}'.", new object[]
				{
					directoryPath
				});
				return;
			}
			try
			{
				OABGenerator.RetryOnIOException(delegate
				{
					if (deleteDirectory)
					{
						Directory.Delete(directoryPath, true);
						return;
					}
					foreach (string path in Directory.EnumerateFiles(directoryPath))
					{
						File.Delete(path);
					}
				}, 3, 3000, false);
			}
			catch (IOException ex)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.DeleteFromDirectory: failed to delete files from directory '{0}' after {1} attempts because of {2}:{3}", new object[]
				{
					directoryPath,
					3,
					ex.GetType(),
					ex.Message
				});
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0006CAB4 File Offset: 0x0006ACB4
		private void MoveFileWithRetriesAndDetachFromFileSet(FileStream sourceFileStream, string destinationFile)
		{
			sourceFileStream.Close();
			this.MoveFileWithRetries(sourceFileStream.Name, destinationFile);
			this.fileSet.Detach(sourceFileStream);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0006CAD5 File Offset: 0x0006ACD5
		private void CopyFileWithRetries(FileStream sourceFileStream, string destinationFile)
		{
			sourceFileStream.Close();
			this.CopyFileWithRetries(sourceFileStream.Name, destinationFile);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0006CB08 File Offset: 0x0006AD08
		private void CopyFileWithRetries(string sourceFile, string destinationFile)
		{
			try
			{
				OABGenerator.RetryOnIOException(delegate
				{
					File.Copy(sourceFile, destinationFile);
				}, 3, 3000, false);
			}
			catch (IOException ex)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.CopyFileWithRetries: failed to copy file ['{0}' => '{1}'] after {2} attempts because of {3}:{4}", new object[]
				{
					sourceFile,
					destinationFile,
					3,
					ex.GetType(),
					ex.Message
				});
				throw;
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0006CBBC File Offset: 0x0006ADBC
		private void MoveFileWithRetries(string sourceFile, string destinationFile)
		{
			try
			{
				OABGenerator.RetryOnIOException(delegate
				{
					File.Move(sourceFile, destinationFile);
				}, 3, 3000, true);
			}
			catch (IOException ex)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.MoveFileWithRetries: failed to move file ['{0}' => '{1}] after {2} attempts because of {3}:{4}", new object[]
				{
					sourceFile,
					destinationFile,
					3,
					ex.GetType(),
					ex.Message
				});
				throw;
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0006CC54 File Offset: 0x0006AE54
		private void SafeFileReplace(string sourceFile, string destinationFile)
		{
			if (File.Exists(destinationFile))
			{
				FileInfo fileInfo = new FileInfo(sourceFile);
				DateTime creationTimeUtc = fileInfo.CreationTimeUtc;
				string text = Path.Combine(this.oabDirectory, Path.GetRandomFileName());
				this.MoveFileWithRetries(destinationFile, text);
				this.fileSet.Attach(text);
				this.MoveFileWithRetries(sourceFile, destinationFile);
				FileInfo fileInfo2 = new FileInfo(destinationFile);
				fileInfo2.CreationTimeUtc = creationTimeUtc;
				return;
			}
			this.MoveFileWithRetries(sourceFile, destinationFile);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0006CCBC File Offset: 0x0006AEBC
		private static void RetryOnIOException(Action del, int numberOfAttempts = 3, int pauseMilliseconds = 3000, bool onlyRetryOnSharingViolation = false)
		{
			int num = 0;
			try
			{
				IL_02:
				num++;
				del();
			}
			catch (IOException ex)
			{
				if (num >= numberOfAttempts || (onlyRetryOnSharingViolation && ex.HResult != OABGenerator.ErrorSharingViolation))
				{
					throw;
				}
				OABLogger.LogRecord(TraceType.DebugTrace, "OABGenerator.RetryOnIOException: sleeping for {0}ms after exception on attempt {1} of {2}: {3}", new object[]
				{
					pauseMilliseconds,
					num,
					numberOfAttempts,
					ex.Message
				});
				Thread.Sleep(pauseMilliseconds);
				pauseMilliseconds *= 2;
				goto IL_02;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0006CD78 File Offset: 0x0006AF78
		private bool IsValidFileSet(List<string> files)
		{
			if (files == null)
			{
				return false;
			}
			string text = files.Find((string filePath) => StringComparer.OrdinalIgnoreCase.Equals(Path.GetFileName(filePath), "oab.xml"));
			if (text == null)
			{
				OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.IsValidFileSet: file set downloaded from mailbox is not valid because manifest file {0} is missing.", new object[]
				{
					"oab.xml"
				});
				return false;
			}
			using (FileStream fileStream = new FileStream(text, FileMode.Open))
			{
				OABManifest oabmanifest = OABManifest.Deserialize(fileStream);
				foreach (OABManifestAddressList oabmanifestAddressList in oabmanifest.AddressLists)
				{
					OABManifestFile[] files2 = oabmanifestAddressList.Files;
					for (int j = 0; j < files2.Length; j++)
					{
						OABManifestFile file = files2[j];
						if (!OABVariantConfigurationSettings.IsSharedTemplateFilesEnabled || (file.Type != OABDataFileType.TemplateMac && file.Type != OABDataFileType.TemplateWin))
						{
							if (-1 == files.FindIndex((string filePath) => OABGenerator.AreFileNamesEqual(filePath, file.FileName)))
							{
								OABLogger.LogRecord(TraceType.ErrorTrace, "OABGenerator.IsValidFileSet: file set downloaded from mailbox is not valid because file {0} is present in manifest but not present in downloaded file set.", new object[]
								{
									file.FileName
								});
								return false;
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0006CECC File Offset: 0x0006B0CC
		private static bool AreFileNamesEqual(string filePath1, string filePath2)
		{
			return StringComparer.OrdinalIgnoreCase.Equals(Path.GetFileName(filePath1), Path.GetFileName(filePath2));
		}

		// Token: 0x04000B35 RID: 2869
		private const int DefaultPauseMilliseconds = 3000;

		// Token: 0x04000B36 RID: 2870
		private const int DefaultNumberOfAttempts = 3;

		// Token: 0x04000B37 RID: 2871
		private static readonly Trace Tracer = ExTraceGlobals.AssistantTracer;

		// Token: 0x04000B38 RID: 2872
		private static readonly StreamCopier streamCopier = new StreamCopier(8192);

		// Token: 0x04000B39 RID: 2873
		private static readonly int ErrorSharingViolation = OABGenerator.MakeHRFromErrorCode(32);

		// Token: 0x04000B3A RID: 2874
		private static IEnumerable<OABFile> predefinedTemplates;

		// Token: 0x04000B3B RID: 2875
		private readonly Action abortProcessingOnShutdown;

		// Token: 0x04000B3C RID: 2876
		private readonly OfflineAddressBook offlineAddressBook;

		// Token: 0x04000B3D RID: 2877
		private readonly IConfigurationSession perOrgAdSystemConfigSession;

		// Token: 0x04000B3E RID: 2878
		private readonly SecurityIdentifier mailboxSid;

		// Token: 0x04000B3F RID: 2879
		private readonly string mailboxDomain;

		// Token: 0x04000B40 RID: 2880
		private MailboxFileStore mailboxFileStore;

		// Token: 0x04000B41 RID: 2881
		private FileSet fileSet;

		// Token: 0x04000B42 RID: 2882
		private Queue<ADObjectId> addressLists;

		// Token: 0x04000B43 RID: 2883
		private ADObjectId currentAddressList;

		// Token: 0x04000B44 RID: 2884
		private AddressListFileGenerator addressListFileGenerator;

		// Token: 0x04000B45 RID: 2885
		private GenerationStats stats;

		// Token: 0x04000B46 RID: 2886
		private PropertyManager propertyManager;

		// Token: 0x04000B47 RID: 2887
		private List<OABFile> addressListFiles;

		// Token: 0x04000B48 RID: 2888
		private List<List<OABFile>> addressListDiffFiles;

		// Token: 0x04000B49 RID: 2889
		private List<OABFile> templateFiles;

		// Token: 0x04000B4A RID: 2890
		private string oabDirectory;

		// Token: 0x04000B4B RID: 2891
		private bool changed;

		// Token: 0x04000B4C RID: 2892
		private OABFile addressListFile;

		// Token: 0x04000B4D RID: 2893
		private OABFile oldOabFile;

		// Token: 0x04000B4E RID: 2894
		private OABManifest previousManifest;

		// Token: 0x04000B4F RID: 2895
		private OABManifest currentManifest;

		// Token: 0x04000B50 RID: 2896
		private string habRootLegacyDN;
	}
}
