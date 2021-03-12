using System;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.Entities.DataProviders;

namespace Microsoft.Exchange.Entities
{
	// Token: 0x02000054 RID: 84
	public static class ExceptionHelper
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x0000699A File Offset: 0x00004B9A
		public static void AssertNotNull<T>(this T value, Action<T> consume, string name) where T : class
		{
			consume(value.AssertNotNull(name));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000069A9 File Offset: 0x00004BA9
		public static TResult AssertNotNull<T, TResult>(this T value, Func<T, TResult> consume, string name) where T : class
		{
			return consume(value.AssertNotNull(name));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000069B8 File Offset: 0x00004BB8
		public static T AssertNotNull<T>(this T value, string name) where T : class
		{
			return value;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000069BB File Offset: 0x00004BBB
		public static void ThrowIfPropertyNotSet(this PropertyChangeTrackingObject container, PropertyDefinition property)
		{
			if (!container.IsPropertySet(property))
			{
				throw new InvalidRequestException(Strings.ErrorMissingRequiredParameter(property.Name));
			}
		}
	}
}
