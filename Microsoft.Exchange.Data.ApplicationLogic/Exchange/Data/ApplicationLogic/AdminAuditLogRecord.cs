using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020000A8 RID: 168
	internal class AdminAuditLogRecord : IAuditLogRecord
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x0001B92E File Offset: 0x00019B2E
		public AdminAuditLogRecord(Trace trace)
		{
			if (trace == null)
			{
				throw new ArgumentNullException("trace");
			}
			this.Tracer = trace;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001B94B File Offset: 0x00019B4B
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001B953 File Offset: 0x00019B53
		public string Cmdlet { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001B95C File Offset: 0x00019B5C
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001B964 File Offset: 0x00019B64
		public IDictionary Parameters { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001B96D File Offset: 0x00019B6D
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001B975 File Offset: 0x00019B75
		public IDictionary ModifiedPropertyValues { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001B97E File Offset: 0x00019B7E
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001B986 File Offset: 0x00019B86
		public IDictionary OriginalPropertyValues { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001B98F File Offset: 0x00019B8F
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001B997 File Offset: 0x00019B97
		public string ObjectModified { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001B9A0 File Offset: 0x00019BA0
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001B9A8 File Offset: 0x00019BA8
		public string ModifiedObjectResolvedName { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0001B9B1 File Offset: 0x00019BB1
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001B9B9 File Offset: 0x00019BB9
		public bool Succeeded { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001B9C2 File Offset: 0x00019BC2
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001B9CA File Offset: 0x00019BCA
		public bool ExternalAccess { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001B9D3 File Offset: 0x00019BD3
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x0001B9DB File Offset: 0x00019BDB
		public string Error { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001B9E4 File Offset: 0x00019BE4
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001B9EC File Offset: 0x00019BEC
		public string UserId { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001B9F5 File Offset: 0x00019BF5
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x0001B9FD File Offset: 0x00019BFD
		public DateTime RunDate { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0001BA06 File Offset: 0x00019C06
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x0001BA0E File Offset: 0x00019C0E
		public bool Verbose { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001BA17 File Offset: 0x00019C17
		public AuditLogRecordType RecordType
		{
			get
			{
				return AuditLogRecordType.AdminAudit;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001BA1A File Offset: 0x00019C1A
		public DateTime CreationTime
		{
			get
			{
				return this.RunDate;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0001BA22 File Offset: 0x00019C22
		public string Operation
		{
			get
			{
				return this.Cmdlet;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001BA2A File Offset: 0x00019C2A
		public string ObjectId
		{
			get
			{
				return this.ObjectModified;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public IEnumerable<KeyValuePair<string, string>> GetDetails()
		{
			yield return new KeyValuePair<string, string>("Cmdlet Name", this.Cmdlet);
			yield return new KeyValuePair<string, string>("Object Modified", this.ObjectModified);
			if (this.Verbose && this.ModifiedObjectResolvedName != null && !this.ModifiedObjectResolvedName.Equals(this.ObjectModified))
			{
				yield return new KeyValuePair<string, string>("Modified Object Resolved Name", this.ModifiedObjectResolvedName);
			}
			foreach (KeyValuePair<string, string> parameter in this.GetPropertyBagDetails("Parameter", this.Parameters, true))
			{
				yield return parameter;
			}
			if (this.Verbose)
			{
				foreach (KeyValuePair<string, string> parameter2 in this.GetPropertyBagDetails("Property Modified", this.ModifiedPropertyValues, true))
				{
					yield return parameter2;
				}
				foreach (KeyValuePair<string, string> parameter3 in this.GetPropertyBagDetails("Property Original", this.OriginalPropertyValues, true))
				{
					yield return parameter3;
				}
			}
			yield return new KeyValuePair<string, string>("Caller", this.UserId);
			yield return new KeyValuePair<string, string>("ExternalAccess", this.ExternalAccess.ToString());
			yield return new KeyValuePair<string, string>("Succeeded", this.Succeeded.ToString());
			if (this.Verbose)
			{
				yield return new KeyValuePair<string, string>("Error", this.Error);
			}
			yield return new KeyValuePair<string, string>("Run Date", string.Format("{0:s}", this.RunDate));
			yield return new KeyValuePair<string, string>("OriginatingServer", string.Format("{0} ({1})", AdminAuditLogRecord.MachineName, "15.00.1497.010"));
			yield break;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001BFE1 File Offset: 0x0001A1E1
		private static StringBuilder GetValueString(object value)
		{
			return AdminAuditLogRecord.GetValueString(value, ';');
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public static StringBuilder GetValueString(object value, char delimiter)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (value is byte[])
			{
				int length = Math.Min(((byte[])value).Length, AdminAuditLogRecord.MaximumByteArrayPropertyValueSize);
				stringBuilder.Append(Convert.ToBase64String((byte[])value, 0, length));
				stringBuilder.Append("...");
			}
			else if (value is ICollection)
			{
				foreach (object value2 in ((ICollection)value))
				{
					stringBuilder.Append(AdminAuditLogRecord.GetLoggableString(value2) + delimiter);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
			}
			else
			{
				stringBuilder.Append(AdminAuditLogRecord.GetLoggableString(value));
			}
			return stringBuilder;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001C0C8 File Offset: 0x0001A2C8
		private static string GetLoggableString(object value)
		{
			if (value is SecureString)
			{
				return Strings.SecureStringParameter;
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001C39C File Offset: 0x0001A59C
		private IEnumerable<KeyValuePair<string, string>> GetPropertyBagDetails(string label, IDictionary propertyBag, bool truncate)
		{
			if (propertyBag != null)
			{
				foreach (object obj in propertyBag.Keys)
				{
					string propName = (string)obj;
					StringBuilder value = AdminAuditLogRecord.GetValueString(propertyBag[propName]);
					if (truncate && value.Length > AdminAuditLogRecord.MaximumPropertyValueSize)
					{
						int num = value.Length - AdminAuditLogRecord.MaximumPropertyValueSize;
						value.Remove(AdminAuditLogRecord.MaximumPropertyValueSize, num);
						value.Append("...");
						if (this.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							this.Tracer.TraceDebug<StringBuilder, int>((long)this.GetHashCode(), "{0} Successfully truncated property bag value by {1} characters", value, num);
						}
					}
					yield return new KeyValuePair<string, string>(label, string.Format("{0} = {1}", propName, value.ToString()));
				}
			}
			yield break;
		}

		// Token: 0x04000335 RID: 821
		private static readonly string MachineName = Environment.MachineName;

		// Token: 0x04000336 RID: 822
		private static int MaximumPropertyValueSize = 1024;

		// Token: 0x04000337 RID: 823
		private static int MaximumByteArrayPropertyValueSize = 64;

		// Token: 0x04000338 RID: 824
		private readonly Trace Tracer;
	}
}
