using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C86 RID: 3206
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class JsonRequestHeaders
	{
		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x060056EE RID: 22254 RVA: 0x00111763 File Offset: 0x0010F963
		// (set) Token: 0x060056EF RID: 22255 RVA: 0x0011176B File Offset: 0x0010F96B
		[IgnoreDataMember]
		public ExchangeVersionType RequestServerVersion { get; set; }

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x060056F0 RID: 22256 RVA: 0x00111774 File Offset: 0x0010F974
		// (set) Token: 0x060056F1 RID: 22257 RVA: 0x00111781 File Offset: 0x0010F981
		[DataMember(Name = "RequestServerVersion", IsRequired = true)]
		private string RequestServerVersionString
		{
			get
			{
				return EnumUtilities.ToString<ExchangeVersionType>(this.RequestServerVersion);
			}
			set
			{
				this.RequestServerVersion = EnumUtilities.Parse<ExchangeVersionType>(value);
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x060056F2 RID: 22258 RVA: 0x0011178F File Offset: 0x0010F98F
		// (set) Token: 0x060056F3 RID: 22259 RVA: 0x00111797 File Offset: 0x0010F997
		[DataMember(Name = "MailboxCulture", IsRequired = false)]
		public string MailboxCulture { get; set; }

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x060056F4 RID: 22260 RVA: 0x001117A0 File Offset: 0x0010F9A0
		// (set) Token: 0x060056F5 RID: 22261 RVA: 0x001117A8 File Offset: 0x0010F9A8
		[DataMember(Name = "ExchangeImpersonation", IsRequired = false)]
		public ExchangeImpersonationType ExchangeImpersonation { get; set; }

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x060056F6 RID: 22262 RVA: 0x001117B1 File Offset: 0x0010F9B1
		// (set) Token: 0x060056F7 RID: 22263 RVA: 0x001117B9 File Offset: 0x0010F9B9
		[DataMember(Name = "SerializedSecurityContext", IsRequired = false)]
		public SerializedSecurityContextType SerializedSecurityContext { get; set; }

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x060056F8 RID: 22264 RVA: 0x001117C2 File Offset: 0x0010F9C2
		// (set) Token: 0x060056F9 RID: 22265 RVA: 0x001117CA File Offset: 0x0010F9CA
		[DataMember(Name = "TimeZoneContext", IsRequired = false)]
		public TimeZoneContextType TimeZoneContext { get; set; }

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x001117D3 File Offset: 0x0010F9D3
		// (set) Token: 0x060056FB RID: 22267 RVA: 0x001117DB File Offset: 0x0010F9DB
		[IgnoreDataMember]
		public DateTimePrecision DateTimePrecision { get; set; }

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x001117E4 File Offset: 0x0010F9E4
		// (set) Token: 0x060056FD RID: 22269 RVA: 0x001117F1 File Offset: 0x0010F9F1
		[DataMember(Name = "DateTimePrecision", IsRequired = false)]
		public string DateTimePrecisionString
		{
			get
			{
				return EnumUtilities.ToString<DateTimePrecision>(this.DateTimePrecision);
			}
			set
			{
				this.DateTimePrecision = EnumUtilities.Parse<DateTimePrecision>(value);
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x001117FF File Offset: 0x0010F9FF
		// (set) Token: 0x060056FF RID: 22271 RVA: 0x00111807 File Offset: 0x0010FA07
		[DataMember(Name = "ManagementRole", IsRequired = false, EmitDefaultValue = false)]
		public ManagementRoleType ManagementRole { get; set; }

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x00111810 File Offset: 0x0010FA10
		// (set) Token: 0x06005701 RID: 22273 RVA: 0x00111818 File Offset: 0x0010FA18
		[DataMember(Name = "BackgroundLoad", IsRequired = false, EmitDefaultValue = false)]
		public bool BackgroundLoad { get; set; }
	}
}
