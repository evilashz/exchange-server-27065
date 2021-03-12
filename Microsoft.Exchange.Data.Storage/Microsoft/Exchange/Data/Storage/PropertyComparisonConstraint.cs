using System;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000EB4 RID: 3764
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PropertyComparisonConstraint : StoreObjectConstraint
	{
		// Token: 0x06008244 RID: 33348 RVA: 0x00239148 File Offset: 0x00237348
		internal PropertyComparisonConstraint(StorePropertyDefinition leftPropertyDefinition, StorePropertyDefinition rightPropertyDefinition, ComparisonOperator comparisonOperator) : base(new PropertyDefinition[]
		{
			leftPropertyDefinition,
			rightPropertyDefinition
		})
		{
			EnumValidator.AssertValid<ComparisonOperator>(comparisonOperator);
			if (comparisonOperator != ComparisonOperator.Equal && comparisonOperator != ComparisonOperator.NotEqual && !typeof(IComparable).GetTypeInfo().IsAssignableFrom(leftPropertyDefinition.Type.GetTypeInfo()))
			{
				throw new NotSupportedException(ServerStrings.ExConstraintNotSupportedForThisPropertyType);
			}
			this.leftPropertyDefinition = leftPropertyDefinition;
			this.rightPropertyDefinition = rightPropertyDefinition;
			this.comparisonOperator = comparisonOperator;
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x06008245 RID: 33349 RVA: 0x002391BE File Offset: 0x002373BE
		public ComparisonOperator ComparisonOperator
		{
			get
			{
				return this.comparisonOperator;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x06008246 RID: 33350 RVA: 0x002391C6 File Offset: 0x002373C6
		public PropertyDefinition LeftPropertyDefinition
		{
			get
			{
				return this.leftPropertyDefinition;
			}
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x06008247 RID: 33351 RVA: 0x002391CE File Offset: 0x002373CE
		public PropertyDefinition RightPropertyDefinition
		{
			get
			{
				return this.rightPropertyDefinition;
			}
		}

		// Token: 0x06008248 RID: 33352 RVA: 0x002391D8 File Offset: 0x002373D8
		internal override StoreObjectValidationError Validate(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			object obj = validatablePropertyBag.TryGetProperty(this.leftPropertyDefinition);
			object obj2 = validatablePropertyBag.TryGetProperty(this.rightPropertyDefinition);
			if (PropertyError.IsPropertyNotFound(obj) && PropertyError.IsPropertyNotFound(obj2) && (this.comparisonOperator == ComparisonOperator.Equal || this.comparisonOperator == ComparisonOperator.LessThanOrEqual || this.comparisonOperator == ComparisonOperator.GreaterThanOrEqual))
			{
				return null;
			}
			if (PropertyError.IsPropertyError(obj))
			{
				return new StoreObjectValidationError(context, this.leftPropertyDefinition, obj, this);
			}
			if (PropertyError.IsPropertyError(obj2))
			{
				return new StoreObjectValidationError(context, this.rightPropertyDefinition, obj2, this);
			}
			bool flag = false;
			switch (this.comparisonOperator)
			{
			case ComparisonOperator.Equal:
				flag = Util.ValueEquals(obj, obj2);
				break;
			case ComparisonOperator.NotEqual:
				flag = !Util.ValueEquals(obj, obj2);
				break;
			case ComparisonOperator.LessThan:
				flag = PropertyComparisonConstraint.LessThan(obj, obj2);
				break;
			case ComparisonOperator.LessThanOrEqual:
				flag = !PropertyComparisonConstraint.LessThan(obj2, obj);
				break;
			case ComparisonOperator.GreaterThan:
				flag = PropertyComparisonConstraint.LessThan(obj2, obj);
				break;
			case ComparisonOperator.GreaterThanOrEqual:
				flag = !PropertyComparisonConstraint.LessThan(obj, obj2);
				break;
			}
			if (flag)
			{
				return null;
			}
			return new StoreObjectValidationError(context, this.rightPropertyDefinition, obj2, this);
		}

		// Token: 0x06008249 RID: 33353 RVA: 0x002392D8 File Offset: 0x002374D8
		private static bool LessThan(object leftValue, object rightValue)
		{
			IComparable comparable = (IComparable)leftValue;
			return comparable.CompareTo(rightValue) < 0;
		}

		// Token: 0x0600824A RID: 33354 RVA: 0x002392F6 File Offset: 0x002374F6
		public override string ToString()
		{
			return string.Format("Property {0} must be {1} when compared to property {2}.", this.leftPropertyDefinition, this.comparisonOperator, this.rightPropertyDefinition);
		}

		// Token: 0x04005770 RID: 22384
		private readonly ComparisonOperator comparisonOperator;

		// Token: 0x04005771 RID: 22385
		private readonly StorePropertyDefinition leftPropertyDefinition;

		// Token: 0x04005772 RID: 22386
		private readonly StorePropertyDefinition rightPropertyDefinition;
	}
}
