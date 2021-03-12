using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000A9E RID: 2718
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class PriorityBasedDisplayNamePropertyRule
	{
		// Token: 0x0600635C RID: 25436 RVA: 0x001A2DEF File Offset: 0x001A0FEF
		public PriorityBasedDisplayNamePropertyRule()
		{
			this.candidateProperties = this.GetCandidateProperties();
			this.allSourceProperties = this.GetAllSourceProperties();
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x001A2E10 File Offset: 0x001A1010
		public virtual List<PropertyReference> GetPropertyReferenceList()
		{
			List<PropertyReference> list = new List<PropertyReference>(this.allSourceProperties.Count + PriorityBasedDisplayNamePropertyRule.destinationProperties.Count);
			foreach (NativeStorePropertyDefinition usedProperty in this.allSourceProperties)
			{
				list.Add(new PropertyReference(usedProperty, PropertyAccess.Read));
			}
			foreach (NativeStorePropertyDefinition usedProperty2 in PriorityBasedDisplayNamePropertyRule.destinationProperties)
			{
				list.Add(new PropertyReference(usedProperty2, PropertyAccess.Write));
			}
			return list;
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x001A2ECC File Offset: 0x001A10CC
		public virtual bool UpdateDisplayNameProperties(ICorePropertyBag propertyBag)
		{
			bool flag = false;
			foreach (NativeStorePropertyDefinition propertyDefinition in this.allSourceProperties)
			{
				if (propertyBag.IsPropertyDirty(propertyDefinition))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
			bool flag2 = false;
			for (int i = 0; i < this.candidateProperties.Count; i++)
			{
				PriorityBasedDisplayNamePropertyRule.CandidateProperty candidateProperty = this.candidateProperties[i];
				if (candidateProperty.HasNonEmptyValue(propertyBag))
				{
					string value = null;
					string value2 = null;
					candidateProperty.GetValue(propertyBag, out value, out value2);
					propertyBag.SetProperty(InternalSchema.DisplayNameFirstLast, value);
					propertyBag.SetProperty(InternalSchema.DisplayNameLastFirst, value2);
					propertyBag.SetProperty(InternalSchema.DisplayNamePriority, i);
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				propertyBag.Delete(InternalSchema.DisplayNameFirstLast);
				propertyBag.Delete(InternalSchema.DisplayNameLastFirst);
				propertyBag.Delete(InternalSchema.DisplayNamePriority);
			}
			return true;
		}

		// Token: 0x0600635F RID: 25439
		protected abstract IList<PriorityBasedDisplayNamePropertyRule.CandidateProperty> GetCandidateProperties();

		// Token: 0x06006360 RID: 25440 RVA: 0x001A2FC4 File Offset: 0x001A11C4
		protected List<NativeStorePropertyDefinition> GetAllSourceProperties()
		{
			List<NativeStorePropertyDefinition> list = new List<NativeStorePropertyDefinition>(this.candidateProperties.Count);
			foreach (PriorityBasedDisplayNamePropertyRule.CandidateProperty candidateProperty in this.candidateProperties)
			{
				list.AddRange(candidateProperty.PropertyDefinitions);
			}
			return list;
		}

		// Token: 0x0400382C RID: 14380
		private static readonly IList<NativeStorePropertyDefinition> destinationProperties = new List<NativeStorePropertyDefinition>
		{
			InternalSchema.DisplayNameFirstLast,
			InternalSchema.DisplayNameLastFirst,
			InternalSchema.DisplayNamePriority
		};

		// Token: 0x0400382D RID: 14381
		private readonly IList<PriorityBasedDisplayNamePropertyRule.CandidateProperty> candidateProperties;

		// Token: 0x0400382E RID: 14382
		private readonly List<NativeStorePropertyDefinition> allSourceProperties;

		// Token: 0x02000A9F RID: 2719
		protected class CandidateProperty
		{
			// Token: 0x17001B8B RID: 7051
			// (get) Token: 0x06006362 RID: 25442 RVA: 0x001A3062 File Offset: 0x001A1262
			// (set) Token: 0x06006363 RID: 25443 RVA: 0x001A306A File Offset: 0x001A126A
			public List<NativeStorePropertyDefinition> PropertyDefinitions { get; private set; }

			// Token: 0x06006364 RID: 25444 RVA: 0x001A3073 File Offset: 0x001A1273
			public CandidateProperty(List<NativeStorePropertyDefinition> propertyDefinitions, PriorityBasedDisplayNamePropertyRule.CandidateProperty.DisplayNameValueDelegate valueDelegate)
			{
				this.PropertyDefinitions = propertyDefinitions;
				this.valueDelegate = valueDelegate;
			}

			// Token: 0x06006365 RID: 25445 RVA: 0x001A3089 File Offset: 0x001A1289
			public void GetValue(ICorePropertyBag propertyBag, out string displayNameFirstLast, out string displayNameLastFirst)
			{
				this.valueDelegate(propertyBag, out displayNameFirstLast, out displayNameLastFirst);
			}

			// Token: 0x06006366 RID: 25446 RVA: 0x001A309C File Offset: 0x001A129C
			public bool HasNonEmptyValue(ICorePropertyBag propertyBag)
			{
				foreach (NativeStorePropertyDefinition propertyDefinition in this.PropertyDefinitions)
				{
					if (propertyBag.GetValueOrDefault<string>(propertyDefinition, string.Empty).Trim() != string.Empty)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0400382F RID: 14383
			private readonly PriorityBasedDisplayNamePropertyRule.CandidateProperty.DisplayNameValueDelegate valueDelegate;

			// Token: 0x02000AA0 RID: 2720
			// (Invoke) Token: 0x06006368 RID: 25448
			internal delegate void DisplayNameValueDelegate(ICorePropertyBag propertyBag, out string displayNameFirstLast, out string displayNameLastFirst);
		}

		// Token: 0x02000AA1 RID: 2721
		protected sealed class SimpleCandidateProperty : PriorityBasedDisplayNamePropertyRule.CandidateProperty
		{
			// Token: 0x0600636B RID: 25451 RVA: 0x001A310C File Offset: 0x001A130C
			public SimpleCandidateProperty(NativeStorePropertyDefinition propertyDefinition) : base(new List<NativeStorePropertyDefinition>(1)
			{
				propertyDefinition
			}, PriorityBasedDisplayNamePropertyRule.SimpleCandidateProperty.ValueDelegate(propertyDefinition))
			{
			}

			// Token: 0x0600636C RID: 25452 RVA: 0x001A3164 File Offset: 0x001A1364
			private static PriorityBasedDisplayNamePropertyRule.CandidateProperty.DisplayNameValueDelegate ValueDelegate(NativeStorePropertyDefinition propertyDefinition)
			{
				return delegate(ICorePropertyBag propertyBag, out string displayNameFirstLast, out string displayNameLastFirst)
				{
					string valueOrDefault;
					displayNameLastFirst = (valueOrDefault = propertyBag.GetValueOrDefault<string>(propertyDefinition, string.Empty));
					displayNameFirstLast = valueOrDefault;
				};
			}
		}
	}
}
