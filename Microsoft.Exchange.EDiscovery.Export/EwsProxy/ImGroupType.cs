using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000DA RID: 218
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ImGroupType
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002052C File Offset: 0x0001E72C
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x00020534 File Offset: 0x0001E734
		public string DisplayName
		{
			get
			{
				return this.displayNameField;
			}
			set
			{
				this.displayNameField = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002053D File Offset: 0x0001E73D
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00020545 File Offset: 0x0001E745
		public string GroupType
		{
			get
			{
				return this.groupTypeField;
			}
			set
			{
				this.groupTypeField = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002054E File Offset: 0x0001E74E
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00020556 File Offset: 0x0001E756
		public ItemIdType ExchangeStoreId
		{
			get
			{
				return this.exchangeStoreIdField;
			}
			set
			{
				this.exchangeStoreIdField = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x0002055F File Offset: 0x0001E75F
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x00020567 File Offset: 0x0001E767
		[XmlArrayItem("ItemId", IsNullable = false)]
		public ItemIdType[] MemberCorrelationKey
		{
			get
			{
				return this.memberCorrelationKeyField;
			}
			set
			{
				this.memberCorrelationKeyField = value;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00020570 File Offset: 0x0001E770
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x00020578 File Offset: 0x0001E778
		public NonEmptyArrayOfExtendedPropertyType ExtendedProperties
		{
			get
			{
				return this.extendedPropertiesField;
			}
			set
			{
				this.extendedPropertiesField = value;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00020581 File Offset: 0x0001E781
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x00020589 File Offset: 0x0001E789
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddressField;
			}
			set
			{
				this.smtpAddressField = value;
			}
		}

		// Token: 0x040005DE RID: 1502
		private string displayNameField;

		// Token: 0x040005DF RID: 1503
		private string groupTypeField;

		// Token: 0x040005E0 RID: 1504
		private ItemIdType exchangeStoreIdField;

		// Token: 0x040005E1 RID: 1505
		private ItemIdType[] memberCorrelationKeyField;

		// Token: 0x040005E2 RID: 1506
		private NonEmptyArrayOfExtendedPropertyType extendedPropertiesField;

		// Token: 0x040005E3 RID: 1507
		private string smtpAddressField;
	}
}
