using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ABGroup : ABObject
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002EBF File Offset: 0x000010BF
		public ABGroup(ABSession ownerSession) : base(ownerSession, ABGroup.allGroupPropertiesCollection)
		{
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002ECD File Offset: 0x000010CD
		public override ABObjectSchema Schema
		{
			get
			{
				return ABGroup.schema;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002ED4 File Offset: 0x000010D4
		public ABObjectId OwnerId
		{
			get
			{
				return (ABObjectId)base[ABGroupSchema.OwnerId];
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EE6 File Offset: 0x000010E6
		public int? MembersCount
		{
			get
			{
				return (int?)base[ABGroupSchema.MembersCount];
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002EF8 File Offset: 0x000010F8
		public bool? HiddenMembership
		{
			get
			{
				return (bool?)base[ABGroupSchema.HiddenMembership];
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F0A File Offset: 0x0000110A
		protected virtual ABObjectId GetOwnerId()
		{
			return null;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F10 File Offset: 0x00001110
		protected virtual int? GetMembersCount()
		{
			return null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F28 File Offset: 0x00001128
		protected virtual bool? GetHiddenMembership()
		{
			return null;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F40 File Offset: 0x00001140
		protected override bool InternalTryGetValue(ABPropertyDefinition property, out object value)
		{
			if (property == ABGroupSchema.OwnerId)
			{
				value = this.GetOwnerId();
				return true;
			}
			if (property == ABGroupSchema.MembersCount)
			{
				value = this.GetMembersCount();
				return true;
			}
			if (property == ABGroupSchema.HiddenMembership)
			{
				value = this.GetHiddenMembership();
				return true;
			}
			return base.InternalTryGetValue(property, out value);
		}

		// Token: 0x04000023 RID: 35
		private static ABGroupSchema schema = new ABGroupSchema();

		// Token: 0x04000024 RID: 36
		private static ABPropertyDefinitionCollection allGroupPropertiesCollection = ABPropertyDefinitionCollection.FromPropertyDefinitionCollection(ABGroup.schema.AllProperties);
	}
}
