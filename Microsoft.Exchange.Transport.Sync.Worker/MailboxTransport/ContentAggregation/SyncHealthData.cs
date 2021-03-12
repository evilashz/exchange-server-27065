using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Worker.Health;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SyncHealthData
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00007791 File Offset: 0x00005991
		public SyncHealthData()
		{
			this.ThrottlingStatistics = new ThrottlingStatistics();
			this.Exceptions = new List<Exception>();
			this.CloudStatistics = new CloudStatistics();
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000077BA File Offset: 0x000059BA
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000077C2 File Offset: 0x000059C2
		public int TotalItemAddsEnumeratedFromRemoteServer { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000077CB File Offset: 0x000059CB
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000077D3 File Offset: 0x000059D3
		public int TotalItemAddsAppliedToLocalServer { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000077DC File Offset: 0x000059DC
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000077E4 File Offset: 0x000059E4
		public int TotalItemChangesEnumeratedFromRemoteServer { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000077ED File Offset: 0x000059ED
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000077F5 File Offset: 0x000059F5
		public int TotalItemChangesAppliedToLocalServer { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000077FE File Offset: 0x000059FE
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00007806 File Offset: 0x00005A06
		public int TotalItemDeletesEnumeratedFromRemoteServer { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000780F File Offset: 0x00005A0F
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00007817 File Offset: 0x00005A17
		public int TotalItemDeletesAppliedToLocalServer { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00007820 File Offset: 0x00005A20
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00007828 File Offset: 0x00005A28
		public int TotalFolderAddsEnumeratedFromRemoteServer { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00007831 File Offset: 0x00005A31
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00007839 File Offset: 0x00005A39
		public int TotalFolderAddsAppliedToLocalServer { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00007842 File Offset: 0x00005A42
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000784A File Offset: 0x00005A4A
		public int TotalFolderChangesEnumeratedFromRemoteServer { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00007853 File Offset: 0x00005A53
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000785B File Offset: 0x00005A5B
		public int TotalFolderChangesAppliedToLocalServer { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00007864 File Offset: 0x00005A64
		// (set) Token: 0x06000125 RID: 293 RVA: 0x0000786C File Offset: 0x00005A6C
		public int TotalFolderDeletesEnumeratedFromRemoteServer { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007875 File Offset: 0x00005A75
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000787D File Offset: 0x00005A7D
		public int TotalFolderDeletesAppliedToLocalServer { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007886 File Offset: 0x00005A86
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000788E File Offset: 0x00005A8E
		public int TotalItemAddsPermanentExceptions { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00007897 File Offset: 0x00005A97
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000789F File Offset: 0x00005A9F
		public int TotalItemAddsTransientExceptions { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000078A8 File Offset: 0x00005AA8
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000078B0 File Offset: 0x00005AB0
		public int TotalItemDeletesPermanentExceptions { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000078B9 File Offset: 0x00005AB9
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000078C1 File Offset: 0x00005AC1
		public int TotalItemDeletesTransientExceptions { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000078CA File Offset: 0x00005ACA
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000078D2 File Offset: 0x00005AD2
		public int TotalItemSoftDeletesPermanentExceptions { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000078DB File Offset: 0x00005ADB
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000078E3 File Offset: 0x00005AE3
		public int TotalItemSoftDeletesTransientExceptions { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000078EC File Offset: 0x00005AEC
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000078F4 File Offset: 0x00005AF4
		public int TotalItemChangesPermanentExceptions { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000078FD File Offset: 0x00005AFD
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00007905 File Offset: 0x00005B05
		public int TotalItemChangesTransientExceptions { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000790E File Offset: 0x00005B0E
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00007916 File Offset: 0x00005B16
		public int TotalFolderAddsPermanentExceptions { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000791F File Offset: 0x00005B1F
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007927 File Offset: 0x00005B27
		public int TotalFolderAddsTransientExceptions { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007930 File Offset: 0x00005B30
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00007938 File Offset: 0x00005B38
		public int TotalFolderDeletesPermanentExceptions { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007941 File Offset: 0x00005B41
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00007949 File Offset: 0x00005B49
		public int TotalFolderDeletesTransientExceptions { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00007952 File Offset: 0x00005B52
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000795A File Offset: 0x00005B5A
		public int TotalFolderSoftDeletesPermanentExceptions { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007963 File Offset: 0x00005B63
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000796B File Offset: 0x00005B6B
		public int TotalFolderSoftDeletesTransientExceptions { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007974 File Offset: 0x00005B74
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000797C File Offset: 0x00005B7C
		public int TotalFolderChangesPermanentExceptions { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007985 File Offset: 0x00005B85
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000798D File Offset: 0x00005B8D
		public int TotalFolderChangesTransientExceptions { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007996 File Offset: 0x00005B96
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000799E File Offset: 0x00005B9E
		public TimeSpan SyncDuration { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000079A7 File Offset: 0x00005BA7
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000079AF File Offset: 0x00005BAF
		public bool RecoverySync { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000079B8 File Offset: 0x00005BB8
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000079C0 File Offset: 0x00005BC0
		public bool IsPermanentSyncError { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000079C9 File Offset: 0x00005BC9
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000079D1 File Offset: 0x00005BD1
		public bool IsTransientSyncError { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000079DA File Offset: 0x00005BDA
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000079E2 File Offset: 0x00005BE2
		public int PoisonItemErrorsCount { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000079EB File Offset: 0x00005BEB
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000079F3 File Offset: 0x00005BF3
		public int OverSizeItemErrorsCount { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000079FC File Offset: 0x00005BFC
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00007A04 File Offset: 0x00005C04
		public int UnresolveableFolderNameErrorsCount { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007A0D File Offset: 0x00005C0D
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00007A15 File Offset: 0x00005C15
		public int ObjectNotFoundErrorsCount { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007A1E File Offset: 0x00005C1E
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00007A26 File Offset: 0x00005C26
		public int OtherItemErrorsCount { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007A2F File Offset: 0x00005C2F
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00007A37 File Offset: 0x00005C37
		public int PermanentItemErrorsCount { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007A40 File Offset: 0x00005C40
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007A48 File Offset: 0x00005C48
		public int TransientItemErrorsCount { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007A51 File Offset: 0x00005C51
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007A59 File Offset: 0x00005C59
		public int PermanentFolderErrorsCount { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007A62 File Offset: 0x00005C62
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007A6A File Offset: 0x00005C6A
		public int TransientFolderErrorsCount { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007A73 File Offset: 0x00005C73
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007A7B File Offset: 0x00005C7B
		public ByteQuantifiedSize TotalBytesEnumeratedFromRemoteServer { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007A84 File Offset: 0x00005C84
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007A8C File Offset: 0x00005C8C
		public int TotalSuccessfulRemoteRoundtrips { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00007A95 File Offset: 0x00005C95
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007A9D File Offset: 0x00005C9D
		public TimeSpan AverageSuccessfulRemoteRoundtripTime { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007AA6 File Offset: 0x00005CA6
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007AAE File Offset: 0x00005CAE
		public int TotalUnsuccessfulRemoteRoundtrips { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00007AB7 File Offset: 0x00005CB7
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007ABF File Offset: 0x00005CBF
		public TimeSpan AverageUnsuccessfulRemoteRoundtripTime { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007AC8 File Offset: 0x00005CC8
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public int TotalSuccessfulEngineRoundtrips { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007AD9 File Offset: 0x00005CD9
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00007AE1 File Offset: 0x00005CE1
		public TimeSpan AverageSuccessfulEngineRoundtripTime { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007AEA File Offset: 0x00005CEA
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007AF2 File Offset: 0x00005CF2
		public int TotalUnsuccessfulEngineRoundtrips { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007AFB File Offset: 0x00005CFB
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007B03 File Offset: 0x00005D03
		public TimeSpan AverageUnsuccessfulEngineRoundtripTime { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007B0C File Offset: 0x00005D0C
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00007B14 File Offset: 0x00005D14
		public TimeSpan AverageEngineBackoffTime { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007B1D File Offset: 0x00005D1D
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00007B25 File Offset: 0x00005D25
		public int TotalSuccessfulNativeRoundtrips { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007B2E File Offset: 0x00005D2E
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00007B36 File Offset: 0x00005D36
		public TimeSpan AverageSuccessfulNativeRoundtripTime { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007B3F File Offset: 0x00005D3F
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00007B47 File Offset: 0x00005D47
		public int TotalUnsuccessfulNativeRoundtrips { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00007B50 File Offset: 0x00005D50
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00007B58 File Offset: 0x00005D58
		public TimeSpan AverageUnsuccessfulNativeRoundtripTime { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00007B61 File Offset: 0x00005D61
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00007B69 File Offset: 0x00005D69
		public TimeSpan AverageNativeBackoffTime { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007B72 File Offset: 0x00005D72
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00007B7A File Offset: 0x00005D7A
		public List<Exception> Exceptions { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00007B83 File Offset: 0x00005D83
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00007B8B File Offset: 0x00005D8B
		public ThrottlingStatistics ThrottlingStatistics { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007B94 File Offset: 0x00005D94
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00007B9C File Offset: 0x00005D9C
		public CloudStatistics CloudStatistics { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00007BA5 File Offset: 0x00005DA5
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00007BAD File Offset: 0x00005DAD
		public Exception SyncEngineException { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00007BB6 File Offset: 0x00005DB6
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00007BBE File Offset: 0x00005DBE
		public int TotalItemsSubmittedToTransport { get; set; }
	}
}
