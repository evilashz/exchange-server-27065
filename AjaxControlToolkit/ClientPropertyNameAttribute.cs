using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ClientPropertyNameAttribute : Attribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000216C File Offset: 0x0000036C
		public ClientPropertyNameAttribute()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002174 File Offset: 0x00000374
		public ClientPropertyNameAttribute(string propertyName)
		{
			this.propertyName = propertyName;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002183 File Offset: 0x00000383
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218B File Offset: 0x0000038B
		public override bool IsDefaultAttribute()
		{
			return string.IsNullOrEmpty(this.PropertyName);
		}

		// Token: 0x04000003 RID: 3
		private string propertyName;
	}
}
