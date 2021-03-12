using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004C8 RID: 1224
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class IPBlockListConfig : MessageHygieneAgentConfig
	{
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x000D87CD File Offset: 0x000D69CD
		public static AsciiString DefaultMachineRejectionResponse
		{
			get
			{
				return IPBlockListConfigSchema.DefaultMachineRejectionResponse;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000D87D4 File Offset: 0x000D69D4
		public static AsciiString DefaultStaticRejectionResponse
		{
			get
			{
				return IPBlockListConfigSchema.DefaultStaticRejectionResponse;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600377C RID: 14204 RVA: 0x000D87DB File Offset: 0x000D69DB
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000D87E3 File Offset: 0x000D69E3
		// (set) Token: 0x0600377E RID: 14206 RVA: 0x000D87F5 File Offset: 0x000D69F5
		[Parameter(Mandatory = false)]
		public AsciiString MachineEntryRejectionResponse
		{
			get
			{
				return (AsciiString)this[IPBlockListConfigSchema.MachineEntryRejectionResponse];
			}
			set
			{
				this[IPBlockListConfigSchema.MachineEntryRejectionResponse] = value;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000D8803 File Offset: 0x000D6A03
		// (set) Token: 0x06003780 RID: 14208 RVA: 0x000D8815 File Offset: 0x000D6A15
		[Parameter(Mandatory = false)]
		public AsciiString StaticEntryRejectionResponse
		{
			get
			{
				return (AsciiString)this[IPBlockListConfigSchema.StaticEntryRejectionResponse];
			}
			set
			{
				this[IPBlockListConfigSchema.StaticEntryRejectionResponse] = value;
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000D8823 File Offset: 0x000D6A23
		internal override ADObjectSchema Schema
		{
			get
			{
				return IPBlockListConfig.schema;
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x000D882A File Offset: 0x000D6A2A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMessageHygieneIPBlockListConfig";
			}
		}

		// Token: 0x04002573 RID: 9587
		public const string CanonicalName = "IPBlockListConfig";

		// Token: 0x04002574 RID: 9588
		private const string MostDerivedClass = "msExchMessageHygieneIPBlockListConfig";

		// Token: 0x04002575 RID: 9589
		private static readonly IPBlockListConfigSchema schema = ObjectSchema.GetInstance<IPBlockListConfigSchema>();
	}
}
