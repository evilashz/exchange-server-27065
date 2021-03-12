using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200014A RID: 330
	internal sealed class GlobCfg
	{
		// Token: 0x06000953 RID: 2387 RVA: 0x00029D25 File Offset: 0x00027F25
		private GlobCfg()
		{
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00029D2D File Offset: 0x00027F2D
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00029D34 File Offset: 0x00027F34
		internal static string ConfigFile { get; private set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x00029D3C File Offset: 0x00027F3C
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x00029D43 File Offset: 0x00027F43
		internal static bool EnableCallerIdDisplayNameResolution { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x00029D4B File Offset: 0x00027F4B
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x00029D52 File Offset: 0x00027F52
		internal static bool GenerateWatsonsForPipelineCleanup { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00029D5A File Offset: 0x00027F5A
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00029D61 File Offset: 0x00027F61
		internal static string ExchangeDirectory { get; private set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00029D69 File Offset: 0x00027F69
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x00029D70 File Offset: 0x00027F70
		internal static bool AllowTemporaryTTS { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00029D78 File Offset: 0x00027F78
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x00029D7F File Offset: 0x00027F7F
		internal static List<CultureInfo> VuiCultures { get; private set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00029D87 File Offset: 0x00027F87
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00029D8E File Offset: 0x00027F8E
		internal static bool AsrOverride { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000962 RID: 2402 RVA: 0x00029D96 File Offset: 0x00027F96
		// (set) Token: 0x06000963 RID: 2403 RVA: 0x00029D9D File Offset: 0x00027F9D
		internal static int MaxMobileSpeechRecoRequestsPerCore { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00029DA5 File Offset: 0x00027FA5
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00029DAC File Offset: 0x00027FAC
		internal static double NormalizationLevel { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00029DB4 File Offset: 0x00027FB4
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00029DBB File Offset: 0x00027FBB
		internal static double NoiseFloorLevel { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00029DC3 File Offset: 0x00027FC3
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00029DCA File Offset: 0x00027FCA
		internal static TimeSpan VoiceMessagePollingTime { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00029DD2 File Offset: 0x00027FD2
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00029DD9 File Offset: 0x00027FD9
		internal static int MaxNonCDRMessagesPendingInPipeline { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00029DE1 File Offset: 0x00027FE1
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00029DE8 File Offset: 0x00027FE8
		internal static TimeSpan CallAnswerMailboxDataDownloadTimeout { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x00029DF0 File Offset: 0x00027FF0
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x00029DF7 File Offset: 0x00027FF7
		internal static TimeSpan CallAnswerMailboxDataDownloadTimeoutThreshold { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00029DFF File Offset: 0x00027FFF
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x00029E06 File Offset: 0x00028006
		internal static bool EnableRemoteGWAutomation { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00029E0E File Offset: 0x0002800E
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00029E15 File Offset: 0x00028015
		internal static IPAddress UMAutomationServerAddress { get; private set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00029E1D File Offset: 0x0002801D
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00029E24 File Offset: 0x00028024
		internal static int UMAutomationServerTCPPort { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00029E2C File Offset: 0x0002802C
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00029E33 File Offset: 0x00028033
		internal static int LanguageAutoDetectionMinLength { get; private set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00029E3B File Offset: 0x0002803B
		// (set) Token: 0x06000979 RID: 2425 RVA: 0x00029E42 File Offset: 0x00028042
		internal static int LanguageAutoDetectionMaxLength { get; private set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00029E4A File Offset: 0x0002804A
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x00029E51 File Offset: 0x00028051
		internal static G711Format G711Format { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00029E59 File Offset: 0x00028059
		// (set) Token: 0x0600097D RID: 2429 RVA: 0x00029E60 File Offset: 0x00028060
		internal static TimeSpan MessageTranscriptionTimeout { get; private set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00029E68 File Offset: 0x00028068
		// (set) Token: 0x0600097F RID: 2431 RVA: 0x00029E6F File Offset: 0x0002806F
		internal static TimeSpan TranscriptionMaximumMessageLength { get; private set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00029E77 File Offset: 0x00028077
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00029E7E File Offset: 0x0002807E
		internal static TimeSpan TranscriptionMaximumBacklogPerCore { get; private set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00029E86 File Offset: 0x00028086
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00029E8D File Offset: 0x0002808D
		internal static GlobCfg.DefaultPromptHelper DefaultPrompts { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00029E95 File Offset: 0x00028095
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00029E9C File Offset: 0x0002809C
		internal static GlobCfg.DefaultPromptForAAHelper DefaultPromptsForAA { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00029EA4 File Offset: 0x000280A4
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00029EAB File Offset: 0x000280AB
		internal static GlobCfg.DefaultPromptForPreviewHelper DefaultPromptsForPreview { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00029EB3 File Offset: 0x000280B3
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00029EBA File Offset: 0x000280BA
		internal static GlobCfg.DefaultGrammarHelper DefaultGrammars { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00029EC2 File Offset: 0x000280C2
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00029EC9 File Offset: 0x000280C9
		internal static string ProductVersion { get; private set; }

		// Token: 0x0600098C RID: 2444 RVA: 0x00029ED4 File Offset: 0x000280D4
		internal static void Init()
		{
			lock (GlobCfg.lockObj)
			{
				try
				{
					GlobCfg.InternalInit();
				}
				catch (Exception ex)
				{
					if (!GrayException.IsGrayException(ex))
					{
						throw;
					}
					throw new ConfigurationException(ex.Message);
				}
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00029F38 File Offset: 0x00028138
		internal static ReplyForwardType SubjectToReplyForwardType(string subject)
		{
			if (string.IsNullOrEmpty(subject))
			{
				return ReplyForwardType.None;
			}
			int num = Math.Min(subject.Length, 4);
			int num2 = -1;
			string key = string.Empty;
			for (int i = 0; i < num; i++)
			{
				if (subject[i] == ':')
				{
					if (i + 1 < subject.Length && subject[i + 1] == ' ')
					{
						num2 = i + 1;
						break;
					}
				}
				else if (!char.IsLetter(subject[i]))
				{
					break;
				}
			}
			if (num2 > 0)
			{
				key = subject.Substring(0, num2 + 1).ToLowerInvariant().Trim();
			}
			ReplyForwardType result = ReplyForwardType.None;
			if (GlobCfg.replyForwardPrefixMap.TryGetValue(key, out result))
			{
				return result;
			}
			return ReplyForwardType.None;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00029FD8 File Offset: 0x000281D8
		private static void InternalInit()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Initializing global configuration", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "GlobCfg.BuildReplyForwardPrefixMap()", new object[0]);
			GlobCfg.BuildReplyForwardPrefixMap();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "GlobCfg.BuildAsrCultureList()", new object[0]);
			GlobCfg.BuildAsrCultureList();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "GlobCfg.ReadAllParametersForWorkerProcess()", new object[0]);
			GlobCfg.ReadAllParametersForWorkerProcess();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "GlobCfg.EnsureMailboxTimeouts()", new object[0]);
			GlobCfg.EnsureMailboxTimeouts();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "GlobCfg.RemoveInvalidPromptCultures()", new object[0]);
			GlobCfg.RemoveInvalidPromptCultures();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "DefaultPromptHelper.Create()", new object[0]);
			GlobCfg.DefaultPrompts = GlobCfg.DefaultPromptHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "DefaultPromptForAAHelper.Create()", new object[0]);
			GlobCfg.DefaultPromptsForAA = GlobCfg.DefaultPromptForAAHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "DefaultPromptForPreviewHelper.Create()", new object[0]);
			GlobCfg.DefaultPromptsForPreview = GlobCfg.DefaultPromptForPreviewHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "DefaultGrammarHelper.Create()", new object[0]);
			GlobCfg.DefaultGrammars = GlobCfg.DefaultGrammarHelper.Create();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "GlobCfg.InitializeG723()", new object[0]);
			GlobCfg.InitializeG723();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Initialize global configuration done.", new object[0]);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0002A170 File Offset: 0x00028370
		private static void ReadAllParametersForWorkerProcess()
		{
			AppConfig instance = AppConfig.Instance;
			GlobCfg.ExchangeDirectory = Utils.GetExchangeDirectory();
			GlobCfg.ConfigFile = Path.Combine(GlobCfg.ExchangeDirectory, instance.Service.FiniteStateMachinePath);
			GlobCfg.AllowTemporaryTTS = instance.Service.EnableTemporaryTTS;
			GlobCfg.AsrOverride = instance.Service.EnableSpeechRecognitionOverride;
			GlobCfg.VoiceMessagePollingTime = instance.Service.PickupDirectoryPollingPeriod;
			GlobCfg.EnableRemoteGWAutomation = instance.Service.EnableRemoteGatewayAutomation;
			GlobCfg.UMAutomationServerTCPPort = instance.Service.AutomationServiceTcpPort;
			GlobCfg.MessageTranscriptionTimeout = instance.Service.MessageTranscriptionTimeout;
			GlobCfg.TranscriptionMaximumMessageLength = instance.Service.TranscriptionMaximumMessageLength;
			GlobCfg.TranscriptionMaximumBacklogPerCore = instance.Service.TranscriptionMaximumBacklogPerCore;
			GlobCfg.EnableCallerIdDisplayNameResolution = instance.Service.EnableCallerIdDisplayNameResolution;
			GlobCfg.GenerateWatsonsForPipelineCleanup = instance.Service.GenerateWatsonsForPipelineCleanup;
			GlobCfg.LanguageAutoDetectionMinLength = instance.Service.LanguageAutoDetectionMinLength;
			GlobCfg.LanguageAutoDetectionMaxLength = instance.Service.LanguageAutoDetectionMaxLength;
			GlobCfg.G711Format = instance.Service.G711EncodingFormat;
			GlobCfg.CallAnswerMailboxDataDownloadTimeout = instance.Service.CallAnswerMailboxDataTimeout;
			GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold = instance.Service.CallAnswerMailboxDataTimeoutThreshold;
			GlobCfg.MaxMobileSpeechRecoRequestsPerCore = instance.Service.MaxMobileSpeechRecoRequestsPerCore;
			GlobCfg.NoiseFloorLevel = AudioNormalizer.ConvertDbToEnergyRms(instance.Service.NoiseFloorLevelDB);
			GlobCfg.NormalizationLevel = AudioNormalizer.ConvertDbToEnergyRms(instance.Service.NormalizationLevelDB);
			GlobCfg.UMAutomationServerAddress = ((instance.Service.AutomationServiceAddress == null) ? Utils.GetLocalIPAddress() : IPAddress.Parse(instance.Service.AutomationServiceAddress));
			GlobCfg.ProductVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
			GlobCfg.MaxNonCDRMessagesPendingInPipeline = instance.Service.MaxMessagesPerMailboxServer * 3 + instance.Service.MaxMessagesPerCore * Environment.ProcessorCount;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0002A338 File Offset: 0x00028538
		private static void RemoveInvalidPromptCultures()
		{
			List<CultureInfo> list = new List<CultureInfo>();
			foreach (CultureInfo cultureInfo in UmCultures.GetSupportedPromptCultures())
			{
				try
				{
					Util.WavPathFromCulture(cultureInfo);
				}
				catch (ResourceDirectoryNotFoundException ex)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_LangPackDirectoryNotFound, null, new object[]
					{
						cultureInfo,
						ex.Message
					});
					list.Add(cultureInfo);
				}
			}
			foreach (CultureInfo culture in list)
			{
				UmCultures.InvalidatePromptCulture(culture);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0002A410 File Offset: 0x00028610
		private static void BuildReplyForwardPrefixMap()
		{
			GlobCfg.replyForwardPrefixMap = new Dictionary<string, ReplyForwardType>();
			foreach (CultureInfo cultureInfo in UmCultures.GetSupportedClientCultures())
			{
				string text = ClientStrings.ItemForward.ToString(cultureInfo).ToLower(cultureInfo).Trim();
				string text2 = ClientStrings.ItemReply.ToString(cultureInfo).ToLower(cultureInfo).Trim();
				if (!string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
				{
					if (!GlobCfg.replyForwardPrefixMap.ContainsKey(text2))
					{
						GlobCfg.replyForwardPrefixMap.Add(text2, ReplyForwardType.Reply);
					}
					if (!GlobCfg.replyForwardPrefixMap.ContainsKey(text))
					{
						GlobCfg.replyForwardPrefixMap.Add(text, ReplyForwardType.Forward);
					}
				}
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0002A4DC File Offset: 0x000286DC
		private static void BuildAsrCultureList()
		{
			GlobCfg.VuiCultures = UmCultures.GetSupportedPromptCultures();
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0002A4E8 File Offset: 0x000286E8
		private static void EnsureMailboxTimeouts()
		{
			if (GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold >= GlobCfg.CallAnswerMailboxDataDownloadTimeout)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.ServiceStartTracer, 0, "Added callAnswerMailboxDataDownloadTimeoutThreshold={0} should be less than  callAnswerMailboxDataDownloadTimeout='{1}'. Will use defaults", new object[]
				{
					GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold,
					GlobCfg.CallAnswerMailboxDataDownloadTimeout
				});
				GlobCfg.CallAnswerMailboxDataDownloadTimeout = TimeSpan.FromMilliseconds(6000.0);
				GlobCfg.CallAnswerMailboxDataDownloadTimeoutThreshold = TimeSpan.FromMilliseconds(4000.0);
			}
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0002A563 File Offset: 0x00028763
		private static void InitializeG723()
		{
			Platform.Utilities.InitializeG723Support();
		}

		// Token: 0x040008E0 RID: 2272
		private const int MaxMailboxServerFailuresToTolerate = 3;

		// Token: 0x040008E1 RID: 2273
		private static object lockObj = new object();

		// Token: 0x040008E2 RID: 2274
		private static Dictionary<CultureInfo, string> promptCultureToSubDirectoryMap = new Dictionary<CultureInfo, string>();

		// Token: 0x040008E3 RID: 2275
		private static Dictionary<CultureInfo, string> grammarCultureToSubDirectoryMap = new Dictionary<CultureInfo, string>();

		// Token: 0x040008E4 RID: 2276
		private static Dictionary<string, ReplyForwardType> replyForwardPrefixMap = new Dictionary<string, ReplyForwardType>();

		// Token: 0x0200014B RID: 331
		internal class TranscriptionSettings
		{
			// Token: 0x17000253 RID: 595
			// (get) Token: 0x06000996 RID: 2454 RVA: 0x0002A599 File Offset: 0x00028799
			// (set) Token: 0x06000997 RID: 2455 RVA: 0x0002A5A1 File Offset: 0x000287A1
			internal float HighConfidence { get; set; }

			// Token: 0x17000254 RID: 596
			// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002A5AA File Offset: 0x000287AA
			// (set) Token: 0x06000999 RID: 2457 RVA: 0x0002A5B2 File Offset: 0x000287B2
			internal float LowConfidence { get; set; }
		}

		// Token: 0x0200014C RID: 332
		internal class DefaultPromptHelper
		{
			// Token: 0x0600099B RID: 2459 RVA: 0x0002A5C3 File Offset: 0x000287C3
			private DefaultPromptHelper()
			{
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x0600099C RID: 2460 RVA: 0x0002A5CB File Offset: 0x000287CB
			// (set) Token: 0x0600099D RID: 2461 RVA: 0x0002A5D3 File Offset: 0x000287D3
			internal PromptConfigBase Mumble1 { get; private set; }

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x0600099E RID: 2462 RVA: 0x0002A5DC File Offset: 0x000287DC
			// (set) Token: 0x0600099F RID: 2463 RVA: 0x0002A5E4 File Offset: 0x000287E4
			internal PromptConfigBase Mumble2 { get; private set; }

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0002A5ED File Offset: 0x000287ED
			// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0002A5F5 File Offset: 0x000287F5
			internal PromptConfigBase Silence1 { get; private set; }

			// Token: 0x17000258 RID: 600
			// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0002A5FE File Offset: 0x000287FE
			// (set) Token: 0x060009A3 RID: 2467 RVA: 0x0002A606 File Offset: 0x00028806
			internal PromptConfigBase Silence2 { get; private set; }

			// Token: 0x17000259 RID: 601
			// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0002A60F File Offset: 0x0002880F
			// (set) Token: 0x060009A5 RID: 2469 RVA: 0x0002A617 File Offset: 0x00028817
			internal PromptConfigBase SpeechError { get; private set; }

			// Token: 0x1700025A RID: 602
			// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0002A620 File Offset: 0x00028820
			// (set) Token: 0x060009A7 RID: 2471 RVA: 0x0002A628 File Offset: 0x00028828
			internal PromptConfigBase InvalidCommand { get; private set; }

			// Token: 0x1700025B RID: 603
			// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0002A631 File Offset: 0x00028831
			// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0002A639 File Offset: 0x00028839
			internal PromptConfigBase DtmfFallback { get; private set; }

			// Token: 0x1700025C RID: 604
			// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002A642 File Offset: 0x00028842
			// (set) Token: 0x060009AB RID: 2475 RVA: 0x0002A64A File Offset: 0x0002884A
			internal PromptConfigBase SorryTryAgainLater { get; private set; }

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002A653 File Offset: 0x00028853
			// (set) Token: 0x060009AD RID: 2477 RVA: 0x0002A65B File Offset: 0x0002885B
			internal PromptConfigBase SorrySystemError { get; private set; }

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002A664 File Offset: 0x00028864
			// (set) Token: 0x060009AF RID: 2479 RVA: 0x0002A66C File Offset: 0x0002886C
			internal PromptConfigBase InvalidKey { get; private set; }

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002A675 File Offset: 0x00028875
			// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0002A67D File Offset: 0x0002887D
			internal PromptConfigBase NotImplemented { get; private set; }

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002A686 File Offset: 0x00028886
			// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0002A68E File Offset: 0x0002888E
			internal PromptConfigBase Repeat { get; private set; }

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0002A697 File Offset: 0x00028897
			// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0002A69F File Offset: 0x0002889F
			internal PromptConfigBase TimeRange { get; private set; }

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002A6A8 File Offset: 0x000288A8
			// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0002A6B0 File Offset: 0x000288B0
			internal PromptConfigBase AreYouThere { get; private set; }

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002A6B9 File Offset: 0x000288B9
			// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0002A6C1 File Offset: 0x000288C1
			internal PromptConfigBase GoodBye { get; private set; }

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x060009BA RID: 2490 RVA: 0x0002A6CA File Offset: 0x000288CA
			// (set) Token: 0x060009BB RID: 2491 RVA: 0x0002A6D2 File Offset: 0x000288D2
			internal PromptConfigBase GoodByeConfirmation { get; private set; }

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x060009BC RID: 2492 RVA: 0x0002A6DB File Offset: 0x000288DB
			// (set) Token: 0x060009BD RID: 2493 RVA: 0x0002A6E3 File Offset: 0x000288E3
			internal PromptConfigBase SilenceOneSecond { get; private set; }

			// Token: 0x17000266 RID: 614
			// (get) Token: 0x060009BE RID: 2494 RVA: 0x0002A6EC File Offset: 0x000288EC
			// (set) Token: 0x060009BF RID: 2495 RVA: 0x0002A6F4 File Offset: 0x000288F4
			internal PromptConfigBase[] MaxCallSecondsExceeded { get; private set; }

			// Token: 0x17000267 RID: 615
			// (get) Token: 0x060009C0 RID: 2496 RVA: 0x0002A6FD File Offset: 0x000288FD
			// (set) Token: 0x060009C1 RID: 2497 RVA: 0x0002A705 File Offset: 0x00028905
			internal PromptConfigBase[] ComfortNoise { get; private set; }

			// Token: 0x17000268 RID: 616
			// (get) Token: 0x060009C2 RID: 2498 RVA: 0x0002A70E File Offset: 0x0002890E
			// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0002A716 File Offset: 0x00028916
			internal PromptConfigBase VoicemailGreeting { get; private set; }

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002A71F File Offset: 0x0002891F
			// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0002A727 File Offset: 0x00028927
			internal PromptConfigBase AwayGreeting { get; private set; }

			// Token: 0x060009C6 RID: 2502 RVA: 0x0002A730 File Offset: 0x00028930
			internal static ArrayList BuildWithValues(CultureInfo culture, GlobCfg.DefaultPromptHelper.AddPromptWithValue f, params PromptConfigBase[] configs)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PromptConfigBase promptConfigBase in configs)
				{
					if (!f(arrayList, promptConfigBase, culture))
					{
						promptConfigBase.AddPrompts(arrayList, null, culture);
					}
				}
				return arrayList;
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x0002A76C File Offset: 0x0002896C
			internal static ArrayList Build(ActivityManager m, CultureInfo culture, params PromptConfigBase[] configs)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PromptConfigBase promptConfigBase in configs)
				{
					promptConfigBase.AddPrompts(arrayList, m, culture);
				}
				return arrayList;
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x0002A7A0 File Offset: 0x000289A0
			internal static GlobCfg.DefaultPromptHelper Create()
			{
				GlobCfg.DefaultPromptHelper defaultPromptHelper = new GlobCfg.DefaultPromptHelper();
				defaultPromptHelper.Mumble1 = PromptConfigBase.Create("vuiDefaultMumble1", "statement", "NOT repeat");
				defaultPromptHelper.Mumble2 = PromptConfigBase.Create("vuiDefaultMumble2", "statement", "NOT repeat");
				defaultPromptHelper.Silence1 = PromptConfigBase.Create("vuiDefaultSilence1", "statement", "NOT repeat");
				defaultPromptHelper.Silence2 = PromptConfigBase.Create("vuiDefaultSilence2", "statement", "NOT repeat");
				defaultPromptHelper.SpeechError = PromptConfigBase.Create("vuiDefaultSpeechError", "statement", "NOT repeat");
				defaultPromptHelper.InvalidCommand = PromptConfigBase.Create("vuiDefaultInvalidCommand", "statement", "NOT repeat");
				defaultPromptHelper.DtmfFallback = PromptConfigBase.Create("vuiDtmfFallback", "statement", "NOT repeat");
				defaultPromptHelper.SorryTryAgainLater = PromptConfigBase.Create("tuiSorryTryAgainLater", "statement", string.Empty);
				defaultPromptHelper.SorrySystemError = PromptConfigBase.Create("tuiSystemError", "statement", string.Empty);
				defaultPromptHelper.NotImplemented = PromptConfigBase.Create("tuiFeatureNotAvailable", "statement", string.Empty);
				defaultPromptHelper.Repeat = PromptConfigBase.Create("vuiRepeating", "statement", string.Empty);
				defaultPromptHelper.AreYouThere = PromptConfigBase.Create("tuiAreYouThere", "statement", string.Empty);
				defaultPromptHelper.GoodBye = PromptConfigBase.Create("vuiGlobalGoodbye", "statement", string.Empty);
				defaultPromptHelper.GoodByeConfirmation = PromptConfigBase.Create("vuiGoodbyeConfirmation", "statement", string.Empty);
				defaultPromptHelper.SilenceOneSecond = PromptConfigBase.Create("1s", "silence", string.Empty);
				defaultPromptHelper.MaxCallSecondsExceeded = new PromptConfigBase[]
				{
					PromptConfigBase.Create("tuiMaxCallDurationMet", "statement", string.Empty),
					PromptConfigBase.Create("tuiGoodbye", "statement", string.Empty)
				};
				VariablePromptConfig<DigitPrompt, string> variablePromptConfig = new VariablePromptConfig<DigitPrompt, string>("lastInput", "digit", string.Empty, null);
				defaultPromptHelper.InvalidKey = PromptConfigBase.Create("tuiInvalidKeys", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig
				});
				VariablePromptConfig<TimePrompt, ExDateTime> variablePromptConfig2 = new VariablePromptConfig<TimePrompt, ExDateTime>("startTime", "time", string.Empty, null);
				VariablePromptConfig<TimePrompt, ExDateTime> variablePromptConfig3 = new VariablePromptConfig<TimePrompt, ExDateTime>("endTime", "time", string.Empty, null);
				defaultPromptHelper.TimeRange = PromptConfigBase.Create("tuiDefaultTimeRange", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig2,
					variablePromptConfig3
				});
				defaultPromptHelper.ComfortNoise = new PromptConfigBase[]
				{
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("comfortnoise.wav", "wave", string.Empty),
					PromptConfigBase.Create("tuiHBODelay", "statement", string.Empty),
					PromptConfigBase.Create("tuiSystemError", "statement", string.Empty)
				};
				PromptConfigBase promptConfigBase = PromptConfigBase.Create("userName", "name", string.Empty);
				defaultPromptHelper.VoicemailGreeting = PromptConfigBase.Create("tuiSystemGreeting", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase
				});
				defaultPromptHelper.AwayGreeting = PromptConfigBase.Create("tuiSystemOofGreeting", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase
				});
				return defaultPromptHelper;
			}

			// Token: 0x0200014D RID: 333
			// (Invoke) Token: 0x060009CA RID: 2506
			internal delegate bool AddPromptWithValue(ArrayList prompts, PromptConfigBase pConfig, CultureInfo c);
		}

		// Token: 0x0200014E RID: 334
		internal class DefaultPromptForAAHelper
		{
			// Token: 0x060009CD RID: 2509 RVA: 0x0002AC2E File Offset: 0x00028E2E
			private DefaultPromptForAAHelper()
			{
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002AC36 File Offset: 0x00028E36
			// (set) Token: 0x060009CF RID: 2511 RVA: 0x0002AC3E File Offset: 0x00028E3E
			internal PromptConfigBase DayRange { get; private set; }

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x060009D0 RID: 2512 RVA: 0x0002AC47 File Offset: 0x00028E47
			// (set) Token: 0x060009D1 RID: 2513 RVA: 0x0002AC4F File Offset: 0x00028E4F
			internal PromptConfigBase DayTimeRange { get; private set; }

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0002AC58 File Offset: 0x00028E58
			// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0002AC60 File Offset: 0x00028E60
			internal PromptConfigBase Everyday { get; private set; }

			// Token: 0x1700026D RID: 621
			// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002AC69 File Offset: 0x00028E69
			// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0002AC71 File Offset: 0x00028E71
			internal PromptConfigBase OpeningHours { get; private set; }

			// Token: 0x1700026E RID: 622
			// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002AC7A File Offset: 0x00028E7A
			// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0002AC82 File Offset: 0x00028E82
			internal PromptConfigBase OpeningHoursStandard { get; private set; }

			// Token: 0x1700026F RID: 623
			// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002AC8B File Offset: 0x00028E8B
			// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0002AC93 File Offset: 0x00028E93
			internal PromptConfigBase WeAreAlwaysClosed { get; private set; }

			// Token: 0x17000270 RID: 624
			// (get) Token: 0x060009DA RID: 2522 RVA: 0x0002AC9C File Offset: 0x00028E9C
			// (set) Token: 0x060009DB RID: 2523 RVA: 0x0002ACA4 File Offset: 0x00028EA4
			internal PromptConfigBase WeAreAlwaysOpen { get; private set; }

			// Token: 0x17000271 RID: 625
			// (get) Token: 0x060009DC RID: 2524 RVA: 0x0002ACAD File Offset: 0x00028EAD
			// (set) Token: 0x060009DD RID: 2525 RVA: 0x0002ACB5 File Offset: 0x00028EB5
			internal PromptConfigBase BusinessAddressNotSet { get; private set; }

			// Token: 0x17000272 RID: 626
			// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002ACBE File Offset: 0x00028EBE
			// (set) Token: 0x060009DF RID: 2527 RVA: 0x0002ACC6 File Offset: 0x00028EC6
			internal PromptConfigBase AABusinessHoursWelcome { get; private set; }

			// Token: 0x17000273 RID: 627
			// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002ACCF File Offset: 0x00028ECF
			// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0002ACD7 File Offset: 0x00028ED7
			internal PromptConfigBase AAAfterHoursWelcome { get; private set; }

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002ACE0 File Offset: 0x00028EE0
			// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0002ACE8 File Offset: 0x00028EE8
			internal PromptConfigBase PleaseChooseFrom { get; private set; }

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002ACF1 File Offset: 0x00028EF1
			// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0002ACF9 File Offset: 0x00028EF9
			internal PromptConfigBase PleaseSayTheName { get; private set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002AD02 File Offset: 0x00028F02
			// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0002AD0A File Offset: 0x00028F0A
			internal PromptConfigBase BusinessAddress { get; private set; }

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0002AD13 File Offset: 0x00028F13
			// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0002AD1B File Offset: 0x00028F1B
			internal PromptConfigBase BusinessHours { get; private set; }

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x060009EA RID: 2538 RVA: 0x0002AD24 File Offset: 0x00028F24
			// (set) Token: 0x060009EB RID: 2539 RVA: 0x0002AD2C File Offset: 0x00028F2C
			internal PromptConfigBase AAWelcomeWithBusinessName { get; private set; }

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002AD35 File Offset: 0x00028F35
			// (set) Token: 0x060009ED RID: 2541 RVA: 0x0002AD3D File Offset: 0x00028F3D
			internal PromptConfigBase CallSomeone { get; private set; }

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x060009EE RID: 2542 RVA: 0x0002AD46 File Offset: 0x00028F46
			// (set) Token: 0x060009EF RID: 2543 RVA: 0x0002AD4E File Offset: 0x00028F4E
			internal PromptConfigBase CustomMenu { get; private set; }

			// Token: 0x1700027B RID: 635
			// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0002AD57 File Offset: 0x00028F57
			// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0002AD5F File Offset: 0x00028F5F
			internal PromptConfigBase[] CustomMenuConfig { get; private set; }

			// Token: 0x060009F2 RID: 2546 RVA: 0x0002AD68 File Offset: 0x00028F68
			internal static ArrayList BuildWithValues(CultureInfo culture, GlobCfg.DefaultPromptForAAHelper.AddPromptWithValue f, params PromptConfigBase[] configs)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PromptConfigBase promptConfigBase in configs)
				{
					if (!f(arrayList, promptConfigBase, culture))
					{
						promptConfigBase.AddPrompts(arrayList, null, culture);
					}
				}
				return arrayList;
			}

			// Token: 0x060009F3 RID: 2547 RVA: 0x0002ADA4 File Offset: 0x00028FA4
			internal static ArrayList Build(ActivityManager m, CultureInfo culture, params PromptConfigBase[] configs)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PromptConfigBase promptConfigBase in configs)
				{
					promptConfigBase.AddPrompts(arrayList, m, culture);
				}
				return arrayList;
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x0002ADD8 File Offset: 0x00028FD8
			internal static GlobCfg.DefaultPromptForAAHelper Create()
			{
				GlobCfg.DefaultPromptForAAHelper defaultPromptForAAHelper = new GlobCfg.DefaultPromptForAAHelper();
				PromptConfigBase promptConfigBase = PromptConfigBase.Create("businessName", "name", string.Empty);
				defaultPromptForAAHelper.AAWelcomeWithBusinessName = PromptConfigBase.Create("tuiAABusinessNameWelcome", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase
				});
				defaultPromptForAAHelper.AABusinessHoursWelcome = PromptConfigBase.Create("tuiAABusinessHoursWelcome", "statement", string.Empty);
				defaultPromptForAAHelper.AAAfterHoursWelcome = PromptConfigBase.Create("tuiAAAfterHoursWelcome", "statement", string.Empty);
				defaultPromptForAAHelper.PleaseChooseFrom = PromptConfigBase.Create("vuiAADsearch_No_Custom_Yes_main", "statement", string.Empty);
				defaultPromptForAAHelper.PleaseSayTheName = PromptConfigBase.Create("vuiAA_Custom_Yes_Dsearch_Yes_main", "statement", string.Empty);
				defaultPromptForAAHelper.CustomMenu = PromptConfigBase.Create("AAContext", "aaCustomMenu", string.Empty);
				defaultPromptForAAHelper.CallSomeone = PromptConfigBase.Create("tuiCallSomeone", "statement", string.Empty);
				defaultPromptForAAHelper.WeAreAlwaysClosed = PromptConfigBase.Create("tuiWeAreClosed", "statement", string.Empty);
				defaultPromptForAAHelper.WeAreAlwaysOpen = PromptConfigBase.Create("tuiWeAreOpen", "statement", string.Empty);
				PromptConfigBase promptConfigBase2 = PromptConfigBase.Create("varTimeZone", "timeZone", string.Empty);
				PromptConfigBase promptConfigBase3 = PromptConfigBase.Create("varScheduleGroupList", "scheduleGroupList", string.Empty);
				defaultPromptForAAHelper.Everyday = PromptConfigBase.Create("tuiEveryday", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase3,
					promptConfigBase2
				});
				defaultPromptForAAHelper.OpeningHours = PromptConfigBase.Create("tuiOurOpeningHoursAre", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase3,
					promptConfigBase2
				});
				PromptConfigBase promptConfigBase4 = PromptConfigBase.Create("varScheduleIntervalList", "scheduleIntervalList", string.Empty);
				defaultPromptForAAHelper.OpeningHoursStandard = PromptConfigBase.Create("tuiOurOpeningHoursAre", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase4,
					promptConfigBase2
				});
				PromptConfigBase promptConfigBase5 = PromptConfigBase.Create("businessAddress", "address", string.Empty);
				defaultPromptForAAHelper.BusinessAddress = PromptConfigBase.Create("tuiWeAreLocatedAt", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase5
				});
				PromptConfigBase promptConfigBase6 = PromptConfigBase.Create("selectedMenu", "text", string.Empty);
				defaultPromptForAAHelper.BusinessAddressNotSet = PromptConfigBase.Create("tuiLocationNotSet", "statement", string.Empty, new PromptConfigBase[]
				{
					promptConfigBase6
				});
				VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext> variablePromptConfig = new VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext>("startDay", "dayOfWeekTime", string.Empty, null);
				VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext> variablePromptConfig2 = new VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext>("endDay", "dayOfWeekTime", string.Empty, null);
				defaultPromptForAAHelper.DayRange = PromptConfigBase.Create("tuiDefaultDayRange", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig,
					variablePromptConfig2
				});
				VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext> variablePromptConfig3 = new VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext>("startDayTime", "dayOfWeekTime", string.Empty, null);
				VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext> variablePromptConfig4 = new VariablePromptConfig<DayOfWeekTimePrompt, DayOfWeekTimeContext>("endDayTime", "dayOfWeekTime", string.Empty, null);
				defaultPromptForAAHelper.DayTimeRange = PromptConfigBase.Create("tuiDefaultTimeRange", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig3,
					variablePromptConfig4
				});
				VariablePromptConfig<TextPrompt, string> variablePromptConfig5 = new VariablePromptConfig<TextPrompt, string>("departmentName", "text", string.Empty, null);
				defaultPromptForAAHelper.CustomMenuConfig = new PromptConfigBase[12];
				defaultPromptForAAHelper.CustomMenuConfig[1] = PromptConfigBase.Create("tuiPAATransferToPhone1", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[2] = PromptConfigBase.Create("tuiPAATransferToPhone2", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[3] = PromptConfigBase.Create("tuiPAATransferToPhone3", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[4] = PromptConfigBase.Create("tuiPAATransferToPhone4", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[5] = PromptConfigBase.Create("tuiPAATransferToPhone5", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[6] = PromptConfigBase.Create("tuiPAATransferToPhone6", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[7] = PromptConfigBase.Create("tuiPAATransferToPhone7", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[8] = PromptConfigBase.Create("tuiPAATransferToPhone8", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[9] = PromptConfigBase.Create("tuiPAATransferToPhone9", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				defaultPromptForAAHelper.CustomMenuConfig[11] = PromptConfigBase.Create("tuiAACustomizedMenuTimeOut", "statement", string.Empty, new PromptConfigBase[]
				{
					variablePromptConfig5
				});
				return defaultPromptForAAHelper;
			}

			// Token: 0x0200014F RID: 335
			// (Invoke) Token: 0x060009F6 RID: 2550
			internal delegate bool AddPromptWithValue(ArrayList prompts, PromptConfigBase pConfig, CultureInfo c);
		}

		// Token: 0x02000150 RID: 336
		internal class DefaultPromptForPreviewHelper
		{
			// Token: 0x060009F9 RID: 2553 RVA: 0x0002B2DF File Offset: 0x000294DF
			private DefaultPromptForPreviewHelper()
			{
			}

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002B2E7 File Offset: 0x000294E7
			// (set) Token: 0x060009FB RID: 2555 RVA: 0x0002B2EF File Offset: 0x000294EF
			internal PromptConfigBase AABusinessHours { get; private set; }

			// Token: 0x1700027D RID: 637
			// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002B2F8 File Offset: 0x000294F8
			// (set) Token: 0x060009FD RID: 2557 RVA: 0x0002B300 File Offset: 0x00029500
			internal PromptConfigBase AABusinessLocation { get; private set; }

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002B309 File Offset: 0x00029509
			// (set) Token: 0x060009FF RID: 2559 RVA: 0x0002B311 File Offset: 0x00029511
			internal PromptConfigBase AAWelcomeGreeting { get; private set; }

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002B31A File Offset: 0x0002951A
			// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0002B322 File Offset: 0x00029522
			internal PromptConfigBase AACustomMenu { get; private set; }

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002B32B File Offset: 0x0002952B
			// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0002B333 File Offset: 0x00029533
			internal PromptConfigBase MbxVoicemailGreeting { get; private set; }

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002B33C File Offset: 0x0002953C
			// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0002B344 File Offset: 0x00029544
			internal PromptConfigBase MbxAwayGreeting { get; private set; }

			// Token: 0x17000282 RID: 642
			// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002B34D File Offset: 0x0002954D
			// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0002B355 File Offset: 0x00029555
			internal PromptConfigBase AACustomPrompt { get; private set; }

			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002B35E File Offset: 0x0002955E
			// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0002B366 File Offset: 0x00029566
			internal PromptConfigBase MbxCustomGreeting { get; private set; }

			// Token: 0x06000A0A RID: 2570 RVA: 0x0002B370 File Offset: 0x00029570
			internal static ArrayList BuildWithValues(CultureInfo culture, GlobCfg.DefaultPromptForPreviewHelper.AddPromptWithValue f, params PromptConfigBase[] configs)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PromptConfigBase promptConfigBase in configs)
				{
					if (!f(arrayList, promptConfigBase, culture))
					{
						promptConfigBase.AddPrompts(arrayList, null, culture);
					}
				}
				return arrayList;
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x0002B3AC File Offset: 0x000295AC
			internal static ArrayList Build(ActivityManager m, CultureInfo culture, params PromptConfigBase[] configs)
			{
				ArrayList arrayList = new ArrayList();
				foreach (PromptConfigBase promptConfigBase in configs)
				{
					promptConfigBase.AddPrompts(arrayList, m, culture);
				}
				return arrayList;
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x0002B3E0 File Offset: 0x000295E0
			internal static GlobCfg.DefaultPromptForPreviewHelper Create()
			{
				return new GlobCfg.DefaultPromptForPreviewHelper
				{
					AABusinessHours = PromptConfigBase.Create("businessSchedule", "businessHours", string.Empty),
					AABusinessLocation = PromptConfigBase.Create("aaLocationContext", "aaBusinessLocation", string.Empty),
					AAWelcomeGreeting = PromptConfigBase.Create("AAContext", "aaWelcomeGreeting", string.Empty),
					AACustomMenu = PromptConfigBase.Create("AAContext", "aaCustomMenu", string.Empty),
					AACustomPrompt = PromptConfigBase.Create("customPrompt", "varwave", string.Empty),
					MbxVoicemailGreeting = PromptConfigBase.Create("userName", "mbxVoicemailGreeting", string.Empty),
					MbxAwayGreeting = PromptConfigBase.Create("userName", "mbxAwayGreeting", string.Empty),
					MbxCustomGreeting = PromptConfigBase.Create("customGreeting", "tempwave", string.Empty)
				};
			}

			// Token: 0x02000151 RID: 337
			// (Invoke) Token: 0x06000A0E RID: 2574
			internal delegate bool AddPromptWithValue(ArrayList prompts, PromptConfigBase pConfig, CultureInfo c);
		}

		// Token: 0x02000152 RID: 338
		internal class DefaultGrammarHelper
		{
			// Token: 0x06000A11 RID: 2577 RVA: 0x0002B4C4 File Offset: 0x000296C4
			private DefaultGrammarHelper()
			{
			}

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002B4CC File Offset: 0x000296CC
			// (set) Token: 0x06000A13 RID: 2579 RVA: 0x0002B4D4 File Offset: 0x000296D4
			internal UMGrammarConfig Help { get; private set; }

			// Token: 0x17000285 RID: 645
			// (get) Token: 0x06000A14 RID: 2580 RVA: 0x0002B4DD File Offset: 0x000296DD
			// (set) Token: 0x06000A15 RID: 2581 RVA: 0x0002B4E5 File Offset: 0x000296E5
			internal UMGrammarConfig Repeat { get; private set; }

			// Token: 0x17000286 RID: 646
			// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0002B4EE File Offset: 0x000296EE
			// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0002B4F6 File Offset: 0x000296F6
			internal UMGrammarConfig Goodbye { get; private set; }

			// Token: 0x17000287 RID: 647
			// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0002B4FF File Offset: 0x000296FF
			// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0002B507 File Offset: 0x00029707
			internal UMGrammarConfig GoodbyeConfirmation { get; private set; }

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0002B510 File Offset: 0x00029710
			// (set) Token: 0x06000A1B RID: 2587 RVA: 0x0002B518 File Offset: 0x00029718
			internal UMGrammarConfig MainMenuShortcut { get; private set; }

			// Token: 0x06000A1C RID: 2588 RVA: 0x0002B524 File Offset: 0x00029724
			internal static GlobCfg.DefaultGrammarHelper Create()
			{
				return new GlobCfg.DefaultGrammarHelper
				{
					Help = UMGrammarConfig.Create("common.grxml", "static", string.Empty, "help", string.Empty, null),
					Repeat = UMGrammarConfig.Create("common.grxml", "static", string.Empty, "repeat", string.Empty, null),
					Goodbye = UMGrammarConfig.Create("common.grxml", "static", string.Empty, "goodbye", string.Empty, null),
					GoodbyeConfirmation = UMGrammarConfig.Create("common.grxml", "static", string.Empty, "yesNo", string.Empty, null),
					MainMenuShortcut = UMGrammarConfig.Create("common.grxml", "static", string.Empty, "mainMenuShortcut", string.Empty, null)
				};
			}
		}
	}
}
