using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.Security.Permissions
{
	// Token: 0x020002E8 RID: 744
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermissionAccessEntry
	{
		// Token: 0x06002696 RID: 9878 RVA: 0x0008BCC6 File Offset: 0x00089EC6
		internal KeyContainerPermissionAccessEntry(KeyContainerPermissionAccessEntry accessEntry) : this(accessEntry.KeyStore, accessEntry.ProviderName, accessEntry.ProviderType, accessEntry.KeyContainerName, accessEntry.KeySpec, accessEntry.Flags)
		{
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0008BCF2 File Offset: 0x00089EF2
		public KeyContainerPermissionAccessEntry(string keyContainerName, KeyContainerPermissionFlags flags) : this(null, null, -1, keyContainerName, -1, flags)
		{
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0008BD00 File Offset: 0x00089F00
		public KeyContainerPermissionAccessEntry(CspParameters parameters, KeyContainerPermissionFlags flags) : this(((parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore) ? "Machine" : "User", parameters.ProviderName, parameters.ProviderType, parameters.KeyContainerName, parameters.KeyNumber, flags)
		{
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0008BD38 File Offset: 0x00089F38
		public KeyContainerPermissionAccessEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec, KeyContainerPermissionFlags flags)
		{
			this.m_providerName = ((providerName == null) ? "*" : providerName);
			this.m_providerType = providerType;
			this.m_keyContainerName = ((keyContainerName == null) ? "*" : keyContainerName);
			this.m_keySpec = keySpec;
			this.KeyStore = keyStore;
			this.Flags = flags;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x0008BD8D File Offset: 0x00089F8D
		// (set) Token: 0x0600269B RID: 9883 RVA: 0x0008BD98 File Offset: 0x00089F98
		public string KeyStore
		{
			get
			{
				return this.m_keyStore;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(value, this.ProviderName, this.ProviderType, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_keyStore = "*";
					return;
				}
				if (value != "User" && value != "Machine" && value != "*")
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidKeyStore", new object[]
					{
						value
					}), "value");
				}
				this.m_keyStore = value;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x0008BE31 File Offset: 0x0008A031
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x0008BE3C File Offset: 0x0008A03C
		public string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, value, this.ProviderType, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_providerName = "*";
					return;
				}
				this.m_providerName = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x0008BE8F File Offset: 0x0008A08F
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x0008BE97 File Offset: 0x0008A097
		public int ProviderType
		{
			get
			{
				return this.m_providerType;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, value, this.KeyContainerName, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				this.m_providerType = value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x0008BED0 File Offset: 0x0008A0D0
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x0008BED8 File Offset: 0x0008A0D8
		public string KeyContainerName
		{
			get
			{
				return this.m_keyContainerName;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, value, this.KeySpec))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				if (value == null)
				{
					this.m_keyContainerName = "*";
					return;
				}
				this.m_keyContainerName = value;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0008BF2B File Offset: 0x0008A12B
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x0008BF33 File Offset: 0x0008A133
		public int KeySpec
		{
			get
			{
				return this.m_keySpec;
			}
			set
			{
				if (KeyContainerPermissionAccessEntry.IsUnrestrictedEntry(this.KeyStore, this.ProviderName, this.ProviderType, this.KeyContainerName, value))
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_InvalidAccessEntry"));
				}
				this.m_keySpec = value;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x0008BF6C File Offset: 0x0008A16C
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x0008BF74 File Offset: 0x0008A174
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				KeyContainerPermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x0008BF84 File Offset: 0x0008A184
		public override bool Equals(object o)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = o as KeyContainerPermissionAccessEntry;
			return keyContainerPermissionAccessEntry != null && !(keyContainerPermissionAccessEntry.m_keyStore != this.m_keyStore) && !(keyContainerPermissionAccessEntry.m_providerName != this.m_providerName) && keyContainerPermissionAccessEntry.m_providerType == this.m_providerType && !(keyContainerPermissionAccessEntry.m_keyContainerName != this.m_keyContainerName) && keyContainerPermissionAccessEntry.m_keySpec == this.m_keySpec;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x0008C000 File Offset: 0x0008A200
		public override int GetHashCode()
		{
			int num = 0;
			num |= (this.m_keyStore.GetHashCode() & 255) << 24;
			num |= (this.m_providerName.GetHashCode() & 255) << 16;
			num |= (this.m_providerType & 15) << 12;
			num |= (this.m_keyContainerName.GetHashCode() & 255) << 4;
			return num | (this.m_keySpec & 15);
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0008C070 File Offset: 0x0008A270
		internal bool IsSubsetOf(KeyContainerPermissionAccessEntry target)
		{
			return (!(target.m_keyStore != "*") || !(this.m_keyStore != target.m_keyStore)) && (!(target.m_providerName != "*") || !(this.m_providerName != target.m_providerName)) && (target.m_providerType == -1 || this.m_providerType == target.m_providerType) && (!(target.m_keyContainerName != "*") || !(this.m_keyContainerName != target.m_keyContainerName)) && (target.m_keySpec == -1 || this.m_keySpec == target.m_keySpec);
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x0008C128 File Offset: 0x0008A328
		internal static bool IsUnrestrictedEntry(string keyStore, string providerName, int providerType, string keyContainerName, int keySpec)
		{
			return (!(keyStore != "*") || keyStore == null) && (!(providerName != "*") || providerName == null) && providerType == -1 && (!(keyContainerName != "*") || keyContainerName == null) && keySpec == -1;
		}

		// Token: 0x04000EAE RID: 3758
		private string m_keyStore;

		// Token: 0x04000EAF RID: 3759
		private string m_providerName;

		// Token: 0x04000EB0 RID: 3760
		private int m_providerType;

		// Token: 0x04000EB1 RID: 3761
		private string m_keyContainerName;

		// Token: 0x04000EB2 RID: 3762
		private int m_keySpec;

		// Token: 0x04000EB3 RID: 3763
		private KeyContainerPermissionFlags m_flags;
	}
}
