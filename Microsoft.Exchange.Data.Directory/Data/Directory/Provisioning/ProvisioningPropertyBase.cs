using System;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x0200078B RID: 1931
	[Serializable]
	internal abstract class ProvisioningPropertyBase : ProvisioningRule
	{
		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x00147A82 File Offset: 0x00145C82
		public ProviderPropertyDefinition ObjectProperty
		{
			get
			{
				return this.objectProperty;
			}
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x0600606A RID: 24682 RVA: 0x00147A8A File Offset: 0x00145C8A
		public ValueConverterDelegate ValueConverter
		{
			get
			{
				return this.valueConverter;
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x0600606B RID: 24683 RVA: 0x00147A92 File Offset: 0x00145C92
		public ADPropertyDefinition PolicyProperty
		{
			get
			{
				return this.policyProperty;
			}
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x00147A9A File Offset: 0x00145C9A
		public ProvisioningPropertyBase(ADPropertyDefinition policyProperty, ProviderPropertyDefinition objectProperty, ValueConverterDelegate valueConverter, Type[] targetObjectTypes) : base(targetObjectTypes)
		{
			if (policyProperty == null)
			{
				throw new ArgumentNullException("policyProperty");
			}
			if (objectProperty == null)
			{
				throw new ArgumentNullException("objectProperty");
			}
			this.policyProperty = policyProperty;
			this.valueConverter = valueConverter;
			this.objectProperty = objectProperty;
		}

		// Token: 0x040040CA RID: 16586
		private ADPropertyDefinition policyProperty;

		// Token: 0x040040CB RID: 16587
		private ProviderPropertyDefinition objectProperty;

		// Token: 0x040040CC RID: 16588
		private ValueConverterDelegate valueConverter;
	}
}
