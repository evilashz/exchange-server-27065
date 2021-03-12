using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000344 RID: 836
	[Serializable]
	public class AddressTemplate : ADConfigurationObject
	{
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000A4EC0 File Offset: 0x000A30C0
		internal override ADObjectSchema Schema
		{
			get
			{
				return AddressTemplate.schema;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x000A4EC7 File Offset: 0x000A30C7
		internal override string MostDerivedObjectClass
		{
			get
			{
				return AddressTemplate.mostDerivedClass;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000A4ECE File Offset: 0x000A30CE
		public string DisplayName
		{
			get
			{
				return (string)this[AddressTemplateSchema.DisplayName];
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000A4EE0 File Offset: 0x000A30E0
		public byte[] AddressSyntax
		{
			get
			{
				return (byte[])this[AddressTemplateSchema.AddressSyntax];
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060026E2 RID: 9954 RVA: 0x000A4EF2 File Offset: 0x000A30F2
		public string AddressType
		{
			get
			{
				return (string)this[AddressTemplateSchema.AddressType];
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x000A4F04 File Offset: 0x000A3104
		public byte[] PerMsgDialogDisplayTable
		{
			get
			{
				return (byte[])this[AddressTemplateSchema.PerMsgDialogDisplayTable];
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000A4F16 File Offset: 0x000A3116
		public byte[] PerRecipDialogDisplayTable
		{
			get
			{
				return (byte[])this[AddressTemplateSchema.PerRecipDialogDisplayTable];
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000A4F28 File Offset: 0x000A3128
		public bool ProxyGenerationEnabled
		{
			get
			{
				return (bool)this[AddressTemplateSchema.ProxyGenerationEnabled];
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000A4F3A File Offset: 0x000A313A
		public byte[] TemplateBlob
		{
			get
			{
				return (byte[])this[AddressTemplateSchema.TemplateBlob];
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000A4F4C File Offset: 0x000A314C
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[AddressTemplateSchema.ExchangeLegacyDN];
			}
		}

		// Token: 0x040017BB RID: 6075
		internal static readonly ADObjectId ContainerId = new ADObjectId("CN=Address-Templates,CN=Addressing");

		// Token: 0x040017BC RID: 6076
		private static AddressTemplateSchema schema = ObjectSchema.GetInstance<AddressTemplateSchema>();

		// Token: 0x040017BD RID: 6077
		private static string mostDerivedClass = "addressTemplate";
	}
}
