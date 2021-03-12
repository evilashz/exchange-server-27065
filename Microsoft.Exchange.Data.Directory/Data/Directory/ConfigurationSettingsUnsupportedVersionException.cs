using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AFC RID: 2812
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsUnsupportedVersionException : ConfigurationSettingsException
	{
		// Token: 0x06008195 RID: 33173 RVA: 0x001A6B65 File Offset: 0x001A4D65
		public ConfigurationSettingsUnsupportedVersionException(string version) : base(DirectoryStrings.ConfigurationSettingsUnsupportedVersion(version))
		{
			this.version = version;
		}

		// Token: 0x06008196 RID: 33174 RVA: 0x001A6B7A File Offset: 0x001A4D7A
		public ConfigurationSettingsUnsupportedVersionException(string version, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsUnsupportedVersion(version), innerException)
		{
			this.version = version;
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x001A6B90 File Offset: 0x001A4D90
		protected ConfigurationSettingsUnsupportedVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.version = (string)info.GetValue("version", typeof(string));
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x001A6BBA File Offset: 0x001A4DBA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("version", this.version);
		}

		// Token: 0x17002EFC RID: 12028
		// (get) Token: 0x06008199 RID: 33177 RVA: 0x001A6BD5 File Offset: 0x001A4DD5
		public string Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x040055D6 RID: 21974
		private readonly string version;
	}
}
