using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x0200078C RID: 1932
	[Serializable]
	internal class ProvisioningPropertyTemplate : ProvisioningPropertyBase, IProvisioningTemplate, IProvisioningRule
	{
		// Token: 0x0600606D RID: 24685 RVA: 0x00147AD5 File Offset: 0x00145CD5
		public ProvisioningPropertyTemplate(ADPropertyDefinition policyProperty, ProviderPropertyDefinition objectProperty, ValueConverterDelegate valueConverter, Type[] targetObjectTypes) : base(policyProperty, objectProperty, valueConverter, targetObjectTypes)
		{
			if (objectProperty.IsReadOnly)
			{
				throw new ArgumentException(string.Format("objectProperty '{0}' is read only.", objectProperty.Name));
			}
		}

		// Token: 0x0600606E RID: 24686 RVA: 0x00147B00 File Offset: 0x00145D00
		public ProvisioningPropertyTemplate(ADPropertyDefinition policyProperty, ProviderPropertyDefinition objectProperty, ValueConverterDelegate valueConverter, Type targetObjectType) : this(policyProperty, objectProperty, valueConverter, new Type[]
		{
			targetObjectType
		})
		{
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x00147B24 File Offset: 0x00145D24
		public virtual void Provision(ADProvisioningPolicy templatePolicy, IConfigurable writablePresentationObject)
		{
			if (templatePolicy == null)
			{
				throw new ArgumentNullException("templatePolicy");
			}
			if (writablePresentationObject == null)
			{
				throw new ArgumentNullException("writablePresentationObject");
			}
			if (!base.TargetObjectTypes.Contains(writablePresentationObject.GetType()))
			{
				throw new ArgumentOutOfRangeException("writablePresentationObject");
			}
			if (!(writablePresentationObject is ADObject))
			{
				throw new ArgumentOutOfRangeException("writablePresentationObject");
			}
			object obj = templatePolicy[base.PolicyProperty];
			if (!this.IsNullOrUnlimited(obj) && !this.IsNullOrEmptyMvp(obj))
			{
				if (base.ValueConverter != null)
				{
					obj = base.ValueConverter(obj);
				}
				((ADObject)writablePresentationObject)[base.ObjectProperty] = obj;
				return;
			}
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x00147BC8 File Offset: 0x00145DC8
		protected bool IsNullOrUnlimited(object templateValue)
		{
			return templateValue == null || string.Empty.Equals(templateValue) || (base.PolicyProperty.Type.IsGenericType && base.PolicyProperty.Type.GetGenericTypeDefinition() == typeof(Unlimited<>) && templateValue.Equals(base.PolicyProperty.DefaultValue));
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x00147C32 File Offset: 0x00145E32
		protected bool IsNullOrEmptyMvp(object templateValue)
		{
			return base.PolicyProperty.IsMultivalued && MultiValuedPropertyBase.IsNullOrEmpty((MultiValuedPropertyBase)templateValue);
		}
	}
}
