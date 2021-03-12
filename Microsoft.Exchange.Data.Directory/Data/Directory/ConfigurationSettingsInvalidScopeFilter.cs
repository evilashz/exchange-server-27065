using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B01 RID: 2817
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsInvalidScopeFilter : ConfigurationSettingsException
	{
		// Token: 0x060081B0 RID: 33200 RVA: 0x001A6E65 File Offset: 0x001A5065
		public ConfigurationSettingsInvalidScopeFilter(string error) : base(DirectoryStrings.ConfigurationSettingsInvalidScopeFilter(error))
		{
			this.error = error;
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x001A6E7A File Offset: 0x001A507A
		public ConfigurationSettingsInvalidScopeFilter(string error, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsInvalidScopeFilter(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x001A6E90 File Offset: 0x001A5090
		protected ConfigurationSettingsInvalidScopeFilter(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x001A6EBA File Offset: 0x001A50BA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17002F03 RID: 12035
		// (get) Token: 0x060081B4 RID: 33204 RVA: 0x001A6ED5 File Offset: 0x001A50D5
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040055DD RID: 21981
		private readonly string error;
	}
}
