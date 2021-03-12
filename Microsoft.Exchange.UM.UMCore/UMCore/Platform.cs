using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002F5 RID: 757
	internal abstract class Platform : IPlatformBuilder, IPlatformUtilities
	{
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x000634AE File Offset: 0x000616AE
		public static IPlatformBuilder Builder
		{
			get
			{
				return Platform.Instance;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x000634B5 File Offset: 0x000616B5
		public static IPlatformUtilities Utilities
		{
			get
			{
				return Platform.Instance;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x000634BC File Offset: 0x000616BC
		private static Platform Instance
		{
			get
			{
				if (Platform.instance == null)
				{
					lock (Platform.staticLock)
					{
						if (Platform.instance == null)
						{
							Platform.instance = Platform.Create(AppConfig.Instance.Service.PlatformType);
						}
					}
				}
				return Platform.instance;
			}
		}

		// Token: 0x06001718 RID: 5912
		public abstract BaseUMVoipPlatform CreateVoipPlatform();

		// Token: 0x06001719 RID: 5913
		public abstract BaseCallRouterPlatform CreateCallRouterVoipPlatform(LocalizedString serviceName, LocalizedString serverName, UMADSettings config);

		// Token: 0x0600171A RID: 5914
		public abstract PlatformSipUri CreateSipUri(string uri);

		// Token: 0x0600171B RID: 5915
		public abstract bool TryCreateSipUri(string uriString, out PlatformSipUri sipUri);

		// Token: 0x0600171C RID: 5916
		public abstract PlatformSipUri CreateSipUri(SipUriScheme scheme, string user, string host);

		// Token: 0x0600171D RID: 5917
		public abstract PlatformSignalingHeader CreateSignalingHeader(string name, string value);

		// Token: 0x0600171E RID: 5918
		public abstract bool TryCreateOfflineTranscriber(CultureInfo transcriptionLanguage, out BaseUMOfflineTranscriber transcriber);

		// Token: 0x0600171F RID: 5919
		public abstract bool TryCreateMobileRecognizer(Guid requestId, CultureInfo culture, SpeechRecognitionEngineType engineType, int maxAlternates, out IMobileRecognizer recognizer);

		// Token: 0x06001720 RID: 5920
		public abstract bool IsTranscriptionLanguageSupported(CultureInfo transcriptionLanguage);

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001721 RID: 5921
		public abstract IEnumerable<CultureInfo> SupportedTranscriptionLanguages { get; }

		// Token: 0x06001722 RID: 5922
		public abstract void CompileGrammar(string grxmlGrammarPath, string compiledGrammarPath, CultureInfo culture);

		// Token: 0x06001723 RID: 5923
		public abstract void CheckGrammarEntryFormat(string wordToCheck);

		// Token: 0x06001724 RID: 5924
		public abstract ITempWavFile SynthesizePromptsToPcmWavFile(ArrayList prompts);

		// Token: 0x06001725 RID: 5925
		public abstract void RecycleServiceDependencies();

		// Token: 0x06001726 RID: 5926
		public abstract void InitializeG723Support();

		// Token: 0x06001727 RID: 5927 RVA: 0x00063524 File Offset: 0x00061724
		private static Platform Create(PlatformType platformType)
		{
			Platform result;
			switch (platformType)
			{
			case PlatformType.MSS:
				result = Platform.CreateDynamicPlatform("Microsoft.Exchange.UM.MSSPlatform.dll", "Microsoft.Exchange.UM.MSSPlatform.MSSPlatform");
				break;
			case PlatformType.UCMA:
				result = Platform.CreateDynamicPlatform("Microsoft.Exchange.UM.UcmaPlatform.dll", "Microsoft.Exchange.UM.UcmaPlatform.UcmaPlatform");
				break;
			default:
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Unknown platform type {0}", new object[]
				{
					platformType.ToString(),
					"platform"
				}));
			}
			return result;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000635A0 File Offset: 0x000617A0
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private static void FirstChanceHandler(object source, FirstChanceExceptionEventArgs e)
		{
			if (Thread.CurrentThread.ManagedThreadId == Platform.platformThreadId)
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMWorkerProcessUnhandledException, null, new object[]
				{
					"In First chance handler: " + e.Exception.ToString()
				});
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "In First chance handler: {0}", new object[]
				{
					e.Exception
				});
				ProcessLog.WriteLine(e.Exception.StackTrace, new object[0]);
				ExceptionHandling.SendWatsonWithExtraData(e.Exception, true);
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00063638 File Offset: 0x00061838
		private static Platform CreateDynamicPlatform(string assemblyName, string className)
		{
			string text = Path.Combine(Utils.GetExchangeDirectory(), "bin");
			text = Path.Combine(text, assemblyName);
			if (Utils.GetLocalHostFqdn().ToLowerInvariant().EndsWith("extest.microsoft.com") && Process.GetCurrentProcess().ProcessName.ToLowerInvariant().Contains("umworkerprocess"))
			{
				Platform.platformThreadId = Thread.CurrentThread.ManagedThreadId;
				AppDomain.CurrentDomain.FirstChanceException += Platform.FirstChanceHandler;
				try
				{
					ObjectHandle objectHandle = Activator.CreateInstanceFrom(text, className);
					return (Platform)objectHandle.Unwrap();
				}
				finally
				{
					AppDomain.CurrentDomain.FirstChanceException -= Platform.FirstChanceHandler;
				}
			}
			ObjectHandle objectHandle2 = Activator.CreateInstanceFrom(text, className);
			return (Platform)objectHandle2.Unwrap();
		}

		// Token: 0x04000D8F RID: 3471
		private static Platform instance;

		// Token: 0x04000D90 RID: 3472
		private static object staticLock = new object();

		// Token: 0x04000D91 RID: 3473
		private static int platformThreadId;
	}
}
