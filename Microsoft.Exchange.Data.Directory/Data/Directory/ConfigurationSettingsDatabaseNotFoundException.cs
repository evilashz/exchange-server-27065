using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF2 RID: 2802
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsDatabaseNotFoundException : ConfigurationSettingsException
	{
		// Token: 0x06008161 RID: 33121 RVA: 0x001A660A File Offset: 0x001A480A
		public ConfigurationSettingsDatabaseNotFoundException(string id) : base(DirectoryStrings.ConfigurationSettingsDatabaseNotFound(id))
		{
			this.id = id;
		}

		// Token: 0x06008162 RID: 33122 RVA: 0x001A661F File Offset: 0x001A481F
		public ConfigurationSettingsDatabaseNotFoundException(string id, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsDatabaseNotFound(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06008163 RID: 33123 RVA: 0x001A6635 File Offset: 0x001A4835
		protected ConfigurationSettingsDatabaseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x06008164 RID: 33124 RVA: 0x001A665F File Offset: 0x001A485F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17002EF0 RID: 12016
		// (get) Token: 0x06008165 RID: 33125 RVA: 0x001A667A File Offset: 0x001A487A
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x040055CA RID: 21962
		private readonly string id;
	}
}
