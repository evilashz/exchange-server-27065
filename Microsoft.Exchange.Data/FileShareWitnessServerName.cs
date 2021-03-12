using System;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000156 RID: 342
	[Serializable]
	public class FileShareWitnessServerName : IComparable, IComparable<FileShareWitnessServerName>, IEquatable<FileShareWitnessServerName>, ISerializable
	{
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00022B8C File Offset: 0x00020D8C
		public bool isHostName
		{
			get
			{
				return this.m_isHostName;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00022B94 File Offset: 0x00020D94
		public bool isFqdn
		{
			get
			{
				return !this.m_isHostName;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00022B9F File Offset: 0x00020D9F
		public string Fqdn
		{
			get
			{
				return this.m_fqdn;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00022BA7 File Offset: 0x00020DA7
		public string HostName
		{
			get
			{
				return this.m_hostName;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00022BAF File Offset: 0x00020DAF
		public string DomainName
		{
			get
			{
				return this.m_domainName;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00022BB7 File Offset: 0x00020DB7
		public string RawData
		{
			get
			{
				if (this.isFqdn)
				{
					return this.Fqdn;
				}
				return this.HostName;
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00022BCE File Offset: 0x00020DCE
		protected FileShareWitnessServerName()
		{
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00022BD6 File Offset: 0x00020DD6
		public static FileShareWitnessServerName Parse(string FileShareWitnessServerName)
		{
			return FileShareWitnessServerName.ParseInternal(FileShareWitnessServerName, new FileShareWitnessServerName());
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00022BE3 File Offset: 0x00020DE3
		public static bool TryParse(string FileShareWitnessServerName, out FileShareWitnessServerName FileShareWitnessServerNameObject)
		{
			FileShareWitnessServerNameObject = FileShareWitnessServerName.TryParseInternal(FileShareWitnessServerName, new FileShareWitnessServerName());
			return null != FileShareWitnessServerNameObject;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00022BFA File Offset: 0x00020DFA
		protected static FileShareWitnessServerName ParseInternal(string FileShareWitnessServerName, FileShareWitnessServerName FileShareWitnessServerNameObject)
		{
			if (FileShareWitnessServerName == null)
			{
				throw new ArgumentNullException("FileShareWitnessServerName");
			}
			if (!FileShareWitnessServerNameObject.ParseCore(FileShareWitnessServerName, false))
			{
				throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameCannotConvert(FileShareWitnessServerName), "FileShareWitnessServerName");
			}
			return FileShareWitnessServerNameObject;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00022C2B File Offset: 0x00020E2B
		protected static FileShareWitnessServerName TryParseInternal(string FileShareWitnessServerName, FileShareWitnessServerName FileShareWitnessServerNameObject)
		{
			if (FileShareWitnessServerNameObject.ParseCore(FileShareWitnessServerName, true))
			{
				return FileShareWitnessServerNameObject;
			}
			return null;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x00022C3A File Offset: 0x00020E3A
		private void AssignFromHostName(string hostName, bool nothrow)
		{
			this.m_fqdn = null;
			this.m_hostName = hostName;
			this.m_domainName = null;
			this.m_isValid = true;
			this.m_isHostName = true;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00022C60 File Offset: 0x00020E60
		private void AssignFromFqdn(string fqdn, bool nothrow)
		{
			int num = fqdn.IndexOf('.');
			if (num > -1)
			{
				this.m_fqdn = fqdn;
				this.m_hostName = fqdn.Substring(0, num);
				this.m_domainName = fqdn.Substring(num + 1);
				this.m_isValid = true;
				this.m_isHostName = false;
				return;
			}
			if (!nothrow)
			{
				throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdn(fqdn), "FileShareWitnessServerName");
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00022CC8 File Offset: 0x00020EC8
		protected virtual bool ParseCore(string FileShareWitnessServerName, bool nothrow)
		{
			IPAddress ipaddress;
			if (string.IsNullOrEmpty(FileShareWitnessServerName))
			{
				if (!nothrow)
				{
					throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameMustNotBeEmpty, "FileShareWitnessServerName");
				}
				return false;
			}
			else if (IPAddress.TryParse(FileShareWitnessServerName, out ipaddress))
			{
				if (!nothrow)
				{
					throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameMustNotBeIP(FileShareWitnessServerName), "FileShareWitnessServerName");
				}
				return false;
			}
			else if (string.Equals(FileShareWitnessServerName, FileShareWitnessServerName.s_localHostName, StringComparison.OrdinalIgnoreCase) || string.Equals(FileShareWitnessServerName, FileShareWitnessServerName.s_localHostFqdn, StringComparison.OrdinalIgnoreCase))
			{
				if (!nothrow)
				{
					throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameIsLocalhost(FileShareWitnessServerName), "FileShareWitnessServerName");
				}
				return false;
			}
			else
			{
				if (Microsoft.Exchange.Data.Fqdn.IsValidFqdn(FileShareWitnessServerName))
				{
					if (FileShareWitnessServerName.IndexOf('.') == -1)
					{
						this.AssignFromHostName(FileShareWitnessServerName, nothrow);
					}
					else
					{
						this.AssignFromFqdn(FileShareWitnessServerName, nothrow);
					}
					return this.m_isValid;
				}
				if (nothrow)
				{
					return false;
				}
				if (FileShareWitnessServerName.Contains(FileShareWitnessServerName.s_wildcard))
				{
					throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdnWildcard(FileShareWitnessServerName), "FileShareWitnessServerName");
				}
				throw new ArgumentException(DataStrings.ErrorFileShareWitnessServerNameIsNotValidHostNameorFqdn(FileShareWitnessServerName), "FileShareWitnessServerName");
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00022DBC File Offset: 0x00020FBC
		public override string ToString()
		{
			return this.RawData;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00022DC4 File Offset: 0x00020FC4
		public override int GetHashCode()
		{
			return this.RawData.ToLowerInvariant().GetHashCode();
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00022DD6 File Offset: 0x00020FD6
		public override bool Equals(object value)
		{
			return this.Equals(value as FileShareWitnessServerName);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00022DE4 File Offset: 0x00020FE4
		public static bool operator ==(FileShareWitnessServerName a, FileShareWitnessServerName b)
		{
			return FileShareWitnessServerName.Equals(a, b);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00022DED File Offset: 0x00020FED
		public static bool operator !=(FileShareWitnessServerName a, FileShareWitnessServerName b)
		{
			return !FileShareWitnessServerName.Equals(a, b);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00022DFC File Offset: 0x00020FFC
		public int CompareTo(object value)
		{
			FileShareWitnessServerName fileShareWitnessServerName = value as FileShareWitnessServerName;
			if (null == fileShareWitnessServerName && value != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "object must be of type {0}", new object[]
				{
					base.GetType().Name
				}));
			}
			return this.CompareTo(fileShareWitnessServerName);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00022E50 File Offset: 0x00021050
		public int CompareTo(FileShareWitnessServerName value)
		{
			if (null == value)
			{
				return 1;
			}
			if (object.ReferenceEquals(this, value))
			{
				return 0;
			}
			Type type = base.GetType();
			if (type != value.GetType())
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "object must be of type {0}", new object[]
				{
					type.Name
				}));
			}
			return string.Compare(this.RawData, value.RawData, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00022EC0 File Offset: 0x000210C0
		public bool Equals(FileShareWitnessServerName value)
		{
			if (object.ReferenceEquals(this, value))
			{
				return true;
			}
			bool result = false;
			if (null != value && base.GetType() == value.GetType())
			{
				result = string.Equals(this.RawData, value.RawData, StringComparison.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00022F0A File Offset: 0x0002110A
		public static bool Equals(FileShareWitnessServerName a, FileShareWitnessServerName b)
		{
			return object.ReferenceEquals(a, b) || (!object.ReferenceEquals(a, null) && !object.ReferenceEquals(b, null) && string.Equals(a.RawData, b.RawData, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00022F40 File Offset: 0x00021140
		private FileShareWitnessServerName(SerializationInfo info, StreamingContext context)
		{
			this.m_fqdn = (string)info.GetValue("fqdn", typeof(string));
			this.m_hostName = (string)info.GetValue("hostName", typeof(string));
			this.m_domainName = (string)info.GetValue("domainName", typeof(string));
			this.m_isValid = (bool)info.GetValue("isValid", typeof(bool));
			this.m_isHostName = (bool)info.GetValue("isHostName", typeof(bool));
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00022FF4 File Offset: 0x000211F4
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("fqdn", this.m_fqdn);
			info.AddValue("hostName", this.m_hostName);
			info.AddValue("domainName", this.m_domainName);
			info.AddValue("isValid", this.m_isValid);
			info.AddValue("isHostName", this.m_isHostName);
		}

		// Token: 0x040006F5 RID: 1781
		private string m_fqdn;

		// Token: 0x040006F6 RID: 1782
		private string m_hostName;

		// Token: 0x040006F7 RID: 1783
		private string m_domainName;

		// Token: 0x040006F8 RID: 1784
		private bool m_isValid;

		// Token: 0x040006F9 RID: 1785
		private bool m_isHostName;

		// Token: 0x040006FA RID: 1786
		private static string s_localHostName = "localhost";

		// Token: 0x040006FB RID: 1787
		private static string s_localHostFqdn = "localhost.localdomain";

		// Token: 0x040006FC RID: 1788
		private static string s_wildcard = "*";
	}
}
