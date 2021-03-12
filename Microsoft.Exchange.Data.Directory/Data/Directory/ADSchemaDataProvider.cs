﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000071 RID: 113
	internal class ADSchemaDataProvider
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001D898 File Offset: 0x0001BA98
		public static ADSchemaDataProvider Instance
		{
			get
			{
				return ADSchemaDataProvider.instance;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		private ADSchemaDataProvider()
		{
			this.schemaSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 82, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\ADSchemaDataProvider.cs");
			this.constraintDictionary = new Dictionary<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]>();
			this.schemaAttributeObjectDictionary = new ConcurrentDictionary<string, ADSchemaAttributeObject>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		public bool UpdateProperties(IList<PropertyDefinition> properties)
		{
			if (TopologyProvider.CurrentTopologyMode == TopologyMode.Adam || TopologyMode.Ldap == TopologyProvider.CurrentTopologyMode)
			{
				ExTraceGlobals.SchemaInitializationTracer.TraceDebug<TopologyMode>((long)this.GetHashCode(), "Aborting Schema Update. TopologyMode: {0}", TopologyProvider.CurrentTopologyMode);
				return false;
			}
			bool flag = this.schemaSession.IsReadConnectionAvailable();
			if (!flag)
			{
				ExTraceGlobals.SchemaInitializationTracer.TraceDebug<bool>((long)this.GetHashCode(), "Aborting Schema Update. IsConnectionAvailable: {1}", flag);
				return false;
			}
			bool result = true;
			List<ADPropertyDefinition> propertiesToRead;
			List<ADPropertyDefinition> list;
			this.GetPropertiesToUpdate(properties, out propertiesToRead, out list);
			this.UpdateConstraintDictionary(propertiesToRead);
			for (int i = 0; i < list.Count; i++)
			{
				ADPropertyDefinition adpropertyDefinition = list[i];
				PropertyDefinitionConstraint[] autogeneratedConstraints;
				if (this.constraintDictionary.TryGetValue(ADSchemaDataProvider.PropertyKey.GetPropertyKey(adpropertyDefinition), out autogeneratedConstraints))
				{
					adpropertyDefinition.SetAutogeneratedConstraints(autogeneratedConstraints);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001D9B8 File Offset: 0x0001BBB8
		private static bool IsKnownType(Type type)
		{
			bool flag;
			bool flag2;
			Type typeInformation = ADSchemaDataProvider.GetTypeInformation(type, out flag, out flag2);
			return typeof(string) == typeInformation || typeof(int) == typeInformation || typeof(long) == typeInformation || typeof(ByteQuantifiedSize) == typeInformation || typeof(SmtpDomain) == typeInformation || typeof(SmtpAddress) == typeInformation || typeof(ProtocolConnectionSettings) == typeInformation || typeof(SmtpDomainWithSubdomains) == typeInformation || typeof(RoleEntry) == typeInformation || typeof(ProxyAddressTemplate) == typeInformation || typeof(UncFileSharePath) == typeInformation || typeof(NonRootLocalLongFullPath) == typeInformation || typeof(ProxyAddress) == typeInformation || typeof(byte[]) == typeInformation;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001DAE4 File Offset: 0x0001BCE4
		private static Type GetTypeInformation(Type inputType, out bool isUnlimitedType, out bool isNullableType)
		{
			isNullableType = false;
			isUnlimitedType = false;
			Type result = inputType;
			if (inputType.IsValueType && inputType.IsGenericType)
			{
				Type genericTypeDefinition = inputType.GetGenericTypeDefinition();
				if (genericTypeDefinition == typeof(Nullable<>))
				{
					isNullableType = true;
					result = Nullable.GetUnderlyingType(inputType);
				}
				else if (genericTypeDefinition == typeof(Unlimited<>))
				{
					isUnlimitedType = true;
					result = inputType.GetGenericArguments()[0];
				}
			}
			return result;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001DB50 File Offset: 0x0001BD50
		private static void GetStringConstraints(ADSchemaAttributeObject schemaObject, ADPropertyDefinition propDef, List<PropertyDefinitionConstraint> constraints)
		{
			int num = schemaObject.RangeLower ?? 0;
			if (num == 1)
			{
				num = 0;
			}
			int maxLength = schemaObject.RangeUpper ?? 0;
			constraints.Add(new StringLengthConstraint(num, maxLength));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		private static void GetIntegerConstraints(ADSchemaAttributeObject schemaObject, ADPropertyDefinition propDef, List<PropertyDefinitionConstraint> constraints)
		{
			bool isUnlimited;
			bool isNullable;
			Type typeInformation = ADSchemaDataProvider.GetTypeInformation(propDef.Type, out isUnlimited, out isNullable);
			if (typeInformation == typeof(int))
			{
				int minValue = schemaObject.RangeLower ?? int.MinValue;
				int maxValue = schemaObject.RangeUpper ?? int.MaxValue;
				constraints.Add(ADSchemaDataProvider.GetRangedConstraint<int>(minValue, maxValue, isUnlimited, isNullable));
				return;
			}
			if (typeInformation == typeof(ByteQuantifiedSize))
			{
				IFormatProvider formatProvider = propDef.FormatProvider;
				ByteQuantifiedSize.Quantifier quantifier = (formatProvider != null) ? ((ByteQuantifiedSize.Quantifier)formatProvider.GetFormat(typeof(ByteQuantifiedSize.Quantifier))) : ByteQuantifiedSize.Quantifier.None;
				ulong number = (ulong)((long)(schemaObject.RangeLower ?? 0));
				int? rangeUpper = schemaObject.RangeUpper;
				ulong number2 = ((rangeUpper != null) ? new ulong?((ulong)((long)rangeUpper.GetValueOrDefault())) : null) ?? Math.Min((ulong)((ByteQuantifiedSize.Quantifier)18446744073709551615UL / quantifier), 2147483647UL);
				ByteQuantifiedSize minValue2 = ByteQuantifiedSize.FromSpecifiedUnit(number, quantifier);
				ByteQuantifiedSize maxValue2 = ByteQuantifiedSize.FromSpecifiedUnit(number2, quantifier);
				constraints.Add(ADSchemaDataProvider.GetRangedConstraint<ByteQuantifiedSize>(minValue2, maxValue2, isUnlimited, isNullable));
				return;
			}
			ExTraceGlobals.SchemaInitializationTracer.TraceDebug<DataSyntax, ADPropertyDefinition>(0L, "Unsupported property type '{1}' for data syntax '{0}'.", schemaObject.DataSyntax, propDef);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0001DD14 File Offset: 0x0001BF14
		private static void GetLargeIntegerConstraints(ADSchemaAttributeObject schemaObject, ADPropertyDefinition propDef, List<PropertyDefinitionConstraint> constraints)
		{
			bool isUnlimited;
			bool isNullable;
			Type typeInformation = ADSchemaDataProvider.GetTypeInformation(propDef.Type, out isUnlimited, out isNullable);
			if (typeInformation == typeof(long))
			{
				int? rangeLower = schemaObject.RangeLower;
				long minValue = (rangeLower != null) ? ((long)rangeLower.GetValueOrDefault()) : long.MinValue;
				int? rangeUpper = schemaObject.RangeUpper;
				long maxValue = (rangeUpper != null) ? ((long)rangeUpper.GetValueOrDefault()) : long.MaxValue;
				constraints.Add(ADSchemaDataProvider.GetRangedConstraint<long>(minValue, maxValue, isUnlimited, isNullable));
				return;
			}
			if (typeInformation == typeof(int))
			{
				int minValue2 = schemaObject.RangeLower ?? int.MinValue;
				int maxValue2 = schemaObject.RangeUpper ?? int.MaxValue;
				constraints.Add(ADSchemaDataProvider.GetRangedConstraint<int>(minValue2, maxValue2, isUnlimited, isNullable));
				return;
			}
			if (typeInformation == typeof(ByteQuantifiedSize))
			{
				IFormatProvider formatProvider = propDef.FormatProvider;
				ByteQuantifiedSize.Quantifier quantifier = (formatProvider != null) ? ((ByteQuantifiedSize.Quantifier)formatProvider.GetFormat(typeof(ByteQuantifiedSize.Quantifier))) : ByteQuantifiedSize.Quantifier.None;
				ulong number = (ulong)((long)(schemaObject.RangeLower ?? 0));
				int? rangeUpper2 = schemaObject.RangeUpper;
				ulong number2 = ((rangeUpper2 != null) ? new ulong?((ulong)((long)rangeUpper2.GetValueOrDefault())) : null) ?? Math.Min((ulong)((ByteQuantifiedSize.Quantifier)18446744073709551615UL / quantifier), 9223372036854775807UL);
				ByteQuantifiedSize minValue3 = ByteQuantifiedSize.FromSpecifiedUnit(number, quantifier);
				ByteQuantifiedSize maxValue3 = ByteQuantifiedSize.FromSpecifiedUnit(number2, quantifier);
				constraints.Add(ADSchemaDataProvider.GetRangedConstraint<ByteQuantifiedSize>(minValue3, maxValue3, isUnlimited, isNullable));
				return;
			}
			ExTraceGlobals.SchemaInitializationTracer.TraceDebug<DataSyntax, ADPropertyDefinition>(0L, "Unsupported property type '{1}' for data syntax '{0}'.", schemaObject.DataSyntax, propDef);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		private static void GetByteConstraints(ADSchemaAttributeObject schemaObject, ADPropertyDefinition propDef, List<PropertyDefinitionConstraint> constraints)
		{
			if (propDef.Type == typeof(byte[]))
			{
				constraints.Add(new ByteArrayLengthConstraint(schemaObject.RangeLower ?? 0, schemaObject.RangeUpper ?? 0));
				return;
			}
			if (propDef.Type == typeof(string))
			{
				ADSchemaDataProvider.GetStringConstraints(schemaObject, propDef, constraints);
				return;
			}
			ExTraceGlobals.SchemaInitializationTracer.TraceDebug<DataSyntax, ADPropertyDefinition>(0L, "Unsupported property type '{1}' for data syntax '{0}'.", schemaObject.DataSyntax, propDef);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001DF90 File Offset: 0x0001C190
		private static PropertyDefinitionConstraint GetRangedConstraint<T>(T minValue, T maxValue, bool isUnlimited, bool isNullable) where T : struct, IComparable
		{
			PropertyDefinitionConstraint result;
			if (isUnlimited)
			{
				result = new RangedUnlimitedConstraint<T>(minValue, maxValue);
			}
			else if (isNullable)
			{
				result = new RangedNullableValueConstraint<T>(minValue, maxValue);
			}
			else
			{
				result = new RangedValueConstraint<T>(minValue, maxValue);
			}
			return result;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		private void GetPropertiesToUpdate(IList<PropertyDefinition> properties, out List<ADPropertyDefinition> propertiesToRead, out List<ADPropertyDefinition> propertiesToUpdate)
		{
			propertiesToRead = new List<ADPropertyDefinition>(properties.Count);
			propertiesToUpdate = new List<ADPropertyDefinition>(properties.Count);
			for (int i = 0; i < properties.Count; i++)
			{
				ADPropertyDefinition adpropertyDefinition = properties[i] as ADPropertyDefinition;
				if (ADPropertyDefinition.CanHaveAutogeneratedConstraints(adpropertyDefinition) && ADSchemaDataProvider.IsKnownType(adpropertyDefinition.Type))
				{
					propertiesToUpdate.Add(adpropertyDefinition);
					if (!this.constraintDictionary.ContainsKey(ADSchemaDataProvider.PropertyKey.GetPropertyKey(adpropertyDefinition)))
					{
						propertiesToRead.Add(adpropertyDefinition);
					}
				}
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001E044 File Offset: 0x0001C244
		private void UpdateConstraintDictionary(List<ADPropertyDefinition> propertiesToRead)
		{
			if (propertiesToRead.Count > 0)
			{
				this.ReadPropertiesFromAD(propertiesToRead, false);
				Dictionary<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]> dictionary = new Dictionary<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]>(propertiesToRead.Count);
				for (int i = 0; i < propertiesToRead.Count; i++)
				{
					ADPropertyDefinition adpropertyDefinition = propertiesToRead[i];
					ADSchemaAttributeObject schemaObject;
					if (this.schemaAttributeObjectDictionary.TryGetValue(adpropertyDefinition.LdapDisplayName, out schemaObject))
					{
						PropertyDefinitionConstraint[] value = this.GeneratePropertyConstraints(schemaObject, adpropertyDefinition);
						dictionary[ADSchemaDataProvider.PropertyKey.GetPropertyKey(adpropertyDefinition)] = value;
					}
					else
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_CONSTRAINT_READ_FAILED, null, new object[]
						{
							adpropertyDefinition.ToString(),
							adpropertyDefinition.LdapDisplayName
						});
					}
				}
				lock (this.constraintDictionary)
				{
					Dictionary<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]> dictionary2 = new Dictionary<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]>(this.constraintDictionary);
					foreach (KeyValuePair<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]> keyValuePair in dictionary)
					{
						dictionary2[keyValuePair.Key] = keyValuePair.Value;
					}
					this.constraintDictionary = dictionary2;
				}
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001E178 File Offset: 0x0001C378
		private void ReadPropertiesFromAD(List<ADPropertyDefinition> propertiesToRead, bool isAll)
		{
			int num = 0;
			bool flag = isAll || num < propertiesToRead.Count;
			int arg = 0;
			while (flag)
			{
				QueryFilter queryFilter;
				if (isAll)
				{
					queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "attributeSchema");
				}
				else
				{
					HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
					while (hashSet.Count < 20 && num < propertiesToRead.Count)
					{
						string ldapDisplayName = propertiesToRead[num++].LdapDisplayName;
						if (!this.schemaAttributeObjectDictionary.ContainsKey(ldapDisplayName))
						{
							hashSet.Add(ldapDisplayName);
						}
					}
					if (hashSet.Count == 0)
					{
						return;
					}
					arg = hashSet.Count;
					QueryFilter[] array = new QueryFilter[hashSet.Count];
					int num2 = 0;
					foreach (string propertyValue in hashSet)
					{
						array[num2++] = new ComparisonFilter(ComparisonOperator.Equal, ADSchemaAttributeSchema.LdapDisplayName, propertyValue);
					}
					queryFilter = QueryFilter.OrTogether(array);
				}
				ADPagedReader<ADSchemaAttributeObject> adpagedReader = this.schemaSession.FindPaged<ADSchemaAttributeObject>(this.schemaSession.GetSchemaNamingContext(), QueryScope.OneLevel, queryFilter, null, 0);
				int num3 = 0;
				foreach (ADSchemaAttributeObject adschemaAttributeObject in adpagedReader)
				{
					if (!this.schemaAttributeObjectDictionary.ContainsKey(adschemaAttributeObject.LdapDisplayName))
					{
						this.schemaAttributeObjectDictionary.TryAdd(adschemaAttributeObject.LdapDisplayName, adschemaAttributeObject);
					}
					num3++;
				}
				ExTraceGlobals.SchemaInitializationTracer.TraceDebug<int, int, QueryFilter>((long)this.GetHashCode(), "Batch Read: Expected {0} results. Found {1} results. Filter: {2}", arg, num3, queryFilter);
				flag = (!isAll && num < propertiesToRead.Count);
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001E338 File Offset: 0x0001C538
		private PropertyDefinitionConstraint[] GeneratePropertyConstraints(ADSchemaAttributeObject schemaObject, ADPropertyDefinition propDef)
		{
			List<PropertyDefinitionConstraint> list = new List<PropertyDefinitionConstraint>();
			switch (schemaObject.DataSyntax)
			{
			case DataSyntax.Boolean:
			case DataSyntax.Sid:
			case DataSyntax.ObjectIdentifier:
			case DataSyntax.UTCTime:
			case DataSyntax.GeneralizedTime:
			case DataSyntax.Interval:
			case DataSyntax.NTSecDesc:
			case DataSyntax.AccessPoint:
			case DataSyntax.DNBinary:
			case DataSyntax.DNString:
			case DataSyntax.DSDN:
			case DataSyntax.ORName:
			case DataSyntax.PresentationAddress:
			case DataSyntax.ReplicaLink:
				goto IL_BE;
			case DataSyntax.Integer:
			case DataSyntax.Enumeration:
				ADSchemaDataProvider.GetIntegerConstraints(schemaObject, propDef, list);
				goto IL_BE;
			case DataSyntax.Octet:
				ADSchemaDataProvider.GetByteConstraints(schemaObject, propDef, list);
				goto IL_BE;
			case DataSyntax.Numeric:
			case DataSyntax.Printable:
			case DataSyntax.Teletex:
			case DataSyntax.IA5:
			case DataSyntax.CaseSensitive:
			case DataSyntax.Unicode:
				ADSchemaDataProvider.GetStringConstraints(schemaObject, propDef, list);
				goto IL_BE;
			case DataSyntax.LargeInteger:
				ADSchemaDataProvider.GetLargeIntegerConstraints(schemaObject, propDef, list);
				goto IL_BE;
			}
			ExTraceGlobals.SchemaInitializationTracer.TraceDebug<DataSyntax, ADPropertyDefinition>((long)this.GetHashCode(), "Unsupported DataSyntax '{0}' found for property '{1}'.", schemaObject.DataSyntax, propDef);
			IL_BE:
			return list.ToArray();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001E40C File Offset: 0x0001C60C
		internal ADSchemaAttributeObject GetADSchemaAttributeObjectByLdapDisplayName(string ldapDisplayName)
		{
			ADSchemaAttributeObject result;
			this.schemaAttributeObjectDictionary.TryGetValue(ldapDisplayName, out result);
			return result;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001E42C File Offset: 0x0001C62C
		public void LoadAllSchemaAttributeObjects()
		{
			bool flag = this.schemaSession.IsReadConnectionAvailable();
			if (!flag)
			{
				ExTraceGlobals.SchemaInitializationTracer.TraceDebug<bool>((long)this.GetHashCode(), "Aborting Schema Update. IsConnectionAvailable: {1}", flag);
				return;
			}
			lock (this.schemaReloadLock)
			{
				if (!this.isSchemaAttributeObjectDictionaryLoaded)
				{
					this.ReadPropertiesFromAD(new List<ADPropertyDefinition>(), true);
					this.isSchemaAttributeObjectDictionaryLoaded = true;
				}
			}
		}

		// Token: 0x04000236 RID: 566
		private const int ReadBatchSize = 20;

		// Token: 0x04000237 RID: 567
		private static ADSchemaDataProvider instance = new ADSchemaDataProvider();

		// Token: 0x04000238 RID: 568
		private ITopologyConfigurationSession schemaSession;

		// Token: 0x04000239 RID: 569
		private Dictionary<ADSchemaDataProvider.PropertyKey, PropertyDefinitionConstraint[]> constraintDictionary;

		// Token: 0x0400023A RID: 570
		private ConcurrentDictionary<string, ADSchemaAttributeObject> schemaAttributeObjectDictionary;

		// Token: 0x0400023B RID: 571
		private object schemaReloadLock = new object();

		// Token: 0x0400023C RID: 572
		private bool isSchemaAttributeObjectDictionaryLoaded;

		// Token: 0x02000072 RID: 114
		private class PropertyKey
		{
			// Token: 0x0600053A RID: 1338 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
			private PropertyKey(string ldapDisplayName, Type propertyType)
			{
				if (ldapDisplayName == null)
				{
					throw new ArgumentNullException("ldapDisplayName");
				}
				if (null == propertyType)
				{
					throw new ArgumentNullException("propertyType");
				}
				this.ldapDisplayName = ldapDisplayName;
				this.propertyType = propertyType;
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x0001E4EC File Offset: 0x0001C6EC
			public static ADSchemaDataProvider.PropertyKey GetPropertyKey(ADPropertyDefinition propDef)
			{
				if (propDef == null)
				{
					throw new ArgumentNullException("propDef");
				}
				return new ADSchemaDataProvider.PropertyKey(propDef.LdapDisplayName, propDef.Type);
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x0001E50D File Offset: 0x0001C70D
			public static ADSchemaDataProvider.PropertyKey GetPropertyKey(string ldapDisplayName, Type propertyType)
			{
				return new ADSchemaDataProvider.PropertyKey(ldapDisplayName, propertyType);
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x0001E518 File Offset: 0x0001C718
			public override bool Equals(object obj)
			{
				ADSchemaDataProvider.PropertyKey propertyKey = obj as ADSchemaDataProvider.PropertyKey;
				return propertyKey != null && string.Equals(propertyKey.ldapDisplayName, this.ldapDisplayName, StringComparison.OrdinalIgnoreCase) && propertyKey.propertyType == this.propertyType;
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x0001E558 File Offset: 0x0001C758
			public override int GetHashCode()
			{
				if (this.hashCode == 0)
				{
					this.hashCode = (this.ldapDisplayName.GetHashCodeCaseInsensitive() ^ this.propertyType.GetHashCode());
				}
				return this.hashCode;
			}

			// Token: 0x0400023D RID: 573
			private string ldapDisplayName;

			// Token: 0x0400023E RID: 574
			private Type propertyType;

			// Token: 0x0400023F RID: 575
			private int hashCode;
		}
	}
}