using System;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000009 RID: 9
	public class AuditUploaderDictionaryKey : IEquatable<AuditUploaderDictionaryKey>
	{
		// Token: 0x060000AB RID: 171 RVA: 0x0000390C File Offset: 0x00001B0C
		public AuditUploaderDictionaryKey(string component, string tenant, string user, string operation)
		{
			this.Component = ((component == null) ? AuditUploaderDictionaryKey.WildCard : component.ToLower());
			this.Tenant = ((tenant == null) ? AuditUploaderDictionaryKey.WildCard : tenant.ToLower());
			this.User = ((user == null) ? AuditUploaderDictionaryKey.WildCard : user.ToLower());
			this.Operation = ((operation == null) ? AuditUploaderDictionaryKey.WildCard : operation.ToLower());
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003979 File Offset: 0x00001B79
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003981 File Offset: 0x00001B81
		public string Component { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000398A File Offset: 0x00001B8A
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003992 File Offset: 0x00001B92
		public string Tenant { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000399B File Offset: 0x00001B9B
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000039A3 File Offset: 0x00001BA3
		public string User { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000039AC File Offset: 0x00001BAC
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000039B4 File Offset: 0x00001BB4
		public string Operation { get; set; }

		// Token: 0x17000068 RID: 104
		public string this[int i]
		{
			set
			{
				switch (i)
				{
				case 0:
					this.Component = value;
					return;
				case 1:
					this.Tenant = value;
					return;
				case 2:
					this.User = value;
					return;
				case 3:
					this.Operation = value;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003A05 File Offset: 0x00001C05
		public override int GetHashCode()
		{
			return this.Component.GetHashCode() ^ this.Tenant.GetHashCode() ^ this.User.GetHashCode() ^ this.Operation.GetHashCode();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A38 File Offset: 0x00001C38
		public bool Equals(AuditUploaderDictionaryKey key)
		{
			return key.Component.Equals(this.Component, StringComparison.OrdinalIgnoreCase) && key.Tenant.Equals(this.Tenant, StringComparison.OrdinalIgnoreCase) && key.User.Equals(this.User, StringComparison.OrdinalIgnoreCase) && key.Operation.Equals(this.Operation, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003A95 File Offset: 0x00001C95
		public override bool Equals(object key)
		{
			return this.Equals(key as AuditUploaderDictionaryKey);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003AA3 File Offset: 0x00001CA3
		public void CopyFrom(AuditUploaderDictionaryKey source)
		{
			this.Component = source.Component;
			this.Tenant = source.Tenant;
			this.User = source.User;
			this.Operation = source.Operation;
		}

		// Token: 0x04000040 RID: 64
		public static readonly int NumberOfFields = 4;

		// Token: 0x04000041 RID: 65
		public static readonly string WildCard = "*";
	}
}
