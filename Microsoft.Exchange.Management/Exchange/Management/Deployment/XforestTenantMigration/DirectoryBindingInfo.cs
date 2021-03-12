using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Net;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D7C RID: 3452
	internal sealed class DirectoryBindingInfo
	{
		// Token: 0x17002928 RID: 10536
		// (get) Token: 0x0600847B RID: 33915 RVA: 0x0021D998 File Offset: 0x0021BB98
		public string SchemaNamingContextDN
		{
			get
			{
				return this.ldapPathToSchema;
			}
		}

		// Token: 0x17002929 RID: 10537
		// (get) Token: 0x0600847C RID: 33916 RVA: 0x0021D9A0 File Offset: 0x0021BBA0
		// (set) Token: 0x0600847D RID: 33917 RVA: 0x0021D9A8 File Offset: 0x0021BBA8
		public NetworkCredential Credential { get; set; }

		// Token: 0x1700292A RID: 10538
		// (get) Token: 0x0600847E RID: 33918 RVA: 0x0021D9B1 File Offset: 0x0021BBB1
		public string DefaultNamingContextDN
		{
			get
			{
				return this.ldapPathToDefault;
			}
		}

		// Token: 0x1700292B RID: 10539
		// (get) Token: 0x0600847F RID: 33919 RVA: 0x0021D9B9 File Offset: 0x0021BBB9
		public string ConfigurationNamingContextDN
		{
			get
			{
				return this.ldapPathToConfiguration;
			}
		}

		// Token: 0x1700292C RID: 10540
		// (get) Token: 0x06008480 RID: 33920 RVA: 0x0021D9C1 File Offset: 0x0021BBC1
		public string LdapBasePath
		{
			get
			{
				return "LDAP://";
			}
		}

		// Token: 0x06008481 RID: 33921 RVA: 0x0021D9C8 File Offset: 0x0021BBC8
		public DirectoryBindingInfo()
		{
			this.ldapPathToSchema = this.GetRootDseProperty("schemaNamingContext");
			this.ldapPathToConfiguration = this.GetRootDseProperty("configurationNamingContext");
			this.ldapPathToDefault = this.GetRootDseProperty("defaultNamingContext");
		}

		// Token: 0x06008482 RID: 33922 RVA: 0x0021DA04 File Offset: 0x0021BC04
		public DirectoryBindingInfo(NetworkCredential credential)
		{
			this.Credential = credential;
			this.ldapPathToSchema = this.GetRootDseProperty("schemaNamingContext");
			this.ldapPathToConfiguration = this.GetRootDseProperty("configurationNamingContext");
			this.ldapPathToDefault = this.GetRootDseProperty("defaultNamingContext");
		}

		// Token: 0x06008483 RID: 33923 RVA: 0x0021DA54 File Offset: 0x0021BC54
		private string GetRootDseProperty(string name)
		{
			string result;
			using (DirectoryEntry rootDse = this.GetRootDse())
			{
				result = rootDse.Properties[name][0].ToString();
			}
			return result;
		}

		// Token: 0x06008484 RID: 33924 RVA: 0x0021DAA0 File Offset: 0x0021BCA0
		private DirectoryEntry GetRootDse()
		{
			string ldapPath;
			if (this.Credential != null && this.Credential.Domain != null)
			{
				ldapPath = string.Format("{0}{1}/RootDSE", this.LdapBasePath, this.Credential.Domain);
			}
			else
			{
				ldapPath = string.Format("{0}RootDSE", this.LdapBasePath);
			}
			return this.GetDirectoryEntry(ldapPath);
		}

		// Token: 0x06008485 RID: 33925 RVA: 0x0021DAF8 File Offset: 0x0021BCF8
		public DirectoryEntry GetDirectoryEntry(string ldapPath)
		{
			if (this.Credential != null && this.Credential.UserName != null && this.Credential.Password != null)
			{
				return new DirectoryEntry(ldapPath, (this.Credential.Domain == null) ? this.Credential.UserName : (this.Credential.Domain + "\\" + this.Credential.UserName), this.Credential.Password);
			}
			return new DirectoryEntry(ldapPath);
		}

		// Token: 0x06008486 RID: 33926 RVA: 0x0021DB7C File Offset: 0x0021BD7C
		public DirectoryContext GetDirectoryContext(DirectoryContextType contextType)
		{
			if (this.Credential != null && this.Credential.UserName != null && this.Credential.Password != null)
			{
				return new DirectoryContext(contextType, this.Credential.Domain, this.Credential.UserName, this.Credential.Password);
			}
			return new DirectoryContext(contextType);
		}

		// Token: 0x04004022 RID: 16418
		private readonly string ldapPathToSchema;

		// Token: 0x04004023 RID: 16419
		private readonly string ldapPathToDefault;

		// Token: 0x04004024 RID: 16420
		private readonly string ldapPathToConfiguration;
	}
}
