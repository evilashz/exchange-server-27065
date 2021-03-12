using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF3 RID: 2803
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsGroupExistsException : ConfigurationSettingsException
	{
		// Token: 0x06008166 RID: 33126 RVA: 0x001A6682 File Offset: 0x001A4882
		public ConfigurationSettingsGroupExistsException(string name) : base(DirectoryStrings.ConfigurationSettingsGroupExists(name))
		{
			this.name = name;
		}

		// Token: 0x06008167 RID: 33127 RVA: 0x001A6697 File Offset: 0x001A4897
		public ConfigurationSettingsGroupExistsException(string name, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsGroupExists(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06008168 RID: 33128 RVA: 0x001A66AD File Offset: 0x001A48AD
		protected ConfigurationSettingsGroupExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06008169 RID: 33129 RVA: 0x001A66D7 File Offset: 0x001A48D7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17002EF1 RID: 12017
		// (get) Token: 0x0600816A RID: 33130 RVA: 0x001A66F2 File Offset: 0x001A48F2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040055CB RID: 21963
		private readonly string name;
	}
}
