using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000A2 RID: 162
	public class ReadADOrgPerson : ReadADRecipient, IComparer<ReadADOrgPerson.ADMember>
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0002DAF8 File Offset: 0x0002BCF8
		protected bool IsAdOrgPerson
		{
			get
			{
				return this.isAdOrgPerson;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0002DB00 File Offset: 0x0002BD00
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (base.ADRecipient is IADOrgPerson)
			{
				this.adOrgPerson = (IADOrgPerson)base.ADRecipient;
				this.isAdOrgPerson = true;
			}
			if (this.isAdOrgPerson && ADCustomPropertyParser.CustomPropertyDictionary != null && ADCustomPropertyParser.CustomPropertyDictionary.Count > 0)
			{
				this.adRawEntry = base.ADRecipientSession.ReadADRawEntry(base.ADRecipient.OriginalId, ADCustomPropertyParser.CustomPropertyDictionary.Values);
				if (this.adRawEntry != null)
				{
					this.renderCustomProperties = true;
				}
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002DB8C File Offset: 0x0002BD8C
		protected void RenderHeader(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.title = (this.isAdOrgPerson ? this.adOrgPerson.Title : string.Empty);
			this.company = (this.isAdOrgPerson ? this.adOrgPerson.Company : string.Empty);
			this.department = (this.isAdOrgPerson ? this.adOrgPerson.Department : string.Empty);
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"pHd\">");
			if (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(base.ADRecipient.PhoneticDisplayName))
			{
				writer.Write("<tr><td class=\"dn pLT\">");
				Utilities.HtmlEncode(base.ADRecipient.PhoneticDisplayName, writer);
				writer.Write("</td></tr>");
			}
			writer.Write("<tr><td class=\"dn pLT\">");
			Utilities.HtmlEncode(base.ADRecipient.DisplayName, writer);
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"pLT\">");
			if (!string.IsNullOrEmpty(this.title))
			{
				writer.Write("<span class=\"txb\">");
				Utilities.HtmlEncode(this.title, writer);
				writer.Write(", </span>");
			}
			writer.Write("<span class=\"txnr\">");
			Utilities.HtmlEncode(this.department, writer);
			writer.Write("</span></td></tr>");
			writer.Write("<tr><td class=\"pLT pB\"><span class=\"txnr\">");
			Utilities.HtmlEncode(this.company, writer);
			writer.Write("</span></td></tr>");
			writer.Write("</table>");
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0002DD08 File Offset: 0x0002BF08
		protected void RenderDetailsBucket(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"pDtls\">");
			ReadADRecipient.RenderDetailHeader(writer, -2101430728);
			writer.Write("<tr><td class=\"lbl lp\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(613689222));
			writer.Write("</td>");
			if (!base.UserContext.IsWebPartRequest)
			{
				writer.Write("<td><a href=\"#\" class=\"peer\" onclick=\"return onSendMail();\">");
			}
			else
			{
				writer.Write("<td class=\"txvl\">");
			}
			Utilities.HtmlEncode(base.ADRecipient.Alias, writer);
			if (!base.UserContext.IsWebPartRequest)
			{
				writer.Write("</a>");
			}
			writer.Write("</td></tr>");
			if (this.isAdOrgPerson)
			{
				base.RenderDetailsLabel(writer, 1111077458, this.adOrgPerson.PrimarySmtpAddress.ToString(), null);
				writer.Write("<tr><td class=\"spcHd\" colspan=2></td></tr>");
				if (!string.IsNullOrEmpty(this.adOrgPerson.Office))
				{
					base.RenderDetailsLabel(writer, 275231482, this.adOrgPerson.Office, null);
				}
				writer.Write("<tr><td class=\"spcHd\" colspan=2></td></tr>");
				if (!string.IsNullOrEmpty(this.adOrgPerson.Phone))
				{
					base.RenderDetailsLabel(writer, -31489650, this.adOrgPerson.Phone, new ThemeFileId?(ThemeFileId.WorkPhone));
				}
				if (!string.IsNullOrEmpty(this.adOrgPerson.HomePhone))
				{
					base.RenderDetailsLabel(writer, -1844864953, this.adOrgPerson.HomePhone, new ThemeFileId?(ThemeFileId.HomePhone));
				}
				if (!string.IsNullOrEmpty(this.adOrgPerson.MobilePhone))
				{
					base.RenderDetailsLabel(writer, 1158653436, this.adOrgPerson.MobilePhone, new ThemeFileId?(ThemeFileId.MobilePhone));
				}
				if (!string.IsNullOrEmpty(this.adOrgPerson.Fax))
				{
					base.RenderDetailsLabel(writer, 696030351, this.adOrgPerson.Fax, new ThemeFileId?(ThemeFileId.Fax));
				}
				if (!string.IsNullOrEmpty(this.adOrgPerson.Pager))
				{
					base.RenderDetailsLabel(writer, -1779142331, this.adOrgPerson.Pager, null);
				}
				bool flag = !string.IsNullOrEmpty(this.adOrgPerson.FirstName) || !string.IsNullOrEmpty(this.adOrgPerson.LastName) || (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adOrgPerson.PhoneticFirstName)) || (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adOrgPerson.PhoneticLastName));
				if (flag)
				{
					writer.Write("<tr><td class=\"spcOP\" colspan=2></td></tr>");
					ReadADRecipient.RenderDetailHeader(writer, -728684336);
				}
				if (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adOrgPerson.PhoneticFirstName))
				{
					base.RenderDetailsLabel(writer, -758272749, this.adOrgPerson.PhoneticFirstName, null);
				}
				if (!string.IsNullOrEmpty(this.adOrgPerson.FirstName))
				{
					base.RenderDetailsLabel(writer, -1134283443, this.adOrgPerson.FirstName, null);
				}
				if (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adOrgPerson.PhoneticLastName))
				{
					base.RenderDetailsLabel(writer, -1100427325, this.adOrgPerson.PhoneticLastName, null);
				}
				if (!string.IsNullOrEmpty(this.adOrgPerson.LastName))
				{
					base.RenderDetailsLabel(writer, -991618307, this.adOrgPerson.LastName, null);
				}
				writer.Write("<tr><td class=\"spcOP\" colspan=2></td></tr>");
				ReadADRecipient.RenderDetailHeader(writer, -905993889);
				if (!string.IsNullOrEmpty(this.title))
				{
					base.RenderDetailsLabel(writer, 587115635, this.title, null);
					this.renderOrganizationDetails = true;
				}
				if (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adOrgPerson.PhoneticDepartment))
				{
					base.RenderDetailsLabel(writer, 871410780, this.adOrgPerson.PhoneticDepartment, null);
					this.renderOrganizationDetails = true;
				}
				if (!string.IsNullOrEmpty(this.department))
				{
					base.RenderDetailsLabel(writer, 1855823700, this.department, null);
					this.renderOrganizationDetails = true;
				}
				if (base.UserContext.IsPhoneticNamesEnabled && !string.IsNullOrEmpty(this.adOrgPerson.PhoneticCompany))
				{
					base.RenderDetailsLabel(writer, -923446215, this.adOrgPerson.PhoneticCompany, null);
					this.renderOrganizationDetails = true;
				}
				if (!string.IsNullOrEmpty(this.company))
				{
					base.RenderDetailsLabel(writer, 642177943, this.company, null);
					this.renderOrganizationDetails = true;
				}
				this.RenderManagementChain(this.adOrgPerson, writer);
				this.RenderPeers(this.adOrgPerson, writer);
				this.RenderDirectReports(this.adOrgPerson, writer);
				if (!this.renderOrganizationDetails)
				{
					writer.Write("<tr><td colspan=2 class=\"nodtls msgpd\">");
					writer.Write(LocalizedStrings.GetHtmlEncoded(1029790140));
					writer.Write("</td></tr>");
				}
			}
			writer.Write("<tr><td class=\"spcOP\" colspan=2></td></tr>");
			writer.Write("</table>");
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0002E240 File Offset: 0x0002C440
		private static void RenderAddressPart(TextWriter writer, string label, string value)
		{
			writer.Write("<tr><td class=\"lbl\">");
			writer.Write(label);
			writer.Write("</td><td class=\"txvl\">");
			Utilities.HtmlEncode(value, writer);
			writer.Write("</td></tr>");
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0002E274 File Offset: 0x0002C474
		private void RenderCustomProperties(TextWriter writer)
		{
			bool flag = false;
			foreach (KeyValuePair<string, PropertyDefinition> keyValuePair in ADCustomPropertyParser.CustomPropertyDictionary)
			{
				string value = this.adRawEntry[keyValuePair.Value].ToString();
				if (!string.IsNullOrEmpty(value))
				{
					if (!flag)
					{
						ReadADRecipient.RenderAddressHeader(writer, -582599340);
						flag = true;
					}
					base.RenderDetailsLabel(writer, Utilities.HtmlEncode(keyValuePair.Key), value, null);
				}
			}
			if (flag)
			{
				writer.Write("<tr><td class=\"spcOP\" colspan=2></td></tr>");
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002E320 File Offset: 0x0002C520
		protected void RenderAddressBucket(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			bool flag = true;
			if (string.IsNullOrEmpty(this.adOrgPerson.StreetAddress) && string.IsNullOrEmpty(this.adOrgPerson.City) && string.IsNullOrEmpty(this.adOrgPerson.StateOrProvince) && string.IsNullOrEmpty(this.adOrgPerson.PostalCode) && string.IsNullOrEmpty(this.adOrgPerson.CountryOrRegionDisplayName))
			{
				flag = false;
			}
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"pAddr\">");
			if (flag)
			{
				ReadADRecipient.RenderAddressHeader(writer, -1159205642);
				IDictionary<AddressFormatTable.AddressPart, AddressComponent> addressInfo = ContactUtilities.GetAddressInfo(this.adOrgPerson);
				foreach (KeyValuePair<AddressFormatTable.AddressPart, AddressComponent> keyValuePair in addressInfo)
				{
					ReadADOrgPerson.RenderAddressPart(writer, keyValuePair.Value.Label, keyValuePair.Value.Value);
				}
			}
			if (this.renderCustomProperties)
			{
				this.RenderCustomProperties(writer);
			}
			ReadADRecipient.RenderAddressHeader(writer, 1601836855);
			writer.Write("<tr><td colspan=2 class=\"rp\"><textarea name=\"notes\" rows=10 cols=32 readonly>");
			Utilities.HtmlEncode(this.adOrgPerson.Notes, writer);
			writer.Write("</textarea></td></tr></table>");
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002E454 File Offset: 0x0002C654
		private void RenderPeers(IADOrgPerson person, TextWriter writer)
		{
			ADObjectId manager = person.Manager;
			if (manager != null)
			{
				ADRecipient adrecipient = base.ADRecipientSession.Read(manager);
				if (adrecipient != null)
				{
					IADOrgPerson iadorgPerson = (IADOrgPerson)adrecipient;
					this.peersResults = iadorgPerson.GetDirectReportsView(new PropertyDefinition[]
					{
						ADRecipientSchema.DisplayName,
						ADObjectSchema.Id,
						ADRecipientSchema.RecipientType
					});
				}
			}
			if (this.peersResults != null && this.peersResults.Length > 1)
			{
				writer.Write("<tr><td class=\"lbl lp\" nowrap>");
				writer.Write(LocalizedStrings.GetHtmlEncoded(-1417802693));
				writer.Write("</td><td>");
				writer.Write("<table cellpadding=0 cellspacing=0 class=\"drpts\">");
				List<ReadADOrgPerson.ADMember> list = new List<ReadADOrgPerson.ADMember>();
				for (int i = 0; i < this.peersResults.Length; i++)
				{
					if (!base.ADRecipient.Id.Equals(this.peersResults[i][1]))
					{
						list.Add(new ReadADOrgPerson.ADMember(this.peersResults[i][0] as string, this.peersResults[i][1] as ADObjectId, this.peersResults[i][2]));
					}
				}
				list.Sort(this);
				foreach (ReadADOrgPerson.ADMember admember in list)
				{
					RecipientType recipientType = (RecipientType)admember.Type;
					int readItemType;
					if (Utilities.IsADDistributionList(recipientType))
					{
						readItemType = 2;
					}
					else
					{
						readItemType = 1;
					}
					writer.Write("<tr><td class=\"rptdpd\">");
					ReadADRecipient.RenderADRecipient(writer, readItemType, admember.Id, admember.DisplayName);
					writer.Write("</td></tr>");
				}
				writer.Write("</table>");
				writer.Write("</td></tr>");
				writer.Write("<tr><td class=\"spcHd\" colspan=2></td></tr>");
				this.renderOrganizationDetails = true;
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0002E624 File Offset: 0x0002C824
		public int Compare(ReadADOrgPerson.ADMember x, ReadADOrgPerson.ADMember y)
		{
			return x.DisplayName.CompareTo(y.DisplayName);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002E63C File Offset: 0x0002C83C
		private void RenderDirectReports(IADOrgPerson person, TextWriter writer)
		{
			this.reports = person.GetDirectReportsView(new PropertyDefinition[]
			{
				ADRecipientSchema.DisplayName,
				ADObjectSchema.Id,
				ADRecipientSchema.RecipientType
			});
			if (this.reports != null && this.reports.Length > 0)
			{
				writer.Write("<tr><td class=\"lbl lp\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(-156515347));
				writer.Write("</td><td>");
				writer.Write("<table cellpadding=0 cellspacing=0 class=\"drpts\">");
				List<ReadADOrgPerson.ADMember> list = new List<ReadADOrgPerson.ADMember>();
				for (int i = 0; i < this.reports.Length; i++)
				{
					list.Add(new ReadADOrgPerson.ADMember(this.reports[i][0] as string, this.reports[i][1] as ADObjectId, this.reports[i][2]));
				}
				list.Sort(this);
				foreach (ReadADOrgPerson.ADMember admember in list)
				{
					RecipientType recipientType = (RecipientType)admember.Type;
					int readItemType;
					if (Utilities.IsADDistributionList(recipientType))
					{
						readItemType = 2;
					}
					else
					{
						readItemType = 1;
					}
					writer.Write("<tr><td class=\"rptdpd\">");
					ReadADRecipient.RenderADRecipient(writer, readItemType, admember.Id, admember.DisplayName);
					writer.Write("</td></tr>");
				}
				writer.Write("</table>");
				writer.Write("</td></tr>");
				this.renderOrganizationDetails = true;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002E7B8 File Offset: 0x0002C9B8
		private void RenderManagementChain(IADOrgPerson person, TextWriter writer)
		{
			this.managersResults = person.GetManagementChainView(false, new PropertyDefinition[]
			{
				ADRecipientSchema.DisplayName,
				ADObjectSchema.Id,
				ADRecipientSchema.RecipientType
			});
			if (this.managersResults.Length <= 1)
			{
				return;
			}
			int num = this.managersResults.Length - 1;
			int num2 = 0;
			int readItemType = 1;
			if (num > 25)
			{
				num2 = num - 25;
			}
			writer.Write("<tr><td class=\"lbl lp\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(1660340599));
			writer.Write("</td><td>");
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"drpts\">");
			for (int i = num2; i < num; i++)
			{
				writer.Write("<tr>");
				if (i != 0)
				{
					writer.Write("<td><img alt=\"\" class=\"mitimg\" src=\"");
					base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.OrganizationUpArrow);
					writer.Write("\"></td>");
					writer.Write("<td class=\"mitdpd\">");
				}
				else
				{
					writer.Write("<td colspan=\"2\" class=\"rptdpd\">");
				}
				ReadADRecipient.RenderADRecipient(writer, readItemType, this.managersResults[i][1] as ADObjectId, this.managersResults[i][0].ToString());
				writer.Write("</td></tr>");
			}
			writer.Write("</table>");
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"spcHd\" colspan=2></td></tr>");
			this.renderOrganizationDetails = true;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002E904 File Offset: 0x0002CB04
		protected void RenderOOF(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			AvailabilityQuery availabilityQuery = new AvailabilityQuery();
			if (HttpContext.Current != null)
			{
				availabilityQuery.HttpResponse = HttpContext.Current.Response;
			}
			availabilityQuery.MailboxArray = new MailboxData[1];
			availabilityQuery.MailboxArray[0] = new MailboxData();
			availabilityQuery.MailboxArray[0].Email = new EmailAddress();
			availabilityQuery.MailboxArray[0].Email.Address = base.ADRecipient.PrimarySmtpAddress.ToString();
			availabilityQuery.ClientContext = ClientContext.Create(base.UserContext.LogonIdentity.ClientSecurityContext, base.UserContext.ExchangePrincipal.MailboxInfo.OrganizationId, OwaContext.TryGetCurrentBudget(), base.UserContext.TimeZone, base.UserContext.UserCulture, AvailabilityQuery.CreateNewMessageId());
			ExDateTime date = DateTimeUtilities.GetLocalTime().Date;
			ExDateTime exDateTime = date.IncrementDays(1);
			availabilityQuery.DesiredFreeBusyView = new FreeBusyViewOptions
			{
				RequestedView = FreeBusyViewType.Detailed,
				MergedFreeBusyIntervalInMinutes = 1440,
				TimeWindow = new Duration((DateTime)date, (DateTime)exDateTime.IncrementDays(1))
			};
			AvailabilityQueryResult availabilityQueryResult;
			if (Utilities.ExecuteAvailabilityQuery(base.OwaContext, availabilityQuery, true, out availabilityQueryResult))
			{
				FreeBusyQueryResult freeBusyQueryResult = availabilityQueryResult.FreeBusyResults[0];
				if (freeBusyQueryResult != null)
				{
					string currentOofMessage = freeBusyQueryResult.CurrentOofMessage;
					if (!string.IsNullOrEmpty(currentOofMessage))
					{
						writer.Write("<tr><td class=\"spcOP\"></td></tr>");
						writer.Write("<tr><td class=\"oof oofF\">");
						writer.Write(LocalizedStrings.GetHtmlEncoded(77678270));
						writer.Write("</td></tr>");
						writer.Write("<tr><td class=\"oof\">");
						writer.Write("<textarea name=\"off\" rows=3 cols=100 readonly>");
						writer.Write(currentOofMessage);
						writer.Write("</textarea>");
					}
				}
			}
		}

		// Token: 0x04000446 RID: 1094
		private const int MaxManagerTreeSize = 25;

		// Token: 0x04000447 RID: 1095
		private IADOrgPerson adOrgPerson;

		// Token: 0x04000448 RID: 1096
		private ADRawEntry adRawEntry;

		// Token: 0x04000449 RID: 1097
		private string department;

		// Token: 0x0400044A RID: 1098
		private string title;

		// Token: 0x0400044B RID: 1099
		private string company;

		// Token: 0x0400044C RID: 1100
		private object[][] reports;

		// Token: 0x0400044D RID: 1101
		private object[][] managersResults;

		// Token: 0x0400044E RID: 1102
		private object[][] peersResults;

		// Token: 0x0400044F RID: 1103
		private bool renderOrganizationDetails;

		// Token: 0x04000450 RID: 1104
		private bool renderCustomProperties;

		// Token: 0x04000451 RID: 1105
		private bool isAdOrgPerson;

		// Token: 0x020000A3 RID: 163
		public struct ADMember
		{
			// Token: 0x060005DF RID: 1503 RVA: 0x0002EACD File Offset: 0x0002CCCD
			public ADMember(string displayName, ADObjectId id, object type)
			{
				this.DisplayName = displayName;
				this.Id = id;
				this.Type = type;
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x0002EAE4 File Offset: 0x0002CCE4
			public override bool Equals(object obj)
			{
				if (!(obj is ReadADOrgPerson.ADMember))
				{
					return false;
				}
				ReadADOrgPerson.ADMember admember = (ReadADOrgPerson.ADMember)obj;
				return ((this.DisplayName == null && admember.DisplayName == null) || (this.DisplayName != null && this.DisplayName.Equals(admember.DisplayName))) && ((this.Id == null && admember.Id == null) || (this.Id != null && this.Id.Equals(admember.Id))) && ((this.Type == null && admember.Type == null) || (this.Type != null && this.Type.Equals(admember.Type)));
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x0002EB90 File Offset: 0x0002CD90
			public override int GetHashCode()
			{
				int num = 1000003;
				int num2 = 0;
				if (this.DisplayName != null)
				{
					num2 = this.DisplayName.GetHashCode();
				}
				if (this.Id != null)
				{
					num2 = num * num2 + this.Id.GetHashCode();
				}
				if (this.Type != null)
				{
					num2 = num * num2 + this.Type.GetHashCode();
				}
				return num2;
			}

			// Token: 0x04000452 RID: 1106
			public string DisplayName;

			// Token: 0x04000453 RID: 1107
			public ADObjectId Id;

			// Token: 0x04000454 RID: 1108
			public object Type;
		}
	}
}
