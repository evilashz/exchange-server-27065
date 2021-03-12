using System;
using System.Collections;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000193 RID: 403
	internal class RightsManagementLicenseData : INestedData
	{
		// Token: 0x06001171 RID: 4465 RVA: 0x0005FA93 File Offset: 0x0005DC93
		public RightsManagementLicenseData()
		{
			this.subProperties = new Hashtable();
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0005FAA6 File Offset: 0x0005DCA6
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x0005FABD File Offset: 0x0005DCBD
		public string TemplateID
		{
			get
			{
				return this.subProperties["TemplateID"] as string;
			}
			set
			{
				this.subProperties["TemplateID"] = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0005FAD0 File Offset: 0x0005DCD0
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0005FAE7 File Offset: 0x0005DCE7
		public string TemplateName
		{
			get
			{
				return this.subProperties["TemplateName"] as string;
			}
			set
			{
				this.subProperties["TemplateName"] = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0005FAFA File Offset: 0x0005DCFA
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0005FB11 File Offset: 0x0005DD11
		public string TemplateDescription
		{
			get
			{
				return this.subProperties["TemplateDescription"] as string;
			}
			set
			{
				this.subProperties["TemplateDescription"] = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0005FB24 File Offset: 0x0005DD24
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x0005FB64 File Offset: 0x0005DD64
		public bool? EditAllowed
		{
			get
			{
				string text = this.subProperties["EditAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["EditAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0005FB98 File Offset: 0x0005DD98
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
		public bool? ReplyAllowed
		{
			get
			{
				string text = this.subProperties["ReplyAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ReplyAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0005FC0C File Offset: 0x0005DE0C
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0005FC4C File Offset: 0x0005DE4C
		public bool? ReplyAllAllowed
		{
			get
			{
				string text = this.subProperties["ReplyAllAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ReplyAllAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0005FC80 File Offset: 0x0005DE80
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x0005FCC0 File Offset: 0x0005DEC0
		public bool? ForwardAllowed
		{
			get
			{
				string text = this.subProperties["ForwardAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ForwardAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x0005FCF4 File Offset: 0x0005DEF4
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x0005FD34 File Offset: 0x0005DF34
		public bool? ModifyRecipientsAllowed
		{
			get
			{
				string text = this.subProperties["ModifyRecipientsAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ModifyRecipientsAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x0005FD68 File Offset: 0x0005DF68
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x0005FDA8 File Offset: 0x0005DFA8
		public bool? ExtractAllowed
		{
			get
			{
				string text = this.subProperties["ExtractAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ExtractAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x0005FDDC File Offset: 0x0005DFDC
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x0005FE1C File Offset: 0x0005E01C
		public bool? PrintAllowed
		{
			get
			{
				string text = this.subProperties["PrintAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["PrintAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x0005FE50 File Offset: 0x0005E050
		// (set) Token: 0x06001187 RID: 4487 RVA: 0x0005FE90 File Offset: 0x0005E090
		public bool? ExportAllowed
		{
			get
			{
				string text = this.subProperties["ExportAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ExportAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0005FEC4 File Offset: 0x0005E0C4
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x0005FF04 File Offset: 0x0005E104
		public bool? ProgrammaticAccessAllowed
		{
			get
			{
				string text = this.subProperties["ProgrammaticAccessAllowed"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["ProgrammaticAccessAllowed"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0005FF38 File Offset: 0x0005E138
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x0005FF78 File Offset: 0x0005E178
		public bool? Owner
		{
			get
			{
				string text = this.subProperties["Owner"] as string;
				if (text == null)
				{
					return null;
				}
				return new bool?(text == "1");
			}
			set
			{
				this.subProperties["Owner"] = ((value != null) ? (value.Value ? "1" : "0") : null);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x00060014 File Offset: 0x0005E214
		public ExDateTime? ContentExpiryDate
		{
			get
			{
				string text = this.subProperties["ContentExpiryDate"] as string;
				if (text == null)
				{
					return null;
				}
				ExDateTime value;
				if (!ExDateTime.TryParseExact(text, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
				{
					throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
					{
						ErrorStringForProtocolLogger = "InvalidDateTimeInContentExpiryDate"
					};
				}
				return new ExDateTime?(value);
			}
			set
			{
				this.subProperties["ContentExpiryDate"] = ((value != null) ? value.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo) : null);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x00060056 File Offset: 0x0005E256
		// (set) Token: 0x0600118F RID: 4495 RVA: 0x0006006D File Offset: 0x0005E26D
		public string ContentOwner
		{
			get
			{
				return this.subProperties["ContentOwner"] as string;
			}
			set
			{
				this.subProperties["ContentOwner"] = value;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x00060080 File Offset: 0x0005E280
		public IDictionary SubProperties
		{
			get
			{
				return this.subProperties;
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00060088 File Offset: 0x0005E288
		public void Clear()
		{
			this.subProperties.Clear();
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00060095 File Offset: 0x0005E295
		public bool ContainsValidData()
		{
			return this.subProperties.Count > 0;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000600A8 File Offset: 0x0005E2A8
		public void InitNoRightsTemplate()
		{
			this.EditAllowed = new bool?(false);
			this.ReplyAllowed = new bool?(false);
			this.ReplyAllAllowed = new bool?(false);
			this.ForwardAllowed = new bool?(false);
			this.PrintAllowed = new bool?(false);
			this.ExtractAllowed = new bool?(false);
			this.ProgrammaticAccessAllowed = new bool?(false);
			this.Owner = new bool?(false);
			this.ExportAllowed = new bool?(false);
			this.ModifyRecipientsAllowed = new bool?(false);
			this.ContentOwner = " ";
			this.ContentExpiryDate = new ExDateTime?(new ExDateTime(ExTimeZone.TimeZoneFromKind(DateTimeKind.Utc), DateTime.UtcNow.AddDays(30.0)));
			this.TemplateName = " ";
			this.TemplateDescription = " ";
			this.TemplateID = Guid.Empty.ToString();
		}

		// Token: 0x04000B36 RID: 2870
		private IDictionary subProperties;
	}
}
