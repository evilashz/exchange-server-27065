using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	internal class AdminPropertyDefinition : ProviderPropertyDefinition
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public AdminPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, object defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : base(name, versionAdded, type, defaultValue, readConstraints, writeConstraints)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E1 File Offset: 0x000002E1
		public AdminPropertyDefinition(string name, ExchangeObjectVersion versionAdded, Type type, object defaultValue, bool mandatory, bool multiValued, bool readOnly, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints) : base(name, versionAdded, type, defaultValue, readConstraints, writeConstraints)
		{
			this.isMandatory = mandatory;
			this.isMultivalued = multiValued;
			this.isReadOnly = readOnly;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000210A File Offset: 0x0000030A
		public override bool IsMandatory
		{
			get
			{
				return this.isMandatory;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002112 File Offset: 0x00000312
		public override bool IsMultivalued
		{
			get
			{
				return this.isMultivalued;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000211A File Offset: 0x0000031A
		public override bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002122 File Offset: 0x00000322
		public override bool IsCalculated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002125 File Offset: 0x00000325
		public override bool IsFilterOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public override bool IsWriteOnce
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212B File Offset: 0x0000032B
		public override bool PersistDefaultValue
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000212E File Offset: 0x0000032E
		public override bool IsBinary
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000001 RID: 1
		private bool isMandatory;

		// Token: 0x04000002 RID: 2
		private bool isMultivalued;

		// Token: 0x04000003 RID: 3
		private bool isReadOnly;
	}
}
