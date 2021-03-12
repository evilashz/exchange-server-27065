using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Speech.Recognition;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200004F RID: 79
	internal class UcmaOfflineTranscriber : BaseUMOfflineTranscriber
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x000109C4 File Offset: 0x0000EBC4
		protected UcmaOfflineTranscriber(SpeechRecognitionEngine transcriptionEngine, CultureInfo transcriptionLanguage)
		{
			this.transcriptionEngine = transcriptionEngine;
			this.transcriptionLanguage = transcriptionLanguage;
			this.transcriptionEngine.SpeechRecognized += this.OnSpeechRecognized;
			this.transcriptionEngine.RecognizeCompleted += this.OnRecognizeCompleted;
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060003AA RID: 938 RVA: 0x00010A20 File Offset: 0x0000EC20
		// (remove) Token: 0x060003AB RID: 939 RVA: 0x00010A58 File Offset: 0x0000EC58
		internal override event EventHandler<BaseUMOfflineTranscriber.TranscribeCompletedEventArgs> TranscribeCompleted;

		// Token: 0x060003AC RID: 940 RVA: 0x00010A90 File Offset: 0x0000EC90
		internal static bool TryCreate(CultureInfo transcriptionLanguage, out BaseUMOfflineTranscriber transcriber)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Entering UcmaOfflineTranscriber.TryCreate", new object[0]);
			transcriber = null;
			string text = null;
			if (UcmaInstalledRecognizers.TryGetRecognizerId(SpeechRecognitionEngineType.Transcription, transcriptionLanguage, out text))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, 0, "Found a transcription engine for {0}", new object[]
				{
					transcriptionLanguage
				});
				transcriber = new UcmaOfflineTranscriber(new SpeechRecognitionEngine(text), transcriptionLanguage);
			}
			else
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.VoiceMailTracer, 0, "Couldn't find a transcription engine for {0}", new object[]
				{
					transcriptionLanguage
				});
				transcriber = null;
			}
			return null != transcriber;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00010B28 File Offset: 0x0000ED28
		internal override void TranscribeFile(string audioFilePath)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering UcmaOfflineTranscriber.TranscribeFile", new object[0]);
			this.transcriptionEngine.SetInputToWaveFile(audioFilePath);
			this.PrepareCustomGrammars();
			string text = Path.Combine(Util.GetTranscriptionGrammarDir(this.transcriptionLanguage), "TSR-LM.cfp");
			Uri uri = new Uri("file:///" + this.customGrammarDir + Path.DirectorySeparatorChar);
			Grammar grammar = new Grammar(text, string.Empty, uri);
			this.transcriptionEngine.LoadGrammar(grammar);
			this.transcriptionEngine.UpdateRecognizerSetting("EngineThreadPriority", -1);
			this.transcriptionEngine.UpdateRecognizerSetting("AccuracyOverride", 100);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Calling RecognizeAsync on {0}", new object[]
			{
				audioFilePath
			});
			this.transcriptionEngine.RecognizeAsync(1);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00010C0B File Offset: 0x0000EE0B
		internal override void CancelTranscription()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering UcmaOfflineTranscriber.CancelTranscription", new object[0]);
			this.transcriptionEngine.RecognizeAsyncCancel();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00010C38 File Offset: 0x0000EE38
		internal override string TestHook_GenerateCustomGrammars()
		{
			this.PrepareCustomGrammars();
			return this.customGrammarDir.FilePath;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00010C4C File Offset: 0x0000EE4C
		internal override List<KeyValuePair<string, int>> FilterWordsInLexion(List<KeyValuePair<string, int>> rawList, int maxNumberToKeep)
		{
			string text = Path.Combine(Util.GetTranscriptionGrammarDir(this.transcriptionLanguage), "TSR-LM.cfp");
			Grammar grammar = new Grammar(text);
			bool flag = false;
			List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>(maxNumberToKeep);
			List<KeyValuePair<string, int>> result;
			try
			{
				this.transcriptionEngine.LoadGrammar(grammar);
				flag = true;
				int num = 0;
				foreach (KeyValuePair<string, int> item in rawList)
				{
					Pronounceable pronounceable = grammar.IsPronounceable(item.Key);
					if (1 == pronounceable || pronounceable == null)
					{
						list.Add(item);
						if (++num >= maxNumberToKeep)
						{
							break;
						}
					}
				}
				result = list;
			}
			finally
			{
				if (flag)
				{
					this.transcriptionEngine.UnloadGrammar(grammar);
				}
			}
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00010D1C File Offset: 0x0000EF1C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering UcmaOfflineTranscriber.Dispose", new object[0]);
				this.TranscribeCompleted = null;
				if (this.transcriptionEngine != null)
				{
					this.transcriptionEngine.SpeechRecognized -= this.OnSpeechRecognized;
					this.transcriptionEngine.RecognizeCompleted -= this.OnRecognizeCompleted;
					this.transcriptionEngine.Dispose();
					this.transcriptionEngine = null;
				}
				if (this.customGrammarDir != null)
				{
					this.customGrammarDir.Dispose();
					this.customGrammarDir = null;
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00010DB8 File Offset: 0x0000EFB8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<UcmaOfflineTranscriber>(this);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		private void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs eventArgs)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering UcmaOfflineTranscriber.OnSpeechRecognized", new object[0]);
			this.transcriptionResults.Add(new UcmaTranscriptionResult(eventArgs.Result));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00010DF8 File Offset: 0x0000EFF8
		private void OnRecognizeCompleted(object sender, RecognizeCompletedEventArgs rcea)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Entering UcmaOfflineTranscriber.OnSpeechRecognized", new object[0]);
			BaseUMOfflineTranscriber.TranscribeCompletedEventArgs e = new BaseUMOfflineTranscriber.TranscribeCompletedEventArgs((rcea.Error == null) ? this.transcriptionResults : new List<IUMTranscriptionResult>(), rcea.Error, rcea.Cancelled, rcea.UserState);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Recognition completed: Error = {0}, Cancelled = {1}", new object[]
			{
				rcea.Error,
				rcea.Cancelled
			});
			this.transcriptionEngine.SetInputToNull();
			this.TranscribeCompleted(this, e);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		private void PrepareCustomGrammars()
		{
			this.customGrammarDir = TempFileFactory.CreateTempDir();
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Created temp dir {0} for containing the custom grammars for current transcription", new object[]
			{
				this.customGrammarDir.FilePath
			});
			string transcriptionGrammarDir = Util.GetTranscriptionGrammarDir(this.transcriptionLanguage);
			foreach (string text in Directory.GetFiles(transcriptionGrammarDir, "*.grxml"))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Copying transcrption grammar {0} to custom grammar dir {1}", new object[]
				{
					text,
					this.customGrammarDir.FilePath
				});
				File.Copy(text, Path.Combine(this.customGrammarDir.FilePath, Path.GetFileName(text)));
			}
			List<ContactInfo> list = new List<ContactInfo>(2);
			List<ContactInfo> list2 = new List<ContactInfo>(1);
			if (base.CallerInfo != null)
			{
				list.Add(base.CallerInfo);
				list2.Add(base.CallerInfo);
			}
			list.Add(base.CalleeInfo);
			List<CustomGrammarBase> list3 = new List<CustomGrammarBase>(2);
			list3.Add(new PersonInfoCustomGrammar(this.transcriptionLanguage, list));
			if (base.TopN != null)
			{
				list3.Add(new TopNWordsCustomGrammar(this.transcriptionLanguage, base.TopN.GetFilteredList(this)));
			}
			foreach (CustomGrammarBase customGrammarBase in new List<CustomGrammarBase>(3)
			{
				new PersonNameCustomGrammar(this.transcriptionLanguage, list),
				new GenAppCustomGrammar(this.transcriptionLanguage, list3)
			})
			{
				customGrammarBase.WriteCustomGrammar(this.customGrammarDir.FilePath);
			}
		}

		// Token: 0x0400011D RID: 285
		private SpeechRecognitionEngine transcriptionEngine;

		// Token: 0x0400011E RID: 286
		private CultureInfo transcriptionLanguage;

		// Token: 0x0400011F RID: 287
		private List<IUMTranscriptionResult> transcriptionResults = new List<IUMTranscriptionResult>();

		// Token: 0x04000120 RID: 288
		private ITempFile customGrammarDir;
	}
}
