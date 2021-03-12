using System;
using System.Threading;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000114 RID: 276
	internal abstract class GlsProperty
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x00035580 File Offset: 0x00033780
		protected GlsProperty(string name, Type dataType, object defaultValue)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentNullException("name");
			}
			if (dataType == null)
			{
				throw new ArgumentNullException("dataType");
			}
			if (defaultValue != null && defaultValue.GetType() != dataType)
			{
				throw new ArgumentException(string.Format("Incompatible type for default Value, expected:{0}, actual:{1}", dataType, defaultValue.GetType()));
			}
			if (defaultValue == null && dataType.IsValueType)
			{
				defaultValue = Activator.CreateInstance(dataType);
			}
			this.name = name;
			this.dataType = dataType;
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0003560A File Offset: 0x0003380A
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00035612 File Offset: 0x00033812
		internal Type DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0003561A File Offset: 0x0003381A
		internal object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x00035622 File Offset: 0x00033822
		internal static string ExoPrefix
		{
			get
			{
				if (GlsCallerId.GLSEnvironment != GlsEnvironmentType.Gallatin)
				{
					return "EXO";
				}
				return "EXO-CN";
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00035637 File Offset: 0x00033837
		internal static string FfoPrefix
		{
			get
			{
				if (GlsCallerId.GLSEnvironment != GlsEnvironmentType.Gallatin)
				{
					return "FFO";
				}
				return "FFO-CN";
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0003564C File Offset: 0x0003384C
		internal static string GlobalPrefix
		{
			get
			{
				return "Global";
			}
		}

		// Token: 0x040005D2 RID: 1490
		protected static readonly Lazy<GlsEnvironmentType> glsEnvironmentType = new Lazy<GlsEnvironmentType>(() => RegistrySettings.ExchangeServerCurrentVersion.GlsEnvironmentType, LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x040005D3 RID: 1491
		private readonly string name;

		// Token: 0x040005D4 RID: 1492
		private readonly Type dataType;

		// Token: 0x040005D5 RID: 1493
		private readonly object defaultValue;
	}
}
