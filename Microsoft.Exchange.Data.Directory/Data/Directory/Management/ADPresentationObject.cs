using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200051C RID: 1308
	[Serializable]
	public abstract class ADPresentationObject : ADObject, IConfigurable, ICloneable
	{
		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000DF9DE File Offset: 0x000DDBDE
		internal ADObject DataObject
		{
			get
			{
				return this.dataObject;
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000DF9E6 File Offset: 0x000DDBE6
		public ADPresentationObject()
		{
			this.dataObject = null;
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x000DF9F5 File Offset: 0x000DDBF5
		public ADPresentationObject(ADObject dataObject)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			this.dataObject = dataObject;
			this.propertyBag = dataObject.propertyBag;
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000DFA20 File Offset: 0x000DDC20
		public override ObjectId Identity
		{
			get
			{
				ObjectId objectId = base.Identity;
				if (objectId is ADObjectId && SuppressingPiiContext.NeedPiiSuppression)
				{
					objectId = (ObjectId)SuppressingPiiProperty.TryRedact(ADObjectSchema.Id, objectId);
				}
				return objectId;
			}
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000DFA55 File Offset: 0x000DDC55
		protected static IEnumerable<PropertyInfo> GetCloneableOnceProperties(ADPresentationObject source)
		{
			return ADPresentationObject.GetPropertiesByAttribute(source, typeof(ProvisionalCloneOnce));
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000DFA67 File Offset: 0x000DDC67
		protected static IEnumerable<PropertyInfo> GetCloneableEnabledStateProperties(ADPresentationObject source)
		{
			return ADPresentationObject.GetPropertiesByAttribute(source, typeof(ProvisionalCloneEnabledState));
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000DFA79 File Offset: 0x000DDC79
		protected static IEnumerable<PropertyInfo> GetCloneableProperties(ADPresentationObject source)
		{
			return ADPresentationObject.GetPropertiesByAttribute(source, typeof(ProvisionalClone));
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000DFA8C File Offset: 0x000DDC8C
		private static IEnumerable<PropertyInfo> GetPropertiesByAttribute(ADPresentationObject source, Type type)
		{
			PropertyInfo[] properties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in properties)
			{
				object[] customAttributes = propertyInfo.GetCustomAttributes(type, true);
				if (customAttributes.Length != 0)
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.MailboxPlans.CloneLimitedSetOfMailboxPlanProperties.Enabled)
					{
						ProvisionalCloneBase provisionalCloneBase = customAttributes[0] as ProvisionalCloneBase;
						if (provisionalCloneBase != null && provisionalCloneBase.CloneSet == CloneSet.CloneLimitedSet)
						{
							goto IL_A4;
						}
					}
					if (propertyInfo.PropertyType == typeof(MultiValuedProperty<Capability>))
					{
						if (propertyInfo.CanRead)
						{
							list.Add(propertyInfo);
						}
					}
					else if (propertyInfo.CanRead && propertyInfo.CanWrite)
					{
						list.Add(propertyInfo);
					}
				}
				IL_A4:;
			}
			return list;
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000DFB50 File Offset: 0x000DDD50
		protected override void ValidateRead(List<ValidationError> errors)
		{
			if (this.dataObject == null)
			{
				throw new NotSupportedException("Can't call ValidateRead on presentation object when DataObject is null");
			}
			ValidationError[] array = this.dataObject.ValidateRead();
			if (array != null && 0 < array.Length)
			{
				errors.AddRange(array);
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000DFB8C File Offset: 0x000DDD8C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			if (this.dataObject == null)
			{
				throw new NotSupportedException("Can't call ValidateWrite on presentation object when DataObject is null");
			}
			ValidationError[] array = this.dataObject.ValidateWrite();
			if (array != null && 0 < array.Length)
			{
				errors.AddRange(array);
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000DFBC8 File Offset: 0x000DDDC8
		public override bool IsValid
		{
			get
			{
				if (this.dataObject == null)
				{
					throw new NotSupportedException("Can't call IsValid on presentation object when DataObject is null");
				}
				return this.dataObject.IsValid;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000DFBE8 File Offset: 0x000DDDE8
		internal override string MostDerivedObjectClass
		{
			get
			{
				if (this.dataObject == null)
				{
					throw new NotSupportedException("Can't call MostDerivedObjectClass on presentation object when DataObject is null");
				}
				return this.dataObject.MostDerivedObjectClass;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x060039DA RID: 14810 RVA: 0x000DFC08 File Offset: 0x000DDE08
		internal sealed override ADObjectSchema Schema
		{
			get
			{
				return this.PresentationSchema;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x060039DB RID: 14811
		internal abstract ADPresentationSchema PresentationSchema { get; }

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x060039DC RID: 14812 RVA: 0x000DFC10 File Offset: 0x000DDE10
		internal override ObjectSchema DeserializationSchema
		{
			get
			{
				if (this.dataObject == null)
				{
					throw new NotSupportedException("Can't call DeserializationSchema on presentation object when DataObject is null");
				}
				return this.dataObject.ObjectSchema;
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x000DFC30 File Offset: 0x000DDE30
		internal override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return this.dataObject != null && this.dataObject.ExchangeVersionUpgradeSupported;
			}
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000DFC47 File Offset: 0x000DDE47
		private static IEnumerable<PropertyInfo> GetEnabledPropertiesFromPropertyInfoList(ADPresentationObject source)
		{
			return ADPresentationObject.GetEnabledPropertiesFromPropertyInfoList(source, null);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000DFC50 File Offset: 0x000DDE50
		private static IEnumerable<PropertyInfo> GetEnabledPropertiesFromPropertyInfoList(ADPresentationObject source1, ADPresentationObject source2)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			if (source1.CloneableEnabledStateProperties != null)
			{
				foreach (PropertyInfo propertyInfo in source1.CloneableEnabledStateProperties)
				{
					if ((bool)propertyInfo.GetValue(source1, null) || (source2 != null && (bool)propertyInfo.GetValue(source2, null)))
					{
						list.Add(propertyInfo);
					}
				}
			}
			return list;
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000DFCD0 File Offset: 0x000DDED0
		internal void ApplyCloneableProperties(ADPresentationObject source)
		{
			IEnumerable<PropertyInfo> enabledPropertiesFromPropertyInfoList = ADPresentationObject.GetEnabledPropertiesFromPropertyInfoList(source);
			IEnumerable<PropertyInfo>[] array = new IEnumerable<PropertyInfo>[]
			{
				this.CloneableOnceProperties,
				this.CloneableProperties,
				enabledPropertiesFromPropertyInfoList
			};
			foreach (IEnumerable<PropertyInfo> enumerable in array)
			{
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					object value = propertyInfo.GetValue(source, null);
					object value2 = propertyInfo.GetValue(this, null);
					if (value != null && value2 != value)
					{
						MultiValuedPropertyBase multiValuedPropertyBase = value as MultiValuedPropertyBase;
						if (multiValuedPropertyBase == null || multiValuedPropertyBase.Count != 0)
						{
							if (propertyInfo.PropertyType == typeof(MultiValuedProperty<Capability>))
							{
								MultiValuedProperty<Capability> multiValuedProperty = (MultiValuedProperty<Capability>)value;
								CapabilityHelper.SetSKUCapability((multiValuedProperty.Count == 0) ? null : new Capability?(multiValuedProperty[0]), (MultiValuedProperty<Capability>)value2);
							}
							else
							{
								propertyInfo.SetValue(this, value, null);
							}
						}
					}
				}
			}
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000DFDF8 File Offset: 0x000DDFF8
		internal static void ApplyPresentationObjectDelta(ADPresentationObject oldPlan, ADPresentationObject newPlan, ADPresentationObject target, ApplyMailboxPlanFlags flags)
		{
			IEnumerable<PropertyInfo> enabledPropertiesFromPropertyInfoList = ADPresentationObject.GetEnabledPropertiesFromPropertyInfoList(newPlan, oldPlan);
			IEnumerable<PropertyInfo>[] array;
			if (oldPlan == null)
			{
				array = new IEnumerable<PropertyInfo>[]
				{
					target.CloneableOnceProperties,
					target.CloneableProperties,
					enabledPropertiesFromPropertyInfoList
				};
			}
			else
			{
				array = new IEnumerable<PropertyInfo>[]
				{
					target.CloneableProperties,
					enabledPropertiesFromPropertyInfoList
				};
			}
			bool flag = oldPlan == null && newPlan != null && flags.HasFlag(ApplyMailboxPlanFlags.PreservePreviousExplicitlySetValues);
			foreach (IEnumerable<PropertyInfo> enumerable in array)
			{
				foreach (PropertyInfo propertyInfo in enumerable)
				{
					object obj = null;
					object value = propertyInfo.GetValue(newPlan, null);
					MultiValuedPropertyBase multiValuedPropertyBase = value as MultiValuedPropertyBase;
					if (oldPlan != null)
					{
						obj = propertyInfo.GetValue(oldPlan, null);
					}
					if (propertyInfo.PropertyType == typeof(MultiValuedProperty<Capability>))
					{
						if (!object.Equals(obj, value))
						{
							MultiValuedProperty<Capability> sourceCapabilities = MultiValuedProperty<Capability>.Empty;
							if (value != null)
							{
								sourceCapabilities = (MultiValuedProperty<Capability>)value;
							}
							CapabilityHelper.SetSKUCapabilities(newPlan.Name, sourceCapabilities, (MultiValuedProperty<Capability>)propertyInfo.GetValue(target, null));
						}
					}
					else if (obj != null || value != null)
					{
						if (obj == null || value == null)
						{
							if (flag)
							{
								object value2 = propertyInfo.GetValue(target, null);
								ADPropertyDefinition adpropertyDefinition = newPlan.Schema[propertyInfo.Name] as ADPropertyDefinition;
								if (adpropertyDefinition != null && ((adpropertyDefinition.DefaultValue == null && value2 != null) || (adpropertyDefinition.DefaultValue != null && !adpropertyDefinition.DefaultValue.Equals(value2))))
								{
									continue;
								}
							}
							propertyInfo.SetValue(target, value, null);
						}
						else if (!obj.Equals(value))
						{
							if (multiValuedPropertyBase == null)
							{
								propertyInfo.SetValue(target, value, null);
							}
							else
							{
								bool flag2 = false;
								MultiValuedPropertyBase multiValuedPropertyBase2 = obj as MultiValuedPropertyBase;
								if (multiValuedPropertyBase2.Count != multiValuedPropertyBase.Count)
								{
									flag2 = true;
								}
								else
								{
									foreach (object obj2 in ((IEnumerable)multiValuedPropertyBase2))
									{
										bool flag3 = false;
										foreach (object obj3 in ((IEnumerable)multiValuedPropertyBase))
										{
											if (obj2.Equals(obj3))
											{
												flag3 = true;
												break;
											}
										}
										if (!flag3)
										{
											flag2 = true;
											break;
										}
									}
								}
								if (flag2)
								{
									propertyInfo.SetValue(target, value, null);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000E00E0 File Offset: 0x000DE2E0
		protected virtual IEnumerable<PropertyInfo> CloneableProperties
		{
			get
			{
				return ADPresentationObject.GetCloneableProperties(this);
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000E00E8 File Offset: 0x000DE2E8
		protected virtual IEnumerable<PropertyInfo> CloneableOnceProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000E00EB File Offset: 0x000DE2EB
		protected virtual IEnumerable<PropertyInfo> CloneableEnabledStateProperties
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400277D RID: 10109
		private ADObject dataObject;
	}
}
