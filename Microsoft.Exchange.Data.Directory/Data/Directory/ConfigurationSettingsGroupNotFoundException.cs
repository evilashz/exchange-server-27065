using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF4 RID: 2804
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsGroupNotFoundException : ConfigurationSettingsException
	{
		// Token: 0x0600816B RID: 33131 RVA: 0x001A66FA File Offset: 0x001A48FA
		public ConfigurationSettingsGroupNotFoundException(string name) : base(DirectoryStrings.ConfigurationSettingsGroupNotFound(name))
		{
			this.name = name;
		}

		// Token: 0x0600816C RID: 33132 RVA: 0x001A670F File Offset: 0x001A490F
		public ConfigurationSettingsGroupNotFoundException(string name, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsGroupNotFound(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600816D RID: 33133 RVA: 0x001A6725 File Offset: 0x001A4925
		protected ConfigurationSettingsGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600816E RID: 33134 RVA: 0x001A674F File Offset: 0x001A494F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17002EF2 RID: 12018
		// (get) Token: 0x0600816F RID: 33135 RVA: 0x001A676A File Offset: 0x001A496A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040055CC RID: 21964
		private readonly string name;
	}
}
