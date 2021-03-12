using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001E9 RID: 489
	[Serializable]
	public class AdminAuditLogCmdletParameter
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x00033825 File Offset: 0x00031A25
		private AdminAuditLogCmdletParameter()
		{
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0003382D File Offset: 0x00031A2D
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00033835 File Offset: 0x00031A35
		public string Name { get; private set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0003383E File Offset: 0x00031A3E
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00033846 File Offset: 0x00031A46
		public string Value { get; private set; }

		// Token: 0x060010E7 RID: 4327 RVA: 0x00033850 File Offset: 0x00031A50
		public static AdminAuditLogCmdletParameter Parse(string propertyValue)
		{
			if (propertyValue == null)
			{
				throw new ArgumentNullException("propertyValue");
			}
			AdminAuditLogCmdletParameter adminAuditLogCmdletParameter = new AdminAuditLogCmdletParameter();
			int num = propertyValue.IndexOf('=');
			if (num > 0)
			{
				adminAuditLogCmdletParameter.Name = propertyValue.Substring(0, num).Trim();
				adminAuditLogCmdletParameter.Value = propertyValue.Substring(num + 1).Trim();
				return adminAuditLogCmdletParameter;
			}
			throw new ArgumentException(DataStrings.AdminAuditLogInvalidParameterOrModifiedProperty(propertyValue));
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000338B9 File Offset: 0x00031AB9
		public override int GetHashCode()
		{
			if (this.Name != null)
			{
				return this.Name.ToUpperInvariant().GetHashCode();
			}
			return string.Empty.GetHashCode();
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000338DE File Offset: 0x00031ADE
		public override string ToString()
		{
			return this.Name;
		}
	}
}
