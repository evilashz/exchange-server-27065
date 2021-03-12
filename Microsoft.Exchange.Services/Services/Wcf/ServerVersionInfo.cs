using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DC8 RID: 3528
	[XmlRoot(IsNullable = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ServerVersionInfo
	{
		// Token: 0x060059C3 RID: 22979 RVA: 0x001185E4 File Offset: 0x001167E4
		internal static ServerVersionInfo BuildFromExecutingAssembly()
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			return new ServerVersionInfo
			{
				MajorVersion = versionInfo.FileMajorPart,
				MinorVersion = versionInfo.FileMinorPart,
				MajorBuildNumber = versionInfo.FileBuildPart,
				MinorBuildNumber = versionInfo.FilePrivatePart,
				Version = ExchangeVersion.Latest.Version
			};
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x00118648 File Offset: 0x00116848
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x00118650 File Offset: 0x00116850
		[DataMember(Order = 1)]
		[XmlAttribute]
		public int MajorVersion
		{
			get
			{
				return this.majorVersionField;
			}
			set
			{
				this.majorVersionField = value;
				this.majorVersionFieldSpecified = true;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x00118660 File Offset: 0x00116860
		// (set) Token: 0x060059C7 RID: 22983 RVA: 0x00118668 File Offset: 0x00116868
		[XmlIgnore]
		public bool MajorVersionSpecified
		{
			get
			{
				return this.majorVersionFieldSpecified;
			}
			set
			{
				this.majorVersionFieldSpecified = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x00118671 File Offset: 0x00116871
		// (set) Token: 0x060059C9 RID: 22985 RVA: 0x00118679 File Offset: 0x00116879
		[DataMember(Order = 2)]
		[XmlAttribute]
		public int MinorVersion
		{
			get
			{
				return this.minorVersionField;
			}
			set
			{
				this.minorVersionField = value;
				this.minorVersionFieldSpecified = true;
			}
		}

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x00118689 File Offset: 0x00116889
		// (set) Token: 0x060059CB RID: 22987 RVA: 0x00118691 File Offset: 0x00116891
		[XmlIgnore]
		public bool MinorVersionSpecified
		{
			get
			{
				return this.minorVersionFieldSpecified;
			}
			set
			{
				this.minorVersionFieldSpecified = value;
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x0011869A File Offset: 0x0011689A
		// (set) Token: 0x060059CD RID: 22989 RVA: 0x001186A2 File Offset: 0x001168A2
		[DataMember(Order = 3)]
		[XmlAttribute]
		public int MajorBuildNumber
		{
			get
			{
				return this.majorBuildNumberField;
			}
			set
			{
				this.majorBuildNumberField = value;
				this.majorBuildNumberFieldSpecified = true;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060059CE RID: 22990 RVA: 0x001186B2 File Offset: 0x001168B2
		// (set) Token: 0x060059CF RID: 22991 RVA: 0x001186BA File Offset: 0x001168BA
		[XmlIgnore]
		public bool MajorBuildNumberSpecified
		{
			get
			{
				return this.majorBuildNumberFieldSpecified;
			}
			set
			{
				this.majorBuildNumberFieldSpecified = value;
			}
		}

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060059D0 RID: 22992 RVA: 0x001186C3 File Offset: 0x001168C3
		// (set) Token: 0x060059D1 RID: 22993 RVA: 0x001186CB File Offset: 0x001168CB
		[DataMember(Order = 4)]
		[XmlAttribute]
		public int MinorBuildNumber
		{
			get
			{
				return this.minorBuildNumberField;
			}
			set
			{
				this.minorBuildNumberField = value;
				this.minorBuildNumberFieldSpecified = true;
			}
		}

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060059D2 RID: 22994 RVA: 0x001186DB File Offset: 0x001168DB
		// (set) Token: 0x060059D3 RID: 22995 RVA: 0x001186E3 File Offset: 0x001168E3
		[XmlIgnore]
		public bool MinorBuildNumberSpecified
		{
			get
			{
				return this.minorBuildNumberFieldSpecified;
			}
			set
			{
				this.minorBuildNumberFieldSpecified = value;
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x001186EC File Offset: 0x001168EC
		// (set) Token: 0x060059D5 RID: 22997 RVA: 0x001186F4 File Offset: 0x001168F4
		[IgnoreDataMember]
		[XmlAttribute]
		public ExchangeVersionType Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x001186FD File Offset: 0x001168FD
		[XmlIgnore]
		public bool VersionSpecified
		{
			get
			{
				return ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1);
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x0011870E File Offset: 0x0011690E
		// (set) Token: 0x060059D8 RID: 23000 RVA: 0x0011871B File Offset: 0x0011691B
		[XmlIgnore]
		[DataMember(Name = "Version", Order = 5)]
		public string VersionString
		{
			get
			{
				return EnumUtilities.ToString<ExchangeVersionType>(this.Version);
			}
			set
			{
				this.versionField = EnumUtilities.Parse<ExchangeVersionType>(value);
			}
		}

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060059D9 RID: 23001 RVA: 0x00118729 File Offset: 0x00116929
		[XmlIgnore]
		public static ServerVersionInfo CurrentAssemblyVersion
		{
			get
			{
				return ServerVersionInfo.serverVersionSingleton.Member;
			}
		}

		// Token: 0x040031AD RID: 12717
		private static LazyMember<ServerVersionInfo> serverVersionSingleton = new LazyMember<ServerVersionInfo>(() => ServerVersionInfo.BuildFromExecutingAssembly());

		// Token: 0x040031AE RID: 12718
		private int majorVersionField;

		// Token: 0x040031AF RID: 12719
		private bool majorVersionFieldSpecified;

		// Token: 0x040031B0 RID: 12720
		private int minorVersionField;

		// Token: 0x040031B1 RID: 12721
		private bool minorVersionFieldSpecified;

		// Token: 0x040031B2 RID: 12722
		private int majorBuildNumberField;

		// Token: 0x040031B3 RID: 12723
		private bool majorBuildNumberFieldSpecified;

		// Token: 0x040031B4 RID: 12724
		private int minorBuildNumberField;

		// Token: 0x040031B5 RID: 12725
		private bool minorBuildNumberFieldSpecified;

		// Token: 0x040031B6 RID: 12726
		private ExchangeVersionType versionField;
	}
}
