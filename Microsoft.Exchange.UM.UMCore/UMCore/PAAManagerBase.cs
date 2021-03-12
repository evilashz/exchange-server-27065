using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.PersonalAutoAttendant;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000294 RID: 660
	internal abstract class PAAManagerBase : ActivityManager, IPAAUI
	{
		// Token: 0x06001379 RID: 4985 RVA: 0x000569A4 File Offset: 0x00054BA4
		internal PAAManagerBase(ActivityManager manager, ActivityManagerConfig config) : base(manager, config)
		{
			this.paaMenuItems = new PAAMenuItem[12];
			for (int i = 1; i <= 10; i++)
			{
				PAAMenuItem paamenuItem = new PAAMenuItem();
				paamenuItem.MenuKey = i;
				paamenuItem.Enabled = false;
				paamenuItem.Context = null;
				paamenuItem.MenuType = null;
				paamenuItem.TargetName = null;
				paamenuItem.TargetPhone = null;
				this.paaMenuItems[i] = paamenuItem;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::ctor()", new object[0]);
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x00056A2D File Offset: 0x00054C2D
		public bool HaveActions
		{
			get
			{
				return 0 != this.personalAutoAttendant.KeyMappingList.Count;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x00056A45 File Offset: 0x00054C45
		public bool HaveAutoActions
		{
			get
			{
				return this.personalAutoAttendant != null && 0 != this.personalAutoAttendant.AutoActionsList.Count;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00056A67 File Offset: 0x00054C67
		// (set) Token: 0x0600137D RID: 4989 RVA: 0x00056A6F File Offset: 0x00054C6F
		public bool HaveGreeting
		{
			get
			{
				return this.haveGreeting;
			}
			set
			{
				this.haveGreeting = value;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600137E RID: 4990 RVA: 0x00056A78 File Offset: 0x00054C78
		public bool AutoGeneratePrompt
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00056A7B File Offset: 0x00054C7B
		public bool TransferToVoiceMessageEnabled
		{
			get
			{
				return this.transferToVoiceMessageEnabled;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00056A83 File Offset: 0x00054C83
		// (set) Token: 0x06001381 RID: 4993 RVA: 0x00056A92 File Offset: 0x00054C92
		internal bool Key1Enabled
		{
			get
			{
				return this.paaMenuItems[1].Enabled;
			}
			set
			{
				this.paaMenuItems[1].Enabled = value;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00056AA2 File Offset: 0x00054CA2
		// (set) Token: 0x06001383 RID: 4995 RVA: 0x00056AB1 File Offset: 0x00054CB1
		internal string MenuType1
		{
			get
			{
				return this.paaMenuItems[1].MenuType;
			}
			set
			{
				this.paaMenuItems[1].MenuType = value;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00056AC1 File Offset: 0x00054CC1
		// (set) Token: 0x06001385 RID: 4997 RVA: 0x00056AD0 File Offset: 0x00054CD0
		internal string Context1
		{
			get
			{
				return this.paaMenuItems[1].Context;
			}
			set
			{
				this.paaMenuItems[1].Context = value;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00056AE0 File Offset: 0x00054CE0
		// (set) Token: 0x06001387 RID: 4999 RVA: 0x00056AEF File Offset: 0x00054CEF
		internal object TargetName1
		{
			get
			{
				return this.paaMenuItems[1].TargetName;
			}
			set
			{
				this.paaMenuItems[1].TargetName = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x00056AFF File Offset: 0x00054CFF
		// (set) Token: 0x06001389 RID: 5001 RVA: 0x00056B0E File Offset: 0x00054D0E
		internal PhoneNumber TargetPhone1
		{
			get
			{
				return this.paaMenuItems[1].TargetPhone;
			}
			set
			{
				this.paaMenuItems[1].TargetPhone = value;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x00056B1E File Offset: 0x00054D1E
		// (set) Token: 0x0600138B RID: 5003 RVA: 0x00056B2D File Offset: 0x00054D2D
		internal bool Key2Enabled
		{
			get
			{
				return this.paaMenuItems[2].Enabled;
			}
			set
			{
				this.paaMenuItems[2].Enabled = value;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00056B3D File Offset: 0x00054D3D
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x00056B4C File Offset: 0x00054D4C
		internal string MenuType2
		{
			get
			{
				return this.paaMenuItems[2].MenuType;
			}
			set
			{
				this.paaMenuItems[2].MenuType = value;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00056B5C File Offset: 0x00054D5C
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00056B6B File Offset: 0x00054D6B
		internal string Context2
		{
			get
			{
				return this.paaMenuItems[2].Context;
			}
			set
			{
				this.paaMenuItems[2].Context = value;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00056B7B File Offset: 0x00054D7B
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x00056B8A File Offset: 0x00054D8A
		internal object TargetName2
		{
			get
			{
				return this.paaMenuItems[2].TargetName;
			}
			set
			{
				this.paaMenuItems[2].TargetName = value;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00056B9A File Offset: 0x00054D9A
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x00056BA9 File Offset: 0x00054DA9
		internal PhoneNumber TargetPhone2
		{
			get
			{
				return this.paaMenuItems[2].TargetPhone;
			}
			set
			{
				this.paaMenuItems[2].TargetPhone = value;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00056BB9 File Offset: 0x00054DB9
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x00056BC8 File Offset: 0x00054DC8
		internal bool Key3Enabled
		{
			get
			{
				return this.paaMenuItems[3].Enabled;
			}
			set
			{
				this.paaMenuItems[3].Enabled = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00056BD8 File Offset: 0x00054DD8
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x00056BE7 File Offset: 0x00054DE7
		internal string MenuType3
		{
			get
			{
				return this.paaMenuItems[3].MenuType;
			}
			set
			{
				this.paaMenuItems[3].MenuType = value;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00056BF7 File Offset: 0x00054DF7
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x00056C06 File Offset: 0x00054E06
		internal string Context3
		{
			get
			{
				return this.paaMenuItems[3].Context;
			}
			set
			{
				this.paaMenuItems[3].Context = value;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00056C16 File Offset: 0x00054E16
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x00056C25 File Offset: 0x00054E25
		internal object TargetName3
		{
			get
			{
				return this.paaMenuItems[3].TargetName;
			}
			set
			{
				this.paaMenuItems[3].TargetName = value;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00056C35 File Offset: 0x00054E35
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00056C44 File Offset: 0x00054E44
		internal PhoneNumber TargetPhone3
		{
			get
			{
				return this.paaMenuItems[3].TargetPhone;
			}
			set
			{
				this.paaMenuItems[3].TargetPhone = value;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00056C54 File Offset: 0x00054E54
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x00056C63 File Offset: 0x00054E63
		internal bool Key4Enabled
		{
			get
			{
				return this.paaMenuItems[4].Enabled;
			}
			set
			{
				this.paaMenuItems[4].Enabled = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00056C73 File Offset: 0x00054E73
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x00056C82 File Offset: 0x00054E82
		internal string MenuType4
		{
			get
			{
				return this.paaMenuItems[4].MenuType;
			}
			set
			{
				this.paaMenuItems[4].MenuType = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x00056C92 File Offset: 0x00054E92
		// (set) Token: 0x060013A3 RID: 5027 RVA: 0x00056CA1 File Offset: 0x00054EA1
		internal string Context4
		{
			get
			{
				return this.paaMenuItems[4].Context;
			}
			set
			{
				this.paaMenuItems[4].Context = value;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00056CB1 File Offset: 0x00054EB1
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x00056CC0 File Offset: 0x00054EC0
		internal object TargetName4
		{
			get
			{
				return this.paaMenuItems[4].TargetName;
			}
			set
			{
				this.paaMenuItems[4].TargetName = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00056CD0 File Offset: 0x00054ED0
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x00056CDF File Offset: 0x00054EDF
		internal PhoneNumber TargetPhone4
		{
			get
			{
				return this.paaMenuItems[4].TargetPhone;
			}
			set
			{
				this.paaMenuItems[4].TargetPhone = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00056CEF File Offset: 0x00054EEF
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00056CFE File Offset: 0x00054EFE
		internal bool Key5Enabled
		{
			get
			{
				return this.paaMenuItems[5].Enabled;
			}
			set
			{
				this.paaMenuItems[5].Enabled = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00056D0E File Offset: 0x00054F0E
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00056D1D File Offset: 0x00054F1D
		internal string MenuType5
		{
			get
			{
				return this.paaMenuItems[5].MenuType;
			}
			set
			{
				this.paaMenuItems[5].MenuType = value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00056D2D File Offset: 0x00054F2D
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x00056D3C File Offset: 0x00054F3C
		internal string Context5
		{
			get
			{
				return this.paaMenuItems[5].Context;
			}
			set
			{
				this.paaMenuItems[5].Context = value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x00056D4C File Offset: 0x00054F4C
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x00056D5B File Offset: 0x00054F5B
		internal object TargetName5
		{
			get
			{
				return this.paaMenuItems[5].TargetName;
			}
			set
			{
				this.paaMenuItems[5].TargetName = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00056D6B File Offset: 0x00054F6B
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x00056D7A File Offset: 0x00054F7A
		internal PhoneNumber TargetPhone5
		{
			get
			{
				return this.paaMenuItems[5].TargetPhone;
			}
			set
			{
				this.paaMenuItems[5].TargetPhone = value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00056D8A File Offset: 0x00054F8A
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x00056D99 File Offset: 0x00054F99
		internal bool Key6Enabled
		{
			get
			{
				return this.paaMenuItems[6].Enabled;
			}
			set
			{
				this.paaMenuItems[6].Enabled = value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00056DA9 File Offset: 0x00054FA9
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00056DB8 File Offset: 0x00054FB8
		internal string MenuType6
		{
			get
			{
				return this.paaMenuItems[6].MenuType;
			}
			set
			{
				this.paaMenuItems[6].MenuType = value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00056DC8 File Offset: 0x00054FC8
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00056DD7 File Offset: 0x00054FD7
		internal string Context6
		{
			get
			{
				return this.paaMenuItems[6].Context;
			}
			set
			{
				this.paaMenuItems[6].Context = value;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x00056DE7 File Offset: 0x00054FE7
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x00056DF6 File Offset: 0x00054FF6
		internal object TargetName6
		{
			get
			{
				return this.paaMenuItems[6].TargetName;
			}
			set
			{
				this.paaMenuItems[6].TargetName = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00056E06 File Offset: 0x00055006
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x00056E15 File Offset: 0x00055015
		internal PhoneNumber TargetPhone6
		{
			get
			{
				return this.paaMenuItems[6].TargetPhone;
			}
			set
			{
				this.paaMenuItems[6].TargetPhone = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00056E25 File Offset: 0x00055025
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00056E34 File Offset: 0x00055034
		internal bool Key7Enabled
		{
			get
			{
				return this.paaMenuItems[7].Enabled;
			}
			set
			{
				this.paaMenuItems[7].Enabled = value;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00056E44 File Offset: 0x00055044
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x00056E53 File Offset: 0x00055053
		internal string MenuType7
		{
			get
			{
				return this.paaMenuItems[7].MenuType;
			}
			set
			{
				this.paaMenuItems[7].MenuType = value;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00056E63 File Offset: 0x00055063
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x00056E72 File Offset: 0x00055072
		internal string Context7
		{
			get
			{
				return this.paaMenuItems[7].Context;
			}
			set
			{
				this.paaMenuItems[7].Context = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00056E82 File Offset: 0x00055082
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x00056E91 File Offset: 0x00055091
		internal object TargetName7
		{
			get
			{
				return this.paaMenuItems[7].TargetName;
			}
			set
			{
				this.paaMenuItems[7].TargetName = value;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00056EA1 File Offset: 0x000550A1
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00056EB0 File Offset: 0x000550B0
		internal PhoneNumber TargetPhone7
		{
			get
			{
				return this.paaMenuItems[7].TargetPhone;
			}
			set
			{
				this.paaMenuItems[7].TargetPhone = value;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00056EC0 File Offset: 0x000550C0
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00056ECF File Offset: 0x000550CF
		internal bool Key8Enabled
		{
			get
			{
				return this.paaMenuItems[8].Enabled;
			}
			set
			{
				this.paaMenuItems[8].Enabled = value;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00056EDF File Offset: 0x000550DF
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x00056EEE File Offset: 0x000550EE
		internal string MenuType8
		{
			get
			{
				return this.paaMenuItems[8].MenuType;
			}
			set
			{
				this.paaMenuItems[8].MenuType = value;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00056EFE File Offset: 0x000550FE
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x00056F0D File Offset: 0x0005510D
		internal string Context8
		{
			get
			{
				return this.paaMenuItems[8].Context;
			}
			set
			{
				this.paaMenuItems[8].Context = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00056F1D File Offset: 0x0005511D
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00056F2C File Offset: 0x0005512C
		internal object TargetName8
		{
			get
			{
				return this.paaMenuItems[8].TargetName;
			}
			set
			{
				this.paaMenuItems[8].TargetName = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00056F3C File Offset: 0x0005513C
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x00056F4B File Offset: 0x0005514B
		internal PhoneNumber TargetPhone8
		{
			get
			{
				return this.paaMenuItems[8].TargetPhone;
			}
			set
			{
				this.paaMenuItems[8].TargetPhone = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x00056F5B File Offset: 0x0005515B
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x00056F6B File Offset: 0x0005516B
		internal bool Key9Enabled
		{
			get
			{
				return this.paaMenuItems[9].Enabled;
			}
			set
			{
				this.paaMenuItems[9].Enabled = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00056F7C File Offset: 0x0005517C
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x00056F8C File Offset: 0x0005518C
		internal string MenuType9
		{
			get
			{
				return this.paaMenuItems[9].MenuType;
			}
			set
			{
				this.paaMenuItems[9].MenuType = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00056F9D File Offset: 0x0005519D
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x00056FAD File Offset: 0x000551AD
		internal string Context9
		{
			get
			{
				return this.paaMenuItems[9].Context;
			}
			set
			{
				this.paaMenuItems[9].Context = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00056FBE File Offset: 0x000551BE
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00056FCE File Offset: 0x000551CE
		internal object TargetName9
		{
			get
			{
				return this.paaMenuItems[9].TargetName;
			}
			set
			{
				this.paaMenuItems[9].TargetName = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00056FDF File Offset: 0x000551DF
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x00056FEF File Offset: 0x000551EF
		internal PhoneNumber TargetPhone9
		{
			get
			{
				return this.paaMenuItems[9].TargetPhone;
			}
			set
			{
				this.paaMenuItems[9].TargetPhone = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00057000 File Offset: 0x00055200
		internal bool HaveRecordedName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00057003 File Offset: 0x00055203
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0005700B File Offset: 0x0005520B
		internal ITempWavFile PersonalGreeting
		{
			get
			{
				return this.personalGreeting;
			}
			set
			{
				this.personalGreeting = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00057014 File Offset: 0x00055214
		internal object RecordedName
		{
			get
			{
				if (this.recordedName == null)
				{
					this.recordedName = base.GetRecordedName(this.Subscriber.ADRecipient);
				}
				return this.recordedName;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0005703B File Offset: 0x0005523B
		internal bool MainMenuUninterruptible
		{
			get
			{
				return this.mainMenuUninterruptible;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00057043 File Offset: 0x00055243
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0005704B File Offset: 0x0005524B
		protected PersonalAutoAttendant PersonalAutoAttendant
		{
			get
			{
				return this.personalAutoAttendant;
			}
			set
			{
				this.personalAutoAttendant = value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00057054 File Offset: 0x00055254
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0005705C File Offset: 0x0005525C
		protected PAAMenuItem[] PAAMenuItems
		{
			get
			{
				return this.paaMenuItems;
			}
			set
			{
				this.paaMenuItems = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00057065 File Offset: 0x00055265
		protected Dictionary<int, PAAManagerBase.PAAPresentationObject> Menu
		{
			get
			{
				return this.menu;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0005706D File Offset: 0x0005526D
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00057075 File Offset: 0x00055275
		protected UMSubscriber Subscriber
		{
			get
			{
				return this.subscriber;
			}
			set
			{
				this.subscriber = value;
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00057080 File Offset: 0x00055280
		public void SetADTransferTargetMenuItem(int key, string type, string context, string legacyExchangeDN, ADRecipient recipient)
		{
			PIIMessage[] data = new PIIMessage[]
			{
				PIIMessage.Create(PIIType._PII, legacyExchangeDN),
				PIIMessage.Create(PIIType._UserDisplayName, (recipient != null) ? recipient.DisplayName : "<null>")
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, data, "SetADTransferTargetMenuItem() Key {0} Type {1} Context {2} LegDN _PII Recipient _UserDisplayName", new object[]
			{
				key,
				type,
				context
			});
			this.SetMenuItem(key, type, context);
			string text = "tempname";
			base.WriteVariable(text, null);
			if (recipient != null)
			{
				base.SetRecordedName(text, recipient);
			}
			else
			{
				int num = legacyExchangeDN.LastIndexOf("/cn=", StringComparison.OrdinalIgnoreCase);
				string varValue = legacyExchangeDN;
				if (num != -1 && legacyExchangeDN.Length > num + 4)
				{
					varValue = legacyExchangeDN.Substring(num + 4);
				}
				base.WriteVariable(text, varValue);
			}
			this.paaMenuItems[key].TargetName = (this.ReadVariable(text) ?? string.Empty);
			base.WriteVariable(text, null);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0005716E File Offset: 0x0005536E
		public void SetPhoneNumberTransferMenuItem(int key, string type, string context, string phoneNumberString)
		{
			this.SetMenuItem(key, type, context);
			this.paaMenuItems[key].TargetPhone = PhoneNumber.Parse(phoneNumberString);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0005718D File Offset: 0x0005538D
		public void SetFindMeMenuItem(int key, string type, string context)
		{
			this.SetMenuItem(key, type, context);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x00057198 File Offset: 0x00055398
		public void SetMenuItemTransferToVoiceMail()
		{
			this.transferToVoiceMessageEnabled = true;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000571A1 File Offset: 0x000553A1
		public virtual void SetADTransferTarget(ADRecipient mailboxTransferTarget)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000571A8 File Offset: 0x000553A8
		public virtual void SetBlindTransferEnabled(bool enabled, PhoneNumber target)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000571AF File Offset: 0x000553AF
		public virtual void SetPermissionCheckFailure()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000571B6 File Offset: 0x000553B6
		public virtual void SetTransferToMailboxEnabled()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000571BD File Offset: 0x000553BD
		public virtual void SetTransferToVoiceMessageEnabled()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x000571C4 File Offset: 0x000553C4
		public virtual void SetInvalidADContact()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x000571CB File Offset: 0x000553CB
		public virtual void SetFindMeNumbers(FindMe[] numbers)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x000571D4 File Offset: 0x000553D4
		internal string PrepareToExecutePAA(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::PrepareToExecutePAA()", new object[0]);
			KeyMappings keyMappings = this.HaveAutoActions ? this.personalAutoAttendant.AutoActionsList : this.personalAutoAttendant.KeyMappingList;
			foreach (KeyMappingBase keyMappingBase in keyMappings.SortedMenu)
			{
				PAAManagerBase.PAAPresentationObject paapresentationObject = PAAManagerBase.PAAPresentationObject.Create(keyMappingBase, this.Subscriber);
				this.menu[keyMappingBase.Key] = paapresentationObject;
				paapresentationObject.SetVariablesForMainMenu(this);
			}
			if (!this.personalAutoAttendant.EnableBargeIn)
			{
				this.mainMenuUninterruptible = true;
			}
			return null;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00057294 File Offset: 0x00055494
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::Start()", new object[0]);
			this.PersonalAutoAttendant = null;
			base.Start(vo, refInfo);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000572BC File Offset: 0x000554BC
		internal virtual string GetGreeting(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PersonalAutoAttendantManager::GetGreeting() PAA = {0}", new object[]
			{
				this.personalAutoAttendant.Identity.ToString()
			});
			this.LoadGreetingForPAA(this.personalAutoAttendant);
			return null;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0005730A File Offset: 0x0005550A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PAAManagerBase>(this);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x00057314 File Offset: 0x00055514
		protected void LoadGreetingForPAA(PersonalAutoAttendant paa)
		{
			ITempWavFile tempWavFile = null;
			this.haveGreeting = false;
			this.personalGreeting = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAManagerBase::LoadGreetingForPAA() PAA {0}", new object[]
			{
				paa.Identity
			});
			try
			{
				using (IPAAStore ipaastore = PAAStore.Create(this.subscriber))
				{
					using (GreetingBase greetingBase = ipaastore.OpenGreeting(paa))
					{
						if (greetingBase != null)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAManagerBase::LoadGreetingForPAA() Found greeting for PAA {0}", new object[]
							{
								paa.Identity
							});
							tempWavFile = greetingBase.Get();
						}
						else
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "PAAManagerBase::LoadGreetingForPAA() Did not find greeting for PAA {0}", new object[]
							{
								paa.Identity
							});
						}
					}
				}
			}
			catch (LocalizedException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Exception downloading PAA Greeting {0}", new object[]
				{
					ex
				});
			}
			catch (ObjectDisposedException ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Exception downloading PAA Greeting {0}", new object[]
				{
					ex2
				});
			}
			if (tempWavFile != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Downloaded greeting for PAA {0}", new object[]
				{
					paa.Identity
				});
				this.personalGreeting = tempWavFile;
				this.haveGreeting = true;
				return;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PersonalAutoAttendantTracer, this, "Could not download greeting for PAA {0}", new object[]
			{
				paa.Identity
			});
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000574C4 File Offset: 0x000556C4
		private void SetMenuItem(int key, string type, string context)
		{
			PAAMenuItem paamenuItem = this.paaMenuItems[key];
			paamenuItem.Enabled = true;
			paamenuItem.MenuType = type;
			paamenuItem.Context = context;
		}

		// Token: 0x04000C71 RID: 3185
		private bool haveGreeting;

		// Token: 0x04000C72 RID: 3186
		private bool transferToVoiceMessageEnabled;

		// Token: 0x04000C73 RID: 3187
		private object recordedName;

		// Token: 0x04000C74 RID: 3188
		private ITempWavFile personalGreeting;

		// Token: 0x04000C75 RID: 3189
		private PersonalAutoAttendant personalAutoAttendant;

		// Token: 0x04000C76 RID: 3190
		private UMSubscriber subscriber;

		// Token: 0x04000C77 RID: 3191
		private PAAMenuItem[] paaMenuItems;

		// Token: 0x04000C78 RID: 3192
		private bool mainMenuUninterruptible;

		// Token: 0x04000C79 RID: 3193
		private Dictionary<int, PAAManagerBase.PAAPresentationObject> menu = new Dictionary<int, PAAManagerBase.PAAPresentationObject>();

		// Token: 0x02000295 RID: 661
		internal abstract class PAAPresentationObject
		{
			// Token: 0x060013F7 RID: 5111 RVA: 0x000574EF File Offset: 0x000556EF
			protected PAAPresentationObject(KeyMappingBase menuItem)
			{
				this.configMenuItem = menuItem;
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x060013F8 RID: 5112 RVA: 0x000574FE File Offset: 0x000556FE
			protected int Key
			{
				get
				{
					return this.configMenuItem.Key;
				}
			}

			// Token: 0x170004FA RID: 1274
			// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0005750B File Offset: 0x0005570B
			protected string Context
			{
				get
				{
					return this.configMenuItem.Context;
				}
			}

			// Token: 0x170004FB RID: 1275
			// (get) Token: 0x060013FA RID: 5114 RVA: 0x00057518 File Offset: 0x00055718
			protected KeyMappingBase ConfigMenuItem
			{
				get
				{
					return this.configMenuItem;
				}
			}

			// Token: 0x060013FB RID: 5115 RVA: 0x00057520 File Offset: 0x00055720
			internal static PAAManagerBase.PAAPresentationObject Create(KeyMappingBase menuItem, UMSubscriber subscriber)
			{
				PAAManagerBase.PAAPresentationObject result = null;
				switch (menuItem.KeyMappingType)
				{
				case KeyMappingTypeEnum.TransferToNumber:
					result = new PAAManagerBase.TransferToNumberUI((TransferToNumber)menuItem);
					break;
				case KeyMappingTypeEnum.TransferToADContactMailbox:
					result = new PAAManagerBase.TransferToADContactMailboxUI((TransferToADContactMailbox)menuItem, subscriber);
					break;
				case KeyMappingTypeEnum.TransferToADContactPhone:
					result = new PAAManagerBase.TransferToADContactPhoneUI((TransferToADContactPhone)menuItem, subscriber);
					break;
				case KeyMappingTypeEnum.TransferToVoicemail:
					result = new PAAManagerBase.TransferToVoiceMailUI((TransferToVoicemail)menuItem);
					break;
				case KeyMappingTypeEnum.FindMe:
					result = new PAAManagerBase.TransferToFindMeUI((TransferToFindMe)menuItem);
					break;
				}
				return result;
			}

			// Token: 0x060013FC RID: 5116 RVA: 0x0005759B File Offset: 0x0005579B
			internal virtual void SetVariablesForMainMenu(IPAAUI manager)
			{
			}

			// Token: 0x060013FD RID: 5117 RVA: 0x0005759D File Offset: 0x0005579D
			internal virtual void SetVariablesForTransfer(IPAAUI manager)
			{
			}

			// Token: 0x04000C7A RID: 3194
			private KeyMappingBase configMenuItem;
		}

		// Token: 0x02000296 RID: 662
		internal class TransferToNumberUI : PAAManagerBase.PAAPresentationObject
		{
			// Token: 0x060013FE RID: 5118 RVA: 0x0005759F File Offset: 0x0005579F
			internal TransferToNumberUI(TransferToNumber menuItem) : base(menuItem)
			{
			}

			// Token: 0x060013FF RID: 5119 RVA: 0x000575A8 File Offset: 0x000557A8
			internal override void SetVariablesForMainMenu(IPAAUI manager)
			{
				TransferToNumber transferToNumber = base.ConfigMenuItem as TransferToNumber;
				manager.SetPhoneNumberTransferMenuItem(base.Key, base.ConfigMenuItem.KeyMappingType.ToString(), base.Context, transferToNumber.PhoneNumberString);
			}

			// Token: 0x06001400 RID: 5120 RVA: 0x000575F0 File Offset: 0x000557F0
			internal override void SetVariablesForTransfer(IPAAUI manager)
			{
				TransferToNumber transferToNumber = (TransferToNumber)base.ConfigMenuItem;
				manager.SetBlindTransferEnabled(false, null);
				if (transferToNumber.ValidationResult == PAAValidationResult.Valid)
				{
					IPhoneNumberTarget phoneNumberTarget = transferToNumber;
					PhoneNumber dialableNumber = phoneNumberTarget.GetDialableNumber();
					manager.SetBlindTransferEnabled(true, dialableNumber);
					return;
				}
				if (transferToNumber.ValidationResult == PAAValidationResult.PermissionCheckFailure)
				{
					manager.SetPermissionCheckFailure();
					return;
				}
				manager.SetPermissionCheckFailure();
			}
		}

		// Token: 0x02000297 RID: 663
		internal abstract class TransferToADContactUI : PAAManagerBase.PAAPresentationObject
		{
			// Token: 0x06001401 RID: 5121 RVA: 0x00057644 File Offset: 0x00055844
			internal TransferToADContactUI(TransferToADContact menuItem, UMSubscriber subscriber) : base(menuItem)
			{
				this.legacyExchangeDN = menuItem.LegacyExchangeDN;
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(subscriber);
				this.recipient = iadrecipientLookup.LookupByLegacyExchangeDN(this.legacyExchangeDN);
			}

			// Token: 0x170004FC RID: 1276
			// (get) Token: 0x06001402 RID: 5122 RVA: 0x0005767D File Offset: 0x0005587D
			protected ADRecipient Recipient
			{
				get
				{
					return this.recipient;
				}
			}

			// Token: 0x170004FD RID: 1277
			// (get) Token: 0x06001403 RID: 5123 RVA: 0x00057685 File Offset: 0x00055885
			protected string LegacyExchangeDN
			{
				get
				{
					return this.legacyExchangeDN;
				}
			}

			// Token: 0x06001404 RID: 5124 RVA: 0x0005768D File Offset: 0x0005588D
			internal override void SetVariablesForMainMenu(IPAAUI manager)
			{
				manager.SetADTransferTargetMenuItem(base.Key, base.ConfigMenuItem.KeyMappingType.ToString(), base.ConfigMenuItem.Context, this.legacyExchangeDN, this.Recipient);
			}

			// Token: 0x04000C7B RID: 3195
			private ADRecipient recipient;

			// Token: 0x04000C7C RID: 3196
			private string legacyExchangeDN;
		}

		// Token: 0x02000298 RID: 664
		internal class TransferToADContactPhoneUI : PAAManagerBase.TransferToADContactUI
		{
			// Token: 0x06001405 RID: 5125 RVA: 0x000576C7 File Offset: 0x000558C7
			internal TransferToADContactPhoneUI(TransferToADContactPhone menuItem, UMSubscriber subscriber) : base(menuItem, subscriber)
			{
			}

			// Token: 0x06001406 RID: 5126 RVA: 0x000576D4 File Offset: 0x000558D4
			internal override void SetVariablesForTransfer(IPAAUI manager)
			{
				manager.SetBlindTransferEnabled(false, null);
				if (base.ConfigMenuItem.ValidationResult == PAAValidationResult.Valid)
				{
					IPhoneNumberTarget phoneNumberTarget = (IPhoneNumberTarget)base.ConfigMenuItem;
					PhoneNumber dialableNumber = phoneNumberTarget.GetDialableNumber();
					manager.SetBlindTransferEnabled(true, dialableNumber);
					return;
				}
				if (base.ConfigMenuItem.ValidationResult == PAAValidationResult.PermissionCheckFailure)
				{
					manager.SetPermissionCheckFailure();
					return;
				}
				manager.SetInvalidADContact();
			}
		}

		// Token: 0x02000299 RID: 665
		internal class TransferToADContactMailboxUI : PAAManagerBase.TransferToADContactUI
		{
			// Token: 0x06001407 RID: 5127 RVA: 0x0005772D File Offset: 0x0005592D
			internal TransferToADContactMailboxUI(TransferToADContactMailbox menuItem, UMSubscriber subscriber) : base(menuItem, subscriber)
			{
			}

			// Token: 0x06001408 RID: 5128 RVA: 0x00057737 File Offset: 0x00055937
			internal override void SetVariablesForTransfer(IPAAUI manager)
			{
				if (base.Recipient != null)
				{
					manager.SetADTransferTarget(base.Recipient);
					manager.SetTransferToMailboxEnabled();
					return;
				}
				manager.SetInvalidADContact();
			}
		}

		// Token: 0x0200029A RID: 666
		internal class TransferToVoiceMailUI : PAAManagerBase.PAAPresentationObject
		{
			// Token: 0x06001409 RID: 5129 RVA: 0x0005775A File Offset: 0x0005595A
			internal TransferToVoiceMailUI(TransferToVoicemail menuItem) : base(menuItem)
			{
			}

			// Token: 0x0600140A RID: 5130 RVA: 0x00057763 File Offset: 0x00055963
			internal override void SetVariablesForMainMenu(IPAAUI manager)
			{
				manager.SetMenuItemTransferToVoiceMail();
			}

			// Token: 0x0600140B RID: 5131 RVA: 0x0005776B File Offset: 0x0005596B
			internal override void SetVariablesForTransfer(IPAAUI manager)
			{
				manager.SetTransferToVoiceMessageEnabled();
			}
		}

		// Token: 0x0200029B RID: 667
		internal class TransferToFindMeUI : PAAManagerBase.PAAPresentationObject
		{
			// Token: 0x0600140C RID: 5132 RVA: 0x00057773 File Offset: 0x00055973
			internal TransferToFindMeUI(TransferToFindMe menuItem) : base(menuItem)
			{
			}

			// Token: 0x0600140D RID: 5133 RVA: 0x0005777C File Offset: 0x0005597C
			internal override void SetVariablesForMainMenu(IPAAUI manager)
			{
				KeyMappingBase configMenuItem = base.ConfigMenuItem;
				manager.SetFindMeMenuItem(base.Key, base.ConfigMenuItem.KeyMappingType.ToString(), base.Context);
			}

			// Token: 0x0600140E RID: 5134 RVA: 0x000577AC File Offset: 0x000559AC
			internal override void SetVariablesForTransfer(IPAAUI manager)
			{
				TransferToFindMe transferToFindMe = base.ConfigMenuItem as TransferToFindMe;
				manager.SetFindMeNumbers(transferToFindMe.Numbers.NumberList);
			}
		}
	}
}
