using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ContextProperty<TValue> : ContextProperty
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000054F0 File Offset: 0x000036F0
		private ContextProperty(Func<object> defaultValueDelegate) : base(Guid.NewGuid(), default(ContextProperty.DeclarationOptions))
		{
			this.defaultValueDelegate = defaultValueDelegate;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005518 File Offset: 0x00003718
		private ContextProperty(ContextProperty<TValue> parentProperty, ContextProperty.DeclarationOptions options) : base(parentProperty.Identity, options)
		{
			base.Name = parentProperty.Name;
			this.defaultValueDelegate = parentProperty.defaultValueDelegate;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000553F File Offset: 0x0000373F
		public override Type Type
		{
			get
			{
				return typeof(TValue);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005568 File Offset: 0x00003768
		public static ContextProperty<TValue> Declare(Func<TValue> defaultValueDelegate)
		{
			return new ContextProperty<TValue>((defaultValueDelegate != null) ? (() => defaultValueDelegate()) : null);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000055B0 File Offset: 0x000037B0
		public static ContextProperty<TValue> Declare(TValue defaultValue)
		{
			return ContextProperty<TValue>.Declare(() => defaultValue);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000055DB File Offset: 0x000037DB
		public static ContextProperty<TValue> Declare()
		{
			return ContextProperty<TValue>.Declare(null);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000055E4 File Offset: 0x000037E4
		public ContextProperty<TValue> GetOnly()
		{
			ContextProperty.DeclarationOptions options = this.Options;
			options.AccessMode = ContextProperty.AccessMode.Get;
			return new ContextProperty<TValue>(this, options);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005608 File Offset: 0x00003808
		public ContextProperty<TValue> SetOnly()
		{
			ContextProperty.DeclarationOptions options = this.Options;
			options.AccessMode = ContextProperty.AccessMode.Set;
			return new ContextProperty<TValue>(this, options);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000562B File Offset: 0x0000382B
		internal override bool TryGetDefaultValue(out object defaultValue)
		{
			if (this.defaultValueDelegate != null)
			{
				defaultValue = (TValue)((object)this.defaultValueDelegate());
				return true;
			}
			defaultValue = null;
			return false;
		}

		// Token: 0x0400008A RID: 138
		private readonly Func<object> defaultValueDelegate;
	}
}
