using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000E2 RID: 226
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationProxyRpcArgs
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00033B0D File Offset: 0x00031D0D
		protected MigrationProxyRpcArgs(string userName, string encryptedPassword, string userDomain, MigrationProxyRpcType type)
		{
			this.Type = type;
			this.PropertyCollection = new MdbefPropertyCollection();
			this.UserName = userName;
			this.EncryptedUserPassword = encryptedPassword;
			this.UserDomain = userDomain;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00033B3D File Offset: 0x00031D3D
		protected MigrationProxyRpcArgs(byte[] requestBlob, MigrationProxyRpcType type)
		{
			MigrationUtil.ThrowOnNullArgument(requestBlob, "requestBlob");
			this.Type = type;
			this.PropertyCollection = MdbefPropertyCollection.Create(requestBlob, 0, requestBlob.Length);
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x00033B67 File Offset: 0x00031D67
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00033B74 File Offset: 0x00031D74
		public string UserName
		{
			get
			{
				return this.GetProperty<string>(2416115743U);
			}
			set
			{
				this.SetPropertyAsString(2416115743U, value);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00033B82 File Offset: 0x00031D82
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00033B8F File Offset: 0x00031D8F
		public string UserDomain
		{
			get
			{
				return this.GetProperty<string>(2416181279U);
			}
			set
			{
				this.SetPropertyAsString(2416181279U, value);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x00033B9D File Offset: 0x00031D9D
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x00033BAA File Offset: 0x00031DAA
		public string EncryptedUserPassword
		{
			get
			{
				return this.GetProperty<string>(2416246815U);
			}
			set
			{
				this.SetPropertyAsString(2416246815U, value);
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00033BB8 File Offset: 0x00031DB8
		public byte[] GetBytes()
		{
			return this.PropertyCollection.GetBytes();
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00033BC5 File Offset: 0x00031DC5
		public virtual bool Validate(out string errorMsg)
		{
			if (string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.EncryptedUserPassword))
			{
				errorMsg = "User Name or password cannot be null or empty.";
				return false;
			}
			errorMsg = null;
			return true;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00033BF0 File Offset: 0x00031DF0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<Request Type='{0}'>", this.Type);
			foreach (KeyValuePair<uint, object> keyValuePair in this.PropertyCollection)
			{
				object arg = keyValuePair.Value;
				if (keyValuePair.Key == 2416246815U)
				{
					arg = "*****";
				}
				else if (keyValuePair.Key == 2416447508U)
				{
					int num = (keyValuePair.Value == null) ? 0 : ((long[])keyValuePair.Value).Length;
					arg = string.Format("{0} PropTag(s)", num);
				}
				stringBuilder.AppendFormat("<Argument Key='{0}' Value='{1}' />", keyValuePair.Key, arg);
			}
			stringBuilder.AppendFormat("</Request>", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00033CE4 File Offset: 0x00031EE4
		protected T GetProperty<T>(uint key) where T : class
		{
			object obj;
			if (this.PropertyCollection.TryGetValue(key, out obj))
			{
				return (T)((object)obj);
			}
			return default(T);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00033D11 File Offset: 0x00031F11
		protected void SetProperty(uint key, object value)
		{
			if (value != null)
			{
				this.PropertyCollection[key] = value;
				return;
			}
			this.PropertyCollection.Remove(key);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00033D31 File Offset: 0x00031F31
		protected void SetPropertyAsString(uint key, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.SetProperty(key, null);
				return;
			}
			this.SetProperty(key, value);
		}

		// Token: 0x04000499 RID: 1177
		public readonly MigrationProxyRpcType Type;

		// Token: 0x0400049A RID: 1178
		protected readonly MdbefPropertyCollection PropertyCollection;
	}
}
