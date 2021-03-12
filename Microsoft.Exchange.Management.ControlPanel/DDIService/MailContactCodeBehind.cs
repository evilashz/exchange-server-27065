using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000516 RID: 1302
	public static class MailContactCodeBehind
	{
		// Token: 0x06003E68 RID: 15976 RVA: 0x000BC84A File Offset: 0x000BAA4A
		public static string GetCountryOrRegionName(object countryOrRegion)
		{
			if (countryOrRegion != null && countryOrRegion is CountryInfo)
			{
				return (countryOrRegion as CountryInfo).Name;
			}
			return null;
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000BC894 File Offset: 0x000BAA94
		public static List<object> FilterEmailAddesses(object emailAddress)
		{
			if (emailAddress != null && emailAddress is ProxyAddressCollection)
			{
				IEnumerable<string> enumerable = from address in emailAddress as ProxyAddressCollection
				where address is SmtpProxyAddress
				where !address.IsPrimaryAddress
				orderby ((SmtpProxyAddress)address).SmtpAddress
				select ((SmtpProxyAddress)address).SmtpAddress;
				List<object> list = new List<object>();
				foreach (string item in enumerable)
				{
					list.Add(item);
				}
				return list;
			}
			return null;
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000BC988 File Offset: 0x000BAB88
		public static string GetFilterByNameString(object name)
		{
			return string.Format("Name -eq '{0}'", ((string)name).Replace("'", "''"));
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x000BC9AC File Offset: 0x000BABAC
		public static void GenerateName(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			ReducedRecipient reducedRecipient = store.GetDataObject("ReducedRecipient") as ReducedRecipient;
			string text = (string)inputRow["DisplayName"];
			if (reducedRecipient != null)
			{
				string text2 = " " + Guid.NewGuid().ToString("B").ToUpperInvariant();
				if (text.Length > 64)
				{
					text = text.SurrogateSubstring(0, text.Length - text2.Length);
				}
				inputRow["Name"] = text + text2;
			}
			else
			{
				if (text.Length > 64)
				{
					text = text.SurrogateSubstring(0, 64);
				}
				inputRow["Name"] = text;
			}
			store.SetModifiedColumns(new List<string>
			{
				"Name"
			});
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x000BCA6C File Offset: 0x000BAC6C
		public static void UpdateEmailAddresses(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailContact mailContact = store.GetDataObject("MailContact") as MailContact;
			ProxyAddressCollection emailAddresses = mailContact.EmailAddresses;
			IEnumerable<object> enumerable = inputRow["FilteredEmailAddresses"] as IEnumerable<object>;
			string text = inputRow["ExternalEmailAddress"] as string;
			if (enumerable != null)
			{
				for (int i = emailAddresses.Count - 1; i >= 0; i--)
				{
					if (emailAddresses[i] is SmtpProxyAddress && !((SmtpProxyAddress)emailAddresses[i]).IsPrimaryAddress)
					{
						emailAddresses.RemoveAt(i);
					}
				}
			}
			if (text != null)
			{
				ProxyAddressBase proxyAddressBase = ProxyAddress.Parse(text).ToPrimary();
				if (!(proxyAddressBase is InvalidProxyAddress) && mailContact.ExternalEmailAddress != proxyAddressBase)
				{
					int num = emailAddresses.IndexOf((ProxyAddress)proxyAddressBase);
					if (num >= 0)
					{
						emailAddresses.MakePrimary(num);
					}
					else
					{
						emailAddresses.Add(proxyAddressBase);
					}
					int num2 = emailAddresses.IndexOf(mailContact.ExternalEmailAddress);
					if (num2 >= 0 && (!(emailAddresses[num2] is SmtpProxyAddress) || !emailAddresses[num2].IsPrimaryAddress))
					{
						emailAddresses.RemoveAt(num2);
					}
				}
			}
			if (enumerable != null)
			{
				foreach (object obj in enumerable)
				{
					string text2 = (string)obj;
					ProxyAddress proxyAddress = ProxyAddress.Parse(text2);
					if (proxyAddress is InvalidProxyAddress)
					{
						InvalidProxyAddress invalidProxyAddress = proxyAddress as InvalidProxyAddress;
						throw new FaultException(invalidProxyAddress.ParseException.Message);
					}
					if (emailAddresses.Contains(proxyAddress))
					{
						throw new FaultException(string.Format(Strings.DuplicateProxyAddressError, text2));
					}
					emailAddresses.Add(proxyAddress);
				}
			}
			inputRow["EmailAddresses"] = emailAddresses;
			store.SetModifiedColumns(new List<string>
			{
				"EmailAddresses"
			});
		}

		// Token: 0x0400288A RID: 10378
		internal const string DatacenterNewObjectWorkflowOutput = "Identity,RecipientTypeDetails,DisplayName,LocRecipientTypeDetails,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Department,EmailAddressesTxt,FirstName,LastName,Office,Phone,PostalCode,RecipientType,StateOrProvince,Title,WhenChanged";

		// Token: 0x0400288B RID: 10379
		internal const string EnterpriseNewObjectWorkflowOutput = "Identity,RecipientTypeDetails,DisplayName,LocRecipientTypeDetails,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Department,EmailAddressesTxt,FirstName,LastName,Office,Phone,PostalCode,RecipientType,StateOrProvince,Title,WhenChanged,EmailAddressPolicyEnabled,Name,OrganizationalUnit";

		// Token: 0x0400288C RID: 10380
		public static readonly string NewObjectWorkflowOutput = Util.IsDataCenter ? "Identity,RecipientTypeDetails,DisplayName,LocRecipientTypeDetails,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Department,EmailAddressesTxt,FirstName,LastName,Office,Phone,PostalCode,RecipientType,StateOrProvince,Title,WhenChanged" : "Identity,RecipientTypeDetails,DisplayName,LocRecipientTypeDetails,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Department,EmailAddressesTxt,FirstName,LastName,Office,Phone,PostalCode,RecipientType,StateOrProvince,Title,WhenChanged,EmailAddressPolicyEnabled,Name,OrganizationalUnit";
	}
}
