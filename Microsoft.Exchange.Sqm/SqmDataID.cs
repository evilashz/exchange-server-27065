﻿using System;

namespace Microsoft.Exchange.Sqm
{
	// Token: 0x02000003 RID: 3
	public enum SqmDataID
	{
		// Token: 0x04000037 RID: 55
		GARYENTERTHIS = 1,
		// Token: 0x04000038 RID: 56
		UMGENERALTOTALCALLS,
		// Token: 0x04000039 RID: 57
		UMCALLANSWERINGCALLS,
		// Token: 0x0400003A RID: 58
		NETWORK_BANDWIDTH_TEST,
		// Token: 0x0400003B RID: 59
		CALLANSWERCALLS,
		// Token: 0x0400003C RID: 60
		NAMETTSED,
		// Token: 0x0400003D RID: 61
		SPOKENNAMEACCESSED,
		// Token: 0x0400003E RID: 62
		CALLSWITHOUTPERSONALGREETING,
		// Token: 0x0400003F RID: 63
		GREETINGTIMEOUT,
		// Token: 0x04000040 RID: 64
		UM_RD_TOTALCALLS,
		// Token: 0x04000041 RID: 65
		UM_RD_DELAYEDCALLS,
		// Token: 0x04000042 RID: 66
		UM_NG_CALLANSWERCALLS,
		// Token: 0x04000043 RID: 67
		UM_NG_NAMETTSED,
		// Token: 0x04000044 RID: 68
		UM_NG_SPOKENNAMEACCESSED,
		// Token: 0x04000045 RID: 69
		UM_NG_CALLSWITHOUTPERSONALGREETING,
		// Token: 0x04000046 RID: 70
		UM_NG_GREETINGTIMEDOUT,
		// Token: 0x04000047 RID: 71
		UM_CC_CALLANSWERVOICEMESSAGES,
		// Token: 0x04000048 RID: 72
		UM_CC_FAXMESSAGES,
		// Token: 0x04000049 RID: 73
		UM_CC_SUBSCRIBERLOGON,
		// Token: 0x0400004A RID: 74
		UM_CC_VOICEMESSAGEQUEUEACCESS,
		// Token: 0x0400004B RID: 75
		UM_CC_CALENDARACCESSED,
		// Token: 0x0400004C RID: 76
		UM_CC_EMAILMESSAGEQUEUEACCESS,
		// Token: 0x0400004D RID: 77
		UM_CC_VOICEMESSAGESSENT,
		// Token: 0x0400004E RID: 78
		UM_CC_REPLYMESSAGESSENT,
		// Token: 0x0400004F RID: 79
		UM_CC_FORWARDMESSAGESSENT,
		// Token: 0x04000050 RID: 80
		UM_CC_LAUNCHEDCALLS,
		// Token: 0x04000051 RID: 81
		UM_CC_AATOTALCALLS,
		// Token: 0x04000052 RID: 82
		UM_CC_AATRANSFERREDCOUNT,
		// Token: 0x04000053 RID: 83
		UM_CC_AADIRECTORYACCESS,
		// Token: 0x04000054 RID: 84
		UM_CC_AADIRECTORYACCESSBYDIALBYNAME,
		// Token: 0x04000055 RID: 85
		UM_CC_AADIRECTORYACCESSSUCCESSFULLYBYDIALBYNAME,
		// Token: 0x04000056 RID: 86
		UM_CC_AADIRECTORYACCESSBYSPOKENNAME,
		// Token: 0x04000057 RID: 87
		UM_CC_AADIRECTORYACCESSSUCCESSFULLYBYSPOKENNAME,
		// Token: 0x04000058 RID: 88
		UM_DR_DELAYEDCALLS,
		// Token: 0x04000059 RID: 89
		UM_TOTALCALLS,
		// Token: 0x0400005A RID: 90
		ISTATSUMMARYTOTALADSITES,
		// Token: 0x0400005B RID: 91
		ISTATSUMMARYTOTALADDOMAINS,
		// Token: 0x0400005C RID: 92
		ISTATSUMMARYTOTALRECIPIENTPOLICIES,
		// Token: 0x0400005D RID: 93
		ISTATSUMMARYTOTALRECIPIENTUPDATESERVICES,
		// Token: 0x0400005E RID: 94
		ISTATSUMMARYTOTALADDRESSLISTS,
		// Token: 0x0400005F RID: 95
		ISTATSUMMARYTOTALGLOBALADDRESSLISTS,
		// Token: 0x04000060 RID: 96
		ISTATSUMMARYTOTALOFFLINEADDRESSLISTS,
		// Token: 0x04000061 RID: 97
		ISTATSUMMARYTOTALMAILBOXES2,
		// Token: 0x04000062 RID: 98
		ISTATSUMMARYTOTALADMINGROUPS,
		// Token: 0x04000063 RID: 99
		ISTATSUMMARYTOTALROUTINGGROUPS,
		// Token: 0x04000064 RID: 100
		ISTATSUMMARYTOTALSMTPCONNECTORS,
		// Token: 0x04000065 RID: 101
		ISTATSUMMARYTOTALRGCONNECTORS,
		// Token: 0x04000066 RID: 102
		ISTATSUMMARYTOTALX400CONNECTORS,
		// Token: 0x04000067 RID: 103
		ISTATSUMMARYTOTALNOTESCONNECTORS,
		// Token: 0x04000068 RID: 104
		ISTATSUMMARYTOTALEDKCONNECTORS,
		// Token: 0x04000069 RID: 105
		ISTATSUMMARYTOTALCONNECTORS,
		// Token: 0x0400006A RID: 106
		ISTATSUMMARYTOTALEXCHANGE2007SERVERS,
		// Token: 0x0400006B RID: 107
		ISTATSUMMARYTOTALEXCHANGE2007MAILBOXROLES,
		// Token: 0x0400006C RID: 108
		ISTATSUMMARYTOTALEXCHANGE2007CLIENTACCESSROLES,
		// Token: 0x0400006D RID: 109
		ISTATSUMMARYTOTALEXCHANGE2007UNIFIEDMESSAGINGROLES,
		// Token: 0x0400006E RID: 110
		ISTATSUMMARYTOTALEXCHANGE2007HUBTRANSPORTROLES,
		// Token: 0x0400006F RID: 111
		ISTATSUMMARYTOTALEXCHANGE2007EDGEROLES,
		// Token: 0x04000070 RID: 112
		ISTATSUMMARYTOTALEXCHANGE2003SERVERS,
		// Token: 0x04000071 RID: 113
		ISTATSUMMARYTOTALEXCHANGE2000SERVERS,
		// Token: 0x04000072 RID: 114
		ISTATSUMMARYTOTALEXCHANGESERVERSWIN2003,
		// Token: 0x04000073 RID: 115
		ISTATSUMMARYTOTALEXCHANGESERVERSWIN2000,
		// Token: 0x04000074 RID: 116
		ISTATSUMMARYTOTALEXCHANGESERVERS,
		// Token: 0x04000075 RID: 117
		ISTATSUMMARYTOTALEXCHANGECLUSTERS,
		// Token: 0x04000076 RID: 118
		ISTATSUMMARYTOTALEXCHANGEONENODECLUSTERS,
		// Token: 0x04000077 RID: 119
		ISTATSUMMARYTOTALEXCHANGETWONODECLUSTERS,
		// Token: 0x04000078 RID: 120
		ISTATSUMMARYTOTALEXCHANGETHREENODECLUSTERS,
		// Token: 0x04000079 RID: 121
		ISTATSUMMARYTOTALEXCHANGEFOURNODECLUSTERS,
		// Token: 0x0400007A RID: 122
		ISTATSUMMARYTOTALEXCHANGEFIVENODECLUSTERS,
		// Token: 0x0400007B RID: 123
		ISTATSUMMARYTOTALEXCHANGESIXNODECLUSTERS,
		// Token: 0x0400007C RID: 124
		ISTATSUMMARYTOTALEXCHANGESEVENNODECLUSTERS,
		// Token: 0x0400007D RID: 125
		ISTATSUMMARYAVERAGEMAILBOXESPERSERVER,
		// Token: 0x0400007E RID: 126
		ISTATSUMMARYTOTALEXCHANGEEIGHTNODECLUSTERS,
		// Token: 0x0400007F RID: 127
		ISTATSUMMARYTOTALFRONTENDSERVERS,
		// Token: 0x04000080 RID: 128
		ISTATSUMMARYTOTALMAILBOXMDBS,
		// Token: 0x04000081 RID: 129
		ISTATSUMMARYTOTALMAILBOXMDBSTORAGE,
		// Token: 0x04000082 RID: 130
		ISTATSUMMARYTOTALMOMAGENTSDEPLOYED,
		// Token: 0x04000083 RID: 131
		ISTATSUMMARYTOTALPUBLICMDBS,
		// Token: 0x04000084 RID: 132
		ISTATSUMMARYTOTALPUBLICMDBSTORAGE,
		// Token: 0x04000085 RID: 133
		ORGOBJECTGUID1,
		// Token: 0x04000086 RID: 134
		ORGOBJECTGUID2,
		// Token: 0x04000087 RID: 135
		ORGOBJECTGUID3,
		// Token: 0x04000088 RID: 136
		ORGOBJECTGUID4,
		// Token: 0x04000089 RID: 137
		EXBPAHEALTHTASKRUN,
		// Token: 0x0400008A RID: 138
		EXBPAPERFTASKRUN,
		// Token: 0x0400008B RID: 139
		EXBPAPERMSTASKRUN,
		// Token: 0x0400008C RID: 140
		EXBPACONNECTIVITYTASKRUN,
		// Token: 0x0400008D RID: 141
		EXBPABASELINETASKRUN,
		// Token: 0x0400008E RID: 142
		EXTRARUN,
		// Token: 0x0400008F RID: 143
		EXPTARUNONEDGE,
		// Token: 0x04000090 RID: 144
		EXPTARUNNOTEDGE,
		// Token: 0x04000091 RID: 145
		EXMFARUN,
		// Token: 0x04000092 RID: 146
		MESSAGETRACKINGRUN,
		// Token: 0x04000093 RID: 147
		EXDRANOTEDGE,
		// Token: 0x04000094 RID: 148
		EXDRAONEDGE,
		// Token: 0x04000095 RID: 149
		TRACECONTROLRUN,
		// Token: 0x04000096 RID: 150
		PROCESSFAILURERUN,
		// Token: 0x04000097 RID: 151
		EXDRADATABASEANALYSISRUN,
		// Token: 0x04000098 RID: 152
		EXDRALOGDRIVEANALYSISRUN,
		// Token: 0x04000099 RID: 153
		EXDRALOGMOVERUN,
		// Token: 0x0400009A RID: 154
		EXDRADBREPAIRRUN,
		// Token: 0x0400009B RID: 155
		EXDRAEVENTVIEWRUN,
		// Token: 0x0400009C RID: 156
		EXDRACREATERSGRUN,
		// Token: 0x0400009D RID: 157
		EXDRAREMOVERSGRUN,
		// Token: 0x0400009E RID: 158
		EXDRAMOUNTRUN,
		// Token: 0x0400009F RID: 159
		EXDRAMERGERUN,
		// Token: 0x040000A0 RID: 160
		EXMFANDRRUN,
		// Token: 0x040000A1 RID: 161
		EXMFAINBOUNDRUN,
		// Token: 0x040000A2 RID: 162
		EXMFAOUTBOUNDRUN,
		// Token: 0x040000A3 RID: 163
		EXMFAQUEUERUN,
		// Token: 0x040000A4 RID: 164
		EXMFASUBMISSIONRUN,
		// Token: 0x040000A5 RID: 165
		ECMFAEDGESYNCRUN,
		// Token: 0x040000A6 RID: 166
		EXPTARPCOPSRUN,
		// Token: 0x040000A7 RID: 167
		EXPTADELAYSRUN,
		// Token: 0x040000A8 RID: 168
		EXPTARPCREQUESTSRUN,
		// Token: 0x040000A9 RID: 169
		EXBPAHELPFUL,
		// Token: 0x040000AA RID: 170
		EXTRAHELPFUL,
		// Token: 0x040000AB RID: 171
		EXDRALOGRESETTASK,
		// Token: 0x040000AC RID: 172
		EXDRADBRESTORESETUP,
		// Token: 0x040000AD RID: 173
		EXDRATROUBLESHOOTER,
		// Token: 0x040000AE RID: 174
		ISTATMAILBOXESE12RTM,
		// Token: 0x040000AF RID: 175
		ISTATMAILBOXESE12SP1,
		// Token: 0x040000B0 RID: 176
		ISTATMAILBOXESE12RTMCLUSMBXCCR,
		// Token: 0x040000B1 RID: 177
		ISTATMAILBOXESE12RTMCLUSMBXSHARED,
		// Token: 0x040000B2 RID: 178
		ISTATMAILBOXESE12RTMMBX,
		// Token: 0x040000B3 RID: 179
		ISTATMAILBOXESE12SP1CLUSMBXCCR,
		// Token: 0x040000B4 RID: 180
		ISTATMAILBOXESE12SP1CLUSMBXSHARED,
		// Token: 0x040000B5 RID: 181
		ISTATMAILBOXESE12SP1MBX,
		// Token: 0x040000B6 RID: 182
		ISTATMAILBOXESE12SP1CLUSMBXCCRSCR,
		// Token: 0x040000B7 RID: 183
		ISTATMAILBOXESE12SP1CLUSMBXSHAREDSCR,
		// Token: 0x040000B8 RID: 184
		ISTATMAILBOXESE12SP1MBXSCR,
		// Token: 0x040000B9 RID: 185
		ISTATMAILBOXESLHE12SP1CLUSMBXCCR,
		// Token: 0x040000BA RID: 186
		ISTATMAILBOXESLHE12SP1CLUSMBXSHARED,
		// Token: 0x040000BB RID: 187
		ISTATMAILBOXESLHE12SP1MBX,
		// Token: 0x040000BC RID: 188
		ISTATMAILBOXESLHE12SP1CLUSMBXCCRSCR,
		// Token: 0x040000BD RID: 189
		ISTATMAILBOXESLHE12SP1CLUSMBXSHAREDSCR,
		// Token: 0x040000BE RID: 190
		ISTATMAILBOXESLHE12SP1MBXSCR,
		// Token: 0x040000BF RID: 191
		ISTATSERVERE12SP1,
		// Token: 0x040000C0 RID: 192
		ISTATSERVERE12RTMCLUSMBXCCR,
		// Token: 0x040000C1 RID: 193
		ISTATSERVERE12RTMCLUSMBXSHARED,
		// Token: 0x040000C2 RID: 194
		ISTATSERVERE12SP1CLUSMBXCCR,
		// Token: 0x040000C3 RID: 195
		ISTATSERVERE12SP1CLUSMBXSHARED,
		// Token: 0x040000C4 RID: 196
		ISTATSERVERE12SP1MBX,
		// Token: 0x040000C5 RID: 197
		ISTATSERVERE12SP1CAS,
		// Token: 0x040000C6 RID: 198
		ISTATSERVERE12SP1UM,
		// Token: 0x040000C7 RID: 199
		ISTATSERVERE12SP1HUB,
		// Token: 0x040000C8 RID: 200
		ISTATSERVERE12SP1EDGE,
		// Token: 0x040000C9 RID: 201
		ISTATSERVERLHE12SP1CLUSMBXCCR,
		// Token: 0x040000CA RID: 202
		ISTATSERVERLHE12SP1CLUSMBXSHARED,
		// Token: 0x040000CB RID: 203
		ISTATSERVERLHE12SP1MBX,
		// Token: 0x040000CC RID: 204
		ISTATSERVERLHE12SP1CAS,
		// Token: 0x040000CD RID: 205
		ISTATSERVERLHE12SP1UM,
		// Token: 0x040000CE RID: 206
		ISTATSERVERLHE12SP1HUB,
		// Token: 0x040000CF RID: 207
		ISTATSERVERLHE12SP1EDGE,
		// Token: 0x040000D0 RID: 208
		SCHEMARANGEUPPER,
		// Token: 0x040000D1 RID: 209
		CUSTOMERTYPE,
		// Token: 0x040000D2 RID: 210
		UMENABLEDUSERS,
		// Token: 0x040000D3 RID: 211
		UMACTIVEUSERS,
		// Token: 0x040000D4 RID: 212
		CMDLET_INFRA_CMDLETINFO,
		// Token: 0x040000D5 RID: 213
		CMDLET_INFRA_PARAMETER_NAME,
		// Token: 0x040000D6 RID: 214
		CMDLET_INFRA_ERROR_NAME,
		// Token: 0x040000D7 RID: 215
		ISTATMAILBOXESE14,
		// Token: 0x040000D8 RID: 216
		ISTATMAILBOXESLHE14MBX,
		// Token: 0x040000D9 RID: 217
		ISTATSERVERE14,
		// Token: 0x040000DA RID: 218
		ISTATSERVERLHE14MBX,
		// Token: 0x040000DB RID: 219
		ISTATSERVERLHE14CAS,
		// Token: 0x040000DC RID: 220
		ISTATSERVERLHE14UM,
		// Token: 0x040000DD RID: 221
		ISTATSERVERLHE14HUB,
		// Token: 0x040000DE RID: 222
		ISTATSERVERLHE14EDGE,
		// Token: 0x040000DF RID: 223
		IDAGSINORG,
		// Token: 0x040000E0 RID: 224
		ISTATSUMMARYTOTALEXWIN2008STD,
		// Token: 0x040000E1 RID: 225
		ISTATSUMMARYTOTALEXWIN2008DTC,
		// Token: 0x040000E2 RID: 226
		ISTATSUMMARYTOTALEXWIN2008SBS,
		// Token: 0x040000E3 RID: 227
		ISTATSUMMARYTOTALEXWIN2008ENT,
		// Token: 0x040000E4 RID: 228
		ISTATSUMMARYTOTALEXWIN2008DTCCORE,
		// Token: 0x040000E5 RID: 229
		ISTATSUMMARYTOTALEXWIN2008STDCORE,
		// Token: 0x040000E6 RID: 230
		ISTATSUMMARYTOTALEXWIN2008ENTCORE,
		// Token: 0x040000E7 RID: 231
		ISTATSUMMARYTOTALEXWIN2008ENTIT,
		// Token: 0x040000E8 RID: 232
		ISTATSUMMARYTOTALEXWIN2008WEB,
		// Token: 0x040000E9 RID: 233
		ISTATSUMMARYTOTALEXWIN2008CLUSTER,
		// Token: 0x040000EA RID: 234
		ISTATSUMMARYTOTALEXWIN2008HOME,
		// Token: 0x040000EB RID: 235
		ISTATSUMMARYTOTALEXWIN2008STORAGEEXPRESS,
		// Token: 0x040000EC RID: 236
		ISTATSUMMARYTOTALEXWIN2008STORAGESTD,
		// Token: 0x040000ED RID: 237
		ISTATSUMMARYTOTALEXWIN2008STORAGEWORKGROUP,
		// Token: 0x040000EE RID: 238
		ISTATSUMMARYTOTALEXWIN2008STORAGEENT,
		// Token: 0x040000EF RID: 239
		ISTATSUMMARYTOTALEXWIN2008SBSPREMIUM,
		// Token: 0x040000F0 RID: 240
		ISTATSUMMARYTOTALEXWIN2008WINEBMANAGEMENT,
		// Token: 0x040000F1 RID: 241
		ISTATSUMMARYTOTALEXWIN2008WINEBSECURITY,
		// Token: 0x040000F2 RID: 242
		ISTATSUMMARYTOTALEXWIN2008WINEBMESSAGING,
		// Token: 0x040000F3 RID: 243
		ISTATSUMMARYTOTALEASMAILBOXPOLICIES,
		// Token: 0x040000F4 RID: 244
		ISTATSUMMARYMAXEASATTACHMENTSIZE,
		// Token: 0x040000F5 RID: 245
		ISTATSUMMARYAVEEASATTACHMENTSIZE,
		// Token: 0x040000F6 RID: 246
		ISTATSUMMARYMINEASDEVICEREFRESHINTERVAL,
		// Token: 0x040000F7 RID: 247
		ISTATSUMMARYMAXEASDEVICEREFRESHINTERVAL,
		// Token: 0x040000F8 RID: 248
		ISTATSUMMARYAVEEASDEVICEREFRESHINTERVAL,
		// Token: 0x040000F9 RID: 249
		ISTATSUMMARYMAXEASDEVICEPASSWORDFAILEDATTEMPTS,
		// Token: 0x040000FA RID: 250
		ISTATSUMMARYAVEEASDEVICEPASSWORDFAILEDATTEMPTS,
		// Token: 0x040000FB RID: 251
		ISTATSUMMARYMAXEASPASSWORDEXPIRATION,
		// Token: 0x040000FC RID: 252
		ISTATSUMMARYAVEEASPASSWORDEXPIRATION,
		// Token: 0x040000FD RID: 253
		ISTATSUMMARYMAXEASPASSWORDHISTORY,
		// Token: 0x040000FE RID: 254
		ISTATSUMMARYAVEEASPASSWORDHISTORY,
		// Token: 0x040000FF RID: 255
		ISTATSUMMARYMAXEASINACTIVITYTIMEDEVICELOCK,
		// Token: 0x04000100 RID: 256
		ISTATSUMMARYAVEEASINACTIVITYTIMEDEVICELOCK,
		// Token: 0x04000101 RID: 257
		ISTATSUMMARYMINEASDEVICEPASSWORDLENGTH,
		// Token: 0x04000102 RID: 258
		ISTATSUMMARYAVEEASDEVICEPASSWORDLENGTH,
		// Token: 0x04000103 RID: 259
		ISTATSUMMARYTOTALEASALLOWNONPROVISIONABLEDEVICES,
		// Token: 0x04000104 RID: 260
		ISTATSUMMARYTOTALEASALPHANUMERICDEVICEPASSWORDREQUIRED,
		// Token: 0x04000105 RID: 261
		ISTATSUMMARYTOTALEASATTACHMENTSENABLED,
		// Token: 0x04000106 RID: 262
		ISTATSUMMARYTOTALEASDEVICEENCRYPTIONENABLED,
		// Token: 0x04000107 RID: 263
		ISTATSUMMARYTOTALEASDEVICEPASSWORDENABLED,
		// Token: 0x04000108 RID: 264
		ISTATSUMMARYTOTALEASPASSWORDRECOVERYENABLED,
		// Token: 0x04000109 RID: 265
		ISTATSUMMARYTOTALEASALLOWSIMPLEDEVICEPASSWORD,
		// Token: 0x0400010A RID: 266
		ISTATSUMMARYTOTALEASALLOWEXTERNALDEVICEMANAGEMENT,
		// Token: 0x0400010B RID: 267
		ISTATSUMMARYTOTALEASINTEGRATIONENABLEDDEFAULTSITE,
		// Token: 0x0400010C RID: 268
		ISTATSUMMARYTOTALEASDEVICEPARTNER,
		// Token: 0x0400010D RID: 269
		ISTATSUMMARYTOTALEASENABLED,
		// Token: 0x0400010E RID: 270
		ISTATSUMMARYTOTALEASWSSACCESSENABLED,
		// Token: 0x0400010F RID: 271
		ISTATSUMMARYTOTALEASUNCACCESSENABLED,
		// Token: 0x04000110 RID: 272
		ISTATSUMMARYEASDOCALLOWEDSERVERSEXIST,
		// Token: 0x04000111 RID: 273
		ISTATDEFAULTOWAVDIRSMARTCARDREQUIRED,
		// Token: 0x04000112 RID: 274
		ISTATDEFAULTOWAVDIRBASICAUTHENTICATION,
		// Token: 0x04000113 RID: 275
		ISTATDEFAULTOWAVDIRFORMSAUTHENTICATION,
		// Token: 0x04000114 RID: 276
		ISTATDEFAULTOWAVDIRDIGESTAUTHENTICATION,
		// Token: 0x04000115 RID: 277
		ISTATDEFAULTOWAVDIRWINDOWSAUTHENTICATION,
		// Token: 0x04000116 RID: 278
		ISTATSUMMARYTOTALOWAVDIRACTIVESYNCINTEGRATIONENABLED,
		// Token: 0x04000117 RID: 279
		ISTATSUMMARYTOTALOWAVDIRALLADDRESSLISTSENABLED,
		// Token: 0x04000118 RID: 280
		ISTATSUMMARYTOTALOWAVDIRCALENDARENABLED,
		// Token: 0x04000119 RID: 281
		ISTATSUMMARYTOTALOWAVDIRCONTACTSENABLED,
		// Token: 0x0400011A RID: 282
		ISTATSUMMARYTOTALOWAVDIRJOURNALENABLED,
		// Token: 0x0400011B RID: 283
		ISTATSUMMARYTOTALOWAVDIRJUNKEMAILENABLED,
		// Token: 0x0400011C RID: 284
		ISTATSUMMARYTOTALOWAVDIRREMINDERSANDNOTIFICATIONSENABLED,
		// Token: 0x0400011D RID: 285
		ISTATSUMMARYTOTALOWAVDIRNOTESENABLED,
		// Token: 0x0400011E RID: 286
		ISTATSUMMARYTOTALOWAVDIRPREMIUMCLIENTENABLED,
		// Token: 0x0400011F RID: 287
		ISTATSUMMARYTOTALOWAVDIRSEARCHFOLDERSENABLED,
		// Token: 0x04000120 RID: 288
		ISTATSUMMARYTOTALOWAVDIRSIGNATURESENABLED,
		// Token: 0x04000121 RID: 289
		ISTATSUMMARYTOTALOWAVDIRSPELLCHECKERENABLED,
		// Token: 0x04000122 RID: 290
		ISTATSUMMARYTOTALOWAVDIRTASKSENABLED,
		// Token: 0x04000123 RID: 291
		ISTATSUMMARYTOTALOWAVDIRTHEMESELECTIONENABLED,
		// Token: 0x04000124 RID: 292
		ISTATSUMMARYTOTALOWAVDIRUNCACCESSONPUBLICCOMPUTERSENABLED,
		// Token: 0x04000125 RID: 293
		ISTATSUMMARYTOTALOWAVDIRUNCACCESSONPRIVATECOMPUTERSENABLED,
		// Token: 0x04000126 RID: 294
		ISTATSUMMARYTOTALOWAVDIRUMINTEGRATIONENABLED,
		// Token: 0x04000127 RID: 295
		ISTATSUMMARYTOTALOWAVDIRWSSACCESSONPUBLICCOMPUTERSENABLED,
		// Token: 0x04000128 RID: 296
		ISTATSUMMARYTOTALOWAVDIRWSSACCESSONPRIVATECOMPUTERSENABLED,
		// Token: 0x04000129 RID: 297
		ISTATSUMMARYTOTALOWAVDIRCHANGEPASSWORDENABLEDD,
		// Token: 0x0400012A RID: 298
		ISTATMAILBOXESE12SP2,
		// Token: 0x0400012B RID: 299
		ISTATMAILBOXESW7E14MBX,
		// Token: 0x0400012C RID: 300
		ISTATSERVERE12SP2,
		// Token: 0x0400012D RID: 301
		ISTATSERVERW7E14MBX,
		// Token: 0x0400012E RID: 302
		ISTATSERVERW7E14CAS,
		// Token: 0x0400012F RID: 303
		ISTATSERVERW7E14UM,
		// Token: 0x04000130 RID: 304
		ISTATSERVERW7E14HUB,
		// Token: 0x04000131 RID: 305
		ISTATSERVERW7E14EDGE,
		// Token: 0x04000132 RID: 306
		DATAID_EMC_GUI_ACTION,
		// Token: 0x04000133 RID: 307
		DATAID_EMC_VERSION,
		// Token: 0x04000134 RID: 308
		DATAID_OS,
		// Token: 0x04000135 RID: 309
		DATAID_ORGANIZATION_SIZE,
		// Token: 0x04000136 RID: 310
		TESTOPTIONSYMBOL,
		// Token: 0x04000137 RID: 311
		TESTCONTINUOUSSYMBOL,
		// Token: 0x04000138 RID: 312
		ISTATSUMMARYTOTALVIRTUALDC,
		// Token: 0x04000139 RID: 313
		ISTATSUMMARYTOTALVMWAREDC,
		// Token: 0x0400013A RID: 314
		ISTATSUMMARYTOTALVIRTUALE12MAILBOXROLE,
		// Token: 0x0400013B RID: 315
		ISTATSUMMARYTOTALVIRTUALE12CLIENTACCESSROLE,
		// Token: 0x0400013C RID: 316
		ISTATSUMMARYTOTALVIRTUALE12UNIFIEDMESSAGINGROLE,
		// Token: 0x0400013D RID: 317
		ISTATSUMMARYTOTALVIRTUALE12BRIDGEHEADROLE,
		// Token: 0x0400013E RID: 318
		ISTATSUMMARYTOTALVIRTUALE12EDGEROLE,
		// Token: 0x0400013F RID: 319
		ISTATSUMMARYTOTALVIRTUALCLUSTERNODE,
		// Token: 0x04000140 RID: 320
		ISTATSUMMARYTOTALVMWAREE12MAILBOXROLE,
		// Token: 0x04000141 RID: 321
		ISTATSUMMARYTOTALVMWAREE12CLIENTACCESSROLE,
		// Token: 0x04000142 RID: 322
		ISTATSUMMARYTOTALVMWAREE12UNIFIEDMESSAGINGROLE,
		// Token: 0x04000143 RID: 323
		ISTATSUMMARYTOTALVMWAREE12BRIDGEHEADROLE,
		// Token: 0x04000144 RID: 324
		ISTATSUMMARYTOTALVMWAREE12EDGEROLE,
		// Token: 0x04000145 RID: 325
		ISTATSUMMARYTOTALVMWARECLUSTERNODE,
		// Token: 0x04000146 RID: 326
		COMMON_DW_ORGID,
		// Token: 0x04000147 RID: 327
		SMS_ST_EASCONFIGURATION,
		// Token: 0x04000148 RID: 328
		SMS_ST_EASESTABLISHMENT,
		// Token: 0x04000149 RID: 329
		SMS_ST_EASMESSAGES,
		// Token: 0x0400014A RID: 330
		SMS_ST_NDRS,
		// Token: 0x0400014B RID: 331
		SMS_ST_NOTIFICATIONMESSAGE,
		// Token: 0x0400014C RID: 332
		SMS_ST_NOTIFICATIONTURNINGON,
		// Token: 0x0400014D RID: 333
		SMS_ST_NOTIFICATIONCONFIGURATION,
		// Token: 0x0400014E RID: 334
		CMN_DYN_EXDEPLOYMENTTYPE,
		// Token: 0x0400014F RID: 335
		CMN_DYN_MAJORBUILDNUMBER,
		// Token: 0x04000150 RID: 336
		CMN_DYN_MINORBUILDNUMBER,
		// Token: 0x04000151 RID: 337
		CMN_DYN_USERID,
		// Token: 0x04000152 RID: 338
		CMN_DYN_ORGID,
		// Token: 0x04000153 RID: 339
		CMN_DYN_MACHINEID,
		// Token: 0x04000154 RID: 340
		CMN_DYN_MAJORBUILD,
		// Token: 0x04000155 RID: 341
		CMN_DYN_MINORBUILD,
		// Token: 0x04000156 RID: 342
		CMDLET_DYN_ERRORID,
		// Token: 0x04000157 RID: 343
		ISTATSERVERE14SP1,
		// Token: 0x04000158 RID: 344
		ISTATMAILBOXESE14SP1,
		// Token: 0x04000159 RID: 345
		DUMMYAPPID,
		// Token: 0x0400015A RID: 346
		ISTATSERVERW7E14SP1CAS,
		// Token: 0x0400015B RID: 347
		CMN_CNT_MAJORBUILDNO,
		// Token: 0x0400015C RID: 348
		CMN_CNT_MINORBUILDNO,
		// Token: 0x0400015D RID: 349
		ISTATSUMMARYTOTALDC,
		// Token: 0x0400015E RID: 350
		ISTATSUMMARYTOTALEXCHANGE2007BRIDGEHEADROLE,
		// Token: 0x0400015F RID: 351
		ISTATSERVERW7E14SP1MBX,
		// Token: 0x04000160 RID: 352
		ISTATSERVERW7E14SP1UM,
		// Token: 0x04000161 RID: 353
		ISTATSERVERW7E14SP1HUB,
		// Token: 0x04000162 RID: 354
		ISTATSERVERW7E14SP1EDGE,
		// Token: 0x04000163 RID: 355
		ISTATSERVERLHE14SP1MBX,
		// Token: 0x04000164 RID: 356
		ISTATSERVERLHE14SP1CAS,
		// Token: 0x04000165 RID: 357
		ISTATSERVERLHE14SP1UM,
		// Token: 0x04000166 RID: 358
		ISTATSERVERLHE14SP1HUB,
		// Token: 0x04000167 RID: 359
		ISTATSERVERLHE14SP1EDGE,
		// Token: 0x04000168 RID: 360
		ISTATMAILBOXESLHE14SP1MBX,
		// Token: 0x04000169 RID: 361
		ISTATMAILBOXESW7E14SP1MBX,
		// Token: 0x0400016A RID: 362
		ISTATMAILBOXESW7E12SP3CLUSMBXCCR,
		// Token: 0x0400016B RID: 363
		ISTATMAILBOXESW7E12SP3CLUSMBXSHARED,
		// Token: 0x0400016C RID: 364
		ISTATMAILBOXESW7E12SP3MBX,
		// Token: 0x0400016D RID: 365
		ISTATMAILBOXESW7E12SP3CLUSMBXCCRSCR,
		// Token: 0x0400016E RID: 366
		ISTATMAILBOXESW7E12SP3CLUSMBXSHAREDSCR,
		// Token: 0x0400016F RID: 367
		ISTATMAILBOXESW7E12SP3MBXSCR,
		// Token: 0x04000170 RID: 368
		ISTATMAILBOXESE12SP3,
		// Token: 0x04000171 RID: 369
		ISTATSERVERE12SP3,
		// Token: 0x04000172 RID: 370
		ISTATSERVERW7E12SP3CLUSMBXCCR,
		// Token: 0x04000173 RID: 371
		ISTATSERVERW7E12SP3CLUSMBXSHARED,
		// Token: 0x04000174 RID: 372
		ISTATSERVERW7E12SP3MBX,
		// Token: 0x04000175 RID: 373
		ISTATSERVERW7E12SP3CAS,
		// Token: 0x04000176 RID: 374
		ISTATSERVERW7E12SP3UM,
		// Token: 0x04000177 RID: 375
		ISTATSERVERW7E12SP3HUB,
		// Token: 0x04000178 RID: 376
		ISTATSERVERW7E12SP3EDGE,
		// Token: 0x04000179 RID: 377
		ISTATSERVERE12SP3CLUSMBXCCR,
		// Token: 0x0400017A RID: 378
		ISTATSERVERE12SP3CLUSMBXSHARED,
		// Token: 0x0400017B RID: 379
		ISTATSERVERE12SP3MBX,
		// Token: 0x0400017C RID: 380
		ISTATSERVERE12SP3CAS,
		// Token: 0x0400017D RID: 381
		ISTATSERVERE12SP3UM,
		// Token: 0x0400017E RID: 382
		ISTATSERVERE12SP3HUB,
		// Token: 0x0400017F RID: 383
		ISTATSERVERE12SP3EDGE,
		// Token: 0x04000180 RID: 384
		ISTATSERVERLHE12SP3CLUSMBXCCR,
		// Token: 0x04000181 RID: 385
		ISTATSERVERLHE12SP3CLUSMBXSHARED,
		// Token: 0x04000182 RID: 386
		ISTATSERVERLHE12SP3MBX,
		// Token: 0x04000183 RID: 387
		ISTATSERVERLHE12SP3CAS,
		// Token: 0x04000184 RID: 388
		ISTATSERVERLHE12SP3UM,
		// Token: 0x04000185 RID: 389
		ISTATSERVERLHE12SP3HUB,
		// Token: 0x04000186 RID: 390
		ISTATSERVERLHE12SP3EDGE,
		// Token: 0x04000187 RID: 391
		ISTATTOTALROLEEDGETRANSPORT,
		// Token: 0x04000188 RID: 392
		CMN_DYN_LABNAME,
		// Token: 0x04000189 RID: 393
		MAX
	}
}