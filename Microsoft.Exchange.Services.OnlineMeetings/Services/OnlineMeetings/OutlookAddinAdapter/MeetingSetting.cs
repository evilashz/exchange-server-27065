using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000BD RID: 189
	[XmlType("Settings")]
	[DataContract(Name = "Settings")]
	[KnownType(typeof(MeetingSetting))]
	public class MeetingSetting
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000B263 File Offset: 0x00009463
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000B26B File Offset: 0x0000946B
		[DataMember(Name = "Public")]
		[XmlElement("Public")]
		public bool IsPublic { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000B274 File Offset: 0x00009474
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000B27C File Offset: 0x0000947C
		[XmlElement("ConferenceID")]
		[DataMember(Name = "ConferenceID")]
		public string ConferenceID { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000B285 File Offset: 0x00009485
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0000B28D File Offset: 0x0000948D
		[DataMember(Name = "HttpJoinLink")]
		[XmlElement("HttpJoinLink")]
		public string HttpJoinLink { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000B296 File Offset: 0x00009496
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000B29E File Offset: 0x0000949E
		[DataMember(Name = "ConfJoinLink")]
		[XmlElement("ConfJoinLink")]
		public string ConfJoinLink { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000B2A7 File Offset: 0x000094A7
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000B2AF File Offset: 0x000094AF
		[DataMember(Name = "Subject", EmitDefaultValue = true)]
		[XmlElement("Subject")]
		public string Subject { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000B2B8 File Offset: 0x000094B8
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000B2C0 File Offset: 0x000094C0
		[XmlIgnore]
		public DateTime? ExpiryDate { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000B2CC File Offset: 0x000094CC
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000B310 File Offset: 0x00009510
		[XmlElement("ExpiryDate")]
		[DataMember(Name = "ExpiryDate", EmitDefaultValue = false)]
		public string ExpiryDateString
		{
			get
			{
				if (this.ExpiryDate == null)
				{
					return string.Empty;
				}
				return this.ExpiryDate.Value.ToString(CultureInfo.InvariantCulture.DateTimeFormat);
			}
			set
			{
				DateTime value2;
				if (DateTime.TryParse(value, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out value2))
				{
					this.ExpiryDate = new DateTime?(value2);
					return;
				}
				this.ExpiryDate = null;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000B34E File Offset: 0x0000954E
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000B356 File Offset: 0x00009556
		[XmlElement("AutoPromote")]
		[DataMember(Name = "AutoPromote")]
		public AutoPromote AutoPromote { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000B35F File Offset: 0x0000955F
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000B367 File Offset: 0x00009567
		[DataMember(Name = "BodyLanguage")]
		[XmlElement("BodyLanguage")]
		public string BodyLanguage { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000B370 File Offset: 0x00009570
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000B378 File Offset: 0x00009578
		[XmlElement("Participants")]
		[DataMember(Name = "Participants")]
		public Participants Participants { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000B381 File Offset: 0x00009581
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000B389 File Offset: 0x00009589
		[XmlElement("Permissions")]
		[DataMember(Name = "Permissions")]
		public Permissions Permissions { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000B392 File Offset: 0x00009592
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0000B39A File Offset: 0x0000959A
		[DataMember(Name = "MeetingOwner")]
		[XmlElement("MeetingOwner")]
		public MeetingOwner MeetingOwner { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000B3A3 File Offset: 0x000095A3
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0000B3AB File Offset: 0x000095AB
		[XmlElement("Audio")]
		[DataMember(Name = "Audio")]
		public Audio Audio { get; set; }

		// Token: 0x0600049D RID: 1181 RVA: 0x0000B3B4 File Offset: 0x000095B4
		internal static string GetBodyLanguage(DialInRegions dialInRegions)
		{
			if (dialInRegions.Count == 0 || dialInRegions[0].Languages == null || dialInRegions[0].Languages.Count == 0)
			{
				return string.Empty;
			}
			string result = string.Empty;
			try
			{
				result = new CultureInfo(dialInRegions[0].Languages[0]).LCID.ToString("x");
			}
			catch (CultureNotFoundException ex)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceError<string, string>(0, 0L, "MeetingSetting::GetBodyLanguage. CultureNotFoundException when parsing language {0}. Exception message: {1}", dialInRegions[0].Languages[0], (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
			}
			return result;
		}
	}
}
