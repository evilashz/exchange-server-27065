using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x0200015B RID: 347
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>
	{
		// Token: 0x06001595 RID: 5525 RVA: 0x0003F8C8 File Offset: 0x0003DAC8
		[__DynamicallyInvokable]
		public Version(int major, int minor, int build, int revision)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (revision < 0)
			{
				throw new ArgumentOutOfRangeException("revision", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
			this._Revision = revision;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0003F96C File Offset: 0x0003DB6C
		[__DynamicallyInvokable]
		public Version(int major, int minor, int build)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (build < 0)
			{
				throw new ArgumentOutOfRangeException("build", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			this._Major = major;
			this._Minor = minor;
			this._Build = build;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0003F9F0 File Offset: 0x0003DBF0
		[__DynamicallyInvokable]
		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", Environment.GetResourceString("ArgumentOutOfRange_Version"));
			}
			this._Major = major;
			this._Minor = minor;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0003FA54 File Offset: 0x0003DC54
		[__DynamicallyInvokable]
		public Version(string version)
		{
			Version version2 = Version.Parse(version);
			this._Major = version2.Major;
			this._Minor = version2.Minor;
			this._Build = version2.Build;
			this._Revision = version2.Revision;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0003FAAC File Offset: 0x0003DCAC
		public Version()
		{
			this._Major = 0;
			this._Minor = 0;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0003FAD0 File Offset: 0x0003DCD0
		[__DynamicallyInvokable]
		public int Major
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Major;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0003FAD8 File Offset: 0x0003DCD8
		[__DynamicallyInvokable]
		public int Minor
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Minor;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0003FAE0 File Offset: 0x0003DCE0
		[__DynamicallyInvokable]
		public int Build
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Build;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0003FAE8 File Offset: 0x0003DCE8
		[__DynamicallyInvokable]
		public int Revision
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Revision;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0003FAF0 File Offset: 0x0003DCF0
		[__DynamicallyInvokable]
		public short MajorRevision
		{
			[__DynamicallyInvokable]
			get
			{
				return (short)(this._Revision >> 16);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x0003FAFC File Offset: 0x0003DCFC
		[__DynamicallyInvokable]
		public short MinorRevision
		{
			[__DynamicallyInvokable]
			get
			{
				return (short)(this._Revision & 65535);
			}
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0003FB0C File Offset: 0x0003DD0C
		public object Clone()
		{
			return new Version
			{
				_Major = this._Major,
				_Minor = this._Minor,
				_Build = this._Build,
				_Revision = this._Revision
			};
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0003FB50 File Offset: 0x0003DD50
		public int CompareTo(object version)
		{
			if (version == null)
			{
				return 1;
			}
			Version version2 = version as Version;
			if (version2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeVersion"));
			}
			if (this._Major != version2._Major)
			{
				if (this._Major > version2._Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Minor != version2._Minor)
			{
				if (this._Minor > version2._Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Build != version2._Build)
			{
				if (this._Build > version2._Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this._Revision == version2._Revision)
				{
					return 0;
				}
				if (this._Revision > version2._Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0003FC04 File Offset: 0x0003DE04
		[__DynamicallyInvokable]
		public int CompareTo(Version value)
		{
			if (value == null)
			{
				return 1;
			}
			if (this._Major != value._Major)
			{
				if (this._Major > value._Major)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Minor != value._Minor)
			{
				if (this._Minor > value._Minor)
				{
					return 1;
				}
				return -1;
			}
			else if (this._Build != value._Build)
			{
				if (this._Build > value._Build)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (this._Revision == value._Revision)
				{
					return 0;
				}
				if (this._Revision > value._Revision)
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0003FCA0 File Offset: 0x0003DEA0
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			Version version = obj as Version;
			return !(version == null) && this._Major == version._Major && this._Minor == version._Minor && this._Build == version._Build && this._Revision == version._Revision;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0003FCFC File Offset: 0x0003DEFC
		[__DynamicallyInvokable]
		public bool Equals(Version obj)
		{
			return !(obj == null) && this._Major == obj._Major && this._Minor == obj._Minor && this._Build == obj._Build && this._Revision == obj._Revision;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0003FD50 File Offset: 0x0003DF50
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			int num = 0;
			num |= (this._Major & 15) << 28;
			num |= (this._Minor & 255) << 20;
			num |= (this._Build & 255) << 12;
			return num | (this._Revision & 4095);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0003FDA2 File Offset: 0x0003DFA2
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this._Build == -1)
			{
				return this.ToString(2);
			}
			if (this._Revision == -1)
			{
				return this.ToString(3);
			}
			return this.ToString(4);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
		[__DynamicallyInvokable]
		public string ToString(int fieldCount)
		{
			switch (fieldCount)
			{
			case 0:
				return string.Empty;
			case 1:
				return this._Major.ToString();
			case 2:
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				Version.AppendPositiveNumber(this._Major, stringBuilder);
				stringBuilder.Append('.');
				Version.AppendPositiveNumber(this._Minor, stringBuilder);
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
			default:
				if (this._Build == -1)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
					{
						"0",
						"2"
					}), "fieldCount");
				}
				if (fieldCount == 3)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					Version.AppendPositiveNumber(this._Major, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Minor, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Build, stringBuilder);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				if (this._Revision == -1)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
					{
						"0",
						"3"
					}), "fieldCount");
				}
				if (fieldCount == 4)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					Version.AppendPositiveNumber(this._Major, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Minor, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Build, stringBuilder);
					stringBuilder.Append('.');
					Version.AppendPositiveNumber(this._Revision, stringBuilder);
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper", new object[]
				{
					"0",
					"4"
				}), "fieldCount");
			}
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0003FF6C File Offset: 0x0003E16C
		private static void AppendPositiveNumber(int num, StringBuilder sb)
		{
			int length = sb.Length;
			do
			{
				int num2 = num % 10;
				num /= 10;
				sb.Insert(length, (char)(48 + num2));
			}
			while (num > 0);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0003FF9C File Offset: 0x0003E19C
		[__DynamicallyInvokable]
		public static Version Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			Version.VersionResult versionResult = default(Version.VersionResult);
			versionResult.Init("input", true);
			if (!Version.TryParseVersion(input, ref versionResult))
			{
				throw versionResult.GetVersionParseException();
			}
			return versionResult.m_parsedVersion;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		[__DynamicallyInvokable]
		public static bool TryParse(string input, out Version result)
		{
			Version.VersionResult versionResult = default(Version.VersionResult);
			versionResult.Init("input", false);
			bool result2 = Version.TryParseVersion(input, ref versionResult);
			result = versionResult.m_parsedVersion;
			return result2;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00040018 File Offset: 0x0003E218
		private static bool TryParseVersion(string version, ref Version.VersionResult result)
		{
			if (version == null)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentNullException);
				return false;
			}
			string[] array = version.Split(Version.SeparatorsArray);
			int num = array.Length;
			if (num < 2 || num > 4)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentException);
				return false;
			}
			int major;
			if (!Version.TryParseComponent(array[0], "version", ref result, out major))
			{
				return false;
			}
			int minor;
			if (!Version.TryParseComponent(array[1], "version", ref result, out minor))
			{
				return false;
			}
			num -= 2;
			if (num > 0)
			{
				int build;
				if (!Version.TryParseComponent(array[2], "build", ref result, out build))
				{
					return false;
				}
				num--;
				if (num > 0)
				{
					int revision;
					if (!Version.TryParseComponent(array[3], "revision", ref result, out revision))
					{
						return false;
					}
					result.m_parsedVersion = new Version(major, minor, build, revision);
				}
				else
				{
					result.m_parsedVersion = new Version(major, minor, build);
				}
			}
			else
			{
				result.m_parsedVersion = new Version(major, minor);
			}
			return true;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000400F0 File Offset: 0x0003E2F0
		private static bool TryParseComponent(string component, string componentName, ref Version.VersionResult result, out int parsedComponent)
		{
			if (!int.TryParse(component, NumberStyles.Integer, CultureInfo.InvariantCulture, out parsedComponent))
			{
				result.SetFailure(Version.ParseFailureKind.FormatException, component);
				return false;
			}
			if (parsedComponent < 0)
			{
				result.SetFailure(Version.ParseFailureKind.ArgumentOutOfRangeException, componentName);
				return false;
			}
			return true;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004011B File Offset: 0x0003E31B
		[__DynamicallyInvokable]
		public static bool operator ==(Version v1, Version v2)
		{
			if (v1 == null)
			{
				return v2 == null;
			}
			return v1.Equals(v2);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004012C File Offset: 0x0003E32C
		[__DynamicallyInvokable]
		public static bool operator !=(Version v1, Version v2)
		{
			return !(v1 == v2);
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00040138 File Offset: 0x0003E338
		[__DynamicallyInvokable]
		public static bool operator <(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) < 0;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00040152 File Offset: 0x0003E352
		[__DynamicallyInvokable]
		public static bool operator <=(Version v1, Version v2)
		{
			if (v1 == null)
			{
				throw new ArgumentNullException("v1");
			}
			return v1.CompareTo(v2) <= 0;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0004016F File Offset: 0x0003E36F
		[__DynamicallyInvokable]
		public static bool operator >(Version v1, Version v2)
		{
			return v2 < v1;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00040178 File Offset: 0x0003E378
		[__DynamicallyInvokable]
		public static bool operator >=(Version v1, Version v2)
		{
			return v2 <= v1;
		}

		// Token: 0x04000742 RID: 1858
		private int _Major;

		// Token: 0x04000743 RID: 1859
		private int _Minor;

		// Token: 0x04000744 RID: 1860
		private int _Build = -1;

		// Token: 0x04000745 RID: 1861
		private int _Revision = -1;

		// Token: 0x04000746 RID: 1862
		private static readonly char[] SeparatorsArray = new char[]
		{
			'.'
		};

		// Token: 0x04000747 RID: 1863
		private const int ZERO_CHAR_VALUE = 48;

		// Token: 0x02000AD6 RID: 2774
		internal enum ParseFailureKind
		{
			// Token: 0x04003159 RID: 12633
			ArgumentNullException,
			// Token: 0x0400315A RID: 12634
			ArgumentException,
			// Token: 0x0400315B RID: 12635
			ArgumentOutOfRangeException,
			// Token: 0x0400315C RID: 12636
			FormatException
		}

		// Token: 0x02000AD7 RID: 2775
		internal struct VersionResult
		{
			// Token: 0x06006966 RID: 26982 RVA: 0x0016AC1D File Offset: 0x00168E1D
			internal void Init(string argumentName, bool canThrow)
			{
				this.m_canThrow = canThrow;
				this.m_argumentName = argumentName;
			}

			// Token: 0x06006967 RID: 26983 RVA: 0x0016AC2D File Offset: 0x00168E2D
			internal void SetFailure(Version.ParseFailureKind failure)
			{
				this.SetFailure(failure, string.Empty);
			}

			// Token: 0x06006968 RID: 26984 RVA: 0x0016AC3B File Offset: 0x00168E3B
			internal void SetFailure(Version.ParseFailureKind failure, string argument)
			{
				this.m_failure = failure;
				this.m_exceptionArgument = argument;
				if (this.m_canThrow)
				{
					throw this.GetVersionParseException();
				}
			}

			// Token: 0x06006969 RID: 26985 RVA: 0x0016AC5C File Offset: 0x00168E5C
			internal Exception GetVersionParseException()
			{
				switch (this.m_failure)
				{
				case Version.ParseFailureKind.ArgumentNullException:
					return new ArgumentNullException(this.m_argumentName);
				case Version.ParseFailureKind.ArgumentException:
					return new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
				case Version.ParseFailureKind.ArgumentOutOfRangeException:
					return new ArgumentOutOfRangeException(this.m_exceptionArgument, Environment.GetResourceString("ArgumentOutOfRange_Version"));
				case Version.ParseFailureKind.FormatException:
					try
					{
						int.Parse(this.m_exceptionArgument, CultureInfo.InvariantCulture);
					}
					catch (FormatException result)
					{
						return result;
					}
					catch (OverflowException result2)
					{
						return result2;
					}
					return new FormatException(Environment.GetResourceString("Format_InvalidString"));
				default:
					return new ArgumentException(Environment.GetResourceString("Arg_VersionString"));
				}
			}

			// Token: 0x0400315D RID: 12637
			internal Version m_parsedVersion;

			// Token: 0x0400315E RID: 12638
			internal Version.ParseFailureKind m_failure;

			// Token: 0x0400315F RID: 12639
			internal string m_exceptionArgument;

			// Token: 0x04003160 RID: 12640
			internal string m_argumentName;

			// Token: 0x04003161 RID: 12641
			internal bool m_canThrow;
		}
	}
}
