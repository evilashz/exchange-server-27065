using System;
using System.Reflection;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000F1 RID: 241
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	internal sealed class SimpleConfigurationPropertyAttribute : Attribute
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0001CAA2 File Offset: 0x0001ACA2
		internal SimpleConfigurationPropertyAttribute(string name) : this(name, false)
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001CAAC File Offset: 0x0001ACAC
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

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001CAE8 File Offset: 0x0001ACE8
		internal Type Type
		{
			get
			{
				return this.PropertyInfo.PropertyType;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x0001CAF5 File Offset: 0x0001ACF5
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0001CAFD File Offset: 0x0001ACFD
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

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x0001CB06 File Offset: 0x0001AD06
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x0001CB0E File Offset: 0x0001AD0E
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001CB17 File Offset: 0x0001AD17
		internal bool IsRequired
		{
			get
			{
				return this.isRequired;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001CB1F File Offset: 0x0001AD1F
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001CB27 File Offset: 0x0001AD27
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

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001CB53 File Offset: 0x0001AD53
		internal object GetValue(object entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			return this.propertyInfo.GetValue(entry, null);
		}

		// Token: 0x0400055D RID: 1373
		private readonly string name;

		// Token: 0x0400055E RID: 1374
		private readonly bool isRequired;

		// Token: 0x0400055F RID: 1375
		private ulong propertyMask;

		// Token: 0x04000560 RID: 1376
		private PropertyInfo propertyInfo;
	}
}
