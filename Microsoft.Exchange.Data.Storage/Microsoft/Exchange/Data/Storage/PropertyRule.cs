using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000A98 RID: 2712
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class PropertyRule
	{
		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x0600633E RID: 25406 RVA: 0x001A2A0F File Offset: 0x001A0C0F
		// (set) Token: 0x0600633F RID: 25407 RVA: 0x001A2A17 File Offset: 0x001A0C17
		protected internal IEnumerable<PropertyDefinition> ReadProperties { get; protected set; }

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06006340 RID: 25408 RVA: 0x001A2A20 File Offset: 0x001A0C20
		// (set) Token: 0x06006341 RID: 25409 RVA: 0x001A2A28 File Offset: 0x001A0C28
		protected internal IEnumerable<PropertyDefinition> WriteProperties { get; protected set; }

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06006342 RID: 25410 RVA: 0x001A2A31 File Offset: 0x001A0C31
		// (set) Token: 0x06006343 RID: 25411 RVA: 0x001A2A39 File Offset: 0x001A0C39
		protected internal Action<ILocationIdentifierSetter> OnSetWriteEnforceLocationIdentifier { get; private set; }

		// Token: 0x06006344 RID: 25412 RVA: 0x001A2A42 File Offset: 0x001A0C42
		protected PropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, params PropertyReference[] references) : this(name, onSetWriteEnforceLocationIdentifier, (IEnumerable<PropertyReference>)references)
		{
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x001A2A54 File Offset: 0x001A0C54
		protected PropertyRule(string name, Action<ILocationIdentifierSetter> onSetWriteEnforceLocationIdentifier, IEnumerable<PropertyReference> references)
		{
			this.Name = name;
			List<PropertyDefinition> list = new List<PropertyDefinition>();
			List<PropertyDefinition> list2 = new List<PropertyDefinition>();
			foreach (PropertyReference propertyReference in references)
			{
				if (propertyReference.AccessType == PropertyAccess.None)
				{
					throw new ArgumentException("invalid type in property reference");
				}
				if (list.Contains(propertyReference.Property) || list2.Contains(propertyReference.Property))
				{
					throw new ArgumentException("duplicate properties in collection");
				}
				if ((propertyReference.AccessType & PropertyAccess.Read) == PropertyAccess.Read)
				{
					list.Add(propertyReference.Property);
				}
				if ((propertyReference.AccessType & PropertyAccess.Write) == PropertyAccess.Write)
				{
					list2.Add(propertyReference.Property);
				}
			}
			this.ReadProperties = list.ToArray();
			this.WriteProperties = list2.ToArray();
			this.OnSetWriteEnforceLocationIdentifier = onSetWriteEnforceLocationIdentifier;
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x001A2B38 File Offset: 0x001A0D38
		internal bool WriteEnforce(ICorePropertyBag propertyBag)
		{
			bool flag = this.WriteEnforceRule(propertyBag);
			if (flag && this.OnSetWriteEnforceLocationIdentifier != null)
			{
				this.OnSetWriteEnforceLocationIdentifier(propertyBag);
			}
			return flag;
		}

		// Token: 0x06006347 RID: 25415
		protected abstract bool WriteEnforceRule(ICorePropertyBag propertyBag);

		// Token: 0x06006348 RID: 25416 RVA: 0x001A2B65 File Offset: 0x001A0D65
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x001A2BA8 File Offset: 0x001A0DA8
		public static bool CheckCircularDependency(IEnumerable<PropertyRule> rules)
		{
			List<PropertyDefinition> readList = new List<PropertyDefinition>();
			List<PropertyDefinition> writeList = new List<PropertyDefinition>();
			foreach (PropertyRule propertyRule in rules)
			{
				if (!propertyRule.ReadProperties.Any((PropertyDefinition p) => writeList.Contains(p)))
				{
					if (!propertyRule.WriteProperties.Any((PropertyDefinition p) => readList.Contains(p)))
					{
						if (!propertyRule.WriteProperties.Any((PropertyDefinition p) => writeList.Contains(p)))
						{
							readList.AddRange(propertyRule.ReadProperties);
							writeList.AddRange(propertyRule.WriteProperties);
							continue;
						}
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x04003824 RID: 14372
		public readonly string Name;
	}
}
