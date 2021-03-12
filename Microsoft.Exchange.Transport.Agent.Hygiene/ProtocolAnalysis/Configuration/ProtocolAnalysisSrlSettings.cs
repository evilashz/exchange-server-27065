using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Agent.Hygiene;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Configuration
{
	// Token: 0x02000045 RID: 69
	[Serializable]
	internal class ProtocolAnalysisSrlSettings
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000D8A3 File Offset: 0x0000BAA3
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000D8AB File Offset: 0x0000BAAB
		public virtual PropertyBag Fields
		{
			get
			{
				return this.fields;
			}
			set
			{
				this.fields = value;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		public ProtocolAnalysisSrlSettings()
		{
			this.fields = new PropertyBag(75);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		public void InitializeDefaults()
		{
			this.ConfigurationVersion = "1";
			this.LogNumMsgs = -0.035;
			this.ZombNdom = -0.078;
			this.ZombNip = 0.509;
			this.ZombNsegs = 1.532;
			this.NullRdns = 2.645;
			this.LogHeloEmpty = 0.0;
			this.LogNormNmsgNumUniqHelo = -0.537;
			this.LogHeloMatchAll = -0.818;
			this.LogHeloMatch2nd = -1.88;
			this.LogHeloMatchLocal = 0.0;
			this.HeloNoMatch = 0.177;
			this.LogLen0 = 0.0;
			this.LogLen1 = 0.0;
			this.LogLen2 = 0.0;
			this.LogLen3 = 0.0;
			this.LogLen4 = 0.0;
			this.LogLen5 = 0.0;
			this.LogLen6 = 0.0;
			this.LogLen7 = 0.0;
			this.LogLen8 = 0.0;
			this.LogLen9 = 0.0;
			this.LogLen10 = 0.0;
			this.LogLen11 = 0.0;
			this.LogLen12 = 0.0;
			this.LogLen13 = 0.0;
			this.LogLen14 = 0.0;
			this.LogNormNmsgNumValid = -0.054;
			this.LogNormNmsgNumInvalid = 0.0;
			this.LogNormNmsgNumUniqValid = 0.057;
			this.LogNormNmsgNumUniqInvalid = 0.0;
			this.LogValidScl0 = -6.788;
			this.LogValidScl1 = -3.237;
			this.LogValidScl2 = -2.086;
			this.LogValidScl3 = -1.303;
			this.LogValidScl4 = -0.106;
			this.LogValidScl5 = 0.447;
			this.LogValidScl6 = 0.988;
			this.LogValidScl7 = 1.899;
			this.LogValidScl8 = 5.445;
			this.LogValidScl9 = 6.347;
			this.LogInvalidScl0 = 0.0;
			this.LogInvalidScl1 = 0.0;
			this.LogInvalidScl2 = 0.0;
			this.LogInvalidScl3 = 0.0;
			this.LogInvalidScl4 = 0.0;
			this.LogInvalidScl5 = 0.0;
			this.LogInvalidScl6 = 0.0;
			this.LogInvalidScl7 = 0.0;
			this.LogInvalidScl8 = 0.0;
			this.LogInvalidScl9 = 0.0;
			this.LogCallIdValid = 0.0;
			this.LogCallIdInvalid = 0.0;
			this.LogCallIdIndeterminate = 0.0;
			this.LogCallIdEpderror = 0.0;
			this.LogCallIdError = 0.0;
			this.LogCallIdNull = 0.0;
			this.Bias = -0.018;
			this.FeatureThreshold0 = -0.933;
			this.FeatureThreshold1 = -0.069;
			this.FeatureThreshold2 = 0.59;
			this.FeatureThreshold3 = 1.276;
			this.FeatureThreshold4 = 1.654;
			this.FeatureThreshold5 = 2.704;
			this.FeatureThreshold6 = 3.597;
			this.FeatureThreshold7 = 4.304;
			this.FeatureThreshold8 = 5.228;
			this.InitWinLen = 100;
			this.MinWinLen = 20;
			this.MaxWinLen = 500;
			this.WinlenShrinkFactor = 0.25;
			this.WinlenExpandFactor = 4.0;
			this.GoodBehaviorPeriod = 100;
			this.ZombieKeywords = new string[]
			{
				"dsl",
				"client",
				"pool",
				"modem",
				"cable",
				"ppp",
				"dialup",
				"dhcp"
			};
			this.RecommendedDownloadIntervalInMinutes = 30.0;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000DD68 File Offset: 0x0000BF68
		public double LogValidScl(int scl)
		{
			switch (scl)
			{
			case 0:
				return this.LogValidScl0;
			case 1:
				return this.LogValidScl1;
			case 2:
				return this.LogValidScl2;
			case 3:
				return this.LogValidScl3;
			case 4:
				return this.LogValidScl4;
			case 5:
				return this.LogValidScl5;
			case 6:
				return this.LogValidScl6;
			case 7:
				return this.LogValidScl7;
			case 8:
				return this.LogValidScl8;
			case 9:
				return this.LogValidScl9;
			default:
				throw new LocalizedException(AgentStrings.InvalidSclLevel(scl));
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		public double LogInvalidScl(int scl)
		{
			switch (scl)
			{
			case 0:
				return this.LogInvalidScl0;
			case 1:
				return this.LogInvalidScl1;
			case 2:
				return this.LogInvalidScl2;
			case 3:
				return this.LogInvalidScl3;
			case 4:
				return this.LogInvalidScl4;
			case 5:
				return this.LogInvalidScl5;
			case 6:
				return this.LogInvalidScl6;
			case 7:
				return this.LogInvalidScl7;
			case 8:
				return this.LogInvalidScl8;
			case 9:
				return this.LogInvalidScl9;
			default:
				throw new LocalizedException(AgentStrings.InvalidSclLevel(scl));
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000DE88 File Offset: 0x0000C088
		public double LogLength(int length)
		{
			switch (length)
			{
			case 0:
				return this.LogLen0;
			case 1:
				return this.LogLen1;
			case 2:
				return this.LogLen2;
			case 3:
				return this.LogLen3;
			case 4:
				return this.LogLen4;
			case 5:
				return this.LogLen5;
			case 6:
				return this.LogLen6;
			case 7:
				return this.LogLen7;
			case 8:
				return this.LogLen8;
			case 9:
				return this.LogLen9;
			case 10:
				return this.LogLen10;
			case 11:
				return this.LogLen11;
			case 12:
				return this.LogLen12;
			case 13:
				return this.LogLen13;
			case 14:
				return this.LogLen14;
			default:
				throw new LocalizedException(AgentStrings.InvalidLogLength(length));
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000DF50 File Offset: 0x0000C150
		public double FeatureThresholds(int srl)
		{
			switch (srl)
			{
			case 0:
				return this.FeatureThreshold0;
			case 1:
				return this.FeatureThreshold1;
			case 2:
				return this.FeatureThreshold2;
			case 3:
				return this.FeatureThreshold3;
			case 4:
				return this.FeatureThreshold4;
			case 5:
				return this.FeatureThreshold5;
			case 6:
				return this.FeatureThreshold6;
			case 7:
				return this.FeatureThreshold7;
			case 8:
				return this.FeatureThreshold8;
			default:
				throw new LocalizedException(AgentStrings.InvalidFeatureThreshold(srl));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000DFD5 File Offset: 0x0000C1D5
		public int MaxFeatureThreshold
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000DFD9 File Offset: 0x0000C1D9
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000DFF0 File Offset: 0x0000C1F0
		public string ConfigurationVersion
		{
			get
			{
				return (string)this.Fields["ConfigurationVersion"];
			}
			set
			{
				this.Fields["ConfigurationVersion"] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000E003 File Offset: 0x0000C203
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000E01A File Offset: 0x0000C21A
		public double LogNumMsgs
		{
			get
			{
				return (double)this.Fields["LogNumMsgs"];
			}
			set
			{
				this.Fields["LogNumMsgs"] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000E032 File Offset: 0x0000C232
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000E049 File Offset: 0x0000C249
		public double ZombNdom
		{
			get
			{
				return (double)this.Fields["ZombNdom"];
			}
			set
			{
				this.Fields["ZombNdom"] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000E061 File Offset: 0x0000C261
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000E078 File Offset: 0x0000C278
		public double ZombNip
		{
			get
			{
				return (double)this.Fields["ZombNip"];
			}
			set
			{
				this.Fields["ZombNip"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000E090 File Offset: 0x0000C290
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000E0A7 File Offset: 0x0000C2A7
		public double ZombNsegs
		{
			get
			{
				return (double)this.Fields["ZombNsegs"];
			}
			set
			{
				this.Fields["ZombNsegs"] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000E0BF File Offset: 0x0000C2BF
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000E0D6 File Offset: 0x0000C2D6
		public double NullRdns
		{
			get
			{
				return (double)this.Fields["NullRdns"];
			}
			set
			{
				this.Fields["NullRdns"] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000E0EE File Offset: 0x0000C2EE
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000E105 File Offset: 0x0000C305
		public double LogHeloEmpty
		{
			get
			{
				return (double)this.Fields["LogHeloEmpty"];
			}
			set
			{
				this.Fields["LogHeloEmpty"] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000E11D File Offset: 0x0000C31D
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000E134 File Offset: 0x0000C334
		public double LogNormNmsgNumUniqHelo
		{
			get
			{
				return (double)this.Fields["LogNormNmsgNumUniqHelo"];
			}
			set
			{
				this.Fields["LogNormNmsgNumUniqHelo"] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000E14C File Offset: 0x0000C34C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000E163 File Offset: 0x0000C363
		public double LogHeloMatchAll
		{
			get
			{
				return (double)this.Fields["LogHeloMatchAll"];
			}
			set
			{
				this.Fields["LogHeloMatchAll"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000E17B File Offset: 0x0000C37B
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000E192 File Offset: 0x0000C392
		public double LogHeloMatch2nd
		{
			get
			{
				return (double)this.Fields["LogHeloMatch2nd"];
			}
			set
			{
				this.Fields["LogHeloMatch2nd"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000E1AA File Offset: 0x0000C3AA
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000E1C1 File Offset: 0x0000C3C1
		public double LogHeloMatchLocal
		{
			get
			{
				return (double)this.Fields["LogHeloMatchLocal"];
			}
			set
			{
				this.Fields["LogHeloMatchLocal"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000E1D9 File Offset: 0x0000C3D9
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public double HeloNoMatch
		{
			get
			{
				return (double)this.Fields["HeloNoMatch"];
			}
			set
			{
				this.Fields["HeloNoMatch"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000E208 File Offset: 0x0000C408
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x0000E21F File Offset: 0x0000C41F
		public double LogLen0
		{
			get
			{
				return (double)this.Fields["LogLen0"];
			}
			set
			{
				this.Fields["LogLen0"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000E237 File Offset: 0x0000C437
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000E24E File Offset: 0x0000C44E
		public double LogLen1
		{
			get
			{
				return (double)this.Fields["LogLen1"];
			}
			set
			{
				this.Fields["LogLen1"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001BB RID: 443 RVA: 0x0000E266 File Offset: 0x0000C466
		// (set) Token: 0x060001BC RID: 444 RVA: 0x0000E27D File Offset: 0x0000C47D
		public double LogLen2
		{
			get
			{
				return (double)this.Fields["LogLen2"];
			}
			set
			{
				this.Fields["LogLen2"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000E295 File Offset: 0x0000C495
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		public double LogLen3
		{
			get
			{
				return (double)this.Fields["LogLen3"];
			}
			set
			{
				this.Fields["LogLen3"] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000E2C4 File Offset: 0x0000C4C4
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000E2DB File Offset: 0x0000C4DB
		public double LogLen4
		{
			get
			{
				return (double)this.Fields["LogLen4"];
			}
			set
			{
				this.Fields["LogLen4"] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000E2F3 File Offset: 0x0000C4F3
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000E30A File Offset: 0x0000C50A
		public double LogLen5
		{
			get
			{
				return (double)this.Fields["LogLen5"];
			}
			set
			{
				this.Fields["LogLen5"] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000E322 File Offset: 0x0000C522
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000E339 File Offset: 0x0000C539
		public double LogLen6
		{
			get
			{
				return (double)this.Fields["LogLen6"];
			}
			set
			{
				this.Fields["LogLen6"] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000E351 File Offset: 0x0000C551
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x0000E368 File Offset: 0x0000C568
		public double LogLen7
		{
			get
			{
				return (double)this.Fields["LogLen7"];
			}
			set
			{
				this.Fields["LogLen7"] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000E380 File Offset: 0x0000C580
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000E397 File Offset: 0x0000C597
		public double LogLen8
		{
			get
			{
				return (double)this.Fields["LogLen8"];
			}
			set
			{
				this.Fields["LogLen8"] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000E3AF File Offset: 0x0000C5AF
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000E3C6 File Offset: 0x0000C5C6
		public double LogLen9
		{
			get
			{
				return (double)this.Fields["LogLen9"];
			}
			set
			{
				this.Fields["LogLen9"] = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000E3F5 File Offset: 0x0000C5F5
		public double LogLen10
		{
			get
			{
				return (double)this.Fields["LogLen10"];
			}
			set
			{
				this.Fields["LogLen10"] = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000E40D File Offset: 0x0000C60D
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000E424 File Offset: 0x0000C624
		public double LogLen11
		{
			get
			{
				return (double)this.Fields["LogLen11"];
			}
			set
			{
				this.Fields["LogLen11"] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000E43C File Offset: 0x0000C63C
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000E453 File Offset: 0x0000C653
		public double LogLen12
		{
			get
			{
				return (double)this.Fields["LogLen12"];
			}
			set
			{
				this.Fields["LogLen12"] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000E46B File Offset: 0x0000C66B
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000E482 File Offset: 0x0000C682
		public double LogLen13
		{
			get
			{
				return (double)this.Fields["LogLen13"];
			}
			set
			{
				this.Fields["LogLen13"] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000E49A File Offset: 0x0000C69A
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000E4B1 File Offset: 0x0000C6B1
		public double LogLen14
		{
			get
			{
				return (double)this.Fields["LogLen14"];
			}
			set
			{
				this.Fields["LogLen14"] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000E4C9 File Offset: 0x0000C6C9
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000E4E0 File Offset: 0x0000C6E0
		public double LogNormNmsgNumValid
		{
			get
			{
				return (double)this.Fields["LogNormNmsgNumValid"];
			}
			set
			{
				this.Fields["LogNormNmsgNumValid"] = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000E50F File Offset: 0x0000C70F
		public double LogNormNmsgNumInvalid
		{
			get
			{
				return (double)this.Fields["LogNormNmsgNumInvalid"];
			}
			set
			{
				this.Fields["LogNormNmsgNumInvalid"] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000E527 File Offset: 0x0000C727
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000E53E File Offset: 0x0000C73E
		public double LogNormNmsgNumUniqValid
		{
			get
			{
				return (double)this.Fields["LogNormNmsgNumUniqValid"];
			}
			set
			{
				this.Fields["LogNormNmsgNumUniqValid"] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000E556 File Offset: 0x0000C756
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000E56D File Offset: 0x0000C76D
		public double LogNormNmsgNumUniqInvalid
		{
			get
			{
				return (double)this.Fields["LogNormNmsgNumUniqInvalid"];
			}
			set
			{
				this.Fields["LogNormNmsgNumUniqInvalid"] = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000E585 File Offset: 0x0000C785
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000E59C File Offset: 0x0000C79C
		public double LogValidScl0
		{
			get
			{
				return (double)this.Fields["LogValidScl0"];
			}
			set
			{
				this.Fields["LogValidScl0"] = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000E5B4 File Offset: 0x0000C7B4
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000E5CB File Offset: 0x0000C7CB
		public double LogValidScl1
		{
			get
			{
				return (double)this.Fields["LogValidScl1"];
			}
			set
			{
				this.Fields["LogValidScl1"] = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000E5E3 File Offset: 0x0000C7E3
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000E5FA File Offset: 0x0000C7FA
		public double LogValidScl2
		{
			get
			{
				return (double)this.Fields["LogValidScl2"];
			}
			set
			{
				this.Fields["LogValidScl2"] = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000E612 File Offset: 0x0000C812
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000E629 File Offset: 0x0000C829
		public double LogValidScl3
		{
			get
			{
				return (double)this.Fields["LogValidScl3"];
			}
			set
			{
				this.Fields["LogValidScl3"] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000E641 File Offset: 0x0000C841
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000E658 File Offset: 0x0000C858
		public double LogValidScl4
		{
			get
			{
				return (double)this.Fields["LogValidScl4"];
			}
			set
			{
				this.Fields["LogValidScl4"] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000E670 File Offset: 0x0000C870
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000E687 File Offset: 0x0000C887
		public double LogValidScl5
		{
			get
			{
				return (double)this.Fields["LogValidScl5"];
			}
			set
			{
				this.Fields["LogValidScl5"] = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000E69F File Offset: 0x0000C89F
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000E6B6 File Offset: 0x0000C8B6
		public double LogValidScl6
		{
			get
			{
				return (double)this.Fields["LogValidScl6"];
			}
			set
			{
				this.Fields["LogValidScl6"] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000E6CE File Offset: 0x0000C8CE
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000E6E5 File Offset: 0x0000C8E5
		public double LogValidScl7
		{
			get
			{
				return (double)this.Fields["LogValidScl7"];
			}
			set
			{
				this.Fields["LogValidScl7"] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000E6FD File Offset: 0x0000C8FD
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000E714 File Offset: 0x0000C914
		public double LogValidScl8
		{
			get
			{
				return (double)this.Fields["LogValidScl8"];
			}
			set
			{
				this.Fields["LogValidScl8"] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000E72C File Offset: 0x0000C92C
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000E743 File Offset: 0x0000C943
		public double LogValidScl9
		{
			get
			{
				return (double)this.Fields["LogValidScl9"];
			}
			set
			{
				this.Fields["LogValidScl9"] = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000E75B File Offset: 0x0000C95B
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000E772 File Offset: 0x0000C972
		public double LogInvalidScl0
		{
			get
			{
				return (double)this.Fields["LogInvalidScl0"];
			}
			set
			{
				this.Fields["LogInvalidScl0"] = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000E78A File Offset: 0x0000C98A
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000E7A1 File Offset: 0x0000C9A1
		public double LogInvalidScl1
		{
			get
			{
				return (double)this.Fields["LogInvalidScl1"];
			}
			set
			{
				this.Fields["LogInvalidScl1"] = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000E7B9 File Offset: 0x0000C9B9
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		public double LogInvalidScl2
		{
			get
			{
				return (double)this.Fields["LogInvalidScl2"];
			}
			set
			{
				this.Fields["LogInvalidScl2"] = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000E7FF File Offset: 0x0000C9FF
		public double LogInvalidScl3
		{
			get
			{
				return (double)this.Fields["LogInvalidScl3"];
			}
			set
			{
				this.Fields["LogInvalidScl3"] = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000E817 File Offset: 0x0000CA17
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000E82E File Offset: 0x0000CA2E
		public double LogInvalidScl4
		{
			get
			{
				return (double)this.Fields["LogInvalidScl4"];
			}
			set
			{
				this.Fields["LogInvalidScl4"] = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000E846 File Offset: 0x0000CA46
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000E85D File Offset: 0x0000CA5D
		public double LogInvalidScl5
		{
			get
			{
				return (double)this.Fields["LogInvalidScl5"];
			}
			set
			{
				this.Fields["LogInvalidScl5"] = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000E875 File Offset: 0x0000CA75
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000E88C File Offset: 0x0000CA8C
		public double LogInvalidScl6
		{
			get
			{
				return (double)this.Fields["LogInvalidScl6"];
			}
			set
			{
				this.Fields["LogInvalidScl6"] = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000E8BB File Offset: 0x0000CABB
		public double LogInvalidScl7
		{
			get
			{
				return (double)this.Fields["LogInvalidScl7"];
			}
			set
			{
				this.Fields["LogInvalidScl7"] = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000E8D3 File Offset: 0x0000CAD3
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000E8EA File Offset: 0x0000CAEA
		public double LogInvalidScl8
		{
			get
			{
				return (double)this.Fields["LogInvalidScl8"];
			}
			set
			{
				this.Fields["LogInvalidScl8"] = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000E902 File Offset: 0x0000CB02
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000E919 File Offset: 0x0000CB19
		public double LogInvalidScl9
		{
			get
			{
				return (double)this.Fields["LogInvalidScl9"];
			}
			set
			{
				this.Fields["LogInvalidScl9"] = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000E931 File Offset: 0x0000CB31
		// (set) Token: 0x06000206 RID: 518 RVA: 0x0000E948 File Offset: 0x0000CB48
		public double LogCallIdValid
		{
			get
			{
				return (double)this.Fields["LogCallIdValid"];
			}
			set
			{
				this.Fields["LogCallIdValid"] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000E960 File Offset: 0x0000CB60
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000E977 File Offset: 0x0000CB77
		public double LogCallIdInvalid
		{
			get
			{
				return (double)this.Fields["LogCallIdInvalid"];
			}
			set
			{
				this.Fields["LogCallIdInvalid"] = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000E98F File Offset: 0x0000CB8F
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		public double LogCallIdIndeterminate
		{
			get
			{
				return (double)this.Fields["LogCallIdIndeterminate"];
			}
			set
			{
				this.Fields["LogCallIdIndeterminate"] = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000E9BE File Offset: 0x0000CBBE
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000E9D5 File Offset: 0x0000CBD5
		public double LogCallIdEpderror
		{
			get
			{
				return (double)this.Fields["LogCallIdEpderror"];
			}
			set
			{
				this.Fields["LogCallIdEpderror"] = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000E9ED File Offset: 0x0000CBED
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public double LogCallIdError
		{
			get
			{
				return (double)this.Fields["LogCallIdError"];
			}
			set
			{
				this.Fields["LogCallIdError"] = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000EA33 File Offset: 0x0000CC33
		public double LogCallIdNull
		{
			get
			{
				return (double)this.Fields["LogCallIdNull"];
			}
			set
			{
				this.Fields["LogCallIdNull"] = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000EA4B File Offset: 0x0000CC4B
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000EA62 File Offset: 0x0000CC62
		public double Bias
		{
			get
			{
				return (double)this.Fields["Bias"];
			}
			set
			{
				this.Fields["Bias"] = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000EA7A File Offset: 0x0000CC7A
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000EA91 File Offset: 0x0000CC91
		public double FeatureThreshold0
		{
			get
			{
				return (double)this.Fields["FeatureThreshold0"];
			}
			set
			{
				this.Fields["FeatureThreshold0"] = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000EAA9 File Offset: 0x0000CCA9
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		public double FeatureThreshold1
		{
			get
			{
				return (double)this.Fields["FeatureThreshold1"];
			}
			set
			{
				this.Fields["FeatureThreshold1"] = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000EAEF File Offset: 0x0000CCEF
		public double FeatureThreshold2
		{
			get
			{
				return (double)this.Fields["FeatureThreshold2"];
			}
			set
			{
				this.Fields["FeatureThreshold2"] = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000EB07 File Offset: 0x0000CD07
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000EB1E File Offset: 0x0000CD1E
		public double FeatureThreshold3
		{
			get
			{
				return (double)this.Fields["FeatureThreshold3"];
			}
			set
			{
				this.Fields["FeatureThreshold3"] = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000EB36 File Offset: 0x0000CD36
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000EB4D File Offset: 0x0000CD4D
		public double FeatureThreshold4
		{
			get
			{
				return (double)this.Fields["FeatureThreshold4"];
			}
			set
			{
				this.Fields["FeatureThreshold4"] = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000EB65 File Offset: 0x0000CD65
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		public double FeatureThreshold5
		{
			get
			{
				return (double)this.Fields["FeatureThreshold5"];
			}
			set
			{
				this.Fields["FeatureThreshold5"] = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000EB94 File Offset: 0x0000CD94
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000EBAB File Offset: 0x0000CDAB
		public double FeatureThreshold6
		{
			get
			{
				return (double)this.Fields["FeatureThreshold6"];
			}
			set
			{
				this.Fields["FeatureThreshold6"] = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000EBC3 File Offset: 0x0000CDC3
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000EBDA File Offset: 0x0000CDDA
		public double FeatureThreshold7
		{
			get
			{
				return (double)this.Fields["FeatureThreshold7"];
			}
			set
			{
				this.Fields["FeatureThreshold7"] = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000EBF2 File Offset: 0x0000CDF2
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000EC09 File Offset: 0x0000CE09
		public double FeatureThreshold8
		{
			get
			{
				return (double)this.Fields["FeatureThreshold8"];
			}
			set
			{
				this.Fields["FeatureThreshold8"] = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000EC21 File Offset: 0x0000CE21
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000EC38 File Offset: 0x0000CE38
		public int InitWinLen
		{
			get
			{
				return (int)this.Fields["InitWinLen"];
			}
			set
			{
				this.Fields["InitWinLen"] = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000EC50 File Offset: 0x0000CE50
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000EC67 File Offset: 0x0000CE67
		public int MinWinLen
		{
			get
			{
				return (int)this.Fields["MinWinLen"];
			}
			set
			{
				this.Fields["MinWinLen"] = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000EC7F File Offset: 0x0000CE7F
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000EC96 File Offset: 0x0000CE96
		public int MaxWinLen
		{
			get
			{
				return (int)this.Fields["MaxWinLen"];
			}
			set
			{
				this.Fields["MaxWinLen"] = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000ECAE File Offset: 0x0000CEAE
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000ECC5 File Offset: 0x0000CEC5
		public double WinlenShrinkFactor
		{
			get
			{
				return (double)this.Fields["WinlenShrinkFactor"];
			}
			set
			{
				this.Fields["WinlenShrinkFactor"] = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000ECDD File Offset: 0x0000CEDD
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public double WinlenExpandFactor
		{
			get
			{
				return (double)this.Fields["WinlenExpandFactor"];
			}
			set
			{
				this.Fields["WinlenExpandFactor"] = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000ED23 File Offset: 0x0000CF23
		public int GoodBehaviorPeriod
		{
			get
			{
				return (int)this.Fields["GoodBehaviorPeriod"];
			}
			set
			{
				this.Fields["GoodBehaviorPeriod"] = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000ED3B File Offset: 0x0000CF3B
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000ED52 File Offset: 0x0000CF52
		public string[] ZombieKeywords
		{
			get
			{
				return (string[])this.Fields["ZombieKeywords"];
			}
			set
			{
				this.Fields["ZombieKeywords"] = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000ED65 File Offset: 0x0000CF65
		// (set) Token: 0x06000234 RID: 564 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public double RecommendedDownloadIntervalInMinutes
		{
			get
			{
				return (double)this.Fields["RecommendedDownloadIntervalInMinutes"];
			}
			set
			{
				this.Fields["RecommendedDownloadIntervalInMinutes"] = value;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000ED94 File Offset: 0x0000CF94
		public bool StoreReputationServiceParams(FileStream stream, Trace tracer)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			bool result;
			try
			{
				xmlDocument.Load(stream);
				result = this.ParseXmlAndSetProperties(xmlDocument, tracer);
			}
			catch (XmlException ex)
			{
				tracer.TraceDebug<string>((long)this.GetHashCode(), "Failed to parse the XML downloaded from the web service. Error {0}", ex.Message);
				result = false;
			}
			catch
			{
				tracer.TraceDebug((long)this.GetHashCode(), "Failed to transfer the XML downloaded from the web service to the config object.");
				result = false;
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000EE0C File Offset: 0x0000D00C
		private bool ParseXmlAndSetProperties(XmlDocument doc, Trace tracer)
		{
			lock (this.syncObject)
			{
				XmlNode firstChild = doc.DocumentElement.FirstChild;
				if (firstChild == null)
				{
					tracer.TraceDebug<string>((long)this.GetHashCode(), "Failed to parse the XML downloaded from the web service: no {0} node.", "ProtocolAnalysisSettings");
					return false;
				}
				XmlNode xmlNode = null;
				PropertyBag propertyBag = new PropertyBag();
				StringBuilder stringBuilder = null;
				try
				{
					xmlNode = firstChild.FirstChild;
					while (xmlNode != null)
					{
						string name;
						if ((name = xmlNode.Name) == null)
						{
							goto IL_1EF;
						}
						if (<PrivateImplementationDetails>{E1E79CF8-70FE-4D9B-9874-2363DD37380A}.$$method0x6000218-1 == null)
						{
							<PrivateImplementationDetails>{E1E79CF8-70FE-4D9B-9874-2363DD37380A}.$$method0x6000218-1 = new Dictionary<string, int>(6)
							{
								{
									"ConfigurationVersion",
									0
								},
								{
									"ZombieKeywords",
									1
								},
								{
									"MinWinLen",
									2
								},
								{
									"MaxWinLen",
									3
								},
								{
									"GoodBehaviorPeriod",
									4
								},
								{
									"InitWinLen",
									5
								}
							};
						}
						int num;
						if (!<PrivateImplementationDetails>{E1E79CF8-70FE-4D9B-9874-2363DD37380A}.$$method0x6000218-1.TryGetValue(name, out num))
						{
							goto IL_1EF;
						}
						switch (num)
						{
						case 0:
							propertyBag[xmlNode.Name] = xmlNode.InnerText;
							Database.UpdateConfiguration(xmlNode.Name, xmlNode.InnerText, tracer);
							break;
						case 1:
						{
							ArrayList arrayList = new ArrayList();
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder();
							}
							else
							{
								stringBuilder.Clear();
							}
							for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
							{
								stringBuilder.Append(xmlNode2.InnerText);
								if (xmlNode2.NextSibling != null)
								{
									stringBuilder.Append(";");
								}
								arrayList.Add(xmlNode2.InnerText);
							}
							if (arrayList.Count != 0)
							{
								propertyBag[xmlNode.Name] = arrayList.ToArray();
								Database.UpdateConfiguration(xmlNode.Name, stringBuilder.ToString(), tracer);
							}
							break;
						}
						case 2:
						case 3:
						case 4:
						case 5:
							propertyBag[xmlNode.Name] = Convert.ToInt32(xmlNode.InnerText, CultureInfo.InvariantCulture);
							Database.UpdateConfiguration(xmlNode.Name, xmlNode.InnerText, tracer);
							break;
						default:
							goto IL_1EF;
						}
						IL_222:
						xmlNode = xmlNode.NextSibling;
						continue;
						IL_1EF:
						propertyBag[xmlNode.Name] = Convert.ToDouble(xmlNode.InnerText, CultureInfo.InvariantCulture);
						Database.UpdateConfiguration(xmlNode.Name, xmlNode.InnerText, tracer);
						goto IL_222;
					}
				}
				catch (Exception ex)
				{
					tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Failed to parse the XML downloaded from the web service: {0} node. Error: {1}", xmlNode.Name, ex.Message);
					return false;
				}
				this.Fields = propertyBag;
			}
			return true;
		}

		// Token: 0x04000180 RID: 384
		private object syncObject = new object();

		// Token: 0x04000181 RID: 385
		private PropertyBag fields;
	}
}
