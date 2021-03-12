using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x020000E7 RID: 231
	[DataContract]
	public abstract class PolicyConfigurationBase
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x00013CEA File Offset: 0x00011EEA
		public PolicyConfigurationBase(ConfigurationObjectType objectType)
		{
			this.ObjectType = objectType;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00013CF9 File Offset: 0x00011EF9
		public PolicyConfigurationBase(ConfigurationObjectType objectType, Guid tenantId, Guid objectId) : this(objectType)
		{
			this.TenantId = tenantId;
			this.ObjectId = objectId;
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00013D10 File Offset: 0x00011F10
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00013D18 File Offset: 0x00011F18
		[DataMember]
		public ConfigurationObjectType ObjectType { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00013D21 File Offset: 0x00011F21
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00013D29 File Offset: 0x00011F29
		[DataMember]
		public Guid TenantId { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00013D32 File Offset: 0x00011F32
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x00013D3A File Offset: 0x00011F3A
		[DataMember]
		public Guid ObjectId { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00013D43 File Offset: 0x00011F43
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00013D4B File Offset: 0x00011F4B
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00013D54 File Offset: 0x00011F54
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00013D5C File Offset: 0x00011F5C
		[DataMember]
		public ChangeType ChangeType { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00013D65 File Offset: 0x00011F65
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x00013D6D File Offset: 0x00011F6D
		[DataMember]
		public Workload Workload { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00013D76 File Offset: 0x00011F76
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x00013D7E File Offset: 0x00011F7E
		[DataMember]
		public DateTime? WhenCreatedUTC { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00013D87 File Offset: 0x00011F87
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x00013D8F File Offset: 0x00011F8F
		[DataMember]
		public DateTime? WhenChangedUTC { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00013D98 File Offset: 0x00011F98
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x00013DA0 File Offset: 0x00011FA0
		[DataMember]
		public PolicyVersion Version { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00013DA9 File Offset: 0x00011FA9
		protected static IDictionary<string, string> BasePropertyNameMapping
		{
			get
			{
				return PolicyConfigurationBase.basePropertyNameMapping;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00013DB0 File Offset: 0x00011FB0
		protected virtual IDictionary<string, string> PropertyNameMapping
		{
			get
			{
				return PolicyConfigurationBase.basePropertyNameMapping;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00013DB8 File Offset: 0x00011FB8
		public virtual void MergeInto(PolicyConfigBase originalObject, bool isObjectSync, PolicyConfigProvider policyConfigProvider = null)
		{
			ArgumentValidator.ThrowIfNull("originalObject", originalObject);
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(bindingAttr))
			{
				string name = propertyInfo.Name;
				if (this.PropertyNameMapping.ContainsKey(name))
				{
					string name2 = this.PropertyNameMapping[name];
					MethodInfo setMethod = originalObject.GetType().GetProperty(name2, bindingAttr).GetSetMethod();
					if (propertyInfo.PropertyType.Name == typeof(IncrementalAttribute<>).Name)
					{
						object obj;
						if (Utility.GetIncrementalProperty(this, name, out obj))
						{
							setMethod.Invoke(originalObject, new object[]
							{
								obj
							});
						}
					}
					else
					{
						object value = propertyInfo.GetValue(this);
						setMethod.Invoke(originalObject, new object[]
						{
							value
						});
					}
				}
			}
		}

		// Token: 0x040003AE RID: 942
		private static IDictionary<string, string> basePropertyNameMapping = new Dictionary<string, string>
		{
			{
				"Workload",
				PolicyConfigBaseSchema.Workload
			},
			{
				"Name",
				PolicyConfigBaseSchema.Name
			},
			{
				"Version",
				PolicyConfigBaseSchema.Version
			}
		};
	}
}
