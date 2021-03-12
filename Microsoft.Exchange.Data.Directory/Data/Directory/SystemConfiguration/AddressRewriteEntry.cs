using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000342 RID: 834
	[Serializable]
	public sealed class AddressRewriteEntry : ADConfigurationObject
	{
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x000A4C92 File Offset: 0x000A2E92
		internal override ADObjectSchema Schema
		{
			get
			{
				return AddressRewriteEntry.schema;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x000A4C99 File Offset: 0x000A2E99
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AddressRewriteEntry.mostDerivedClass;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x000A4CA8 File Offset: 0x000A2EA8
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x000A4CBA File Offset: 0x000A2EBA
		[Parameter(Mandatory = false)]
		public string InternalAddress
		{
			get
			{
				return (string)this[AddressRewriteEntrySchema.InternalAddress];
			}
			set
			{
				this[AddressRewriteEntrySchema.InternalAddress] = value;
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x000A4CC8 File Offset: 0x000A2EC8
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x000A4CDA File Offset: 0x000A2EDA
		[Parameter(Mandatory = false)]
		public string ExternalAddress
		{
			get
			{
				return (string)this[AddressRewriteEntrySchema.ExternalAddress];
			}
			set
			{
				this[AddressRewriteEntrySchema.ExternalAddress] = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x000A4CE8 File Offset: 0x000A2EE8
		// (set) Token: 0x060026D8 RID: 9944 RVA: 0x000A4CFA File Offset: 0x000A2EFA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExceptionList
		{
			get
			{
				return (MultiValuedProperty<string>)this[AddressRewriteEntrySchema.ExceptionList];
			}
			set
			{
				this[AddressRewriteEntrySchema.ExceptionList] = value;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x000A4D08 File Offset: 0x000A2F08
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x000A4D1D File Offset: 0x000A2F1D
		[Parameter(Mandatory = false)]
		public bool OutboundOnly
		{
			get
			{
				return (EntryDirection)this[AddressRewriteEntrySchema.Direction] == EntryDirection.OutboundOnly;
			}
			set
			{
				this[AddressRewriteEntrySchema.Direction] = (value ? EntryDirection.OutboundOnly : EntryDirection.Bidirectional);
			}
		}

		// Token: 0x040017B1 RID: 6065
		private static AddressRewriteEntrySchema schema = ObjectSchema.GetInstance<AddressRewriteEntrySchema>();

		// Token: 0x040017B2 RID: 6066
		private static string mostDerivedClass = "msExchAddressRewriteEntry";
	}
}
