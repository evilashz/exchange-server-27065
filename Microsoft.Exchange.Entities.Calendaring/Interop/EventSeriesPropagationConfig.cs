using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.Calendaring.Interop
{
	// Token: 0x02000063 RID: 99
	public class EventSeriesPropagationConfig : RegistryObject
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public EventSeriesPropagationConfig()
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009CEA File Offset: 0x00007EEA
		public EventSeriesPropagationConfig(RegistryObjectId identity) : base(identity)
		{
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009CF3 File Offset: 0x00007EF3
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00009D05 File Offset: 0x00007F05
		public TimeSpan MaxActionLifetime
		{
			get
			{
				return (TimeSpan)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.MaxActionLifetime];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.MaxActionLifetime] = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009D18 File Offset: 0x00007F18
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009D2A File Offset: 0x00007F2A
		public uint MaxCollisionRetryCount
		{
			get
			{
				return (uint)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.MaxCollisionRetryCount];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.MaxCollisionRetryCount] = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00009D3D File Offset: 0x00007F3D
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00009D4F File Offset: 0x00007F4F
		public uint PropagationCountLimit
		{
			get
			{
				return (uint)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.PropagationCountLimit];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.PropagationCountLimit] = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00009D62 File Offset: 0x00007F62
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00009D74 File Offset: 0x00007F74
		public TimeSpan PropagationTimeLimit
		{
			get
			{
				return (TimeSpan)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.PropagationTimeLimit];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.PropagationTimeLimit] = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00009D87 File Offset: 0x00007F87
		// (set) Token: 0x0600029A RID: 666 RVA: 0x00009D99 File Offset: 0x00007F99
		public bool ReversePropagationOrder
		{
			get
			{
				return (bool)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.ReversePropagationOrder];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.ReversePropagationOrder] = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00009DAC File Offset: 0x00007FAC
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00009DBE File Offset: 0x00007FBE
		public bool ShouldCleanup
		{
			get
			{
				return (bool)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.ShouldCleanup];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.ShouldCleanup] = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00009DD1 File Offset: 0x00007FD1
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00009DE3 File Offset: 0x00007FE3
		public bool IgnorePropagationErrors
		{
			get
			{
				return (bool)this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.IgnorePropagationErrors];
			}
			set
			{
				this[EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema.IgnorePropagationErrors] = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00009DF6 File Offset: 0x00007FF6
		internal override RegistryObjectSchema RegistrySchema
		{
			get
			{
				return EventSeriesPropagationConfig.Schema;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009DFD File Offset: 0x00007FFD
		public static EventSeriesPropagationConfig GetBackgroundPropagationConfig()
		{
			return EventSeriesPropagationConfig.GetConfig("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Calendaring\\Interop\\BackgroundEventSeriesPropagation", EventSeriesPropagationConfig.DefaultBackgroundPropagationConfig);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009E0E File Offset: 0x0000800E
		public static EventSeriesPropagationConfig GetInlinePropagationConfig()
		{
			return EventSeriesPropagationConfig.GetConfig("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Calendaring\\Interop\\InlineEventSeriesPropagation", EventSeriesPropagationConfig.DefaultInlinePropagationConfig);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009E20 File Offset: 0x00008020
		private static EventSeriesPropagationConfig GetConfig(string configObjectPath, EventSeriesPropagationConfig defaultValue)
		{
			RegistrySession registrySession = new RegistrySession(false);
			EventSeriesPropagationConfig[] array = registrySession.Find<EventSeriesPropagationConfig>(new RegistryObjectId(configObjectPath));
			if (array.Length == 0)
			{
				return defaultValue;
			}
			return array[0];
		}

		// Token: 0x040000B1 RID: 177
		public const string BackgroundEventSeriesPropagationSubKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Calendaring\\Interop\\BackgroundEventSeriesPropagation";

		// Token: 0x040000B2 RID: 178
		public const string CalendarInteropConfigurationRegistryKeyRoot = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Calendaring\\Interop";

		// Token: 0x040000B3 RID: 179
		public const string EventSeriesPropagationConfigName = "EventSeriesPropagationConfig";

		// Token: 0x040000B4 RID: 180
		public const string InlineEventSeriesPropagationSubKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Calendaring\\Interop\\InlineEventSeriesPropagation";

		// Token: 0x040000B5 RID: 181
		private static readonly EventSeriesPropagationConfig DefaultBackgroundPropagationConfig = new EventSeriesPropagationConfig
		{
			PropagationCountLimit = 50U,
			ReversePropagationOrder = true,
			ShouldCleanup = true,
			PropagationTimeLimit = TimeSpan.FromSeconds(20.0),
			IgnorePropagationErrors = false
		};

		// Token: 0x040000B6 RID: 182
		private static readonly EventSeriesPropagationConfig DefaultInlinePropagationConfig = new EventSeriesPropagationConfig
		{
			PropagationCountLimit = 10U,
			ReversePropagationOrder = false,
			ShouldCleanup = false,
			PropagationTimeLimit = TimeSpan.FromSeconds(10.0),
			IgnorePropagationErrors = true
		};

		// Token: 0x040000B7 RID: 183
		private static readonly EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema Schema = ObjectSchema.GetInstance<EventSeriesPropagationConfig.EventSeriesPropagationConfigSchema>();

		// Token: 0x02000064 RID: 100
		internal class EventSeriesPropagationConfigSchema : RegistryObjectSchema
		{
			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x00009EDD File Offset: 0x000080DD
			public override string DefaultName
			{
				get
				{
					return "EventSeriesPropagationConfig";
				}
			}

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x00009EE4 File Offset: 0x000080E4
			public override string DefaultRegistryKeyPath
			{
				get
				{
					return "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Calendaring\\Interop";
				}
			}

			// Token: 0x040000B8 RID: 184
			public static readonly RegistryPropertyDefinition MaxActionLifetime = new RegistryPropertyDefinition("MaxActionLifetime", typeof(TimeSpan), TimeSpan.FromHours(24.0));

			// Token: 0x040000B9 RID: 185
			public static readonly RegistryPropertyDefinition MaxCollisionRetryCount = new RegistryPropertyDefinition("MaxCollisionRetryCount", typeof(uint), 5U);

			// Token: 0x040000BA RID: 186
			public static readonly RegistryPropertyDefinition PropagationCountLimit = new RegistryPropertyDefinition("PropagationCountLimit", typeof(uint), 50U);

			// Token: 0x040000BB RID: 187
			public static readonly RegistryPropertyDefinition PropagationTimeLimit = new RegistryPropertyDefinition("PropagationTimeLimit", typeof(TimeSpan), TimeSpan.FromSeconds(10.0));

			// Token: 0x040000BC RID: 188
			public static readonly RegistryPropertyDefinition ReversePropagationOrder = new RegistryPropertyDefinition("ReversedPropagationOrder", typeof(bool), false);

			// Token: 0x040000BD RID: 189
			public static readonly RegistryPropertyDefinition ShouldCleanup = new RegistryPropertyDefinition("ShouldCleanup", typeof(bool), true);

			// Token: 0x040000BE RID: 190
			public static readonly RegistryPropertyDefinition IgnorePropagationErrors = new RegistryPropertyDefinition("IgnorePropagationErrors", typeof(bool), true);
		}
	}
}
