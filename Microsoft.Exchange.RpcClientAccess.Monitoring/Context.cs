using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Context : IContext
	{
		// Token: 0x0600014D RID: 333 RVA: 0x0000536F File Offset: 0x0000356F
		private Context(IEnvironment environment, ILogger logger, PropertyBag properties, Context rootContext)
		{
			Util.ThrowOnNullArgument(environment, "environment");
			Util.ThrowOnNullArgument(logger, "logger");
			this.environment = environment;
			this.logger = logger;
			this.properties = properties;
			this.rootContext = rootContext;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000053AA File Offset: 0x000035AA
		public ILogger Logger
		{
			get
			{
				return this.logger;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000053B2 File Offset: 0x000035B2
		public IEnvironment Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000053BA File Offset: 0x000035BA
		public IPropertyBag Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000053C2 File Offset: 0x000035C2
		public static IContext CreateRoot(IEnvironment environment, ILogger logger)
		{
			return new Context(environment, logger, new PropertyBag(), null);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000053D1 File Offset: 0x000035D1
		public IContext CreateDerived()
		{
			return new Context(this.environment, this.logger, this.CreateDerivedPropertyBag(), this.rootContext ?? this);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000053F8 File Offset: 0x000035F8
		private PropertyBag CreateDerivedPropertyBag()
		{
			PropertyBag propertyBag = new PropertyBag();
			foreach (ContextProperty property in this.properties.AllProperties)
			{
				object value;
				ExAssert.RetailAssert(this.properties.TryGet(property, out value), "Property that was returned from AllProperites must yield true from TryGet");
				propertyBag.Set(property, value);
			}
			return propertyBag;
		}

		// Token: 0x0400007E RID: 126
		private readonly IEnvironment environment;

		// Token: 0x0400007F RID: 127
		private readonly ILogger logger;

		// Token: 0x04000080 RID: 128
		private readonly PropertyBag properties;

		// Token: 0x04000081 RID: 129
		private readonly Context rootContext;
	}
}
