using System;
using System.Collections;
using System.Reflection;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000299 RID: 665
	[Serializable]
	internal class SimpleProviderPropertyDefinition : ProviderPropertyDefinition
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0004B318 File Offset: 0x00049518
		public PropertyDefinitionFlags Flags
		{
			get
			{
				return (PropertyDefinitionFlags)this.flags;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0004B320 File Offset: 0x00049520
		public override bool IsMultivalued
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.MultiValued) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001810 RID: 6160 RVA: 0x0004B330 File Offset: 0x00049530
		public override bool IsReadOnly
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.ReadOnly) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0004B340 File Offset: 0x00049540
		public override bool IsFilterOnly
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.FilterOnly) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x0004B350 File Offset: 0x00049550
		public override bool IsMandatory
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.Mandatory) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0004B361 File Offset: 0x00049561
		public override bool IsCalculated
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.Calculated) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0004B371 File Offset: 0x00049571
		public override bool PersistDefaultValue
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.PersistDefaultValue) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0004B382 File Offset: 0x00049582
		public override bool IsWriteOnce
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.WriteOnce) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0004B393 File Offset: 0x00049593
		public override bool IsBinary
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.Binary) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0004B3A7 File Offset: 0x000495A7
		public override bool IsTaskPopulated
		{
			get
			{
				return (this.Flags & PropertyDefinitionFlags.TaskPopulated) != PropertyDefinitionFlags.None;
			}
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0004B3BC File Offset: 0x000495BC
		private bool IsKnownFailure(Type type)
		{
			return "Microsoft.Exchange.Data.Schedule" == type.FullName || "Microsoft.Exchange.Common.ScheduleInterval[]" == type.FullName || "Microsoft.Exchange.Data.Storage.Management.ADRecipientOrAddress[]" == type.FullName || "System.Collections.Generic.List`1[[Microsoft.Exchange.Monitoring.MonitoringEvent, Microsoft.Exchange.Configuration.ObjectModel, Version=15.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]" == type.FullName || "Microsoft.Exchange.Data.Directory.SystemConfiguration.MessageClassification[]" == type.FullName || "Microsoft.Exchange.Data.ContentAggregation.AggregationSubscriptionIdentity[]" == type.FullName || "System.DirectoryServices.ActiveDirectoryRights[]" == type.FullName || "Microsoft.Exchange.Configuration.Tasks.ExtendedRightIdParameter[]" == type.FullName || "Microsoft.Exchange.Configuration.Tasks.ADSchemaObjectIdParameter[]" == type.FullName || "Microsoft.Exchange.Management.RecipientTasks.MailboxRights[]" == type.FullName || "Microsoft.Exchange.Transport.Sync.Common.Subscription.AggregationSubscriptionIdentity[]" == type.FullName || "Microsoft.Exchange.MessagingPolicies.Rules.Tasks.TransportRulePredicate[]" == type.FullName || "Microsoft.Exchange.MessagingPolicies.Rules.Tasks.TransportRuleAction[]" == type.FullName || "Microsoft.Exchange.Configuration.Tasks.RecipientIdParameter[]" == type.FullName || "Microsoft.Exchange.Data.Word[]" == type.FullName || "Microsoft.Exchange.MessagingPolicies.Rules.Tasks.Pattern[]" == type.FullName || "Microsoft.Exchange.Data.SmtpAddress[]" == type.FullName || "System.String[]" == type.FullName || "Microsoft.Exchange.Management.Tracking.RecipientTrackingEvent[]" == type.FullName;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x0004B540 File Offset: 0x00049740
		internal SimpleProviderPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, ProviderPropertyDefinition[] supportingProperties, CustomFilterBuilderDelegate customFilterBuilderDelegate, GetterDelegate getterDelegate, SetterDelegate setterDelegate) : base(name, versionAdded, type, defaultValue, readConstraints, writeConstraints, supportingProperties, customFilterBuilderDelegate, getterDelegate, setterDelegate)
		{
			this.flags = (int)flags;
			Type left = base.Type.GetTypeInfo().IsGenericType ? base.Type.GetTypeInfo().GetGenericTypeDefinition() : null;
			if (typeof(ICollection).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()) && typeof(byte[]) != type && !this.IsKnownFailure(type))
			{
				if (this.IsMultivalued)
				{
					throw new ArgumentException(base.Name + ": Only specify the element type for MultiValued properties. Type: " + type.FullName, "type");
				}
				throw new ArgumentException(base.Name + ": Instead of specifying a collection type, please mark the property definition as MultiValued and specify the element type as the type Type: " + type.FullName, "type");
			}
			else
			{
				if (this.IsMultivalued && base.DefaultValue != null)
				{
					throw new ArgumentException(base.Name + ": Multivalued properties should not have default value", "defaultValue");
				}
				if (base.DefaultValue == null && this.PersistDefaultValue)
				{
					throw new ArgumentException(base.Name + ": Cannot persist default value when no default value is specified.", "defaultValue");
				}
				if (this.IsTaskPopulated)
				{
					if (this.IsFilterOnly)
					{
						throw new ArgumentException(base.Name + ": TaskPopulated properties are not supported as FilterOnly properties at this time.", "flags");
					}
					if (this.IsCalculated)
					{
						throw new ArgumentException(base.Name + ": TaskPopulated properties should not be marked as calculated.", "flags");
					}
					if (this.IsReadOnly || this.IsWriteOnce)
					{
						throw new ArgumentException(base.Name + ": TaskPopulated properties must be modifiable from within the task and cannot be marked ReadOnly or WriteOnce.", "flags");
					}
				}
				if (this.IsWriteOnce && this.IsReadOnly)
				{
					throw new ArgumentException(base.Name + ": Properties cannot be marked as both ReadOnly and WriteOnce.", "flags");
				}
				if (this.IsMandatory && this.IsFilterOnly)
				{
					throw new ArgumentException(base.Name + ": Mandatory properties should not be marked FilterOnly.", "flags");
				}
				if (this.IsFilterOnly && this.IsCalculated)
				{
					throw new ArgumentException(base.Name + ": Calculated properties should not be marked as FilterOnly.", "flags");
				}
				if (!this.IsMultivalued && !this.IsFilterOnly && !this.IsCalculated)
				{
					Type underlyingType = Nullable.GetUnderlyingType(type);
					if (base.Type.GetTypeInfo().IsValueType && left != typeof(Nullable<>) && base.DefaultValue == null)
					{
						throw new ArgumentException(base.Name + ": Value type properties must have default value.", "defaultValue");
					}
					if (left == typeof(Nullable<>) && base.DefaultValue != null)
					{
						throw new ArgumentException(base.Name + ": Default value for a property of Nullable type must be 'null'.", "defaultValue");
					}
					if (null != underlyingType && underlyingType.GetTypeInfo().IsGenericType && underlyingType.GetGenericTypeDefinition() == typeof(Unlimited<>))
					{
						throw new ArgumentException(base.Name + ": Properties cannot be both Unlimited and Nullable.", "type");
					}
					if (left == typeof(Unlimited<>) && !(bool)base.Type.GetTypeInfo().GetDeclaredProperty("IsUnlimited").GetValue(base.DefaultValue, null))
					{
						throw new ArgumentException(base.Name + ": Default value for a property of Unlimited type must be 'unlimited'.", "defaultValue");
					}
					if (left == typeof(Unlimited<>) && this.PersistDefaultValue)
					{
						throw new ArgumentException(base.Name + ": Cannot persist default value for Unlimited type.", "flags");
					}
				}
				if (this.IsCalculated != (base.GetterDelegate != null))
				{
					throw new ArgumentException(base.Name + ": Calculated properties must have GetterDelegate, non-calculated ones must not", "getterDelegate");
				}
				if ((this.IsCalculated && !this.IsReadOnly) != (base.SetterDelegate != null))
				{
					throw new ArgumentException(base.Name + ": Writable calculated properties must have SetterDelegate, non-calculated & readonly ones must not", "setterDelegate");
				}
				if (this.IsCalculated != (base.SupportingProperties.Count != 0))
				{
					throw new ArgumentException(base.Name + ": Calculated properties must have supporting properties, non-calculated ones must not", "supportingProperties");
				}
				if (this.IsReadOnly && writeConstraints.Length > 0)
				{
					throw new ArgumentException(base.Name + ": Readonly properties should not have write-only constraints", "writeConstraints");
				}
				if (base.DefaultValue != null && !this.PersistDefaultValue && !this.IsCalculated && !this.IsTaskPopulated && (base.Type == typeof(int) || base.Type == typeof(uint) || base.Type == typeof(long) || base.Type == typeof(ulong) || base.Type == typeof(ByteQuantifiedSize) || base.Type == typeof(EnhancedTimeSpan) || base.Type == typeof(DateTime)))
				{
					throw new ArgumentException(string.Format("Property {0} has type {1} and a default value that is not persisted", base.Name, base.Type.ToString()), "flags");
				}
				if (this.IsCalculated && base.Type.GetTypeInfo().IsValueType && left != typeof(Nullable<>) && base.DefaultValue == null && !this.IsMultivalued)
				{
					throw new ArgumentException(string.Format("Calculated property {0} has type {1} and no default value", base.Name, base.Type.ToString()), "flags");
				}
				return;
			}
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0004BAFC File Offset: 0x00049CFC
		public SimpleProviderPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, PropertyDefinitionFlags flags, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : this(name, versionAdded, type, flags, defaultValue, readConstraints, writeConstraints, SimpleProviderPropertyDefinition.None, null, null, null)
		{
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0004BB22 File Offset: 0x00049D22
		public override bool Equals(ProviderPropertyDefinition other)
		{
			return object.ReferenceEquals(other, this) || (base.Equals(other) && (other as SimpleProviderPropertyDefinition).Flags == this.Flags);
		}

		// Token: 0x04000E3B RID: 3643
		public new static SimpleProviderPropertyDefinition[] None = new SimpleProviderPropertyDefinition[0];

		// Token: 0x04000E3C RID: 3644
		private readonly int flags;
	}
}
