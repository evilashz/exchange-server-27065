using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F2 RID: 242
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsScopePropertyNotFoundException : ConfigurationSettingsException
	{
		// Token: 0x0600085A RID: 2138 RVA: 0x0001B6E5 File Offset: 0x000198E5
		public ConfigurationSettingsScopePropertyNotFoundException(string name, string knownScopes) : base(DataStrings.ConfigurationSettingsScopePropertyNotFound2(name, knownScopes))
		{
			this.name = name;
			this.knownScopes = knownScopes;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001B702 File Offset: 0x00019902
		public ConfigurationSettingsScopePropertyNotFoundException(string name, string knownScopes, Exception innerException) : base(DataStrings.ConfigurationSettingsScopePropertyNotFound2(name, knownScopes), innerException)
		{
			this.name = name;
			this.knownScopes = knownScopes;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0001B720 File Offset: 0x00019920
		protected ConfigurationSettingsScopePropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.knownScopes = (string)info.GetValue("knownScopes", typeof(string));
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001B775 File Offset: 0x00019975
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("knownScopes", this.knownScopes);
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001B7A1 File Offset: 0x000199A1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0001B7A9 File Offset: 0x000199A9
		public string KnownScopes
		{
			get
			{
				return this.knownScopes;
			}
		}

		// Token: 0x04000598 RID: 1432
		private readonly string name;

		// Token: 0x04000599 RID: 1433
		private readonly string knownScopes;
	}
}
