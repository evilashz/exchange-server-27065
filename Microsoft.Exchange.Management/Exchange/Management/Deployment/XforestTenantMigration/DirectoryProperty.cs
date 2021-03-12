using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D7F RID: 3455
	[Serializable]
	public sealed class DirectoryProperty
	{
		// Token: 0x17002935 RID: 10549
		// (get) Token: 0x0600849A RID: 33946 RVA: 0x0021DF02 File Offset: 0x0021C102
		// (set) Token: 0x0600849B RID: 33947 RVA: 0x0021DF0A File Offset: 0x0021C10A
		public string Name { get; set; }

		// Token: 0x17002936 RID: 10550
		// (get) Token: 0x0600849C RID: 33948 RVA: 0x0021DF13 File Offset: 0x0021C113
		// (set) Token: 0x0600849D RID: 33949 RVA: 0x0021DF1B File Offset: 0x0021C11B
		public List<object> Values { get; set; }

		// Token: 0x17002937 RID: 10551
		// (get) Token: 0x0600849E RID: 33950 RVA: 0x0021DF24 File Offset: 0x0021C124
		// (set) Token: 0x0600849F RID: 33951 RVA: 0x0021DF8C File Offset: 0x0021C18C
		public object Value
		{
			get
			{
				if (this.Values.Count < 1)
				{
					throw new ArgumentNullException("Values");
				}
				if (this.Values.Count > 1)
				{
					throw new ArgumentException(Strings.ErrorMoreThanOneValue(this.Name, this.Values.Count), "Values");
				}
				return this.Values[0];
			}
			set
			{
				if (this.Values.Count < 1)
				{
					throw new ArgumentNullException("Values");
				}
				if (this.Values.Count > 1)
				{
					throw new ArgumentException(Strings.ErrorMoreThanOneValue(this.Name, this.Values.Count), "Values");
				}
				this.Values[0] = value;
			}
		}

		// Token: 0x17002938 RID: 10552
		public object this[int index]
		{
			get
			{
				return this.Values[index];
			}
			set
			{
				this.Values[index] = value;
			}
		}

		// Token: 0x17002939 RID: 10553
		// (get) Token: 0x060084A2 RID: 33954 RVA: 0x0021E010 File Offset: 0x0021C210
		// (set) Token: 0x060084A3 RID: 33955 RVA: 0x0021E018 File Offset: 0x0021C218
		internal bool IsRequired { get; set; }

		// Token: 0x1700293A RID: 10554
		// (get) Token: 0x060084A4 RID: 33956 RVA: 0x0021E021 File Offset: 0x0021C221
		// (set) Token: 0x060084A5 RID: 33957 RVA: 0x0021E029 File Offset: 0x0021C229
		internal bool IsSystemOnly { get; set; }

		// Token: 0x1700293B RID: 10555
		// (get) Token: 0x060084A6 RID: 33958 RVA: 0x0021E032 File Offset: 0x0021C232
		// (set) Token: 0x060084A7 RID: 33959 RVA: 0x0021E03A File Offset: 0x0021C23A
		internal bool IsBackLink { get; set; }

		// Token: 0x1700293C RID: 10556
		// (get) Token: 0x060084A8 RID: 33960 RVA: 0x0021E043 File Offset: 0x0021C243
		// (set) Token: 0x060084A9 RID: 33961 RVA: 0x0021E04B File Offset: 0x0021C24B
		internal bool IsLink { get; set; }

		// Token: 0x1700293D RID: 10557
		// (get) Token: 0x060084AA RID: 33962 RVA: 0x0021E054 File Offset: 0x0021C254
		// (set) Token: 0x060084AB RID: 33963 RVA: 0x0021E05C File Offset: 0x0021C25C
		internal ActiveDirectorySyntax Syntax { get; set; }

		// Token: 0x060084AC RID: 33964 RVA: 0x0021E065 File Offset: 0x0021C265
		public DirectoryProperty()
		{
		}

		// Token: 0x060084AD RID: 33965 RVA: 0x0021E06D File Offset: 0x0021C26D
		public DirectoryProperty(string searchRoot, DictionaryEntry value)
		{
			this.Name = value.Key.ToString();
			this.Values = ((ResultPropertyValueCollection)value.Value).Cast<object>().ToList<object>();
		}
	}
}
