using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200061B RID: 1563
	[Serializable]
	public sealed class Pop3AdConfiguration : PopImapAdConfiguration
	{
		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06004A22 RID: 18978 RVA: 0x001124D9 File Offset: 0x001106D9
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06004A23 RID: 18979 RVA: 0x001124E1 File Offset: 0x001106E1
		internal override ADObjectSchema Schema
		{
			get
			{
				return Pop3AdConfiguration.schema;
			}
		}

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x001124E8 File Offset: 0x001106E8
		internal override string MostDerivedObjectClass
		{
			get
			{
				return Pop3AdConfiguration.MostDerivedClass;
			}
		}

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x001124EF File Offset: 0x001106EF
		public override string ProtocolName
		{
			get
			{
				return "POP3";
			}
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x001124F6 File Offset: 0x001106F6
		// (set) Token: 0x06004A27 RID: 18983 RVA: 0x00112508 File Offset: 0x00110708
		[Parameter(Mandatory = false)]
		public override int MaxCommandSize
		{
			get
			{
				return (int)this[Pop3AdConfigurationSchema.MaxCommandSize];
			}
			set
			{
				this[Pop3AdConfigurationSchema.MaxCommandSize] = value;
			}
		}

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x0011251B File Offset: 0x0011071B
		// (set) Token: 0x06004A29 RID: 18985 RVA: 0x0011252D File Offset: 0x0011072D
		[Parameter(Mandatory = false)]
		public SortOrder MessageRetrievalSortOrder
		{
			get
			{
				return (SortOrder)this[Pop3AdConfigurationSchema.MessageRetrievalSortOrder];
			}
			set
			{
				this[Pop3AdConfigurationSchema.MessageRetrievalSortOrder] = value;
			}
		}

		// Token: 0x06004A2A RID: 18986 RVA: 0x00112540 File Offset: 0x00110740
		internal static object MessageRetrievalSortOrderGetter(IPropertyBag propertyBag)
		{
			int num = 1;
			ADPropertyDefinition popImapFlags = PopImapAdConfigurationSchema.PopImapFlags;
			if ((num & (int)propertyBag[popImapFlags]) != 0)
			{
				return SortOrder.Descending;
			}
			return SortOrder.Ascending;
		}

		// Token: 0x06004A2B RID: 18987 RVA: 0x00112574 File Offset: 0x00110774
		internal static void MessageRetrievalSortOrderSetter(object value, IPropertyBag propertyBag)
		{
			int num = 1;
			ADPropertyDefinition popImapFlags = PopImapAdConfigurationSchema.PopImapFlags;
			if ((SortOrder)value == SortOrder.Descending)
			{
				propertyBag[popImapFlags] = ((int)propertyBag[popImapFlags] | num);
				return;
			}
			propertyBag[popImapFlags] = ((int)propertyBag[popImapFlags] & ~num);
		}

		// Token: 0x04003356 RID: 13142
		private static Pop3AdConfigurationSchema schema = ObjectSchema.GetInstance<Pop3AdConfigurationSchema>();

		// Token: 0x04003357 RID: 13143
		public static string MostDerivedClass = "protocolCfgPOPServer";
	}
}
