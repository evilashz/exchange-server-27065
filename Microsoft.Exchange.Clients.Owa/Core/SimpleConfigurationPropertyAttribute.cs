using System;
using System.Reflection;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000250 RID: 592
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal sealed class SimpleConfigurationPropertyAttribute : Attribute
	{
		// Token: 0x060013E0 RID: 5088 RVA: 0x0007A043 File Offset: 0x00078243
		internal SimpleConfigurationPropertyAttribute(string name) : this(name, false)
		{
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0007A04D File Offset: 0x0007824D
		internal SimpleConfigurationPropertyAttribute(string name, bool isRequired)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentOutOfRangeException("name", "name should not be empty");
			}
			this.name = name;
			this.isRequired = isRequired;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0007A089 File Offset: 0x00078289
		internal void SetValue(object entry, object value)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.propertyInfo.SetValue(entry, value, null);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0007A0B5 File Offset: 0x000782B5
		internal object GetValue(object entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			return this.propertyInfo.GetValue(entry, null);
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0007A0D2 File Offset: 0x000782D2
		internal Type Type
		{
			get
			{
				return this.PropertyInfo.PropertyType;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0007A0DF File Offset: 0x000782DF
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0007A0E7 File Offset: 0x000782E7
		internal PropertyInfo PropertyInfo
		{
			get
			{
				return this.propertyInfo;
			}
			set
			{
				this.propertyInfo = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0007A0F0 File Offset: 0x000782F0
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0007A0F8 File Offset: 0x000782F8
		internal ulong PropertyMask
		{
			get
			{
				return this.propertyMask;
			}
			set
			{
				this.propertyMask = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0007A101 File Offset: 0x00078301
		internal bool IsRequired
		{
			get
			{
				return this.isRequired;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0007A109 File Offset: 0x00078309
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000DA7 RID: 3495
		private string name;

		// Token: 0x04000DA8 RID: 3496
		private bool isRequired;

		// Token: 0x04000DA9 RID: 3497
		private ulong propertyMask;

		// Token: 0x04000DAA RID: 3498
		private PropertyInfo propertyInfo;
	}
}
