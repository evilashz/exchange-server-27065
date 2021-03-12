using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x02000018 RID: 24
	internal abstract class PublishingSessionBase : DisposableBase, IPublishingSession, IDisposeTrackable, IDisposable
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00006C88 File Offset: 0x00004E88
		private IUMPromptStorage PromptStore
		{
			get
			{
				if (this.promptStorage == null)
				{
					this.InitializePromptStorage(this.config.OrganizationId);
				}
				return this.promptStorage;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006CA9 File Offset: 0x00004EA9
		protected PublishingSessionBase(string userName, ADConfigurationObject config)
		{
			this.userName = userName;
			this.config = config;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00006CD5 File Offset: 0x00004ED5
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00006CDD File Offset: 0x00004EDD
		public TimeSpan TestHookKeepOrphanFilesInterval
		{
			get
			{
				return this.keepOrphanFilesInterval;
			}
			set
			{
				this.keepOrphanFilesInterval = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006CE6 File Offset: 0x00004EE6
		protected ADConfigurationObject ConfigurationObject
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001CD RID: 461
		protected abstract UMDialPlan DialPlan { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006CEE File Offset: 0x00004EEE
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006CF6 File Offset: 0x00004EF6
		protected string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName = value;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00006D00 File Offset: 0x00004F00
		public virtual void Upload(string source, string destinationName)
		{
			PIIMessage data = PIIMessage.Create(PIIType._User, this.userName);
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, data, "PublishingSessionBase.Upload(_User, {0}, {1}).", new object[]
			{
				source,
				destinationName
			});
			if (source == null)
			{
				throw new ArgumentNullException();
			}
			FileInfo fileInfo = new FileInfo(source);
			if (!fileInfo.Exists)
			{
				throw new SourceFileNotFoundException(fileInfo.FullName);
			}
			try
			{
				this.UpdatePromptChangeKey(Guid.NewGuid());
				string audioBytes = null;
				using (ITempFile tempFile = this.ValidateAndCompressForUpload(fileInfo))
				{
					using (FileStream fileStream = new FileStream(tempFile.FilePath, FileMode.Open, FileAccess.Read))
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Uploading from {0} to {1}.", new object[]
						{
							fileInfo.FullName,
							destinationName
						});
						audioBytes = CommonUtil.GetBase64StringFromStream(fileStream);
					}
				}
				this.PromptStore.CreatePrompt(destinationName, audioBytes);
				this.RemoveOrphanEntries();
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new PublishingPointException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006E2C File Offset: 0x0000502C
		public virtual void Download(string sourceName, string destination)
		{
			PIIMessage data = PIIMessage.Create(PIIType._User, this.userName);
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, data, "PublishingSessionBase.Download(_User, {0}, {1}).", new object[]
			{
				sourceName,
				destination
			});
			if (sourceName == null || destination == null)
			{
				throw new ArgumentNullException();
			}
			FileInfo fileInfo = new FileInfo(destination);
			if (fileInfo.Exists)
			{
				throw new DestinationAlreadyExistsException(fileInfo.FullName);
			}
			FileInfo fileInfo2 = new FileInfo(sourceName);
			try
			{
				string prompt = this.PromptStore.GetPrompt(sourceName);
				if (fileInfo2.Extension.Equals(".wma", StringComparison.OrdinalIgnoreCase))
				{
					this.WriteToWmaFile(fileInfo.FullName, prompt);
				}
				else
				{
					if (!fileInfo2.Extension.Equals(".wav", StringComparison.OrdinalIgnoreCase))
					{
						throw new SourceFileNotFoundException(sourceName);
					}
					this.WriteToWavFile(fileInfo.FullName, prompt);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Downloaded from {0} to {1}.", new object[]
				{
					sourceName,
					fileInfo.FullName
				});
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new PublishingPointException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006F48 File Offset: 0x00005148
		public virtual ITempWavFile DownloadAsWav(string sourceName)
		{
			PIIMessage.Create(PIIType._User, this.userName);
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "PublishingSessionBase.Download(_User, {0}, into a temp file).", new object[]
			{
				sourceName
			});
			if (sourceName == null)
			{
				throw new ArgumentNullException("sourceName");
			}
			new FileInfo(sourceName);
			ITempWavFile result;
			try
			{
				string prompt = this.PromptStore.GetPrompt(sourceName);
				ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
				this.WriteToWavFile(tempWavFile.FilePath, prompt);
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Downloaded from {0} to {1}.", new object[]
				{
					sourceName,
					tempWavFile.FilePath
				});
				result = tempWavFile;
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new PublishingPointException(ex.Message, ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000700C File Offset: 0x0000520C
		public void DownloadAllAsWma(DirectoryInfo directory)
		{
			if (!directory.Exists)
			{
				throw new DirectoryNotFoundException(directory.FullName);
			}
			try
			{
				string[] promptNames = this.PromptStore.GetPromptNames();
				foreach (string text in promptNames)
				{
					string text2 = Path.Combine(directory.FullName, text);
					text2 += ".wma";
					string prompt = this.PromptStore.GetPrompt(text);
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Download from {0} to {1}.", new object[]
					{
						text,
						text2
					});
					this.WriteToWmaFile(text2, prompt);
				}
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new PublishingPointException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000070D8 File Offset: 0x000052D8
		public void Delete()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "PublishingSessionBase.Delete().", new object[0]);
			try
			{
				this.PromptStore.DeleteAllPrompts();
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new DeleteContentException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007130 File Offset: 0x00005330
		protected static void AddIfNotEmpty(IDictionary fileList, string fileName)
		{
			if (!string.IsNullOrEmpty(fileName) && !fileList.Contains(fileName))
			{
				fileList.Add(fileName, null);
			}
		}

		// Token: 0x060001D6 RID: 470
		protected abstract void UpdatePromptChangeKey(Guid guid);

		// Token: 0x060001D7 RID: 471 RVA: 0x0000714B File Offset: 0x0000534B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "PublishingSessionBase.Dispose() called.", new object[0]);
				if (this.PromptStore != null)
				{
					this.PromptStore.Dispose();
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007179 File Offset: 0x00005379
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PublishingSessionBase>(this);
		}

		// Token: 0x060001D9 RID: 473
		protected abstract void AddConfiguredFiles(IDictionary fileList);

		// Token: 0x060001DA RID: 474 RVA: 0x00007184 File Offset: 0x00005384
		private static bool IsPublishingPointException(Exception e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, null, "IsPublishingPointException e='{0}'", new object[]
			{
				e
			});
			return e is IOException || e is StoragePermanentException || e is StorageTransientException || e is ADTransientException || e is COMException || e is InvalidWaveFormatException || e is InvalidWmaFormatException || e is EWSUMMailboxAccessException;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000071F0 File Offset: 0x000053F0
		private void WriteToWmaFile(string filePath, string audioBytes)
		{
			using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
			{
				CommonUtil.CopyBase64StringToSteam(audioBytes, fileStream);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000722C File Offset: 0x0000542C
		private void WriteToWavFile(string filePath, string audioBytes)
		{
			using (ITempFile tempFile = TempFileFactory.CreateTempWmaFile())
			{
				using (FileStream fileStream = new FileStream(tempFile.FilePath, FileMode.Create, FileAccess.ReadWrite))
				{
					CommonUtil.CopyBase64StringToSteam(audioBytes, fileStream);
					using (PcmWriter pcmWriter = new PcmWriter(filePath, WaveFormat.Pcm8WaveFormat))
					{
						using (WmaReader wmaReader = new WmaReader(tempFile.FilePath))
						{
							byte[] array = new byte[wmaReader.SampleSize * 2];
							int count;
							while ((count = wmaReader.Read(array, array.Length)) > 0)
							{
								pcmWriter.Write(array, count);
							}
						}
					}
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007300 File Offset: 0x00005500
		private void InitializePromptStorage(OrganizationId orgId)
		{
			try
			{
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromOrganizationId(orgId, null);
				ADUser umdataStorageMailbox = iadrecipientLookup.GetUMDataStorageMailbox();
				this.promptStorage = InterServerMailboxAccessor.GetUMPromptStoreAccessor(umdataStorageMailbox, this.ConfigurationObject.Guid);
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new PublishingPointException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007360 File Offset: 0x00005560
		private void RemoveOrphanEntries()
		{
			IDictionary dictionary = new Hashtable(StringComparer.OrdinalIgnoreCase);
			this.AddConfiguredFiles(dictionary);
			string[] promptNames = this.PromptStore.GetPromptNames(this.keepOrphanFilesInterval);
			List<string> list = new List<string>(promptNames.Length);
			foreach (string text in promptNames)
			{
				if (!dictionary.Contains(text))
				{
					list.Add(text);
				}
			}
			this.PromptStore.DeletePrompts(list.ToArray());
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000073D8 File Offset: 0x000055D8
		private ITempFile ValidateAndCompressForUpload(FileInfo sourceFile)
		{
			string a;
			if ((a = sourceFile.Extension.ToLowerInvariant()) != null)
			{
				ITempFile tempFile;
				if (!(a == ".wav"))
				{
					if (!(a == ".wma"))
					{
						goto IL_41;
					}
					tempFile = this.ValidateAndCompressFromWma(sourceFile);
				}
				else
				{
					tempFile = this.ValidateAndCompressFromPcm(sourceFile);
				}
				FileInfo fileInfo = new FileInfo(tempFile.FilePath);
				if (fileInfo.Length > 507392L)
				{
					throw new UnsupportedCustomGreetingSizeFormatException(5L.ToString(CultureInfo.InvariantCulture));
				}
				return tempFile;
			}
			IL_41:
			throw new InvalidOperationException();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000745C File Offset: 0x0000565C
		private ITempFile ValidateAndCompressFromPcm(FileInfo sourceFile)
		{
			bool flag = false;
			ITempFile tempFile = TempFileFactory.CreateTempWmaFile();
			try
			{
				using (PcmReader pcmReader = new PcmReader(sourceFile.FullName))
				{
					using (WmaWriter wmaWriter = new Wma8Writer(tempFile.FilePath, pcmReader.WaveFormat))
					{
						if (this.ValidateWaveFormatForUpload(pcmReader.WaveFormat))
						{
							byte[] array = new byte[wmaWriter.BufferSize];
							int count;
							while ((count = pcmReader.Read(array, array.Length)) > 0)
							{
								wmaWriter.Write(array, count);
							}
							flag = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new UnsupportedCustomGreetingWaveFormatException(ex);
				}
				throw;
			}
			finally
			{
				if (!flag)
				{
					tempFile.Dispose();
					tempFile = null;
				}
			}
			if (!flag || tempFile == null)
			{
				throw new UnsupportedCustomGreetingWaveFormatException();
			}
			return tempFile;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000754C File Offset: 0x0000574C
		private bool ValidateWaveFormatForUpload(WaveFormat format)
		{
			return this.EqualWaveFormatsForUpload(format, WaveFormat.Pcm8WaveFormat) || this.EqualWaveFormatsForUpload(format, WaveFormat.Pcm16WaveFormat);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000756A File Offset: 0x0000576A
		private bool EqualWaveFormatsForUpload(WaveFormat lhs, WaveFormat rhs)
		{
			return lhs.SamplesPerSec == rhs.SamplesPerSec && lhs.BitsPerSample == rhs.BitsPerSample && lhs.Channels == rhs.Channels;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00007598 File Offset: 0x00005798
		private ITempFile ValidateAndCompressFromWma(FileInfo sourceFile)
		{
			bool flag = false;
			ITempFile tempFile = TempFileFactory.CreateTempWmaFile();
			try
			{
				this.EnsureE12NotPresent(sourceFile);
				using (ITempFile tempFile2 = TempFileFactory.CreateTempWavFile())
				{
					using (WmaReader wmaReader = new WmaReader(sourceFile.FullName))
					{
						using (PcmWriter pcmWriter = new PcmWriter(tempFile2.FilePath, wmaReader.Format))
						{
							byte[] array = new byte[wmaReader.SampleSize * 2];
							int count;
							while ((count = wmaReader.Read(array, array.Length)) > 0)
							{
								pcmWriter.Write(array, count);
							}
						}
						using (PcmReader pcmReader = new PcmReader(tempFile2.FilePath))
						{
							using (WmaWriter wmaWriter = new Wma8Writer(tempFile.FilePath, pcmReader.WaveFormat))
							{
								byte[] array = new byte[wmaWriter.BufferSize];
								int count;
								while ((count = pcmReader.Read(array, array.Length)) > 0)
								{
									wmaWriter.Write(array, count);
								}
							}
						}
						flag = true;
					}
				}
			}
			catch (Exception ex)
			{
				if (PublishingSessionBase.IsPublishingPointException(ex))
				{
					throw new UnsupportedCustomGreetingWmaFormatException(ex);
				}
				throw;
			}
			finally
			{
				if (!flag)
				{
					tempFile.Dispose();
					tempFile = null;
				}
			}
			if (!flag || tempFile == null)
			{
				throw new UnsupportedCustomGreetingWmaFormatException();
			}
			return tempFile;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000777C File Offset: 0x0000597C
		private void EnsureE12NotPresent(FileInfo sourceFile)
		{
			ADTopologyLookup adtopologyLookup = ADTopologyLookup.CreateLocalResourceForestLookup();
			using (IEnumerator<Server> enumerator = adtopologyLookup.GetEnabledUMServersInDialPlan(VersionEnum.E12Legacy, this.DialPlan.Id).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Server server = enumerator.Current;
					throw new UnsupportedCustomGreetingLegacyFormatException(sourceFile.Name);
				}
			}
		}

		// Token: 0x04000086 RID: 134
		public const long MaxGreetingSizeMinutes = 5L;

		// Token: 0x04000087 RID: 135
		private const long MaxGreetingSizeBytes = 507392L;

		// Token: 0x04000088 RID: 136
		private IUMPromptStorage promptStorage;

		// Token: 0x04000089 RID: 137
		private string userName = string.Empty;

		// Token: 0x0400008A RID: 138
		private ADConfigurationObject config;

		// Token: 0x0400008B RID: 139
		private TimeSpan keepOrphanFilesInterval = CommonConstants.PromptProvisioning.KeepOrphanFilesInterval;
	}
}
