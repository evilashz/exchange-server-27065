using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AFD RID: 2813
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsDriverNotInitializedException : ConfigurationSettingsException
	{
		// Token: 0x0600819A RID: 33178 RVA: 0x001A6BDD File Offset: 0x001A4DDD
		public ConfigurationSettingsDriverNotInitializedException(string id) : base(DirectoryStrings.ConfigurationSettingsDriverNotInitialized(id))
		{
			this.id = id;
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x001A6BF2 File Offset: 0x001A4DF2
		public ConfigurationSettingsDriverNotInitializedException(string id, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsDriverNotInitialized(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x001A6C08 File Offset: 0x001A4E08
		protected ConfigurationSettingsDriverNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x001A6C32 File Offset: 0x001A4E32
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17002EFD RID: 12029
		// (get) Token: 0x0600819E RID: 33182 RVA: 0x001A6C4D File Offset: 0x001A4E4D
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x040055D7 RID: 21975
		private readonly string id;
	}
}
