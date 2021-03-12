using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000F0 RID: 240
	internal sealed class DataValidationResult : IDataValidationResult
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001ED9F File Offset: 0x0001CF9F
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0001EDA7 File Offset: 0x0001CFA7
		public PAAValidationResult PAAValidationResult { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001EDB0 File Offset: 0x0001CFB0
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
		public ADRecipient ADRecipient { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001EDC1 File Offset: 0x0001CFC1
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0001EDC9 File Offset: 0x0001CFC9
		public PhoneNumber PhoneNumber { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001EDD2 File Offset: 0x0001CFD2
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0001EDDA File Offset: 0x0001CFDA
		public PersonalContactInfo PersonalContactInfo { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001EDE3 File Offset: 0x0001CFE3
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0001EDEB File Offset: 0x0001CFEB
		public PersonaType PersonaContactInfo { get; set; }

		// Token: 0x060007EB RID: 2027 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		public DataValidationResult()
		{
			this.PAAValidationResult = PAAValidationResult.Valid;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001EE04 File Offset: 0x0001D004
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3},{4}", new object[]
			{
				this.PAAValidationResult,
				(this.ADRecipient != null) ? this.ADRecipient.ToString() : string.Empty,
				(this.PhoneNumber != null) ? this.PhoneNumber.ToString() : string.Empty,
				(this.PersonalContactInfo != null) ? this.PersonalContactInfo.ToString() : string.Empty,
				(this.PersonaContactInfo != null) ? this.PersonaContactInfo.DisplayName : string.Empty
			});
		}
	}
}
