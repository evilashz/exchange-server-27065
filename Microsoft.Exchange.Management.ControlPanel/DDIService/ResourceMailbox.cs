using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000531 RID: 1329
	public static class ResourceMailbox
	{
		// Token: 0x06003F08 RID: 16136 RVA: 0x000BDC08 File Offset: 0x000BBE08
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)dataRow["RecipientTypeDetails"];
				dataRow["IsRoom"] = (recipientTypeDetails == RecipientTypeDetails.RoomMailbox);
				dataRow["MailboxType"] = MailboxPropertiesHelper.TranslateMailboxTypeForListview(recipientTypeDetails, false, Guid.Empty);
			}
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000BDC98 File Offset: 0x000BBE98
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.GetCountryOrRegion(inputRow, dataTable, store);
			MailboxPropertiesHelper.GetEmailAddresses(inputRow, dataTable, store);
			DataRow dataRow = dataTable.Rows[0];
			CalendarConfiguration calendarConfiguration = store.GetDataObject("CalendarConfiguration") as CalendarConfiguration;
			if (calendarConfiguration != null)
			{
				bool flag = calendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept;
				bool flag2 = flag && !calendarConfiguration.AllBookInPolicy && calendarConfiguration.AllRequestInPolicy;
				bool flag3 = flag && calendarConfiguration.AllBookInPolicy && !calendarConfiguration.AllRequestInPolicy;
				dataRow["AutomateProcessingManual"] = flag2;
				dataRow["AutomateProcessingAuto"] = flag3;
				dataRow["AutomateProcessingCustomized"] = (!flag2 && !flag3);
				dataRow["AdditionalResponse"] = (calendarConfiguration.AddAdditionalResponse ? calendarConfiguration.AdditionalResponse : string.Empty);
			}
			MailboxPropertiesHelper.UpdateCanSetABP(dataRow, store);
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000BDD78 File Offset: 0x000BBF78
		public static void SetObjectPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			List<string> list = new List<string>();
			bool flag = false;
			bool flag2 = false;
			if (!DBNull.Value.Equals(dataRow["AutomateProcessingAuto"]))
			{
				flag = (bool)dataRow["AutomateProcessingAuto"];
			}
			if (!DBNull.Value.Equals(dataRow["AutomateProcessingManual"]))
			{
				flag2 = (bool)dataRow["AutomateProcessingManual"];
			}
			if (flag && flag2)
			{
				throw new ArgumentException("automateProcessingAuto and automateProcessingManual cannot be true at the same time.");
			}
			if (flag || flag2)
			{
				inputRow["AutomateProcessing"] = (dataRow["AutomateProcessing"] = CalendarProcessingFlags.AutoAccept);
				inputRow["AllBookInPolicy"] = (dataRow["AllBookInPolicy"] = flag);
				inputRow["AllRequestInPolicy"] = (dataRow["AllRequestInPolicy"] = !flag);
				list.Add("AutomateProcessing");
				list.Add("AllBookInPolicy");
				list.Add("AllRequestInPolicy");
			}
			if (!DBNull.Value.Equals(dataRow["AdditionalResponse"]))
			{
				bool flag3 = !string.IsNullOrEmpty((string)dataRow["AdditionalResponse"]);
				inputRow["AddAdditionalResponse"] = (dataRow["AddAdditionalResponse"] = flag3);
				list.Add("AddAdditionalResponse");
			}
			if (list.Count > 0)
			{
				store.SetModifiedColumns(list);
			}
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x000BDF04 File Offset: 0x000BC104
		public static void SetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			if (results[0].ErrorRecords != null && results[0].ErrorRecords.Length > 0)
			{
				string[] array = new string[]
				{
					Strings.NewRoomCreationWarningText
				};
				array = array.Concat(from x in results[0].ErrorRecords
				select x.Message).ToArray<string>();
				if (results[0].Warnings != null)
				{
					array = array.Concat(results[0].Warnings).ToArray<string>();
				}
				results[0].Warnings = array;
				results[0].ErrorRecords = Array<ErrorRecord>.Empty;
			}
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000BDFC8 File Offset: 0x000BC1C8
		public static void GetSDOPostAction(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			CalendarConfiguration calendarConfiguration = store.GetDataObject("CalendarConfiguration") as CalendarConfiguration;
			if (calendarConfiguration != null)
			{
				dataRow["ResourceDelegates"] = calendarConfiguration.ResourceDelegates.ResolveRecipientsForSDO(3, (RecipientObjectResolverRow x) => Strings.DisplayedResourceDelegates(x.DisplayName, x.PrimarySmtpAddress));
				dataRow["AutomateProcessingAuto"] = (calendarConfiguration.AutomateProcessing == CalendarProcessingFlags.AutoAccept && calendarConfiguration.AllBookInPolicy && !calendarConfiguration.AllRequestInPolicy);
			}
			RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)dataRow["RecipientTypeDetails"];
			dataRow["IsRoom"] = (recipientTypeDetails == RecipientTypeDetails.RoomMailbox);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000BE080 File Offset: 0x000BC280
		public static void TargetAllMDBsPostAction(DataRow input, DataTable dataTable, DataObjectStore store)
		{
			store.SetModifiedColumns(new List<string>
			{
				"TargetAllMDBs"
			});
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000BE0A5 File Offset: 0x000BC2A5
		public static void GetSuggestionPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.FilterNoSmtpEmailAddresses(inputRow, dataTable, store);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000BE0AF File Offset: 0x000BC2AF
		public static void FilterCloudSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.FilterCloudSendAsPermission(inputRow, dataTable, store);
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000BE0B9 File Offset: 0x000BC2B9
		public static void FilterEntSendAsPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.FilterEntSendAsPermission(inputRow, dataTable, store);
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000BE0C3 File Offset: 0x000BC2C3
		public static void FilterFullAccessPermission(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.FilterFullAccessPermission(inputRow, dataTable, store);
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000BE0CD File Offset: 0x000BC2CD
		public static void SetMailboxPermissionPreAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			MailboxPropertiesHelper.SetMailboxPermissionPreAction(inputRow, dataTable, store);
		}

		// Token: 0x040028C5 RID: 10437
		private const string RecipientTypeDetailsColumnName = "RecipientTypeDetails";

		// Token: 0x040028C6 RID: 10438
		private const string IsRoomColumnName = "IsRoom";

		// Token: 0x040028C7 RID: 10439
		private const string AutomateProcessingCustomizedColumnName = "AutomateProcessingCustomized";

		// Token: 0x040028C8 RID: 10440
		private const string AutomateProcessingManualColumnName = "AutomateProcessingManual";

		// Token: 0x040028C9 RID: 10441
		private const string AutomateProcessingAutoColumnName = "AutomateProcessingAuto";

		// Token: 0x040028CA RID: 10442
		private const string AdditionalResponseColumnName = "AdditionalResponse";

		// Token: 0x040028CB RID: 10443
		private const string AutomateProcessingColumnName = "AutomateProcessing";

		// Token: 0x040028CC RID: 10444
		private const string AllBookInPolicyColumnName = "AllBookInPolicy";

		// Token: 0x040028CD RID: 10445
		private const string AllRequestInPolicyColumnName = "AllRequestInPolicy";

		// Token: 0x040028CE RID: 10446
		private const string AddAdditionalResponseColumnName = "AddAdditionalResponse";

		// Token: 0x040028CF RID: 10447
		private const string ResourceDelegatesColumnName = "ResourceDelegates";

		// Token: 0x040028D0 RID: 10448
		private const string CalendarConfigurationObjectName = "CalendarConfiguration";

		// Token: 0x040028D1 RID: 10449
		private const int DisplayedRecipientsNumberForSDO = 3;

		// Token: 0x040028D2 RID: 10450
		public static readonly string NewObjectWorkflowOutput = "Identity,RecipientTypeDetails,IsRoom,DisplayNameForList,MailboxType,PrimarySmtpAddress,Alias,City,Company,CountryOrRegion,Database,Department,EmailAddressesTxt,EmailAddressPolicyEnabled,ExchangeVersion,HiddenFromAddressListsEnabled,Name,Office,OrganizationalUnit,Phone,PostalCode,RecipientType,StateOrProvince,WhenChanged,CustomAttribute1,CustomAttribute2,CustomAttribute3,CustomAttribute4,CustomAttribute5,CustomAttribute6,CustomAttribute7,CustomAttribute8,CustomAttribute9,CustomAttribute10,CustomAttribute11,CustomAttribute12,CustomAttribute13,CustomAttribute14,CustomAttribute15";
	}
}
