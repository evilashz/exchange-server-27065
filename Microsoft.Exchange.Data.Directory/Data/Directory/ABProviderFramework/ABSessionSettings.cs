using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ABProviderFramework
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ABSessionSettings : IABSessionSettings
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003E5F File Offset: 0x0000205F
		public void Set(string propertyName, object propertyValue)
		{
			this.settings[propertyName] = propertyValue;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003E70 File Offset: 0x00002070
		public bool TryGet<T>(string propertyName, out T propertyValue)
		{
			object obj;
			if (this.settings.TryGetValue(propertyName, out obj))
			{
				propertyValue = (T)((object)obj);
				return true;
			}
			propertyValue = default(T);
			return false;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003EA3 File Offset: 0x000020A3
		public T Get<T>(string propertyName)
		{
			return (T)((object)this.settings[propertyName]);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003EB8 File Offset: 0x000020B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ABSessionSettings = ");
			if (this.settings.Count == 0)
			{
				stringBuilder.AppendLine("{}");
			}
			else
			{
				stringBuilder.AppendLine("{");
				foreach (KeyValuePair<string, object> keyValuePair in this.settings)
				{
					stringBuilder.AppendFormat("  {0} = {1}{2}", keyValuePair.Key, (keyValuePair.Value == null) ? "<null>" : keyValuePair.Value.ToString(), Environment.NewLine);
				}
				stringBuilder.AppendLine("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000036 RID: 54
		public const string Provider = "Provider";

		// Token: 0x04000037 RID: 55
		public const string UserName = "UserName";

		// Token: 0x04000038 RID: 56
		public const string Password = "Password";

		// Token: 0x04000039 RID: 57
		public const string Domain = "Domain";

		// Token: 0x0400003A RID: 58
		public const string Email = "Email";

		// Token: 0x0400003B RID: 59
		public const string Uri = "Uri";

		// Token: 0x0400003C RID: 60
		public const string OrganizationId = "OrganizationId";

		// Token: 0x0400003D RID: 61
		public const string SearchRoot = "SearchRoot";

		// Token: 0x0400003E RID: 62
		public const string Lcid = "Lcid";

		// Token: 0x0400003F RID: 63
		public const string ConsistencyMode = "ConsistencyMode";

		// Token: 0x04000040 RID: 64
		public const string ClientSecurityContext = "ClientSecurityContext";

		// Token: 0x04000041 RID: 65
		public const string Disabled = "Disabled";

		// Token: 0x04000042 RID: 66
		public const string SubscriptionGuid = "SubscriptionGuid";

		// Token: 0x04000043 RID: 67
		public const string Subscription = "Subscription";

		// Token: 0x04000044 RID: 68
		public const string SyncLog = "SyncLog";

		// Token: 0x04000045 RID: 69
		public const string ExchangeVersion = "ExchangeVersion";

		// Token: 0x04000046 RID: 70
		private Dictionary<string, object> settings = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
	}
}
