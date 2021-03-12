using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200003A RID: 58
	internal static class AnalysisHelpers
	{
		// Token: 0x06000143 RID: 323
		[DllImport("Advapi32.dll", SetLastError = true)]
		public static extern bool GetSecurityDescriptorControl(IntPtr pSecurityDescriptor, out ValidationConstant.SecurityDescriptorControl sdcontrol, out int dwRevision);

		// Token: 0x06000144 RID: 324 RVA: 0x00006214 File Offset: 0x00004414
		public static int VersionCompare(string first, string second)
		{
			Version version = new Version(first);
			Version value = new Version(second);
			return version.CompareTo(value);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006238 File Offset: 0x00004438
		public static string[] Replace(string[] arrayToSearch, string pattern, string replacement)
		{
			if (arrayToSearch == null || arrayToSearch.Length == 0)
			{
				return arrayToSearch;
			}
			List<string> list = new List<string>();
			foreach (string val in arrayToSearch)
			{
				string text = AnalysisHelpers.Replace(val, pattern, replacement);
				if (text != null)
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006287 File Offset: 0x00004487
		public static string Replace(string val, string pattern, string replacement)
		{
			if (string.IsNullOrEmpty(val))
			{
				return val;
			}
			if (string.IsNullOrEmpty(pattern))
			{
				return val;
			}
			if (string.IsNullOrEmpty(replacement))
			{
				return val;
			}
			return Regex.Replace(val, pattern, replacement, AnalysisHelpers.matchOptions);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000062B4 File Offset: 0x000044B4
		public static string ConvertBinaryToString(object val)
		{
			string text = string.Empty;
			if (AnalysisHelpers.IsNullOrEmpty(val))
			{
				return text;
			}
			Type type = val.GetType();
			StringBuilder stringBuilder = new StringBuilder();
			if (type == typeof(string))
			{
				val = ((string)val).ToCharArray();
				text = string.Empty;
				foreach (char c in (char[])val)
				{
					string str = text;
					int num = (int)c;
					text = str + num.ToString("X4");
				}
			}
			else
			{
				if (!(type == typeof(byte[])))
				{
					throw new ArgumentException("val must be of type string or byte[].");
				}
				stringBuilder.Capacity = 2 * ((byte[])val).Length + 1;
				foreach (byte b in (byte[])val)
				{
					stringBuilder.Append(b.ToString("X2"));
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000063AA File Offset: 0x000045AA
		public static string ConvertToStringSid(object val)
		{
			if (AnalysisHelpers.IsNullOrEmpty(val))
			{
				return string.Empty;
			}
			if (val.GetType() == typeof(byte[]))
			{
				return AnalysisHelpers.ConvertToStringSid((byte[])val);
			}
			return val.ToString();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000063E4 File Offset: 0x000045E4
		public static string ConvertToStringSid(byte[] sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			GCHandle gchandle = GCHandle.Alloc(sid, GCHandleType.Pinned);
			IntPtr sid2 = gchandle.AddrOfPinnedObject();
			string empty = string.Empty;
			try
			{
				NativeMethodProvider.ConvertSidToStringSid(sid2, ref empty);
			}
			finally
			{
				gchandle.Free();
			}
			return empty;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000643C File Offset: 0x0000463C
		public static bool IsNullOrEmpty(object obj)
		{
			bool result = false;
			if (obj == null)
			{
				result = true;
			}
			else if (obj.GetType().IsArray)
			{
				if (((Array)obj).Length == 0)
				{
					result = true;
				}
			}
			else if (obj is string && string.IsNullOrEmpty((string)obj))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006488 File Offset: 0x00004688
		public static object GetObjectPropertyByName(object val, string propName)
		{
			PropertyInfo property = val.GetType().GetProperty(propName);
			if (property == null)
			{
				throw new ArgumentException(propName);
			}
			return property.GetValue(val, null);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000064BA File Offset: 0x000046BA
		public static bool Exist(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentException("filePath should not be null or empty");
			}
			return File.Exists(filePath) || Directory.Exists(filePath);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000064E0 File Offset: 0x000046E0
		public static bool Match(string pattern, params string[] strings)
		{
			if (string.IsNullOrEmpty(pattern))
			{
				throw new ArgumentException("Argument 'pattern' should not be null or empty.");
			}
			if (!AnalysisHelpers.IsNullOrEmpty(strings))
			{
				foreach (string text in strings)
				{
					if (!string.IsNullOrEmpty(text) && Regex.IsMatch(text, pattern, AnalysisHelpers.matchOptions))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000653C File Offset: 0x0000473C
		public static int SdGet(byte[] sd)
		{
			int result = 0;
			GCHandle gchandle = GCHandle.Alloc(sd, GCHandleType.Pinned);
			IntPtr pSecurityDescriptor = gchandle.AddrOfPinnedObject();
			try
			{
				ValidationConstant.SecurityDescriptorControl securityDescriptorControl;
				int num;
				AnalysisHelpers.GetSecurityDescriptorControl(pSecurityDescriptor, out securityDescriptorControl, out num);
				result = (int)securityDescriptorControl;
			}
			finally
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x040000F0 RID: 240
		private static RegexOptions matchOptions = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
	}
}
