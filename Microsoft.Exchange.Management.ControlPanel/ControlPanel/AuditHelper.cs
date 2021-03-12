using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003B8 RID: 952
	internal class AuditHelper
	{
		// Token: 0x060031D8 RID: 12760 RVA: 0x00099DC4 File Offset: 0x00097FC4
		public static string[] GetIdentitiesFromSmtpAddresses(SmtpAddress[] addresses)
		{
			List<string> list = new List<string>();
			foreach (RecipientObjectResolverRow recipientObjectResolverRow in RecipientObjectResolver.Instance.ResolveSmtpAddress(addresses))
			{
				list.Add(recipientObjectResolverRow.ADRawEntry.Id.ToString());
			}
			return list.ToArray();
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00099E34 File Offset: 0x00098034
		public static Identity CreateIdentity(string mailbox, string startDate, string endDate)
		{
			startDate = (startDate.IsNullOrBlank() ? "NoStart" : startDate);
			endDate = (endDate.IsNullOrBlank() ? "NoEnd" : endDate);
			string text = string.Format("{0};{1};{2}", mailbox, startDate, endDate);
			return new Identity(text, text);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00099E7C File Offset: 0x0009807C
		public static Identity CreateNonOwnerAccessIdentity(string mailbox, string startDate, string endDate, string logonTypes)
		{
			startDate = (startDate.IsNullOrBlank() ? "NoStart" : startDate);
			endDate = (endDate.IsNullOrBlank() ? "NoEnd" : endDate);
			string text = string.Format("{0};{1};{2};{3}", new object[]
			{
				mailbox,
				startDate,
				endDate,
				logonTypes
			});
			return new Identity(text, text);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x00099ED8 File Offset: 0x000980D8
		public static string GetLaterDate(string date1, string date2)
		{
			DateTime t;
			if (!DateTime.TryParse(date1, out t))
			{
				throw new ArgumentException("date1: {0}", date1);
			}
			DateTime t2;
			if (!DateTime.TryParse(date2, out t2))
			{
				throw new ArgumentException("date2: {0}", date2);
			}
			if (!(t > t2))
			{
				return date2;
			}
			return date1;
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00099F20 File Offset: 0x00098120
		public static string MakeUserFriendly(string objectName)
		{
			if (objectName != null && objectName.Contains("/"))
			{
				int num = objectName.LastIndexOf("/") + 1;
				if (num < objectName.Length)
				{
					objectName = objectName.Substring(num, objectName.Length - num);
				}
				else
				{
					objectName = string.Empty;
				}
			}
			return objectName;
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x00099F70 File Offset: 0x00098170
		public static string GetDateForAuditReportsFilter(string value, bool isEndDate)
		{
			if (!value.IsNullOrBlank())
			{
				ExDateTime? exDateTime;
				if (value.Length == "yyyy/MM/dd".Length)
				{
					exDateTime = value.ToEcpExDateTime("yyyy/MM/dd");
				}
				else if (value.Length == "yyyy/MM/dd HH:mm:ss".Length)
				{
					exDateTime = value.ToEcpExDateTime("yyyy/MM/dd HH:mm:ss");
				}
				else
				{
					exDateTime = value.ToEcpExDateTime("yyyy-MM-ddTHH:mm:ss.fffffffzzz");
				}
				if (exDateTime != null)
				{
					DateTime dateTime = new DateTime(exDateTime.Value.Year, exDateTime.Value.Month, exDateTime.Value.Day, exDateTime.Value.Hour, exDateTime.Value.Minute, exDateTime.Value.Second, exDateTime.Value.Millisecond, DateTimeKind.Local);
					if (isEndDate)
					{
						return dateTime.AddDays(1.0).ToString("o", CultureInfo.InvariantCulture.DateTimeFormat);
					}
					return dateTime.ToString("o", CultureInfo.InvariantCulture.DateTimeFormat);
				}
			}
			return null;
		}

		// Token: 0x04002426 RID: 9254
		internal const string NoStart = "NoStart";

		// Token: 0x04002427 RID: 9255
		internal const string NoEnd = "NoEnd";

		// Token: 0x04002428 RID: 9256
		internal const string ReadScope = "@R:Organization";
	}
}
