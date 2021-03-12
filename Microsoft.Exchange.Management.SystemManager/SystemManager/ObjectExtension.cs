using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200002A RID: 42
	public static class ObjectExtension
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x000080E0 File Offset: 0x000062E0
		private static bool IsQuotationRequired(object value)
		{
			return value is string;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000080EC File Offset: 0x000062EC
		public static string ToUserFriendText(this object item, string separator, ObjectExtension.IsQuotationRequiredDelegate IsQuotationRequired, int lengthConstraint)
		{
			string text = string.Empty;
			if (item != null)
			{
				if (string.IsNullOrEmpty(separator))
				{
					throw new ArgumentNullException("separator");
				}
				if (IsQuotationRequired == null)
				{
					throw new ArgumentNullException("IsQuotationRequired");
				}
				Type type = typeof(ICollection);
				if (item.GetType().IsGenericType)
				{
					type = typeof(ICollection<>).MakeGenericType(item.GetType().GetGenericArguments());
				}
				if (type.IsAssignableFrom(item.GetType()))
				{
					text = ObjectExtension.CollectionToString(item as IEnumerable, separator, lengthConstraint, IsQuotationRequired);
				}
				else
				{
					text = ObjectExtension.AtomToString(item);
				}
				if (IsQuotationRequired(item) && !string.IsNullOrEmpty(text))
				{
					text = text.Replace("'", "''");
					text = Strings.QuotedString(text);
				}
			}
			return text;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000081B0 File Offset: 0x000063B0
		private static string AtomToString(object item)
		{
			string text = string.Empty;
			if (item != null)
			{
				if (item is Enum)
				{
					text = LocalizedDescriptionAttribute.FromEnum(item.GetType(), item);
				}
				else if (item is ADObjectId)
				{
					ADObjectId adobjectId = (ADObjectId)item;
					text = ((!string.IsNullOrEmpty(adobjectId.Name)) ? adobjectId.Name : adobjectId.ToString());
				}
				else if (item is CultureInfo)
				{
					text = ((CultureInfo)item).DisplayName;
				}
				else if (item is DateTime)
				{
					text = ((DateTime)item).ToLongDateString();
				}
				else
				{
					text = item.ToString();
				}
				if (string.IsNullOrEmpty(text))
				{
					text = string.Empty;
				}
			}
			return text;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008254 File Offset: 0x00006454
		private static string CollectionToString(IEnumerable collection, string separator, int lengthConstraint, ObjectExtension.IsQuotationRequiredDelegate IsQuotationRequired)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (collection != null)
			{
				bool flag = true;
				foreach (object obj in collection)
				{
					stringBuilder.Append(flag ? string.Empty : separator);
					flag = false;
					if (obj != null)
					{
						stringBuilder.Append(obj.ToUserFriendText(separator, IsQuotationRequired, lengthConstraint - stringBuilder.Length));
					}
					if (lengthConstraint > 0 && stringBuilder.Length >= lengthConstraint)
					{
						stringBuilder.Remove(lengthConstraint, stringBuilder.Length - lengthConstraint);
						stringBuilder.Append(" ...");
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000830C File Offset: 0x0000650C
		public static string ToUserFriendText(this object item, string separator, ObjectExtension.IsQuotationRequiredDelegate IsQuotationRequired)
		{
			return item.ToUserFriendText(separator, IsQuotationRequired, 5120);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000831B File Offset: 0x0000651B
		public static string ToUserFriendText(this object item, string separator)
		{
			return item.ToUserFriendText(separator, new ObjectExtension.IsQuotationRequiredDelegate(ObjectExtension.IsQuotationRequired));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008330 File Offset: 0x00006530
		public static string ToUserFriendText(this object item)
		{
			return item.ToUserFriendText(CultureInfo.CurrentUICulture.TextInfo.ListSeparator);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008348 File Offset: 0x00006548
		public static string ToQuotationEscapedString(this object item)
		{
			string result = string.Empty;
			if (item != null)
			{
				result = item.ToString().Replace("'", "''");
			}
			return result;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008378 File Offset: 0x00006578
		public static string ToSustainedString(this object item)
		{
			string result = string.Empty;
			if (item is ADObjectId && !Guid.Empty.Equals(((ADObjectId)item).ObjectGuid))
			{
				result = ((ADObjectId)item).ObjectGuid.ToString();
			}
			else
			{
				result = item.ToQuotationEscapedString();
			}
			return result;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000083D1 File Offset: 0x000065D1
		public static bool IsNullValue(this object item)
		{
			return item == null || DBNull.Value.Equals(item);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000083E6 File Offset: 0x000065E6
		public static bool IsTrue(this object item)
		{
			if (item.IsNullValue())
			{
				return false;
			}
			if (item is bool)
			{
				return (bool)item;
			}
			throw new ArgumentException("item is not bool or Nullable<bool>");
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000840B File Offset: 0x0000660B
		public static bool IsFalse(object item)
		{
			if (item.IsNullValue())
			{
				return false;
			}
			if (item is bool)
			{
				return !(bool)item;
			}
			throw new ArgumentException("value is not bool or Nullable<bool>");
		}

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x06000203 RID: 515
		public delegate bool IsQuotationRequiredDelegate(object value);
	}
}
