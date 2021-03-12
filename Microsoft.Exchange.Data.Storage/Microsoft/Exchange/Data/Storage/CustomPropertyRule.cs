using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AA3 RID: 2723
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CustomPropertyRule : PropertyRule
	{
		// Token: 0x06006371 RID: 25457 RVA: 0x001A33AF File Offset: 0x001A15AF
		public CustomPropertyRule(string name, Func<ICorePropertyBag, bool> writeEnforceDelegate, params PropertyReference[] references) : this(name, null, writeEnforceDelegate, references)
		{
			this.writeEnforceDelegate = writeEnforceDelegate;
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x001A33C2 File Offset: 0x001A15C2
		public CustomPropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, Func<ICorePropertyBag, bool> writeEnforceDelegate, params PropertyReference[] references) : base(name, onSetWriteEnforceLocationIdentifier, references)
		{
			this.writeEnforceDelegate = writeEnforceDelegate;
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x001A33D5 File Offset: 0x001A15D5
		protected override bool WriteEnforceRule(ICorePropertyBag propertyBag)
		{
			return this.writeEnforceDelegate(propertyBag);
		}

		// Token: 0x04003832 RID: 14386
		private readonly Func<ICorePropertyBag, bool> writeEnforceDelegate;
	}
}
