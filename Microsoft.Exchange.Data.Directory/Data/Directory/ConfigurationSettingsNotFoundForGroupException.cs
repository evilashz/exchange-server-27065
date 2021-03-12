using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF6 RID: 2806
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsNotFoundForGroupException : ConfigurationSettingsException
	{
		// Token: 0x06008175 RID: 33141 RVA: 0x001A67EA File Offset: 0x001A49EA
		public ConfigurationSettingsNotFoundForGroupException(string groupName, string key) : base(DirectoryStrings.ConfigurationSettingsNotFoundForGroup(groupName, key))
		{
			this.groupName = groupName;
			this.key = key;
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x001A6807 File Offset: 0x001A4A07
		public ConfigurationSettingsNotFoundForGroupException(string groupName, string key, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsNotFoundForGroup(groupName, key), innerException)
		{
			this.groupName = groupName;
			this.key = key;
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x001A6828 File Offset: 0x001A4A28
		protected ConfigurationSettingsNotFoundForGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.groupName = (string)info.GetValue("groupName", typeof(string));
			this.key = (string)info.GetValue("key", typeof(string));
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x001A687D File Offset: 0x001A4A7D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("groupName", this.groupName);
			info.AddValue("key", this.key);
		}

		// Token: 0x17002EF4 RID: 12020
		// (get) Token: 0x06008179 RID: 33145 RVA: 0x001A68A9 File Offset: 0x001A4AA9
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x17002EF5 RID: 12021
		// (get) Token: 0x0600817A RID: 33146 RVA: 0x001A68B1 File Offset: 0x001A4AB1
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x040055CE RID: 21966
		private readonly string groupName;

		// Token: 0x040055CF RID: 21967
		private readonly string key;
	}
}
