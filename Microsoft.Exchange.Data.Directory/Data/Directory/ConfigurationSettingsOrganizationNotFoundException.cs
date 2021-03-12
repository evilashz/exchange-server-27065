using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF8 RID: 2808
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsOrganizationNotFoundException : ConfigurationSettingsException
	{
		// Token: 0x06008180 RID: 33152 RVA: 0x001A6931 File Offset: 0x001A4B31
		public ConfigurationSettingsOrganizationNotFoundException(string id) : base(DirectoryStrings.ConfigurationSettingsOrganizationNotFound(id))
		{
			this.id = id;
		}

		// Token: 0x06008181 RID: 33153 RVA: 0x001A6946 File Offset: 0x001A4B46
		public ConfigurationSettingsOrganizationNotFoundException(string id, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsOrganizationNotFound(id), innerException)
		{
			this.id = id;
		}

		// Token: 0x06008182 RID: 33154 RVA: 0x001A695C File Offset: 0x001A4B5C
		protected ConfigurationSettingsOrganizationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.id = (string)info.GetValue("id", typeof(string));
		}

		// Token: 0x06008183 RID: 33155 RVA: 0x001A6986 File Offset: 0x001A4B86
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("id", this.id);
		}

		// Token: 0x17002EF7 RID: 12023
		// (get) Token: 0x06008184 RID: 33156 RVA: 0x001A69A1 File Offset: 0x001A4BA1
		public string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x040055D1 RID: 21969
		private readonly string id;
	}
}
