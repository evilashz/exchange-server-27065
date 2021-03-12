using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AFA RID: 2810
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsRestrictionNotExpectedException : ConfigurationSettingsException
	{
		// Token: 0x0600818A RID: 33162 RVA: 0x001A6A21 File Offset: 0x001A4C21
		public ConfigurationSettingsRestrictionNotExpectedException(string name) : base(DirectoryStrings.ConfigurationSettingsRestrictionNotExpected(name))
		{
			this.name = name;
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x001A6A36 File Offset: 0x001A4C36
		public ConfigurationSettingsRestrictionNotExpectedException(string name, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsRestrictionNotExpected(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x001A6A4C File Offset: 0x001A4C4C
		protected ConfigurationSettingsRestrictionNotExpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x001A6A76 File Offset: 0x001A4C76
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17002EF9 RID: 12025
		// (get) Token: 0x0600818E RID: 33166 RVA: 0x001A6A91 File Offset: 0x001A4C91
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040055D3 RID: 21971
		private readonly string name;
	}
}
