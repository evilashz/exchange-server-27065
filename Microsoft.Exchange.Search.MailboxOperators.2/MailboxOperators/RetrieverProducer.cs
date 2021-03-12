using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Metadata;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.EventLog;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200001A RID: 26
	internal class RetrieverProducer : XSOProducerBase<RetrieverOperator>, IAttachmentRetrieverProducer
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00006A61 File Offset: 0x00004C61
		public RetrieverProducer(RetrieverOperator retrieverOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(retrieverOperator, inputType, context, ExTraceGlobals.RetrieverOperatorTracer)
		{
			this.contentForLanguageDetection = new StringBuilder(base.Config.LanguageDetectionMaximumCharacters);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006A87 File Offset: 0x00004C87
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00006A8F File Offset: 0x00004C8F
		public IUpdateableRecord Holder { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00006A98 File Offset: 0x00004C98
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public int ErrorCodeIndex { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006AA9 File Offset: 0x00004CA9
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00006AB1 File Offset: 0x00004CB1
		public int ShouldBranchToErrorIndex { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006ABA File Offset: 0x00004CBA
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00006AC2 File Offset: 0x00004CC2
		public int LastAttemptTimeIndex { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00006ACB File Offset: 0x00004CCB
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00006AD3 File Offset: 0x00004CD3
		public int ErrorMessageIndex { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00006ADC File Offset: 0x00004CDC
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public int DocumentIdIndex { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006AED File Offset: 0x00004CED
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00006AF5 File Offset: 0x00004CF5
		public int AttachmentsIndex { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006AFE File Offset: 0x00004CFE
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006B06 File Offset: 0x00004D06
		public int AttachmentFileNamesIndex { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006B0F File Offset: 0x00004D0F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006B17 File Offset: 0x00004D17
		internal int CompositeItemIdIndex { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006B20 File Offset: 0x00004D20
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006B28 File Offset: 0x00004D28
		internal int IsLocalMdbIndex { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006B31 File Offset: 0x00004D31
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006B39 File Offset: 0x00004D39
		internal int IsMoveDestinationIndex { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006B42 File Offset: 0x00004D42
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006B4A File Offset: 0x00004D4A
		internal int PathIndex { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006B53 File Offset: 0x00004D53
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006B5B File Offset: 0x00004D5B
		internal int AttemptCountIndex { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006B64 File Offset: 0x00004D64
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006B6C File Offset: 0x00004D6C
		internal int InstanceNameIndex { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006B75 File Offset: 0x00004D75
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006B7D File Offset: 0x00004D7D
		internal int IndexSystemNameIndex { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006B86 File Offset: 0x00004D86
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006B8E File Offset: 0x00004D8E
		internal int CorrelationIdIndex { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006B97 File Offset: 0x00004D97
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006B9F File Offset: 0x00004D9F
		internal int TenantIdIndex { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006BA8 File Offset: 0x00004DA8
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006BB0 File Offset: 0x00004DB0
		internal int DetectedLanguageIndex { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006BB9 File Offset: 0x00004DB9
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006BC1 File Offset: 0x00004DC1
		internal int TempBodyIndex { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006BCA File Offset: 0x00004DCA
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006BD2 File Offset: 0x00004DD2
		internal int MailboxLanguageDetectionTextIndex { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006BDB File Offset: 0x00004DDB
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006BE3 File Offset: 0x00004DE3
		internal int AnnotationTokenIndex { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006BEC File Offset: 0x00004DEC
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006BF4 File Offset: 0x00004DF4
		internal int BodyIndex { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006BFD File Offset: 0x00004DFD
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006C05 File Offset: 0x00004E05
		internal int SubjectIndex { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006C0E File Offset: 0x00004E0E
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00006C16 File Offset: 0x00004E16
		internal int MeetingLocationIndex { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006C1F File Offset: 0x00004E1F
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006C27 File Offset: 0x00004E27
		internal int IsReadIndex { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006C30 File Offset: 0x00004E30
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00006C38 File Offset: 0x00004E38
		internal int ToIndex { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006C41 File Offset: 0x00004E41
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00006C49 File Offset: 0x00004E49
		internal int CcIndex { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006C52 File Offset: 0x00004E52
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00006C5A File Offset: 0x00004E5A
		internal int BccIndex { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006C63 File Offset: 0x00004E63
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00006C6B File Offset: 0x00004E6B
		internal int ToGroupExpansionRecipientsIndex { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006C74 File Offset: 0x00004E74
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00006C7C File Offset: 0x00004E7C
		internal int CcGroupExpansionRecipientsIndex { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006C85 File Offset: 0x00004E85
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00006C8D File Offset: 0x00004E8D
		internal int BccGroupExpansionRecipientsIndex { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006C96 File Offset: 0x00004E96
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006C9E File Offset: 0x00004E9E
		internal int GroupExpansionRecipientsIndex { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006CA7 File Offset: 0x00004EA7
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00006CAF File Offset: 0x00004EAF
		internal int MessageRecipientsIndex { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006CB8 File Offset: 0x00004EB8
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006CC0 File Offset: 0x00004EC0
		internal int SharingInfoIndex { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006CC9 File Offset: 0x00004EC9
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00006CD1 File Offset: 0x00004ED1
		internal int FolderIdIndex { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006CDA File Offset: 0x00004EDA
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006CE2 File Offset: 0x00004EE2
		internal int ReceivedIndex { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006CEB File Offset: 0x00004EEB
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006CF3 File Offset: 0x00004EF3
		internal int SentIndex { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006CFC File Offset: 0x00004EFC
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006D04 File Offset: 0x00004F04
		internal int FromIndex { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006D0D File Offset: 0x00004F0D
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00006D15 File Offset: 0x00004F15
		internal int ReceivedByIndex { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006D1E File Offset: 0x00004F1E
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006D26 File Offset: 0x00004F26
		internal int ReceivedRepresentingIndex { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006D2F File Offset: 0x00004F2F
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006D37 File Offset: 0x00004F37
		internal int ConversationTopicIndex { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006D40 File Offset: 0x00004F40
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006D48 File Offset: 0x00004F48
		internal int AccountIndex { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006D51 File Offset: 0x00004F51
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006D59 File Offset: 0x00004F59
		internal int DisplayNameIndex { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006D62 File Offset: 0x00004F62
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006D6A File Offset: 0x00004F6A
		internal int FirstNameIndex { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006D73 File Offset: 0x00004F73
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006D7B File Offset: 0x00004F7B
		internal int LastNameIndex { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006D84 File Offset: 0x00004F84
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006D8C File Offset: 0x00004F8C
		internal int FileAsIndex { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006D95 File Offset: 0x00004F95
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00006D9D File Offset: 0x00004F9D
		internal int EmailDisplayNameIndex { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00006DA6 File Offset: 0x00004FA6
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00006DAE File Offset: 0x00004FAE
		internal int EmailAddressIndex { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006DB7 File Offset: 0x00004FB7
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00006DBF File Offset: 0x00004FBF
		internal int EmailOriginalDisplayNameIndex { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006DC8 File Offset: 0x00004FC8
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00006DD0 File Offset: 0x00004FD0
		internal int IMAddressIndex { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006DD9 File Offset: 0x00004FD9
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00006DE1 File Offset: 0x00004FE1
		internal int HomeAddressIndex { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006DEA File Offset: 0x00004FEA
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00006DF2 File Offset: 0x00004FF2
		internal int OtherAddressIndex { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006DFB File Offset: 0x00004FFB
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00006E03 File Offset: 0x00005003
		internal int BusinessAddressIndex { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00006E0C File Offset: 0x0000500C
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00006E14 File Offset: 0x00005014
		internal int MiddleNameIndex { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006E1D File Offset: 0x0000501D
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00006E25 File Offset: 0x00005025
		internal int NicknameIndex { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00006E2E File Offset: 0x0000502E
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00006E36 File Offset: 0x00005036
		internal int YomiCompanyNameIndex { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00006E3F File Offset: 0x0000503F
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00006E47 File Offset: 0x00005047
		internal int YomiFirstNameIndex { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00006E50 File Offset: 0x00005050
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00006E58 File Offset: 0x00005058
		internal int YomiLastNameIndex { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006E61 File Offset: 0x00005061
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00006E69 File Offset: 0x00005069
		internal int BusinessPhoneNumberIndex { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006E72 File Offset: 0x00005072
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00006E7A File Offset: 0x0000507A
		internal int CarPhoneNumberIndex { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00006E83 File Offset: 0x00005083
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00006E8B File Offset: 0x0000508B
		internal int MobilePhoneNumberIndex { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006E94 File Offset: 0x00005094
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00006E9C File Offset: 0x0000509C
		internal int BusinessMainPhoneIndex { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006EA5 File Offset: 0x000050A5
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00006EAD File Offset: 0x000050AD
		internal int CompanyNameIndex { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006EB6 File Offset: 0x000050B6
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00006EBE File Offset: 0x000050BE
		internal int OfficeLocationIndex { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006EC7 File Offset: 0x000050C7
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00006ECF File Offset: 0x000050CF
		internal int DepartmentNameIndex { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006ED8 File Offset: 0x000050D8
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00006EE0 File Offset: 0x000050E0
		internal int DisplayNamePrefixIndex { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006EE9 File Offset: 0x000050E9
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00006EF1 File Offset: 0x000050F1
		internal int HomePhoneIndex { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00006EFA File Offset: 0x000050FA
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00006F02 File Offset: 0x00005102
		internal int PrimaryTelephoneNumberIndex { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006F0B File Offset: 0x0000510B
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006F13 File Offset: 0x00005113
		internal int ContactTitleIndex { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006F1C File Offset: 0x0000511C
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006F24 File Offset: 0x00005124
		internal int TaskTitleIndex { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006F2D File Offset: 0x0000512D
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00006F35 File Offset: 0x00005135
		internal int UMAudioNotesIndex { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00006F3E File Offset: 0x0000513E
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00006F46 File Offset: 0x00005146
		internal int ImportanceIndex { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00006F4F File Offset: 0x0000514F
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00006F57 File Offset: 0x00005157
		internal int SizeIndex { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006F60 File Offset: 0x00005160
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00006F68 File Offset: 0x00005168
		internal int ItemClassIndex { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006F71 File Offset: 0x00005171
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00006F79 File Offset: 0x00005179
		internal int CategoriesIndex { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006F82 File Offset: 0x00005182
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00006F8A File Offset: 0x0000518A
		internal int FileTypeIndex { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00006F93 File Offset: 0x00005193
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00006F9B File Offset: 0x0000519B
		internal int ConversationIdIndex { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006FA4 File Offset: 0x000051A4
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00006FAC File Offset: 0x000051AC
		internal int VersionIndex { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00006FB5 File Offset: 0x000051B5
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00006FBD File Offset: 0x000051BD
		internal int HasIrmIndex { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006FC6 File Offset: 0x000051C6
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00006FCE File Offset: 0x000051CE
		internal int IconIndexIndex { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006FD7 File Offset: 0x000051D7
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006FDF File Offset: 0x000051DF
		internal int HasAttachmentIndex { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006FE8 File Offset: 0x000051E8
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00006FF0 File Offset: 0x000051F0
		internal int MidIndex { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006FF9 File Offset: 0x000051F9
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00007001 File Offset: 0x00005201
		internal int FlagStatusIndex { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000700A File Offset: 0x0000520A
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00007012 File Offset: 0x00005212
		internal int BodyPreviewIndex { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000701B File Offset: 0x0000521B
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00007023 File Offset: 0x00005223
		internal int ConversationGuidIndex { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000702C File Offset: 0x0000522C
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00007034 File Offset: 0x00005234
		internal int RefinableReceivedIndex { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000703D File Offset: 0x0000523D
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00007045 File Offset: 0x00005245
		internal int RefinableFromIndex { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000704E File Offset: 0x0000524E
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00007056 File Offset: 0x00005256
		internal int WorkingSetIdIndex { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000705F File Offset: 0x0000525F
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007067 File Offset: 0x00005267
		internal int WorkingSetSourceIndex { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007070 File Offset: 0x00005270
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00007078 File Offset: 0x00005278
		internal int WorkingSetSourcePartitionIndex { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007081 File Offset: 0x00005281
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007089 File Offset: 0x00005289
		internal int WorkingSetFlagsIndex { get; private set; }

		// Token: 0x060001A9 RID: 425 RVA: 0x00007094 File Offset: 0x00005294
		public override void InternalProcessRecord(IRecord record)
		{
			LapTimer lapTimer = new LapTimer();
			if (this.tempBodyFieldReference == null)
			{
				this.tempBodyFieldReference = this.Holder[this.TempBodyIndex];
			}
			else if (this.Holder[this.TempBodyIndex] == this.Holder[this.BodyIndex])
			{
				this.Holder[this.TempBodyIndex] = this.tempBodyFieldReference;
			}
			foreach (IField field in this.Holder)
			{
				IUpdateableField updateableField = (IUpdateableField)field;
				if (updateableField.Value is int)
				{
					updateableField.Value = 0;
				}
				else if (updateableField.Value is Guid)
				{
					updateableField.Value = Guid.Empty;
				}
				else
				{
					updateableField.Value = null;
				}
			}
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			((IUpdateableStringField)this.Holder[this.BodyIndex]).StringValue = string.Empty;
			RecordCode recordMetaType = this.Holder.Metadata.RecordMetaType;
			Guid primitiveGuidValue = ((IGuidField)this.Holder[this.CorrelationIdIndex]).PrimitiveGuidValue;
			base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Begin processing record with CorrelationId: {0}", primitiveGuidValue);
			EvaluationErrors evaluationErrors = (EvaluationErrors)(((IInt32Field)this.Holder[this.ErrorCodeIndex]).NullableInt32Value ?? 0);
			int attemptCount = ((IInt32Field)this.Holder[this.AttemptCountIndex]).NullableInt32Value ?? 0;
			long int64Value = ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value;
			Guid primitiveGuidValue2 = ((IGuidField)this.Holder[this.TenantIdIndex]).PrimitiveGuidValue;
			MdbItemIdentity mdbItemIdentity = MdbItemIdentity.Parse(((IUpdateableStringField)this.Holder[this.CompositeItemIdIndex]).StringValue);
			bool valueOrDefault = ((IBooleanField)this.Holder[this.IsMoveDestinationIndex]).NullableBooleanValue.GetValueOrDefault(false);
			bool valueOrDefault2 = ((IBooleanField)this.Holder[this.IsLocalMdbIndex]).NullableBooleanValue.GetValueOrDefault(false);
			string stringValue = ((IStringField)this.Holder[this.IndexSystemNameIndex]).StringValue;
			int int32Value = ((IInt32Field)this.Holder[this.VersionIndex]).Int32Value;
			ItemDepot.Instance.SaveAssociatedRecordInformation(base.FlowIdentifier, primitiveGuidValue, int64Value, primitiveGuidValue2, mdbItemIdentity, (int)evaluationErrors, attemptCount, stringValue);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(4077268285U);
			if (evaluationErrors != EvaluationErrors.None && evaluationErrors != EvaluationErrors.AnnotationTokenError)
			{
				throw new InvalidOperationException(EvaluationErrorsHelper.GetErrorDescription(evaluationErrors));
			}
			Guid mdbGuid = mdbItemIdentity.GetMdbGuid(MdbItemIdentity.Location.ExchangeMdb);
			string stringValue2 = ((IUpdateableStringField)this.Holder[this.InstanceNameIndex]).StringValue;
			this.SetInstance(mdbGuid, stringValue2 ?? mdbGuid.ToString());
			lapTimer.GetLapTime();
			if (recordMetaType != 2)
			{
				bool sessionShouldBeOpenedForRMS = false;
				int arg = 0;
				EvaluationErrors evaluationErrors2;
				Exception sessionException;
				for (;;)
				{
					bool flag = false;
					bool discard = true;
					StoreSession storeSession = base.GetStoreSession(mdbItemIdentity, valueOrDefault, valueOrDefault2, sessionShouldBeOpenedForRMS, lapTimer, out evaluationErrors2, out sessionException);
					if (evaluationErrors2 != EvaluationErrors.None)
					{
						break;
					}
					try
					{
						Item item = null;
						IRetrieverItem retrieverItem = null;
						try
						{
							MdbItemIdentity mdbItemIdentity2;
							item = base.ItemBind(storeSession, mdbItemIdentity, FastDocumentSchema.Instance.DefaultProperties, lapTimer, out mdbItemIdentity2);
							if (mdbItemIdentity2 != null)
							{
								mdbItemIdentity = mdbItemIdentity2;
							}
						}
						catch (ConnectionFailedTransientException)
						{
							if (arg++ == base.Config.MaxRetryCount)
							{
								throw;
							}
							base.Tracer.TraceDebug<int, int, int>((long)base.TracingContext, "Retry opening message of Document Id {0}. ({1}/{2})", mdbItemIdentity.DocumentId, arg, base.Config.MaxRetryCount);
							flag = true;
						}
						string text = null;
						ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2626039101U, ref text);
						if (text != null && string.Compare(item.Subject, text, true) == 0 && !this.fireTransientFaultInjection)
						{
							this.fireTransientFaultInjection = true;
							throw new ComponentFailedTransientException();
						}
						List<Item> list = null;
						if (item != null)
						{
							discard = false;
							try
							{
								bool flag2 = false;
								bool flag3 = false;
								MessageItem messageItem = item as MessageItem;
								if (messageItem != null)
								{
									if (base.Config.IRMMessageProcessingEnabled && (messageItem.IsRestricted || ObjectClass.IsSmime(messageItem.ClassName)))
									{
										if (!StoreSessionManager.IsSessionUsableForRMS(storeSession) && arg++ < base.Config.MaxRetryCount)
										{
											sessionShouldBeOpenedForRMS = true;
											flag = true;
											goto IL_9C0;
										}
										bool flag4 = false;
										for (int i = 0; i < base.Config.MaxDecodeIterations; i++)
										{
											if (ObjectClass.IsSmime(messageItem.ClassName))
											{
												Item item2;
												switch (RightsManagement.ProcessSMIMEMessage(storeSession, item, out item2))
												{
												case RightsManagementProcessingResult.IsSMIME:
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementAttempted);
													if (item2 != null)
													{
														if (list == null)
														{
															list = new List<Item>(1);
														}
														list.Add(item);
														item = item2;
														flag4 = true;
													}
													break;
												case RightsManagementProcessingResult.FailedTransient:
													this.MarkupRecordForRightsManagmentFailure(null, false);
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementPartiallyProcessed);
													break;
												case RightsManagementProcessingResult.FailedPermanent:
													this.MarkupRecordForRightsManagmentFailure(null, true);
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementPartiallyProcessed);
													break;
												case RightsManagementProcessingResult.Skipped:
													this.MarkupRecordForRightsManagmentFailure(null, true);
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementSkipped);
													break;
												}
											}
											if (messageItem.IsRestricted)
											{
												switch (RightsManagement.ProcessRightsManagedMessage(storeSession, item))
												{
												case RightsManagementProcessingResult.Success:
												{
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementAttempted);
													RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)item;
													flag2 = true;
													if (rightsManagedMessageItem.ProtectedAttachmentCollection.GetAllHandles().Count > 0)
													{
														flag3 = true;
													}
													flag4 = true;
													break;
												}
												case RightsManagementProcessingResult.FailedTransient:
													this.MarkupRecordForRightsManagmentFailure(messageItem, false);
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementPartiallyProcessed);
													flag2 = true;
													break;
												case RightsManagementProcessingResult.FailedPermanent:
													this.MarkupRecordForRightsManagmentFailure(messageItem, true);
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementPartiallyProcessed);
													flag2 = true;
													break;
												case RightsManagementProcessingResult.Skipped:
													this.MarkupRecordForRightsManagmentFailure(messageItem, true);
													base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverItemsWithRightsManagementSkipped);
													flag2 = true;
													break;
												}
											}
											if (!flag4)
											{
												break;
											}
										}
									}
									this.SetPropertiesToHolder(item, FastDocumentSchema.Instance.MessageProperties, int32Value);
								}
								if (item is CalendarItem)
								{
									this.SetPropertiesToHolder(item, FastDocumentSchema.Instance.CalendarProperties, int32Value);
								}
								if (item is MeetingMessage)
								{
									this.SetPropertiesToHolder(item, FastDocumentSchema.Instance.MeetingProperties, int32Value);
								}
								if (item is Contact || item is DistributionList)
								{
									this.SetPropertiesToHolder(item, FastDocumentSchema.Instance.ContactProperties, int32Value);
								}
								if (item is Task)
								{
									this.SetPropertiesToHolder(item, FastDocumentSchema.Instance.TaskProperties, int32Value);
								}
								this.SetPropertiesToHolder(item, FastDocumentSchema.Instance.ItemProperties, int32Value);
								((IUpdateableBooleanField)this.Holder[this.HasIrmIndex]).BooleanValue = flag2;
								if (string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.DetectedLanguageIndex]).StringValue))
								{
									((IUpdateableStringField)this.Holder[this.DetectedLanguageIndex]).StringValue = "en";
									((IUpdateableStringField)this.Holder[this.MailboxLanguageDetectionTextIndex]).StringValue = this.GetContentForLanguageDetection(item);
								}
								if (!flag2 && item.AttachmentCollection.GetAllHandles().Count > 0)
								{
									flag3 = true;
								}
								if (flag3)
								{
									retrieverItem = XSOItemHandlerAdaptor.WrapXSOItem(item);
									item = null;
									if (this.attachmentRetrieverHandler == null)
									{
										this.attachmentRetrieverHandler = new AttachmentRetrieverHandler(this, base.Config, base.Tracer, base.TracingContext, false);
										this.attachmentRetrieverHandler.ErrorMessageIndex = this.ErrorMessageIndex;
									}
									string currentPath = int64Value.ToString();
									bool itemIsAnnotated = ((IBlobField)this.Holder[this.AnnotationTokenIndex]).BlobValue != null;
									try
									{
										this.attachmentRetrieverHandler.ProcessItemForAttachments(primitiveGuidValue, mdbItemIdentity.GetMdbGuid(MdbItemIdentity.Location.ExchangeMdb), mdbItemIdentity.MailboxGuid, retrieverItem, itemIsAnnotated, currentPath);
										base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalAttachmentBytes, this.attachmentRetrieverHandler.TotalBytesProcessed);
										base.IncrementPerfcounterBy(OperatorPerformanceCounter.TotalAttachmentsRead, this.attachmentRetrieverHandler.TotalAttachmentsRead);
									}
									catch (OrgIdNotFoundException)
									{
										if (arg++ < base.Config.MaxRetryCount)
										{
											sessionShouldBeOpenedForRMS = true;
											ItemDepot.Instance.DisposeItems(base.FlowIdentifier);
											flag = true;
										}
										else
										{
											this.MarkupRecordForRightsManagmentFailure(null, true);
										}
									}
									catch (RightsManagementException)
									{
										this.MarkupRecordForRightsManagmentFailure(null, true);
									}
									catch (RightsManagementPermanentException)
									{
										this.MarkupRecordForRightsManagmentFailure(null, true);
									}
									catch (RightsManagementTransientException)
									{
										this.MarkupRecordForRightsManagmentFailure(null, false);
									}
								}
							}
							finally
							{
								if (item != null)
								{
									item.Dispose();
									item = null;
								}
								if (list != null)
								{
									foreach (Item item3 in list)
									{
										item3.Dispose();
									}
								}
								if (retrieverItem != null)
								{
									retrieverItem.Dispose();
									retrieverItem = null;
								}
								this.contentForLanguageDetection.Clear();
							}
						}
					}
					catch (NoSupportException ex)
					{
						base.Tracer.TraceDebug<NoSupportException>((long)base.TracingContext, "{0}", ex);
						((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = 14;
						this.AddExceptionToErrorMessage(ex);
						base.EventLog.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_RopNotSupported, mdbItemIdentity.MailboxGuid.ToString(), new object[]
						{
							mdbItemIdentity.GetMdbGuid(MdbItemIdentity.Location.ExchangeMdb),
							mdbItemIdentity.MailboxGuid,
							ex
						});
						if (!ExEnvironment.IsTestProcess)
						{
							EventNotificationItem.PublishPeriodic(ExchangeComponent.Search.Name, "SearchRetrieverProducerRopNotSupported", string.Empty, Strings.RetrieverProducerRopNotSupported(base.InstanceName, mdbItemIdentity.MailboxGuid.ToString(), ex.ToString()), "SearchRetrieverProducerRopNotSupported", TimeSpan.FromMinutes(10.0), ResultSeverityLevel.Error, false);
						}
					}
					finally
					{
						StoreSessionManager.ReturnStoreSessionToCache(ref storeSession, discard);
					}
					IL_9C0:
					if (!flag)
					{
						goto Block_12;
					}
				}
				this.MarkupRecordForSessionFailure(evaluationErrors2, sessionException, attemptCount);
				base.SetNextRecord(this.Holder);
				return;
				Block_12:;
			}
			else
			{
				base.Tracer.TraceDebug<int>((long)base.TracingContext, "Sending Delete Operation for Document Id {0}", mdbItemIdentity.DocumentId);
			}
			TimeSpan splitTime = lapTimer.GetSplitTime();
			base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time using the item (including bind):    {0} ms", splitTime.TotalMilliseconds);
			this.RecordPerformanceCounter((long)splitTime.TotalMilliseconds);
			((IUpdateableStringField)this.Holder[this.PathIndex]).StringValue = ((IUpdateableStringField)this.Holder[this.CompositeItemIdIndex]).StringValue;
			base.SetNextRecord(this.Holder);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007BF4 File Offset: 0x00005DF4
		public void RecordPerformanceCounter(long elapsedTime)
		{
			if (elapsedTime < 50L)
			{
				base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedIn0to50Milliseconds);
				return;
			}
			if (elapsedTime < 100L)
			{
				base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedIn50to100Milliseconds);
				return;
			}
			if (elapsedTime <= 2000L)
			{
				base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedIn100to2000Milliseconds);
				return;
			}
			base.IncrementPerfcounter(OperatorPerformanceCounter.RetrieverNumberOfItemsProcessedInGreaterThan2000Milliseconds);
			if (elapsedTime > (long)base.Config.EventLogThreshold)
			{
				base.EventLog.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_ItemProcessingTimeExceeded, string.Empty, new object[]
				{
					base.InstanceName,
					base.Config.EventLogThreshold,
					elapsedTime
				});
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00007C87 File Offset: 0x00005E87
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RetrieverProducer>(this);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007C8F File Offset: 0x00005E8F
		internal void TestSetInstance(Guid instanceGuid, string instanceName)
		{
			this.SetInstance(instanceGuid, instanceName);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007C9C File Offset: 0x00005E9C
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, this.recordImpl);
			this.CorrelationIdIndex = base.InputType.RecordType.Position(base.Operator.CorrelationId);
			this.TenantIdIndex = base.InputType.RecordType.Position(base.Operator.TenantId);
			this.CompositeItemIdIndex = base.InputType.RecordType.Position(base.Operator.CompositeItemId);
			this.IsMoveDestinationIndex = base.InputType.RecordType.Position(base.Operator.IsMoveDestination);
			this.IsLocalMdbIndex = base.InputType.RecordType.Position(base.Operator.IsLocalMdb);
			this.PathIndex = base.InputType.RecordType.Position(base.Operator.Path);
			this.ErrorCodeIndex = base.InputType.RecordType.Position(base.Operator.ErrorCode);
			this.ErrorMessageIndex = base.InputType.RecordType.Position(base.Operator.ErrorMessage);
			this.ShouldBranchToErrorIndex = base.InputType.RecordType.Position(base.Operator.ShouldBranchToError);
			this.AttemptCountIndex = base.InputType.RecordType.Position(base.Operator.AttemptCount);
			this.InstanceNameIndex = base.InputType.RecordType.Position(base.Operator.InstanceName);
			this.IndexSystemNameIndex = base.InputType.RecordType.Position(base.Operator.IndexSystemName);
			this.DocumentIdIndex = base.InputType.RecordType.Position(base.Operator.DocumentId);
			this.DetectedLanguageIndex = base.InputType.RecordType.Position(base.Operator.DetectedLanguage);
			this.TempBodyIndex = base.InputType.RecordType.Position(base.Operator.TempBody);
			this.MailboxLanguageDetectionTextIndex = base.InputType.RecordType.Position(base.Operator.MailboxLanguageDetectionText);
			this.AnnotationTokenIndex = base.InputType.RecordType.Position(base.Operator.AnnotationToken);
			this.BodyIndex = base.InputType.RecordType.Position(base.Operator.Body);
			this.SubjectIndex = base.InputType.RecordType.Position(base.Operator.Subject);
			this.MeetingLocationIndex = base.InputType.RecordType.Position(base.Operator.MeetingLocation);
			this.ToIndex = base.InputType.RecordType.Position(base.Operator.To);
			this.CcIndex = base.InputType.RecordType.Position(base.Operator.Cc);
			this.BccIndex = base.InputType.RecordType.Position(base.Operator.Bcc);
			this.ToGroupExpansionRecipientsIndex = base.InputType.RecordType.Position(base.Operator.ToGroupExpansionRecipients);
			this.CcGroupExpansionRecipientsIndex = base.InputType.RecordType.Position(base.Operator.CcGroupExpansionRecipients);
			this.BccGroupExpansionRecipientsIndex = base.InputType.RecordType.Position(base.Operator.BccGroupExpansionRecipients);
			this.GroupExpansionRecipientsIndex = base.InputType.RecordType.Position(base.Operator.GroupExpansionRecipients);
			this.MessageRecipientsIndex = base.InputType.RecordType.Position(base.Operator.MessageRecipients);
			this.SharingInfoIndex = base.InputType.RecordType.Position(base.Operator.SharingInfoField);
			this.FolderIdIndex = base.InputType.RecordType.Position(base.Operator.FolderId);
			this.ReceivedIndex = base.InputType.RecordType.Position(base.Operator.Received);
			this.SentIndex = base.InputType.RecordType.Position(base.Operator.Sent);
			this.FromIndex = base.InputType.RecordType.Position(base.Operator.From);
			this.ReceivedByIndex = base.InputType.RecordType.Position(base.Operator.ReceivedBy);
			this.ReceivedRepresentingIndex = base.InputType.RecordType.Position(base.Operator.ReceivedRepresenting);
			this.ConversationTopicIndex = base.InputType.RecordType.Position(base.Operator.ConversationTopic);
			this.AccountIndex = base.InputType.RecordType.Position(base.Operator.AccountField);
			this.DisplayNameIndex = base.InputType.RecordType.Position(base.Operator.DisplayNameField);
			this.FirstNameIndex = base.InputType.RecordType.Position(base.Operator.FirstNameField);
			this.LastNameIndex = base.InputType.RecordType.Position(base.Operator.LastNameField);
			this.FileAsIndex = base.InputType.RecordType.Position(base.Operator.FileAsField);
			this.EmailDisplayNameIndex = base.InputType.RecordType.Position(base.Operator.EmailDisplayNameField);
			this.EmailAddressIndex = base.InputType.RecordType.Position(base.Operator.EmailAddressField);
			this.EmailOriginalDisplayNameIndex = base.InputType.RecordType.Position(base.Operator.EmailOriginalDisplayNameField);
			this.IMAddressIndex = base.InputType.RecordType.Position(base.Operator.IMAddressField);
			this.HomeAddressIndex = base.InputType.RecordType.Position(base.Operator.HomeAddressField);
			this.OtherAddressIndex = base.InputType.RecordType.Position(base.Operator.OtherAddressField);
			this.BusinessAddressIndex = base.InputType.RecordType.Position(base.Operator.BusinessAddressField);
			this.MiddleNameIndex = base.InputType.RecordType.Position(base.Operator.MiddleNameField);
			this.NicknameIndex = base.InputType.RecordType.Position(base.Operator.NicknameField);
			this.YomiCompanyNameIndex = base.InputType.RecordType.Position(base.Operator.YomiCompanyNameField);
			this.YomiFirstNameIndex = base.InputType.RecordType.Position(base.Operator.YomiFirstNameField);
			this.YomiLastNameIndex = base.InputType.RecordType.Position(base.Operator.YomiLastNameField);
			this.BusinessPhoneNumberIndex = base.InputType.RecordType.Position(base.Operator.BusinessPhoneNumberField);
			this.CarPhoneNumberIndex = base.InputType.RecordType.Position(base.Operator.CarPhoneNumberField);
			this.MobilePhoneNumberIndex = base.InputType.RecordType.Position(base.Operator.MobilePhoneNumberField);
			this.BusinessMainPhoneIndex = base.InputType.RecordType.Position(base.Operator.BusinessMainPhoneField);
			this.CompanyNameIndex = base.InputType.RecordType.Position(base.Operator.CompanyNameField);
			this.OfficeLocationIndex = base.InputType.RecordType.Position(base.Operator.OfficeLocationField);
			this.DepartmentNameIndex = base.InputType.RecordType.Position(base.Operator.DepartmentNameField);
			this.DisplayNamePrefixIndex = base.InputType.RecordType.Position(base.Operator.DisplayNamePrefixField);
			this.HomePhoneIndex = base.InputType.RecordType.Position(base.Operator.HomePhoneField);
			this.PrimaryTelephoneNumberIndex = base.InputType.RecordType.Position(base.Operator.PrimaryTelephoneNumberField);
			this.ContactTitleIndex = base.InputType.RecordType.Position(base.Operator.ContactTitleField);
			this.TaskTitleIndex = base.InputType.RecordType.Position(base.Operator.TaskTitleField);
			this.UMAudioNotesIndex = base.InputType.RecordType.Position(base.Operator.UMAudioNotesField);
			this.ImportanceIndex = base.InputType.RecordType.Position(base.Operator.ImportanceField);
			this.SizeIndex = base.InputType.RecordType.Position(base.Operator.SizeField);
			this.ItemClassIndex = base.InputType.RecordType.Position(base.Operator.ItemClassField);
			this.CategoriesIndex = base.InputType.RecordType.Position(base.Operator.CategoriesField);
			this.LastAttemptTimeIndex = base.InputType.RecordType.Position(base.Operator.LastAttemptTime);
			this.AttachmentsIndex = base.InputType.RecordType.Position(base.Operator.AttachmentsField);
			this.AttachmentFileNamesIndex = base.InputType.RecordType.Position(base.Operator.AttachmentFileNamesField);
			this.FileTypeIndex = base.InputType.RecordType.Position(base.Operator.FileTypeField);
			this.ConversationIdIndex = base.InputType.RecordType.Position(base.Operator.ConversationIdField);
			this.VersionIndex = base.InputType.RecordType.Position(base.Operator.VersionField);
			this.IsReadIndex = base.InputType.RecordType.Position(base.Operator.IsReadField);
			this.HasIrmIndex = base.InputType.RecordType.Position(base.Operator.HasIrmField);
			this.IconIndexIndex = base.InputType.RecordType.Position(base.Operator.IconIndexField);
			this.HasAttachmentIndex = base.InputType.RecordType.Position(base.Operator.HasAttachmentField);
			this.MidIndex = base.InputType.RecordType.Position(base.Operator.MidField);
			this.FlagStatusIndex = base.InputType.RecordType.Position(base.Operator.FlagStatusField);
			this.BodyPreviewIndex = base.InputType.RecordType.Position(base.Operator.BodyPreviewField);
			this.ConversationGuidIndex = base.InputType.RecordType.Position(base.Operator.ConversationGuidField);
			this.RefinableReceivedIndex = base.InputType.RecordType.Position(base.Operator.RefinableReceivedField);
			this.RefinableFromIndex = base.InputType.RecordType.Position(base.Operator.RefinableFromField);
			this.WorkingSetIdIndex = base.InputType.RecordType.Position(base.Operator.WorkingSetIdField);
			this.WorkingSetSourceIndex = base.InputType.RecordType.Position(base.Operator.WorkingSetSourceField);
			this.WorkingSetSourcePartitionIndex = base.InputType.RecordType.Position(base.Operator.WorkingSetSourcePartitionField);
			this.WorkingSetFlagsIndex = base.InputType.RecordType.Position(base.Operator.WorkingSetFlagsField);
			this.participantFields = new int[]
			{
				this.FromIndex,
				this.ToIndex,
				this.CcIndex,
				this.BccIndex
			};
			this.contactFields = new int[]
			{
				this.DisplayNameIndex,
				this.FirstNameIndex,
				this.LastNameIndex,
				this.BodyIndex,
				this.MiddleNameIndex,
				this.NicknameIndex,
				this.ContactTitleIndex,
				this.DisplayNamePrefixIndex,
				this.CompanyNameIndex,
				this.DepartmentNameIndex,
				this.OfficeLocationIndex,
				this.HomeAddressIndex,
				this.OtherAddressIndex,
				this.BusinessAddressIndex
			};
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008951 File Offset: 0x00006B51
		protected override void ReleaseManagedResources()
		{
			ItemDepot.Instance.CleanupFlowInstance(base.FlowIdentifier);
			base.ReleaseManagedResources();
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000896C File Offset: 0x00006B6C
		private void SetPropertiesToHolder(Item item, List<FastPropertyDefinition> properties, int version)
		{
			foreach (FastPropertyDefinition fastPropertyDefinition in properties)
			{
				if (fastPropertyDefinition.FieldDefinition != null && fastPropertyDefinition.FieldDefinition.Version.FeedingVersion > version)
				{
					base.Tracer.TraceDebug<long, string>((long)base.TracingContext, "[{0}] [GET] {1} : Skipping Due to FeedingVersion", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name);
				}
				else if (!fastPropertyDefinition.ShouldProcess(item, this))
				{
					base.Tracer.TraceDebug<long, string>((long)base.TracingContext, "[{0}] [GET] {1} : ShouldProcess check returned False", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name);
				}
				else
				{
					object obj = null;
					try
					{
						obj = fastPropertyDefinition.GetValue(item);
					}
					catch (NotInBagPropertyErrorException ex)
					{
						base.Tracer.TraceDebug<long, string, string>((long)base.TracingContext, "[{0}] [GET] {1} : {2}", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name, ex.Message);
					}
					catch (PropertyErrorException ex2)
					{
						base.Tracer.TraceDebug<long, string, string>((long)base.TracingContext, "[{0}] [GET] {1} : {2}", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name, ex2.Message);
					}
					catch (Exception ex3)
					{
						base.Tracer.TraceDebug<long, string, string>((long)base.TracingContext, "[{0}] [GET] {1} : {2}", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name, ex3.Message);
						throw;
					}
					try
					{
						if (obj != null)
						{
							fastPropertyDefinition.SetValue(this, obj);
							if (!fastPropertyDefinition.Name.Equals(FastIndexSystemSchema.Body.Name))
							{
								base.Tracer.TraceDebug<long, string, object>((long)base.TracingContext, "[{0}] [SET] {1} : {2}", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name, obj);
							}
							else
							{
								base.Tracer.TraceDebug<long, string, string>((long)base.TracingContext, "[{0}] [SET] {1} : {2}", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name, "-Body Value Set-");
							}
						}
					}
					catch (Exception ex4)
					{
						base.Tracer.TraceDebug<long, string, string>((long)base.TracingContext, "[{0}] [SET] {1} : {2}", ((IUpdateableInt64Field)this.Holder[this.DocumentIdIndex]).Int64Value, fastPropertyDefinition.Name, ex4.Message);
						throw;
					}
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008C84 File Offset: 0x00006E84
		private void MarkupRecordForRightsManagmentFailure(MessageItem messageItem, bool isPermanent)
		{
			ManagedProperties.SetAsPartiallyProcessed(this.Holder);
			((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = (isPermanent ? EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.RightsManagementFailure) : 10);
			((IUpdateableDateTimeField)this.Holder[this.LastAttemptTimeIndex]).DateTimeValue = Util.NormalizeDateTime(DateTime.UtcNow);
			RightsManagedMessageItem rightsManagedMessageItem = messageItem as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.DecryptionStatus.Failed)
			{
				this.AddExceptionToErrorMessage(rightsManagedMessageItem.DecryptionStatus.Exception);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008D18 File Offset: 0x00006F18
		private void MarkupRecordForSessionFailure(EvaluationErrors errorcode, Exception sessionException, int attemptCount)
		{
			((IUpdateableBooleanField)this.Holder[this.ShouldBranchToErrorIndex]).BooleanValue = true;
			((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = (EvaluationErrorsHelper.ShouldMakePermanent(attemptCount, (int)errorcode) ? EvaluationErrorsHelper.MakePermanentError(errorcode) : ((int)errorcode));
			this.AddExceptionToErrorMessage(sessionException);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008D78 File Offset: 0x00006F78
		private void AddExceptionToErrorMessage(Exception exception)
		{
			IUpdateableStringField updateableStringField = (IUpdateableStringField)this.Holder[this.ErrorMessageIndex];
			string stringValue = string.IsNullOrEmpty(updateableStringField.StringValue) ? exception.ToString() : string.Format("{0} {1}", updateableStringField.StringValue, exception);
			updateableStringField.StringValue = stringValue;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008DCC File Offset: 0x00006FCC
		private string GetContentForLanguageDetection(Item item)
		{
			int languageDetectionMaximumCharacters = base.Config.LanguageDetectionMaximumCharacters;
			if (item is Contact)
			{
				if (!string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.YomiLastNameIndex]).StringValue) || !string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.YomiFirstNameIndex]).StringValue) || !string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.YomiCompanyNameIndex]).StringValue))
				{
					base.Tracer.TraceDebug((long)base.TracingContext, "Yomi name fields exist on this contact.");
					((IUpdateableStringField)this.Holder[this.DetectedLanguageIndex]).StringValue = "ja";
					return string.Empty;
				}
				foreach (int num in this.contactFields)
				{
					if (!string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[num]).StringValue))
					{
						this.contentForLanguageDetection.AppendFormat("{0} ", ((IUpdateableStringField)this.Holder[num]).StringValue);
						if (this.contentForLanguageDetection.Length > languageDetectionMaximumCharacters)
						{
							break;
						}
					}
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.TempBodyIndex]).StringValue))
				{
					if (((IUpdateableStringField)this.Holder[this.TempBodyIndex]).StringValue.Length >= languageDetectionMaximumCharacters)
					{
						return ((IUpdateableStringField)this.Holder[this.TempBodyIndex]).StringValue;
					}
					this.contentForLanguageDetection.AppendFormat("{0} ", ((IUpdateableStringField)this.Holder[this.TempBodyIndex]).StringValue);
				}
				if (!string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.SubjectIndex]).StringValue))
				{
					this.contentForLanguageDetection.AppendFormat("{0} ", ((IUpdateableStringField)this.Holder[this.SubjectIndex]).StringValue);
					if (this.contentForLanguageDetection.Length > languageDetectionMaximumCharacters)
					{
						return this.contentForLanguageDetection.ToString();
					}
				}
				foreach (int num2 in this.participantFields)
				{
					if (((IUpdateableListField<string>)this.Holder[num2]).List != null)
					{
						foreach (string participantString in ((IUpdateableListField<string>)this.Holder[num2]).List)
						{
							SearchParticipant searchParticipant = SearchParticipant.FromParticipantString(participantString);
							if (searchParticipant != null && !string.IsNullOrEmpty(searchParticipant.DisplayName))
							{
								this.contentForLanguageDetection.AppendFormat("{0} ", searchParticipant.DisplayName);
								if (this.contentForLanguageDetection.Length > languageDetectionMaximumCharacters)
								{
									break;
								}
							}
						}
					}
				}
				if (item is Task && !string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.TaskTitleIndex]).StringValue))
				{
					this.contentForLanguageDetection.AppendFormat("{0} ", ((IUpdateableStringField)this.Holder[this.TaskTitleIndex]).StringValue);
				}
				if ((item is MeetingMessage || item is CalendarItem) && !string.IsNullOrEmpty(((IUpdateableStringField)this.Holder[this.MeetingLocationIndex]).StringValue))
				{
					this.contentForLanguageDetection.AppendFormat("{0} ", ((IUpdateableStringField)this.Holder[this.MeetingLocationIndex]).StringValue);
				}
			}
			return this.contentForLanguageDetection.ToString();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00009190 File Offset: 0x00007390
		string IAttachmentRetrieverProducer.get_FlowIdentifier()
		{
			return base.FlowIdentifier;
		}

		// Token: 0x040000E4 RID: 228
		internal const string SearchRetrieverProducerRopNotSupported = "SearchRetrieverProducerRopNotSupported";

		// Token: 0x040000E5 RID: 229
		private IRecordImplDescriptor recordImpl;

		// Token: 0x040000E6 RID: 230
		private AttachmentRetrieverHandler attachmentRetrieverHandler;

		// Token: 0x040000E7 RID: 231
		private IUpdateableField tempBodyFieldReference;

		// Token: 0x040000E8 RID: 232
		private bool fireTransientFaultInjection;

		// Token: 0x040000E9 RID: 233
		private int[] contactFields;

		// Token: 0x040000EA RID: 234
		private int[] participantFields;

		// Token: 0x040000EB RID: 235
		private StringBuilder contentForLanguageDetection;
	}
}
