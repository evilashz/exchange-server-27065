using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Signaling;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using Microsoft.Speech.Synthesis;
using Microsoft.Win32;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000048 RID: 72
	internal class UcmaPlatform : Platform
	{
		// Token: 0x06000340 RID: 832 RVA: 0x0000D774 File Offset: 0x0000B974
		public static void ValidateRealTimeUri(string uri)
		{
			try
			{
				new RealTimeAddress(uri);
			}
			catch (ArgumentException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, null, "RealTimeUri is invalid.  {0}", new object[]
				{
					ex
				});
				throw new RealTimeException(ex.Message, ex);
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		public override BaseUMVoipPlatform CreateVoipPlatform()
		{
			return new UcmaVoipPlatform();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D7CB File Offset: 0x0000B9CB
		public override BaseCallRouterPlatform CreateCallRouterVoipPlatform(LocalizedString serviceName, LocalizedString serverName, UMADSettings config)
		{
			return new UcmaCallRouterPlatform(serviceName, serverName, config);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000D7D5 File Offset: 0x0000B9D5
		public override PlatformSipUri CreateSipUri(string uri)
		{
			return new UcmaSipUri(uri);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000D7DD File Offset: 0x0000B9DD
		public override bool TryCreateSipUri(string uriString, out PlatformSipUri sipUri)
		{
			return UcmaSipUri.TryParse(uriString, out sipUri);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		public override PlatformSipUri CreateSipUri(SipUriScheme scheme, string user, string host)
		{
			return new UcmaSipUri(scheme, user, host);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		public override PlatformSignalingHeader CreateSignalingHeader(string name, string value)
		{
			return new UcmaSignalingHeader(name, value);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D7F9 File Offset: 0x0000B9F9
		public override bool TryCreateOfflineTranscriber(CultureInfo transcriptionLanguage, out BaseUMOfflineTranscriber transcriber)
		{
			return UcmaOfflineTranscriber.TryCreate(transcriptionLanguage, out transcriber);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D802 File Offset: 0x0000BA02
		public override bool TryCreateMobileRecognizer(Guid requestId, CultureInfo culture, SpeechRecognitionEngineType engineType, int maxAlternates, out IMobileRecognizer recognizer)
		{
			return UcmaMobileRecognizer.TryCreate(requestId, culture, engineType, maxAlternates, out recognizer);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D810 File Offset: 0x0000BA10
		public override bool IsTranscriptionLanguageSupported(CultureInfo transcriptionLanguage)
		{
			return UcmaInstalledRecognizers.IsTranscriptionLanguageSupported(transcriptionLanguage);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000D818 File Offset: 0x0000BA18
		public override IEnumerable<CultureInfo> SupportedTranscriptionLanguages
		{
			get
			{
				return UcmaInstalledRecognizers.SupportedTranscriptionLanguages;
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D820 File Offset: 0x0000BA20
		public override void CompileGrammar(string grxmlGrammarPath, string compiledGrammarPath, CultureInfo culture)
		{
			using (ITempFile tempFile = TempFileFactory.CreateTempGrammarFile())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this.GetHashCode(), "Compiling to CFG {0} --> {1}", new object[]
				{
					grxmlGrammarPath,
					tempFile.FilePath
				});
				using (Stream stream = new FileStream(tempFile.FilePath, FileMode.Create))
				{
					SrgsGrammarCompiler.Compile(grxmlGrammarPath, stream);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this.GetHashCode(), "Compiling to CFP {0} --> {1}", new object[]
				{
					tempFile.FilePath,
					compiledGrammarPath
				});
				using (GrammarInfo grammarInfo = new GrammarInfo(tempFile.FilePath))
				{
					using (SpeechRecognitionEngine speechRecognitionEngine = new SpeechRecognitionEngine(UcmaInstalledRecognizers.GetRecognizerId(SpeechRecognitionEngineType.CmdAndControl, culture)))
					{
						using (Stream stream2 = new FileStream(compiledGrammarPath, FileMode.Create))
						{
							grammarInfo.ExtraParts.AddEnginePart(speechRecognitionEngine);
							grammarInfo.Save(stream2);
						}
					}
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this.GetHashCode(), "Compliation Complete!", new object[0]);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000D984 File Offset: 0x0000BB84
		public override void CheckGrammarEntryFormat(string wordToCheck)
		{
			new SrgsText(wordToCheck);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000D990 File Offset: 0x0000BB90
		public override ITempWavFile SynthesizePromptsToPcmWavFile(ArrayList prompts)
		{
			ITempWavFile tempWavFile = TempFileFactory.CreateTempWavFile();
			bool flag = false;
			using (SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer())
			{
				PromptBuilder promptBuilder = new PromptBuilder();
				foreach (object obj in prompts)
				{
					Prompt prompt = (Prompt)obj;
					promptBuilder.AppendSsmlMarkup(prompt.ToSsml());
				}
				try
				{
					SpeechAudioFormatInfo speechAudioFormatInfo = new SpeechAudioFormatInfo(WaveFormat.Pcm8WaveFormat.SamplesPerSec, 16, 1);
					speechSynthesizer.SetOutputToWaveFile(tempWavFile.FilePath, speechAudioFormatInfo);
					speechSynthesizer.Speak(promptBuilder);
					speechSynthesizer.SetOutputToNull();
					flag = true;
				}
				catch (Exception ex)
				{
					if (ex is IOException || ex is COMException || ex is FormatException || ex is ArgumentException || ex is InvalidOperationException)
					{
						throw new PromptSynthesisException(ex.Message, ex);
					}
					throw;
				}
				finally
				{
					if (!flag)
					{
						tempWavFile.Dispose();
						tempWavFile = null;
					}
				}
			}
			return tempWavFile;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000DAB8 File Offset: 0x0000BCB8
		public override void RecycleServiceDependencies()
		{
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000DABC File Offset: 0x0000BCBC
		public override void InitializeG723Support()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this.GetHashCode(), "AppConfig EnableG723 = {0}", new object[]
			{
				AppConfig.Instance.Service.EnableG723
			});
			int num = AppConfig.Instance.Service.EnableG723 ? 0 : 1;
			object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Unified Managed Communications API\\AudioVideo", "DisableG723", 0);
			int num2 = (value is int) ? ((int)value) : 0;
			if (num != num2)
			{
				Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Unified Managed Communications API\\AudioVideo", "DisableG723", num, RegistryValueKind.DWord);
				CallIdTracer.TraceDebug(ExTraceGlobals.VoipPlatformTracer, this.GetHashCode(), "Set {0} to {1}", new object[]
				{
					"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Unified Managed Communications API\\AudioVideo@DisableG723",
					num
				});
			}
		}

		// Token: 0x04000109 RID: 265
		private const string G723RegistryKeyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Unified Managed Communications API\\AudioVideo";

		// Token: 0x0400010A RID: 266
		private const string G723RegistryValueName = "DisableG723";

		// Token: 0x0400010B RID: 267
		private static object staticLock = new object();
	}
}
