using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001CD RID: 461
	public class EmailAddressPolicy
	{
		// Token: 0x0600251D RID: 9501 RVA: 0x00071ECC File Offset: 0x000700CC
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.Rows[0];
			string value = string.Empty;
			dataRow["IsPrecannedRecipientFilterType"] = (dataRow["IsCustomRecipientFilterType"] = (dataRow["IsOtherRecipientFilterType"] = false));
			if (dataRow["RecipientFilterType"] != DBNull.Value)
			{
				switch ((WellKnownRecipientFilterType)dataRow["RecipientFilterType"])
				{
				case WellKnownRecipientFilterType.Unknown:
				case WellKnownRecipientFilterType.Legacy:
					dataRow["IsOtherRecipientFilterType"] = true;
					value = Strings.CustomFilterDescription((string)dataRow["LdapRecipientFilter"]);
					break;
				case WellKnownRecipientFilterType.Custom:
					dataRow["IsCustomRecipientFilterType"] = true;
					value = Strings.CustomFilterDescription((string)dataRow["RecipientFilter"]);
					break;
				case WellKnownRecipientFilterType.Precanned:
					dataRow["IsPrecannedRecipientFilterType"] = true;
					value = LocalizedDescriptionAttribute.FromEnum(typeof(WellKnownRecipientType), dataRow["IncludedRecipients"]);
					break;
				default:
					throw new NotSupportedException("Unkown WellKnownRecipientFilterType: " + dataRow["RecipientFilterType"].ToStringWithNull());
				}
				dataRow["IncludeRecipientDescription"] = value;
			}
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x00072012 File Offset: 0x00070212
		public static void NewObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			DDIUtil.InsertWarningIfSucceded(results, Strings.NewEAPWarning);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00072025 File Offset: 0x00070225
		public static void SetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			DDIUtil.InsertWarningIfSucceded(results, Strings.EditEAPWarning);
		}
	}
}
