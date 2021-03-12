using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Data.Directory.Provisioning
{
	// Token: 0x02000789 RID: 1929
	internal abstract class ProvisioningRule : IProvisioningRule
	{
		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x06006061 RID: 24673 RVA: 0x00147A34 File Offset: 0x00145C34
		public ICollection<Type> TargetObjectTypes
		{
			get
			{
				return this.targetObjectTypes;
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x06006062 RID: 24674 RVA: 0x00147A3C File Offset: 0x00145C3C
		// (set) Token: 0x06006063 RID: 24675 RVA: 0x00147A44 File Offset: 0x00145C44
		public ProvisioningContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Context");
				}
				this.context = value;
			}
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x00147A5B File Offset: 0x00145C5B
		public ProvisioningRule(Type[] targetObjectTypes)
		{
			if (targetObjectTypes == null || targetObjectTypes.Length == 0)
			{
				throw new ArgumentNullException("targetObjectTypes");
			}
			this.targetObjectTypes = new ReadOnlyCollection<Type>(targetObjectTypes);
		}

		// Token: 0x040040C8 RID: 16584
		private ReadOnlyCollection<Type> targetObjectTypes;

		// Token: 0x040040C9 RID: 16585
		private ProvisioningContext context;
	}
}
