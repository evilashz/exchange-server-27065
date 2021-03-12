using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000033 RID: 51
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal abstract class ContextProperty
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0000546C File Offset: 0x0000366C
		protected ContextProperty(Guid identity, ContextProperty.DeclarationOptions options)
		{
			this.Identity = identity;
			this.Options = options;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005482 File Offset: 0x00003682
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000548A File Offset: 0x0000368A
		public string Name { get; internal set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000157 RID: 343
		public abstract Type Type { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005493 File Offset: 0x00003693
		public ContextProperty.AccessMode AllowedAccessMode
		{
			get
			{
				return this.Options.AccessMode;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000054A0 File Offset: 0x000036A0
		public override bool Equals(object obj)
		{
			return obj is ContextProperty && ((ContextProperty)obj).Identity == this.Identity;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000054C4 File Offset: 0x000036C4
		public override int GetHashCode()
		{
			return this.Identity.GetHashCode();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000054E5 File Offset: 0x000036E5
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600015C RID: 348
		internal abstract bool TryGetDefaultValue(out object defaultValue);

		// Token: 0x04000082 RID: 130
		protected readonly Guid Identity;

		// Token: 0x04000083 RID: 131
		protected readonly ContextProperty.DeclarationOptions Options;

		// Token: 0x02000034 RID: 52
		[Flags]
		internal enum AccessMode
		{
			// Token: 0x04000086 RID: 134
			None = 0,
			// Token: 0x04000087 RID: 135
			Get = 1,
			// Token: 0x04000088 RID: 136
			Set = 2
		}

		// Token: 0x02000035 RID: 53
		protected struct DeclarationOptions
		{
			// Token: 0x04000089 RID: 137
			public ContextProperty.AccessMode AccessMode;
		}
	}
}
