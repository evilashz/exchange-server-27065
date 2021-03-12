using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000128 RID: 296
	public static class DDIUtil
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x00062748 File Offset: 0x00060948
		public static string NullableDateTimeToUserString(object value)
		{
			if (DDIHelper.IsEmptyValue(value))
			{
				return null;
			}
			DateTime? dateTime = (DateTime?)value;
			if (dateTime == null)
			{
				return null;
			}
			return dateTime.Value.LocalToUserDateTimeGeneralFormatString();
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0006277D File Offset: 0x0006097D
		public static string EnumToLocalizedString(object value)
		{
			if (value.GetType().Equals(typeof(DBNull)))
			{
				return string.Empty;
			}
			return LocalizedDescriptionAttribute.FromEnum(value.GetType(), value);
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000627A8 File Offset: 0x000609A8
		public static string EnumToLocalizedStringForOwaOption(object value)
		{
			return LocalizedDescriptionAttribute.FromEnumForOwaOption(value.GetType(), value);
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x000627B8 File Offset: 0x000609B8
		public static int Length(object value)
		{
			if (!DDIHelper.IsEmptyValue(value))
			{
				Array array = value as Array;
				if (array != null)
				{
					return array.Length;
				}
			}
			return 0;
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x000627DF File Offset: 0x000609DF
		public static string[] ToStringArray(this MultiValuedProperty<string> stringProperty)
		{
			if (MultiValuedPropertyBase.IsNullOrEmpty(stringProperty))
			{
				return new string[0];
			}
			return stringProperty.ToArray();
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000627F8 File Offset: 0x000609F8
		public static int GetLength(this object stringProperty)
		{
			MultiValuedProperty<string> multiValuedProperty = stringProperty as MultiValuedProperty<string>;
			if (multiValuedProperty != null)
			{
				string[] array = multiValuedProperty.ToStringArray();
				if (array != null)
				{
					return array.Length;
				}
			}
			return 0;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00062820 File Offset: 0x00060A20
		public static string Join(object separator, object value)
		{
			if (DDIHelper.IsEmptyValue(value))
			{
				return string.Empty;
			}
			List<object> list = value as List<object>;
			if (DDIHelper.IsEmptyValue(list))
			{
				return string.Empty;
			}
			if (separator == null || separator.ToString() == null)
			{
				separator = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in list)
			{
				if (!DDIHelper.IsEmptyValue(obj) && !DDIHelper.IsEmptyValue(obj.ToString()))
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(separator);
					}
					stringBuilder.Append(obj.ToString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x000628E4 File Offset: 0x00060AE4
		public static List<object> ADObjectIdsToNames(object adObjectIds)
		{
			IEnumerable<ADObjectId> enumerable = adObjectIds as IEnumerable<ADObjectId>;
			if (enumerable != null)
			{
				return (from x in enumerable
				select x.Name).ToList<object>();
			}
			return null;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00062928 File Offset: 0x00060B28
		internal static string ConvertUnlimitedToString<T>(object value, Converter<T, string> converter) where T : struct, IComparable
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			if (DBNull.Value.Equals(value))
			{
				return string.Empty;
			}
			Unlimited<T> unlimited = (Unlimited<T>)value;
			if (unlimited.IsUnlimited)
			{
				return "unlimited";
			}
			return converter(unlimited.Value);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00062987 File Offset: 0x00060B87
		internal static string ConvertStringToUnlimited(string value, Converter<string, string> converter)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("value");
			}
			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}
			if (!value.Equals("unlimited", StringComparison.OrdinalIgnoreCase))
			{
				return converter(value);
			}
			return value;
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000629C1 File Offset: 0x00060BC1
		public static string ZeroToUnlimited(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("value");
			}
			if (value == "0")
			{
				return "unlimited";
			}
			return value;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000629EA File Offset: 0x00060BEA
		public static string ZeroToUnlimited(object value)
		{
			if (value == null)
			{
				throw new ArgumentException("value");
			}
			return DDIUtil.ZeroToUnlimited(value.ToString());
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00062A08 File Offset: 0x00060C08
		public static string ConcatenateStringAndSubstring(object firstString, object secondString)
		{
			if (firstString == null)
			{
				return string.Empty;
			}
			if (secondString == null || secondString.ToString().Equals(string.Empty) || secondString.ToString().Equals("Success"))
			{
				return firstString.ToString();
			}
			return string.Format("{0} ({1})", firstString.ToString(), secondString.ToString());
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00062A64 File Offset: 0x00060C64
		public static string ByteQuantifiedSizeToMBString(object value)
		{
			ulong num = 0UL;
			try
			{
				Unlimited<ByteQuantifiedSize> unlimited = (Unlimited<ByteQuantifiedSize>)value;
				if (unlimited.IsUnlimited)
				{
					return "unlimited";
				}
				num = unlimited.Value.ToBytes();
			}
			catch (InvalidCastException)
			{
				try
				{
					num = ((ByteQuantifiedSize)value).ToBytes();
				}
				catch (InvalidCastException)
				{
					return null;
				}
			}
			return Math.Round(num / 1048576.0).ToString();
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00062AF4 File Offset: 0x00060CF4
		public static ByteQuantifiedSize MBStringToByteQuantifiedSize(object value)
		{
			string text = value as string;
			if (text != null)
			{
				return ByteQuantifiedSize.Parse((ulong.Parse(text) * 1024UL * 1024UL).ToString());
			}
			throw new ArgumentException("The argument should be string.");
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00062B38 File Offset: 0x00060D38
		public static Unlimited<ByteQuantifiedSize> MBStringToUnlimitedByteQuantifiedSize(object value)
		{
			string text = value as string;
			if (text == null)
			{
				throw new ArgumentException("The argument should be string.");
			}
			if (string.Equals(text.Trim(), Unlimited<ByteQuantifiedSize>.UnlimitedString, StringComparison.OrdinalIgnoreCase))
			{
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
			return Unlimited<ByteQuantifiedSize>.Parse((ulong.Parse(text) * 1024UL * 1024UL).ToString());
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00062B94 File Offset: 0x00060D94
		public static string RetractUsernameFromPSCredential(object value)
		{
			PSCredential pscredential = value as PSCredential;
			if (pscredential != null)
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				PSCredentialHelper.GetUserPassFromCredential(pscredential, out empty, out empty2, false);
				return empty;
			}
			return string.Empty;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00062BCC File Offset: 0x00060DCC
		public static string RetractPlainPasswordFromPSCredential(object value)
		{
			PSCredential pscredential = value as PSCredential;
			if (pscredential != null)
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				PSCredentialHelper.GetUserPassFromCredential(pscredential, out empty, out empty2, false);
				return empty2;
			}
			return string.Empty;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00062C01 File Offset: 0x00060E01
		public static bool IsInRole(string role)
		{
			return RbacCheckerWrapper.RbacChecker.IsInRole(role);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00062C0E File Offset: 0x00060E0E
		public static string UrlDecode(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return HttpUtility.UrlDecode(value);
			}
			return value;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00062C20 File Offset: 0x00060E20
		internal static PowerShellResults InsertWarning(PowerShellResults result, string warning)
		{
			string[] warnings = new string[]
			{
				warning
			};
			if (result.Warnings == null)
			{
				result.Warnings = warnings;
			}
			else
			{
				result.Warnings = result.Warnings.Concat(new string[]
				{
					warning
				}).ToArray<string>();
			}
			return result;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00062C70 File Offset: 0x00060E70
		internal static PowerShellResults InsertError(PowerShellResults result, string error)
		{
			Microsoft.Exchange.Management.ControlPanel.ErrorRecord[] array = new Microsoft.Exchange.Management.ControlPanel.ErrorRecord[]
			{
				new Microsoft.Exchange.Management.ControlPanel.ErrorRecord(new Exception(error))
			};
			if (result.ErrorRecords == null)
			{
				result.ErrorRecords = array;
			}
			else
			{
				result.ErrorRecords = result.ErrorRecords.Concat(array).ToArray<Microsoft.Exchange.Management.ControlPanel.ErrorRecord>();
			}
			return result;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00062CBD File Offset: 0x00060EBD
		internal static PowerShellResults[] InsertWarningIfSucceded(PowerShellResults[] results, string warning)
		{
			if (results != null && results.Length == 1 && results[0].Succeeded)
			{
				DDIUtil.InsertWarning(results[0], warning);
			}
			return results;
		}
	}
}
