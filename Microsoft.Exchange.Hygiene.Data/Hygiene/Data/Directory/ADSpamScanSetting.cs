using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x02000100 RID: 256
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADSpamScanSetting : ADObject
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x0001E295 File Offset: 0x0001C495
		public ADSpamScanSetting()
		{
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001E29D File Offset: 0x0001C49D
		internal ADSpamScanSetting(IConfigurationSession session, string tenantId)
		{
			this.m_Session = session;
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		internal ADSpamScanSetting(string tenantId)
		{
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0001E2CC File Offset: 0x0001C4CC
		public override ObjectId Identity
		{
			get
			{
				return base.Id;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0001E2E6 File Offset: 0x0001C4E6
		public ObjectId ConfigurationId
		{
			get
			{
				return (ObjectId)this[ADSpamScanSettingSchema.ConfigurationIdProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.ConfigurationIdProp] = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x0001E306 File Offset: 0x0001C506
		[Parameter(Mandatory = false)]
		public byte ActionTypeId
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.ActionTypeIdProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.ActionTypeIdProp] = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0001E319 File Offset: 0x0001C519
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x0001E32B File Offset: 0x0001C52B
		[Parameter(Mandatory = false)]
		public string Parameter
		{
			get
			{
				return (string)this[ADSpamScanSettingSchema.ParameterProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.ParameterProp] = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0001E339 File Offset: 0x0001C539
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x0001E34B File Offset: 0x0001C54B
		[Parameter(Mandatory = false)]
		public byte CsfmImage
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmImageProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmImageProp] = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001E35E File Offset: 0x0001C55E
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0001E370 File Offset: 0x0001C570
		[Parameter(Mandatory = false)]
		public byte CsfmEmpty
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmEmptyProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmEmptyProp] = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001E383 File Offset: 0x0001C583
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0001E395 File Offset: 0x0001C595
		[Parameter(Mandatory = false)]
		public byte CsfmScript
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmScriptProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmScriptProp] = value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0001E3BA File Offset: 0x0001C5BA
		[Parameter(Mandatory = false)]
		public byte CsfmIframe
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmIframeProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmIframeProp] = value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0001E3CD File Offset: 0x0001C5CD
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0001E3DF File Offset: 0x0001C5DF
		[Parameter(Mandatory = false)]
		public byte CsfmObject
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmObjectProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmObjectProp] = value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0001E3F2 File Offset: 0x0001C5F2
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0001E404 File Offset: 0x0001C604
		[Parameter(Mandatory = false)]
		public byte CsfmEmbed
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmEmbedProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmEmbedProp] = value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0001E417 File Offset: 0x0001C617
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0001E429 File Offset: 0x0001C629
		[Parameter(Mandatory = false)]
		public byte CsfmForm
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmFormProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmFormProp] = value;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0001E43C File Offset: 0x0001C63C
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0001E44E File Offset: 0x0001C64E
		[Parameter(Mandatory = false)]
		public byte CsfmWebBugs
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmWebBugsProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmWebBugsProp] = value;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0001E461 File Offset: 0x0001C661
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0001E473 File Offset: 0x0001C673
		[Parameter(Mandatory = false)]
		public byte CsfmWordList
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmWordListProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmWordListProp] = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0001E486 File Offset: 0x0001C686
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0001E498 File Offset: 0x0001C698
		[Parameter(Mandatory = false)]
		public byte CsfmUrlNumericIP
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmUrlNumericIPProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmUrlNumericIPProp] = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0001E4AB File Offset: 0x0001C6AB
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0001E4BD File Offset: 0x0001C6BD
		[Parameter(Mandatory = false)]
		public byte CsfmUrlRedirect
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmUrlRedirectProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmUrlRedirectProp] = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0001E4E2 File Offset: 0x0001C6E2
		[Parameter(Mandatory = false)]
		public byte CsfmWebsite
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmWebsiteProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmWebsiteProp] = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0001E4F5 File Offset: 0x0001C6F5
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0001E507 File Offset: 0x0001C707
		[Parameter(Mandatory = false)]
		public byte CsfmSpfFail
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmSpfFailProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmSpfFailProp] = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001E51A File Offset: 0x0001C71A
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0001E52C File Offset: 0x0001C72C
		[Parameter(Mandatory = false)]
		public byte CsfmSpfFromFail
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmSpfFromFailProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmSpfFromFailProp] = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0001E53F File Offset: 0x0001C73F
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0001E551 File Offset: 0x0001C751
		[Parameter(Mandatory = false)]
		public byte CsfmNdrBackscatter
		{
			get
			{
				return (byte)this[ADSpamScanSettingSchema.CsfmNdrBackScatterProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmNdrBackScatterProp] = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0001E564 File Offset: 0x0001C764
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0001E576 File Offset: 0x0001C776
		[Parameter(Mandatory = false)]
		public SpamScanFlags Flags
		{
			get
			{
				return (SpamScanFlags)this[ADSpamScanSettingSchema.FlagsProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.FlagsProp] = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0001E589 File Offset: 0x0001C789
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0001E59B File Offset: 0x0001C79B
		[Parameter(Mandatory = false)]
		public string CsfmTestBccAddress
		{
			get
			{
				return (string)this[ADSpamScanSettingSchema.CsfmTestBccAddressProp];
			}
			set
			{
				this[ADSpamScanSettingSchema.CsfmTestBccAddressProp] = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0001E5A9 File Offset: 0x0001C7A9
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADSpamScanSetting.schema;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADSpamScanSetting.mostDerivedClass;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001E5B7 File Offset: 0x0001C7B7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001E5BE File Offset: 0x0001C7BE
		internal override bool ShouldValidatePropertyLinkInSameOrganization(ADPropertyDefinition property)
		{
			return false;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0001E5C1 File Offset: 0x0001C7C1
		internal override void InitializeSchema()
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0001E5C3 File Offset: 0x0001C7C3
		protected override void ValidateWrite(List<ValidationError> errors)
		{
		}

		// Token: 0x04000537 RID: 1335
		private static readonly ADSpamScanSettingSchema schema = ObjectSchema.GetInstance<ADSpamScanSettingSchema>();

		// Token: 0x04000538 RID: 1336
		private static string mostDerivedClass = "ADSpamScanSetting";
	}
}
