using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EAF RID: 3759
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NonMoveMailboxPropertyConstraint : PropertyDefinitionConstraint
	{
		// Token: 0x06008236 RID: 33334 RVA: 0x00238EDE File Offset: 0x002370DE
		internal NonMoveMailboxPropertyConstraint(PropertyDefinitionConstraint constraint)
		{
			ArgumentValidator.ThrowIfNull("constraint", constraint);
			this.constraint = constraint;
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x06008237 RID: 33335 RVA: 0x00238EF8 File Offset: 0x002370F8
		public PropertyDefinitionConstraint Constraint
		{
			get
			{
				return this.constraint;
			}
		}

		// Token: 0x06008238 RID: 33336 RVA: 0x00238F00 File Offset: 0x00237100
		public override PropertyConstraintViolationError Validate(object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			return this.constraint.Validate(value, propertyDefinition, propertyBag);
		}

		// Token: 0x06008239 RID: 33337 RVA: 0x00238F10 File Offset: 0x00237110
		public override PropertyConstraintViolationError Validate(ExchangeOperationContext context, object value, PropertyDefinition propertyDefinition, IPropertyBag propertyBag)
		{
			ArgumentValidator.ThrowIfNull("propertyDefinition", propertyDefinition);
			if (context == null || !context.IsMoveUser)
			{
				return this.Validate(value, propertyDefinition, propertyBag);
			}
			NonMoveMailboxPropertyConstraint.Tracer.TraceDebug<PropertyDefinition>((long)this.GetHashCode(), "Skipping validation of property '{0}' during move mailbox stage", propertyDefinition);
			return null;
		}

		// Token: 0x0400576E RID: 22382
		private static readonly Trace Tracer = ExTraceGlobals.StorageTracer;

		// Token: 0x0400576F RID: 22383
		private PropertyDefinitionConstraint constraint;
	}
}
