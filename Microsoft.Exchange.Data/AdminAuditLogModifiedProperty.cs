using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	public class AdminAuditLogModifiedProperty
	{
		// Token: 0x06001104 RID: 4356 RVA: 0x00033C95 File Offset: 0x00031E95
		private AdminAuditLogModifiedProperty()
		{
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001105 RID: 4357 RVA: 0x00033C9D File Offset: 0x00031E9D
		// (set) Token: 0x06001106 RID: 4358 RVA: 0x00033CA5 File Offset: 0x00031EA5
		public string Name { get; private set; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x00033CAE File Offset: 0x00031EAE
		// (set) Token: 0x06001108 RID: 4360 RVA: 0x00033CB6 File Offset: 0x00031EB6
		public string NewValue { get; internal set; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00033CBF File Offset: 0x00031EBF
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x00033CC7 File Offset: 0x00031EC7
		public string OldValue { get; internal set; }

		// Token: 0x0600110B RID: 4363 RVA: 0x00033CD0 File Offset: 0x00031ED0
		public static AdminAuditLogModifiedProperty Parse(string propertyValue, bool newValue)
		{
			if (propertyValue == null)
			{
				throw new ArgumentNullException("propertyValue");
			}
			AdminAuditLogModifiedProperty adminAuditLogModifiedProperty = new AdminAuditLogModifiedProperty();
			int num = propertyValue.IndexOf('=');
			if (num > 0)
			{
				adminAuditLogModifiedProperty.Name = propertyValue.Substring(0, num).Trim();
				if (newValue)
				{
					adminAuditLogModifiedProperty.NewValue = propertyValue.Substring(num + 1).Trim();
				}
				else
				{
					adminAuditLogModifiedProperty.OldValue = propertyValue.Substring(num + 1).Trim();
				}
				return adminAuditLogModifiedProperty;
			}
			throw new ArgumentException(DataStrings.AdminAuditLogInvalidParameterOrModifiedProperty(propertyValue));
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00033D52 File Offset: 0x00031F52
		public override int GetHashCode()
		{
			if (this.Name != null)
			{
				return this.Name.ToUpperInvariant().GetHashCode();
			}
			return string.Empty.GetHashCode();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00033D77 File Offset: 0x00031F77
		public override string ToString()
		{
			return this.Name;
		}
	}
}
