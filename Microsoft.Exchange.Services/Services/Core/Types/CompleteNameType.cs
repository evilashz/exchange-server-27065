using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005AE RID: 1454
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class CompleteNameType
	{
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000AED12 File Offset: 0x000ACF12
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x000AED1A File Offset: 0x000ACF1A
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Title { get; set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000AED23 File Offset: 0x000ACF23
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x000AED2B File Offset: 0x000ACF2B
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string FirstName { get; set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000AED34 File Offset: 0x000ACF34
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000AED3C File Offset: 0x000ACF3C
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string MiddleName { get; set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000AED45 File Offset: 0x000ACF45
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x000AED4D File Offset: 0x000ACF4D
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string LastName { get; set; }

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000AED56 File Offset: 0x000ACF56
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x000AED5E File Offset: 0x000ACF5E
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string Suffix { get; set; }

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000AED67 File Offset: 0x000ACF67
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x000AED6F File Offset: 0x000ACF6F
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string Initials { get; set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000AED78 File Offset: 0x000ACF78
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000AED80 File Offset: 0x000ACF80
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string FullName { get; set; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000AED89 File Offset: 0x000ACF89
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x000AED91 File Offset: 0x000ACF91
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public string Nickname { get; set; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000AED9A File Offset: 0x000ACF9A
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x000AEDA2 File Offset: 0x000ACFA2
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public string YomiFirstName { get; set; }

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000AEDAB File Offset: 0x000ACFAB
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x000AEDB3 File Offset: 0x000ACFB3
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public string YomiLastName { get; set; }
	}
}
