using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200005D RID: 93
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceBandSettingsStorageDiagnosableArguments : LoadBalanceDiagnosableArgumentBase
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00009CDC File Offset: 0x00007EDC
		public bool ShowPersistedBands
		{
			get
			{
				return base.HasArgument("persistedBands");
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00009CE9 File Offset: 0x00007EE9
		public bool ShowActiveBands
		{
			get
			{
				return base.HasArgument("listBands");
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00009CF6 File Offset: 0x00007EF6
		public bool ProcessAction
		{
			get
			{
				return base.HasArgument("action");
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00009D03 File Offset: 0x00007F03
		public LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType Action
		{
			get
			{
				return (LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType)Enum.Parse(typeof(LoadBalanceBandSettingsStorageDiagnosableArguments.BandStorageActionType), base.GetArgument<string>("action"), true);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00009D25 File Offset: 0x00007F25
		public Band.BandProfile Profile
		{
			get
			{
				return (Band.BandProfile)Enum.Parse(typeof(Band.BandProfile), base.GetArgument<string>("profile"), true);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00009D47 File Offset: 0x00007F47
		public ByteQuantifiedSize MinSize
		{
			get
			{
				return ByteQuantifiedSize.Parse(base.GetArgument<string>("min"), ByteQuantifiedSize.Quantifier.MB);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009D5F File Offset: 0x00007F5F
		public ByteQuantifiedSize MaxSize
		{
			get
			{
				return ByteQuantifiedSize.Parse(base.GetArgument<string>("max"), ByteQuantifiedSize.Quantifier.MB);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00009D77 File Offset: 0x00007F77
		public bool Enabled
		{
			get
			{
				return base.HasArgument("enabled");
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00009D84 File Offset: 0x00007F84
		public TimeSpan? MaxLogonAge
		{
			get
			{
				if (!base.HasArgument("maxLogonAge"))
				{
					return null;
				}
				return new TimeSpan?(base.GetArgument<TimeSpan>("maxLogonAge"));
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public TimeSpan? MinLogonAge
		{
			get
			{
				if (!base.HasArgument("minLogonAge"))
				{
					return null;
				}
				return new TimeSpan?(base.GetArgument<TimeSpan>("minLogonAge"));
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00009DEC File Offset: 0x00007FEC
		public bool IncludePhysicalOnly
		{
			get
			{
				return base.HasArgument("onlyPhysicalMailboxes");
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00009DF9 File Offset: 0x00007FF9
		public double MailboxWeightFactor
		{
			get
			{
				return base.GetArgument<double>("mailboxSizeFactor");
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009E06 File Offset: 0x00008006
		public Band CreateBand()
		{
			return new Band(this.Profile, this.MinSize, this.MaxSize, this.MailboxWeightFactor, this.IncludePhysicalOnly, this.MinLogonAge, this.MaxLogonAge);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009E38 File Offset: 0x00008038
		protected override void ExtendSchema(Dictionary<string, Type> schema)
		{
			schema["persistedBands"] = typeof(bool);
			schema["listBands"] = typeof(bool);
			schema["action"] = typeof(string);
			schema["enabled"] = typeof(bool);
			schema["max"] = typeof(string);
			schema["min"] = typeof(string);
			schema["profile"] = typeof(string);
			schema["onlyPhysicalMailboxes"] = typeof(bool);
			schema["mailboxSizeFactor"] = typeof(double);
			schema["maxLogonAge"] = typeof(TimeSpan);
			schema["minLogonAge"] = typeof(TimeSpan);
		}

		// Token: 0x040000EF RID: 239
		private const string PersistedBands = "persistedBands";

		// Token: 0x040000F0 RID: 240
		private const string StorageAction = "action";

		// Token: 0x040000F1 RID: 241
		private const string BandProfile = "profile";

		// Token: 0x040000F2 RID: 242
		private const string BandMinSize = "min";

		// Token: 0x040000F3 RID: 243
		private const string BandMaxSize = "max";

		// Token: 0x040000F4 RID: 244
		private const string EnabledFlag = "enabled";

		// Token: 0x040000F5 RID: 245
		private const string ListBands = "listBands";

		// Token: 0x040000F6 RID: 246
		private const string BandMaxLogonAge = "maxLogonAge";

		// Token: 0x040000F7 RID: 247
		private const string BandMinLogonAge = "minLogonAge";

		// Token: 0x040000F8 RID: 248
		private const string BandIncludePhysicalMailboxesOnly = "onlyPhysicalMailboxes";

		// Token: 0x040000F9 RID: 249
		private const string BandMailboxSizeFactor = "mailboxSizeFactor";

		// Token: 0x0200005E RID: 94
		internal enum BandStorageActionType
		{
			// Token: 0x040000FB RID: 251
			Create,
			// Token: 0x040000FC RID: 252
			Remove,
			// Token: 0x040000FD RID: 253
			Enable,
			// Token: 0x040000FE RID: 254
			Disable
		}
	}
}
