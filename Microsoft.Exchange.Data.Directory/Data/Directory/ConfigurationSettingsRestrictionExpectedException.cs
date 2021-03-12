using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AF9 RID: 2809
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsRestrictionExpectedException : ConfigurationSettingsException
	{
		// Token: 0x06008185 RID: 33157 RVA: 0x001A69A9 File Offset: 0x001A4BA9
		public ConfigurationSettingsRestrictionExpectedException(string name) : base(DirectoryStrings.ConfigurationSettingsRestrictionExpected(name))
		{
			this.name = name;
		}

		// Token: 0x06008186 RID: 33158 RVA: 0x001A69BE File Offset: 0x001A4BBE
		public ConfigurationSettingsRestrictionExpectedException(string name, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsRestrictionExpected(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x06008187 RID: 33159 RVA: 0x001A69D4 File Offset: 0x001A4BD4
		protected ConfigurationSettingsRestrictionExpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x001A69FE File Offset: 0x001A4BFE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17002EF8 RID: 12024
		// (get) Token: 0x06008189 RID: 33161 RVA: 0x001A6A19 File Offset: 0x001A4C19
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040055D2 RID: 21970
		private readonly string name;
	}
}
