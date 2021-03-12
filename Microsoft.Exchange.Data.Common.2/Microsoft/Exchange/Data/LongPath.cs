using System;
using System.Globalization;
using System.IO;
using System.Security;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class LongPath : IComparable, IComparable<LongPath>, IEquatable<LongPath>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003653 File Offset: 0x00001853
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000365B File Offset: 0x0000185B
		public string PathName
		{
			get
			{
				return this.pathName;
			}
			protected set
			{
				this.pathName = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003664 File Offset: 0x00001864
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000366C File Offset: 0x0000186C
		protected bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003675 File Offset: 0x00001875
		public bool IsLocalFull
		{
			get
			{
				return this.isLocalFull;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000367D File Offset: 0x0000187D
		public bool IsUnc
		{
			get
			{
				return this.isUnc;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003685 File Offset: 0x00001885
		public string DriveName
		{
			get
			{
				if (!this.IsLocalFull)
				{
					throw new NotSupportedException("DriveName");
				}
				return this.driveName;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000036A0 File Offset: 0x000018A0
		public string ServerName
		{
			get
			{
				if (!this.IsUnc)
				{
					throw new NotSupportedException("ServerName");
				}
				return this.serverName;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000036BB File Offset: 0x000018BB
		protected LongPath()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000036C3 File Offset: 0x000018C3
		public static LongPath Parse(string path)
		{
			return LongPath.ParseInternal(path, new LongPath());
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000036D0 File Offset: 0x000018D0
		public static explicit operator LongPath(FileInfo file)
		{
			if (file == null)
			{
				return null;
			}
			return LongPath.Parse(file.FullName);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000036E2 File Offset: 0x000018E2
		public static explicit operator LongPath(DirectoryInfo dir)
		{
			if (dir == null)
			{
				return null;
			}
			return LongPath.Parse(dir.FullName);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000036F4 File Offset: 0x000018F4
		public static bool TryParse(string path, out LongPath resultObject)
		{
			resultObject = LongPath.TryParseInternal(path, new LongPath());
			return null != resultObject;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000370B File Offset: 0x0000190B
		protected static LongPath ParseInternal(string path, LongPath pathObject)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (!pathObject.ParseCore(path, false))
			{
				throw new ArgumentException(DataStrings.ErrorLongPathCannotConvert(path), "path");
			}
			return pathObject;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003737 File Offset: 0x00001937
		protected static LongPath TryParseInternal(string path, LongPath pathObject)
		{
			if (pathObject.ParseCore(path, true))
			{
				return pathObject;
			}
			return null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003748 File Offset: 0x00001948
		protected virtual bool ParseCore(string path, bool nothrow)
		{
			try
			{
				if (!string.IsNullOrEmpty(path))
				{
					if (path.Length == 2 && path[1] == Path.VolumeSeparatorChar)
					{
						path += Path.DirectorySeparatorChar;
					}
					if (LongPath.IsLongPath(path) && ((this.isLocalFull = LongPath.IsLocalFullPath(path, out this.driveName)) || (this.isUnc = LongPath.IsUncPath(path, out this.serverName))))
					{
						string text = Path.GetFullPath(path);
						if (this.IsUnc || (this.IsLocalFull && !text.Equals(Path.GetPathRoot(text), StringComparison.OrdinalIgnoreCase)))
						{
							text = text.TrimEnd(new char[]
							{
								Path.DirectorySeparatorChar,
								Path.AltDirectorySeparatorChar
							});
						}
						this.PathName = text;
						this.IsValid = true;
					}
				}
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (PathTooLongException)
			{
			}
			catch (SecurityException ex)
			{
				if (!nothrow)
				{
					throw new ArgumentException(ex.Message, ex);
				}
			}
			return this.IsValid;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003874 File Offset: 0x00001A74
		private static bool IsLongPath(string path)
		{
			return path != null && -1 == path.IndexOf('~');
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003888 File Offset: 0x00001A88
		private static bool IsLocalFullPath(string path, out string drive)
		{
			bool flag = false;
			drive = null;
			try
			{
				flag = !new Uri(path).IsUnc;
			}
			catch (UriFormatException ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			if (flag)
			{
				if (!Path.IsPathRooted(path) || -1 == path.IndexOf(Path.VolumeSeparatorChar))
				{
					flag = false;
				}
				else
				{
					drive = path.Substring(0, path.IndexOf(Path.VolumeSeparatorChar) + 1);
				}
			}
			return flag;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003900 File Offset: 0x00001B00
		private static bool IsUncPath(string path, out string server)
		{
			server = null;
			try
			{
				Uri uri = new Uri(path);
				if (uri.IsUnc)
				{
					server = uri.Host;
				}
			}
			catch (UriFormatException ex)
			{
				throw new ArgumentException(ex.Message, ex);
			}
			return null != server;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003950 File Offset: 0x00001B50
		public override string ToString()
		{
			return this.PathName;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003958 File Offset: 0x00001B58
		public override int GetHashCode()
		{
			return this.PathName.ToLower(CultureInfo.InvariantCulture).GetHashCode();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003970 File Offset: 0x00001B70
		public int CompareTo(LongPath value)
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
			return string.Compare(this.PathName, value.PathName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000039E0 File Offset: 0x00001BE0
		public int CompareTo(object value)
		{
			LongPath longPath = value as LongPath;
			if (null == longPath && value != null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "object must be of type {0}", new object[]
				{
					base.GetType().Name
				}));
			}
			return this.CompareTo(longPath);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003A32 File Offset: 0x00001C32
		public override bool Equals(object value)
		{
			return this.Equals(value as LongPath);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003A40 File Offset: 0x00001C40
		public bool Equals(LongPath value)
		{
			if (object.ReferenceEquals(this, value))
			{
				return true;
			}
			bool result = false;
			if (null != value && base.GetType() == value.GetType())
			{
				result = string.Equals(this.PathName, value.PathName, StringComparison.OrdinalIgnoreCase);
			}
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003A8A File Offset: 0x00001C8A
		public static bool operator ==(LongPath a, LongPath b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003A93 File Offset: 0x00001C93
		public static bool operator !=(LongPath a, LongPath b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x0400002B RID: 43
		private string pathName;

		// Token: 0x0400002C RID: 44
		private bool isValid;

		// Token: 0x0400002D RID: 45
		private bool isLocalFull;

		// Token: 0x0400002E RID: 46
		private bool isUnc;

		// Token: 0x0400002F RID: 47
		private string driveName;

		// Token: 0x04000030 RID: 48
		private string serverName;
	}
}
