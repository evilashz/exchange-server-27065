using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.Psws;
using Microsoft.Exchange.Management.FfoQuarantine;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000370 RID: 880
	public static class QuarantineCodeBehind
	{
		// Token: 0x0600301C RID: 12316 RVA: 0x00092964 File Offset: 0x00090B64
		public static void IdentityToStringAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			List<string> list = new List<string>();
			Identity identity = inputrow["Identity"] as Identity;
			inputrow["Identity"] = identity.RawIdentity.Replace(" ", "+");
			list.Add("Identity");
			store.SetModifiedColumns(list);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000929BC File Offset: 0x00090BBC
		public static void GetObjectPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			dataRow["Identity"] = new Identity(dataRow["Identity"].ToString().Replace(" ", "+"));
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x00092A13 File Offset: 0x00090C13
		public static string ConvertSizeForDisplay(int size)
		{
			return new ByteQuantifiedSize((ulong)Convert.ToUInt32(size)).ToAppropriateUnitFormatString();
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00092A28 File Offset: 0x00090C28
		public static bool IsSpamMessage(object value)
		{
			if (value != null)
			{
				QuarantineMessageType quarantineMessageType = (QuarantineMessageType)value;
				return quarantineMessageType.Value == QuarantineMessageTypeEnum.Spam;
			}
			return false;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x00092A4A File Offset: 0x00090C4A
		public static string ConvertEnumToLocalizedString(object value)
		{
			return DDIUtil.EnumToLocalizedString(((QuarantineMessageType)value).Value);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x00092A64 File Offset: 0x00090C64
		public static string ToUserDateTime(object value)
		{
			if (value == null)
			{
				return string.Empty;
			}
			string result;
			try
			{
				ExDateTime dateTimeValue = new ExDateTime(EcpDateTimeHelper.GetCurrentUserTimeZone(), (DateTime)value);
				result = dateTimeValue.ToUserDateTimeString();
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00092AB0 File Offset: 0x00090CB0
		public static void GetRecipientsPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			dataRow["Identity"] = new Identity(dataRow["Identity"].ToString().Replace(" ", "+"));
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x00092B08 File Offset: 0x00090D08
		public static string[] GetNotYetReleasedUsers(object quarantinedUsers, object releasedUsers)
		{
			IEnumerable<string> first;
			if (quarantinedUsers == null)
			{
				first = new List<string>();
			}
			else
			{
				first = (List<string>)quarantinedUsers;
			}
			IEnumerable<string> second;
			if (releasedUsers == null)
			{
				second = new List<string>();
			}
			else
			{
				second = (List<string>)releasedUsers;
			}
			return first.Except(second, StringComparer.OrdinalIgnoreCase).ToArray<string>();
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x00092B4C File Offset: 0x00090D4C
		public static void GetListPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["Identity"] = new Identity(dataRow["Identity"].ToString().Replace(" ", "+"));
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x00092BD0 File Offset: 0x00090DD0
		public static void GetListPreAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			ExDateTime now = ExDateTime.Now;
			ExDateTime value = ExDateTime.Create(EcpDateTimeHelper.GetCurrentUserTimeZone(), (DateTime)now.ToUserExDateTime()).First<ExDateTime>();
			TimeSpan value2 = now.Subtract(value);
			if (!DDIHelper.IsEmptyValue(inputrow["ReceivedFilter"]))
			{
				string text = inputrow["ReceivedFilter"].ToStringWithNull();
				string a;
				ExDateTime exDateTime;
				ExDateTime exDateTime2;
				if ((a = text) != null)
				{
					if (a == "2d")
					{
						exDateTime = value.Date.AddDays(-2.0);
						exDateTime2 = value.Date;
						goto IL_13A;
					}
					if (a == "7d")
					{
						exDateTime = value.Date.AddDays(-7.0);
						exDateTime2 = value.Date;
						goto IL_13A;
					}
					if (a == "custom")
					{
						exDateTime = ExDateTime.ParseExact(inputrow["ReceivedStartDateFilter"].ToString(), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
						exDateTime2 = ExDateTime.ParseExact(inputrow["ReceivedEndDateFilter"].ToString(), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
						goto IL_13A;
					}
				}
				exDateTime = value.Date;
				exDateTime2 = value.Date.AddDays(-1.0);
				IL_13A:
				inputrow["ReceivedStartDateFilter"] = exDateTime.Add(value2);
				inputrow["ReceivedEndDateFilter"] = exDateTime2.Add(value2);
			}
			else
			{
				inputrow["ReceivedStartDateFilter"] = DBNull.Value;
				inputrow["ReceivedEndDateFilter"] = DBNull.Value;
			}
			store.ModifiedColumns.Add("ReceivedStartDateFilter");
			store.ModifiedColumns.Add("ReceivedEndDateFilter");
			if (!DDIHelper.IsEmptyValue(inputrow["ExpiresFilter"]))
			{
				string text2 = inputrow["ExpiresFilter"].ToStringWithNull();
				string a2;
				ExDateTime exDateTime3;
				ExDateTime exDateTime4;
				if ((a2 = text2) != null)
				{
					if (a2 == "2d")
					{
						exDateTime3 = value.Date;
						exDateTime4 = value.Date.AddDays(2.0);
						goto IL_2C5;
					}
					if (a2 == "7d")
					{
						exDateTime3 = value.Date;
						exDateTime4 = value.Date.AddDays(7.0);
						goto IL_2C5;
					}
					if (a2 == "custom")
					{
						exDateTime3 = ExDateTime.ParseExact(inputrow["ExpiresStartDateFilter"].ToString(), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
						exDateTime4 = ExDateTime.ParseExact(inputrow["ExpiresEndDateFilter"].ToString(), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
						goto IL_2C5;
					}
				}
				exDateTime3 = value.Date;
				exDateTime4 = value.Date.AddDays(1.0);
				IL_2C5:
				inputrow["ExpiresStartDateFilter"] = exDateTime3.Add(value2);
				inputrow["ExpiresEndDateFilter"] = exDateTime4.Add(value2);
			}
			else
			{
				inputrow["ExpiresStartDateFilter"] = DBNull.Value;
				inputrow["ExpiresEndDateFilter"] = DBNull.Value;
			}
			store.ModifiedColumns.Add("ExpiresStartDateFilter");
			store.ModifiedColumns.Add("ExpiresEndDateFilter");
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00092F16 File Offset: 0x00091116
		public static string GetExecutingUserPrimarySmtpAddress()
		{
			return (string)HttpContext.Current.Items[TokenIssuer.ItemTagUpn];
		}
	}
}
