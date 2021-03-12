using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000517 RID: 1303
	public static class MailUser
	{
		// Token: 0x06003E72 RID: 15986 RVA: 0x000BCC60 File Offset: 0x000BAE60
		public static void GetObjectPostAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			DataRow dataRow = table.Rows[0];
			if (dataRow["SimpleEmailAddresses"] != DBNull.Value)
			{
				dataRow["EmailAddresses"] = EmailAddressList.FromProxyAddressCollection((ProxyAddressCollection)dataRow["SimpleEmailAddresses"]);
			}
			if (dataRow["ObjectCountryOrRegion"] != DBNull.Value)
			{
				dataRow["CountryOrRegion"] = ((CountryInfo)dataRow["ObjectCountryOrRegion"]).Name;
			}
			MailboxPropertiesHelper.GetMaxSendReceiveSize(inputRow, table, store);
			MailboxPropertiesHelper.GetAcceptRejectSendersOrMembers(inputRow, table, store);
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000BCCEE File Offset: 0x000BAEEE
		public static void SetRecipientFilterPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			if (DatacenterRegistry.IsForefrontForOffice())
			{
				inputRow["RecipientTypeFilter"] = inputRow["FFORecipientTypeFilter"].ToString();
			}
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000BCD12 File Offset: 0x000BAF12
		public static void GetSuggestionPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.FilterNoSmtpEmailAddresses(inputRow, dataTable, store);
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000BCD1C File Offset: 0x000BAF1C
		public static void SetObjectPreAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			MailboxPropertiesHelper.SetMaxSendReceiveSize(inputRow, table, store);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000BCD28 File Offset: 0x000BAF28
		public static void NewObjectPreAction(DataRow inputRow, DataTable table, DataObjectStore store)
		{
			DataRow dataRow = table.Rows[0];
			bool flag = !(dataRow["IsNotSmtpAddress"] is DBNull) && (bool)dataRow["IsNotSmtpAddress"];
			string text = (dataRow["ExternalEmailAddress"] is DBNull) ? null : ((string)dataRow["ExternalEmailAddress"]);
			List<string> list = new List<string>();
			list.Add("ExternalEmailAddress");
			if (string.IsNullOrEmpty(text))
			{
				text = (string)dataRow["MicrosoftOnlineServicesID"];
			}
			if (flag)
			{
				dataRow["ExternalEmailAddress"] = (string)dataRow["AddressPrefix"] + ":" + text;
			}
			else
			{
				dataRow["ExternalEmailAddress"] = "SMTP:" + text;
			}
			store.SetModifiedColumns(list);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000BCE00 File Offset: 0x000BB000
		public static string GetFilterByNameString(object name)
		{
			return string.Format("Name -eq '{0}'", ((string)name).Replace("'", "''"));
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000BCE24 File Offset: 0x000BB024
		public static void GenerateName(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ReducedRecipient reducedRecipient = store.GetDataObject("ReducedRecipient") as ReducedRecipient;
			string text = (string)inputRow["MailUserDisplayName"];
			if (reducedRecipient != null)
			{
				string text2 = " " + Guid.NewGuid().ToString("B").ToUpperInvariant();
				if (text.Length > 64)
				{
					text = text.SurrogateSubstring(0, text.Length - text2.Length);
				}
				inputRow["MailUserName"] = text + text2;
			}
			else
			{
				if (text.Length > 64)
				{
					text = text.SurrogateSubstring(0, 64);
				}
				inputRow["MailUserName"] = text;
			}
			store.SetModifiedColumns(new List<string>
			{
				"MailUserName"
			});
		}

		// Token: 0x04002891 RID: 10385
		public static readonly string NewObjectWorkflowOutput = Util.IsDataCenter ? "Identity,RecipientTypeDetails,DisplayName,LocRecipientTypeDetails,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Department,EmailAddressesTxt,FirstName,LastName,Office,Phone,PostalCode,RecipientType,StateOrProvince,Title,WhenChanged" : "Identity,RecipientTypeDetails,DisplayName,LocRecipientTypeDetails,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Department,EmailAddressesTxt,FirstName,LastName,Office,Phone,PostalCode,RecipientType,StateOrProvince,Title,WhenChanged,EmailAddressPolicyEnabled,HiddenFromAddressListsEnabled,Name,OrganizationalUnit,CustomAttribute1,CustomAttribute2,CustomAttribute3,CustomAttribute4,CustomAttribute5,CustomAttribute6,CustomAttribute7,CustomAttribute8,CustomAttribute9,CustomAttribute10,CustomAttribute11,CustomAttribute12,CustomAttribute13,CustomAttribute14,CustomAttribute15";
	}
}
