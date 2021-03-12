using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	// Token: 0x0200011C RID: 284
	[ComVisible(true)]
	[Serializable]
	public sealed class OperatingSystem : ICloneable, ISerializable
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x000328D8 File Offset: 0x00030AD8
		private OperatingSystem()
		{
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000328E0 File Offset: 0x00030AE0
		public OperatingSystem(PlatformID platform, Version version) : this(platform, version, null)
		{
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000328EC File Offset: 0x00030AEC
		internal OperatingSystem(PlatformID platform, Version version, string servicePack)
		{
			if (platform < PlatformID.Win32S || platform > PlatformID.MacOSX)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[]
				{
					(int)platform
				}), "platform");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this._platform = platform;
			this._version = (Version)version.Clone();
			this._servicePack = servicePack;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00032958 File Offset: 0x00030B58
		private OperatingSystem(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "_version"))
				{
					if (!(name == "_platform"))
					{
						if (name == "_servicePack")
						{
							this._servicePack = info.GetString("_servicePack");
						}
					}
					else
					{
						this._platform = (PlatformID)info.GetValue("_platform", typeof(PlatformID));
					}
				}
				else
				{
					this._version = (Version)info.GetValue("_version", typeof(Version));
				}
			}
			if (this._version == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_MissField", new object[]
				{
					"_version"
				}));
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00032A34 File Offset: 0x00030C34
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_version", this._version);
			info.AddValue("_platform", this._platform);
			info.AddValue("_servicePack", this._servicePack);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00032A87 File Offset: 0x00030C87
		public PlatformID Platform
		{
			get
			{
				return this._platform;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x00032A8F File Offset: 0x00030C8F
		public string ServicePack
		{
			get
			{
				if (this._servicePack == null)
				{
					return string.Empty;
				}
				return this._servicePack;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00032AA5 File Offset: 0x00030CA5
		public Version Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00032AAD File Offset: 0x00030CAD
		public object Clone()
		{
			return new OperatingSystem(this._platform, this._version, this._servicePack);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00032AC6 File Offset: 0x00030CC6
		public override string ToString()
		{
			return this.VersionString;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00032AD0 File Offset: 0x00030CD0
		public string VersionString
		{
			get
			{
				if (this._versionString != null)
				{
					return this._versionString;
				}
				string str;
				switch (this._platform)
				{
				case PlatformID.Win32S:
					str = "Microsoft Win32S ";
					goto IL_9A;
				case PlatformID.Win32Windows:
					if (this._version.Major > 4 || (this._version.Major == 4 && this._version.Minor > 0))
					{
						str = "Microsoft Windows 98 ";
						goto IL_9A;
					}
					str = "Microsoft Windows 95 ";
					goto IL_9A;
				case PlatformID.Win32NT:
					str = "Microsoft Windows NT ";
					goto IL_9A;
				case PlatformID.WinCE:
					str = "Microsoft Windows CE ";
					goto IL_9A;
				case PlatformID.MacOSX:
					str = "Mac OS X ";
					goto IL_9A;
				}
				str = "<unknown> ";
				IL_9A:
				if (string.IsNullOrEmpty(this._servicePack))
				{
					this._versionString = str + this._version.ToString();
				}
				else
				{
					this._versionString = str + this._version.ToString(3) + " " + this._servicePack;
				}
				return this._versionString;
			}
		}

		// Token: 0x040005CF RID: 1487
		private Version _version;

		// Token: 0x040005D0 RID: 1488
		private PlatformID _platform;

		// Token: 0x040005D1 RID: 1489
		private string _servicePack;

		// Token: 0x040005D2 RID: 1490
		private string _versionString;
	}
}
