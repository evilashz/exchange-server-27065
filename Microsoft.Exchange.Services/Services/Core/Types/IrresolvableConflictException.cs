using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007E0 RID: 2016
	[Serializable]
	internal sealed class IrresolvableConflictException : ServicePermanentException
	{
		// Token: 0x06003B42 RID: 15170 RVA: 0x000CFE40 File Offset: 0x000CE040
		public IrresolvableConflictException(PropertyConflict[] propertyConflicts) : base(CoreResources.IDs.ErrorIrresolvableConflict)
		{
			if (propertyConflicts != null)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<int>((long)this.GetHashCode(), "IrresolvableConflictException constructor called for '{0}' property conflicts.", propertyConflicts.Length);
				foreach (PropertyConflict propertyConflict in propertyConflicts)
				{
					ExTraceGlobals.ExceptionTracer.TraceError((long)this.GetHashCode(), "Property conflict: DisplayName: '{0}', Resolvable: '{1}', OriginalValue: '{2}', ClientValue: '{3}', ServerValue: '{4}'", new object[]
					{
						(propertyConflict.PropertyDefinition != null) ? propertyConflict.PropertyDefinition.Name : ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.PropertyDefinition),
						propertyConflict.ConflictResolvable,
						ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.OriginalValue),
						ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.ClientValue),
						ServiceDiagnostics.HandleNullObjectTrace(propertyConflict.ServerValue)
					});
				}
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000CFF0F File Offset: 0x000CE10F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
