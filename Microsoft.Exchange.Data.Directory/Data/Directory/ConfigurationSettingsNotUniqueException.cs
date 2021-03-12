using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF7 RID: 2807
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsNotUniqueException : ConfigurationSettingsException
	{
		// Token: 0x0600817B RID: 33147 RVA: 0x001A68B9 File Offset: 0x001A4AB9
		public ConfigurationSettingsNotUniqueException(string name) : base(DirectoryStrings.ConfigurationSettingsNotUnique(name))
		{
			this.name = name;
		}

		// Token: 0x0600817C RID: 33148 RVA: 0x001A68CE File Offset: 0x001A4ACE
		public ConfigurationSettingsNotUniqueException(string name, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsNotUnique(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600817D RID: 33149 RVA: 0x001A68E4 File Offset: 0x001A4AE4
		protected ConfigurationSettingsNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600817E RID: 33150 RVA: 0x001A690E File Offset: 0x001A4B0E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17002EF6 RID: 12022
		// (get) Token: 0x0600817F RID: 33151 RVA: 0x001A6929 File Offset: 0x001A4B29
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040055D0 RID: 21968
		private readonly string name;
	}
}
