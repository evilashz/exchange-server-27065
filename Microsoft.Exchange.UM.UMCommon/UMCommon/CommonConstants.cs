using System;
using System.Globalization;
using System.Net.Mime;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200003B RID: 59
	internal class CommonConstants
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B1F7 File Offset: 0x000093F7
		internal static bool UseDataCenterLogging
		{
			get
			{
				return CommonConstants.useDataCenterLogging;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B1FE File Offset: 0x000093FE
		internal static bool UseDataCenterCallRouting
		{
			get
			{
				return CommonConstants.useDataCenterRouting;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000B205 File Offset: 0x00009405
		internal static bool DataCenterADPresent
		{
			get
			{
				return CommonConstants.dataCenterADPresent;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B20C File Offset: 0x0000940C
		internal static int? MaxCallsAllowed
		{
			get
			{
				return CommonConstants.maxCallsAllowed.Value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000B218 File Offset: 0x00009418
		internal static string ApplicationVersion
		{
			get
			{
				return CommonConstants.applicationVersion;
			}
		}

		// Token: 0x040000EC RID: 236
		internal const string UMRegKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole";

		// Token: 0x040000ED RID: 237
		internal const string TestModeRegKeyName = "TestMode";

		// Token: 0x040000EE RID: 238
		internal const string DummyDTMFusedForFindMe = "D";

		// Token: 0x040000EF RID: 239
		internal const string UMServiceShortName = "MSExchangeUM";

		// Token: 0x040000F0 RID: 240
		internal const string UMCallRouterShortName = "MSExchangeUMCR";

		// Token: 0x040000F1 RID: 241
		internal const string ExchangeDirectoryRegKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x040000F2 RID: 242
		internal const string ExchangeDirectoryRegValue = "MSIInstallPath";

		// Token: 0x040000F3 RID: 243
		internal const string ADTopologyServiceShortName = "MSExchangeADTopology";

		// Token: 0x040000F4 RID: 244
		internal const string CNGKeyIsolationServiceShortName = "KeyIso";

		// Token: 0x040000F5 RID: 245
		internal const int CbPasswordSalt = 8;

		// Token: 0x040000F6 RID: 246
		internal const int CbPasswordHash = 16;

		// Token: 0x040000F7 RID: 247
		internal const string SHA256Name = "SHA256";

		// Token: 0x040000F8 RID: 248
		internal const string SHA512Name = "SHA512";

		// Token: 0x040000F9 RID: 249
		internal const string SHA256NamePreSP1Beta = "SHA256CryptoServiceProvider";

		// Token: 0x040000FA RID: 250
		internal const string PBKDF1Algorithm = "SHA256";

		// Token: 0x040000FB RID: 251
		internal const int PBKDF1Iterations = 1000;

		// Token: 0x040000FC RID: 252
		internal const string DefaultWorkerProcessName = "UMworkerprocess.exe";

		// Token: 0x040000FD RID: 253
		internal const int WorkerProcessStartupTime = 30;

		// Token: 0x040000FE RID: 254
		internal const string PlusSign = "+";

		// Token: 0x040000FF RID: 255
		internal const string TLStransportHeader = ";transport=TLS";

		// Token: 0x04000100 RID: 256
		internal const int MaxAttemptsToRestartUMServiceEndpoint = 5;

		// Token: 0x04000101 RID: 257
		internal const string TCPtransportHeader = ";transport=TCP";

		// Token: 0x04000102 RID: 258
		internal const string JobObjectName = "Microsoft Exchange UM Job";

		// Token: 0x04000103 RID: 259
		internal const string SmtpAddressPrefix = "SMTP:";

		// Token: 0x04000104 RID: 260
		internal const string SIPAddressPrefix = "SIP:";

		// Token: 0x04000105 RID: 261
		internal const string UserPhoneParam = "user=phone";

		// Token: 0x04000106 RID: 262
		internal const string ReferredByHeader = "Referred-By";

		// Token: 0x04000107 RID: 263
		internal const string SupportedHeader = "Supported";

		// Token: 0x04000108 RID: 264
		internal const string MsFeHeader = "ms-fe";

		// Token: 0x04000109 RID: 265
		internal const int MsOrganizationMaxEntries = 3;

		// Token: 0x0400010A RID: 266
		internal const string MSUMCallOnBehalfOf = "X-MSUM-Call-On-Behalf-Of";

		// Token: 0x0400010B RID: 267
		internal const string MSUMOriginatingSessionCallId = "X-MSUM-Originating-Session-Call-Id";

		// Token: 0x0400010C RID: 268
		internal const string ToHeader = "To";

		// Token: 0x0400010D RID: 269
		internal const string ContactHeader = "Contact";

		// Token: 0x0400010E RID: 270
		internal const string StopSemaphoreName = "Global\\ExchangeUMStopKey-";

		// Token: 0x0400010F RID: 271
		internal const string ResetSemaphoreName = "Global\\ExchangeUMResetKey-";

		// Token: 0x04000110 RID: 272
		internal const string FatalSemaphoreName = "Global\\ExchangeUMFatalKey-";

		// Token: 0x04000111 RID: 273
		internal const string ReadySemaphoreName = "Global\\ExchangeUMReadyKey-";

		// Token: 0x04000112 RID: 274
		internal const string PortArg = "-port:";

		// Token: 0x04000113 RID: 275
		internal const string StopKeyArg = "-stopkey:";

		// Token: 0x04000114 RID: 276
		internal const string ResetKeyArg = "-resetkey:";

		// Token: 0x04000115 RID: 277
		internal const string FatalKeyArg = "-fatalkey:";

		// Token: 0x04000116 RID: 278
		internal const string ReadyKeyArg = "-readykey:";

		// Token: 0x04000117 RID: 279
		internal const string TempDirArg = "-tempdir:";

		// Token: 0x04000118 RID: 280
		internal const string SipPortArg = "-sipport:";

		// Token: 0x04000119 RID: 281
		internal const string PerfArg = "-perfenabled:";

		// Token: 0x0400011A RID: 282
		internal const string StartupModeArg = "-startupMode:";

		// Token: 0x0400011B RID: 283
		internal const string CertThumbprintArg = "-thumbprint:";

		// Token: 0x0400011C RID: 284
		internal const string PassiveArg = "-passive";

		// Token: 0x0400011D RID: 285
		internal const string HeartBeatResponse = "HR";

		// Token: 0x0400011E RID: 286
		internal const int BytesInMB = 1048576;

		// Token: 0x0400011F RID: 287
		internal const double BytesInKB = 1024.0;

		// Token: 0x04000120 RID: 288
		internal const long BytesNeededForUMEnablement = 102400L;

		// Token: 0x04000121 RID: 289
		internal const int DefaultRetireTimeout = 1800;

		// Token: 0x04000122 RID: 290
		internal const int XSOResponseTimeoutMiliseconds = 3000;

		// Token: 0x04000123 RID: 291
		internal const int CAMailboxOperationThresholdMilliseconds = 4000;

		// Token: 0x04000124 RID: 292
		internal const int CAMailboxOperationTimeoutMilliseconds = 6000;

		// Token: 0x04000125 RID: 293
		internal const int DefaultSipTCPPortNumber = 5060;

		// Token: 0x04000126 RID: 294
		internal const int DefaultSipTLSPortNumber = 5061;

		// Token: 0x04000127 RID: 295
		internal const int DefaultDataCenterMailboxServerSipTLSPortNumber = 5063;

		// Token: 0x04000128 RID: 296
		internal const int ConnectionBufferSize = 10;

		// Token: 0x04000129 RID: 297
		internal const int MaxSemaphoreCreationAttempts = 10;

		// Token: 0x0400012A RID: 298
		internal const int StreamBufferSize = 4096;

		// Token: 0x0400012B RID: 299
		internal const string UmrecCfgSchemaFile = "umrecyclerconfig.xsd";

		// Token: 0x0400012C RID: 300
		internal const int PortRangeStart = 16000;

		// Token: 0x0400012D RID: 301
		internal const int PortRangeEnd = 17000;

		// Token: 0x0400012E RID: 302
		internal const string UMComponentGuid = "321b4079-df13-45c3-bbc9-2c610013dff4";

		// Token: 0x0400012F RID: 303
		internal const int ReadChunkSize = 32768;

		// Token: 0x04000130 RID: 304
		internal const int CbPaddedChecksum = 160;

		// Token: 0x04000131 RID: 305
		internal const char SmtpDelimiter = '@';

		// Token: 0x04000132 RID: 306
		internal const char DomainDelimiter = '.';

		// Token: 0x04000133 RID: 307
		internal const string AccessNumberSeparator = ", ";

		// Token: 0x04000134 RID: 308
		internal const int MaxAttempts = 100;

		// Token: 0x04000135 RID: 309
		internal const string DirectoryLookupEnabledAttributeName = "msExchHideFromAddressLists";

		// Token: 0x04000136 RID: 310
		internal const string DialPlanContainerName = "CN=UM DialPlan Container";

		// Token: 0x04000137 RID: 311
		internal const string AutoAttendantContainerName = "CN=UM AutoAttendant Container";

		// Token: 0x04000138 RID: 312
		internal const string UMIPGatewayContainerName = "CN=UM IPGateway Container";

		// Token: 0x04000139 RID: 313
		internal const uint MaxUMMailSize = 2097152U;

		// Token: 0x0400013A RID: 314
		internal const int WmaBitsperSec = 13312;

		// Token: 0x0400013B RID: 315
		internal const int WmaHeaderBytes = 8192;

		// Token: 0x0400013C RID: 316
		internal const int MaxAudioDataMegabytes = 5;

		// Token: 0x0400013D RID: 317
		internal const int PromptFileNameMaximumLength = 128;

		// Token: 0x0400013E RID: 318
		internal const string PromptFileExtension = ".wav";

		// Token: 0x0400013F RID: 319
		internal const string TifFileExtension = ".tif";

		// Token: 0x04000140 RID: 320
		internal const string TmpFileExtension = ".tmp";

		// Token: 0x04000141 RID: 321
		internal const string LogFileExtension = ".log";

		// Token: 0x04000142 RID: 322
		internal const string WmaContentType = "audio/wma";

		// Token: 0x04000143 RID: 323
		internal const string GsmContentType = "audio/gsm";

		// Token: 0x04000144 RID: 324
		internal const string Mp3ContentType = "audio/mp3";

		// Token: 0x04000145 RID: 325
		internal const string WavContentType = "audio/wav";

		// Token: 0x04000146 RID: 326
		internal const string TiffContentType = "image/tiff";

		// Token: 0x04000147 RID: 327
		internal const string XmlContentType = "text/xml";

		// Token: 0x04000148 RID: 328
		internal const bool PrecompileGrammars = true;

		// Token: 0x04000149 RID: 329
		internal const string GrammarFileExtension = ".grxml";

		// Token: 0x0400014A RID: 330
		internal const string CompiledGrammarFileExtension = ".cfg";

		// Token: 0x0400014B RID: 331
		internal const string CommonGrammarFileName = "common.grxml";

		// Token: 0x0400014C RID: 332
		internal const string PeopleSearchGrammarTemplateFileName = "peoplesearchtemplate.grxml";

		// Token: 0x0400014D RID: 333
		internal const string PromptResourceBaseName = "Microsoft.Exchange.UM.Prompts.Prompts.Strings";

		// Token: 0x0400014E RID: 334
		internal const string PromptResourceAssemblyName = "Microsoft.Exchange.UM.Prompts";

		// Token: 0x0400014F RID: 335
		internal const string GrammarResourceBaseName = "Microsoft.Exchange.UM.Grammars.Grammars.Strings";

		// Token: 0x04000150 RID: 336
		internal const string GrammarResourceAssemblyName = "Microsoft.Exchange.UM.Grammars";

		// Token: 0x04000151 RID: 337
		internal const string LocConfigResourceBaseName = "Microsoft.Exchange.UM.UMCommon.LocConfig.Strings";

		// Token: 0x04000152 RID: 338
		internal const string LocConfigResourceAssemblyName = "Microsoft.Exchange.UM.UMCommon";

		// Token: 0x04000153 RID: 339
		internal const string LanguagePromptNameFormat = "Language-{0}";

		// Token: 0x04000154 RID: 340
		internal const int UMMailboxPolicyNameMaxLength = 64;

		// Token: 0x04000155 RID: 341
		internal const string OWAInstanceName = "OWA";

		// Token: 0x04000156 RID: 342
		internal const string OutlookInstanceName = "Outlook";

		// Token: 0x04000157 RID: 343
		internal const string LocalHost = "localhost";

		// Token: 0x04000158 RID: 344
		internal const string PersonalContactEwsType = "Contact";

		// Token: 0x04000159 RID: 345
		internal const string ADUserEwsType = "Mailbox";

		// Token: 0x0400015A RID: 346
		internal const string ActiveWPFileName = "wp.active";

		// Token: 0x0400015B RID: 347
		internal const string DataCenterAutoAttendantPerfCounterName = "DataCenterAutoAttendantPerfCounterName";

		// Token: 0x0400015C RID: 348
		internal const string BadVoiceMailPath = "UnifiedMessaging\\badvoicemail";

		// Token: 0x0400015D RID: 349
		internal const string ExchangeBinFolder = "bin";

		// Token: 0x0400015E RID: 350
		internal const string VoiceMailPath = "UnifiedMessaging\\voicemail";

		// Token: 0x0400015F RID: 351
		internal const string LogFilePath = "UnifiedMessaging\\log";

		// Token: 0x04000160 RID: 352
		internal const string TempPath = "UnifiedMessaging\\temp";

		// Token: 0x04000161 RID: 353
		internal const string UMTempFilePath = "UnifiedMessaging\\temp\\UMTempFiles";

		// Token: 0x04000162 RID: 354
		internal const string ServiceRegistryKey = "MSExchange Unified Messaging";

		// Token: 0x04000163 RID: 355
		internal const string ServiceRegistryKeyPath = "System\\CurrentControlSet\\Services\\MSExchange Unified Messaging";

		// Token: 0x04000164 RID: 356
		internal const string ParameterRegistryKeyPath = "System\\CurrentControlSet\\Services\\MSExchange Unified Messaging\\Parameters";

		// Token: 0x04000165 RID: 357
		internal const string EnableBadVoiceMailFolder = "EnableBadVoiceMailFolder";

		// Token: 0x04000166 RID: 358
		internal const int TestDtmfPort = 7001;

		// Token: 0x04000167 RID: 359
		internal const string MSSPlatformAssembly = "Microsoft.Exchange.UM.MSSPlatform.dll";

		// Token: 0x04000168 RID: 360
		internal const string RTCPlatformAssembly = "Microsoft.Exchange.UM.RTCPlatform.dll";

		// Token: 0x04000169 RID: 361
		internal const string IntelPlatformAssembly = "Microsoft.Exchange.UM.IntelPlatform.dll";

		// Token: 0x0400016A RID: 362
		internal const string UcmaPlatformAssembly = "Microsoft.Exchange.UM.UcmaPlatform.dll";

		// Token: 0x0400016B RID: 363
		internal const string TestPlatformAssembly = "Microsoft.Exchange.UM.TestPlatform.dll";

		// Token: 0x0400016C RID: 364
		internal const string MSSPlatformClassName = "Microsoft.Exchange.UM.MSSPlatform.MSSPlatform";

		// Token: 0x0400016D RID: 365
		internal const string UcmaPlatformClassName = "Microsoft.Exchange.UM.UcmaPlatform.UcmaPlatform";

		// Token: 0x0400016E RID: 366
		internal const float MinimumConfidence = 0.25f;

		// Token: 0x0400016F RID: 367
		public const string FieldSeparator = "​";

		// Token: 0x04000170 RID: 368
		public const char FakeColumnSeparator = '‚';

		// Token: 0x04000171 RID: 369
		internal static readonly System.Net.Mime.ContentType ContentTypeTextPlain = new System.Net.Mime.ContentType("text/plain");

		// Token: 0x04000172 RID: 370
		internal static readonly char[] MsOrganizationDomainSeparators = new char[]
		{
			',',
			';',
			' '
		};

		// Token: 0x04000173 RID: 371
		internal static readonly CultureInfo DefaultCulture = UMLanguage.DefaultLanguage.Culture;

		// Token: 0x04000174 RID: 372
		private static readonly string applicationVersion = typeof(CommonConstants).GetApplicationVersion();

		// Token: 0x04000175 RID: 373
		private static readonly bool useDataCenterLogging = Utils.GetDatacenterLoggingEnabled();

		// Token: 0x04000176 RID: 374
		private static readonly bool useDataCenterRouting = Utils.GetDatacenterRoutingEnabled();

		// Token: 0x04000177 RID: 375
		private static readonly bool dataCenterADPresent = Utils.GetDatacenterADPresent();

		// Token: 0x04000178 RID: 376
		private static readonly Lazy<int?> maxCallsAllowed = new Lazy<int?>(new Func<int?>(Utils.GetMaxCallsAllowed));

		// Token: 0x0200003C RID: 60
		[Flags]
		internal enum UMOutlookUIFlags
		{
			// Token: 0x0400017A RID: 378
			None = 0,
			// Token: 0x0400017B RID: 379
			VoicemailForm = 1,
			// Token: 0x0400017C RID: 380
			VoicemailOptions = 2
		}

		// Token: 0x0200003D RID: 61
		internal enum ApplicationState
		{
			// Token: 0x0400017E RID: 382
			Idle,
			// Token: 0x0400017F RID: 383
			Playing,
			// Token: 0x04000180 RID: 384
			Recording,
			// Token: 0x04000181 RID: 385
			DtmfWait,
			// Token: 0x04000182 RID: 386
			SpeechWait
		}

		// Token: 0x0200003E RID: 62
		internal enum TaskCallType
		{
			// Token: 0x04000184 RID: 388
			Voice,
			// Token: 0x04000185 RID: 389
			Fax,
			// Token: 0x04000186 RID: 390
			OutCall
		}

		// Token: 0x0200003F RID: 63
		internal abstract class Transcription
		{
			// Token: 0x02000040 RID: 64
			internal abstract class Xml
			{
				// Token: 0x04000187 RID: 391
				internal const string Language = "lang";

				// Token: 0x04000188 RID: 392
				internal const string Confidence = "confidence";

				// Token: 0x04000189 RID: 393
				internal const string ConfidenceBand = "confidenceBand";

				// Token: 0x0400018A RID: 394
				internal const string RecognitionResult = "recognitionResult";

				// Token: 0x0400018B RID: 395
				internal const string RecognitionError = "recognitionError";

				// Token: 0x0400018C RID: 396
				internal const string SchemaVersion = "schemaVersion";

				// Token: 0x0400018D RID: 397
				internal const string ProductID = "productID";

				// Token: 0x0400018E RID: 398
				internal const string ProductVersion = "productVersion";

				// Token: 0x0400018F RID: 399
				internal const string Break = "Break";

				// Token: 0x04000190 RID: 400
				internal const string Weight = "wt";

				// Token: 0x04000191 RID: 401
				internal const string High = "high";

				// Token: 0x04000192 RID: 402
				internal const string Medium = "medium";

				// Token: 0x04000193 RID: 403
				internal const string Low = "low";

				// Token: 0x04000194 RID: 404
				internal const string Feature = "Feature";

				// Token: 0x04000195 RID: 405
				internal const string FeatureClass = "class";

				// Token: 0x04000196 RID: 406
				internal const string Reference = "reference";

				// Token: 0x04000197 RID: 407
				internal const string Reference2 = "reference2";

				// Token: 0x04000198 RID: 408
				internal const string NextNodeId = "nx";

				// Token: 0x04000199 RID: 409
				internal const string Id = "id";

				// Token: 0x0400019A RID: 410
				internal const string ConfidenceTag = "c";

				// Token: 0x0400019B RID: 411
				internal const string StartTimeOffset = "ts";

				// Token: 0x0400019C RID: 412
				internal const string EndTimeOffset = "te";

				// Token: 0x0400019D RID: 413
				internal const string BE = "be";

				// Token: 0x0400019E RID: 414
				internal const string EvmNamespace = "http://schemas.microsoft.com/exchange/um/2010/evm";

				// Token: 0x0400019F RID: 415
				internal const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

				// Token: 0x040001A0 RID: 416
				internal const string XsiNamespaceTag = "xmlns:xsi";

				// Token: 0x040001A1 RID: 417
				internal const string AsrTag = "ASR";

				// Token: 0x040001A2 RID: 418
				internal const string TextTag = "Text";

				// Token: 0x040001A3 RID: 419
				internal const string PhoneNumber = "PhoneNumber";

				// Token: 0x040001A4 RID: 420
				internal const string InformationTag = "Information";

				// Token: 0x040001A5 RID: 421
				internal const string ErrorInformationTag = "ErrorInformation";

				// Token: 0x040001A6 RID: 422
				internal const string LinkText = "linkText";

				// Token: 0x040001A7 RID: 423
				internal const string LinkURL = "linkURL";

				// Token: 0x040001A8 RID: 424
				internal const string LinkURLValue = "http://go.microsoft.com/fwlink/?LinkId=150048";
			}
		}

		// Token: 0x02000041 RID: 65
		internal abstract class UserConfig
		{
			// Token: 0x040001A9 RID: 425
			internal const string Greetings = "Um.CustomGreetings";

			// Token: 0x040001AA RID: 426
			internal const string Password = "Um.Password";

			// Token: 0x040001AB RID: 427
			internal const string General = "Um.General";

			// Token: 0x040001AC RID: 428
			internal const string OWA = "OWA.UserOptions";

			// Token: 0x040001AD RID: 429
			internal const string PersonalAutoAttendant = "UM.E14.PersonalAutoAttendants";

			// Token: 0x040001AE RID: 430
			internal const string Outlook = "UMOLK.UserOptions";

			// Token: 0x040001AF RID: 431
			internal const string External = "External";

			// Token: 0x040001B0 RID: 432
			internal const string Oof = "Oof";

			// Token: 0x040001B1 RID: 433
			internal const string BlockedNumbers = "BlockedNumbers";

			// Token: 0x040001B2 RID: 434
			internal const string Name = "RecordedName";

			// Token: 0x040001B3 RID: 435
			internal const string Current = "Password";

			// Token: 0x040001B4 RID: 436
			internal const string TimeSet = "PasswordSetTime";

			// Token: 0x040001B5 RID: 437
			internal const string PreviousPasswords = "PreviousPasswords";

			// Token: 0x040001B6 RID: 438
			internal const string LockoutCount = "LockoutCount";

			// Token: 0x040001B7 RID: 439
			internal const string FirstTime = "FirstTimeUser";

			// Token: 0x040001B8 RID: 440
			internal const string OofStatus = "OofStatus";

			// Token: 0x040001B9 RID: 441
			internal const string PlayOnPhoneDialString = "PlayOnPhoneDialString";

			// Token: 0x040001BA RID: 442
			internal const string TelephoneAccessFolderEmail = "TelephoneAccessFolderEmail";

			// Token: 0x040001BB RID: 443
			internal const string UseAsr = "UseAsr";

			// Token: 0x040001BC RID: 444
			internal const string ReceivedVoiceMailPreviewEnabled = "ReceivedVoiceMailPreviewEnabled";

			// Token: 0x040001BD RID: 445
			internal const string SentVoiceMailPreviewEnabled = "SentVoiceMailPreviewEnabled";

			// Token: 0x040001BE RID: 446
			internal const string ReadUnreadVoicemailInFIFOOrder = "ReadUnreadVoicemailInFIFOOrder";

			// Token: 0x040001BF RID: 447
			internal const string PromptCount = "PromptCount_";

			// Token: 0x040001C0 RID: 448
			internal const string TimeZone = "timezone";

			// Token: 0x040001C1 RID: 449
			internal const string TimeFormat = "timeformat";

			// Token: 0x040001C2 RID: 450
			internal const string OutlookFlags = "outlookFlags";

			// Token: 0x040001C3 RID: 451
			internal const int MaxPasswordDays = 36500;

			// Token: 0x040001C4 RID: 452
			internal const int MaxPasswordLength = 24;

			// Token: 0x040001C5 RID: 453
			internal const int MinPasswordLength = 4;

			// Token: 0x040001C6 RID: 454
			internal const string ChecksumAttributeName = "msExchTUIPassword";

			// Token: 0x040001C7 RID: 455
			internal const int CbMaxAdGreeting = 32768;

			// Token: 0x040001C8 RID: 456
			internal const string VoiceNotificationStatus = "VoiceNotificationStatus";
		}

		// Token: 0x02000042 RID: 66
		internal abstract class SipInfo
		{
			// Token: 0x040001C9 RID: 457
			internal const string UserAgent = "Unified Messaging WebService";

			// Token: 0x040001CA RID: 458
			internal const string MessageIdHeaderName = "UMWS-MESSAGE-ID";

			// Token: 0x040001CB RID: 459
			internal const string MessageTypeHeaderName = "UMWS-MESSAGE-TYPE";

			// Token: 0x040001CC RID: 460
			internal const int PingTimeout = 30;

			// Token: 0x040001CD RID: 461
			internal static readonly System.Net.Mime.ContentType XmlContentType = new System.Net.Mime.ContentType("text/xml");
		}

		// Token: 0x02000043 RID: 67
		internal abstract class DiagnosticTool
		{
			// Token: 0x040001CE RID: 462
			internal const string UserAgent = "MSExchangeUM-Diagnostics";

			// Token: 0x040001CF RID: 463
			internal const string UserAgentHeaderName = "user-agent";

			// Token: 0x040001D0 RID: 464
			internal const string DiagnosticHeaderName = "msexum-connectivitytest";

			// Token: 0x040001D1 RID: 465
			internal const string LocalDiagnosticHeaderValue = "local";

			// Token: 0x040001D2 RID: 466
			internal const string RemoteDiagnosticHeaderValue = "remote";

			// Token: 0x040001D3 RID: 467
			internal const string DiagnosticInfoMsg = "UM Operation Check";

			// Token: 0x040001D4 RID: 468
			internal const string DiagnosticInfoHeader = "UMTUCFirstResponse";

			// Token: 0x040001D5 RID: 469
			internal const string DiagnosticDialPlanName = "UMDialPlan";

			// Token: 0x040001D6 RID: 470
			internal const string DiagnosticInfoResp = "OK";

			// Token: 0x040001D7 RID: 471
			internal const int DiagnosticToolInterDigitSendGapforTUITest = 500;

			// Token: 0x040001D8 RID: 472
			internal const int DiagnosticToolInitialResponseGap = 1000;

			// Token: 0x040001D9 RID: 473
			internal const int DiagnosticToolInterDigitSendGap = 100;

			// Token: 0x040001DA RID: 474
			internal const int DiagnosticToolToneDuration = 100;

			// Token: 0x040001DB RID: 475
			internal const int DiagnosticToolFaxToneDuration = 4000;

			// Token: 0x040001DC RID: 476
			internal const string DiagnosticSequence = "ABCD*#0123456789";

			// Token: 0x040001DD RID: 477
			internal const int DiagnosticSipPort = 9000;

			// Token: 0x040001DE RID: 478
			internal const int DiagnosticINFOTimeout = 20;

			// Token: 0x040001DF RID: 479
			internal const int DiagnosticMediaEstablishmentTimeout = 60;

			// Token: 0x040001E0 RID: 480
			internal const int DiagnosticToolSrvResponseWaitTimeout = 30;

			// Token: 0x040001E1 RID: 481
			internal const int DiagnosticINFOStateChangeTimeout = 60;

			// Token: 0x040001E2 RID: 482
			internal const int DiagnosticDTMFTimeout = 270;

			// Token: 0x040001E3 RID: 483
			internal const int UMPingRetryLimit = 2;

			// Token: 0x040001E4 RID: 484
			internal const int OptionsRespCode = 200;

			// Token: 0x040001E5 RID: 485
			internal const int OptionsServerUnavailableRespCode = 503;

			// Token: 0x040001E6 RID: 486
			internal const int OptionsForbidden = 403;

			// Token: 0x040001E7 RID: 487
			internal const string OptionsServerUnavailableRespText = "Service Unavailable";

			// Token: 0x040001E8 RID: 488
			internal const int OptionsServerInternalErrorRespCode = 500;

			// Token: 0x040001E9 RID: 489
			internal const string OptionsServerInternalErrorRespText = "Server Internal Error";

			// Token: 0x040001EA RID: 490
			internal const string ContentType = "application/sdp";

			// Token: 0x040001EB RID: 491
			internal const string GatewayNotFoundResponseText = "Gateway not found";

			// Token: 0x040001EC RID: 492
			internal const int GatewayNotFoundResponseCode = 404;

			// Token: 0x040001ED RID: 493
			internal const int MovedTemporarilyResponseCode = 302;

			// Token: 0x040001EE RID: 494
			internal const int ProxyRedirectResponseCode = 303;

			// Token: 0x040001EF RID: 495
			internal const int DeclinedResponseCode = 603;

			// Token: 0x040001F0 RID: 496
			internal static readonly TimeSpan UMPingResponseTimeout = TimeSpan.FromSeconds(4.0);

			// Token: 0x040001F1 RID: 497
			internal static readonly TimeSpan UMPingRetryWaitTime = TimeSpan.FromSeconds(60.0);
		}

		// Token: 0x02000044 RID: 68
		internal abstract class XsoUtil
		{
			// Token: 0x040001F2 RID: 498
			internal const string ItemClassSeparator = ".";

			// Token: 0x040001F3 RID: 499
			internal const char VoiceMessageAttachmentOrderSeparator = ';';
		}

		// Token: 0x02000045 RID: 69
		internal abstract class MessageContentBuilder
		{
			// Token: 0x040001F4 RID: 500
			internal const string UTF8CharSet = "utf-8";
		}

		// Token: 0x02000046 RID: 70
		internal abstract class PromptProvisioning
		{
			// Token: 0x040001F5 RID: 501
			internal const string ManifestFileName = "ExchangeUM.xml";

			// Token: 0x040001F6 RID: 502
			internal const string PublishingPointShareName = "ExchangeUM";

			// Token: 0x040001F7 RID: 503
			internal const string LocalPublishingPointPath = "UnifiedMessaging\\Prompts\\Custom";

			// Token: 0x040001F8 RID: 504
			internal const string LocalPickupPath = "UnifiedMessaging\\Prompts\\Pickup";

			// Token: 0x040001F9 RID: 505
			internal const string LocalPromptCachePath = "UnifiedMessaging\\Prompts\\Cache";

			// Token: 0x040001FA RID: 506
			internal static readonly TimeSpan PublishingLockRetryInterval = TimeSpan.FromMilliseconds(500.0);

			// Token: 0x040001FB RID: 507
			internal static readonly TimeSpan PublishingLockRetryTimeout = TimeSpan.FromMilliseconds(5000.0);

			// Token: 0x040001FC RID: 508
			internal static readonly TimeSpan PromptCacheLockRetryTimeout = TimeSpan.FromMilliseconds(5000.0);

			// Token: 0x040001FD RID: 509
			internal static readonly TimeSpan KeepOrphanFilesInterval = TimeSpan.FromDays(1.0);

			// Token: 0x040001FE RID: 510
			internal static readonly WaveFormat SupportedInputFormat = WaveFormat.Pcm8WaveFormat;
		}

		// Token: 0x02000047 RID: 71
		internal abstract class SipHeaders
		{
			// Token: 0x040001FF RID: 511
			internal const string DiversionHeader = "Diversion";

			// Token: 0x04000200 RID: 512
			internal const string DiversionHeaderVariant = "CC-Diversion";

			// Token: 0x04000201 RID: 513
			internal const string HistoryInfoHeader = "History-Info";

			// Token: 0x04000202 RID: 514
			internal const string TransportModeTcp = "TCP";

			// Token: 0x04000203 RID: 515
			internal const string TransportModeTls = "TLS";

			// Token: 0x04000204 RID: 516
			internal const string SIPAddressPrefix = "SIP:";

			// Token: 0x04000205 RID: 517
			internal const string TELAddressPrefix = "TEL:";

			// Token: 0x04000206 RID: 518
			internal const string EnclosedSipFormat = "<{0}>";

			// Token: 0x04000207 RID: 519
			internal const string SipUriFormat = "sip:{0}";

			// Token: 0x04000208 RID: 520
			internal const string SipUriFormatWithPort = "sip:{0}:{1}";

			// Token: 0x04000209 RID: 521
			internal const string SipUriFormatWithUserInfo = "sip:{0}@{1}";

			// Token: 0x0400020A RID: 522
			internal const string OpaqueParameter = "opaque";

			// Token: 0x0400020B RID: 523
			internal const string GruuParameter = "gruu";

			// Token: 0x0400020C RID: 524
			internal const string AppExumPrefix = "app:exum:";

			// Token: 0x0400020D RID: 525
			internal const string TCPtransportHeader = ";transport=TCP";

			// Token: 0x0400020E RID: 526
			internal const string TLStransportHeader = ";transport=TLS";

			// Token: 0x0400020F RID: 527
			internal const string UserPhoneParam = "user=phone";

			// Token: 0x04000210 RID: 528
			internal const string ReferredByHeader = "Referred-By";

			// Token: 0x04000211 RID: 529
			internal const string ToHeader = "To";

			// Token: 0x04000212 RID: 530
			internal const string ContactHeader = "Contact";

			// Token: 0x04000213 RID: 531
			internal const string EventHeader = "Event";

			// Token: 0x04000214 RID: 532
			internal const string MSApplicationAorHeader = "ms-application-aor";

			// Token: 0x04000215 RID: 533
			internal const string MsOrganization = "ms-organization";

			// Token: 0x04000216 RID: 534
			internal const string MsOrganizationGuid = "ms-organization-guid";

			// Token: 0x04000217 RID: 535
			internal const string UserDefault = "user-default";

			// Token: 0x04000218 RID: 536
			internal const string MAddrParameter = "maddr";
		}

		// Token: 0x02000048 RID: 72
		internal abstract class UMVersions
		{
			// Token: 0x04000219 RID: 537
			internal static readonly QueryFilter CompatibleServersFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E15MinVersion),
				new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ServerSchema.VersionNumber, Server.E16MinVersion)
			});

			// Token: 0x0400021A RID: 538
			internal static readonly QueryFilter E12LegacyServerFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E2007MinVersion),
				new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E14MinVersion)
			});

			// Token: 0x0400021B RID: 539
			internal static readonly QueryFilter E14LegacyServerFilter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.E14MinVersion),
				new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.E15MinVersion)
			});
		}

		// Token: 0x02000049 RID: 73
		internal abstract class GalGrammar
		{
			// Token: 0x0400021C RID: 540
			internal const string LocalGrammarCachePath = "UnifiedMessaging\\grammars";

			// Token: 0x0400021D RID: 541
			internal const int MaxEquivalentForms = 1;

			// Token: 0x0400021E RID: 542
			internal const string FileName = "gal";

			// Token: 0x0400021F RID: 543
			internal const string FileExtension = ".grxml";

			// Token: 0x04000220 RID: 544
			internal const string DistributionListGrammarFileName = "distributionList";

			// Token: 0x04000221 RID: 545
			internal const string FileNameComponentSeparator = "_";

			// Token: 0x04000222 RID: 546
			internal const string GeneratedGrammarsRootName = "Names";

			// Token: 0x04000223 RID: 547
			internal const string RuleElementName = "rule";

			// Token: 0x04000224 RID: 548
			internal const string BucketFilePrefix = "\r\n<grammar root=\"Names\" xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" xmlns:sapi=\"http://schemas.microsoft.com/Speech/2002/06/SRGSExtensions\" tag-format=\"semantics-ms/1.0\">\r\n\t<!-- BucketLevel File Grammar -->\r\n";

			// Token: 0x04000225 RID: 549
			internal const string GrammarPrefix = "\r\n<grammar root=\"Names\" xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" xmlns:sapi=\"http://schemas.microsoft.com/Speech/2002/06/SRGSExtensions\" tag-format=\"semantics-ms/1.0\">";

			// Token: 0x04000226 RID: 550
			internal const string PersonalContactsRulePrefix = "\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n\t\t<tag>$.ResultType={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid._value=\"{0}\";</tag>\r\n\t\t<tag>$.ContactId={{}};</tag>\r\n\t\t<tag>$.ContactName={{}};</tag>\r\n\t\t<tag>$.DisambiguationField={{}};</tag>\r\n";

			// Token: 0x04000227 RID: 551
			internal const string MowaPersonalContactsRulePrefix = "\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n\t\t<tag>$.ResultType={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid={{}};</tag>\r\n\t\t<tag>$.UmSubscriberObjectGuid._value=\"{0}\";</tag>\r\n\t\t<tag>$.ContactId={{}};</tag>\r\n\t\t<tag>$.ContactName={{}};</tag>\r\n\t\t<tag>$.DisambiguationField={{}};</tag>\r\n\t\t<tag>$.PersonId={{}};</tag>\r\n\t\t<tag>$.GALLinkID={{}};</tag>\r\n";

			// Token: 0x04000228 RID: 552
			internal const string DirectoryContactRulePrefix = "\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={};</tag>\r\n\t\t<tag>$.ResultType={};</tag>\r\n\t\t<tag>$.RecoEvent._value=\"recoNameOrDepartment\";</tag>\r\n\t\t<tag>$.ResultType._value=\"DirectoryContact\";</tag>\r\n\t\t<tag>$.ObjectGuid={};</tag>\r\n\t\t<tag>$.SMTP={};</tag>\r\n\t\t<tag>$.ContactName={};</tag>\r\n";

			// Token: 0x04000229 RID: 553
			internal const string OneOfPrefix = "\t\t<one-of>\r\n";

			// Token: 0x0400022A RID: 554
			internal const string OneOfSuffix = "\t\t</one-of>\r\n";

			// Token: 0x0400022B RID: 555
			internal const string ItemNode = "<item>{0}</item>";

			// Token: 0x0400022C RID: 556
			internal const string TopLevelFileOneOfSuffix = "\t\t</one-of>\r\n        <tag>$=$$;</tag>\r\n\t</rule>";

			// Token: 0x0400022D RID: 557
			internal const string RuleRefForBucketFile = "          <item>\r\n\t\t\t\t<ruleref uri=\"{0}#Names\"/>\r\n\t\t\t</item>\r\n";

			// Token: 0x0400022E RID: 558
			internal const string PersonalContactsRuleSuffix = "\r\n\t\t<!-- the following will add an option politeending to the recognition -->\r\n\t\t<item repeat=\"0-1\">\r\n\t\t\t<ruleref uri=\"{0}#politeEndPhrases\"/>\r\n\t\t</item>\r\n\t</rule>";

			// Token: 0x0400022F RID: 559
			internal const string DirectoryContactRuleSuffix = "\r\n\t\t<!-- the following will add an option politeending to the recognition -->\r\n\t\t<item repeat=\"0-1\">\r\n\t\t\t<ruleref uri=\"{0}#politeEndPhrases\"/>\r\n\t\t</item>\r\n\t</rule>";

			// Token: 0x04000230 RID: 560
			internal const string GrammarSuffix = "\r\n</grammar>";

			// Token: 0x04000231 RID: 561
			internal const string EmptyNamesRule = "\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<ruleref special=\"VOID\" />\r\n\t</rule>";

			// Token: 0x04000232 RID: 562
			internal const string DirectoryContactNode = "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.ObjectGuid._value=\"{2}\";\r\n\t\t\t\t\t$.SMTP._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{0}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n";

			// Token: 0x04000233 RID: 563
			internal const string PersonalContactNode = "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.RecoEvent._value=\"recoNameOrDepartment\";\r\n\t\t\t\t\t$.ResultType._value=\"PersonalContact\";\r\n\t\t\t\t\t$.ContactId._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{0}\";\r\n\t\t\t\t\t$.DisambiguationField._value=\"{2}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n";

			// Token: 0x04000234 RID: 564
			internal const string MowaPersonalContactNode = "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.RecoEvent._value=\"recoNameOrDepartment\";\r\n\t\t\t\t\t$.ResultType._value=\"PersonalContact\";\r\n\t\t\t\t\t$.ContactId._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{5}\";\r\n\t\t\t\t\t$.DisambiguationField._value=\"{2}\";\r\n\t\t\t\t\t$.PersonId._value=\"{3}\";\r\n\t\t\t\t\t$.GALLinkID._value=\"{4}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n";

			// Token: 0x04000235 RID: 565
			internal const string PromptForAliasPrefix = "<grammar xml:lang=\"{0}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" tag-format=\"semantics-ms/1.0\">\r\n\r\n\t<rule id=\"Names\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n\t\t<tag>$.ResultType={{}};</tag>\r\n\t\t<tag>$.ObjectGuid={{}};</tag>\r\n\t\t<tag>$.SMTP={{}};</tag>\r\n\t\t<tag>$.ContactName={{}};</tag>\r\n\t\t<one-of>\r\n";

			// Token: 0x04000236 RID: 566
			internal const string PromptForAliasUserNode = "\t\t\t<item>{0}\r\n\t\t\t\t<tag>\r\n\t\t\t\t\t$.RecoEvent._value=\"recoNameOrDepartment\";\r\n\t\t\t\t\t$.ResultType._value=\"DirectoryContact\";\r\n\t\t\t\t\t$.ObjectGuid._value=\"{3}\";\r\n\t\t\t\t\t$.SMTP._value=\"{1}\";\r\n\t\t\t\t\t$.ContactName._value=\"{2}\";\r\n\t\t\t\t</tag>\r\n\t\t\t</item>\r\n";

			// Token: 0x04000237 RID: 567
			internal const string PromptForAliasSuffix = "\r\n\t\t</one-of>\r\n\t\t<!-- the following will add an option politeending to the recognition -->\r\n\t\t<item repeat=\"0-1\">\r\n\t\t\t<ruleref uri=\"{0}#politeEndPhrases\"/>\r\n\t\t</item>\r\n\t</rule>\r\n</grammar>";

			// Token: 0x04000238 RID: 568
			internal const string CustomizedMenuGrammarHeader = "<grammar root=\"{0}\"\txml:lang=\"{2}\" version=\"1.0\" xmlns=\"http://www.w3.org/2001/06/grammar\" tag-format=\"semantics-ms/1.0\">\r\n\t<!-- NoGrammar rule recognizes phrases like 'No Sales', where No is optional -->\r\n\t<rule id=\"No{0}\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n   \t\t <item repeat=\"0-1\">\r\n\t\t\t\t<ruleref uri=\"{1}#noPhrases\"/>\r\n\t\t </item>\r\n\t\t <ruleref uri=\"#{0}\"/>\r\n\t\t<tag>$=$$;</tag>\r\n\t</rule>\r\n\r\n\t<rule id=\"{0}\" scope=\"public\">\r\n\t\t<tag>$.RecoEvent={{}};</tag>\r\n\t\t<tag>$.Extension={{}};</tag>\r\n\t\t<tag>$.ResultType={{}};</tag>\r\n\t\t<tag>$.DepartmentName={{}};</tag>\r\n\t\t<tag>$.MappedKey={{}};</tag>\r\n\t\t<tag>$.CustomMenuTarget={{}};</tag>\r\n\t\t<tag>$.PromptFileName={{}};</tag>\r\n\t\t<one-of>";

			// Token: 0x04000239 RID: 569
			internal const string CustomizedMenuGrammarElementTemplate = "\t\t\t <item>{0}\r\n\t\t\t\t <tag>\r\n\t\t\t\t\t $.RecoEvent._value=\"recoNameOrDepartment\";\r\n\t\t\t\t\t $.Extension._value=\"{1}\";\r\n\t\t\t\t\t $.ResultType._value=\"Department\";\r\n\t\t\t\t\t $.DepartmentName._value=\"{0}\";\r\n\t\t\t\t\t $.MappedKey._value=\"{4}\";\r\n\t\t\t\t\t $.CustomMenuTarget._value = \"{2}\"\r\n\t\t\t\t\t $.PromptFileName._value = \"{3}\"\r\n\t\t\t\t </tag>\r\n\t\t\t </item>";

			// Token: 0x0400023A RID: 570
			internal const string CustomizedMenuGrammarTrailer = "\t\t</one-of>\r\n\t\t<!-- the following will add an option politeending to the recognition -->\r\n\t   \t<item repeat=\"0-1\">\r\n\t\t\t<ruleref uri=\"{0}#politeEndPhrases\"/>\r\n\t\t</item>\r\n\t</rule>\r\n</grammar>";

			// Token: 0x0400023B RID: 571
			internal const string ManifestFileGrammarNode = "<resource src=\"{0}\"/>";

			// Token: 0x0400023C RID: 572
			internal const string ManifestFileGrammarNodeName = "resource";

			// Token: 0x0400023D RID: 573
			internal const string ManifestFileGrammarNodeSrcAttributeName = "src";

			// Token: 0x0400023E RID: 574
			internal const string ManifestFileRegKey = "SOFTWARE\\Microsoft\\Microsoft Speech Server\\2.0\\Applications\\ExUM";

			// Token: 0x0400023F RID: 575
			internal const string ManifestFileRegValue = "PreloadedResourceManifest";

			// Token: 0x04000240 RID: 576
			internal const string ManifestFileName = "manifest.xml";

			// Token: 0x04000241 RID: 577
			internal const string File = "file:///";

			// Token: 0x04000242 RID: 578
			internal const string ManifestFileXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><manifest version=\"2.0\">{0}</manifest>";

			// Token: 0x04000243 RID: 579
			internal const int ManifestFileReloadServiceCommand = 131;

			// Token: 0x04000244 RID: 580
			internal const int SesRecycleServiceCommand = 133;

			// Token: 0x04000245 RID: 581
			internal const int ReloadRegSettingsServiceCommand = 132;

			// Token: 0x04000246 RID: 582
			internal const string AssemblyBasedRegValue = "AssemblyBased";

			// Token: 0x04000247 RID: 583
			internal const string DnisRegValue = "DNIS";

			// Token: 0x04000248 RID: 584
			internal const string EnabledRegValue = "Enabled";

			// Token: 0x04000249 RID: 585
			internal const string PrecedenceRegValue = "Precedence";

			// Token: 0x0400024A RID: 586
			internal const string UriRegValue = "URI";

			// Token: 0x0400024B RID: 587
			internal const string TypeRegValue = "Type";

			// Token: 0x0400024C RID: 588
			internal const string NotifMsgQueueRegValue = "NotificationMessageQueue";

			// Token: 0x0400024D RID: 589
			internal const string UseSRTPRegValue = "UseSecureRTP";

			// Token: 0x0400024E RID: 590
			internal const string ManifestFileContentsRegValue = "PreloadedResourceManifestXml";

			// Token: 0x0400024F RID: 591
			internal const string DefaultUri = "http://localhost";
		}

		// Token: 0x0200004A RID: 74
		internal abstract class ServiceNames
		{
			// Token: 0x04000250 RID: 592
			public const string SearchIndexerServiceShortName = "MSExchangeSearch";
		}

		// Token: 0x0200004B RID: 75
		internal abstract class UMReporting
		{
			// Token: 0x04000251 RID: 593
			public const string AggregatedDataXmlNamespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData";

			// Token: 0x0200004C RID: 76
			internal abstract class CallType
			{
				// Token: 0x04000252 RID: 594
				public const string None = "None";

				// Token: 0x04000253 RID: 595
				public const string CAVoiceMessage = "CallAnsweringVoiceMessage";

				// Token: 0x04000254 RID: 596
				public const string CAMissedCall = "CallAnsweringMissedCall";

				// Token: 0x04000255 RID: 597
				public const string VirtualNumberCall = "VirtualNumberCall";

				// Token: 0x04000256 RID: 598
				public const string AutoAttendant = "AutoAttendant";

				// Token: 0x04000257 RID: 599
				public const string SubscriberAccess = "SubscriberAccess";

				// Token: 0x04000258 RID: 600
				public const string Fax = "Fax";

				// Token: 0x04000259 RID: 601
				public const string PlayOnPhone = "PlayOnPhone";

				// Token: 0x0400025A RID: 602
				public const string FindMe = "FindMe";

				// Token: 0x0400025B RID: 603
				public const string UnAuthenticatedPilotNumber = "UnAuthenticatedPilotNumber";

				// Token: 0x0400025C RID: 604
				public const string PromptProvisioning = "PromptProvisioning";

				// Token: 0x0400025D RID: 605
				public const string PlayOnPhonePAAGreeting = "PlayOnPhonePAAGreeting";

				// Token: 0x0400025E RID: 606
				public const string Diagnostics = "Diagnostics";
			}

			// Token: 0x0200004D RID: 77
			internal abstract class ReasonForCall
			{
				// Token: 0x0400025F RID: 607
				public const string None = "None";

				// Token: 0x04000260 RID: 608
				public const string Direct = "Direct";

				// Token: 0x04000261 RID: 609
				public const string DivertNoAnswer = "DivertNoAnswer";

				// Token: 0x04000262 RID: 610
				public const string DivertBusy = "DivertBusy";

				// Token: 0x04000263 RID: 611
				public const string DivertForward = "DivertForward";

				// Token: 0x04000264 RID: 612
				public const string Outbound = "Outbound";
			}

			// Token: 0x0200004E RID: 78
			internal abstract class DropCallReason
			{
				// Token: 0x04000265 RID: 613
				public const string None = "None";

				// Token: 0x04000266 RID: 614
				public const string UserError = "UserError";

				// Token: 0x04000267 RID: 615
				public const string SystemError = "SystemError";

				// Token: 0x04000268 RID: 616
				public const string GracefulHangup = "GracefulHangup";

				// Token: 0x04000269 RID: 617
				public const string OutboundFailedCall = "OutboundFailedCall";
			}

			// Token: 0x0200004F RID: 79
			internal abstract class OfferResult
			{
				// Token: 0x0400026A RID: 618
				public const string None = "None";

				// Token: 0x0400026B RID: 619
				public const string Answer = "Answer";

				// Token: 0x0400026C RID: 620
				public const string Reject = "Reject";

				// Token: 0x0400026D RID: 621
				public const string Redirect = "Redirect";
			}
		}
	}
}
