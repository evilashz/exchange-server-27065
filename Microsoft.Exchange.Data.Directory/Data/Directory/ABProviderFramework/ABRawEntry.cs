using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ABRawEntry : IReadOnlyPropertyBag
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected ABRawEntry(ABSession ownerSession, ABPropertyDefinitionCollection propertyDefinitionCollection)
		{
			if (ownerSession == null)
			{
				throw new ArgumentNullException("ownerSession");
			}
			if (propertyDefinitionCollection == null)
			{
				throw new ArgumentNullException("propertyDefinitionCollection");
			}
			this.ownerSession = ownerSession;
			this.propertyDefinitionCollection = propertyDefinitionCollection;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002102 File Offset: 0x00000302
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000210A File Offset: 0x0000030A
		public ABPropertyDefinitionCollection PropertyDefinitionCollection
		{
			get
			{
				return this.propertyDefinitionCollection;
			}
			internal set
			{
				this.propertyDefinitionCollection = value;
			}
		}

		// Token: 0x17000002 RID: 2
		object IReadOnlyPropertyBag.this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return this[(ABPropertyDefinition)propertyDefinition];
			}
		}

		// Token: 0x17000003 RID: 3
		public virtual object this[ABPropertyDefinition property]
		{
			get
			{
				object result;
				if (!this.TryGetValue(property, out result))
				{
					throw new KeyNotFoundException(property.Name);
				}
				return result;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000214C File Offset: 0x0000034C
		public bool TryGetValue(ABPropertyDefinition property, out object value)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			if (!this.propertyDefinitionCollection.Contains(property))
			{
				throw new KeyNotFoundException(property.Name);
			}
			bool result = this.InternalTryGetValue(property, out value);
			if (value != null && !property.Type.IsInstanceOfType(value))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Provider '{0}' returned type '{1}' rather than '{2}' for property '{3}'", new object[]
				{
					this.ownerSession.ProviderName,
					(value == null) ? "null reference" : value.GetType().Name,
					property.Type.Name,
					property.Name
				}));
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021FC File Offset: 0x000003FC
		public bool TryGetValue<T>(ABPropertyDefinition property, out T value)
		{
			object obj;
			if (!this.TryGetValue(property, out obj))
			{
				value = default(T);
				return false;
			}
			value = (T)((object)obj);
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000222C File Offset: 0x0000042C
		object[] IReadOnlyPropertyBag.GetProperties(ICollection<PropertyDefinition> propertyDefinitionCollection)
		{
			if (propertyDefinitionCollection == null)
			{
				throw new ArgumentNullException("propertyDefinitionCollection");
			}
			object[] array = new object[propertyDefinitionCollection.Count];
			int num = 0;
			foreach (PropertyDefinition propertyDefinition in propertyDefinitionCollection)
			{
				ABPropertyDefinition abpropertyDefinition = (ABPropertyDefinition)propertyDefinition;
				if (abpropertyDefinition == null)
				{
					throw new ArgumentException("Property definition collection contains null entries.", "propertyDefinitionCollection");
				}
				array[num++] = this[abpropertyDefinition];
			}
			return array;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022B0 File Offset: 0x000004B0
		protected virtual bool InternalTryGetValue(ABPropertyDefinition property, out object value)
		{
			value = null;
			return false;
		}

		// Token: 0x04000001 RID: 1
		private ABSession ownerSession;

		// Token: 0x04000002 RID: 2
		private ABPropertyDefinitionCollection propertyDefinitionCollection;
	}
}
