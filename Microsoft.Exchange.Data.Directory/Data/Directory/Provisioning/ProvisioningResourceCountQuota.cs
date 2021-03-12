using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x0200078D RID: 1933
	[Serializable]
	internal abstract class ProvisioningResourceCountQuota : ProvisioningRule, IProvisioningEnforcement, IProvisioningRule
	{
		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x06006072 RID: 24690 RVA: 0x00147C51 File Offset: 0x00145E51
		public ADPropertyDefinition CountQuotaProperty
		{
			get
			{
				return this.countQuotaProperty;
			}
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x00147C59 File Offset: 0x00145E59
		public string SystemAddressListName
		{
			get
			{
				return this.systemAddressListName;
			}
		}

		// Token: 0x06006074 RID: 24692 RVA: 0x00147C64 File Offset: 0x00145E64
		public ProvisioningResourceCountQuota(ADPropertyDefinition countQuotaProperty, string systemAddressListName, Type[] targetObjectTypes) : base(targetObjectTypes)
		{
			if (countQuotaProperty == null)
			{
				throw new ArgumentNullException("countQuotaProperty");
			}
			if (systemAddressListName == null)
			{
				throw new ArgumentNullException("systemAddressListName");
			}
			if (typeof(Unlimited<int>) != countQuotaProperty.Type)
			{
				throw new ArgumentException("The type of countQuotaProperty is not Unlimited(of int32): dev code bug.");
			}
			this.countQuotaProperty = countQuotaProperty;
			this.systemAddressListName = systemAddressListName;
		}

		// Token: 0x06006075 RID: 24693 RVA: 0x00147CC4 File Offset: 0x00145EC4
		public virtual bool IsApplicable(IConfigurable readOnlyPresentationObject)
		{
			if (readOnlyPresentationObject == null)
			{
				throw new ArgumentNullException("readOnlyPresentationObject");
			}
			if (!base.TargetObjectTypes.Contains(readOnlyPresentationObject.GetType()))
			{
				throw new ArgumentOutOfRangeException("readOnlyPresentationObject");
			}
			return ObjectState.New == readOnlyPresentationObject.ObjectState;
		}

		// Token: 0x06006076 RID: 24694 RVA: 0x00147CFB File Offset: 0x00145EFB
		public virtual ProvisioningValidationError[] Validate(ADProvisioningPolicy enforcementPolicy, IConfigurable readOnlyPresentationObject)
		{
			if (enforcementPolicy == null)
			{
				throw new ArgumentNullException("enforcementPolicy");
			}
			if (readOnlyPresentationObject == null)
			{
				throw new ArgumentNullException("readOnlyPresentationObject");
			}
			if (!base.TargetObjectTypes.Contains(readOnlyPresentationObject.GetType()))
			{
				throw new ArgumentOutOfRangeException("readOnlyPresentationObject");
			}
			return null;
		}

		// Token: 0x040040CD RID: 16589
		private ADPropertyDefinition countQuotaProperty;

		// Token: 0x040040CE RID: 16590
		private string systemAddressListName;
	}
}
